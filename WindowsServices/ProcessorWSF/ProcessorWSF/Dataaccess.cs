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
        
        public DataSet RetrieveWSFInformation()
        {
           return  new DataAccess().ExecuteDataSet("avz_cnt_spRetrieveInformationWSF", new { });

        }
       
    }
}
