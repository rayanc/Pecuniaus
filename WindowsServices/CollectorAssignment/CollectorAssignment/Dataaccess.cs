using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.DataAccess;
namespace Collector
{
    public class DataLayer
    {
        public int AssignCollectorFromUsers()
        {
            if (new DataAccess().ExecuteNonQuery("AVZ_COL_spAssignCollections", new {}))
             { }
            return 1;

        }
    }
}
