using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bridge.Models
{
    public class MPMerchantBusinessInfoModel
    {
        public MPMerchantBusinessInfoModel()
        {
            EntityTypes = new List<SelectListItem>();
            IndustryTypes = new List<SelectListItem>();
        }
        public string nameOfCompany { get; set; }
        public string nameOfBusiness { get; set; }
        public string businessStatus { get; set; }
        public int entityTypeID { get; set; }
        public string RNC { get; set; }
        public DateTime businessStartDate { get; set; }
        public DateTime businessCCStartDate { get; set; }
        public int industryTypeID { get; set; }
        public string affilliationNumber { get; set; }
        public IEnumerable<SelectListItem> EntityTypes { get; set; }
        public IEnumerable<SelectListItem> IndustryTypes { get; set; }
    }
}