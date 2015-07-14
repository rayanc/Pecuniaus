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
        public int InsertProcessorActivities(string iNprovidedXml)
        {
            if (new DataAccess().ExecuteNonQuery("avz_mrc_spInsertProcessorActivities", new { providedXml = iNprovidedXml }))
             { }
            return 1;

        }
        public int UpdateCollectionValues()
        {
             new DataAccess().ExecuteNonQuery("avz_col_spUpdateCollectionfromProcessors", new { });
             return 1;
        }

        public void ProcessRenewals()
        {
            new DataAccess().ExecuteNonQuery("AVZ_ren_spProcessRenewals", null);
        }
       
    }
}
