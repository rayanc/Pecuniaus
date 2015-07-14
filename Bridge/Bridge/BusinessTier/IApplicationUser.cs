using Bridge.Models;
using Bridge.Models.Users;
using Bridge.Repository;
using System;
using System.Collections.Generic;

namespace Bridge.BusinessTier
{
    public interface IApplicationUser : IRepository<ApplicationUser, long>
    {
        ApplicationUser GetById(long id);
        ApplicationUser GetByLogin(string loginName);
        IList<GroupPermissionModel> GetPermissions(long userId);
        GroupPermissionModel GetUserGroup(long userId);
    }
}