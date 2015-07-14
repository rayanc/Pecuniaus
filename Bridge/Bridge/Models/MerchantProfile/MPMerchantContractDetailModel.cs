using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantContractDetailModel
    {
        public MPMerchantContractDetailModel()
        {
            ContractDetail = new List<MPMerchantContractModel>();
        }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public List<MPMerchantContractModel> ContractDetail { get; set; }
    }
}