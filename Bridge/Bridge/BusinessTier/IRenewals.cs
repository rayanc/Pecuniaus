using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;

namespace Bridge.BusinessTier
{
    public interface IRenewals 
    {
        //IList<RenewalModel> RenewalsList(string filter);
        DataSet RetrieveRenewalsList(string filter);
        long CompleteRenewal(Int64 contractId);
        bool Snooze(int Contractid, DateTime snooze, int PercentPaid);       
        IList<GeneralModel> RetreiveDeclineReasons();
        DataSet RetrieveContractDetatis(Int64 contractId);
        bool RenewalFunded(FundingModel model);
        int DeclineContract(Int64 contractId, Int64 declineReasonId, Int64 workflowId, string declineNotes);
        IList<FinalVerificationModel> RetrieveFinalValidation(long MerchantId, long ContractId);
        bool SaveFinalValidation(long ContractId, string action);
        FundingModel RetrieveFundingDetails(Int64 contractId, Int64 merchantId);
        SnoozeModel RetreiveSnooze(long Contractid);
        long GetActiveContractID(long merchantId, long ContractId);

    }
}