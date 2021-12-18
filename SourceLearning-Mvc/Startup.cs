using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SourceLearning_Mvc
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
            services.AddRazorPages();
            //services.AddSingleton<IDeveloperPageExceptionFilter,MyDeveloperPageExceptionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                // app.UseExceptionHandler(app =>
                // {
                //     var loggFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
                //     var logger = loggFactory.CreateLogger("ExceptionHandlerPathFeature.Error");
                //     app.Run(async context =>
                //     {
                //         var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
                //         logger.LogError($"Exception Handled：{exceptionHandlerPathFeature.Error}");
                //         var statusCode = StatusCodes.Status500InternalServerError;
                //         var message = exceptionHandlerPathFeature.Error.Message;
                //         if (exceptionHandlerPathFeature.Error is NotImplementedException)
                //         {
                //             message = "not implement";
                //             statusCode = StatusCodes.Status501NotImplemented;
                //         }
                //
                //         context.Response.StatusCode = statusCode;
                //         context.Response.ContentType = "applicaiton/json";
                //         await context.Response.WriteAsJsonAsync(new
                //         {
                //             message = message,
                //             Success = false
                //         });
                //     });
                // });
               // app.UseExceptionHandler("/Error");
               // app.UseStatusCodePages();
               // app.UseStatusCodePages("text/plain", "Status code is: {0}");
               // app.UseStatusCodePages(async context =>
               // {
               //     context.HttpContext.Response.ContentType = "text/plain";
               //     await context.HttpContext.Response.WriteAsync(
               //         $"Status code    is :{context.HttpContext.Response.StatusCode}");
               // });
               
              //  app.UseStatusCodePagesWithRedirects("/Error?code={0}");
              // app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");
              
            }
            else
            {
                
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //
            // app.Use((context, next) =>
            // {
            //     int a = 1;
            //     int b = 0;
            //     var c = a / b;
            //     return  next();
            // });

           // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
