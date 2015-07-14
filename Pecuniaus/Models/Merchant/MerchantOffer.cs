using Pecuniaus.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecuniaus.Models.Merchant
{

    public class MerchantOffer
    {
        public MerchantOffer()
        {
            Merchant = new MerchantDetails();
            Owner = new OwnerDetails();
            Contract = new ContractDetails();
            Offers = new List<OfferModel>();
        }

        public MerchantDetails Merchant { get; set; }
        public OwnerDetails Owner { get; set; }
        public ContractDetails Contract { get; set; }
        public List<OfferModel> Offers { get; set; }
    }

    public class MerchantDetails
    {
        public MerchantDetails()
        {
            Address = new Address();
        }
        public long MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string EntityType { get; set; }
        public string RentFlag { get; set; }
        public decimal? RentedAmount { get; set; }
        public string IndustryType { get; set; }
        public DateTime? BusinessStartDate { get; set; }
        public decimal? AvgMccv { get; set; }
        public Address Address { get; set; }
    }

    public class OwnerDetails
    {
        public OwnerDetails()
        {
            Address = new Address();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }
    }

    public class ContractDetails
    {
        public long ContractId { get; set; }
        public string Type { get; set; }
    }
}
