using System;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Contract.Repository
{
    public class OwnerCorpSessionRepository
    {

        private readonly string SessionOwnerList = "_Corp_OwnerList";

        public List<OwnerCorpModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionOwnerList] != null)
                return (List<OwnerCorpModel>)HttpContext.Current.Session[SessionOwnerList];
            return new List<OwnerCorpModel>();
        }

        public void Set(List<OwnerCorpModel> owners)
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

        public void AddOwner(OwnerCorpModel owner)
        {
            var data = GetAll();

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

        public void Update(OwnerCorpModel owner)
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

        public OwnerCorpModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.Id == id).FirstOrDefault();
        }

    }
}