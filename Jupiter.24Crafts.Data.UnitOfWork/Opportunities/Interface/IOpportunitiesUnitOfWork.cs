using Jupiter._24Crafts.Data.Repositories.Opportunities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Opportunities.Interface
{
   public interface IOpportunitiesUnitOfWork 
    {
       OpportunitiesRepository OpportunitiesRepository { get; }
    }
}
