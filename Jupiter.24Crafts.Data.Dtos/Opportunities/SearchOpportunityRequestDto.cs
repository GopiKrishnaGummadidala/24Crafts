using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Opportunities
{
   public class SearchOpportunityRequestDto
    {
        public string Title { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string City { get; set; }
        public List<int> ProfessionId { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public DateTime? AvailableEndDate { get; set; }
        public string Remuneration { get; set; }
        public List<int> LanguagesKnown { get; set; }
    }
}
