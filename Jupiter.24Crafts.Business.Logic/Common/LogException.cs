using Jupiter._24Crafts.Business.Logic.Common.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic
{
   public class LogException : ILogException
    {
       private readonly ILogExceptionUnitOfWork _lUoW;

       public LogException(ILogExceptionUnitOfWork unitOfWork)
        {
            this._lUoW = unitOfWork;
        }
       public void ExceptionLog(Exception e)
       {
           _lUoW.LogExceptionRepository.AddLogException(e);
       }
       
    }
}
