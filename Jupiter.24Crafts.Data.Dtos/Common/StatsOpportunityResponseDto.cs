using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Common
{
   public class StatsOpportunityResponseDto
    {
        public long UserID { get; set; }
        public string CustId { get; set; }
        public string FirstName { get; set; }
        public int? ProfileType { get; set; }
        public int? NumOfOpportunitiesCreated { get; set; }
        public int? NumOfOpportunitiesApplied { get; set; }
    }
}
