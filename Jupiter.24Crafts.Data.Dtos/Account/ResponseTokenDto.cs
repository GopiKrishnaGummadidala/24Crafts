using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Account
{
    public class ResponseTokenDto
    {
        public long UserID { get; set; }
        public string TokenKey { get; set; }
        public DateTime? IssuedTime { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
