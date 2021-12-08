using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (contxt, next) =>
            {
                if (contxt.Request.Path == "/")
                {
                    await contxt.Response.WriteAsync("hello world!");
                    return;
                }

                await next();
            });

            //app.Use(async (context, next) =>
            //{
            //    var cultureQuery = context.Request.Query["culture"];
            //    if (!string.IsNullOrWhiteSpace(cultureQuery))
            //    {
            //        var culture = new CultureInfo(cultureQuery);

            //        CultureInfo.CurrentUICulture = culture;
            //        CultureInfo.CurrentUICulture = culture;
            //    }
            //    await next();
            //});

            //app.Run(async (context) => 
            //{
               
            //    await context.Response.WriteAsync($"Hello {CultureInfo.CurrentCulture.DisplayName}");
            //});
        }
    }
}
