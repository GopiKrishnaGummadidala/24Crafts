using Jupiter._24Crafts.Data.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Business.Logic.Account.Interface
{
    public interface IAccountBLL
    {
        ResponseUserDto ValidateUser(string emailId, string password);

        ResponseUserDto ValidateOTPUser(string mobileNum, string otp);

        IEnumerable<MainMenuDto> GetMenuByUserId(long userId);

        UserDto GetUserInfoByUserId(long userId);

        string[] GetRolesByUserId(long userId);

        void Registration(RegisterDto registerDto);

    }
}
