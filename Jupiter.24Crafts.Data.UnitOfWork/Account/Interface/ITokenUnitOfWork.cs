using Jupiter._24Crafts.Data.Repositories.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.UnitOfWork.Account.Interface
{
    public interface ITokenUnitOfWork
    {
        TokenRepository TokenRepository { get; }
    }
}
