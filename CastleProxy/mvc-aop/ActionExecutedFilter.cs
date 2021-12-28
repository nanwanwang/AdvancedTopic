namespace CastleProxy;

public class ActionExecutedFilter:FilterAttribute
{
    public override string FilterType => "AFTER";
    public override void Exec(Controller controller, object extData)
    {
        Console.WriteLine($"我是在{controller.GetType().Name}.ActionExecutedFilter中拦截发出的消息！-{DateTime.Now.ToString()}");
    }
}