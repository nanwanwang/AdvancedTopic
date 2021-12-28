namespace CastleProxy;

public interface ICustomServiceWithArgs
{
    [CustomInterceptor]
    [CustomInterceptorWithArgs("test")]
    void Call();
}