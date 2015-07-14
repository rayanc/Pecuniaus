using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.DataAccess;
namespace ProcessTasks
{
    public class DataLayer
    {
        public void ProcessTasks()
        {
            new DataAccess().ExecuteNonQuery("AVZ_TSK_spAssignTasks_1", null);

        }

       
    }
}
