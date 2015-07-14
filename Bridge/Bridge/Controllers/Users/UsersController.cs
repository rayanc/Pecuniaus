using Bridge.BusinessTier;
using Bridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models.Users;

namespace Bridge.Controllers.Users
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        #region User
        [HttpGet]
       

        [HttpPost]
        [Route("CheckUserExist")]
        public HttpResponseMessage CheckUserExist(UserModel user)
        {
            bool response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.CheckUserExist(user);
                if (response)
                { return this.Request.CreateResponse(HttpStatusCode.OK, response); }
                else
                    return this.Request.CreateResponse(HttpStatusCode.Accepted, response);

            }
        }

        [HttpGet]
        [Route("GetAllUsers/{search?}")]
        public HttpResponseMessage GetAllUsers(string search="")
        {
            search = search.Replace("null", "");
            IList<UserModel> response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetAllUsers(search);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPost]
        [Route("updateuser")]
        public HttpResponseMessage UpdateUser(UserModel usermodel)
        {

            using (UserTier usertier = new UserTier())
            {
                bool result = usertier.Update(usermodel);
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        [Route("validateuser")]
        public HttpResponseMessage Validate(UserModel usermodel)
        {
            using (UserTier usertier = new UserTier())
            {
                bool result = usertier.Validate(usermodel);
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        [HttpPost]
        [Route("createuser")]
        public HttpResponseMessage Create(UserModel usermodel)
        {
            using (UserTier usertier = new UserTier())
            {
                bool response = usertier.Create(usermodel);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        } 
        #endregion

        #region Group
        [HttpPost]
        [Route("CheckGroupExist")]
        public HttpResponseMessage CheckGroupExist(GroupModel group)
        {
            bool response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.CheckGroupExist(group);
                if (response)
                { return this.Request.CreateResponse(HttpStatusCode.OK, response); }
                else
                    return this.Request.CreateResponse(HttpStatusCode.Accepted, response);

            }
        }

        [HttpPost]
        [Route("addUpdateGroup")]
        public HttpResponseMessage UpdateUser(GroupModel groupmodel)
        {

            using (UserTier usertier = new UserTier())
            {
                bool result = usertier.AddUpdateGroup(groupmodel);
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        [HttpGet]
        [Route("GetAllGroups/{search}")]
        public HttpResponseMessage GetAllGroups(string search)
        {
            search = search.Replace("null", "");
            IList<GroupModel> response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetAllGroups(search);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        #endregion

        #region Group Rights Permissions
        [HttpGet]
        [Route("GetAllRightsforGroups/{moduleID}/{groupID}")]
        public HttpResponseMessage GetAllRightsforGroups(int moduleID,int groupID)
        {
           
            IList<GroupPermissionModel> response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetAllRightsforGroupsByGroupID(moduleID,groupID);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut] 
        [Route("addUpdateGroupPermissions")]
        public HttpResponseMessage AddUpdateGroupPermissions(List<GroupPermissionModel> model)
        {
            using (UserTier usr = new UserTier())
            {
                if (usr.AddUpdateGroupPermissions(model))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
        }
           
        #endregion

        #region User Profile
        [HttpGet]
        [Route("GetAllTimeTasksWithDetails/{workflowID}/{userID}")] 
        public HttpResponseMessage GetAllTimeTasksWithDetails(int workflowID, long userID)
        {
            UsertimeTableModel response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetAllTimeTasksWithDetailsByUserID(workflowID, userID);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("addUpdateUserTimeTableWithDetails")] 
        public HttpResponseMessage AddUpdateUserTimeTableWithDetails(UsertimeTableModel model)
        {
            using (UserTier usr = new UserTier())
            {
                if (usr.AddUpdateUserTimeTableWithDetails(model))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
        }

        #endregion

        #region Task Assignment
        //
        [HttpGet]
        [Route("gettaskassignments")]
        public HttpResponseMessage GetAllTaskAssignmentsGrouped()
        {
            IList<TaskAssignmentModel> response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetAllTaskAssignmentsGrouped();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        //
        [HttpGet]
        [Route("{UserID}/gettaskassignmentdetails")]
        public HttpResponseMessage GetAllTaskAssignmentDetails(Int64 UserID)
        {
            IList<TaskAssignmentDetailModel> response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetAllTaskAssignmentDetails(UserID);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        //
        [HttpGet]
        [Route("{UserID}/update")]
        public HttpResponseMessage UpdateAllTaskAssignmentDetails(IList<TaskAssignmentDetailModel> response)
        {
            using (UserTier usertier = new UserTier())
            {
                foreach (TaskAssignmentDetailModel obj in response)
                {
                    response = usertier.UpdateAllTaskAssignmentDetails(obj.UserID, obj.TaskID);
                }
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        //
        [HttpGet]
        [Route("{UserID}/gettaskassignmentusers")]
        public HttpResponseMessage GetAllTaskAssignmentUsers(Int64 UserID)
        {
            IList<GeneralModel> response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetAllTaskAssignmentUsers(UserID);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        #endregion

        #region RecentActivites
        [HttpGet]
        [Route("recent/merchants/{userId}")]
        public HttpResponseMessage Recent(Int64 userId)
        {
            using (UserTier usertier = new UserTier())
            {
                IList<RecentlyVisitedModel> recent = usertier.RetrieveRecentSearch(userId);
                return this.Request.CreateResponse(HttpStatusCode.OK, recent);
            }
            
        }
        [HttpPost]
        [Route("recent/save/{userId}/{merchantId}")]
        public HttpResponseMessage InsertRecent(Int64 userId,Int64 merchantId)
        {
            using (UserTier usertier = new UserTier())
            {
                if (usertier.RecentVisitedMerchant(merchantId, userId))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

            }

        }
        #endregion

        #region "Dashboard"
        [HttpGet]
        [Route("GetDashboardDetails")]
        public HttpResponseMessage GetDashboardDetails(Int64 UserId)
        {
            UserDashboardModel response;
            using (UserTier usertier = new UserTier())
            {
                response = usertier.GetDashboardDetails(UserId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion
    }
}
