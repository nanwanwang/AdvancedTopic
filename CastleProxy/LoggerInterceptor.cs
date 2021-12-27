using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace CastleProxy;

public class LoggerInterceptor:IInterceptor
{
    private readonly ILogger _logger;
    private readonly LoggerAsyncIntercecptor _intercecptor;

    public LoggerInterceptor(ILogger logger, LoggerAsyncIntercecptor intercecptor)
    {
        _logger = logger;
        _intercecptor = intercecptor;
    }

    public void Intercept(IInvocation invocation)
    {
        // var methodName = invocation.Method.Name;
        //
        // invocation.Proceed();
        //
        // _logger.Info($"{methodName} 已执行");

        _intercecptor.ToInterceptor().Intercept(invocation);
    }
}