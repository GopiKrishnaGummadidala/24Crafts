using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class NotificationDto
    {
        public long NotificationId { get; set; }
        public string Notification { get; set; }
        public long ToUserId { get; set; }
        public Nullable<bool> IsViewed { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
