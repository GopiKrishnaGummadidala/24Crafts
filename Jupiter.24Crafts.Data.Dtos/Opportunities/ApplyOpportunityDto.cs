using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Opportunities
{
   public class ApplyOpportunityDto
    {
        public long UserId { get; set; }
        public int OpportunityId { get; set; }
        public string Description { get; set; }
        public string ImageUrls { get; set; }
    }
}
