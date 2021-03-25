using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiRestNorthwnd.Filters
{
    public class AutomaticExceptionTrigger : ExceptionFilterAttribute
    {

        //Esto se ejecutará, cuando ocurra una excepción en mi endpoint
        public override void OnException(ExceptionContext context)
        {

            var statusCode = HttpStatusCode.InternalServerError;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(new {
                error = "Ha ocurrido un problema. " + context.Exception.Message,
                date = DateTime.Now,

            });

        }

    }
}
