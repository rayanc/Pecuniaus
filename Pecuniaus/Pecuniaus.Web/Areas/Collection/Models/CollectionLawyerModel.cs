using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.Collection.Models 
{
    public class LawyerModel
    {
        public int ID { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public Int64 lawyerId { get; set; }

        public Int64 insertUserId { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Pecuniaus.Resources.Collection.Collection))]
        public string firstName { get; set; }

         [Required]
        [Display(Name = "LastName", ResourceType = typeof(Pecuniaus.Resources.Collection.Collection))]
        public string lastName { get; set; }

         [Required]
        [Display(Name = "Company", ResourceType = typeof(Pecuniaus.Resources.Collection.Collection))]
        public string companyName { get; set; }
        public DateTime dateOfAssignment { get; set; }
        public string documentType { get; set; }
        public bool isDeleted { get; set; }
        //public List<LegalDocuments> LegalDocuments { get; set; }
    }
    
    public class LegalDocuments
    {
        public bool Select { get; set; }
        public Int64 documentId { get; set; }
        public string documentType { get; set; }
    }

   
    //public class AssignLawyerModel
    //{
    //    public List<LawyerModel> lawyerModel { get; set; }
    //    public List<LegalDocuments> legalDocuments { get; set; }
    //}
}