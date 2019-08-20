using Jupiter._24Crafts.Common.Resources;
using Jupiter._24Crafts.Data.Dtos.Account;
using Jupiter._24Crafts.Data.EF;
using Jupiter._24Crafts.Data.Repositories.Account.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Repositories.Account
{
    public class TokenRepository : ITokenRepository
    {
       private readonly CratfsDb cratfsDb;
       public TokenRepository(CratfsDb cratfsDb)
        {
            this.cratfsDb = cratfsDb;
        }

        /// <summary>
        /// Function to generate unique token with expiry against the provided userId.
        /// Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TokenDto GenerateToken(long userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddMinutes(Convert.ToDouble(Settings.AuthTokenExpiryTime));
            Token tokenObj = new Token()
            {
                ExpiredTime = expiredOn,
                IssuedTime = issuedOn,
                UserID = userId,
                IsActive = true,
                TokenKey = token
            };
            cratfsDb.Tokens.Add(tokenObj);
            cratfsDb.SaveChanges();

            return new TokenDto()
            {
                TokenKey = token,
                IssuedTime = issuedOn,
                ExpiredTime = expiredOn,
                IsActive = true
            };
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public bool ValidateToken(string tokenKey)
        {
            var token = cratfsDb.Tokens.Where(t => t.TokenKey == tokenKey && t.ExpiredTime > DateTime.Now).FirstOrDefault();

            if (token != null ? (!(DateTime.Now > token.ExpiredTime) && Convert.ToBoolean(token.IsActive)) : false)
            {
                token.ExpiredTime = Convert.ToDateTime(token.ExpiredTime).AddSeconds(Convert.ToDouble(Settings.AuthTokenExpiryTime));
                cratfsDb.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">true for successful delete</param>
        public bool Kill(string tokenKey)
        {
            var token = cratfsDb.Tokens.Where(t => t.TokenKey == tokenKey).FirstOrDefault();
            if (token != null)
            {
                token.IsActive = false;
                cratfsDb.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true for successful delete</returns>
        public bool DeleteUserId(long userId)
        {
            return true;
            //var token = cratfsDb.Tokens.Where(t => t.TokenKey == tokenKey).FirstOrDefault();
            //if (token != null)
            //{
            //    token.IsActive = false;
            //    cratfsDb.SaveChanges();
            //    return true;
            //}
            //return false;
        }
    }
}
