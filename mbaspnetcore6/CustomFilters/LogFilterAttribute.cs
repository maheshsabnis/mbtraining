using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace mbaspnetcore6.CustomFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private void Log(RouteData route, string status)
        {
            if (route != null)
            {
                string controller = route.Values["controller"].ToString();
                string action = route.Values["action"].ToString();

                string message = $"Current status of execution is {status} in {controller} controller and it {action} action method ";

                Debug.WriteLine(message);
            }
            else
            {
                Debug.WriteLine("Some Log");
            }
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log(context.RouteData, "OnActionExecuting");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log(context.RouteData, "OnActionExecuted");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Log(context.RouteData, "OnResultExecuting");
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Log(context.RouteData, "OnResultExecuted");
        }
    }
}

