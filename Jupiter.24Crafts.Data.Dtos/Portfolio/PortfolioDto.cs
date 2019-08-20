using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Portfolio
{
   public class PortfolioDto 
    {
        public long UserID { get; set; }
        public string CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string PhoneNum { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ProfileType { get; set; }
        public bool? IsContentApproverApproved { get; set; }
        public bool? IsAdminApproved { get; set; }
        public bool? IsPaymentUser { get; set; }
        public bool? IsProfileCreated { get; set; }
        public int? Views { get; set; }
        public int? NumOfOpportunitiesCreated { get; set; }
        public string LanguageIds { get; set; }
        public string ProfessionIds { get; set; }
        public string ProfilePicPathUrl { get; set; }
        public List<string> VideoPathUrls { get; set; }
        public List<string> ImagesPathUrls { get; set; }
        public UserInfoDto userInfoObj { get; set; }
        public List<LanguageInfo> LanguageInfo { get; set; }
        public List<ProfInfo> ProfessionInfo { get; set; }
        public bool IsBusinessEnabled { get; set; }
        public BusinessProfile BusinessObj { get; set; }
        public bool? IsEmailApproved { get; set; }
        public string countryCodes { get; set; }
        public HttpPostedFile ProfilePicturePath { get; set; }
        public bool IsImageVideoContentModified { get; set; }
    }

   public class BusinessProfile {
       public string BusinessCustId { get; set; }
       public string Name { get; set; }
       public string EmailID { get; set; }
       public string PhoneNum { get; set; }
       public string CurrentAssignment { get; set; }
       public string GoogleDriveUrl { get; set; }
       public string ImagePathUrls { get; set; }
       public string Remunaration { get; set; }
       public long BusinessUserId { get; set; }
    }

   public class LanguageInfo 
   {
       public int LanguageId { get; set; }
       public string Name { get; set; }
       public bool isSelected { get; set; }
   }

   public class ProfInfo
   {
       public int ProfessionId { get; set; }
       public string Name { get; set; }
       public bool isSelected { get; set; }
   }
}
