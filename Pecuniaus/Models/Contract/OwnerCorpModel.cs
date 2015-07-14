
using System.ComponentModel.DataAnnotations;
namespace Pecuniaus.Models.Contract
{
    public class OwnerCorpModel : BaseModel
    {
        public int Id { get; set; }

        public long OwnerId { get; set; }

        [Display(Name = "OwnerName", ResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [Required(ErrorMessageResourceName = "OwnerNameReq", ErrorMessageResourceType = typeof(Resources.Contract.CorpDocVerification))]
        public string OwnerName { get; set; }

        [Display(Name = "OwnerLastName", ResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [Required(ErrorMessageResourceName = "OwnerLastNameReq", ErrorMessageResourceType = typeof(Resources.Contract.CorpDocVerification))]
        public string OwnerLastName { get; set; }

        [Display(Name = "PassportNbr", ResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [Required(ErrorMessageResourceName = "PassportNbrReq", ErrorMessageResourceType = typeof(Resources.Contract.CorpDocVerification))]
        public string PassportNbr { get; set; }

        [Display(Name = "Telephone", ResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [Required(ErrorMessageResourceName = "TelephoneReq", ErrorMessageResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "IsAuthorized", ResourceType = typeof(Resources.Contract.CorpDocVerification))]
        [Required(ErrorMessageResourceName = "IsAuthorizedReq", ErrorMessageResourceType = typeof(Resources.Contract.CorpDocVerification))]
        public bool IsAuthorized { get; set; }
        public long contactId { get; set; }
        public long addressId { get; set; }
    }
}
