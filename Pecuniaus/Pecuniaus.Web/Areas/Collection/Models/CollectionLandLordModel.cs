using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Collection.Models 
{

    public class CollectionLandLordModel
    {        
        public Int64? MerchantID { get; set; }    
   
        //[Required]
        [Display(Name = "TypeofProperty", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Landlord))]
        public Int64? typeOfPropertyId { get; set; }

        [Required]
        [Display(Name = "LandlordCompanyName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Landlord))]
        public string landlordCompanyName { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Pecuniaus.Resources.Collection.Collection))]
        public string landlordFirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Pecuniaus.Resources.Collection.Collection))]
        public string landlordLastName { get; set; }

        [Required]
        [Display(Name = "MonthlyRentAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Landlord))]
        public string monthlyRentAmount { get; set; }
        [Required]
        [Display(Name = "Address", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string address { get; set; }

        [Required]
        [Display(Name = "City", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string city { get; set; }

        [Required]
        [Display(Name = "Telephone", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        [DataType(DataType.PhoneNumber)]
        public string telePhone { get; set; }

        [Required]
        [Display(Name = "Cellphone", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        [DataType(DataType.PhoneNumber)]
        public string cellPhone { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string email { get; set; }
                
        public Int64? userId { get; set; }
        public LandlordQuestionsModel Questions { get; set; }
        
    }  

    public class LandlordQuestionsModel
    {
        [Display(Name = "ScriptFile", ResourceType = typeof(Resources.Contract.VerificationCall))]
        public string ScriptFile { get; set; }
        public string ScriptFilePath { get; set; }
        public string UserFullName { get; set; }
        public LLRightWrong Answers { get; set; }
        public LlMerchantDetailModel MerchantDetails { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
        
    }

    public class LLRightWrong
    {
        public bool OwnerName { get; set; }
        public bool MerchantAddress { get; set; }
        public bool LandlordName { get; set; }
        public bool LegalName { get; set; }
        public bool ContractStartDate { get; set; }
        public bool ContractAutoRenew { get; set; }
        public bool ContractExpiryDate { get; set; }
        public bool ContractRenewAtEnd { get; set; }
        public bool RentedAmount { get; set; }
        public bool ContinueRent { get; set; }
        public bool AgreeRentLate { get; set; }
        public bool LessesBusiness { get; set; }
    }

    public class LlMerchantDetailModel
    {
        public long MerchantId { get; set; }
        [Required]
        public string LegalName { get; set; }
        [Required]
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string MerchantAddress { get; set; }
        public decimal? RentedAmount { get; set; }
        //  public string LandlordName { get; set; }
        public string LLFirstName { get; set; }
        public string LLLastName { get; set; }
        public long RentFlag { get; set; }
    }

    public class QuestionModel
    {
        public QuestionModel()
        {
            DropDown = new List<SelectListItem>();
        }
        public long QuestionId { get; set; }
        public string QuestionDesc { get; set; }
        public string QuestionType { get; set; }
        public long AnswerId { get; set; }
        public string AnswerDesc { get; set; }
        public string Entity { get; set; }
        public long ContractId { get; set; }
        public long MerchantId { get; set; }
        public IEnumerable<SelectListItem> DropDown { get; set; }
    }
   
}