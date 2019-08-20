using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
    public class BlockDto
    {
        public int BlockId { get; set; }
        public long BlockedByUserId { get; set; }
        public long BlockedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public bool IsApprovedAdmin { get; set; }
    }
}
