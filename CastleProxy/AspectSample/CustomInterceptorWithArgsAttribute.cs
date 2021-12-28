using AspectCore.DynamicProxy;

namespace CastleProxy;

public class CustomInterceptorWithArgsAttribute:AbstractInterceptorAttribute
{
    private readonly string _name;

    public CustomInterceptorWithArgsAttribute(string name)
    {
        _name = name;
    }

    public override async Task Invoke(AspectContext context, AspectDelegate next)
    {
        try
        {
            Console.WriteLine($"Before service call {_name}");
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine("Service threw an exception!");
            throw;
        }
        finally
        {
            Console.WriteLine("After service call");
        }
    } 
}