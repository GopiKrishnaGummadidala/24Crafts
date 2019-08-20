using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class AdvertisementDto
    {
        public int AdvertisementId { get; set; }
        public Nullable<int> AdvertisementType { get; set; }
        public Nullable<long> UserId { get; set; }
        public string CustId { get; set; }
        public string AdvertisementUrl { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public System.DateTime FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public long IsApprovedBy { get; set; }
        public string Comments { get; set; }
    }
}
