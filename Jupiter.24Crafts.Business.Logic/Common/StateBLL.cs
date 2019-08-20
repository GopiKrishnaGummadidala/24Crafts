using Jupiter._24Crafts.Data.Dtos.Common;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Common.Interface
{
    public class StateBLL : IStateBLL
    {
        private readonly IStateUnitOfWork _mUoW;

        public StateBLL(IStateUnitOfWork unitOfWork)
        {
            this._mUoW = unitOfWork;
        }
        public List<StateDto> GetStatesByCountry(long countryId)
        {
            return _mUoW.StateRepository.GetStatesByCountry(countryId);
        }
    }
}
