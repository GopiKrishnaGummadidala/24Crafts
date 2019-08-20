using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Opportunities
{
   public class OpportunityResponseDto
    {
       public long UserId { get; set; }
       public string Name { get; set; }
       public string CustomerId { get; set; }
       public string Title { get; set; }
       public string Description { get; set; }
    }
}
