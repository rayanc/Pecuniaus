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
        public int InsertProcessorResponsesetup(string iNprovidedXml)
        {
            if (new DataAccess().ExecuteNonQuery("avz_mrc_spInsertProcessorResponsesetup", new { providedXml = iNprovidedXml }))
             { }
            return 1;

        }
       
    }
}
