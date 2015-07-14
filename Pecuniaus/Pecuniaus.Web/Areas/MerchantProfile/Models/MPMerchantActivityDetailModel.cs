using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    //[FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantActivityValidator))]
    public class MPMerchantActivityDetailModel
    {
        public MPMerchantActivityDetailModel()
        {
            ActivityDetail = new List<MPMerchantActivityModel>();
            Processors = new List<SelectListItem>();
        }

        public Int64 MerchantID { get; set; }
        [Display(Name = "Processor", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Activities))]
        public int ProcessorTypeId { get; set; }
        [Display(Name = "ProcessorNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Activities))]
        public string ProcessorNumber { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "FromDateReq")]
        public DateTime? ActivityFrom { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateReq")]
        [DateGreaterThan("ActivityFrom", ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateVal")]
        public DateTime? ActivityTo { get; set; }
        public List<MPMerchantActivityModel> ActivityDetail { get; set; }
        public IEnumerable<SelectListItem> Processors { get; set; }
    }
}