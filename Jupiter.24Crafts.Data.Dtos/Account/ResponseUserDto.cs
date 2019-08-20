using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Account
{
    public class ResponseUserDto
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string PhoneNum { get; set; }
        public TokenDto TokenData { get; set; }
       
        public  string[] Roles { get; set; }
        public string CustomerId { get; set; }
        public IEnumerable <MainMenuDto> Menu { get; set; }

    }
}
