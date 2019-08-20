using Jupiter._24Crafts.Business.Logic.Account.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Jupiter._24Crafts.Service.Api.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        //public string[] roles { get; set; }
        private const string Token = "Token";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            // Get API key provider
            var provider = filterContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ITokenBLL)) as ITokenBLL;
            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                // Validate Token
                if (provider != null && !provider.ValidateToken(tokenValue))
                {
                    var responseMessage = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                    filterContext.Response = responseMessage;
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}