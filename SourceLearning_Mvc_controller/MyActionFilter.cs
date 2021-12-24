using Microsoft.AspNetCore.Mvc.Filters;

namespace SourceLearning_Mvc_controller;

public class MyActionFilter:IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine(nameof(MyActionFilter)+ "  OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine(nameof(MyActionFilter)+ "  OnActionExecuted");
    }
}