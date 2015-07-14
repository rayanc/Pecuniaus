using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    [FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantBusinessInfoValidator))]
    public class MPMerchantBusinessInfoModel
    {
        public MPMerchantBusinessInfoModel()
        {
            EntityTypes = new List<SelectListItem>();
            IndustryTypes = new List<SelectListItem>();
        }

        [Display(Name = "NameofCompany", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string nameOfCompany { get; set; }

        [Display(Name = "NameofBusiness", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string nameOfBusiness { get; set; }

        [Display(Name = "BusinessStatus", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string businessStatus { get; set; }

        [Display(Name = "TypeofEntity", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public int entityTypeID { get; set; }

        [Display(Name = "RNCNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string RNC { get; set; }

        [Display(Name = "BusinessStartDate", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        [DataType(DataType.Date)]
        public DateTime? businessStartDate { get; set; }

        [Display(Name = "CreditCardBeginDate", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        [DataType(DataType.Date)]
        public DateTime? businessCCStartDate { get; set; }

        [Display(Name = "TypeofIndustry", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public int industryTypeID { get; set; }

        [Display(Name = "AffiliationNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string affilliationNumber { get; set; }

        public IEnumerable<SelectListItem> EntityTypes { get; set; }

        public IEnumerable<SelectListItem> IndustryTypes { get; set; }
    }
}