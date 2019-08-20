using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Common
{
   public class LogExceptionRepository: ILogExceptionRepository
    {
       private readonly CratfsDb _craftsDb;

       public LogExceptionRepository(CratfsDb craftsDb)
        {
            this._craftsDb = craftsDb;
        }

       public void AddLogException(Exception e, string url=null)
       {

           var log = new ExceptionLog
           {
               ExceptionMsg = e.Message,
               ExceptionSource = e.StackTrace,
               ExceptionURL = url,
               LogDate = DateTime.UtcNow
           };
           _craftsDb.ExceptionLogs.Add(log);
           _craftsDb.SaveChanges();
       }
    }
}
