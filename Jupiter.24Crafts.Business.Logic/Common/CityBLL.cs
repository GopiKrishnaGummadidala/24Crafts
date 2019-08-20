using Jupiter._24Crafts.Business.Logic.Common.Interface;
using Jupiter._24Crafts.Data.Dtos.Common;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Common.Interface
{
    public class CityBLL : ICityBLL
    {
        private readonly ICityUnitOfWork _mUoW;

        public CityBLL(ICityUnitOfWork unitOfWork)
        {
            this._mUoW = unitOfWork;
        }
        public List<CityDto> GetCitesByState(long stateId)
        {
            return _mUoW.CityRepository.GetCitesByState(stateId);
        }
    }
}
