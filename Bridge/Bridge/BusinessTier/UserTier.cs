using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Bridge.Models;
using Bridge.Models.Users;
using Bridge.Repository;


namespace Bridge.BusinessTier
{
    public class UserTier : IDisposable
    {
        #region Private Variables
        private IUser usersRepository;
        #endregion

        #region Contructors
        public UserTier() : this(new UserRepository()) { }
        public UserTier(IUser usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        #endregion

        #region Methods


        #region User 
        public IList<UserModel> GetAllUsers(string search)
        {
            return usersRepository.GetAllUsers(search);
        }


        public UserDashboardModel GetDashboardDetails(Int64 UserId)
        {
            return usersRepository.GetDashboardDetails(UserId);
        }
      
        public bool CheckUserExist(UserModel user)
        {
            return usersRepository.CheckUserExist(user);
        }
       
        

        public bool Update(UserModel model)
        {
            return usersRepository.Update(model);
        }
        public bool Create(UserModel model)
        {
            return usersRepository.Create(model);
        }
        public bool Validate(UserModel model)
        {
            return usersRepository.Validate(model);
        } 
        #endregion

        #region Group

        public bool AddUpdateGroup(GroupModel model)
        {
            return usersRepository.AddUpdateGroup(model);
        }

        public IList<GroupModel> GetAllGroups(string search)
        {
            return usersRepository.GetAllGroups(search);
        }
        public bool CheckGroupExist(GroupModel group)
        {
            return usersRepository.CheckGroupExist(group);
        }
        #endregion

        #region Group Permissions
        public IList<GroupPermissionModel> GetAllRightsforGroupsByGroupID(int moduleID,int groupID)
        {
            return usersRepository.GetAllRightsforGroupsByGroupID(moduleID,groupID);
        }
        public bool AddUpdateGroupPermissions(IList<GroupPermissionModel> lstGroupPermissions)
        {
            string rightsXML = GenerateGroupPermissionXML(lstGroupPermissions);
            return usersRepository.AddUpdateGroupPermissions(rightsXML);
        }

        private string GenerateGroupPermissionXML(IList<GroupPermissionModel> lstGroupPermissions)
        {
            var xEle = new XElement("GroupRights",
               from permission in lstGroupPermissions 
               select new XElement("Right",
                              new XElement("pGroupRightID", permission.GroupRightID),
                              new XElement("pGroupID", permission.GroupID),
                              new XElement("pModuleID", permission.ModuleID),
                              new XElement("pPageId", permission.PageId),
                              new XElement("pRead", permission.Read),
                              new XElement("pWrite", permission.Write),
                              new XElement("pEdit", permission.Edit),
                              new XElement("pIsActive", permission.IsActive),
                              new XElement("pInsertUserId", permission.InsertUserId),
                              new XElement("pModifyUserId", permission.ModifyUserId)
                          ));
          
            return xEle.ToString();
        }


        #endregion

        #region UserProfile
        public UsertimeTableModel GetAllTimeTasksWithDetailsByUserID(int workFlowID, long userID)
        { 
            UsertimeTableModel usrTimeTableModel = new UsertimeTableModel();
            IList<UsertimeTableModel> lstusertimeTable= usersRepository.GetAllTimeTasksByUserID(workFlowID, userID);
            if (lstusertimeTable != null && lstusertimeTable.Count == 1)
            {
                usrTimeTableModel = lstusertimeTable[0];
                if(usrTimeTableModel!=null)
                {
                    usrTimeTableModel.lstTimeTableDetails = usersRepository.GetAllTimeTaskDetailsByUserID(workFlowID, userID);
                }
            }
            return usrTimeTableModel;
        }

        public bool AddUpdateUserTimeTableWithDetails(UsertimeTableModel userTimetable)
        { 
            //get last insert/update id from table
            long UserTimeTableID= usersRepository.AddUpdateUserTimeTable(userTimetable);
            string strUserWorkflowTasksXml = GenerateUsertimetableDetailsXML(UserTimeTableID, userTimetable.InsertUserId, userTimetable.ModifyUserId, userTimetable.lstTimeTableDetails);
            return usersRepository.AddUpdateUserTimeTableDetails(strUserWorkflowTasksXml);
        }

        private string GenerateUsertimetableDetailsXML(long UserTimetableID, long InsertUserId,long ModifyUserId, IList<UserTimeTableDetailsModel> lstUserTimetableDetails)
        {
            var xEle = new XElement("TimeTable",
               from task in lstUserTimetableDetails 
               select new XElement("Task",
                              new XElement("UserTimetableDetailsID", task.UserTimetableDetailsID),
                              new XElement("UserTimetableID", UserTimetableID),
                              new XElement("UserID", task.UserID),
                              new XElement("TaskTypeID",task.TaskTypeID), 
                              new XElement("Monday", task.Monday),
                              new XElement("Tuesday",task.Tuesday),
                              new XElement("Wednesday",task.Wednesday),
                              new XElement("Thursday",task.Thursday),
                              new XElement("Friday",task.Friday),
                              new XElement("Saturday",task.Saturday),
                              new XElement("Sunday",task.Sunday),
                              new XElement("InsertUserID", InsertUserId),
                              new XElement("ModifyUserID", ModifyUserId)
                          ));
           
            return xEle.ToString();
        }

        #endregion

        #region Task Assignment

        public IList<TaskAssignmentModel> GetAllTaskAssignmentsGrouped()
        {
            return usersRepository.GetAllTaskAssignmentsGrouped();
        }

        public IList<TaskAssignmentDetailModel> GetAllTaskAssignmentDetails(Int64 UserID)
        {
            return usersRepository.GetAllTaskAssignmentDetails(UserID);
        }

        public IList<TaskAssignmentDetailModel> UpdateAllTaskAssignmentDetails(Int64 UserID, Int64 TaskID)
        {
            return usersRepository.UpdateAllTaskAssignmentDetails(UserID,TaskID);
        }

        public IList<GeneralModel> GetAllTaskAssignmentUsers(Int64 UserID)
        {
            return usersRepository.GetAllTaskAssignmentUsers(UserID);
        }
        #endregion

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #region Recently Visited
        /// <summary>
        /// Retrieve the recently visited merchants by the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<RecentlyVisitedModel> RetrieveRecentSearch(Int64 userId)
        {
            return usersRepository.RecentlyVisited(userId);
        }

        /// <summary>
        /// Insert the recent search
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool RecentVisitedMerchant(Int64 merchantId, Int64 userId)
        {
            return  usersRepository.InsertRecentlyVisited(merchantId, userId);

        }

#endregion
    }
}