
using Pecuniaus.Utilities.Localization.Enum;
using System.ComponentModel.DataAnnotations;
namespace Pecuniaus.UICore
{
    public enum TaskTypes
    {
        [LocalizedDescription("PQScanDocument", typeof(Resources.Common))]
        PQScanDocument = 2,

        PQMerchantReview = 1,

        [LocalizedDescription("PQMonthlyCreditCardVolumes", typeof(Resources.Common))]
        PQMonthlyCreditCardVolumes = 4,

        [LocalizedDescription("PQMerchantEvaluation", typeof(Resources.Common))]
        PQMerchantEvaluation = 5,

        [LocalizedDescription("PQOfferCreation", typeof(Resources.Common))]
        PQOfferCreation = 6,

        [LocalizedDescription("PQOfferAcceptance", typeof(Resources.Common))]
        PQOfferAcceptance = 7,

        [LocalizedDescription("PQDataEntry", typeof(Resources.Common))]
        PQDataEntry = 3,

        [LocalizedDescription("CWScanDocument", typeof(Resources.Common))]
        CWScanDocument = 8,

               [LocalizedDescription("CWVerificationCall", typeof(Resources.Common))]
        CWVerificationCall = 9,

        [LocalizedDescription("CWDataEntry", typeof(Resources.Common))]
              CWDataEntry = 10,

        //[LocalizedDescription("CWBankInformationVerification", typeof(Resources.Common))]
        //CWBankInformationVerification = 11,

        [LocalizedDescription("CWVerificationTask", typeof(Resources.Common))]
        CWVerificationTask = 11,

        //[LocalizedDescription("CWCorporateDocumentsVerification", typeof(Resources.Common))]
        //CWCorporateDocumentsVerification = 12,

        //[LocalizedDescription("CWCommercialNameVerification", typeof(Resources.Common))]
        //CWCommercialNameVerification = 13,

        //[LocalizedDescription("CWLandlordVerification", typeof(Resources.Common))]
        //CWLandlordVerification = 14,

        [LocalizedDescription("CWReview", typeof(Resources.Common))]
        CWReview = 15,

        [LocalizedDescription("CWContract", typeof(Resources.Common))]
        CWContract = 16,

        [LocalizedDescription("CWFunding", typeof(Resources.Common))]
        CWFunding = 17,

        CWFinalValidation = 18,


        CWListOfMerchants = 19,

        [LocalizedDescription("RenewalEvaluation", typeof(Resources.Renewal.Renewal))]
        RWMerchantEvaluation = 20,
        [LocalizedDescription("DocVerification", typeof(Resources.Renewal.Renewal))]
        RWDocumentVerification = 21,
        [LocalizedDescription("OfferList", typeof(Resources.Renewal.Renewal))]
        RWOfferCreation = 22,
        [LocalizedDescription("RenewalReview", typeof(Resources.Renewal.Renewal))]
        RWRenewalReview = 23,
        [LocalizedDescription("Contract", typeof(Resources.Renewal.Renewal))]
        RWContract = 24,
        [LocalizedDescription("CWFunding", typeof(Resources.Common))]
        RWFunding = 25,
        
        RWFinalValidation = 26
		
    }
}
