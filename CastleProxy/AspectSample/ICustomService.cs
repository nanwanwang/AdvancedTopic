namespace CastleProxy;

public interface ICustomService
{
    [CustomInterceptorWithArgs("customservice")]
    void Call();
}