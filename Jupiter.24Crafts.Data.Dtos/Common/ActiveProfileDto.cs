using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
    public class ActiveProfileDto
    {
        public long UserID { get; set; }
        public string CustId { get; set; }
        public string FirstName { get; set; }
        public string EmailID { get; set; }
        public string PrimaryProfession { get; set; }
        public string PhoneNum { get; set; }
        public int? ProfileType { get; set; }
        public double ActiveDays { get; set; }
    }
}
