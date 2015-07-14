using Pecuniaus.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantLandlordQuestionsModel
    {
        //public string Answer { get; set; }
        //public string Question { get; set; }

        public long MerchantId { get; set; }
        public string LegalName { get; set; }
        public string BusinessName { get; set; }

        public string SalesRepName { get; set; }

        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }

        //public string OwnerName { get; set; }

        public int IsAuthorised { get; set; }

        public long RetensionPct { get; set; }

        public double LoanAmount { get; set; }

        public double OwnedAmount { get; set; }
        public double GrossAnnualSales { get; set; }
        public double AdminExp { get; set; }

        public string OwnerPassport { get; set; }

        public DateTime BusinessStartDate { get; set; }

        //public List<UseOfAdvanceModel> advances { get; set; }

        public string MerchantAddress { get; set; }

        public string LandlordAddress { get; set; }

        public double? RentedAmount { get; set; }

        public string LLFirstName { get; set; }
        public string LLLastName { get; set; }

        public long TypeOfAdvanceId { get; set; }

        public long RentFlag { get; set; }
        public DateTime ContractStartDate;

        public DateTime ContractExpireDate;

        public string LLCompany { get; set; }
        public LLRightWrong Answers { get; set; }
        public IEnumerable<Pecuniaus.Models.Contract.QuestionModel> Questions { get; set; }


        public string scriptFile { get; set; }
        public string ScriptFilePath { get; set; }
        
    }
}