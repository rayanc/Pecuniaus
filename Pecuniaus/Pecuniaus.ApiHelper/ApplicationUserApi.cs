using System;
using Pecuniaus.Models.User;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pecuniaus.ApiHelper
{
    public class ApplicationUserApi
    {
        public Task<ApplicationUser> FindByIdAsync(string id)
        {
            string apiData = string.Format("ApplicationUser/GetById?id={0}", id);
            return BaseApiData.GetAPIResultAsync<ApplicationUser>(apiData, () => new ApplicationUser());
        }

        public Task<ApplicationUser> FindByNameAsync(string loginName)
        {
            string apiData = string.Format("ApplicationUser/GetByLogin?loginName={0}", loginName);
            return BaseApiData.GetAPIResultAsync<ApplicationUser>(apiData, () => new ApplicationUser());
            //return new ApplicationUser { UserId = "1", UserName = "admin", Password = "AEGD/0jpn7lVdCuA1wOk3Wuj+vdL4HfMWrjV6gAYLvUFSwW3lhbH2QO8i+fXc1MwMw==" };
        }

        public IList<GroupPermission> GetPermissions(long userId)
        {
            string apiData = string.Format("ApplicationUser/GetPermissions/{0}", userId);
            return BaseApiData.GetAPIResult<IList<GroupPermission>>(apiData, () => new List<GroupPermission>());
        }

        public GroupPermission GetUserGroup(long userId)
        {
            string apiData = string.Format("ApplicationUser/GetUserGroup/{0}", userId);
            return BaseApiData.GetAPIResult<GroupPermission>(apiData, () => new GroupPermission());
        }

        #region IuserPasswordStore
        public virtual string GetPasswordHashAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return user.Password;
        }

        #endregion

        #region IuserSecurityStampStore
        public virtual string GetSecurityStampAsync(ApplicationUser user)
        {
            return user.UserName;
        }

        #endregion
    }
}
