using Jupiter._24Crafts.Data.Dtos.Common;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Common.Interface
{
    public class CountryBLL : ICountryBLL
    {
        private readonly ICountryUnitOfWork _mUoW;

        public CountryBLL(ICountryUnitOfWork unitOfWork)
        {
            this._mUoW = unitOfWork;
        }
        public List<CountryDto> GetCountryList()
        {
            return _mUoW.CountryRepository.GetCountryList();
        }
    }
}
