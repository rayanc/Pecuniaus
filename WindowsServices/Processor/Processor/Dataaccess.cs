using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.DataAccess;
namespace Processor
{
    public class DataLayer
    {
        public int InsertCreditProfiles(string iNprovidedXml)
        {
             if (new DataAccess().ExecuteNonQuery("avz_mrc_InsertCardProfiles", new { providedXml = iNprovidedXml }))
             { }
            return 1;

        }
        //

        public int UpdateProcessorQueue(Int64 merchantId, string processorId)
        {
            if (new DataAccess().ExecuteNonQuery("avz_cc_spUpdatePocessorQueue", new { merchantId = merchantId, processorId = processorId }))
            { }
            return 1;

        }
        /// <summary>
        /// Runs daily to execute the Credit volumes to be calculated
        /// </summary>
        public void CalculateCreditVolumes()
        {
            new DataAccess().ExecuteNonQuery("avz_cc_spInsertCreditVolumesAggregate", new { });
            
        }
        public DataSet RetrieveProcessorQueue()
        {
           return  new DataAccess().ExecuteDataSet("avz_cc_spRetrievePocessorQueue", new { });

        }
        public DataSet RetrieveScanDocuments()
        {
            return new DataAccess().ExecuteDataSet("avz_cc_spRetrieveSacanDocuments", new { });

        }
       
    }
}
