using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Account;
using Jupiter._24Crafts.Data.UnitOfWork.Account.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Account
{
    public class AccountUnitOfWork : BaseUnitOfWork, IAccountUnitOfWork
    {
        private AccountRepository _accountRepository;
        public AccountUnitOfWork()
            : base()
        {

        }

        public AccountUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        {
        }

        public AccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(Context);
                }
                return _accountRepository;
            }
        }
    }
}
