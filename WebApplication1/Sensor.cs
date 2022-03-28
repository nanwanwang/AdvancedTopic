using System.ComponentModel.DataAnnotations;

namespace WebApplication1;

public class  Sensor
{
    [Key]
    public DateTime ts { get; set; }
    public double degree { get; set; }
    public int pm25 { get; set; }
}
