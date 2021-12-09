using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SourceLearning_WebApi
{
    public class StartupProduction
    {
        public StartupProduction(IConfiguration configuration, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
            WebHostEnvironment = webHostEnvironment;
        }

        public IHostEnvironment HostEnvironment { get; set; }

        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public IConfiguration Configuration { get; }
        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (WebHostEnvironment.IsTest())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsDevelopment())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsStaging())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsProduction())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            services.AddTransient<IStartupFilter, SecondStartupFilter>();
            //services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SourceLearning_WebApi", Version = "v1" });
            //});
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((context, next) =>
            {
                Console.WriteLine("Forth");
                return next();
            });
            
            if (WebHostEnvironment.IsTest())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsDevelopment())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsStaging())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
            else if (WebHostEnvironment.IsProduction())
            {
                Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
            }
        
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SourceLearning_WebApi v1"));
            //}
        
            //app.UseHttpsRedirection();
        
            //app.UseRouting();
        
            //app.UseAuthorization();
        
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}