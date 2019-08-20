using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Opportunities
{
  public  class OpportunityDto
    {
        public string CustId { get; set; }
        public string Name { get; set; }
        public int OpportunityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Remuneration { get; set; }
        public long CreatedByUserId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public bool IsApplied { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public List<Professions> Professions { get; set; }
        public string ProfessionIds { get; set; }
    }

  public class Professions
  {
      public int Id { get; set; }
      public string ProfessionName { get; set; }
  }
}
