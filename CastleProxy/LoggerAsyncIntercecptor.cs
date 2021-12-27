using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace CastleProxy;

public class LoggerAsyncIntercecptor:IAsyncInterceptor
{
    private readonly ILogger _logger;

    public LoggerAsyncIntercecptor(ILogger logger)
    {
        _logger = logger;
    }
    
    public void InterceptSynchronous(IInvocation invocation)
    {
        throw new NotImplementedException();
    }

    public void InterceptAsynchronous(IInvocation invocation)
    {
        throw new NotImplementedException();
    }

    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
    }

    private async Task<T> InternalInterceptAsynchronous<T>(IInvocation invocation)
    {
        var methodName = invocation.Method.Name;
        invocation.Proceed();
        var task = (Task<T>)invocation.ReturnValue;
        T result = await task;
        
        _logger.Info($"{methodName}已执行,返回结果为:{result}");
        return result;
    }
}