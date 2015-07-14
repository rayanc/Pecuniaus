using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Repository;
using Bridge.Models;
using System.Data;

namespace Bridge.BusinessTier
{
    public interface IFinance : IRepository<FinanceModel, int>
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IList<FinanceModel> RetrieveFinanceDetails(SearchFinanceModel model);

        /// <summary>
        /// Insert Finance detials in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IList<FinanceModel> InsertFinanceDetails(SearchFinanceModel model);

        /// <summary>
        /// To fetch activity list from DB
        /// </summary>
        /// <returns></returns>
        IList<FinanceDD> ListActivityType();

        /// <summary>
        /// To fetch processor name from DB
        /// </summary>
        /// <returns></returns>
        IList<FinanceDD> ListProcessorName();

        /// <summary>
       /// To retrieve active funded contract
       /// </summary>
       /// <param name="MerchantId"></param>
       /// <returns></returns>
        IList<AddFinanceModel> RetrieveFundedContract(Int64 MerchantId);

        bool DeleteActivity(Int64 actityID);

        #endregion
    }
}