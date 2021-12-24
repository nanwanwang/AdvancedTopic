using Microsoft.AspNetCore.Mvc.Filters;

namespace SourceLearning_Mvc_controller;

public class MyOrderActionFilterAttribute1:Attribute, IActionFilter,IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"{nameof(MyOrderActionFilterAttribute1)}  : =>OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"{nameof(MyOrderActionFilterAttribute1)}  : =>OnActionExecuted");
    }

    public int Order { get; } = 1;
}