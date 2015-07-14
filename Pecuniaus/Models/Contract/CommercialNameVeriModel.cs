using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Pecuniaus.Utilities.Validation;

namespace Pecuniaus.Models.Contract
{
    public class CommercialNameVeriModel
    {
        public CommercialNameVeriModel()
        {
            States = new List<SelectListItem>();
        }

        public IEnumerable<SelectListItem> States { get; set; }
        [Display(Name = "BusinessName", ResourceType = typeof(Resources.Contract.CommertialNameVerification))]
        public string BusinessName { get; set; }

        [Display(Name = "Telephone", ResourceType = typeof(Resources.Contract.CommertialNameVerification))]
        [RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "TelephoneVal")]
        public string telephone { get; set; }

        public long addressId { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resources.Contract.CommertialNameVerification))]
        public string Address { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources.Contract.CommertialNameVerification))]
        public string City { get; set; }

        public string State { get; set; }

        [Display(Name = "State", ResourceType = typeof(Resources.Contract.CommertialNameVerification))]
        public long StateId { get; set; }

        public long DocTypeId{ get; set; }

        //[Display(Name = "FileName", ResourceType = typeof(Resources.Contract.CommertialNameVerification))]
        //public string FileName { get; set; }

        //[Display(Name = "FileDetails", ResourceType = typeof(Resources.Contract.CommertialNameVerification))]
        //public string FileDetails { get; set; }
    }
}
