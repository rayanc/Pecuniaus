using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    //[FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantContractDetailValidator))]
    public class MPMerchantContractDetailModel
    {
        public MPMerchantContractDetailModel()
        {
            ContractDetail = new List<MPMerchantContractModel>();
        }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "FromDateReq")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateReq")]
        [DateGreaterThan("StartDate", ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateVal")]
        public DateTime? EndDate { get; set; }
        public List<MPMerchantContractModel> ContractDetail { get; set; }
    }
}