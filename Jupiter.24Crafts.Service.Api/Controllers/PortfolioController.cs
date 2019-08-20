using Jupiter._24Crafts.Business.Logic.Common.Interface;
using Jupiter._24Crafts.Business.Logic.Portfolio.Interface;
using Jupiter._24Crafts.Data.Dtos.Portfolio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Jupiter._24Crafts.Service.Api.Controllers
{
    [RoutePrefix("Portfolio")]
    public class PortfolioController : ApiController
    {
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
        public static string DeleteLocalFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["DeleteImageDirectory"];
            }
        }


        /// <summary>
        /// Local folder path for saving images
        /// </summary>
        public static string WaterMarkImagePath
        {
            get
            {
                return ConfigurationManager.AppSettings["WaterMarkImagePath"];
            }
        }

        /// <summary>
        /// Exe path
        /// </summary>
        public static string ExePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ExePath"];
            }
        }

        private readonly IPortfolioBLL _portfolioBll;

        private readonly ILogException _logBll;

        public PortfolioController()
        { }

        public PortfolioController(IPortfolioBLL bll, ILogException lBll)
        {
            this._portfolioBll = bll;
            this._logBll = lBll;
        }

        [Route("searchProfiles")]
        [HttpGet]
        public object searchProfiles([FromUri]SearchProfileRequestDto searchObj=null)
        {
            return _portfolioBll.searchProfiles(searchObj);
        }

        [Route("getProfileByUserId")]
        [HttpGet]
        public object getProfileByUserId(long userId)
        {
            return _portfolioBll.getProfileByUserId(userId);
        }

        [Route("createPortfolio")]
        [HttpPost]
        public IHttpActionResult createPortfolio(PortfolioDto profile)
        {
            try
            {
            _portfolioBll.createPortfolio(profile);            
                return Ok();
        }
            catch (Exception ex)
            {
                return InternalServerError();
            }      
        }
        [Route("uploadFiles")]
        [HttpPost]
        public IHttpActionResult UploadFiles()
        {
            try
            {
                var isContentModified = Convert.ToBoolean(HttpContext.Current.Request.Form["isContentModified"]);
                var customerId = Convert.ToString(HttpContext.Current.Request.Form["customerId"]);
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0 && !string.IsNullOrEmpty(customerId))
                {
                    // var docfiles = new List<string>();
                    HttpPostedFile postedFile;
                    string ProfilePicPath = @LocalFolderPath + "/" + customerId + (isContentModified ? "Modified/ProfilePics/" : "/ProfilePics/");
                    string PicsPath = @LocalFolderPath + "/" + customerId + (isContentModified ? "Modified/Pics/" : "/Pics/");
                    string VideosPath = @LocalFolderPath + "/" + customerId + (isContentModified ? "Modified/Videos/" : "/Videos/");

                    foreach (string file in httpRequest.Files)
                    {
                        //Profile pic
                        if (file == "ProfilePicturePath")
                        {
                            postedFile = httpRequest.Files[file];
                            if (postedFile.ContentType == "image/jpeg" || postedFile.ContentType == "image/png" || postedFile.ContentType == "image/gif" || postedFile.ContentType == "image/tiff")
                            {
                                var filePath = string.Format("{0}{1}", this.CheckDirectoryExisted(ProfilePicPath), postedFile.FileName);
                                postedFile.SaveAs(filePath);
                                string outFilePath = this.SaveFilePath(ProfilePicPath, postedFile.ContentType);
                                AddWaterMarkToImages(filePath, outFilePath);
                            }
                        }
                        else
                        {
                            postedFile = httpRequest.Files[file];
                            //Multiple images
                            if (postedFile.ContentType == "image/jpeg" || postedFile.ContentType == "image/png" || postedFile.ContentType == "image/gif" || postedFile.ContentType == "image/tiff")
                            {
                                var filePath = string.Format("{0}{1}", this.CheckDirectoryExisted(PicsPath), postedFile.FileName);
                                postedFile.SaveAs(filePath);
                                string outFilePath = this.SaveFilePath(PicsPath, postedFile.ContentType);
                                AddWaterMarkToImages(filePath, outFilePath);
                            }
                            //Multiple videos
                            else if (postedFile.ContentType == "video/mp4")
                            {
                                var filePath = string.Format("{0}{1}", this.CheckDirectoryExisted(VideosPath), postedFile.FileName);
                                postedFile.SaveAs(filePath);
                                string outFilePath = this.SaveFilePath(VideosPath, postedFile.ContentType);
                                AddWaterMarkToVideos(filePath, outFilePath);
                            }
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            // Task.Run(() => { uploadFilesAsync(customerId, isContentModified, httpRequest); });
        }

        private void uploadFilesAsync(string customerId, bool isContentModified = false)
        {

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0 && !string.IsNullOrEmpty(customerId))
            {
                // var docfiles = new List<string>();
                HttpPostedFile postedFile;
                string ProfilePicPath = @LocalFolderPath + "/" + customerId + (isContentModified ? "Modified/ProfilePics/" : "/ProfilePics/");
                string PicsPath = @LocalFolderPath + "/" + customerId + (isContentModified ? "Modified/Pics/" : "/Pics/");
                string VideosPath = @LocalFolderPath + "/" + customerId + (isContentModified ? "Modified/Videos/" : "/Videos/");

                foreach (string file in httpRequest.Files)
                {
                    //Profile pic
                    if (file == "ProfilePicturePath")
                    {
                        postedFile = httpRequest.Files[file];
                        if (postedFile.ContentType == "image/jpeg" || postedFile.ContentType == "image/png" || postedFile.ContentType == "image/gif" || postedFile.ContentType == "image/tiff")
                        {
                            var filePath = string.Format("{0}{1}", this.CheckDirectoryExisted(ProfilePicPath), postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            string outFilePath = this.SaveFilePath(ProfilePicPath, postedFile.ContentType);
                            AddWaterMarkToImages(filePath, outFilePath);
                        }
                    }
                    else
                    {
                        postedFile = httpRequest.Files[file];
                        //Multiple images
                        if (postedFile.ContentType == "image/jpeg" || postedFile.ContentType == "image/png" || postedFile.ContentType == "image/gif" || postedFile.ContentType == "image/tiff")
                        {
                            var filePath = string.Format("{0}{1}", this.CheckDirectoryExisted(PicsPath), postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            string outFilePath = this.SaveFilePath(PicsPath, postedFile.ContentType);
                            AddWaterMarkToImages(filePath, outFilePath);
                        }
                        //Multiple videos
                        //else if (postedFile.ContentType == "video/mp4")
                        //{
                        //    var filePath = string.Format("{0}{1}", this.CheckDirectoryExisted(VideosPath), postedFile.FileName);
                        //    postedFile.SaveAs(filePath);
                        //    string outFilePath = this.SaveFilePath(VideosPath, postedFile.ContentType);
                        //    AddWaterMarkToVideos(filePath, outFilePath);
                        //}
                    }
                }
            }
        }

        [Route("deleteFiles")]
        [HttpGet]
        public IHttpActionResult DeleteFiles(string filePaths)
            {
            var physicalPath = DeleteLocalFolderPath + filePaths;
            if ((File.Exists(physicalPath)))
                {
                File.Delete(physicalPath);
                return Ok();
                }
            else {
                return NotFound();
            }
        }

        private void AddWaterMarkToImages(string filePath, string outFilePath)
        {
            using (Image image = Image.FromFile(@filePath))
            using (Image watermarkImage = Image.FromFile(@WaterMarkImagePath))
            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
            {
                int x = (image.Width / 10 - watermarkImage.Width / 2);
                int y = (image.Height / 10 - watermarkImage.Height / 2);
                watermarkBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));
                image.Save(@outFilePath);
            }

            //FileStream source = new FileStream(filePath, FileMode.Open);
            //Stream output = new MemoryStream();
            //Image img = Image.FromStream(source);

            //// choose font for text
            //Font font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel);

            ////choose color and transparency
            //Color color = Color.FromArgb(100, 255, 0, 0);

            ////location of the watermark text in the parent image
            //Point pt = new Point(10, 5);
            //SolidBrush brush = new SolidBrush(color);

            ////draw text on image
            //Graphics graphics = Graphics.FromImage(img);
            //graphics.DrawString("My 24 Crafts", font, brush, pt);
            //graphics.Dispose();

            ////update image memorystream
            //img.Save(output, ImageFormat.Png);
            //Image imgFinal = Image.FromStream(output);

            ////write modified image to file
            //Bitmap bmp = new System.Drawing.Bitmap(img.Width, img.Height, img.PixelFormat);
            //Graphics graphics2 = Graphics.FromImage(bmp);
            //graphics2.DrawImage(imgFinal, new Point(0, 0));
            //bmp.Save(outFilePath, ImageFormat.Png);

            //imgFinal.Dispose();
            //img.Dispose();

            //if ((File.Exists(filePath)))
            //{
            //    File.Delete(filePath);
            //}
        }

        private string SaveFilePath(string filePath, string extension)
        {
            var directory = new DirectoryInfo(filePath);
            return filePath + "/24Crafts_" + directory.GetFiles().Count() + extension.Replace("image/", ".").Replace("video/", ".");
        }

        private void AddWaterMarkToVideos(string inputFilePath, string outputFilePath)
        {
            using (var proc = new Process())
            {
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = ExePath;
                proc.StartInfo.WorkingDirectory = new FileInfo(WaterMarkImagePath).Directory.FullName;
                proc.StartInfo.Arguments = string.Format(
                  "-y -i \"{0}\" -vf \"movie=watermark.png [watermark]; [in][watermark] overlay=10:10 [out]\" {1}",
                  inputFilePath, outputFilePath);
                proc.StartInfo.LoadUserProfile = false;
                proc.Start();
                proc.WaitForExit();
            }
            //if ((File.Exists(inputFilePath)))
            //{
            //    File.Delete(inputFilePath);
            //}
        }

        [Route("createBusinessProfie")]
        [HttpPost]
        public void createBusinessProfie(PortfolioDto profile)
        {
            _portfolioBll.createBusinessProfie(profile);
        }

        [Route("getAllProfessions")]
        [HttpGet]
        public IEnumerable<ProfessionDto> getAllProfessions(bool isAdmin = false)
        {
            return _portfolioBll.getAllProfessions(isAdmin);
        }

        [Route("getAllProfessionsByUserId")]
        [HttpGet]
        public IEnumerable<ProfessionDto> getAllProfessionsByUserId(long userId)
        {
            return _portfolioBll.getAllProfessionsByUserId(userId);
        }

        [Route("getAllLanguages")]
        [HttpGet]
        public IEnumerable<LanguageDto> getAllLanguages(bool isAdmin=false)
        {
            return _portfolioBll.getAllLanguages(isAdmin);
        }

        [Route("getAllLanguagesByUserId")]
        [HttpGet]
        public IEnumerable<LanguageDto> getAllLanguagesByUserId(long userId)
        {
            return _portfolioBll.getAllLanguagesByUserId(userId);
        }

        [Route("ValidateProfile")]
        [HttpGet]
        public bool Validateprofile(string mobileNum, int profileType)
        {
            return _portfolioBll.Validateprofile(mobileNum, profileType);
        }

        [Route("ValidateEmail")]
        [HttpGet]
        public bool ValidateEmail(string mobileNum,string emailid, int profileType, long userId = 0)
        {
            return _portfolioBll.ValidateEmail(mobileNum,emailid, profileType, userId);
        }

        [Route("SaveProfession")]
        [HttpPost]
        public bool SaveProfession(ProfessionDto profession)
        {
            return _portfolioBll.SaveProfession(profession);
        }

        [Route("DeleteProfession")]
        [HttpGet]
        public bool DeleteProfession(int professionId)
        {
            return _portfolioBll.DeleteProfession(professionId);
        }

        [Route("SaveLanguage")]
        [HttpPost]
        public bool SaveLanguage(LanguageDto language)
        {
            return _portfolioBll.SaveLanguage(language);
        }

        [Route("DeleteLanguage")]
        [HttpGet]
        public bool DeleteLanguage(int languageId)
        {
            return _portfolioBll.DeleteLanguage(languageId);
        }

        /// <summary>
        /// This method id used to check folder existed or not, if not will creat folder
        /// </summary>
        /// <param name="folderName"></param>
        private string CheckDirectoryExisted(string folderName)
        {
            try
            {
                // Checking folder is existed or not
                if (!System.IO.Directory.Exists(folderName))
                {
                    System.IO.Directory.CreateDirectory(folderName);
                }

                return folderName;
            }
            catch (Exception ex)
            {
                _logBll.ExceptionLog(ex);
                throw ex;
            }
        }
    }
}
