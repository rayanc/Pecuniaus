using Microsoft.AspNet.Identity;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models.User;
using Pecuniaus.UICore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pecuniaus.UICore.Identity
{
    public class UserStore : IUserStore<User>, IUserLoginStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>
    {
        ApplicationUserApi _userapi;

        public UserStore()
        {
            _userapi = new ApplicationUserApi();
        }

        public void Dispose()
        {

        }

        #region IUserStore
        public virtual Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public virtual Task<User> FindByIdAsync(string userId)
        {
            var user = ConvertAppUserToUser(_userapi.FindByIdAsync(userId).Result);
            return Task.FromResult(user);
        }

        public virtual Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            var user = ConvertAppUserToUser(_userapi.FindByNameAsync(userName).Result);
            return Task.FromResult(user);
        }

        public virtual Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserLoginStore
        public virtual Task AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public virtual Task<User> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public virtual Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserPasswordStore
        public virtual Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public virtual Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        #endregion

        #region IUserSecurityStampStore
        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        #endregion

        private User ConvertAppUserToUser(ApplicationUser appUser)
        {
            if (appUser != null)
            {
                return new User
                {
                    UserId = appUser.Id.ToString(),
                    UserName = appUser.UserId,
                    SecurityStamp = appUser.SecurityStamp,
                    PasswordHash = appUser.Password,
                    FullName = appUser.UserName,
                };
            }
            return null;
        }

        private ApplicationUser ConvertUserToAppUser(User user)
        {
            if (user != null)
            {
                return new ApplicationUser
                {
                    Id = Convert.ToInt64(user.UserId),
                    UserId = user.UserName,
                    SecurityStamp = user.SecurityStamp,
                    Password = user.PasswordHash,
                    UserName = user.FullName,
                };
            }
            return null;
        }
    }

}
