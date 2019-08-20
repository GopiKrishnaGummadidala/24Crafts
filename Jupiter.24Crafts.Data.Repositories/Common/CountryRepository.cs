
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
    public class CountryRepository: ICountryRepository
    {
        private readonly CratfsDb _craftsDb;

        public CountryRepository(CratfsDb craftsDb)
        {
            this._craftsDb = craftsDb;
        }
        public List<CountryDto> GetCountryList()
        {
            var response = _craftsDb.Countries.Select(c => new CountryDto()
            {
                Id=c.CountryID,
                Name=c.CountryName
            }).ToList();
           
            return response;
        }

    }
}
