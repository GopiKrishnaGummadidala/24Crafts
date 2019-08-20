
using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Common;
using Jupiter._24Crafts.Data.UnitOfWork.Base;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
namespace Jupiter._24Crafts.Data.UnitOfWork.Common
{
    public class CountryUnitOfWork:BaseUnitOfWork, ICountryUnitOfWork
    {
        private CountryRepository _countryRepository;

        public CountryUnitOfWork() : base()
        {
        }

        public CountryUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        { }


        public CountryRepository CountryRepository
        {
            get
            {
                if (_countryRepository == null)
                {
                    _countryRepository = new CountryRepository(Context);
                }
                return _countryRepository;
            }
        }

    }
}
