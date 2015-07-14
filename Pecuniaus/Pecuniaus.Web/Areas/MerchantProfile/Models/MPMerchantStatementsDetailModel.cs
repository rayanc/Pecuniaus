using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    //[FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantStatementsValidator))]
    public class MPMerchantStatementsDetailModel
    {
        public string StatementPeriod { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "FromDateReq")]
        public DateTime? StatementsFrom { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateReq")]
        [DateGreaterThan("StatementsFrom", ErrorMessageResourceType = typeof(Resources.MerchantProfile.ValidationMessages), ErrorMessageResourceName = "ToDateVal")]
        public DateTime? StatementsTo { get; set; }
        public int TradeID { get; set; }
        public string FirmName { get; set; }
        public string RNC { get; set; }
        public string TradeName { get; set; }
        public MPMerchantAddressInfoModel AddressDetail { get; set; }
        public string NFC { get; set; }
        //[DataType(DataType.Currency)]
        public string TotalCashRecieved { get; set; }
        //[DataType(DataType.Currency)]
        public string TotalAdjustmentApplied { get; set; }
        //[DataType(DataType.Currency)]
        public string TotalCashApplied { get; set; }
        //[DataType(DataType.Currency)]
        public string TotalPriceApplied { get; set; }
        //[DataType(DataType.Currency)]
        public string OutstandingBalance { get; set; }
        //[DataType(DataType.Currency)]
        public string RightstoRecieve { get; set; }
        //[DataType(DataType.Currency)]
        public string AdminCharges { get; set; }
        public IList<MPMerchantStatementModel> StatementsDetail { get; set; } 
    }
}