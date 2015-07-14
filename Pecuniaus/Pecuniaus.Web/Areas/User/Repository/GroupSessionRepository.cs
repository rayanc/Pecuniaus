using Pecuniaus.ApiHelper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pecuniaus.User.Models;
using Pecuniaus.Models;

namespace Pecuniaus.User.Repository 
{
    class GroupSessionRepository 
    {  
        private readonly string SessionGroupList = "_Cont_GroupList";
         
        public List<GroupModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionGroupList] != null)
                return (List<GroupModel>)HttpContext.Current.Session[SessionGroupList];
            return new List<GroupModel>();
        }

        public void Set(List<GroupModel> Groups)
        {
            if (Groups != null)
            {
                foreach (var o in Groups)
                {
                    if (o.GroupID == 0)
                        o.GroupID = Groups.Max(a => a.GroupID) + 1;
                }
            }
            HttpContext.Current.Session[SessionGroupList] = Groups;
        }

        public void AddGroup(GroupModel group)
        {
            var data = GetAll();

            var groups = CommonFunctions.GetGroups();
            //GeneralModel p = groups.FirstOrDefault(a => a.KeyId == group.GroupTypeID);

            //if (p != null)
            //{ 
            //    group.GroupTypeName = p.Description;
            //    group.GroupTypeID = (int)p.KeyId;
            //}


            if (group.GroupID == 0)
            {
                if (data.Count > 0)
                    group.GroupID = data.Max(a => a.GroupID) + 1;
                else
                    group.GroupID = 100000;
            }

            data.Add(group);
            HttpContext.Current.Session[SessionGroupList] = data;
        }

        public void Update(GroupModel group)
        {
            DelGroup(group.GroupID);
            AddGroup(group);
        }

        public void DelGroup(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.GroupID == id).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionGroupList] = data;
            }
        }

        public GroupModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.GroupID == id).FirstOrDefault();
        }
    }
}