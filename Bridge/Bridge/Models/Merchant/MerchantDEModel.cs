using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models.Merchant;

namespace Bridge.Models
{
    public class MerchantDEModel
    {
        //Merchant basic   

        public string merchantName { get; set; }
        public Int64 contractId { get; set; }
        public string rnc { get; set; }
        // Business 
        public string businessName { get; set; }
        public string legalName { get; set; }
        public DateTime businessStartDate { get; set; }
        public DateTime firstProcessedDate { get; set; }
        public string businessWebSite { get; set; }
        public Int64 businessTypeId { get; set; }
        public int ownerId { get; set; }
        public Int16 isSynced { get; set; }
        public double rentAmount { get; set; }
        public double annualSales { get; set; }
        public double loanAmountRequired { get; set; }
        public Int32 industryTypeId { get; set; }
        public int propertyType { get; set; }
        public int processorCompany { get; set; }
        public int affiliationNumber { get; set; }

        public int cNetProcessorId { get; set; }
        public int vNetProcessoId { get; set; }
        public Int64 addressId { get; set; }
        public int salesRepId { get; set; }
        public int companyId { get; set; }
        public List<OwnerModel> owners { get; set; }
        public Address address { get; set; }
        public Int64 BankID { get; set; }

        public string accountNumber { get; set; }

        public string accountName { get; set; }
        public List<MerchantProcessorModel> processor { get; set; }

        public List<MerchantTradeReference> TradeReference { get; set; }

        public List<MerchantBankStatement> BankStatements { get; set; }

        public MerchantLandLord LandlordInformation { get; set; }

        public string BankCode { get; set; }

        public long TypeOfAdvanceId { get; set; }

        public int PsalesRepId { get; set; }

        public int SecsalesRepId { get; set; }
        public string AnnualSalesCalcFile { get; set; }
    }
    public class MerchantsAdditionalInfo : MerchantDEModel
    {

        public string assignedSales { get; set; }
        public int salesRepId { get; set; }
        public string taskName { get; set; }
        public string workFlowName { get; set; }
        public int workflowId { get; set; }
        public int assigneduserId { get; set; }
        public DateTime assignedDate { get; set; }
        public int merchantId { get; set; }
    }

    public class MerchantLandLord //: MerchantDEModel
    {
        public long LandlordId { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}