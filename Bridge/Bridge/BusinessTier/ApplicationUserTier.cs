using Bridge.Models;
using Bridge.Models.Users;
using Bridge.Repository;
using System;
using System.Collections.Generic;

namespace Bridge.BusinessTier
{
    public class ApplicationUserTier : IDisposable
    {
        IApplicationUser repository;

        public ApplicationUserTier()
            : this(new ApplicationUserRepository())
        {

        }

        public ApplicationUserTier(IApplicationUser repository)
        {
            this.repository =repository;
        }

        public ApplicationUser GetById(long id)
        {
            return repository.GetById(id);
        }
        
        public ApplicationUser GetByLogin(string login)
        {
            return repository.GetByLogin(login);
        }

        public IList<GroupPermissionModel> GetPermissions(long userId)
        {
            return repository.GetPermissions(userId);
        }

        public GroupPermissionModel GetUserGroup(long userId)
        {
            return repository.GetUserGroup(userId);
        }
        

        public void Dispose()
        {
        }
    }
}