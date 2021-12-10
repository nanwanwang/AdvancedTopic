using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SourceLearning_WebApi
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        public MyMiddleware(RequestDelegate next,ITransientService transientService,
            //IScopedService scopedService,
         ISingletonService singletonService )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITransientService transientService,
           // IScopedService scopedService,
            ISingletonService singletonService)
        {
            Console.WriteLine("MyMiddleware Begin");
            await _next(context);
            Console.WriteLine("MyMiddleware End");
        }
    }

    public interface ISingletonService
    {
    }
    
    public  class  SingletonService:ISingletonService{}

    public interface IScopedService
    {
    }
    public  class  ScopedService:IScopedService{}

    public interface ITransientService
    {

    }

    public class TransientService : ITransientService
    {
     
    }

    public static class AppMiddlewareApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<MyMiddleware>();

        public static IApplicationBuilder UseBaseFactoryMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<MyFactoryMiddleware>();
    }
}