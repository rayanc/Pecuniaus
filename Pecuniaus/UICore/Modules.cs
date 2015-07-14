using Pecuniaus.Utilities.Localization.Enum;
namespace Pecuniaus.UICore
{
    public enum Modules
    {
    //    [LocalizedDescription("Administrator", typeof(Resource))]
        UserManagement = 1,
        ContractWorkflow = 2,
        CollectionWorkflow = 3,
        PrequalWorkflow = 4,
        PQ_MerchantReview = 5,
        PQ_DocumentScanning = 6,
        PQ_DataEntry = 7,
        PQ_MonthlyCCV = 8,
        PQ_MerchantEvaluation = 9,
        PQ_OfferCreation = 10,
        PQ_OfferAcceptance = 11,
        CW_DocumentScanning = 12,
        CW_VerificatonCall = 13,
        CW_DataEntry = 14,
        CW_VerificationTask = 15,
        CW_Review = 16,
        CW_Contract = 17,
        CW_Funding = 18,
        CW_FinalValidation = 19,
        CW_BankInformationVerification = 20,
        CW_CorporateDocumentVerification = 21,
        CW_CommercialNameVerification = 22,
        CW_LandlordCall = 23,
        RenewalsWorkflow = 24,
        Finance=25,
        MP_LandingPage=26,
        MP_Business=27,
        MP_Landlord = 28,
        MP_Contacts = 29,
        MP_Owners = 30,
        MP_Profiles = 31,
        MP_Documents = 32,
        MP_Activity = 33,
        MP_Statements = 34,
        MP_History = 35,
        MP_RiskEvaluation = 36,
        MP_Contracts = 37,
        MP_Collections = 38,
        MP_Affiliations = 39,
        MP_CreditReport = 40,        
        RW_ListOfMerchantMerchants = 41,
        RW_MerchantEvaluation = 42,
        RW_DocumentVerification = 43,
        RW_OfferCreation = 44,
        RW_RenewalReview = 45,
        RW_Contract = 46,
        RW_Funding = 47,
        RW_FinalValidation = 48,
    }
}

//var modules = from Modules module in Enum.GetValues(typeof(Modules))
//                    select new
//                    {
//                        Id = (int)module,
//                        Name = module.GetDescription()
//                    };
// searchModel.modules = new MultiSelectList(modules, "Id", "Name");
