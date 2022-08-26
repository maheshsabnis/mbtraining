using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace mbaspnetcore6.CustomFilters
{
    public class AppExceptionFilterAttribute : ExceptionFilterAttribute
    {
        IModelMetadataProvider modelMetadataProvider;
        /// <summary>
        /// Injecting the IModelMetadataProvider in Customm Exception Filter
        /// This will be resolved when the filter is eregistered in Global Scope
        /// </summary>
        /// <param name="modelMetadataProvider"></param>
        public AppExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider)
        {
            this.modelMetadataProvider = modelMetadataProvider;
        }
        /// <summary>
        /// THe Logic for Handling Exceptions
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            // 1. Handle the exception so that the excpetion handling logic will start executing
            context.ExceptionHandled = true;
            // 2. Listen to an exception
            Exception exception = context.Exception;
            // 3. Prepeare for the Result
            // a. Using the ViewResult
            ViewResult viewResult = new ViewResult();
            // b. Set the ViewName that will be responded
            viewResult.ViewName = "Error";
            // c. Since the 'Model' is readonly property, we need to pas data to view using ViewDataDictionary
            ViewDataDictionary viewData = new ViewDataDictionary(modelMetadataProvider,context.ModelState);
            // d. Deine Key:Value pairs for ViewData
            // The 'controller' and 'action' will be read from the Route for the Current HTTP Request    
            viewData["ControllerName"] = context.RouteData.Values["controller"].ToString();
            viewData["ActionName"] = context.RouteData.Values["action"].ToString();
            viewData["ErrorMessage"] = exception.Message;
            // e. Set the viewData to the ViewData property of the ViewResult Instance
            viewResult.ViewData = viewData;
            // f. Set the viewResult to Result Porperty of the Context
            context.Result = viewResult;
        }
    }
}

