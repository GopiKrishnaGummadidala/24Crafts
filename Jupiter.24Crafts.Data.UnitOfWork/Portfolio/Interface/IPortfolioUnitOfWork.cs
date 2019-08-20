using Jupiter._24Crafts.Data.Repositories.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Portfolio.Interface
{
   public interface IPortfolioUnitOfWork
    {
       PortfolioRepository PortfolioRepository { get; }
    }
}
