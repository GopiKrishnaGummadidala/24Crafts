using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class StatsProfileResponseDto
    {
        public long UserID { get; set; }
        public string CustId { get; set; }
        public string FirstName { get; set; }
        public int? ProfileType { get; set; }
        public bool? IsPaymentUser { get; set; }
        public string PhoneNum { get; set; }
        public bool? IsActive { get; set; }
    }
}
