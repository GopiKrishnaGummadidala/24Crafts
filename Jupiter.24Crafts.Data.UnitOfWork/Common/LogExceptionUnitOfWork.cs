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
   public class LogExceptionUnitOfWork: BaseUnitOfWork, ILogExceptionUnitOfWork
    {
       private LogExceptionRepository _logExceptionRepository;

        public LogExceptionUnitOfWork()
            : base()
        { }

        public LogExceptionUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        { }

        public LogExceptionRepository LogExceptionRepository
        {
            get
            {
                if (_logExceptionRepository == null)
                {
                    _logExceptionRepository = new LogExceptionRepository(Context);
                }
                return _logExceptionRepository;
            }
        }
    }
}
