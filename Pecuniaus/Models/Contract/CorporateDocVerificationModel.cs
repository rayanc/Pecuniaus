using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pecuniaus.Utilities.Validation;

namespace Pecuniaus.Models.Contract
{
   public class CorporateDocVerificationModel
    {
        [Display(Name = "NameOfCompany", ResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [Required(ErrorMessageResourceName = "NameOfCompanyReq", ErrorMessageResourceType = typeof(Resources.Contract.CorpDocVerification))]
        public string NameOfCompany { get; set; }

        [Display(Name = "RNCNumber", ResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [Required(ErrorMessageResourceName = "RNCNumberReq", ErrorMessageResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [RegularExpression(ValidationRules.RNC, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "RNCValidation")]
        public string RNCNumber { get; set; } 
        public string AddressDesc { get; set; }
        public List<OwnerModel> OwnerList { get; set; }
        public long DocTypeId { get; set; }
        //public string FileName { get; set; }
        //public string FileDetails { get; set; }   
    }
}
