using AspectCore.DynamicProxy;

namespace CastleProxy;

[NonAspect]
public interface ICustomService
{
    [CustomInterceptorWithArgs("customservice")]
    void Call();
}