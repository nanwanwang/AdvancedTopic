using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace SourceLearning_WebApi
{
    public class MyDeveloperPageExceptionFilter:IDeveloperPageExceptionFilter
    {
        public Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
        {
            errorContext.HttpContext.Response.WriteAsync($"MyDeveloperPageExceptionFilter:{errorContext.Exception}");

            return next(errorContext);
        }
    }
}