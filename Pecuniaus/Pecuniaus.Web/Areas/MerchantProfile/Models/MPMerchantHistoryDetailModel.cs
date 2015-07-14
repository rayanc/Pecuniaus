using Pecuniaus.Resources.MerchantProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    //[FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantHistoryValidator))]
    public class MPMerchantHistoryDetailModel
    {
        public MPMerchantHistoryDetailModel()
        {
            HistoryDetail = new List<MPMerchantHistoryModel>();
        }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "FromDateReq")]
        public DateTime? HistoryStartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateReq")]
        [DateGreaterThan("HistoryStartDate", ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateVal")]
        public DateTime? HistoryEndDate { get; set; }
        public List<MPMerchantHistoryModel> HistoryDetail { get; set; }
    }
}