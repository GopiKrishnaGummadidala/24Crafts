using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class SpamDto
    {
        public int SpamId { get; set; }
        public long ByUserId { get; set; }
        public string ByCustId { get; set; }
        public long SpamUserId { get; set; }
        public string SpamCustId { get; set; }
        public List<MessagesDto> Messages { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public bool IsApprovedAdmin { get; set; }
    }
}
