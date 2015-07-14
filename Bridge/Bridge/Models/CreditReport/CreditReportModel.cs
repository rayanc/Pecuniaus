using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class CreditReportModel
    {
        public Int64 creditReportId { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public double creditscore { get; set; }
        public string firstName { get; set; }
        public string riskCategory { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string rawData { get; set; }
        public byte[] rawData1 { get; set; }
        public double probabilityOfDefault { get; set; }
        public DateTime? monthEvaualted { get; set; }
        public DateTime? timeofreport { get; set; }
        public string passport { get; set; }
        public string occupation { get; set; }
        public string nationality { get; set; }
        public string errors { get; set; }
        public string type { get; set; }
        public string rnc { get; set; }
        public string commercialname { get; set; }
        public string commercialactivity { get; set; }
        public Int32 numberofCreditCards { get; set; }
        public Int32 numberofLoans { get; set; }
        public Int32 numberofOthers { get; set; }
        public Int32 isavailable { get; set; }
        public byte[] image { get; set; }
        public List<CreditAnalysis> creditAnalysis { get; set; }
       
    }
    public class CreditAnalysis
    {
        public Int64 reportId { get; set; }
        public Int64 numberofloans { get; set; }
        public string loancredit { get; set; }
        public double loanowedamount { get; set; }
        public string consolidateMonth { get; set; }
        public string firstcreditdate { get; set; }
        public double approvedcredit { get; set; }
        
        public string loanamountinlegal { get; set; }
        public string loanlateamount { get; set; }
        public double loanmonthlypayment { get; set; }

        public string currency { get; set; }
      
        public string typeofActivity { get; set; }
    }
    public class RetrieveMerchantInformation
    {
        public string ownerId { get; set; }
        public string rnc { get; set; }
        public double mccv { get; set; }
        public string score { get; set; }
        public string finalletter { get; set; }
        public string roundedscore { get; set; }
        public string rawData { get; set; }
    }
}