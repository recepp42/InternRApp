using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ExceptionFilters;


public class GlobalExceptionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        
        if(context.ActionDescriptor.Parameters.Count>0&&context.ActionArguments.Count!= context.ActionDescriptor.Parameters.Count) 
        {
            context.Result = new BadRequestResult();
        }
    }
}
