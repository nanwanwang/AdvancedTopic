using Microsoft.AspNetCore.Mvc.Filters;

namespace SourceLearning_Mvc_controller;

public class MyActionFilterWithTypeFilter : IActionFilter
{
    private string _caller;

    public MyActionFilterWithTypeFilter(string caller, IWebHostEnvironment env)
    {
        _caller = caller;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine(_caller + ":" + nameof(MyActionFilterWithTypeFilter) + ":  OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine(_caller + ":" + nameof(MyActionFilterWithTypeFilter) + ":  OnActionExecuted");
    }
}