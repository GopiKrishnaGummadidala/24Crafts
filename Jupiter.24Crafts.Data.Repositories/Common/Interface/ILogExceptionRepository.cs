using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Common.Interface
{
   public interface ILogExceptionRepository
    {
       void AddLogException(Exception e, string url);
    }
}
