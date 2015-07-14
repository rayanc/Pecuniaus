using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.Models.Users;
using System;
using System.Collections.Generic;

namespace Bridge.Repository
{
    public class ApplicationUserRepository : IApplicationUser, IDisposable
    {
        public ApplicationUser GetById(long id)
        {
            var result = new DataAccess.DataAccess().ExecuteReader<ApplicationUser>("avz_usr_getById", new { id = id });
            if (result.Count > 0)
                return result[0];
            return null;
        }

        public ApplicationUser GetByLogin(string loginName)
        {
            var result = new DataAccess.DataAccess().ExecuteReader<ApplicationUser>("avz_usr_getByLogin", new { userid = loginName });
            if (result.Count > 0)
                return result[0];
            return null;
        }


        public IList<GroupPermissionModel> GetPermissions(long userId)
        {
            return new DataAccess.DataAccess().ExecuteReader<GroupPermissionModel>("avz_usr_getPermissions", new { userid = userId });
        }

         public GroupPermissionModel GetUserGroup(long userId)
        {
            var result = new DataAccess.DataAccess().ExecuteReader<GroupPermissionModel>("avz_usr_getUserGroup", new { userid = userId });
             if (result.Count > 0)
                 return result[0];
             return null;
        }
        

        public bool Create(Models.Users.ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Models.Users.ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}