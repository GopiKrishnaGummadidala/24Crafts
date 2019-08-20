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
    public class TokenUnitOfWork : BaseUnitOfWork, ITokenUnitOfWork
    {
        private TokenRepository _tokenRepository;
        public TokenUnitOfWork()
            : base()
        {

        }

        public TokenUnitOfWork(CratfsDb cratfsDb)
            : base(cratfsDb)
        {
        }

        public TokenRepository TokenRepository
        {
            get
            {
                if (_tokenRepository == null)
                {
                    _tokenRepository = new TokenRepository(Context);
                }
                return _tokenRepository;
            }
        }
    }
}
