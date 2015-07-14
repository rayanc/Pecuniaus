using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Data.SqlClient;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Bridge.Models;

namespace Bridge.Handlers
{
    public class HandleException:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            HandleErrorModel error = new HandleErrorModel();
            error.type ="Unable to process";
            var exType = actionExecutedContext.Exception.GetType();
            if (exType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.BadRequest;
                error.type = "You are not authorized for this resource";
            }
            else if (exType == typeof(ArgumentException))
            {
                status = HttpStatusCode.BadRequest;
                error.type = "Invalid Data";
            }
            
            error.target = actionExecutedContext.Exception.TargetSite.DeclaringType.FullName + "." + actionExecutedContext.Exception.TargetSite.Name;
            error.message = actionExecutedContext.Exception.Message;
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(status, new { errors = new List<HandleErrorModel> { error } });
            base.OnException(actionExecutedContext);
        }
    

    }
}