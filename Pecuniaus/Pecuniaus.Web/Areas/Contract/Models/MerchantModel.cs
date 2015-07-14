
namespace Pecuniaus.Contract.Models
{
    public class MerchantModel
    {
        public long MerchantID { get; set; }
        public string BusinessName { get; set; }
        public string MerchantName { get; set; }
        public string LegalName { get; set; }
        public string AssignedSales { get; set; }
        public string Rnc { get; set; }

        //Owner info
        public string OwnerName { get; set; }
        public string AddressLine1 { get; set; }
        public string Ssn { get; set; }
        public string Phone1 { get; set; }
        public string AssignedSalesRep { get; set; }
    }
}