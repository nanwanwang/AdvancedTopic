using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SourceLearning_Mvc_controller.Models;

namespace SourceLearning_Mvc_controller;

public class MyExceptionFilterAttribute:ExceptionFilterAttribute
{
    private readonly IModelMetadataProvider _modelMetadataProvider;

    public MyExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider)
    {
        _modelMetadataProvider = modelMetadataProvider;
    }

    public override void OnException(ExceptionContext context)
    {
        if (!context.ExceptionHandled)
        {
            var exception = context.Exception;
            var result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
                {
                    Model = new ErrorViewModel()
                    {
                        RequestId = exception.ToString()
                    }
                }
            };
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}