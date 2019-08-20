using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Common.Resources
{
   public static class IsSpamDataContains
    {
       public static bool IsValidData(string str)
       {
           bool res = false;
           if (str.Contains("@gmail") || str.Contains(".com") || str.Contains("@hot") || str.Contains("@yahoo") || str.Contains("skype"))
           {
               res = true;
           }
           var numbers = str.Split().Where(x => x.All(char.IsDigit))
                          .Select(x => long.Parse(x).ToString().Length > 9).ToList();
           res = numbers.Any(_ => _.Equals(true));
           
           return res;
       }
    }
}
