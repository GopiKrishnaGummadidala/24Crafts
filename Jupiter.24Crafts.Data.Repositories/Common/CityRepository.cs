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
    public class CityRepository:ICityRepository
    {
        private readonly CratfsDb _craftsDb;

        public CityRepository(CratfsDb craftsDb)
        {
            this._craftsDb = craftsDb;
        }

        public List<CityDto> GetCitesByState(long stateId)
        {
            var response = _craftsDb.Cities.Where(c => c.StateID == stateId).Select(cd => new CityDto()
            {
                Id=cd.CityID,
                Name=cd.CityName
            }).ToList();
            return response;
        }
    }
}
