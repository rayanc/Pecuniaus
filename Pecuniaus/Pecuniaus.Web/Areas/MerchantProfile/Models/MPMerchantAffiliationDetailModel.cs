using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantAffiliationDetailModel
    {
        public MPMerchantAffiliationDetailModel()
        {
            ActiveAffiliations = new List<MPMerchantAffiliationModel>();
            InActiveAffiliations = new List<MPMerchantAffiliationModel>();
            RequestTypes = new List<SelectListItem>() { new SelectListItem() { Text = "RNC", Value = "RNC" }, new SelectListItem() { Text = "Owner", Value = "Owner" } };
        }
        public string RequestType { get; set; }
        public IList<SelectListItem> RequestTypes { get; set; }
        public IList<MPMerchantAffiliationModel> ActiveAffiliations { get; set; }
        public IList<MPMerchantAffiliationModel> InActiveAffiliations { get; set; }
    }
}