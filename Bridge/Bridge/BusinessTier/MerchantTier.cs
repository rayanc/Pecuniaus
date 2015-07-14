using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Models.General;
using Bridge.Repository;
using Bridge.Models.Merchant;


namespace Bridge.BusinessTier
{
    public class MerchantTier : IDisposable
    {
        #region Private Variables
        private IMerchant merchantsRepository;
        #endregion


        #region Contructors
        public MerchantTier() : this(new MerchantRepository()) { }
        public MerchantTier(IMerchant merchantsRepository)
        {
            this.merchantsRepository = merchantsRepository;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Retrieve merchants on the basis of ids
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public MerchantsModel RetrieveMerchants(Int64 merchantId)
        {
            return merchantsRepository.ListMerchants(merchantId);
        }

        public IList<EmailModel> RetriveEmails(Int64 SalesRepId)
        {
            return merchantsRepository.RetriveEmails(SalesRepId);
        }

        /// <summary>
        /// Retrieve search
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        /// )
        public IList<MerchantsModel> RetrieveMerchants(string businessName, string rnc, string legalName, string ownerName, Int64? merchantId, Int64? contractId,
            Int64? workflowId, Int64? statusId, Int64? processornbr, string processorName, Int64? tasktype)
        {
            return merchantsRepository.ListMerchants(businessName, rnc, legalName, ownerName, merchantId, contractId,
             workflowId, statusId, processornbr, processorName, tasktype);
        }


        /// <summary>
        /// Retrieve search
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        /// )
        public IList<SearchResultsModel> RetrieveMerchantsSeachResults(string businessName, string rnc, string legalName, string ownerName, Int64? merchantId, Int64? contractId,
            Int64? workflowId, Int64? statusId, Int64? processornbr, string processorName, Int64? tasktype, Int16? showTemp, string SearchType, Int64 assignedUserId)
        {
            return merchantsRepository.ListMerchantsSeach(businessName, rnc, legalName, ownerName, merchantId, contractId,
             workflowId, statusId, processornbr, processorName, tasktype, showTemp, SearchType, assignedUserId);
        }

        public bool Update(MerchantsModel merchant)
        {
            return merchantsRepository.Update(merchant);
        }
        public bool Create(MerchantsModel merchant)
        {
            return merchantsRepository.Create(merchant);
        }
        public bool Remove(int merchantId)
        {
            return merchantsRepository.Remove(merchantId);
        }
        public Int64 Save(MerchantTempModel merchant, Int64 merchantId, string isCompleted)
        {
            Int64 _merchantId = 0;
            _merchantId = merchantsRepository.Save(merchant, merchantId, isCompleted);
            return _merchantId;
        }
        public bool SaveDE(MerchantDEModel merchant, Int64 merchantId, string isCompleted)
        {

            return merchantsRepository.SaveDE(merchant, merchantId, isCompleted);

        }
        /// <summary>
        /// Retrieve Queue
        /// </summary>
        /// <returns></returns>
        public IList<MerchantTempModel> RetrieveQueue()
        {
            return merchantsRepository.ListMerchantQueue();
        }
        public IList<MerchantTempModel> RetriveMerchantSalesForce(string businessName, string rnc, string legalName, string ownerName)
        {
            return merchantsRepository.RetriveMerchantSalesForce(businessName, rnc, legalName, ownerName);
        }
        public IList<OwnerModel> RetrieveOwner(Int64 merchantId)
        {
            return merchantsRepository.ListMerchantOwner(merchantId);
        }
        public IList<MerchantProfile> RetriveCreditCardProfiles(Int64 merchantId)
        {
            return merchantsRepository.ListCreditCardProfiles(merchantId);
        }
        public DataSet RetrieveRenewalsList()
        {
            return merchantsRepository.RetrieveRenewalsList();
        }
        public MerchantsAdditionalInfo RetrieveMerchantDataEntry(Int64 merchantId)
        {
            return merchantsRepository.RetrieveMerchantDataEntry(merchantId);
        }
        public MerchantsModel RetrieveMerchantInfo(Int64 merchantId, Int64 tasktypeId, Int64 contractId)
        {
            return merchantsRepository.RetrieveMerchantInfo(merchantId, tasktypeId, contractId);
        }
        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #region Profiles

        public int ManageCreditCardProfile(long merchantId, IList<MerchantProfile> merchantProfile, int iscompleted, Int64 contractId)
        {
            merchantsRepository.ManageCreditCardProfile(merchantId, merchantProfile, iscompleted, contractId);
            return 1;
        }
        public bool SendRequesttoProcessor(Int64 merchantId, Int64 processorId)
        {
            return merchantsRepository.SendRequesttoProcessor(merchantId, processorId);


        }
        public IList<ProcessorModel> RetrieveProcessorbyMerchant(Int64 merchantId)
        {

            return merchantsRepository.RetrieveProcessorbyMerchant(merchantId);
        }
        #endregion
        #region Renewals
        public bool CompleteRenewalsTask(Int64 contractId)
        {
            return merchantsRepository.CompleteRenewalsTask(contractId);
        }

        public MerchantOffer RetrieveMerchantOffer(Int64 merchantId, Int64 contractId)
        {
            return merchantsRepository.RetrieveMerchantOffer(merchantId, contractId);
        }
        #endregion

    }
}