using Microsoft.AspNetCore.Mvc.Filters;

namespace SourceLearning_Mvc_controller;

public class MyOrderActionFilterAttribute2:Attribute,IActionFilter,IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"{nameof(MyOrderActionFilterAttribute2)}  : =>OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"{nameof(MyOrderActionFilterAttribute2)}  : =>OnActionExecuted");
    }

    public int Order { get; } = 2;
}