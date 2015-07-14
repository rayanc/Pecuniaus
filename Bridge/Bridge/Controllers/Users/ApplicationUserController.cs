using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.Models.Users;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bridge.Controllers.Users
{
    [RoutePrefix("ApplicationUser")]
    public class ApplicationUserController : ApiController
    {
        [HttpGet]
        [Route("GetById")]
        public ApplicationUser GetById(long id)
        {
            using (ApplicationUserTier applicationUser = new ApplicationUserTier())
            {
                return applicationUser.GetById(id);
            }
        }

        [HttpGet]
        [Route("GetByLogin")]
        public ApplicationUser GetByLogin(string loginName)
        {
            using (ApplicationUserTier applicationUser = new ApplicationUserTier())
            {
                return applicationUser.GetByLogin(loginName);
            }
        }

        [HttpGet]
        [Route("GetPermissions/{UserId}")]
        public IList<GroupPermissionModel> GetPermissions(long userId)
        {
            using (ApplicationUserTier applicationUser = new ApplicationUserTier())
            {
                return applicationUser.GetPermissions(userId);
            }
        }

        [HttpGet]
        [Route("GetUserGroup/{UserId}")]
        public GroupPermissionModel GetUserGroup(long userId)
        {
            using (ApplicationUserTier applicationUser = new ApplicationUserTier())
            {
                return applicationUser.GetUserGroup(userId);
            }
        }
    }
}
