namespace RecordSample;

/// <inheritdoc />
public record Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

public record Human(string Name,int Age);