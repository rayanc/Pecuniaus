using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.Repository;
using Bridge.Models;
using System.Data;
using Bridge.Models.General;
using Bridge.Models.Merchant;

namespace Bridge.BusinessTier
{
    public interface IMerchant : IRepository<MerchantsModel, int>
    {
        MerchantsModel ListMerchants(Int64 merchantId);
        IList<MerchantsModel> ListMerchants(string businessName, string rnc, string legalName, string ownerName, Int64? merchantId, Int64? contractId,
            Int64? workflowId, Int64? statusId, Int64? processornbr, string processorName, Int64? tasktype);
        IList<MerchantTempModel> ListMerchantQueue();
        IList<OwnerModel> ListMerchantOwner(Int64 merchantId);
        IList<MerchantProfile> ListCreditCardProfiles(Int64 merchantId);
        Int64 Save(MerchantTempModel model, Int64 merchantId, string isCompleted);
        int ManageCreditCardProfile(long merchantId, IList<MerchantProfile> merchantProfile, int iscompleted, Int64 contractId);
        IList<MerchantTempModel> RetriveMerchantSalesForce(string businessName, string rnc, string legalName, string ownerName);
        bool SaveDE(MerchantDEModel model, long merchantId, string isCompleted);
        MerchantsAdditionalInfo RetrieveMerchantDataEntry(Int64 merchantId);
        IList<ProcessorModel> RetrieveProcessorbyMerchant(Int64 merchantId);
        #region Renewals
        DataSet RetrieveRenewalsList();
        bool CompleteRenewalsTask(Int64 contractId);
        bool SendRequesttoProcessor(Int64 merchantId, Int64 processorId);
        IList<SearchResultsModel> ListMerchantsSeach(string businessName, string rnc, string legalName, string ownerName, long? merchantId, long? contractId, long? workflowId, long? statusId, long? processornbr, string processorName, long? tasktype, Int16? showTemp, string SearchType, Int64 assignedUserId);
        MerchantsModel RetrieveMerchantInfo(Int64 merchantId, Int64 tasktypeId, Int64 contractId);

        IList<EmailModel> RetriveEmails(Int64 SalesRepId);

        MerchantOffer RetrieveMerchantOffer(Int64 merchantId, Int64 contractId);

        #endregion
    }
}
