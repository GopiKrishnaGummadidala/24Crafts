using Jupiter._24Crafts.Data.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Account.Interface
{
    public interface ITokenRepository
    {
        TokenDto GenerateToken(long userId);

        bool ValidateToken(string tokenKey);

        bool Kill(string tokenKey);

        bool DeleteUserId(long userId);
    }
}
