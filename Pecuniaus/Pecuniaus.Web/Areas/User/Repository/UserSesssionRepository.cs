using Pecuniaus.ApiHelper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pecuniaus.User.Models;
using Pecuniaus.Models;

namespace Pecuniaus.User.Repository 
{
    class UserSesssionRepository 
    {
        private readonly string SessionUserList = "_Cont_UserList";

        public List<UserModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionUserList] != null)
                return (List<UserModel>)HttpContext.Current.Session[SessionUserList];
            return new List<UserModel>();
        }

        public void Set(List<UserModel> users)
        {
            if (users != null)
            {
                foreach (var o in users)
                {
                    if (o.ID == 0)
                        o.ID = users.Max(a => a.ID) + 1;
                }
            }
            HttpContext.Current.Session[SessionUserList] = users;
        }

        public void AddUser(UserModel user)
        {
            var data = GetAll();

            var states = CommonFunctions.GetStates();
            GeneralModel p = states.FirstOrDefault(a => a.KeyId == user.StateID);

            if (p != null)
                user.State = p.Description;


            if (user.ID == 0)
            {
                if (data.Count > 0)
                    user.ID = data.Max(a => a.ID) + 1;
                else
                    user.ID = 100000;
            }

            data.Add(user);
            HttpContext.Current.Session[SessionUserList] = data;
        }

        public void Update(UserModel user)
        {
            DelUser(user.ID);
            AddUser(user);
        }

        public void DelUser(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.ID == id).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionUserList] = data;
            }
        }

        public UserModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.ID == id).FirstOrDefault();
        }
    }
}