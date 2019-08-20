using Jupiter._24Crafts.Common.Resources;
using Jupiter._24Crafts.Data.Dtos.Common;
using Jupiter._24Crafts.Data.Dtos.Portfolio;
using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Common.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Jupiter._24Crafts.Data.Repositories.Common
{
    public class CommonRepository : ICommonRepository
    {
        private readonly CratfsDb _craftsDb;

        /// <summary>
        /// OTPAPI Url
        /// </summary>
        public static string OTPAPIUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["OTPAPIUrl"];
            }
        }

        public CommonRepository(CratfsDb craftsDb)
        {
            this._craftsDb = craftsDb;
        }

        public string OTP(string mobileNum, string otp)
        {
            OTP objOtp = new OTP();
            string textOtp = string.Empty;
            if (string.IsNullOrEmpty(otp))
            {
                textOtp = RandomOTP();
                objOtp.IsActive = true;
                objOtp.MobileNum = mobileNum;
                objOtp.IssuedDate = DateTime.Now;
                objOtp.OTP1 = textOtp;
                _craftsDb.OTPs.Add(objOtp);
                _craftsDb.SaveChanges();
                if (OTPSend(mobileNum, textOtp))
                    return "OTP sent successfully";
                else
                    return "OTP does not sent due to some technical issues";
            }
            else
            {
                var obj = _craftsDb.OTPs.Where(_ => _.MobileNum == mobileNum && _.OTP1 == otp && _.IsActive == true).FirstOrDefault();
                if (obj != null)
                {
                    obj.IsActive = false;
                    _craftsDb.SaveChanges();
                    return "1";
                }
                else
                {
                    return "There are no active OTPs at present for this Mobile Number";
                }
            }
        }

        public string loginSendOTP(string mobileNum)
        {
            if (_craftsDb.Users.Any(u => u.PhoneNum == mobileNum && u.IsActive == true))
            {
                OTP objOtp = new OTP();
                string textOtp = string.Empty;

                textOtp = RandomOTP();
                objOtp.IsActive = true;
                objOtp.MobileNum = mobileNum;
                objOtp.IssuedDate = DateTime.Now;
                objOtp.OTP1 = textOtp;
                _craftsDb.OTPs.Add(objOtp);
                _craftsDb.SaveChanges();
                if (OTPSend(mobileNum, textOtp))
                    return "OTP sent successfully";
                else
                    return "OTP does not sent due to some technical issues";
            }
            else
            {
                return "Active Mobile Number does not exist in our records";
            }
        }
        private string RandomOTP()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            return r;
        }

        private bool OTPSend(string mobileNum, string msg)
        {
            try
            {
                if (Settings.EnableOTPSending)
                {
                    WebClient client = new WebClient();
                    Stream data = client.OpenRead(OTPAPIUrl.Replace("[Test]", msg) + mobileNum);
                    StreamReader reader = new StreamReader(data);
                    XmlDocument xmlResult = new XmlDocument();
                    xmlResult.LoadXml(reader.ReadToEnd());
                    return xmlResult.GetElementsByTagName("status")[0].InnerText == "0" ? true : false;
                }
                else
                {
                    return false;
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        public string forgetPwdSendMail(string email)
        {
            var user = _craftsDb.Users.Where(_ => _.EmailID == email && _.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                var data = _craftsDb.EmailTemplates.Where(e => e.TemplateName == "EmailPasswordRecovery").FirstOrDefault();
                if (data != null)
                {
                    EmailOutbox obj = new EmailOutbox();
                    obj.DateSent = DateTime.Now;
                    obj.EmailBody = data.TemplateHTML.Replace("[Password]", user.Password);
                    obj.EmailSubject = data.EmailSubject;
                    obj.EmailTo = email;
                    _craftsDb.EmailOutboxes.Add(obj);
                    _craftsDb.SaveChanges();
                    
                    if (MailSender.MailSend(email, data.EmailSubject, data.TemplateHTML.Replace("[Password]", user.Password)))
                        return "Email sent successfully";
                    else
                        return "Email does not sent due to some technical issues";
                }
                else
                {
                    return "Email Template Not Found";
                }
            }
            else
            {
                return "Active EmailId does not exist in our records";
            }
        }

        public bool forgetPwdSendOTP(string mobileNum)
        {
            return OTPSend(mobileNum, string.Empty);
        }

        public bool sendMessage(MessagesDto msgObj)
        {
            if (!IsSpamDataContains.IsValidData(msgObj.Message))
            {
                if (!_craftsDb.SpamLists.Any(_ => _.SpamUserId == msgObj.FromUserId))
                {
                    Message obj = new Message();
                    obj.FromUserId = msgObj.FromUserId;
                    obj.Message1 = msgObj.Message;
                    obj.IsActive = true;
                    obj.ToUserId = msgObj.ToUserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.StartTime = DateTime.Now.TimeOfDay;
                    _craftsDb.Messages.Add(obj);
                    _craftsDb.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<MessagesDto> getMessagesByUserId(long userId)
        {
            return _craftsDb.Messages.Where(_ => _.ToUserId == userId).Select(s => new MessagesDto
            {
                FromUserId = s.FromUserId,
                FromCustId = (_craftsDb.Users.Where(u=>u.UserID == s.FromUserId).Select(a=>a.CustomerId).FirstOrDefault()),
                FromUserName = (_craftsDb.Users.Where(u => u.UserID == s.FromUserId).Select(a => a.FirstName).FirstOrDefault()),
                ToUserId = s.ToUserId,
                Message = s.Message1,
                MessageId = s.MessageId,
                CreatedDate = s.CreatedDate,
                IsActive = s.IsActive,
                IsViewed = s.IsViewed
            }).ToList();
        }

        public bool Spam(SpamDto spamObj)
        {
            if (!_craftsDb.SpamLists.Any(_ => _.SpamUserId == spamObj.SpamUserId && _.ByUserId == spamObj.ByUserId))
            {
                SpamList obj = new SpamList();
                obj.ByUserId = spamObj.ByUserId;
                obj.IsActive = true;
                obj.CreatedDate = DateTime.Now;
                obj.IsApprovedAdmin = false;
                obj.SpamUserId = spamObj.SpamUserId;

                _craftsDb.SpamLists.Add(obj);
                _craftsDb.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<SpamDto> GetAllSpam()
        {
            List<SpamDto> spamRes = new List<SpamDto>();
            var spamList = (from spam in _craftsDb.SpamLists
                            select new SpamDto { SpamId = spam.SpamId, ByUserId = spam.ByUserId, ByCustId = (_craftsDb.Users.Where(u => u.UserID == spam.ByUserId).FirstOrDefault().CustomerId), SpamUserId = spam.SpamUserId, SpamCustId = (_craftsDb.Users.Where(u => u.UserID == spam.SpamUserId).FirstOrDefault().CustomerId) }).ToList();

            foreach (var spam in spamList)
            {
                SpamDto spamSingle = new SpamDto();
                spamSingle.SpamId = spam.SpamId;
                spamSingle.ByUserId = spam.ByUserId;
                spamSingle.SpamId = spam.SpamId;
                spamSingle.ByCustId = spam.ByCustId;
                spamSingle.SpamCustId = spam.SpamCustId;
                spamSingle.Messages = _craftsDb.Messages.Where(m => m.FromUserId == spam.SpamUserId && m.ToUserId == spam.ByUserId).Select(s => new MessagesDto { Message = "Hiiii," }).ToList();
                spamRes.Add(spamSingle);
            }
            return spamRes;
        }

        public int PendingSpamUsersCount()
        {
            return _craftsDb.SpamLists.Where(s => s.IsApprovedAdmin != true).Count();
        }

        public bool DisableSpamUserAccount(long ByUserId, long SpamUserId)
        {
            var user = _craftsDb.Users.Where(u => u.UserID == SpamUserId).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = false;
                var consumerEvent = _craftsDb.ConsumerEvents.Where(c => c.EventName == "DisabledUserAccount").FirstOrDefault();
                if (consumerEvent != null)
                {
                    AuditTrack audit = new AuditTrack();
                    audit.ConsumerEventId = consumerEvent.ConsumerEventId;
                    audit.ByUserId = ByUserId;
                    audit.ToUserId = SpamUserId;
                    audit.IsActive = true;
                    audit.CreatedDate = DateTime.Now;
                    _craftsDb.AuditTracks.Add(audit);
                }
                _craftsDb.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Block(BlockDto blockObj)
        {
            if (!_craftsDb.BlockLists.Any(_ => _.BlockedByUserId == blockObj.BlockedByUserId && _.BlockedUserId == blockObj.BlockedUserId))
            {
                BlockList obj = new BlockList();
                obj.BlockedByUserId = blockObj.BlockedByUserId;
                obj.IsActive = true;
                obj.CreatedDate = DateTime.Now;
                obj.IsApprovedAdmin = false;
                obj.BlockedUserId = blockObj.BlockedUserId;

                _craftsDb.BlockLists.Add(obj);
                _craftsDb.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UnBlock(BlockDto blockObj)
        {
            var res = _craftsDb.BlockLists.Where(_ => _.BlockedByUserId == blockObj.BlockedByUserId && _.BlockedUserId == blockObj.BlockedUserId).Single();
            if(res != null)
            {
                res.IsActive = false;
                res.CreatedDate = DateTime.Now;
                _craftsDb.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ContentApproverUserResponseDto> getAllUsersForContentApproval(int profileType)
        {
            int? profile = profileType != 0 ? profileType : 0;
            return _craftsDb.Users.Where(u => !(u.IsContentApproverApproved == true) && (profile == 0 ? true : u.ProfileType == profile)).Select(s => new ContentApproverUserResponseDto
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNum=s.PhoneNum,
                EmailID=s.EmailID,
                UserId = s.UserID,
                ProfileType = s.ProfileType
            }).ToList();
        }

        public bool ContentApproverApprovesUser(long approvedBy, long userId, bool approved, string comments)
        {
            var user = _craftsDb.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {
                user.IsContentApproverApproved = approved;
                user.IsImageVideoContentModified = false;
                user.ContentApproverComments = comments;
                _craftsDb.SaveChanges();

                if (user.IsEmailApproved == true)
                {
                    var data = _craftsDb.EmailTemplates.Where(e => approved ? (e.TemplateName == "ContentApproverApproved") : (e.TemplateName == "ContentApproverRejected")).FirstOrDefault();
                    if (data != null)
                    {
                        EmailOutbox obj = new EmailOutbox();
                        obj.DateSent = DateTime.Now;
                        obj.EmailBody = data.TemplateHTML.Replace("[Name]", user.FirstName);
                        obj.EmailSubject = data.EmailSubject;
                        obj.EmailTo = user.EmailID;
                        _craftsDb.EmailOutboxes.Add(obj);
                        _craftsDb.SaveChanges();

                        Notification notify = new Notification();
                        notify.CreatedDate = DateTime.Now;
                        notify.IsActive = true;
                        notify.IsViewed = false;
                        notify.ToUserId = user.UserID;
                        notify.Notification1 = approved ? "Content Approver Approved" : "Content Approver Rejected";
                        _craftsDb.Notifications.Add(notify);
                        _craftsDb.SaveChanges();

                        //MailSender.MailSend(user.EmailID, data.EmailSubject, data.TemplateHTML.Replace("[Password]", user.Password));
                        MailSender.MailSend(Settings.AdminEmailId, data.EmailSubject, data.TemplateHTML.Replace("[Name]", user.FirstName));
                    }
                }

                return true;
            }
            return false;
        }

        public List<ContentApproverUserResponseDto> getAllUsersImagesForApproval(int profileType)
        {
            int? profile = profileType != 0 ? profileType : 0;
            return _craftsDb.Users.Where(u => u.IsImageVideoContentModified == true && (profile == 0 ? true : u.ProfileType == profile)).Select(s => new ContentApproverUserResponseDto
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNum = s.PhoneNum,
                EmailID = s.EmailID,
                UserId = s.UserID,
                ProfileType = s.ProfileType
            }).ToList();
        }

        public bool ApprovesUserImages(long approvedBy, long userId, bool approved, string comments)
        {
            var user = _craftsDb.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {
                user.IsImageVideoContentModified = approved;
                user.ContentApproverComments = comments;
                _craftsDb.SaveChanges();
                return true;
            }
            return false;
        }

        public List<AdminUserResponseDto> getAllUsersForAdminApproval(int profileType)
        {
            int? profile = profileType != 0 ? profileType : 0;
            return _craftsDb.Users.Where(u => !(u.IsAdminApproved == true) && u.IsContentApproverApproved == true && (profile == 0 ? true : u.ProfileType == profile)).Select(s => new AdminUserResponseDto
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                PhoneNum = s.PhoneNum,
                EmailID = s.EmailID,
                UserId = s.UserID,
                ProfileType = s.ProfileType
            }).ToList();
        }

        public bool AdminApprovesUser(long approvedBy, long userId, bool approved, string comments)
        {
            var user = _craftsDb.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {
                user.IsAdminApproved = approved;
                user.AdminComments  = comments;
                _craftsDb.SaveChanges();

                if (user.IsEmailApproved == true)
                {
                    var data = _craftsDb.EmailTemplates.Where(e => approved ? (e.TemplateName == "AdminApproved") : (e.TemplateName == "AdminRejected")).FirstOrDefault();
                    if (data != null)
                    {
                        EmailOutbox obj = new EmailOutbox();
                        obj.DateSent = DateTime.Now;
                        obj.EmailBody = data.TemplateHTML.Replace("[Name]", user.FirstName);
                        obj.EmailSubject = data.EmailSubject;
                        obj.EmailTo = user.EmailID;
                        _craftsDb.EmailOutboxes.Add(obj);
                        //_craftsDb.SaveChanges();

                        Notification notify = new Notification();
                        notify.CreatedDate = DateTime.Now;
                        notify.IsActive = true;
                        notify.IsViewed = false;
                        notify.ToUserId = user.UserID;
                        notify.Notification1 = approved ? "Admin Approved" : "Admin Rejected";
                        _craftsDb.Notifications.Add(notify);
                        _craftsDb.SaveChanges();

                        return MailSender.MailSend(user.EmailID, data.EmailSubject, data.TemplateHTML.Replace("[Name]", user.FirstName));
                    }
                }

                return true;
            }
            return false;
        }

        public int PendingApprovalUsersCount()
        {
            return _craftsDb.Users.Where(u => u.IsAdminApproved != true || u.IsContentApproverApproved != true).Count();
        }

        public int PendingApprovalImagesCount()
        {
            return _craftsDb.Users.Where(u => u.IsImageVideoContentModified == true).Count();
        }

        public Dictionary<string, int> getAllCounts()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("PendingApprovalUsersCount", _craftsDb.Users.Where(u => u.IsAdminApproved != true || u.IsContentApproverApproved != true).Count());
            dictionary.Add("PendingApprovalImagesCount", _craftsDb.Users.Where(u => u.IsImageVideoContentModified == true).Count());
            dictionary.Add("PendingSpamUsersCount", _craftsDb.SpamLists.Where(s => s.IsApprovedAdmin != true).Count());
            return dictionary;
        }

        public List<MessageDeliveryConfigurationDto> GetMessagesConfigurationSettings()
        {
            return _craftsDb.MessageDeliveryConfigurations.Select(s => new MessageDeliveryConfigurationDto
            {
                Id = s.Id,
                UserType = s.UserType,
                CreatedDate = s.CreatedDate,
                IsActive =s.IsActive,
                DeliveryHours = s.DeliveryHours
            }).ToList();
        }

        public bool SaveMessagesConfigurationSettings(MessageDeliveryConfigurationDto setting)
        {
            if (setting.Id != 0)
            {
                var data = _craftsDb.MessageDeliveryConfigurations.Where(m => m.Id == setting.Id).FirstOrDefault();
                if (data != null)
                {
                    data.IsActive = setting.IsActive;
                    data.DeliveryHours = setting.DeliveryHours;
                    data.ModifiedDate = DateTime.Now;
                    _craftsDb.SaveChanges();

                }
            }
            else
            {
                MessageDeliveryConfiguration data = new MessageDeliveryConfiguration();
                data.DeliveryHours = setting.DeliveryHours;
                data.CreatedDate = DateTime.Now;
                data.CreatedByUserId = setting.CreatedByUserId;
                data.IsActive = setting.IsActive;
                _craftsDb.MessageDeliveryConfigurations.Add(data);
                _craftsDb.SaveChanges();
            }
            return true;
        }

        public List<NotificationDto> GetMyActiveNotifications(long userId)
        {
            return _craftsDb.Notifications.Where(n => n.ToUserId == userId && n.IsViewed == false).Select(s => new NotificationDto
            {
                NotificationId = s.NotificationId,
                Notification = s.Notification1,
                ToUserId = s.ToUserId,
                CreatedDate = s.CreatedDate
            }).ToList();
        }

        public bool InActiveNotification(int NotificationId, long userId)
        {
            var data = _craftsDb.Notifications.Where(n => n.NotificationId == NotificationId).FirstOrDefault();
            if (data != null)
            {
                data.IsViewed = true;
                _craftsDb.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<ContentApproverUserDto> GetAllContentApprovers()
        {
            return (from user in _craftsDb.Users
                        join roleTie in _craftsDb.User_Role_Tie on user.UserID equals roleTie.UserID
                        join roles in _craftsDb.Roles on roleTie.RoleID equals roles.RoleID
                    where roles.RoleName == "ContentApprover"
                    select new ContentApproverUserDto { CustId = user.CustomerId, EmailID = user.EmailID, FirstName = user.FirstName, LastName = user.LastName, PhoneNum = user.PhoneNum, IsAccessNewUsers = roleTie.IsAccessNewUsers, IsAccessImageVideoUpdate = roleTie.IsAccessImageVideoUpdate, IsAccessReportedSpam = roleTie.IsAccessReportedSpam }).ToList();
        }

        public bool AddUpdateContentApprover(ContentApproverUserDto userDto)
        {
            if (userDto.UserID == 0)
            {
                User user = new User();
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Password = userDto.Password;
                user.EmailID = userDto.EmailID;
                user.CustomerId = RandomString(8);
                user.IsActive = true;
                user.PhoneNum = userDto.PhoneNum;
                _craftsDb.Users.Add(user);
                _craftsDb.SaveChanges();

                long userId = _craftsDb.Users.OrderByDescending(u => u.UserID).FirstOrDefault().UserID;
                User_Role_Tie userRoleTie = new User_Role_Tie();
                userRoleTie.UserID = userId;
                userRoleTie.RoleID = 2;
                userRoleTie.IsActive = true;
                userRoleTie.CreatedBy_UserID = userDto.CreatedByUserID;
                userRoleTie.IsAccessNewUsers = userDto.IsAccessNewUsers;
                userRoleTie.IsAccessImageVideoUpdate = userDto.IsAccessImageVideoUpdate;
                userRoleTie.IsAccessReportedSpam = userDto.IsAccessReportedSpam;
                _craftsDb.User_Role_Tie.Add(userRoleTie);
                _craftsDb.SaveChanges();
            }
            else
            {
                User user = _craftsDb.Users.Where(u => u.UserID == userDto.UserID).FirstOrDefault();
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Password = userDto.Password;
                user.EmailID = userDto.EmailID;
                user.IsActive = true;
                user.PhoneNum = userDto.PhoneNum;

                User_Role_Tie userRoleTie = _craftsDb.User_Role_Tie.Where(u => u.UserID == userDto.UserID).FirstOrDefault();
                userRoleTie.IsActive = true;
                userRoleTie.CreatedBy_UserID = userDto.CreatedByUserID;
                userRoleTie.IsAccessNewUsers = userDto.IsAccessNewUsers;
                userRoleTie.IsAccessImageVideoUpdate = userDto.IsAccessImageVideoUpdate;
                userRoleTie.IsAccessReportedSpam = userDto.IsAccessReportedSpam;
                _craftsDb.SaveChanges();
            }
            return true;
        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool DeleteContentApprover(long ByUserId, long DeleteUserId)
        {
            try
            {
                var user = _craftsDb.Users.Where(u => u.UserID == DeleteUserId).FirstOrDefault();
                if (user != null)
                {
                    user.IsActive = false;
                    user.ModifiedDate = DateTime.Now;
                    _craftsDb.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<StatsProfileResponseDto> GetStatsAllProfiles()
        {
            return (_craftsDb.Users.Select(s => new StatsProfileResponseDto { UserID = s.UserID, CustId = s.CustomerId, FirstName = s.FirstName, IsActive = s.IsActive, IsPaymentUser = s.IsPaymentUser, PhoneNum = s.PhoneNum, ProfileType = s.ProfileType })).ToList();
        }

        public IEnumerable<StatsOpportunityResponseDto> GetStatsOpportunitiesInfo()
        {
            return _craftsDb.Users.Where(u => u.IsActive == true).Select(s => new StatsOpportunityResponseDto
            {
                UserID = s.UserID,
                CustId = s.CustomerId,
                FirstName = s.FirstName,
                NumOfOpportunitiesCreated = s.Opportunities.Where(o => o.CreatedByUserId == s.UserID).Count(),
                NumOfOpportunitiesApplied = s.OpportunityResponse_User_Tie.Where(ort => ort.UserId == s.UserID).Count()
            }).ToList();
        }

        public IEnumerable<ActiveProfileDto> GetAllActiveProfiles()
        {
            var res = (from user in _craftsDb.Users
                       join userInfo in _craftsDb.UserInfoes on user.UserID equals userInfo.UserId
                       join profession in _craftsDb.Professions on userInfo.PrimaryProfessionId equals profession.ProfessionId
                       where user.IsActive == true
                       select new ActiveProfileDto
                           {
                               CustId = user.CustomerId,
                               UserID = user.UserID,
                               FirstName = user.FirstName,
                               EmailID = user.EmailID,
                               PhoneNum = user.PhoneNum,
                               ProfileType = user.ProfileType,
                               PrimaryProfession = profession.ProfessionName,
                               ActiveDays = 10
                           }).ToList();
            return res;
        }

        public IEnumerable<AdvertisementDto> GetAllAdvertisements()
        {
            return _craftsDb.Advertisements.Select(s => new AdvertisementDto
            {
                AdvertisementId=s.AdvertisementId,
                AdvertisementUrl = s.AdvertisementUrl,
                AdvertisementType =s.AdvertisementType,
                UserId = s.UserId,
                CustId = _craftsDb.Users.Where(u=>u.UserID == s.UserId).FirstOrDefault().CustomerId,
                Comments = s.Comments,
                IsApproved = s.IsApproved,
                FromDate = s.FromDate,
                ToDate=s.ToDate                 
            }).ToList();
        }

        public bool AddOrUpdateAdvertisement(AdvertisementDto addDto)
        {
            if (addDto.AdvertisementId != 0)
            {
                var add = _craftsDb.Advertisements.Where(a => a.AdvertisementId == addDto.AdvertisementId).FirstOrDefault();
                add.AdvertisementType = addDto.AdvertisementType;
                add.AdvertisementUrl = addDto.AdvertisementUrl;
                add.AdvertisementType = addDto.AdvertisementType;
                add.FromDate = addDto.FromDate;
                add.ToDate = addDto.ToDate;
            }
            else
            {
                Advertisement add = new Advertisement();
                add.AdvertisementType = addDto.AdvertisementType;
                add.AdvertisementUrl = addDto.AdvertisementUrl;
                add.AdvertisementType = addDto.AdvertisementType;
                add.FromDate = addDto.FromDate;
                add.ToDate = addDto.ToDate;
                add.UserId = addDto.UserId;
                _craftsDb.Advertisements.Add(add);
            }
            _craftsDb.SaveChanges();
            return true;
        }

        public IEnumerable<AdvertisementDto> GetAllClientAdvertisements()
        {
            return _craftsDb.Advertisements.Where(a=>a.IsActive == true && a.IsApproved == true).Select(s => new AdvertisementDto
            {
                AdvertisementId = s.AdvertisementId,
                AdvertisementUrl = s.AdvertisementUrl,
                AdvertisementType = s.AdvertisementType,
                UserId = s.UserId,
                CustId = _craftsDb.Users.Where(u => u.UserID == s.UserId).FirstOrDefault().CustomerId,
                Comments = s.Comments,
                IsApproved = s.IsApproved,
                FromDate = s.FromDate,
                ToDate = s.ToDate
            }).ToList();
        }

        public bool ApproveAdvertisements(long ByUserId, int AdvertisementId, bool isApproved, string comments)
        {
            var add = _craftsDb.Advertisements.Where(a => a.AdvertisementId == AdvertisementId).FirstOrDefault();
            if (add != null)
            {
                add.IsApproved = isApproved;
                add.IsApprovedBy = ByUserId;
                add.Comments = comments;
                _craftsDb.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SendEmailOrMessage(long userId, bool isMobile, bool isEmail, string message)
        {
            var user = _craftsDb.Users.Where(u => u.UserID == userId).FirstOrDefault();
            if (user != null)
            {
                if (isMobile && !string.IsNullOrEmpty(user.PhoneNum))
                {
                    OTPSend(user.PhoneNum, message);
                }
                if (isEmail && !string.IsNullOrEmpty(user.EmailID))
                {
                    MailSender.MailSend(user.EmailID, "Notification from My24Crafts", message);
                }
            }
            return false;
        }

        public bool Test()
        {
            try
            {
                return _craftsDb.Users.Any(u => u.UserID == 1);
            }
            catch (Exception e)
            {
                File.WriteAllText(@"C:\error.txt", e.StackTrace + e.Message + e.InnerException);
                throw e;
            }
        }
    }
}
