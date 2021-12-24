namespace ExpressionTreeSample;

public class User
{
    public  int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public  int Age { get; set; }
    public DateTime CreateTime { get; set; }
    public  Address Address { get; set; }
    
}

public class Address
{
    public string  Province { get; set; }
    public string City { get; set; }
}