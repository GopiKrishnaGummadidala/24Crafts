using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using Jupiter._24Crafts.Business.Logic.Portfolio.Interface;
using Jupiter._24Crafts.Data.Dtos.Portfolio;
using Jupiter._24Crafts.Data.UnitOfWork.Portfolio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Configuration;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Jupiter._24Crafts.Business.Logic.Portfolio
{
    public class PortfolioBLL : IPortfolioBLL
    {
        private readonly IPortfolioUnitOfWork _pUoW;
        private readonly ILogExceptionUnitOfWork _logUoW;

        /// <summary>
        /// Local folder path for saving images
        /// </summary>
        public static string LocalFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ProfileImageDirectory"];
            }
        }

        public PortfolioBLL(IPortfolioUnitOfWork unitOfWork,ILogExceptionUnitOfWork logUnitOfwork)
        {
            this._pUoW = unitOfWork;
            this._logUoW = logUnitOfwork;
        }

        public object searchProfiles(SearchProfileRequestDto searchObj)
        {
            try
            {
                return _pUoW.PortfolioRepository.searchProfiles(searchObj);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public void createPortfolio(PortfolioDto profile)
        {
            try
            {
               _pUoW.PortfolioRepository.createPortfolio(profile);
               // CheckDirectoryExisted(profile.PhoneNum);
               // ImageCompression(string.Empty, @LocalFolderPath + profile.PhoneNum, string.Empty, new Size { Height = 520, Width = 480 });
            }
            catch (Exception e)
            {
                _logUoW.LogExceptionRepository.AddLogException(e);
                throw e;
            }
        }
        
        public void createBusinessProfie(PortfolioDto profile)
        {
            try
            {
                _pUoW.PortfolioRepository.createBusinessProfie(profile);
            }
            catch (Exception e)
            {
                _logUoW.LogExceptionRepository.AddLogException(e);
                throw e;
            }
        }

        /// <summary>
        /// This method is used for Image compression
        /// </summary>
        public void ImageCompression(string ImageUrl, string targetLocation, string extension, Size size)
        {
            try
            {
                ISupportedImageFormat format = null;
                switch (extension)
                {
                    case "png":
                        format = new PngFormat { Quality = 70 };
                        break;
                    case "jpg":
                    case "jpeg":
                        format = new JpegFormat { Quality = 70 };
                        break;
                    default:
                        format = new PngFormat { Quality = 70 };
                        break;
                }

                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(ImageUrl);
                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {
                        using (var outputStream = new MemoryStream())
                        {
                            using (ImageFactory imageFactory = new ImageFactory(true))
                            {
                                imageFactory.Load(stream)
                                           .Resize(size)
                                           .Format(format)
                                           .Save(outputStream);
                                using (var original = Image.FromStream(outputStream))
                                    original.Save(@LocalFolderPath + targetLocation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// This method id used to check folder existed or not, if not will creat folder
        /// </summary>
        /// <param name="folderName"></param>
        public void CheckDirectoryExisted(string folderName)
        {
            try
            {
                // Checking folder is existed or not
                if (!System.IO.Directory.Exists(@LocalFolderPath + folderName))
                {
                    System.IO.Directory.CreateDirectory(@LocalFolderPath + folderName);
                }
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<ProfessionDto> getAllProfessions(bool isAdmin = false)
        {
            try
            {
                return _pUoW.PortfolioRepository.getAllProfessions(isAdmin);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public object getProfileByUserId(long userId)
        {
            try
            {
               return _pUoW.PortfolioRepository.getProfileByUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<LanguageDto> getAllLanguages(bool isAdmin = false)
        {
            try
            {
                return _pUoW.PortfolioRepository.getAllLanguages(isAdmin);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<LanguageDto> getAllLanguagesByUserId(long userId)
        {
            try
            {
                return _pUoW.PortfolioRepository.getAllLanguagesByUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public IEnumerable<ProfessionDto> getAllProfessionsByUserId(long userId)
        {
            try
            {
                return _pUoW.PortfolioRepository.getAllProfessionsByUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool Validateprofile(string mobileNum, int profileType)
        {
            try
            {
                return _pUoW.PortfolioRepository.Validateprofile(mobileNum, profileType);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool ValidateEmail(string mobileNum, string emailid, int profileType, long userId)
        {
            try
            {
                return _pUoW.PortfolioRepository.ValidateEmail(mobileNum,emailid, profileType,userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool SaveProfession(ProfessionDto profession)
        {
            try
            {
                return _pUoW.PortfolioRepository.SaveProfession(profession);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool DeleteProfession(int professionId)
        {
            try
            {
                return _pUoW.PortfolioRepository.DeleteProfession(professionId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool SaveLanguage(LanguageDto language)
        {
            try
            {
                return _pUoW.PortfolioRepository.SaveLanguage(language);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool DeleteLanguage(int languageId)
        {
            try
            {
                return _pUoW.PortfolioRepository.DeleteLanguage(languageId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
    }
}
