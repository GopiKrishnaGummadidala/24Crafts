using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Common;
using Jupiter._24Crafts.Data.UnitOfWork.Base;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Common
{
    public class CommonUnitOfWork : BaseUnitOfWork, ICommonUnitOfWork
    {
        private CommonRepository _commonRepository;

        public CommonUnitOfWork() : base()
        {
        }

        public CommonUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        { }


        public CommonRepository CommonRepository
        {
            get
            {
                if (_commonRepository == null)
                {
                    _commonRepository = new CommonRepository(Context);
                }
                return _commonRepository;
            }
        }
    }
}
