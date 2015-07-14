using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.Repository;
using System.Globalization;

namespace Bridge.BusinessTier
{
    public class FinanceTier : IDisposable
    {
        #region Private Variables

        private IFinance financeRepository;

        #endregion

        #region Contructors

        public FinanceTier() : this(new FinanceRepository()) { }
        public FinanceTier(IFinance financeRepository)
        {
            this.financeRepository = financeRepository;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<FinanceModel> RetrieveFinanceDetails(SearchFinanceModel model)
        {
            if (String.IsNullOrEmpty(model.dateOfActivity))
                model.dateOfActivity = "0000-00-00";
            else
            {
                DateTime dt = DateTime.ParseExact(model.dateOfActivity, "yyyy-MM-ddThh:mm:ss",
                                  CultureInfo.InvariantCulture);
                model.dateOfActivity = dt.ToString("yyyy-MM-dd");
            }
            return financeRepository.RetrieveFinanceDetails(model);
        }

        /// <summary>
        /// Insert Finance detials in DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<FinanceModel> InsertFinanceDetails(SearchFinanceModel model)
        {
            return financeRepository.InsertFinanceDetails(model);
        }

        /// <summary>
        /// To fetch activity list from DB
        /// </summary>
        /// <returns></returns>
        public IList<FinanceDD> ListActivityType()
        {
            return financeRepository.ListActivityType();
        }

        /// <summary>
        /// To fetch processor name from DB
        /// </summary>
        /// <returns></returns>
        public IList<FinanceDD> ListProcessorName()
        {
            return financeRepository.ListProcessorName();
        }

        /// <summary>
        /// To retrieve active funded contract
        /// </summary>
        /// <param name="MerchantId"></param>
        /// <returns></returns>
        public IList<AddFinanceModel> RetrieveFundedContract(Int64 MerchantId)
        {
            return financeRepository.RetrieveFundedContract(MerchantId);
        }

        public bool DeleteActivity(Int64 actityID)
        {
            return financeRepository.DeleteActivity(actityID);
        }

        
        #endregion
        public void Dispose()
        {

        }
    }
}