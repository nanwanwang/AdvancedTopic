namespace CastleProxy;

public class ActionExecutingFilter:FilterAttribute
{
    public override string FilterType => "BEFORE";
    public override void Exec(Controller controller, object extData)
    {
        Console.WriteLine($"我是在{controller.GetType().Name}.ActionExecutingFilter中拦截发出的消息！-{DateTime.Now.ToString()}");
    }
}