using Jupiter._24Crafts.Business.Logic.Common.Interface;
using Jupiter._24Crafts.Data.Dtos.Common;
using Jupiter._24Crafts.Data.Dtos.Portfolio;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Common
{
   public class CommonBLL : ICommonBLL
    {
       private readonly ICommonUnitOfWork _cUoW;
       private readonly ILogExceptionUnitOfWork _logUoW;

       public CommonBLL(ICommonUnitOfWork unitOfWork, ILogExceptionUnitOfWork logUnitOfwork)
        {
            this._cUoW = unitOfWork;
            this._logUoW = logUnitOfwork;
        }
       
        public string OTP(string mobileNum, string otp)
        {
            try
            {
                return _cUoW.CommonRepository.OTP(mobileNum, otp);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public string loginSendOTP(string mobileNum)
        {
            try
            {
                return _cUoW.CommonRepository.loginSendOTP(mobileNum);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public string forgetPwdSendMail(string email)
        {
            try
            {
                return _cUoW.CommonRepository.forgetPwdSendMail(email);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool forgetPwdSendOTP(string mobileNum)
        {
            try
            {
                return _cUoW.CommonRepository.forgetPwdSendOTP(mobileNum);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool sendMessage(MessagesDto msgObj)
        {
            try
            {
                return _cUoW.CommonRepository.sendMessage(msgObj);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public List<MessagesDto> getMessagesByUserId(long userId)
        {
            try
            {
                return _cUoW.CommonRepository.getMessagesByUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool Spam(SpamDto spamObj)
        {
            try
            {
                return _cUoW.CommonRepository.Spam(spamObj);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public IEnumerable<SpamDto> GetAllSpam()
        {
            try
            {
                return _cUoW.CommonRepository.GetAllSpam();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public int PendingSpamUsersCount() {
            try
            {
                return _cUoW.CommonRepository.PendingSpamUsersCount();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public int PendingApprovalUsersCount() {
            try
            {
                return _cUoW.CommonRepository.PendingApprovalUsersCount();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public int PendingApprovalImagesCount()
        {
            try
            {
                return _cUoW.CommonRepository.PendingApprovalImagesCount();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public Dictionary<string, int> getAllCounts()
        {
            try
            {
                return _cUoW.CommonRepository.getAllCounts();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public bool DisableSpamUserAccount(long ByUserId, long SpamUserId)
        {
            try
            {
                return _cUoW.CommonRepository.DisableSpamUserAccount(ByUserId,SpamUserId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }


        public bool Block(BlockDto blockObj)
        {
            try
            {
                return _cUoW.CommonRepository.Block(blockObj);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool UnBlock(BlockDto blockObj)
        {
            try
            {
                return _cUoW.CommonRepository.UnBlock(blockObj);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public List<ContentApproverUserResponseDto> getAllUsersForContentApproval(int profileType)
        {
            try
            {
                return _cUoW.CommonRepository.getAllUsersForContentApproval(profileType);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool ContentApproverApprovesUser(long approvedBy,long userId, bool approved, string comments)
        {
            try
            {
                return _cUoW.CommonRepository.ContentApproverApprovesUser(approvedBy,userId, approved, comments);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public List<ContentApproverUserResponseDto> getAllUsersImagesForApproval(int profileType)
        {
            try
            {
                return _cUoW.CommonRepository.getAllUsersForContentApproval(profileType);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool ApprovesUserImages(long approvedBy,long userId, bool approved, string comments)
        {
            try
            {
                return _cUoW.CommonRepository.ContentApproverApprovesUser(approvedBy,userId, approved, comments);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public List<AdminUserResponseDto> getAllUsersForAdminApproval(int profileType)
        {
            try
            {
                return _cUoW.CommonRepository.getAllUsersForAdminApproval(profileType);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool AdminApprovesUser(long approvedBy,long userId, bool approved, string comments)
        {
            try
            {
                return _cUoW.CommonRepository.AdminApprovesUser(approvedBy,userId, approved, comments);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public List<MessageDeliveryConfigurationDto> GetMessagesConfigurationSettings()
        {
            try
            {
                return _cUoW.CommonRepository.GetMessagesConfigurationSettings();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool SaveMessagesConfigurationSettings(MessageDeliveryConfigurationDto setting)
        {
            try
            {
                return _cUoW.CommonRepository.SaveMessagesConfigurationSettings(setting);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }


        public List<NotificationDto> GetMyActiveNotifications(long userId)
        {
            try
            {
                return _cUoW.CommonRepository.GetMyActiveNotifications(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool InActiveNotification(int NotificationId, long userId)
        {
            try
            {
                return _cUoW.CommonRepository.InActiveNotification(NotificationId, userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<ContentApproverUserDto> GetAllContentApprovers() 
        {
            try
            {
                return _cUoW.CommonRepository.GetAllContentApprovers();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public bool AddUpdateContentApprover(ContentApproverUserDto userDto)
        {
            try
            {
                return _cUoW.CommonRepository.AddUpdateContentApprover(userDto);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public bool DeleteContentApprover(long ByUserId, long DeleteUserId) 
        {
            try
            {
                return _cUoW.CommonRepository.DeleteContentApprover(ByUserId, DeleteUserId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public IEnumerable<StatsProfileResponseDto> GetStatsAllProfiles()
        {
            try
            {
                return _cUoW.CommonRepository.GetStatsAllProfiles();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<StatsOpportunityResponseDto> GetStatsOpportunitiesInfo()
        {
            try
            {
                return _cUoW.CommonRepository.GetStatsOpportunitiesInfo();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public IEnumerable<ActiveProfileDto> GetAllActiveProfiles()
        {
            try
            {
                return _cUoW.CommonRepository.GetAllActiveProfiles();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<AdvertisementDto> GetAllAdvertisements()
        {
            try
            {
                return _cUoW.CommonRepository.GetAllAdvertisements();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public bool AddOrUpdateAdvertisement(AdvertisementDto addDto)
        {
            try
            {
                return _cUoW.CommonRepository.AddOrUpdateAdvertisement(addDto);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<AdvertisementDto> GetAllClientAdvertisements()
        {
            try
            {
                return _cUoW.CommonRepository.GetAllClientAdvertisements();
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public bool ApproveAdvertisements(long ByUserId, int AdvertisementId, bool isApproved, string comments)
        {
            try
            {
                return _cUoW.CommonRepository.ApproveAdvertisements(ByUserId, AdvertisementId, isApproved, comments);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public bool SendEmailOrMessage(long userId, bool isMobile, bool isEmail, string message)
        {
            try
            {
                return _cUoW.CommonRepository.SendEmailOrMessage(userId, isMobile, isEmail, message);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public bool Test()
        {
            return _cUoW.CommonRepository.Test();
        }
    }
}
