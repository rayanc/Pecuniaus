using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantRiskEvaluationModel
    {
        public Int64 CreditReportID { get; set; }
        public Int64 ContractId { get; set; }
        public DateTime EvaluationDate { get; set; }

        public dynamic ContractNumber { get; set; }

        public dynamic DateofCredit { get; set; }

        public dynamic ScoresGenerated { get; set; }
    }
}