using Jupiter._24Crafts.Common.Resources;
using Jupiter._24Crafts.Data.Dtos.Account;
using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Account.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CratfsDb cratfsDb;
        public AccountRepository(CratfsDb cratfsDb)
        {
            this.cratfsDb = cratfsDb;
        }
        /// <summary>
        /// Method for validating user by email and pwd and sending response
        /// </summary>
        /// <param name="emailId">emailId or MblNum</param>
        /// <param name="password">pwd</param>
        /// <returns></returns>
        public ResponseUserDto ValidateUser(string custIdOrMobileNum, string password)
        {
            var user = cratfsDb.Users.Where(ur => (ur.CustomerId == custIdOrMobileNum || ur.PhoneNum == custIdOrMobileNum) && ur.Password == password && ur.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                user.LastSignIn = DateTime.Now;

                cratfsDb.SaveChanges();
                ResponseUserDto retUser = new ResponseUserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserID = user.UserID
                };
                return retUser;
            }
            else {
                return null;
            }
        }

        public ResponseUserDto ValidateOTPUser(string mobileNum, string otp)
        {
            if (!string.IsNullOrEmpty(otp))
            {
                var obj = cratfsDb.OTPs.Where(_ => _.MobileNum == mobileNum && _.OTP1 == otp && _.IsActive == true).FirstOrDefault();
                if (obj != null)
                {
                    obj.IsActive = false;

                    var user = cratfsDb.Users.Where(ur => ur.PhoneNum == mobileNum && ur.IsActive == true).FirstOrDefault();
                    user.LastSignIn = DateTime.Now;

                    cratfsDb.SaveChanges();
                    ResponseUserDto retUser = new ResponseUserDto()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserID = user.UserID
                    };
                    return retUser;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<MainMenuDto> GetMenuByUserId(long userId)
        {
            return cratfsDb.Role_Menu_Tie.Where(umt => umt.RoleId == (cratfsDb.User_Role_Tie.Where(urt => urt.UserID == userId).FirstOrDefault().RoleID)).Select(main => new MainMenuDto
            {
                Name = main.MainMenu.Name,
                Description = main.MainMenu.Description,
                NavigateURL = main.MainMenu.NavigateURL,
                SortOrder = main.MainMenu.SortOrder
            }).ToList();
        }

        public string[] GetRolesByUserId(long userId)
        {
            return cratfsDb.User_Role_Tie.Where(urt => urt.UserID == userId).Select(res => res.Role.RoleName).ToArray();
        }

        public void Registration(RegisterDto registerDto)
        {

            User user = new User();
            user.FirstName = registerDto.FirstName;
            user.CreatedDate = DateTime.Now;
            user.EmailID = registerDto.EmailID;
            user.IsActive = true;
            user.IsPaymentUser = registerDto.IsPaymentUser;
            user.IsProfileCreated = true;
            user.LastName = registerDto.LastName;
            user.ModifiedDate = null;
            user.PhoneNum = registerDto.PhoneNum;
            user.Password = registerDto.Password;
            user.ProfileType = registerDto.ProfileType;
            user.CustomerId = RandomString(8);
            cratfsDb.Users.Add(user);
            cratfsDb.SaveChanges();

            var data = cratfsDb.EmailTemplates.Where(e => e.TemplateName == "UserRegisteredSuccessTemplate").FirstOrDefault();
            if (data != null)
            {
                EmailOutbox obj = new EmailOutbox();
                obj.DateSent = DateTime.Now;
                obj.EmailBody = data.TemplateHTML.Replace("[Password]", user.Password);
                obj.EmailSubject = data.EmailSubject;
                obj.EmailTo = registerDto.EmailID;
                cratfsDb.EmailOutboxes.Add(obj);
                cratfsDb.SaveChanges();
                MailSender.MailSend(registerDto.EmailID, data.EmailSubject, data.TemplateHTML.Replace("[Password]", user.Password));
            }
        }


        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public UserDto GetUserInfoByUserId(long userId)
        {
            return cratfsDb.Users.Where(ur => ur.UserID == userId).Select(s => new UserDto()
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                UserID = s.UserID,
                EmailID = s.EmailID,
                PhoneNum = s.PhoneNum,
                CustomerId =  s.CustomerId
            }).FirstOrDefault();
        }
    }
}
