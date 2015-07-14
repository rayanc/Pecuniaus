using Pecuniaus.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecuniaus.Models.Contract
{
    public class MerchantInformationOfferModel
    {
        private List<MinPrice> _turnMinPrice;
        public MerchantInformationOfferModel()
        {
            offers = new List<OfferModel>();
            _turnMinPrice = new List<MinPrice>();
        }
        public int Id { get; set; }

        [Display(Name = "Business Name")]
        public string businessName { get; set; }

        [Display(Name = "Address")]
        public Address address { get; set; }

        [Display(Name = "Province")]
        public string province { get; set; }

        /// [RequiredIf("AddManulMccv == true")]
        [Display(Name = "Reason")]
        public string reason { get; set; }

        public bool AddManulMccv { get; set; }

        [Display(Name = "Score")]
        [Required]
        public double score { get; set; }

        [Display(Name = "Gross yearly Sales")]
        [DataType(DataType.Currency)]
        public decimal yearlysales { get; set; }

        [Display(Name = "Average MCCV")]
        [DataType(DataType.Currency)]
        public decimal avgmccv { get; set; }
        public int ismccvupdated { get; set; }
        public List<OfferModel> offers { get; set; }
        public Int64 SelectedOfferId { get; set; }

        [Display(Name = "Final Score")]
        public string finalscore { get; set; }
        public List<MinPrice> TurnMinPrice { get { return _turnMinPrice; } set { _turnMinPrice = value ?? new List<MinPrice>(); } }

        public int MaxTurn { get; set; }
        public int SalesTaken { get; set; }

        public OfferModel SelectedOffer { get { return offers.FirstOrDefault(a => a.IsSelected == true); } }

        public bool IsOffersEmailSent { get; set; }
    }


    public class Address
    {
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }

        [Display(Name = "Address Line 1")]
        public string addressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string addressLine2 { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string zipId { get; set; }
        public Int64 addressId { get; set; }
        public Int64 stateId { get; set; }

        public string CountryName { get; set; }

        public string FullAdderssString
        {
            get
            {
                return addressLine1 + " " + addressLine2 + ", " + city + " " + state + ", " + CountryName;
            }
        }
    }
    public class MinPrice
    {
        public string Score { get; set; }
        public long MaxTime { get; set; }
        public double minPrice { get; set; }

    }
}
