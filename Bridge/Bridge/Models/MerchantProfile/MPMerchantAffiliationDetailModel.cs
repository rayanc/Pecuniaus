using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantAffiliationDetailModel
    {
        public MPMerchantAffiliationDetailModel()
        {
            ActiveAffiliations = new List<MPMerchantAffiliationModel>();
            InActiveAffiliations = new List<MPMerchantAffiliationModel>();
        }

        public IList<MPMerchantAffiliationModel> ActiveAffiliations { get; set; }
        public IList<MPMerchantAffiliationModel> InActiveAffiliations { get; set; }
    }
}