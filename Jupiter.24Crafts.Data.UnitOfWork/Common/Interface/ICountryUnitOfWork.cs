using Jupiter._24Crafts.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Common.Interface
{
    public interface ICountryUnitOfWork
    {
        CountryRepository CountryRepository { get; }
    }
}
