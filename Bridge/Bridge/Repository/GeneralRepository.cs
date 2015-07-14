using Bridge.BusinessTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;
using Bridge.DataAccess;
using Bridge.Utility;
namespace Bridge.Repository
{
    public class GeneralRepository : IGeneral, IDisposable
    {
        #region Base Methods
        public void Add(GeneralModel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(GeneralModel entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(GeneralModel entity)
        {
            throw new NotImplementedException();
        }

        public GeneralModel FindBy(int id)
        {
            throw new NotImplementedException();
        }

        public IList<GeneralModel> FindBy(dynamic query)
        {
            IList<GeneralModel> generallist = null;
            if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.indusrty))
            {
                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveIndustryTypes", null);

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.address))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveAddressTypes", null);

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.declinereason))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveDeclineReasons", null);

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.tasktypes))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveTaskTypes", new
                               {
                                   workflowId = 1
                               });

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.taskstatus))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveTaskStatus", null
                              );

            }
            //
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.collectiontypes))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_col_spRetrieveActivityTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.business))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveBusinessTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.processor))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveProcessorTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.province))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveProvinceTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.notetype))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveNoteTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.propertyType))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrievePropertyTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.documentType))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveDocumentTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.workflowType))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveWorkflowTypes", null
                              );

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.groupType))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveGroupTypes", null
                              );
            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.group))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveGroups", null
                              );
            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.salesreps))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveSalesReps", null);
            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.advance))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveAdvances", null);

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.user))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveUserTypes", null);

            }
            else if (query == Convert.ToInt32(Bridge.Utility.Utilities.Types.module))
            {

                generallist = new DataAccess.DataAccess().ExecuteReader<GeneralModel>("avz_gen_spRetrieveModuleTypes", null);

            }
            return generallist;
        }

        public IList<GeneralModel> FindBy(dynamic query, int index, int count)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Create(GeneralModel entity)
        {
            throw new NotImplementedException();
        }

        bool IRepository<GeneralModel, int>.Update(GeneralModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        public bool SetSnooze(Int64 contractId, double percentPaid, DateTime snoozeDate)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_gen_spInsertSnooze", new { contractId = contractId, percentPaid = percentPaid, snoozeDate = snoozeDate });
        }
         public bool Kickback(Int64 merchantId,Int64 tasktypeId,Int64 workflowId,Int64 contractid)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_gen_spKickback", new { 
                merchantId = merchantId,             
                tasktypeId = tasktypeId,
                workflowId = workflowId,
                contractid = contractid
            });
        }
    }
}