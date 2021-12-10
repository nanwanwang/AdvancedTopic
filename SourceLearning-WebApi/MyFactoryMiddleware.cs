using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SourceLearning_WebApi
{
    public class MyFactoryMiddleware:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("MyBaseFactoryMiddleware Begin");
            await next(context);
            Console.WriteLine("MyBaseFactoryMiddleware End");
            
        }
    }
    

}