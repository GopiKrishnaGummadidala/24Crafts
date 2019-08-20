using Jupiter._24Crafts.Business.Logic.Account.Interface;
using Jupiter._24Crafts.Data.Dtos.Account;
using Jupiter._24Crafts.Data.UnitOfWork.Account.Interface;
using Jupiter._24Crafts.Data.UnitOfWork.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Account
{
    public class AccountBLL : IAccountBLL
    {
        private readonly IAccountUnitOfWork _aUoW;
        private readonly ILogExceptionUnitOfWork _logUoW;

        public AccountBLL(IAccountUnitOfWork aUoW,ILogExceptionUnitOfWork logUnitOfwork)
        {
            _aUoW = aUoW;
            this._logUoW = logUnitOfwork;
        }
        public ResponseUserDto ValidateUser(string emailId, string password)
        {
            try
            {
                return _aUoW.AccountRepository.ValidateUser(emailId, password);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public ResponseUserDto ValidateOTPUser(string mobileNum, string otp)
        {
            try
            {
                return _aUoW.AccountRepository.ValidateOTPUser(mobileNum, otp);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
        public IEnumerable<MainMenuDto> GetMenuByUserId(long userId)
        {
            try
            {
                return _aUoW.AccountRepository.GetMenuByUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public string[] GetRolesByUserId(long userId)
        {
            try
            {
                return _aUoW.AccountRepository.GetRolesByUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }

        public void Registration(RegisterDto registerDto)
        {
            try
            {
                _aUoW.AccountRepository.Registration(registerDto);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }


        public UserDto GetUserInfoByUserId(long userId)
        {
            try
            {
                return _aUoW.AccountRepository.GetUserInfoByUserId(userId);
            }
            catch (Exception ex)
            {
                _logUoW.LogExceptionRepository.AddLogException(ex);
                throw ex;
            }
        }
    }
}
