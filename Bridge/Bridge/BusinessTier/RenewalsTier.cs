using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;

namespace Bridge.BusinessTier
{
    public class RenewalsTier : IDisposable
    {
        #region Private Variables
        private IRenewals renewalRepository;
        #endregion

        #region Contructors
        public RenewalsTier() : this(new RenewalsRepository()) { }
        public RenewalsTier(IRenewals renewalsRepository)
        {
            this.renewalRepository = renewalsRepository;
        }

        #endregion

        #region Methods
       
        //public IList<RenewalModel> RenewalsList(string filter)
        //{
        //    return renewalRepository.RenewalsList(filter);
        //}
        public DataSet RetrieveRenewalsList(string filter)
        {
            return renewalRepository.RetrieveRenewalsList(filter);
        }

        public long CompleteRenewal(Int64 contractId)
        {
            return renewalRepository.CompleteRenewal(contractId);
        }

        public bool Snooze(int Contractid, DateTime snooze, int PercentPaid)
        {
            return renewalRepository.Snooze(Contractid, snooze, PercentPaid);
        }

        public IList<GeneralModel> RetreiveDeclineReasons()
        {
            return renewalRepository.RetreiveDeclineReasons();
        }

        public DataSet RetrieveContractDetatis(Int64 contractId)
        {
            return renewalRepository.RetrieveContractDetatis(contractId);
        }
        public bool RenewalFunded(FundingModel model)
        {
            return renewalRepository.RenewalFunded(model);
        }
        public int DeclineContract(Int64 contractId, Int64 declineReasonId, Int64 workflowId, string declineNotes)
        {
            return renewalRepository.DeclineContract(contractId, declineReasonId, workflowId, declineNotes);
        }

        public IList<FinalVerificationModel> RetrieveFinalValidation(long MerchantId, long ContractId)
        {
            return renewalRepository.RetrieveFinalValidation( MerchantId,  ContractId);
        }
        public bool SaveFinalValidation(long ContractId, string action)
        {
            return renewalRepository.SaveFinalValidation(ContractId,action);
        }
        public FundingModel RetrieveFundingDetails(Int64 contractId, Int64 merchantId)
        {
            return renewalRepository.RetrieveFundingDetails(contractId, merchantId);
        }

        public SnoozeModel RetreiveSnooze(long Contractid)
        {
            return renewalRepository.RetreiveSnooze(Contractid);
        }

        public long GetActiveContractID(long merchantId, long ContractId)
        {
            return renewalRepository.GetActiveContractID(merchantId, ContractId);

        }
        #endregion

        # region Distructor
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        #endregion

    }
}