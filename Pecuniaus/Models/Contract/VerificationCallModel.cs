using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Models.Contract
{
    public class VerificationCallModel : BaseModel
    {
        public MerchantDetailQuestionModel MerchantDetails { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }

        //   public string SelectedAdvance{get;set;}
        public IEnumerable<SelectListItem> AdvanceTypes { get; set; }
        public VCRightWrong VcAnswers { get; set; }
        [Display(Name = "ScriptFile", ResourceType = typeof(Resources.Contract.VerificationCall))]
        public string ScriptFile { get; set; }
        public string RandStr { get { return new Random().Next().ToString(); } }
        public string ScriptFilePath { get; set; }
        public string UserFullName { get; set; }
        //public IEnumerable<QuestionModel> questions { get; set; }
    }

    public class VCRightWrong
    {
        public bool OwnerName { get; set; }
        public bool OwnerPassport { get; set; }
        public bool LegalName { get; set; }
        public bool BusinessName { get; set; }
        public bool IsAuthorised { get; set; }
        public bool BusinessStartDate { get; set; }
        public bool GrossAnnualSales { get; set; }
        public bool TypeOfAdvanceId { get; set; }
        public bool LoanAmount { get; set; }
        public bool OwnedAmount { get; set; }
        public bool RetensionPct { get; set; }
        public bool TCCardProcessor { get; set; }
        public bool AdminExp { get; set; }
        public bool AcceptedTrade { get; set; }

        //public bool SalesRepName { get; set; }
        //public bool OwnerLastName { get; set; }
        //public bool OwnedAmount { get; set; }
        //public bool MerchantAddress { get; set; }
        //public bool RentedAmount { get; set; }
    }
}
