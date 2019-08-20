using Jupiter._24Crafts.Data.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Common.Interface
{
    public interface ICityRepository
    {
        List<CityDto> GetCitesByState(long stateId);
    }
}
