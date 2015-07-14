
using System.ComponentModel.DataAnnotations;
namespace Pecuniaus.Models
{
    public class MerchantModel
    {
        public long MerchantID { get; set; }
        public string BusinessName { get; set; }
        public string MerchantName { get; set; }
        public string LegalName { get; set; }
        public string assignedSalesRep { get; set; }
        public string Rnc { get; set; }
        public string SaleRepEmailId { get; set; }

        //Owner info
        public string OwnerName { get; set; }
        public string AddressLine1 { get; set; }
        public string Ssn { get; set; }
        public string Phone1 { get; set; }
        public long ContractId { get; set; }
        public long TaskStatusId { get; set; }
        public string phoneNumber { get; set; }
        public string OwnerAddress { get; set; }

        [DataType(DataType.Currency)]
        public decimal ownedamount { get; set; }
  
        [DataType(DataType.Currency)]
        public decimal paidamount { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal balanceamount { get; set; }

        public long taskTypeId { get; set; }

        public string assignedUser { get; set; }
        public string industrytype { get; set; }
        public string merchantpropertytype { get; set; }

    }
}
