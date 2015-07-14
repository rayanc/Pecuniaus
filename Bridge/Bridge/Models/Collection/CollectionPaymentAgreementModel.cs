using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class CollectionPaymentAgreementModel
    {
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }   
        public DateTime startDate { get; set; }        
        public DateTime endDate { get; set; }
        public DateTime dateOfAgreement { get; set; }        
        public string status { get; set; }        
        public int intervalofDays { get; set; }        
        public DateTime dateOfPayment { get; set; }
        public Int64 insertUserId { get; set; }
        public DateTime dueDate { get; set; }
        public double amount { get; set; }   
    }
}