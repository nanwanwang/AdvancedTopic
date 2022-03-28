using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public class WebDbContext:DbContext
{
    public DbSet<Sensor> Sensor { get; set; }
    public WebDbContext(DbContextOptions options)
        : base(options)
    {

    }
}