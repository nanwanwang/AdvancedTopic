using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace SourceLearning_Mvc
{
    public class MyDeveloperPageExceptionFilter:IDeveloperPageExceptionFilter
    {
        public Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
        {
            errorContext.HttpContext.Response.WriteAsJsonAsync($"MyDeveloperPageExceptionFilter{errorContext.Exception}");
            return Task.CompletedTask;
        }
    }
}