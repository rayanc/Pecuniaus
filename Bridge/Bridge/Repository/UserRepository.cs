using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.Models.Users;


namespace Bridge.Repository
{
    public class UserRepository : IUser, IDisposable
    {
        #region User

        public bool Validate(LoginModel model)
        {
            if (model.email != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Create(UserModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_USER_spAddUser", new
            {
                ID = entity.ID,
                UserId = entity.UserID,
                UserName = entity.UserName,
                Password = entity.Password,
                SecurityStamp = entity.SecurityStamp,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateofBirth = entity.DateofBirth,
                GroupID = entity.GroupID,
                isActive = entity.IsActive,
                isSales = entity.IsSales,
                isCollector = entity.IsCollector,
                SSN = entity.SSN,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                StateID = entity.StateID,
                PostalCode = entity.PostalCode,
                uemail = entity.Email,
                ContactID = entity.ContactID,
                AddressID = entity.AddressID,
                InsertUserID = entity.InsertUserId,
                ModifyUserID = entity.ModifyUserId
            });
        }
        public bool Update(LoginModel entity)
        {
            if (entity.password == entity.confirmpassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public IList<UserModel> GetAllUsers(string searchText)
        {
            return new DataAccess.DataAccess().ExecuteReader<UserModel>("avz_usr_GetAllUsers", new { search = searchText });
        }
        public UserDashboardModel GetDashboardDetails(Int64 userId)
        {
            UserDashboardModel dashboard = new UserDashboardModel();
            DataSet dashboardinfo = new DataAccess.DataAccess().ExecuteDataSet("avz_sp_dashboarddetails", new { logedinUser = userId.ToString() });

            if (dashboardinfo.Tables[0].Rows.Count > 0)
            {
                List<AssignedTasks> tasks = new List<AssignedTasks>();
                for (int i = 0; i < dashboardinfo.Tables[0].Rows.Count; i++)
                {
                    AssignedTasks task = new AssignedTasks();
                    task.WorkFlowName = dashboardinfo.Tables[0].Rows[i]["WorkFlowName"].ToString();
                    task.TaskName = dashboardinfo.Tables[0].Rows[i]["TaskName"].ToString();
                    task.LegalName = dashboardinfo.Tables[0].Rows[i]["LegalName"].ToString();
                    task.StatusName = dashboardinfo.Tables[0].Rows[i]["StatusName"].ToString();
                    if (!string.IsNullOrEmpty(dashboardinfo.Tables[0].Rows[i]["AssignedDate"].ToString()))
                        task.AssignedDate = Convert.ToDateTime(dashboardinfo.Tables[0].Rows[i]["AssignedDate"].ToString());
                    if (!string.IsNullOrEmpty(dashboardinfo.Tables[0].Rows[i]["TaskTypeId"].ToString()))
                        task.TaskTypeId = Convert.ToInt64(dashboardinfo.Tables[0].Rows[i]["TaskTypeId"].ToString());
                    if (!string.IsNullOrEmpty(dashboardinfo.Tables[0].Rows[i]["MerchantId"].ToString()))
                        task.MerchantId = Convert.ToInt64(dashboardinfo.Tables[0].Rows[i]["MerchantId"].ToString());
                    tasks.Add(task);
                }
                dashboard.UserAssignedTasks = tasks;
            }

            if (dashboardinfo.Tables[1].Rows.Count > 0)
            {
                List<CollectionActivity> activities = new List<CollectionActivity>();
                for (int i = 0; i < dashboardinfo.Tables[1].Rows.Count; i++)
                {
                    CollectionActivity activity = new CollectionActivity();
                    activity.StatusName = dashboardinfo.Tables[1].Rows[i]["StatusName"].ToString();
                    activity.LegalName = dashboardinfo.Tables[1].Rows[i]["LegalName"].ToString();
                    if (!string.IsNullOrEmpty(dashboardinfo.Tables[1].Rows[i]["AssignedDate"].ToString()))
                        activity.AssignedDate = Convert.ToDateTime(dashboardinfo.Tables[1].Rows[i]["AssignedDate"].ToString());
                    activities.Add(activity);
                }
                dashboard.CollectionActivities = activities;
            }

            if (dashboardinfo.Tables[2].Rows.Count > 0)
            {
                List<TotalSale> totalloans = new List<TotalSale>();
                for (int i = 0; i < dashboardinfo.Tables[2].Rows.Count; i++)
                {
                    TotalSale totalloan = new TotalSale();
                    totalloan.Description = dashboardinfo.Tables[2].Rows[i]["Description"].ToString();
                    totalloan.LoanedAmount = Convert.ToDouble(dashboardinfo.Tables[2].Rows[i]["LoanedAmount"].ToString());
                    totalloan.PaidAmount = Convert.ToDouble(dashboardinfo.Tables[2].Rows[i]["PaidAmount"].ToString());
                    totalloan.BalanceAmount = Convert.ToDouble(dashboardinfo.Tables[2].Rows[i]["BalanceAmount"].ToString());
                    if (dashboardinfo.Tables[2].Rows[i]["fundingDate"].ToString().Length > 1)
                        totalloan.fundingDate = Convert.ToDateTime(dashboardinfo.Tables[2].Rows[i]["fundingDate"].ToString());
                    totalloans.Add(totalloan);
                }
                dashboard.TotalSales = totalloans;
            }

            if (dashboardinfo.Tables[3].Rows.Count > 0)
            {
                List<NewLead> leads = new List<NewLead>();
                for (int i = 0; i < dashboardinfo.Tables[3].Rows.Count; i++)
                {
                    NewLead lead = new NewLead();
                    lead.WorkFlowName = dashboardinfo.Tables[3].Rows[i]["WorkFlowName"].ToString();
                    lead.TaskName = dashboardinfo.Tables[3].Rows[i]["TaskName"].ToString();
                    lead.LeadSource = dashboardinfo.Tables[3].Rows[i]["LeadSource"].ToString();
                    lead.LegalName = dashboardinfo.Tables[3].Rows[i]["LegalName"].ToString();
                    if (!string.IsNullOrEmpty(dashboardinfo.Tables[3].Rows[i]["InsertDate"].ToString()))
                        lead.AssignedDate = Convert.ToDateTime(dashboardinfo.Tables[3].Rows[i]["InsertDate"].ToString());
                    leads.Add(lead);
                }
                dashboard.NewLeads = leads;
            }


            return dashboard;
        }
        public bool CheckUserExist(UserModel user)
        {
            int result = new DataAccess.DataAccess().ExecuteScalar<int>("avz_usr_CheckusernameExist", new { userName = user.UserName, ID = user.ID });
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IList<RecentlyVisitedModel> RecentlyVisited(Int64 id)
        {
            return new DataAccess.DataAccess().ExecuteReader<RecentlyVisitedModel>("avz_mrc_spretrieveRecentlyVisted", new { userId = id });
        }
        public bool InsertRecentlyVisited(long merchantId, long userId)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_mrc_spinsertRecentlyVisted", new { userId = userId, merchantId = merchantId });
        }
        public bool Validate(UserModel model)
        {
            throw new NotImplementedException();
        }
        public bool Update(UserModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_USER_spUpdateUser", new
            {
                uID = entity.ID,
                uUserId = entity.UserID,
                uUserName = entity.UserName,
                uFirstName = entity.FirstName,
                uLastName = entity.LastName,
                uDateofBirth = String.Format("{0:yyyy-MM-dd}", entity.DateofBirth),
                uGroupID = entity.GroupID,
                uisActive = entity.IsActive,
                uisSales = entity.IsSales,
                uisCollector = entity.IsCollector,
                uSSN = entity.SSN,
                uAddressLine1 = entity.AddressLine1,
                uAddressLine2 = entity.AddressLine2,
                uStateID = entity.StateID,
                uPostalCode = entity.PostalCode,
                uemail = entity.Email,
                uContactID = entity.ContactID,
                uAddressID = entity.AddressID,
                uInsertUserID = entity.InsertUserId,
                uModifyUserID = entity.ModifyUserId,
                uPassword = entity.Password,
            });
        }
        #endregion

        #region Group
        public bool CheckGroupExist(GroupModel group)
        {
            int result = new DataAccess.DataAccess().ExecuteScalar<int>("avz_grp_CheckgroupNameExist", new { groupName = group.GroupName, GroupID = group.GroupID });
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddUpdateGroup(GroupModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_grp_spAddUpdateGroup", new
            {
                GroupID = entity.GroupID,
                GroupName = entity.GroupName,
                GroupTypeID = entity.GroupTypeID,
                IsActive = entity.IsActive,
                InsertUserID = entity.InsertUserId,
                ModifyUserID = entity.ModifyUserId
            });
        }

        public IList<GroupModel> GetAllGroups(string searchText)
        {
            return new DataAccess.DataAccess().ExecuteReader<GroupModel>("avz_grp_GetAllGroups", new { search = searchText });
        }

        #endregion

        #region Group Permissions
        public IList<GroupPermissionModel> GetAllRightsforGroupsByGroupID(int moduleID, int groupID)
        {
            return new DataAccess.DataAccess().ExecuteReader<GroupPermissionModel>("avz_grp_spRetrieveModulesWithRights", new { ModuleID = moduleID, GroupID = groupID });
        }

        public bool AddUpdateGroupPermissions(string strGroupRightsXML)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_grp_spAddUpdateGroupPermissions", new { GroupRightsXml = strGroupRightsXML });
        }
        #endregion

        #region UserProfile

        public IList<UsertimeTableModel> GetAllTimeTasksByUserID(int workFlowID, long userID)
        {
            return new DataAccess.DataAccess().ExecuteReader<UsertimeTableModel>("avz_usr_spRetrieveTimeTableByUserID", new { WorkFlowID = workFlowID, UserID = userID });
        }

        public IList<UserTimeTableDetailsModel> GetAllTimeTaskDetailsByUserID(int workFlowID, long userID)
        {
            return new DataAccess.DataAccess().ExecuteReader<UserTimeTableDetailsModel>("avz_usr_spRetrieveTimeTableDetailsByUserID", new { WorkFlowID = workFlowID, UserID = userID });
        }
        public long AddUpdateUserTimeTable(UsertimeTableModel usrTimeTable)
        {
            return new DataAccess.DataAccess().ExecuteScalar<long>("avz_usr_spAddUpdateUserTimeTable", new
               {

                   UserTimeTableID = usrTimeTable.UserTimeTableID,
                   UserID = usrTimeTable.UserID,
                   WorkFlowID = usrTimeTable.WorkFlowID,
                   IsMondayWork = usrTimeTable.IsMondayWork,
                   IsTuesdayWork = usrTimeTable.IsTuesdayWork,
                   IsWednesdayWork = usrTimeTable.IsWednesdayWork,
                   IsThursdayWork = usrTimeTable.IsThursdayWork,
                   IsFridayWork = usrTimeTable.IsFridayWork,
                   IsSaturdayWork = usrTimeTable.IsSaturdayWork,
                   IsSundayWork = usrTimeTable.IsSundayWork,
                   MondayFromTime = string.IsNullOrWhiteSpace(usrTimeTable.MondayFromTime) ? @"""" : usrTimeTable.MondayFromTime,
                   MondayToTime = string.IsNullOrWhiteSpace(usrTimeTable.MondayToTime) ? "''" : usrTimeTable.MondayToTime,
                   TuesdayFromTime = string.IsNullOrWhiteSpace(usrTimeTable.TuesdayFromTime) ? "''" : usrTimeTable.TuesdayFromTime,
                   TuesdayToTime = string.IsNullOrWhiteSpace(usrTimeTable.TuesdayToTime) ? "''" : usrTimeTable.TuesdayToTime,
                   WednesdayFromTime = string.IsNullOrWhiteSpace(usrTimeTable.WednesdayFromTime) ? "''" : usrTimeTable.WednesdayFromTime,
                   WednesdayToTime = string.IsNullOrWhiteSpace(usrTimeTable.WednesdayToTime) ? "''" : usrTimeTable.WednesdayToTime,
                   ThursdayFromTime = string.IsNullOrWhiteSpace(usrTimeTable.ThursdayFromTime) ? "''" : usrTimeTable.ThursdayFromTime,
                   ThursdayToTime = string.IsNullOrWhiteSpace(usrTimeTable.ThursdayToTime) ? "''" : usrTimeTable.ThursdayToTime,
                   FridayFromTime = string.IsNullOrWhiteSpace(usrTimeTable.FridayFromTime) ? "''" : usrTimeTable.FridayFromTime,
                   FridayToTime = string.IsNullOrWhiteSpace(usrTimeTable.FridayToTime) ? "''" : usrTimeTable.FridayToTime,
                   SaturdayFromTime = string.IsNullOrWhiteSpace(usrTimeTable.SaturdayFromTime) ? "''" : usrTimeTable.SaturdayFromTime,
                   SaturdayToTime = string.IsNullOrWhiteSpace(usrTimeTable.SaturdayToTime) ? "''" : usrTimeTable.SaturdayToTime,
                   SundayFromTime = string.IsNullOrWhiteSpace(usrTimeTable.SundayFromTime) ? "''" : usrTimeTable.SundayFromTime,
                   SundayToTime = string.IsNullOrWhiteSpace(usrTimeTable.SundayToTime) ? "''" : usrTimeTable.SundayToTime,
                   InsertUserID = usrTimeTable.InsertUserId,
                   ModifyUserID = usrTimeTable.ModifyUserId
               });

        }

        public bool AddUpdateUserTimeTableDetails(string struserTimetableDetails)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_usr_spAddUpdateUserTimeTableDetails", new { UserTimeTableDetailsXml = struserTimetableDetails });
        }

        #endregion

        #region
        public IList<TaskAssignmentModel> GetAllTaskAssignmentsGrouped()
        {
            return new DataAccess.DataAccess().ExecuteReader<TaskAssignmentModel>("avz_usr_spRetrieveAllTaskAssignmentsGrouped");
        }
        public IList<TaskAssignmentDetailModel> GetAllTaskAssignmentDetails(Int64 UserID)
        {
            return new DataAccess.DataAccess().ExecuteReader<TaskAssignmentDetailModel>("avz_usr_spRetrieveTaskAssignmentDetails", new { UserID = UserID });
        }
        public IList<TaskAssignmentDetailModel> UpdateAllTaskAssignmentDetails(Int64 UserID, Int64 TaskID)
        {
            return new DataAccess.DataAccess().ExecuteReader<TaskAssignmentDetailModel>("avz_usr_spUpdateTaskAssignmentDetails", new { UserID = UserID, TaskID = TaskID });
        }
        public IList<GeneralModel> GetAllTaskAssignmentUsers(Int64 UserID)
        {
            return new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_usr_spRetrieveTaskAssignmentUsers", new { UserID = UserID });
        }

        #endregion
    }
}