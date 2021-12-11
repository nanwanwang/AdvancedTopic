using Microsoft.EntityFrameworkCore;

namespace SourceLearning_WebApi
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public  virtual DbSet<JsonConfiguration> JsonConfigurations { get; set; }
    }
}