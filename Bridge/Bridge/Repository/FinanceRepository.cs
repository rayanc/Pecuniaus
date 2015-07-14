using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;
using System.Data;


namespace Bridge.Repository
{
    public class FinanceRepository : IFinance, IDisposable
    {
        #region Methods

        #region Retrieve/List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<FinanceModel> RetrieveFinanceDetails(SearchFinanceModel model)
        {
            return new DataAccess.DataAccess().ExecuteReader<FinanceModel>("AVZ_FIN_spRetriveActivities", new
            {
                MerchantId = model.merchantId == null ? 0 : model.merchantId,
                ActivityTypeId = model.activityTypeId == null ? 0 : model.activityTypeId,               
                DateOfActivity = model.dateOfActivity,
                Affiliation = model.affiliationId == null ? 0 : model.affiliationId,
                ProcessorId = model.processorId == null ? 0 : model.processorId
            });
        }

        #endregion

        #region Update/Insert
        /// <summary>
        /// Insert Finance detials in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<FinanceModel> InsertFinanceDetails(SearchFinanceModel model)
        {
            return new DataAccess.DataAccess().ExecuteReader<FinanceModel>("AVZ_FIN_spUpdateActivities", new
            {
                MerchantId = model.merchantId,
                ActivityTypeId = model.activityTypeId,
                DateOfActivity = model.dateOfActivity,
                Amount = model.amount,
                ProcessorId = model.processorId,
                Notes = string.IsNullOrEmpty(model.notes)? string.Empty : model.notes,
                InsertUserId = model.insertUserId,
                ContractId=model.contractId,
                TransferContractId = model.transferContractId
            });
        }

        /// <summary>
        /// To fetch activity list from DB
        /// </summary>
        /// <returns></returns>
        public IList<FinanceDD> ListActivityType()
        {
            return new DataAccess.DataAccess().ExecuteReader<FinanceDD>("AVZ_FIN_spListActivityTypes");
        }

        /// <summary>
        /// To fetch processor name from DB
        /// </summary>
        /// <returns></returns>
        public IList<FinanceDD> ListProcessorName()
        {
            return new DataAccess.DataAccess().ExecuteReader<FinanceDD>("AVZ_FIN_spProcessorNames");
        }

       /// <summary>
       /// To retrieve active funded contract
       /// </summary>
       /// <param name="MerchantId"></param>
       /// <returns></returns>
        public IList<AddFinanceModel> RetrieveFundedContract(Int64 MerchantId)
        {
            return new DataAccess.DataAccess().ExecuteReader<AddFinanceModel>("AVZ_FIN_spGetFundedContracts", new { MerchantId = MerchantId });  
        }

        public bool Update(FinanceModel model)
        {
            return true;
        }

        public bool DeleteActivity(Int64 actityID)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_FIN_spDeleteActivity",
                    new
                    {
                        actityID = actityID,
                    });
        }


        #endregion

        #region Others
        public bool Remove(int id)
        {
            return true;
        }

        public bool Create(FinanceModel model)
        {
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}