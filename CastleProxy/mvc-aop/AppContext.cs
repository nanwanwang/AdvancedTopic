using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using Castle.DynamicProxy;

namespace CastleProxy;

public class AppContext
{
    private static readonly Type ControllerType = typeof(Controller);
    private static readonly Dictionary<string, Type> matchedControllerTypes = new Dictionary<string, Type>();

    private static readonly Dictionary<string, MethodInfo> matchedControllerActions =
        new Dictionary<string, MethodInfo>();

    private Dictionary<string, string[]> routeTemplates = new Dictionary<string, string[]>();

    public void AddExecRouteTemplate(string execRouteTemplate)
    {
        if (!Regex.IsMatch(execRouteTemplate, "{controller}", RegexOptions.IgnoreCase))
        {
            throw new ArgumentException("执行路由模板不正确，缺少{controller}");
        }
 
        if (!Regex.IsMatch(execRouteTemplate, "{action}", RegexOptions.IgnoreCase))
        {
            throw new ArgumentException("执行路由模板不正确，缺少{action}");
        }
 
        string[] keys = Regex.Matches(execRouteTemplate, @"(?<={)\w+(?=})", RegexOptions.IgnoreCase).Cast<Match>().Select(c => c.Value.ToLower()).ToArray();
 
        routeTemplates.Add(execRouteTemplate,keys);
    }

    public object Run(string execRoute)
    {
        // {controller}/{action}/{id}
        string controller = null;
        string actionName = null;
        ArrayList args = null;
        Type controllerType = null;
        bool findResult = false;

        foreach (var r in routeTemplates)
        {
            string[] keys = r.Value;
            string execRoutePattern = Regex.Replace(r.Key, @"{(?<key>\w+)}",
                (m) => string.Format(@"(?<{0}>.[^/\\]+)", m.Groups["key"].Value.ToLower()), RegexOptions.IgnoreCase);
            args = new ArrayList();
            if (Regex.IsMatch(execRoute, execRoutePattern))
            {
                var match = Regex.Match(execRoute, execRoutePattern);
                for (int i = 0; i < keys.Length; i++)
                {
                    if ("controller".Equals(keys[i], StringComparison.OrdinalIgnoreCase))
                    {
                        controller = match.Groups["controller"].Value;
                    }
                    else if ("action".Equals(keys[i], StringComparison.OrdinalIgnoreCase))
                    {
                        actionName = match.Groups["action"].Value;
                    }
                    else
                    {
                        args.Add(match.Groups[keys[i]].Value);
                    }
                }
                
                if ((controllerType = FindControllerType(controller)) != null && FindAction(controllerType, actionName, args.ToArray()) != null)
                {
                    findResult = true;
                    break;
                }
            }
        }
        if (findResult)
        {
            return Process(controller, actionName, args.ToArray());
        }
        else
        {
            throw new Exception($"在已配置的路由模板列表中未找到与该执行路由相匹配的路由信息：{execRoute}");
        }
    }

    public object Process(string controller, string actionName, params object[] args)
    {
         Type matchedControllerType = FindControllerType(controller);
 
        if (matchedControllerType == null)
        {
            throw new ArgumentException($"未找到类型为{controller}的Controller类型");
        }
 
        object execResult = null;
        if (matchedControllerType != null)
        {
            var matchedController = (Controller)Activator.CreateInstance(matchedControllerType);
            MethodInfo action = FindAction(matchedControllerType, actionName, args);
            if (action == null)
            {
                throw new ArgumentException($"在{matchedControllerType.FullName}中未找到与方法名：{actionName}及参数个数：{args.Count()}相匹配的方法");
            }
 
 
            var filters = action.GetCustomAttributes<FilterAttribute>(true);
            List<FilterAttribute> execBeforeFilters = new List<FilterAttribute>();
            List<FilterAttribute> execAfterFilters = new List<FilterAttribute>();
            List<FilterAttribute> exceptionFilters = new List<FilterAttribute>();
 
            if (filters != null && filters.Count() > 0)
            {
                execBeforeFilters = filters.Where(f => f.FilterType == "BEFORE").ToList();
                execAfterFilters = filters.Where(f => f.FilterType == "AFTER").ToList();
                exceptionFilters = filters.Where(f => f.FilterType == "EXCEPTION").ToList();
            }
 
            try
            {
                matchedController.OnActionExecuting(action);
 
                if (execBeforeFilters != null && execBeforeFilters.Count > 0)
                {
                    execBeforeFilters.ForEach(f => f.Exec(matchedController, null));
                }
 
                var mParams = action.GetParameters();
                object[] newArgs = new object[args.Length];
                for (int i = 0; i < mParams.Length; i++)
                {
                    newArgs[i] = Convert.ChangeType(args[i], mParams[i].ParameterType);
                }
 
                execResult = action.Invoke(matchedController, newArgs);
 
                matchedController.OnActionExecuted(action);
 
                if (execBeforeFilters != null && execBeforeFilters.Count > 0)
                {
                    execAfterFilters.ForEach(f => f.Exec(matchedController, null));
                }
 
            }
            catch (Exception ex)
            {
                matchedController.OnActionError(action, ex);
 
                if (exceptionFilters != null && exceptionFilters.Count > 0)
                {
                    exceptionFilters.ForEach(f => f.Exec(matchedController, ex));
                }
            }
 
 
        }
 
        return execResult;
    }

    private Type FindControllerType(string ctrller)
    {
        Type matchedControllerType = null;
        if (!matchedControllerTypes.ContainsKey(ctrller))
        {
            var assy = Assembly.GetAssembly(typeof(Controller));
 
            foreach (var m in assy.GetModules(false))
            {
                foreach (var t in m.GetTypes())
                {
                    if (ControllerType.IsAssignableFrom(t) && !t.IsAbstract)
                    {
                        if (t.Name.Equals(ctrller, StringComparison.OrdinalIgnoreCase) || t.Name.Equals($"{ctrller}Controller", StringComparison.OrdinalIgnoreCase))
                        {
                            matchedControllerType = t;
                            matchedControllerTypes[ctrller] = matchedControllerType;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            matchedControllerType = matchedControllerTypes[ctrller];
        }
 
        return matchedControllerType;
    }
    
    private MethodInfo FindAction(Type matchedControllerType, string actionName, object[] args)
    {
        string ctrlerWithActionKey = $"{matchedControllerType.FullName}.{actionName}";
        MethodInfo action = null;
        if (!matchedControllerActions.ContainsKey(ctrlerWithActionKey))
        {
            if (args == null) args = new object[0];
            foreach (var m in matchedControllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                if (m.Name.Equals(actionName, StringComparison.OrdinalIgnoreCase) && m.GetParameters().Length == args.Length)
                {
                    action = m;
                    matchedControllerActions[ctrlerWithActionKey] = action;
                    break;
                }
            }
        }
        else
        {
            action = matchedControllerActions[ctrlerWithActionKey];
        }
 
        return action;
    }
}

