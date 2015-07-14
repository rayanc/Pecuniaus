using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.Reports.Models
{
    public class ReportParametersModel
    {
        public string ReportId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastActivityDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string Agent { get; set; }

        public string SalesRep { get; set; }

        public string Module { get; set; }

        public string ContractStatus { get; set; }

        public string Type { get; set; }

        public string Percent { get; set; }

        public string Merchant { get; set; }

        public string TaskStatus { get; set; }
    }
}