using Jupiter._24Crafts.Data.Dtos.Common;
using Jupiter._24Crafts.Data.Dtos.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Common.Interface
{
   public interface ICommonBLL
    {
       string OTP(string mobileNum, string otp);
       string loginSendOTP(string mobileNum);
       string forgetPwdSendMail(string email);
       bool forgetPwdSendOTP(string mobileNum);
       bool sendMessage(MessagesDto msgObj);
       List<MessagesDto> getMessagesByUserId(long userId);
       bool Spam(SpamDto spamObj);
       IEnumerable<SpamDto> GetAllSpam();
       int PendingSpamUsersCount();
       bool DisableSpamUserAccount(long ByUserId, long SpamUserId);
       bool Block(BlockDto blockObj);
       bool UnBlock(BlockDto blockObj);
       List<ContentApproverUserResponseDto> getAllUsersForContentApproval(int profileType);
       bool ContentApproverApprovesUser(long approvedBy,long userId, bool approved, string comments);
       List<AdminUserResponseDto> getAllUsersForAdminApproval(int profileType);
       int PendingApprovalUsersCount();
       int PendingApprovalImagesCount();
       bool AdminApprovesUser(long approvedBy,long userId, bool approved, string comments);
       List<ContentApproverUserResponseDto> getAllUsersImagesForApproval(int profileType);
       bool ApprovesUserImages(long approvedBy, long userId, bool approved, string comments);
       List<MessageDeliveryConfigurationDto> GetMessagesConfigurationSettings();
       bool SaveMessagesConfigurationSettings(MessageDeliveryConfigurationDto setting);
       List<NotificationDto> GetMyActiveNotifications(long userId);
       bool InActiveNotification(int NotificationId, long userId);
       IEnumerable<ContentApproverUserDto> GetAllContentApprovers();
       bool AddUpdateContentApprover(ContentApproverUserDto userDto);
       bool DeleteContentApprover(long ByUserId, long DeleteUserId);
       IEnumerable<StatsProfileResponseDto> GetStatsAllProfiles();
       IEnumerable<StatsOpportunityResponseDto> GetStatsOpportunitiesInfo();
       IEnumerable<ActiveProfileDto> GetAllActiveProfiles();
       IEnumerable<AdvertisementDto> GetAllAdvertisements();
       Dictionary<string, int> getAllCounts();
       bool AddOrUpdateAdvertisement(AdvertisementDto addDto);
       IEnumerable<AdvertisementDto> GetAllClientAdvertisements();
       bool ApproveAdvertisements(long ByUserId, int AdvertisementId, bool isApproved, string comments);
       bool SendEmailOrMessage(long userId, bool isMobile, bool isEmail, string message);
       bool Test();
    }
}
