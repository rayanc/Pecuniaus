using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.Repository;
using Bridge.Models;
using Bridge.Models.Users;

namespace Bridge.BusinessTier
{
    public interface IUser : IRepository<UserModel, int>
    {

      
        bool CheckUserExist(UserModel user);
        bool Create(UserModel entity);
        bool Update(UserModel entity);
        bool Validate(UserModel model);
        IList<RecentlyVisitedModel> RecentlyVisited(Int64 id);
        bool InsertRecentlyVisited(Int64 merchantId, Int64 userId);
        IList<UserModel> GetAllUsers(string search);

        bool AddUpdateGroup(GroupModel entity);
        IList<GroupModel> GetAllGroups(string search);
        bool CheckGroupExist(GroupModel group);

        IList<GroupPermissionModel> GetAllRightsforGroupsByGroupID(int moduleID, int groupID);
        bool AddUpdateGroupPermissions(string strGroupRightsXML);
        IList<UsertimeTableModel> GetAllTimeTasksByUserID(int WorkFlowID, long UserID);
        IList<TaskAssignmentModel> GetAllTaskAssignmentsGrouped();
        IList<TaskAssignmentDetailModel> GetAllTaskAssignmentDetails(Int64 UserID);
        IList<TaskAssignmentDetailModel> UpdateAllTaskAssignmentDetails(Int64 UserID, Int64 TaskID);
        IList<GeneralModel> GetAllTaskAssignmentUsers(Int64 UserID);
        IList<UserTimeTableDetailsModel> GetAllTimeTaskDetailsByUserID(int WorkFlowID,long UserID);
        long AddUpdateUserTimeTable(UsertimeTableModel usrTimeTable);
        bool AddUpdateUserTimeTableDetails(string struserTimetableDetails);
        UserDashboardModel GetDashboardDetails(Int64 userId);
    }
}
