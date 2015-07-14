using Pecuniaus.Validations;
using System.ComponentModel.DataAnnotations;

namespace Pecuniaus.Models.Contract
{
    public class LandlordInformationModel
    {
        public long LandlordId { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public long MerchantId { get; set; }
    }
}
