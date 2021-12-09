using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SourceLearning_WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
            WebHostEnvironment = webHostEnvironment;
        }

        public IHostEnvironment HostEnvironment { get; set; }

        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public IConfiguration Configuration { get; }

        private void StartupConfigureServices(IServiceCollection sevices)
        {
            Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        }

        public void ConfigureDevelopmentServices(IServiceCollection serives)
        {
            StartupConfigureServices(serives);
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            StartupConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            StartupConfigureServices(services);
        }

        private void StartupConfigure(IApplicationBuilder app)
        {
            Console.WriteLine($"{nameof(Configure)}:{WebHostEnvironment.EnvironmentName}");
        }

        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            StartupConfigure(app);
        }

        public void ConfigureStaging(IApplicationBuilder app)
        {
            StartupConfigure(app);
        }

        public void ConfigureProduction(IApplicationBuilder app)
        {
            StartupConfigure(app);
        }

        public void Configure(IApplicationBuilder app)
        {
            StartupConfigure(app);
        }


        // // This method gets called by the runtime. Use this method to add services to the container.
        // public void ConfigureServices(IServiceCollection services)
        // {
        //     if (WebHostEnvironment.IsTest())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //     else if (WebHostEnvironment.IsDevelopment())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //     else if (WebHostEnvironment.IsStaging())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //     else if (WebHostEnvironment.IsProduction())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //     services.AddTransient<IStartupFilter, SecondStartupFilter>();
        //     //services.AddControllers();
        //     //services.AddSwaggerGen(c =>
        //     //{
        //     //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SourceLearning_WebApi", Version = "v1" });
        //     //});
        // }
        //
        // // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // {
        //     app.Use((context, next) =>
        //     {
        //         Console.WriteLine("Forth");
        //         return next();
        //     });
        //     
        //     if (WebHostEnvironment.IsTest())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //     else if (WebHostEnvironment.IsDevelopment())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //     else if (WebHostEnvironment.IsStaging())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //     else if (WebHostEnvironment.IsProduction())
        //     {
        //         Console.WriteLine($"{nameof(ConfigureServices)}:{WebHostEnvironment.EnvironmentName}");
        //     }
        //
        //     //if (env.IsDevelopment())
        //     //{
        //     //    app.UseDeveloperExceptionPage();
        //     //    app.UseSwagger();
        //     //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SourceLearning_WebApi v1"));
        //     //}
        //
        //     //app.UseHttpsRedirection();
        //
        //     //app.UseRouting();
        //
        //     //app.UseAuthorization();
        //
        //     //app.UseEndpoints(endpoints =>
        //     //{
        //     //    endpoints.MapControllers();
        //     //});
        // }
    }
}
