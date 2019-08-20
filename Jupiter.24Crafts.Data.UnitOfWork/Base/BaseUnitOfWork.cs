using Jupiter._24Crafts.Data.EF;
using System;

namespace Jupiter._24Crafts.Data.UnitOfWork.Base
{
    public class BaseUnitOfWork : IDisposable, IBaseUnitOfWork
    {
        private bool _disposed;
        protected CratfsDb Context;
        public BaseUnitOfWork()
        {
            Context = new CratfsDb();
        }

        public BaseUnitOfWork(CratfsDb context)
        {
            Context = context;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        #region IDisposable's method implementation.

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}