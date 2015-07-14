using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statements
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
        public double TotalCashRecieved { get; set; }
        public double TotalAdjustmentApplied { get; set; }
        public double TotalCashApplied { get; set; }
        public double TotalPriceApplied { get; set; }
        public double OutstandingBalance { get; set; }
        public double RightstoRecieve { get; set; }
        public double AdminCharges { get; set; }
        public List<MPMerchantStatementModel> StatementsDetail { get; set; }
    }
}