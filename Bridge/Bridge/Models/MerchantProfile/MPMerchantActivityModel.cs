using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantActivityModel
    {
        public int ProcessorId { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string MonthName { get; set; }
        public string ProcessorName { get; set; }
        public int RetentionPercentage { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public decimal Capital { get; set; }
        public decimal ProcessorIncome { get; set; }
        public decimal OtherIncome { get; set; }
        public string TypeofActivity { get; set; }
        public string Contract { get; set; }
        public string Note { get; set; }
    }
}