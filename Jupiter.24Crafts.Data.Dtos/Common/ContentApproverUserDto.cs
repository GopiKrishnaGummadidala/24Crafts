using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class ContentApproverUserDto
    {
        public long UserID { get; set; }
        public string CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string PhoneNum { get; set; }
        public bool? IsAccessNewUsers { get; set; }
        public bool? IsAccessImageVideoUpdate { get; set; }
        public bool? IsAccessReportedSpam { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedByUserID { get; set; }
    }
}
