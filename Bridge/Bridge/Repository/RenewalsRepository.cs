using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;


namespace Bridge.Repository
{
    public class RenewalsRepository : IRenewals
    {
        //public IList<RenewalModel> RenewalsList(string filter)
        //{

        //    return new DataAccess.DataAccess().ExecuteReader<RenewalModel>("avz_ren_spRetrievRenewalsList", new { Filter = filter });

        //}
        public DataSet RetrieveRenewalsList(string filter)
        {
            DataSet renewalsList = null;
            renewalsList = new DataAccess.DataAccess().ExecuteDataSet("avz_ren_spRetrievRenewalsList", new { Filter = filter });
            return renewalsList;
        }

        public bool Snooze(int Contractid, DateTime snooze, int PercentPaid)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_ren_spUpdateSnooze", new { ContractId = Contractid, Snooze = snooze, PercentPaid = PercentPaid });
        }

        public SnoozeModel RetreiveSnooze(long Contractid)
        {
            return new DataAccess.DataAccess().ExecuteReader<SnoozeModel>("avz_ren_spRetrieveSnoozeDetails", new { contractid = Contractid }).FirstOrDefault();
        }

        public long CompleteRenewal(Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteScalar<long>("avz_ren_spCompleteRenewals", new { contractId = contractId });
        }

        public IList<GeneralModel> RetreiveDeclineReasons()
        {
            GeneralModel declinemodel = null;
            IList<GeneralModel> lstDeclinereason = new List<GeneralModel>();
            DataSet ds = new DataAccess.DataAccess().ExecuteDataSet("avz_ren_spRetrieveDeclineReasons", null);
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                declinemodel = new GeneralModel();
                declinemodel.keyId = Convert.ToInt32(ds.Tables[0].Rows[i]["DeclineReasonId"]);
                declinemodel.description = Convert.ToString(ds.Tables[0].Rows[i]["ReasonDescription"]);
                lstDeclinereason.Add(declinemodel);
            };
            return lstDeclinereason;
        }

        public DataSet RetrieveContractDetatis(Int64 contractId)
        {
            DataSet contInformtion = null;
            contInformtion = new DataAccess.DataAccess().ExecuteDataSet("avz_ren_spretrieveContractInformation", new { contractId = contractId });
            return contInformtion;
        }

        public FundingModel RetrieveFundingDetails(Int64 contractId, Int64 merchantId)
        {

            return new DataAccess.DataAccess().ExecuteReader<FundingModel>("avz_ren_spFunding", new { ContractId = contractId, MerchantId = merchantId }).FirstOrDefault();
        }

        public bool RenewalFunded(FundingModel model)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("azn_ren_spCompleteFunding", new
                {
                    MerchantId = model.merchantId,
                    ContractId = model.contractId,
                    BankId = model.bankId,
                    AccountNumber = model.accountNumber,
                    AccountName = model.accountName,
                    McaAmount = model.mcaAmount,
                    ExpenseAmount = model.expenseAmount,
                    TotalOwnedAmount = model.totalFundingAmount,
                    OwnerId = model.ownerId,
                    ContractReviewed = model.contractReviewed,
                    ContractFunded = model.contractFunded,
                    Action = model.action
                });
        }

        public int DeclineContract(Int64 contractId, Int64 declineReasonId, Int64 workflowId, string declineNotes)
        {

            if (new DataAccess.DataAccess().ExecuteNonQuery("avz_ren_spDeclineRenewal", new { contractId = contractId, declineReasonId = declineReasonId, workflowId = workflowId, declineNotes = declineNotes }))
            {
                return 1;

            }
            else { return 0; }
        }

        public IList<FinalVerificationModel> RetrieveFinalValidation(long MerchantId, long ContractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<FinalVerificationModel>("AVZ_ren_spRetrieveFinalValidation", new { MerchantId = MerchantId, ContractId = ContractId });
        }

        public bool SaveFinalValidation(long ContractId, string action)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("azn_ren_spCompleteFinalValidation", new { ContractId = ContractId, Action = action });
        }


        public long GetActiveContractID(long merchantId, long ContractId)
        {
            return new DataAccess.DataAccess().ExecuteScalar<long>("azn_sp_GetActiveContractID", new { MerchantId = merchantId, ContractId = ContractId });

        }

    }
}