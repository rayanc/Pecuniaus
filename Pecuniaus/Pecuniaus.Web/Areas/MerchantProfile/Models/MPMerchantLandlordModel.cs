using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    [FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantLandlordValidator))]
    public class MPMerchantLandlordModel
    {
        public MPMerchantLandlordModel()
        {
            Questions = new MPMerchantLandlordQuestionsModel();
            LandlordAddress = new MPMerchantAddressInfoModel();
        }
        public Int64 UserId { get; set; }
        public Int64 MerchantID { get; set; }
        public string MerchantStatus { get; set; }
        public string ContractStatus { get; set; }

        [Display(Name = "TypeofProperty", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Landlord))]
        public string TypeofPropertyId { get; set; }

        [Display(Name = "LandlordCompanyName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Landlord))]
        public string LandlordCompanyName { get; set; }

        [Display(Name = "LandlordName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Landlord))]
        public string LandlordName { get; set; }


        [Display(Name = "FirstName", ResourceType = typeof(Pecuniaus.Resources.Collection.Collection))]
        public string landlordFirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Pecuniaus.Resources.Collection.Collection))]
        public string landlordLastName { get; set; }



        [Display(Name = "MonthlyRentAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Landlord))]
        [DataType(DataType.Currency)]
        public decimal? MonthlyRentAmount { get; set; }

        public MPMerchantAddressInfoModel LandlordAddress { get; set; }

        public MPMerchantLandlordQuestionsModel Questions { get; set; }

        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
    }
}