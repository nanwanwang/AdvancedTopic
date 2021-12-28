using System.Reflection;

namespace CastleProxy;

public class TestController:Controller
{
    public override void OnActionExecuting(MethodInfo action)
    {
        Console.WriteLine($"{action.Name}执行前，OnActionExecuting---{DateTime.Now.ToString()}");
    }
 
    public override void OnActionExecuted(MethodInfo action)
    {
        Console.WriteLine($"{action.Name}执行后，OnActionExecuted--{DateTime.Now.ToString()}");
    }
 
    public override void OnActionError(MethodInfo action, Exception ex)
    {
        Console.WriteLine($"{action.Name}执行，OnActionError--{DateTime.Now.ToString()}:{ex.Message}");
    }

    [ActionExecutingFilter]
    [ActionExecutedFilter]
    public string HelloWorld(string name)
    {
        return $"Hello World!->{name}";
    }

    [ActionExecutingFilter]
    [ActionExecutedFilter]
    [ActionErrorFilter]
    public string TestError(string name)
    {
        throw new Exception("异常");
    }

    [ActionExecutingFilter]
    [ActionExecutedFilter]
    public int Add(int a, int b)
    {
        return a + b;
    }
}