using Jupiter._24Crafts.Business.Logic.Common.Interface;
using Jupiter._24Crafts.Data.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jupiter._24Crafts.Service.Api.Controllers
{
    [RoutePrefix("Common")]
    public class CommonController : ApiController
    {
        private readonly ICityBLL _iCityBll;
        private readonly ICountryBLL _iCountryBll;
        private readonly IStateBLL _iStateBll;
        private readonly ICommonBLL _commonBll;

        #region Public Constructor
        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public CommonController()
        {

        }
        public CommonController(ICityBLL iCityBll, ICountryBLL iCountryBLL,IStateBLL iStateBLL, ICommonBLL icommonBll)
        {
            _iCityBll = iCityBll;
            _iCountryBll = iCountryBLL;
            _iStateBll = iStateBLL;
            _commonBll = icommonBll;
        }
        #endregion
        [HttpGet]
        [Route("getCities")]
        public IHttpActionResult GetCitesByState([FromUri]long stateId)
        {
            IEnumerable<CityDto> response = new List<CityDto>();
            response = _iCityBll.GetCitesByState(stateId);
            return Ok(response);
        }

        [HttpGet]
        [Route("getCountries")]
        public IHttpActionResult GetCountryList()
        {
            IEnumerable<CountryDto> response = new List<CountryDto>();
            response = _iCountryBll.GetCountryList();
            return Ok(response);
        }

        [HttpGet]
        [Route("getStates")]
        public IHttpActionResult GetStatesByCountry([FromUri]long countryId)
        {
            IEnumerable<StateDto> response = new List<StateDto>();
            response = _iStateBll.GetStatesByCountry(countryId);
            return Ok(response);
        }

        [HttpGet]
        [Route("OTP")]
        public string OTPStatus([FromUri]string mobileNum, string otp=null)
        {
            return _commonBll.OTP(mobileNum, otp);
        }

        [HttpGet]
        [Route("LoginSendOTP")]
        public string LoginSendOTP([FromUri]string mobileNum)
        {
            return _commonBll.loginSendOTP(mobileNum);
        }

        [HttpGet]
        [Route("ForgetPasswordEmail")]
        public string ForgetPasswordEmail([FromUri]string email)
        {
            return _commonBll.forgetPwdSendMail(email);
        }

        [HttpGet]
        [Route("ForgetPwdOTP")]
        public bool ForgetPasswordOTP([FromUri]string mobileNum)
        {
            return _commonBll.forgetPwdSendOTP(mobileNum);
        }

        [HttpPost]
        [Route("sendMessage")]
        public bool sendMessage(MessagesDto msgObj)
        {
            return _commonBll.sendMessage(msgObj);
        }

        [HttpGet]
        [Route("getMessagesByUserId")]
        public List<MessagesDto> getMessagesByUserId(long userId)
        {
            return _commonBll.getMessagesByUserId(userId);
        }

        [HttpPost]
        [Route("SpamUser")]
        public bool Spam(SpamDto spamObj)
        {
            return _commonBll.Spam(spamObj);
        }

        [HttpGet]
        [Route("GetAllSpam")]
        public IEnumerable<SpamDto> GetAllSpam()
        {
            return _commonBll.GetAllSpam();
        }

        [HttpGet]
        [Route("DisableSpamUserAccount")]
        public bool DisableSpamUserAccount(long ByUserId, long SpamUserId)
        {
            return _commonBll.DisableSpamUserAccount(ByUserId, SpamUserId);
        }

        [HttpGet]
        [Route("getAllUsersForContentApproval")]
        public List<ContentApproverUserResponseDto> getAllUsersForContentApproval(int profileType=0)
        {
            return _commonBll.getAllUsersForContentApproval(profileType);
        }

        [HttpGet]
        [Route("ContentApproverApprovesUser")]
        public bool ContentApproverApprovesUser(long approvedBy,long userId, bool approved, string comments=null)
        {
            return _commonBll.ContentApproverApprovesUser(approvedBy,userId, approved, comments);
        }

        [HttpGet]
        [Route("getAllUsersImagesForApproval")]
        public List<ContentApproverUserResponseDto> getAllUsersImagesForApproval(int profileType = 0)
        {
            return _commonBll.getAllUsersImagesForApproval(profileType);
        }

        [HttpGet]
        [Route("ApprovesUserImages")]
        public bool ApprovesUserImages(long approvedBy, long userId, bool approved, string comments = null)
        {
            return _commonBll.ApprovesUserImages(approvedBy,userId, approved, comments);
        }

        [HttpGet]
        [Route("getAllUsersForAdminApproval")]
        public List<AdminUserResponseDto> getAllUsersForAdminApproval(int profileType = 0)
        {
            return _commonBll.getAllUsersForAdminApproval(profileType);
        }

        [HttpGet]
        [Route("AdminApprovesUser")]
        public bool AdminApprovesUser(long approvedBy,long userId, bool approved, string comments = null)
        {
            return _commonBll.AdminApprovesUser(approvedBy,userId, approved, comments);
        }

        [HttpGet]
        [Route("GetMessagesConfigurationSettings")]
        public List<MessageDeliveryConfigurationDto> GetMessagesConfigurationSettings()
        {
            return _commonBll.GetMessagesConfigurationSettings();
        }

        [HttpGet]
        [Route("SaveMessagesConfigurationSettings")]
        public bool SaveMessagesConfigurationSettings(MessageDeliveryConfigurationDto setting)
        {
            return _commonBll.SaveMessagesConfigurationSettings(setting);
        }

        [HttpGet]
        [Route("GetMyActiveNotifications")]
        public List<NotificationDto> GetMyActiveNotifications(long userId)
        {
            return _commonBll.GetMyActiveNotifications(userId);
        }

        [HttpGet]
        [Route("InActiveNotification")]
        public bool InActiveNotification(int NotificationId, long userId)
        {
            return _commonBll.InActiveNotification(NotificationId, userId);
        }

        [HttpGet]
        [Route("GetAllContentApprovers")]
        public IEnumerable<ContentApproverUserDto> GetAllContentApprovers()
        {
            return _commonBll.GetAllContentApprovers();
        }

        [HttpPost]
        [Route("AddUpdateContentApprover")]
        public bool AddUpdateContentApprover(ContentApproverUserDto userDto) 
        {
            return _commonBll.AddUpdateContentApprover(userDto);
        }

        [HttpGet]
        [Route("DeleteContentApprover")]
        public bool DeleteContentApprover(long ByUserId, long DeleteUserId) 
        {
            return _commonBll.DeleteContentApprover(ByUserId, DeleteUserId);
        }

        [HttpGet]
        [Route("GetStatsAllProfiles")]
        public IEnumerable<StatsProfileResponseDto> GetStatsAllProfiles()
        {
            return _commonBll.GetStatsAllProfiles();
        }

        [HttpGet]
        [Route("GetStatsOpportunitiesInfo")]
        public IEnumerable<StatsOpportunityResponseDto> GetStatsOpportunitiesInfo()
        {
            return _commonBll.GetStatsOpportunitiesInfo();
        }

        [HttpGet]
        [Route("GetAllActiveProfiles")]
        public IEnumerable<ActiveProfileDto> GetAllActiveProfiles()
        {
            return _commonBll.GetAllActiveProfiles();
        }

        [HttpGet]
        [Route("GetAllAdvertisements")]
        public IEnumerable<AdvertisementDto> GetAllAdvertisements()
        {
            return _commonBll.GetAllAdvertisements();
        }

        [HttpPost]
        [Route("AddOrUpdateAdvertisement")]
        public bool AddOrUpdateAdvertisement(AdvertisementDto addDto)
        {
            return _commonBll.AddOrUpdateAdvertisement(addDto);
        }

        [HttpGet]
        [Route("GetAllClientAdvertisements")]
        public IEnumerable<AdvertisementDto> GetAllClientAdvertisements()
        {
            return _commonBll.GetAllClientAdvertisements();
        }

        [HttpGet]
        [Route("ApproveAdvertisements")]
        public bool ApproveAdvertisements(long ByUserId, int AdvertisementId, bool isApproved, string comments)
        {
            return _commonBll.ApproveAdvertisements(ByUserId, AdvertisementId, isApproved, comments);
        }

        [HttpGet]
        [Route("PendingSpamUsersCount")]
        public int PendingSpamUsersCount()
        {
            return _commonBll.PendingSpamUsersCount();
        }

        [HttpGet]
        [Route("PendingApprovalImagesCount")]
        public int PendingApprovalImagesCount()
        {
            return _commonBll.PendingApprovalImagesCount();
        }

        [HttpGet]
        [Route("PendingApprovalUsersCount")]
        public int PendingApprovalUsersCount()
        {
            return _commonBll.PendingApprovalUsersCount();
        }

        [HttpGet]
        [Route("getDashboardAllCounts")]
        public Dictionary<string, int> getAllCounts()
        {
            return _commonBll.getAllCounts();
        }

        [HttpGet]
        [Route("SendEmailOrMessage")]
        public bool SendEmailOrMessage(long userId, bool isMobile, bool isEmail, string message)
        {
            return _commonBll.SendEmailOrMessage(userId, isMobile, isEmail, message);
        }

        [HttpGet]
        [Route("Test")]
        public bool Test()
        {
            return _commonBll.Test();
        }
    }
}

