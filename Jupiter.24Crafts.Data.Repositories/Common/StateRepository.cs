
using Jupiter._24Crafts.Data.Dtos.Common;
using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Common
{
    public class StateRepository: IStateRepository
    {
        private readonly CratfsDb _craftsDb;

        public StateRepository(CratfsDb craftsDb)
        {
            this._craftsDb = craftsDb;
        }

        public List<StateDto> GetStatesByCountry(long countryId)
        {
            var response = _craftsDb.States.Where(s => s.CountryID == countryId).Select(sd => new StateDto()
            {
                Id=sd.StateID,
                Name=sd.StateName
            }).ToList();
            return response;
        }
    }
}
