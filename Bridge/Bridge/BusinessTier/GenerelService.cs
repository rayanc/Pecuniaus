using System;
using System.Collections.Generic;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.Repository;

namespace Bridge.BusinessTier
{
    public class GenerelService:IDisposable
    {
        #region Variables & Constants

        private IGeneral generalRepository;

        #endregion

        #region contructors/Destructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GenerelService() : this(new GeneralRepository()) { }

        /// <summary>
        /// Inject repository
        /// </summary>
        /// <param name="documentsRepository"></param>
     public GenerelService(IGeneral generalRepository)
        {
            this.generalRepository = generalRepository;
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
       
        #endregion

        #region Methods
        public IList<GeneralModel> FindBy(dynamic query)
        {
            return generalRepository.FindBy(query);
        }
       public  bool SetSnooze(Int64 contractId, double percentPaid, DateTime snoozeDate)
        {
            return generalRepository.SetSnooze(contractId, percentPaid, snoozeDate);

        }
       public bool Kickback(Int64 merchantId,Int64 tasktypeId,Int64 workflowId,Int64 contractid)
       {
           return generalRepository.Kickback(merchantId,  tasktypeId,workflowId,contractid);

       }
        #endregion
    }
}