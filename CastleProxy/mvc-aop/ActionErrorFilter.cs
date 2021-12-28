namespace CastleProxy;

public class ActionErrorFilter:FilterAttribute
{
    public override string FilterType => "EXCEPTION";
    public override void Exec(Controller controller, object extData)
    {
        Console.WriteLine($"我是在{controller.GetType().Name}.ActionErrorFilter中拦截发出的消息！-{DateTime.Now.ToString()}-Error Msg:{(extData as Exception).Message}");
    }
}