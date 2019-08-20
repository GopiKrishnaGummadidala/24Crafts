namespace Jupiter._24Crafts.Data.UnitOfWork.Base
{
    public interface IBaseUnitOfWork
    {
        void Save();
        void Dispose();
    }
}