namespace CastleProxy;

public class CustomService:ICustomService
{
    public void Call()
    {
        Console.WriteLine("service calling ....");
    }
}