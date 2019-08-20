using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class MessageDeliveryConfigurationDto
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public int DeliveryHours { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public long CreatedByUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
