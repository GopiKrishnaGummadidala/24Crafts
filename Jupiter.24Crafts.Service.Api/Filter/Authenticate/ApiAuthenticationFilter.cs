using Jupiter._24Crafts.Business.Logic.Account;
using Jupiter._24Crafts.Business.Logic.Account.Interface;
using Jupiter._24Crafts.Data.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace Jupiter._24Crafts.Service.Api.Filter.Authenticate
{
    public class ApiAuthenticationFilter: GenericAuthenticationFilter
    {
        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public ApiAuthenticationFilter()

        {
        }
        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }
        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// /// <returns></returns>
        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration
            .DependencyResolver.GetService(typeof(IAccountBLL)) as IAccountBLL;
            if (provider != null)
            {
                string typeOfCreation = string.Empty;
                if (actionContext.Request.Headers.Contains("LoginType"))
                {
                    typeOfCreation = actionContext.Request.Headers.GetValues("LoginType").First();
                }
                ResponseUserDto responseUserDto = typeOfCreation =="OTP" ? provider.ValidateOTPUser(username, password) : provider.ValidateUser(username, password);
                if ( responseUserDto != null && responseUserDto.UserID > 0)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                        basicAuthenticationIdentity.UserId = responseUserDto.UserID;
                    return true;
                }
            }
            return false;
        }
    }
}