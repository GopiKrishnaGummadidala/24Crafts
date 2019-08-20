using Jupiter._24Crafts.Business.Logic.Account.Interface;
using Jupiter._24Crafts.Data.Dtos.Account;
using Jupiter._24Crafts.Service.Api.Filter.Authenticate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jupiter._24Crafts.Service.Api.Controllers
{
  
    [RoutePrefix("accounts")]
    public class AccountController : ApiController
    {
        #region Private variable.
        private readonly ITokenBLL _tokenServices;
        private readonly IAccountBLL _iAccountBLL;
        #endregion
        #region Public Constructor
        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public AccountController()
        {

        }
        public AccountController(ITokenBLL tokenServices, IAccountBLL iAccountBLL)
        {
            _tokenServices = tokenServices;
            _iAccountBLL = iAccountBLL;
        }
        #endregion
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Registration(RegisterDto registerDto)
        {
            _iAccountBLL.Registration(registerDto);
            return Ok();
        }

        /// <summary>
        /// Authenticates user and returns token with expiry.
        /// </summary>
        /// <returns></returns>
        [ApiAuthenticationFilter]
        [HttpPost]
        [Route("login")]
        public ResponseUserDto Authenticate()
        {
            ResponseUserDto respData = null;
            IEnumerable<MainMenuDto> mainMenuResponse = new List<MainMenuDto>();
            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;

                    mainMenuResponse = _iAccountBLL.GetMenuByUserId(userId);

                    string[] roles = _iAccountBLL.GetRolesByUserId(userId);

                    TokenDto responseTokenDto = GetAuthToken(userId);

                    UserDto user = _iAccountBLL.GetUserInfoByUserId(userId);

                    respData = new ResponseUserDto()
                    {
                        UserID = user.UserID,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailID = user.EmailID,
                        PhoneNum = user.PhoneNum,
                        TokenData = responseTokenDto,
                        Roles = roles,
                        Menu = mainMenuResponse,
                       CustomerId =user.CustomerId
                    };

                    return respData;
                }
            }
            return respData;
        }

        [HttpGet]
        [Route("loginOtp")]
        public ResponseUserDto AuthenticateOtp(string mobileNum, string otp)
        {
            return _iAccountBLL.ValidateOTPUser(mobileNum, otp);
        }

        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private TokenDto GetAuthToken(long userId)
        {
            return _tokenServices.GenerateToken(userId);
        }
    }
}
