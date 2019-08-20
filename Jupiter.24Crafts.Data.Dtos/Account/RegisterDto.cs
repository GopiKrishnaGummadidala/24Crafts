using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Account
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string PhoneNum { get; set; }
        public int? ProfileType { get; set; }
        public bool? IsPaymentUser { get; set; }
        public int? NumOfOpportunitiesCreated { get; set; }

    }
}
