using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class BankDetailModel
    {
        public int bankDetailId { get; set; }
        public int bankId { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string bankCode { get; set; }
        public string fileName { get; set; }
        public string fileDetails { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
    }

    public class BankNameModel
    {
        public int bankId { get; set; }
        public string bankName { get; set; }       
    }
}