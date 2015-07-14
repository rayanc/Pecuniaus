using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantRiskEvaluationDetailModel
    {
        public MPMerchantRiskEvaluationDetailModel()
        {
            RiskEvaluationDetail = new List<MPMerchantRiskEvaluationModel>();
        }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public List<MPMerchantRiskEvaluationModel> RiskEvaluationDetail { get; set; }
    }
}