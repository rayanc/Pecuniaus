using System;

namespace Pecuniaus.Models.Contract
{
    public class CreditReport
    {

        public Int64 CreditReportId { get; set; }
        public Int64 MerchantId { get; set; }
        public Int64 ContractId { get; set; }
        public double creditscore { get; set; }
        public string FirstName { get; set; }
        public string RiskCategory { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string RawData { get; set; }
        public byte[] RawData1 { get; set; }
        public double ProbabilityOfDefault { get; set; }
        public DateTime? MonthEvaualted { get; set; }
        public DateTime? Timeofreport { get; set; }
        public string Passport { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public string Errors { get; set; }
        public string Type { get; set; }
        public string Rnc { get; set; }
        public string Commercialname { get; set; }
        public string Commercialactivity { get; set; }
        public Int32 NumberofCreditCards { get; set; }
        public byte[] Image { get; set; }
        public string Rawdatavalue { get; set; }
        public bool Isavailable { get; set; }

        public string IsOwner { get; set; }
        public string IsCompany { get; set; }

    }
    
}
