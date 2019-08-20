using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Portfolio;
using Jupiter._24Crafts.Data.UnitOfWork.Base;
using Jupiter._24Crafts.Data.UnitOfWork.Portfolio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Portfolio
{
    public class PortfolioUnitOfWork : BaseUnitOfWork, IPortfolioUnitOfWork
    {
        private PortfolioRepository _portfolioRepository;

        public PortfolioUnitOfWork()
            : base()
        { }

        public PortfolioUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        { }

        public PortfolioRepository PortfolioRepository
        {
            get
            {
                if (_portfolioRepository == null)
                {
                    _portfolioRepository = new PortfolioRepository(Context);
                }
                return _portfolioRepository;
            }
        }
    }
}
