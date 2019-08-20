using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class MessagesDto
    {
        public long MessageId { get; set; }
        public string Message { get; set; }
        public long FromUserId { get; set; }
        public string FromCustId { get; set; }
        public string FromUserName { get; set; }
        public long ToUserId { get; set; }
        public Nullable<bool> IsViewed { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
    }
}
