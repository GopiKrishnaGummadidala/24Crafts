using Jupiter._24Crafts.Data.Repositories.Opportunities;
using Jupiter._24Crafts.Data.UnitOfWork.Base;
using Jupiter._24Crafts.Data.UnitOfWork.Opportunities.Interface;
using Jupiter._24Crafts.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Opportunities
{
    public class OpportunitiesUnitOfWork : BaseUnitOfWork, IOpportunitiesUnitOfWork
    {
        private OpportunitiesRepository _opportunitiesRepository;

        public OpportunitiesUnitOfWork()
            : base()
        { }

        public OpportunitiesUnitOfWork(CratfsDb craftsDb)
            : base(craftsDb)
        { }

        public OpportunitiesRepository OpportunitiesRepository
        {
            get
            {
                if (_opportunitiesRepository == null)
                {
                    _opportunitiesRepository = new OpportunitiesRepository(Context);
                }
                return _opportunitiesRepository;
            }
        }

    }
}
