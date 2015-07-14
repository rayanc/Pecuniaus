using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Collection.Models
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
        public string monthEvaualted { get; set; }
        public string timeofreport { get; set; }
        public string passport { get; set; }
        public string occupation { get; set; }
        public string nationality { get; set; }
        public string errors { get; set; }
        public string type { get; set; }
        public string rnc { get; set; }
        public string commercialname { get; set; }
        public string commercialactivity { get; set; }
        public Int32 numberofCreditCards { get; set; }
        public byte[] image { get; set; }
        public string rawdatavalue { get; set; }
        public bool isavailable { get; set; }
        public string IsOwner { get; set; }
        public string IsCompany { get; set; }
                    
    }
    
}