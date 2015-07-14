using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.Utilities.Validation;
using FluentValidation;
using Pecuniaus.Validators;

namespace Pecuniaus.MerchantProfile.Models
{
    [FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantAddressInfoValidator))]
    public class MPMerchantAddressInfoModel
    {
        public MPMerchantAddressInfoModel()
        {
            Provinces = new List<SelectListItem>();
        }

        //[Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "AddressReq")]
        [Display(Name = "Address", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string Address { get; set; }
        
        [Display(Name = "City", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string City { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ProvinceReq")]
        [Display(Name = "Province", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public int ProvinceID { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "Telephone1Req")]
        [Display(Name = "Telephone1", ResourceType = typeof(Resources.MerchantProfile.Business))]//, RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "Telephone1Val")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "Telephone2Req")]
        [Display(Name = "Telephone2", ResourceType = typeof(Resources.MerchantProfile.Business))]//, RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "Telephone2Val")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "CellphoneReq")]
        [Display(Name = "Cellphone", ResourceType = typeof(Resources.MerchantProfile.Business))]//, RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "CellphoneVal")]
        [DataType(DataType.PhoneNumber)]
        public string Cell { get; set; }

        //[EmailAddress(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "EmailValReq", ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string Email { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "FaxReq")]
        [Display(Name = "Fax", ResourceType = typeof(Resources.MerchantProfile.Business))]//, RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "FaxVal")]
        public string Fax { get; set; }

        public IEnumerable<SelectListItem> Provinces { get; set; }
    }
}