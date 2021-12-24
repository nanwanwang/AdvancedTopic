using Microsoft.AspNetCore.Mvc.Filters;

namespace SourceLearning_Mvc_controller;

/// <summary>
/// using attribute style action filter
/// </summary>
public class MyActionFilterWithControllerAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine(nameof(MyActionFilterWithControllerAttribute) + ":  OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine(nameof(MyActionFilterWithControllerAttribute) + ":  OnActionExecuted");
    }
}