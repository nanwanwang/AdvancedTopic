using Microsoft.AspNetCore.Mvc.Filters;

namespace SourceLearning_Mvc_controller;

public class MyActionFilterWithSeriverFilter : IActionFilter
{
    public MyActionFilterWithSeriverFilter(IWebHostEnvironment env)
    {
        
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine(nameof(MyActionFilterWithSeriverFilter) + ":  OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine(nameof(MyActionFilterWithSeriverFilter) + ":  OnActionExecuted");
    }
}