using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantActivityModel
    {
        public int ProcessorId { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string MonthName { get; set; }
        public string ProcessorName { get; set; }
        public int RetentionPercentage { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [DataType(DataType.Currency)]
        public decimal Capital { get; set; }
        [DataType(DataType.Currency)]
        public decimal ProcessorIncome { get; set; }
        [DataType(DataType.Currency)]
        public decimal OtherIncome { get; set; }
        public string TypeofActivity { get; set; }
        public string Contract { get; set; }
        public string Note { get; set; }
    }
}