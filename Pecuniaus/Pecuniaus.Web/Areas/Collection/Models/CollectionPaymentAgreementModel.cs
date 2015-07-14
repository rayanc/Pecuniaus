using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
namespace Pecuniaus.Collection.Models
{
    public class CollectionPaymentAgreementModel
    {
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")] 
        [Display(Name = "StartDate", ResourceType = typeof(Resources.Collection.PaymentAgreement))]
        public DateTime? startDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")] 
        [Display(Name = "EndDate", ResourceType = typeof(Resources.Collection.PaymentAgreement))]        
        public DateTime? endDate { get; set; }
      
        [Display(Name = "Status", ResourceType = typeof(Resources.Collection.PaymentAgreement))]
        public string status { get; set; }

        [Required]
        [Display(Name = "IntervalofDays", ResourceType = typeof(Resources.Collection.PaymentAgreement))]
        public Int64? intervalofDays { get; set; }
        [Display(Name = "DateofPayment", ResourceType = typeof(Resources.Collection.PaymentAgreement))]
        public DateTime dateofPayment { get; set; }
        public DateTime dueDate { get; set; }
        public double amount { get; set; }
        
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")] 
        [Display(Name = "Dateofagreement", ResourceType = typeof(Resources.Collection.PaymentAgreement))]
        public DateTime? dateOfAgreement { get; set; }
        public Int64 insertUserId { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        
    }
}