using System.Reflection;

namespace CastleProxy;

public abstract class Controller
{
    public virtual void OnActionExecuting(MethodInfo action)
    {
        
    }

    public virtual void OnActionExecuted(MethodInfo action)
    {
        
    }

    public virtual void OnActionError(MethodInfo action, Exception exception)
    {
        
    }
    
}