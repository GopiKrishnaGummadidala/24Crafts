
using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Common;
using Jupiter._24Crafts.Data.UnitOfWork.Base;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
namespace Jupiter._24Crafts.Data.UnitOfWork.Common
{
    public class StateUnitOfWork:BaseUnitOfWork, IStateUnitOfWork
    {
        private StateRepository _stateRepository;

        public StateUnitOfWork() : base()
        {
        }

        public StateUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        { }


        public StateRepository StateRepository
        {
            get
            {
                if (_stateRepository == null)
                {
                    _stateRepository = new StateRepository(Context);
                }
                return _stateRepository;
            }
        }
    }
}
