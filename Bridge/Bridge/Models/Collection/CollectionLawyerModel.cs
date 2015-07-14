using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models 
{
    public class LawyerModel
    {
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public Int64 lawyerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string companyName { get; set; }
        public DateTime dateOfAssignment { get; set; }
        public Int64 insertUserId { get; set; }
        public string documentType { get; set; }
        public bool isDeleted { get; set; }
        //public List<LegalDocuments> LegalDocuments { get; set; }
    }
    
    public class LegalDocuments
    {
        public Int64 documentId { get; set; }
        public string documentType { get; set; }
    }

    //public class AssignLawyerModel
    //{
    //    public List<LawyerModel> lawyerModel { get; set; }
    //    public List<LegalDocuments> legalDocuments { get; set; }
    //}
}