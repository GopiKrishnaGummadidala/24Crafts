using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Jupiter._24Crafts.Service.Api.Filter
{
    public class ExceptionHandlingAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //Log.Exception(context.Exception);
        }
    }
}