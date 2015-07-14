using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Repository;
using Bridge.Models;
using System.Data;
using Bridge.Models.Contracts;

namespace Bridge.BusinessTier
{
    public interface IContract : IRepository<BankDetailModel, int>
    {
        #region Methods
        /// <summary>
        /// To retrieve bank name list
        /// </summary>
        /// <returns></returns>
        IList<BankNameModel> ListBankNames();

        /// <summary>
        /// To retrieve bank details
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
        IList<BankDetailModel> RetrieveBankDetails(Int64 contractId, Int64 merchantId, int bankId);

        /// <summary>
        /// To get the details of decline
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        Int64 DeclineContract(Int64 contractId, Int64 declineReasonId, Int64 workflowId, string declineNotes, bool evaluateAgain, Int64 merchantId, string screen);

        /// <summary>
        /// To Insert or Update the bank details of Merchant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool InsUpdateBankDetails(BankDetailModel model, Int16 isCompleted);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        DataSet RetrieveCorpDetails(Int64 contractId, Int64 merchantId);

        /// <summary>
        /// To Retrieve Commercial Bank Information
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        IList<CommercialVerification> RetrieveCommercialBankInfo(Int64 contractId, Int64 merchantId);

        /// <summary>
        /// To retrieve funding details
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        FundingModel RetrieveFundingDetails(Int64 contractId, Int64 merchantId);

        /// <summary>
        /// To Retrieve BLA Details for a particular Contract - Generate Contract Screen
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        DataSet RetrieveBLADetails(Int64 contractId, Int64 merchantId);
        /// <summary>
        /// Reterieve IOU
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        DataSet RetrieveIOUDetails(Int64 contractId, Int64 merchantId);
        /// <summary>
        /// To complete the tasks of Contract
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="taskTypeId"></param>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        bool CompleteContractTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId);

        DataSet GetMerchantDetailsForQuestion(Int64 merchantId);
        VerificationModel RetrieveVerificationDetails(Int64 contractId,Int64 merchantId, string entity);

        bool UpdateCommercialDetails(Int64 merchantId, CommercialVerification cm);

        bool UpdateCommericalOwnerDetails(Int64 contractId, Int64 merchantId, List<OwnerModel> cm);

        bool ContractFunded(FundingModel model);

        bool SaveFinalValidation(long ContractId, string action);
        IList<FinalVerificationModel> RetrieveFinalValidation(long MerchantId, long ContractId);

        bool CompleteOfferAcceptanceTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId);

        bool UpdateVeriCall(VerificationModel verModel, long contractId, Int16 isCompleted, string scriptFile);
       
        bool UpdateLandLordCall(VerificationModel verModel, long contractId, Int16 isCompleted, string scriptFile);

        ContractModel GetAdminExp(Int64 contractId);

        AnnualSaleModel RetrieveAnnualSalesFile(long merchantId, long contractId);
       
        #endregion
    }
}