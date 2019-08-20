using Jupiter._24Crafts.Data.Dtos.Portfolio;
using Jupiter._24Crafts.Data.Repositories.Portfolio.Interface;
using Jupiter._24Crafts.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using EntityFramework.Extensions;
using Jupiter._24Crafts.Common.Resources;

namespace Jupiter._24Crafts.Data.Repositories.Portfolio
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly CratfsDb _craftsDb;

        public PortfolioRepository(CratfsDb craftsDb)
        {
            this._craftsDb = craftsDb;
        }

        /// <summary>
        /// Local folder path for saving images
        /// </summary>
        public static string LocalFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageDirectory"];
            }
        }
 
        public object searchProfiles(SearchProfileRequestDto searchObj)
        {
            var popularProfilesList = searchObj != null ? _craftsDb.Users.Where(u => !string.IsNullOrEmpty(u.FirstName) && (!string.IsNullOrEmpty(searchObj.Name) ? u.FirstName.Contains(searchObj.Name) : true) && u.IsActive == true && (!string.IsNullOrEmpty(searchObj.City) ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && ui.City.Contains(searchObj.City)) : true) && (searchObj.Age != 0 ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && (ui.Age >= searchObj.Age && ui.Age <= searchObj.Age)) : true) && (searchObj.Gender != 0 ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && ui.Gender == searchObj.Gender) : true) && (searchObj.AvailableStartDate != null ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && (ui.AvailabilityFrom >= searchObj.AvailableStartDate && ui.AvailabilityTo <= searchObj.AvailableStartDate)) : true)).ToList() : _craftsDb.Users.Take(5).ToList();
            var editorsChoiceProfilesList = searchObj != null ? _craftsDb.Users.Where(u => !string.IsNullOrEmpty(u.FirstName) && (!string.IsNullOrEmpty(searchObj.Name) ? u.FirstName.Contains(searchObj.Name) : true) && u.IsActive == true && (!string.IsNullOrEmpty(searchObj.City) ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && ui.City.Contains(searchObj.City)) : true) && (searchObj.Age != 0 ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && (ui.Age >= searchObj.Age && ui.Age <= searchObj.Age)) : true) && (searchObj.Gender != 0 ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && ui.Gender == searchObj.Gender) : true) && (searchObj.AvailableStartDate != null ? u.UserInfoes.Any(ui => ui.UserId == u.UserID && (ui.AvailabilityFrom >= searchObj.AvailableStartDate && ui.AvailabilityTo <= searchObj.AvailableStartDate)) : true)).ToList() : _craftsDb.Users.Take(5).ToList();
            List<string> professions = new List<string>() { "Actor", "Lawyer" };
            return new
            {
                PopularProfiles = popularProfilesList != null ? popularProfilesList.Select(s => new ProfileResponseDto
                {
                    UserId = s.UserID,
                    Name = s.FirstName,
                    City ="Hyd",
                    Profession = professions,
                    //City =  s.UserInfoes.Where(info => info.UserId == s.UserID).SingleOrDefault() != null ? (_craftsDb.Cities.Where(c=>c.CityID == Convert.ToInt32(s.UserInfoes.Where(info => info.UserId == s.UserID).SingleOrDefault().City)).SingleOrDefault() != null ? (_craftsDb.Cities.Where(c=>c.CityID == Convert.ToInt32(s.UserInfoes.Where(info => info.UserId == s.UserID).SingleOrDefault().City)).SingleOrDefault().CityName) : string.Empty ) : string.Empty,
                    //Profession = s.Profession_User_Tie.Where(p => p.UserID == s.UserID).Select(ps => ps.Profession.ProfessionName).ToList(),
                    //ImageSrc = ProfilePicImageUrl(s.PhoneNum)
                    ImageSrc = @"D:\24Crafts\1.png"
                }).ToList() : null,
                EditorChoiceProfiles = editorsChoiceProfilesList != null ? editorsChoiceProfilesList.Select(s => new ProfileResponseDto
                {
                    UserId = s.UserID,
                    Name = s.FirstName,
                    City = "Bangalore",
                    Profession = professions,
                    //City = s.UserInfoes.Where(info => info.UserId == s.UserID).SingleOrDefault() != null ? (_craftsDb.Cities.Where(c => c.CityID == Convert.ToInt32(s.UserInfoes.Where(info => info.UserId == s.UserID).SingleOrDefault().City)).SingleOrDefault() != null ? (_craftsDb.Cities.Where(c => c.CityID == Convert.ToInt32(s.UserInfoes.Where(info => info.UserId == s.UserID).SingleOrDefault().City)).SingleOrDefault().CityName) : string.Empty) : string.Empty,
                    //Profession = s.Profession_User_Tie.Where(p => p.UserID == s.UserID).Select(ps => ps.Profession.ProfessionName).ToList(),
                    //ImageSrc = ProfilePicImageUrl(s.PhoneNum)
                    ImageSrc = @"D:\24Crafts\2.png"
                }).ToList() : null
            };
        }

        /// <summary>
        /// This method id used to check folder existed or not, if not will creat folder
        /// </summary>
        /// <param name="folderName"></param>
        public string ProfilePicImageUrl(string custId )
        {
            try
            {
                // Checking folder is existed or not
                if (System.IO.Directory.Exists(@LocalFolderPath  + custId + "/ProfilePics/"))
                {
                    var directory = new DirectoryInfo(@LocalFolderPath + "/" + custId + "/ProfilePics/");
                    var myFile = directory.GetFiles().Count() > 0 ? directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First() : null;
                    return myFile != null ? (myFile.FullName.Replace(@LocalFolderPath, "/Images/")).Replace(@"\", "/") : "";
                }
                else {
                    //return  "/Images/DefaultImages/DefaultProfile.png";
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method id used to check folder existed or not, if not will creat folder
        /// </summary>
        /// <param name="folderName"></param>
        public List<string> AllImageUrls(string custId)
        {
            List<string> imagesList = new List<string>();
            try
            {
               
                // Checking folder is existed or not
                if (System.IO.Directory.Exists(@LocalFolderPath + custId + "/Pics/"))
                {
                    var directory = new DirectoryInfo(@LocalFolderPath + "/" + custId + "/Pics/");
                    var myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();
                    for (int i = 0; i < myFile.Count; i++)
                    {
                        if (myFile[i].FullName.Contains("24Crafts"))
                        imagesList.Add((myFile[i].FullName.Replace(@LocalFolderPath, "/Images/")).Replace(@"\", "/"));
                        // Console.WriteLine(String.Format(@"{0}/{1}", folderName, Images[i].Name));
                    }
                    return imagesList;
                }
                return imagesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method id used to check folder existed or not, if not will creat folder
        /// </summary>
        /// <param name="folderName"></param>
        public List<string> AllVideosUrls(string custId)
        {
            List<string> videosList = new List<string>();
            try
            {
                // Checking folder is existed or not
                if (System.IO.Directory.Exists(@LocalFolderPath + custId + "/Videos/"))
                {
                    var directory = new DirectoryInfo(@LocalFolderPath + "/" + custId + "/Videos/");
                    var myFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();
                    for (int i = 0; i < myFile.Count; i++)
                    {
                        if (myFile[i].FullName.Contains("24Crafts"))
                        videosList.Add((myFile[i].FullName.Replace(@LocalFolderPath, "/Images/")).Replace(@"\", "/"));
                        // Console.WriteLine(String.Format(@"{0}/{1}", folderName, Images[i].Name));
                    }
                    return videosList;
                }
                return videosList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void createPortfolio(PortfolioDto profile)
        {
            var existProfile = _craftsDb.Users.Where(u => u.UserID == profile.UserID).FirstOrDefault();
            if(existProfile != null)
            {
                existProfile.IsActive = profile.IsActive;
                existProfile.IsPaymentUser = profile.IsPaymentUser;
                existProfile.IsImageVideoContentModified = profile.IsImageVideoContentModified;
                if (profile.IsImageVideoContentModified)
                {
                    existProfile.IsContentApproverApproved = false;
                    existProfile.IsAdminApproved = false;
                }

                _craftsDb.SaveChanges();

                var existUserInfo = _craftsDb.UserInfoes.Where(u => u.UserId == profile.UserID).FirstOrDefault();

                if (existUserInfo == null)
                {
                    UserInfo userInfo = new UserInfo()
                    {
                        UserId = existProfile.UserID,
                        Gender = profile.userInfoObj.Gender,
                        Age = profile.userInfoObj.Age,
                        MaritalStatus = profile.userInfoObj.MaritalStatus,
                        Hobbies = profile.userInfoObj.Hobbies,
                        SelfDescription = profile.userInfoObj.SelfDescription,
                        Qualification = profile.userInfoObj.Qualification,
                        ExpYear = profile.userInfoObj.ExpYear,
                        ExpMonth = profile.userInfoObj.ExpMonth,
                        CurrAssignment = profile.userInfoObj.CurrAssignment,
                        LastAssignment = profile.userInfoObj.LastAssignment,
                        AvailabilityFrom = profile.userInfoObj.AvailabilityFrom,
                        AvailabilityTo = profile.userInfoObj.AvailabilityTo,
                        City = profile.userInfoObj.City,
                        State = profile.userInfoObj.State,
                        Country = profile.userInfoObj.Country,
                        PrimaryProfessionId=profile.userInfoObj.PrimaryProfessionId,
                        GoogleDriveUrl = profile.userInfoObj.GoogleDriveUrl,
                        YputubeChannel = profile.userInfoObj.YputubeChannel,
                        Remunaration = profile.userInfoObj.Remunaration,
                        CountryCode = Convert.ToInt32(profile.countryCodes)
                    };

                    _craftsDb.UserInfoes.Add(userInfo);
                    _craftsDb.SaveChanges();
                }
                else
                {
                    existUserInfo.Gender = profile.userInfoObj.Gender;
                    existUserInfo.Age = profile.userInfoObj.Age;
                    existUserInfo.MaritalStatus = profile.userInfoObj.MaritalStatus;
                    existUserInfo.Hobbies = profile.userInfoObj.Hobbies;
                    existUserInfo.SelfDescription = profile.userInfoObj.SelfDescription;
                    existUserInfo.Qualification = profile.userInfoObj.Qualification;
                    existUserInfo.ExpYear = profile.userInfoObj.ExpYear;
                    existUserInfo.ExpMonth = profile.userInfoObj.ExpMonth;
                    existUserInfo.Remunaration = profile.userInfoObj.Remunaration;
                    existUserInfo.CurrAssignment = profile.userInfoObj.CurrAssignment;
                    existUserInfo.LastAssignment = profile.userInfoObj.LastAssignment;
                    existUserInfo.AvailabilityFrom = profile.userInfoObj.AvailabilityFrom;
                    existUserInfo.AvailabilityTo = profile.userInfoObj.AvailabilityTo;
                    existUserInfo.City = profile.userInfoObj.City;
                    existUserInfo.State = profile.userInfoObj.State;
                    existUserInfo.Country = profile.userInfoObj.Country;
                    existUserInfo.PrimaryProfessionId=profile.userInfoObj.PrimaryProfessionId;
                    existUserInfo.GoogleDriveUrl = profile.userInfoObj.GoogleDriveUrl;
                    existUserInfo.YputubeChannel = profile.userInfoObj.YputubeChannel;
                    existUserInfo.CountryCode = Convert.ToInt32(profile.countryCodes);

                    _craftsDb.SaveChanges();
                }

                _craftsDb.Language_User_Tie.Where(l => l.UserID == profile.UserID).Delete();
                _craftsDb.SaveChanges();

                if (!string.IsNullOrEmpty(profile.LanguageIds))
                {
                    string[] languageId = profile.LanguageIds.Split(',');
                    List<Language_User_Tie> listLut = new List<Language_User_Tie>();
                    for (int i = 0; i < languageId.Length; i++)
                    {
                        var lut = new Language_User_Tie();
                        lut.UserID = profile.UserID;
                        lut.LanguageId = Convert.ToInt32(languageId[i]);
                        lut.IsActive = true;
                        lut.CreatedDate = System.DateTime.Now;
                        listLut.Add(lut);
                    }
                    _craftsDb.Language_User_Tie.AddRange(listLut);
                    _craftsDb.SaveChanges();
                }

                _craftsDb.Profession_User_Tie.Where(p => p.UserID == profile.UserID).Delete();
                _craftsDb.SaveChanges();

                if (!string.IsNullOrEmpty(profile.ProfessionIds))
                {
                    string[] professionId = profile.ProfessionIds.Split(',');
                    List<Profession_User_Tie> listPut = new List<Profession_User_Tie>();
                    for (int i = 0; i < professionId.Length; i++)
                    {
                        var put = new Profession_User_Tie();
                        put.UserID = profile.UserID;
                        put.ProfessionID = Convert.ToInt32(professionId[i]);
                        put.IsActive = true;
                        put.CreatedDate = System.DateTime.Now;
                        listPut.Add(put);
                    }
                    _craftsDb.Profession_User_Tie.AddRange(listPut);
                    _craftsDb.SaveChanges();
                }

                if (profile.IsImageVideoContentModified && existProfile.IsEmailApproved == true)
                {
                    var data = _craftsDb.EmailTemplates.Where(e => e.TemplateName == "UserModifiedImageVideoContent").FirstOrDefault();
                    if (data != null)
                    {
                        EmailOutbox obj = new EmailOutbox();
                        obj.DateSent = DateTime.Now;
                        obj.EmailBody = data.TemplateHTML.Replace("[Name]", profile.FirstName);
                        obj.EmailSubject = data.EmailSubject;
                        obj.EmailTo = Settings.ContentApproverEmailId;
                        _craftsDb.EmailOutboxes.Add(obj);
                        _craftsDb.SaveChanges();

                        Notification notify = new Notification();
                        notify.CreatedDate = DateTime.Now;
                        notify.IsActive = true;
                        notify.IsViewed = false;
                        notify.ToUserId = (from r in _craftsDb.Roles
                         join t in _craftsDb.User_Role_Tie on r.RoleID equals t.RoleID
                         join user in _craftsDb.Users on t.UserID equals user.UserID
                                           where r.RoleName == "Admin"
                                           select user.UserID).FirstOrDefault();
                        notify.Notification1 = "User Modified Image or Video Content";
                        _craftsDb.Notifications.Add(notify);
                        _craftsDb.SaveChanges();

                        Notification notify2 = new Notification();
                        notify2.CreatedDate = DateTime.Now;
                        notify2.IsActive = true;
                        notify2.IsViewed = false;
                        notify2.ToUserId = (from r in _craftsDb.Roles
                                            join t in _craftsDb.User_Role_Tie on r.RoleID equals t.RoleID
                                            join user in _craftsDb.Users on t.UserID equals user.UserID
                                            where r.RoleName == "ContentApprover"
                                            select user.UserID).FirstOrDefault();
                        notify2.Notification1 = "User Modified Image or Video Content";
                        _craftsDb.Notifications.Add(notify2);
                        _craftsDb.SaveChanges();

                        MailSender.MailSend(Settings.ContentApproverEmailId, data.EmailSubject, data.TemplateHTML.Replace("[Name]", profile.FirstName));
                        MailSender.MailSend(Settings.AdminEmailId, data.EmailSubject, data.TemplateHTML.Replace("[Name]", profile.FirstName));
                    }
                }
            }
        }
        public IEnumerable<ProfessionDto> getAllProfessions(bool isAdmin)
        {
          return  _craftsDb.Professions.Where(p => isAdmin ? true : p.IsActive == true).Select(s => new ProfessionDto
            {
                ProfessionId = s.ProfessionId,
                ProfessionName = s.ProfessionName,
                isActive = s.IsActive
            }).ToList();
        }

        public object getProfileByUserId(long userId)
        {
            var res = _craftsDb.Users.Where(u => u.UserID == userId).Select(s => new PortfolioDto
            {
                CustId = s.CustomerId,
                UserID = s.UserID,
                FirstName = s.FirstName,
                LastName = s.LastName,
                EmailID = s.EmailID,
                PhoneNum = s.PhoneNum,
                ProfileType = s.ProfileType,
                Views = s.Views,
                IsPaymentUser = s.IsPaymentUser,
                IsEmailApproved = s.IsEmailApproved,
                NumOfOpportunitiesCreated = s.NumOfOpportunitiesCreated,
                LanguageInfo = (_craftsDb.Languages.Select(ls => new LanguageInfo { LanguageId = ls.LanguageId, Name = ls.LanguageName, isSelected = s.Language_User_Tie.Any(l => l.UserID == userId && l.LanguageId == ls.LanguageId) })).ToList(),
                ProfessionInfo = (_craftsDb.Professions.Select(ps => new ProfInfo { ProfessionId = ps.ProfessionId, Name = ps.ProfessionName, isSelected = s.Profession_User_Tie.Any(p => p.UserID == userId && p.ProfessionID == ps.ProfessionId) })).ToList(),
                userInfoObj = s.UserInfoes.Where(a => a.UserId == userId).Select(b => new UserInfoDto { Gender = b.Gender, Age = b.Age, MaritalStatus = b.MaritalStatus, Hobbies = b.Hobbies, SelfDescription = b.SelfDescription, Qualification = b.Qualification, ExpYear = b.ExpYear, ExpMonth = b.ExpMonth, CurrAssignment = b.CurrAssignment, LastAssignment = b.LastAssignment, AvailabilityFrom = b.AvailabilityFrom, AvailabilityTo = b.AvailabilityTo, City = b.City, State = b.State, Country = b.Country, PrimaryProfessionId = b.PrimaryProfessionId, GoogleDriveUrl = b.GoogleDriveUrl, YputubeChannel = b.YputubeChannel,Remunaration = b.Remunaration }).FirstOrDefault(),
                IsBusinessEnabled = (_craftsDb.Users.Any(b => b.PhoneNum == s.PhoneNum && b.ProfileType == 2)),
                countryCodes = s.UserInfoes.Where(a=>a.UserId == s.UserID).FirstOrDefault().CountryCode.ToString(),
                BusinessObj = (_craftsDb.Users.Any(b => b.PhoneNum == s.PhoneNum && b.ProfileType == 2) ? (_craftsDb.Users.Where(b => b.PhoneNum == s.PhoneNum && b.ProfileType == 2).Select(ss =>
                   new BusinessProfile {
                       BusinessUserId =ss.UserID,
                       BusinessCustId = ss.CustomerId,
                       Name = ss.FirstName,
                       CurrentAssignment = ss.UserInfoes.Where(ssu => ssu.UserId == ss.UserID).FirstOrDefault().CurrAssignment,
                       GoogleDriveUrl = ss.UserInfoes.Where(ssu => ssu.UserId == ss.UserID).FirstOrDefault().GoogleDriveUrl,
                       EmailID = ss.EmailID,
                       PhoneNum = ss.PhoneNum,
                       Remunaration = ss.UserInfoes.Where(ssu => ssu.UserId == ss.UserID).FirstOrDefault().Remunaration
                   }).FirstOrDefault()) : null),
            }).FirstOrDefault();

            if (res != null)
            {
                res.ProfilePicPathUrl = ProfilePicImageUrl(res.CustId);
                res.ImagesPathUrls = AllImageUrls(res.CustId);
                res.VideoPathUrls = AllVideosUrls(res.CustId);
            }

            return res;
        }

        public IEnumerable<ProfessionDto> getAllProfessionsByUserId(long userId)
        {
            return _craftsDb.Profession_User_Tie.Where(p => p.IsActive == true && p.UserID == userId).Select(s => new ProfessionDto
            {
                ProfessionId = s.ProfessionID,
                ProfessionName = s.Profession.Description
            }).ToList();
        }

        public IEnumerable<LanguageDto> getAllLanguages(bool isAdmin)
        {
            return _craftsDb.Languages.Where(p => isAdmin ? true : p.IsActive == true).Select(s => new LanguageDto
            {
                LanguageId = s.LanguageId,
                LanguageName = s.LanguageName,
                isActive = s.IsActive
            }).ToList();
        }

        public IEnumerable<LanguageDto> getAllLanguagesByUserId(long userId)
        {
            return _craftsDb.Language_User_Tie.Where(p => p.IsActive == true && p.UserID == userId).Select(s => new LanguageDto
            {
                LanguageId = s.LanguageId,
                LanguageName = s.Language.LanguageName
            }).ToList();
        }

        public bool Validateprofile(string mobileNum, int profileType)
        {
            return !_craftsDb.Users.Any(u => u.PhoneNum == mobileNum && u.ProfileType == profileType && u.IsActive == true);
        }

        public bool ValidateEmail(string mobileNum, string emailid, int profileType, long userId)
        {
            if (!string.IsNullOrEmpty(emailid) && userId == 0)
            {
                return !_craftsDb.Users.Any(u => u.EmailID == emailid);
            }
            else if (userId != 0 && !string.IsNullOrEmpty(emailid))
            {
                return !_craftsDb.Users.Any(u => u.EmailID == emailid && !(u.UserID == userId ));
            }
            else
            {
                return !_craftsDb.Users.Any(u => u.UserID == userId && u.ProfileType == profileType);
            }
        }

        public void createBusinessProfie(PortfolioDto profile)
        {
            var existProfile = _craftsDb.Users.Where(u => u.UserID == profile.UserID).FirstOrDefault();
            if (existProfile != null)
            {
                existProfile.IsActive = profile.IsActive;
                existProfile.IsPaymentUser = profile.IsPaymentUser;
                existProfile.Views = 0;

                _craftsDb.SaveChanges();

                var existUserInfo = _craftsDb.UserInfoes.Where(u => u.UserId == profile.UserID).FirstOrDefault();

                if (existUserInfo == null)
                {
                    UserInfo userInfo = new UserInfo()
                    {
                        UserId = existProfile.UserID,
                        CurrAssignment = profile.userInfoObj.CurrAssignment,
                        GoogleDriveUrl = profile.userInfoObj.GoogleDriveUrl,
                    };

                    _craftsDb.UserInfoes.Add(userInfo);
                    _craftsDb.SaveChanges();
                }
                else
                {

                    existUserInfo.CurrAssignment = profile.userInfoObj.CurrAssignment;
                    existUserInfo.GoogleDriveUrl = profile.userInfoObj.GoogleDriveUrl;
                    _craftsDb.SaveChanges();
                }
            }
            else
            {
                User user = new User();
                user.FirstName = profile.FirstName;
                user.CreatedDate = DateTime.Now;
                user.EmailID = profile.EmailID;
                user.IsActive = true;
                user.ProfileType = 2;
                user.IsProfileCreated = true;
                user.PhoneNum = profile.PhoneNum;
                user.Password = profile.Password;
                user.CustomerId = RandomString(8);
                _craftsDb.Users.Add(user);
                _craftsDb.SaveChanges();

                var existUserInfo = _craftsDb.UserInfoes.Where(u => u.UserId == profile.UserID).FirstOrDefault();
                if (existUserInfo == null)
                {
                    UserInfo userInfo = new UserInfo()
                    {
                        UserId = _craftsDb.Users.OrderByDescending(x=>x.UserID).FirstOrDefault().UserID,
                        CurrAssignment = profile.userInfoObj.CurrAssignment,
                        GoogleDriveUrl = profile.userInfoObj.GoogleDriveUrl,
                    };

                    _craftsDb.UserInfoes.Add(userInfo);
                    _craftsDb.SaveChanges();
                }
            }

            bool status = MailSender.MailSend(profile.EmailID, string.Empty, string.Empty);

        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool SaveProfession(ProfessionDto profession)
        {
            if (profession.ProfessionId != 0)
            {
                var prof = _craftsDb.Professions.Where(p => p.ProfessionId == profession.ProfessionId).FirstOrDefault();
                prof.ProfessionName = profession.ProfessionName;
                prof.IsActive = profession.isActive;
                _craftsDb.SaveChanges();
            }
            else
            {
                if (!_craftsDb.Professions.Any(p => p.ProfessionName == profession.ProfessionName))
                {
                    Profession professionObj = new Profession();
                    professionObj.ProfessionName = profession.ProfessionName;
                    professionObj.IsActive = profession.isActive;
                    _craftsDb.Professions.Add(professionObj);
                    _craftsDb.SaveChanges();
                }
            }
            return true;
        }

        public bool DeleteProfession(int professionId)
        {
            if (professionId != 0)
            {
                var prof = _craftsDb.Professions.Where(p => p.ProfessionId == professionId).FirstOrDefault();
                prof.IsActive = false;
                _craftsDb.SaveChanges();
                return true;
            }
            return false;
        }

        //public bool StatuUpdateProfession(int professionId, bool isActive)
        //{
        //    if (professionId != 0)
        //    {
        //        var prof = _craftsDb.Professions.Where(p => p.ProfessionId == professionId).FirstOrDefault();
        //        prof.IsActive = isActive;
        //        _craftsDb.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        public bool SaveLanguage(LanguageDto language)
        {
            if (language.LanguageId != 0)
            {
                var lang = _craftsDb.Languages.Where(p => p.LanguageId == language.LanguageId).FirstOrDefault();
                lang.IsActive = language.isActive;
                lang.LanguageName = language.LanguageName;
                _craftsDb.SaveChanges();
            }
            else
            {
                if (!_craftsDb.Languages.Any(l => l.LanguageName == language.LanguageName))
                {
                    Language languageObj = new Language();
                    languageObj.IsActive = language.isActive;
                    languageObj.LanguageName = language.LanguageName;
                    _craftsDb.Languages.Add(languageObj);
                    _craftsDb.SaveChanges();
                }
            }
            return true;
        }

        public bool DeleteLanguage(int languageId)
        {
            if (languageId != 0)
            {
                var lang = _craftsDb.Languages.Where(p => p.LanguageId == languageId).FirstOrDefault();
                lang.IsActive = false;
                _craftsDb.SaveChanges();
                return true;
            }
            return false;
        }

        //public bool StatuUpdateLanguage(int languageId, bool isActive)
        //{
        //    if (languageId != 0)
        //    {
        //        var lang = _craftsDb.Languages.Where(p => p.LanguageId == languageId).FirstOrDefault();
        //        lang.IsActive = isActive;
        //        _craftsDb.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}
    }
}
