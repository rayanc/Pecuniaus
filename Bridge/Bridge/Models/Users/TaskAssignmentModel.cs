using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class TaskAssignmentModel
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string PQMerchantReview { get; set; }
        public string PQScanDocument { get; set; }
        public string PQDataEntry { get; set; }
        public string PQMonthlyCreditCardVolumes { get; set; }
        public string PQMerchantEvaluation { get; set; }
        public string PQOfferCreation { get; set; }
        public string PQOfferAcceptance { get; set; }
        public string CWScanDocument { get; set; }
        public string CWVerificationCall { get; set; }
        public string CWDataEntry { get; set; }
        public string CWVerificationTask { get; set; }
        public string CWReview { get; set; }
        public string CWContract { get; set; }
        public string CWFunding { get; set; }
        public string CWFinalValidation { get; set; }
        public string RWListofmerchants { get; set; }
        public string RWMerchantEvaluation { get; set; }
        public string RWDocumentVerification { get; set; }
        public string RWOfferCreation { get; set; }
        public string RWRenewalReview { get; set; }
        public string RWContract { get; set; }
        public string RWFunding { get; set; }
        public string RWFinalValidation { get; set; }
    }
}