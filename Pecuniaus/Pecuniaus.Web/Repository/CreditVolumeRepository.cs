using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Pecuniaus.Web.Models;

namespace Pecuniaus.Web.Repository
{
    public class CreditVolumeRepository
    {
        private static readonly string SessionCreditVolumesModelList = "_Cont_CreditVolumesModelList";

        public static List<CreditVolumesModel> GetAll()
        {
            if (HttpContext.Current.Session[SessionCreditVolumesModelList] != null)
                return (List<CreditVolumesModel>)HttpContext.Current.Session[SessionCreditVolumesModelList];
            return new List<CreditVolumesModel>();
        }
        public static List<CreditVolumesModel> GetSelect(int processorTypeId)
        {
            if (HttpContext.Current.Session[SessionCreditVolumesModelList] != null)
            {
                
                    List<CreditVolumesModel> volumelist = new List<CreditVolumesModel>();
                    volumelist = (List<CreditVolumesModel>)HttpContext.Current.Session[SessionCreditVolumesModelList];
                    List<CreditVolumesModel> volumelistFiltered = volumelist.Where(c => c.processorTypeId == processorTypeId).ToList();
                    List<CreditVolumesModel> volumelistFilteredactive = volumelistFiltered.Where(c => c.isActive == 1).ToList();
                    for (int k = 0; k < volumelistFilteredactive.Count; k++)
                    {
                        volumelistFilteredactive[k].monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(volumelistFiltered[k].month));
                    }
                    //HttpContext.Current.Session[SessionCreditVolumesModelList] = volumelistFilteredactive;
                    return volumelistFilteredactive;
                
            }
            return new List<CreditVolumesModel>();
        }
        public static List<CreditVolumesModel> GetListAfterDelete()
        {
            if (HttpContext.Current.Session[SessionCreditVolumesModelList] != null)
            {
                List<CreditVolumesModel> volumelist = new List<CreditVolumesModel>();
                volumelist = (List<CreditVolumesModel>)HttpContext.Current.Session[SessionCreditVolumesModelList];
                //List<CreditVolumesModel> volumelistFiltered = volumelist.ToList();
                List<CreditVolumesModel> volumelistFiltered = volumelist.Where(c => c.isActive == 1).ToList();
                for (int k = 0; k < volumelistFiltered.Count; k++)
                {
                    volumelistFiltered[k].monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(volumelistFiltered[k].month));
                }
                return volumelistFiltered;

            }
            return new List<CreditVolumesModel>();
        }

        public static void Set(List<CreditVolumesModel> model)
        {
            List<CreditVolumesModel> modellist = new List<CreditVolumesModel>();
            modellist = model;
            for (int k = 0; k < modellist.Count; k++)
            {
                modellist[k].isActive = 1;
            }
            HttpContext.Current.Session[SessionCreditVolumesModelList] = model;
        }

        public static void Add(CreditVolumesModel model)
        {
            var data = GetAll();

            if (model.creditcardActivityId == 0)
            {
                if (data.Count > 0)
                    model.creditcardActivityId = data.Max(a => a.creditcardActivityId) + 1;
                else
                    model.creditcardActivityId = 100000;
            }
            model.isActive = 1;
            data.Add(model);
            HttpContext.Current.Session[SessionCreditVolumesModelList] = data;
        }

        public static void Update(CreditVolumesModel model)
        {
            Delete(model.creditcardActivityId);
            Add(model);
            //var data = GetOwnersFromSession();
            //var oUpdate=  data.Where(a => a.OwnerId == owner.OwnerId).FirstOrDefault();
            //oUpdate.OwnerLastName = owner.OwnerLastName;
            //oUpdate.OwnerFirstName= owner.OwnerFirstName;
            //oUpdate.OwnerFirstName = owner.OwnerFirstName;
            //HttpContext.Current.Session[SessionOwnerList] = data;
        }

        public static void Delete(long ID)
        {
            var data = GetAll();
            var itm = data.Where(a => a.creditcardActivityId == ID).FirstOrDefault();
            if (itm != null)
            {
                data.Remove(itm);
                HttpContext.Current.Session[SessionCreditVolumesModelList] = data;
            }
        }
        public static void Deleted(long ID)
        {
            var data = GetAll();
            var itm = data.Where(a => a.creditcardActivityId == ID).FirstOrDefault();
            if (itm != null)
            {
                itm.isActive = 0;
                HttpContext.Current.Session[SessionCreditVolumesModelList] = data;
                                
            }
        }

        public static CreditVolumesModel GetByID(long ID)
        {
            var data = GetAll();
            return data.Where(a => a.creditcardActivityId == ID).FirstOrDefault();
        }
    }
}