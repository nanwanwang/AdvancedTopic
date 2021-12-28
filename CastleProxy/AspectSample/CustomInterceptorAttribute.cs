using AspectCore.DynamicProxy;

namespace CastleProxy;

public class CustomInterceptorAttribute:AbstractInterceptorAttribute
{
    public override async Task Invoke(AspectContext context, AspectDelegate next)
    {
        try
        {
            Console.WriteLine("Before service call");
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Console.WriteLine("After service call");
        }
    }
}