using Pecuniaus.ApiHelper;
using Pecuniaus.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Web.Models
{
    class OwnerSesssionRepository
    {
        private readonly string SessionOwnerList = "M_Cont_OwnerList";

        public List<OwnerModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionOwnerList] != null)
                return (List<OwnerModel>)HttpContext.Current.Session[SessionOwnerList];
            return new List<OwnerModel>();
        }

        public void Set(List<OwnerModel> owners)
        {
            if (owners != null)
            {
                foreach (var o in owners)
                {
                    if (o.Id == 0)
                        o.Id = owners.Max(a => a.Id) + 1;
                }
            }
            HttpContext.Current.Session[SessionOwnerList] = owners;
        }

        public void AddOwner(OwnerModel owner)
        {
            var data = GetAll();

           var states = CommonFunctions.GetStates();
           
            var p = states.FirstOrDefault(a => a.KeyId == owner.StateID);
            if (p != null)
                owner.State = p.Description;


            if (owner.Id == 0)
            {
                if (data.Count > 0)
                    owner.Id = data.Max(a => a.Id) + 1;
                else
                    owner.Id = 100000;
            }

            data.Add(owner);
            HttpContext.Current.Session[SessionOwnerList] = data;
        }

        public void Update(OwnerModel owner)
        {
            DelOwner(owner.Id);
            AddOwner(owner);
        }

        public void DelOwner(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.Id == id).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionOwnerList] = data;
            }
        }

        public OwnerModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.Id == id).FirstOrDefault();
        }
    }
}