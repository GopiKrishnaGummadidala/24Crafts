

using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Common;
using Jupiter._24Crafts.Data.UnitOfWork.Base;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
namespace Jupiter._24Crafts.Data.UnitOfWork.Common
{
    public class CityUnitOfWork:BaseUnitOfWork, ICityUnitOfWork
    {
        private CityRepository _cityRepository;

        public CityUnitOfWork() : base()
        {
        }

        public CityUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        { }


        public CityRepository CityRepository
        {
            get
            {
                if (_cityRepository == null)
                {
                    _cityRepository = new CityRepository(Context);
                }
                return _cityRepository;
            }
        }
    }
}
