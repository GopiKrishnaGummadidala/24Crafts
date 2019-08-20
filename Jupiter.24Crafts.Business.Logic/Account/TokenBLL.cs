using Jupiter._24Crafts.Business.Logic.Account.Interface;
using Jupiter._24Crafts.Data.Dtos.Account;
using Jupiter._24Crafts.Data.UnitOfWork.Account;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Account
{
    public class TokenBLL : ITokenBLL
    {
        private readonly TokenUnitOfWork _aUoW;
        private readonly ILogExceptionUnitOfWork _logUoW;

        public TokenBLL(TokenUnitOfWork aUoW, ILogExceptionUnitOfWork logUnitOfwork)
        {
            _aUoW = aUoW;
            this._logUoW = logUnitOfwork;
        }
        public TokenDto GenerateToken(long userId)
        {
            try
            {
                return _aUoW.TokenRepository.GenerateToken(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool ValidateToken(string tokenKey)
        {
            try
            {
                return _aUoW.TokenRepository.ValidateToken(tokenKey);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool Kill(string tokenKey)
        {
            try
            {
                return _aUoW.TokenRepository.Kill(tokenKey);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public bool DeleteUserId(long userId)
        {
            try
            {
                return _aUoW.TokenRepository.DeleteUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
    }
}
