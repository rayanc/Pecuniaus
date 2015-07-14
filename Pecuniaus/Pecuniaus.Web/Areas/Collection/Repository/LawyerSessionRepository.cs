using Pecuniaus.ApiHelper;
using Pecuniaus.Collection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Collection.Repository
{

    class LawyerSessionRepository
    {
        private readonly string SessionLawyerList = "M_LawyerList";

        public List<LawyerModel> GetAll()
        {
           
            if (HttpContext.Current.Session[SessionLawyerList] != null)
                return (List<LawyerModel>)HttpContext.Current.Session[SessionLawyerList];
            return new List<LawyerModel>();
        }

        public void Set(List<LawyerModel> lawyers)
        {
            if (lawyers != null)
                foreach (var p in lawyers)
                {
                    if (p.ID == 0)
                        p.ID = lawyers.Max(a => a.ID) + 1;
                }

            HttpContext.Current.Session[SessionLawyerList] = lawyers;
        }

        public void Add(LawyerModel lawyer)
        {
            var data = GetAll();

            //var lawyers = CommonFunctions.Getlawyers();

            //var p = lawyers.FirstOrDefault(a => a.KeyId == lawyer.lawyerTypeId);
            //if (p != null)
            //    lawyer.firstName = p.firstName;
            if (lawyer.lawyerId > 0)
            {
                lawyer.ID =Convert.ToInt16(lawyer.lawyerId);
            }
            else if (lawyer.ID == 0)
            {
                if (data.Count > 0)
                    lawyer.ID = data.Max(a => a.ID) + 1;
                else
                    lawyer.ID = 100000;
            }

            data.Add(lawyer);
            HttpContext.Current.Session[SessionLawyerList] = data;
        }

        public void Update(LawyerModel lawyer)
        {
            //Delete(lawyer.ID);
            var data = GetAll();
            var itm = data.Where(a => a.ID == lawyer.ID).FirstOrDefault();
            if (itm != null)
            {
                
                data.Remove(itm);
                HttpContext.Current.Session[SessionLawyerList] = data;
            }
            Add(lawyer);
        }

        public void Delete(long id)
        {
            var data = GetAll();
            var itm = data.Where(a => a.ID == id).FirstOrDefault();
            if (itm != null)
            {
                if (itm.lawyerId > 0)                
                    itm.isDeleted = true;                    
                else
                    data.Remove(itm);
                HttpContext.Current.Session[SessionLawyerList] = data;
            }
        }

        public LawyerModel GetByID(long id)
        {
            var data = GetAll();
            return data.Where(a => a.ID == id).FirstOrDefault();
        }

        public void ClearAll()
        {
            if (HttpContext.Current.Session[SessionLawyerList] != null)
            HttpContext.Current.Session[SessionLawyerList] = null;
 
        }
    }
}