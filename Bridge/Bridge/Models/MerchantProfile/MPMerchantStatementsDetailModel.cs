using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantStatementsDetailModel
    {
        public string StatementPeriod { get; set; }
        public DateTime StatementsFrom { get; set; }
        public DateTime StatementsTo { get; set; }
        public int TradeID { get; set; }
        public string FirmName { get; set; }
        public string RNC { get; set; }
        public string TradeName { get; set; }
        public MPMerchantAddressInfoModel AddressDetail { get; set; }
        public string NFC { get; set; }
        public string TotalCashRecieved { get; set; }
        public string TotalAdjustmentApplied { get; set; }
        public string TotalCashApplied { get; set; }
        public string TotalPriceApplied { get; set; }
        public string OutstandingBalance { get; set; }
        public string RightstoRecieve { get; set; }
        public string AdminCharges { get; set; }
        public List<MPMerchantStatementModel> StatementsDetail { get; set; }
    }
}