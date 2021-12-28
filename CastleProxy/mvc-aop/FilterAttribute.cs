namespace CastleProxy;

public abstract  class FilterAttribute:Attribute
{
    public  abstract  string FilterType { get; }
    public abstract void Exec(Controller controller, object extData);
}