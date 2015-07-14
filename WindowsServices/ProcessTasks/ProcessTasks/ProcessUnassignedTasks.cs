using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pecuniaus.ServiceContract;

namespace ProcessTasks
{
    /// <summary>
    /// Motive is to fetch the unassigned tasks and process them so that they are assigned to a particular user based on the availability
    /// </summary>
    public class ProcessUnassignedTasks : IWindowsService
    {
        NameValueCollection AppSettings = ConfigurationManager.GetSection("Pecuniaus.ProcessTask") as NameValueCollection;
        private int _timerInetrval;
        public ProcessUnassignedTasks()
        {

            if (AppSettings.Get("TimeInterval") != null)
            {
                _timerInetrval = Convert.ToInt16(AppSettings.Get("TimeInterval"));
            }
            else
            {
                _timerInetrval = 1000;
            }

        }
        public int TimerInterval
        {
            get
            {
                return _timerInetrval;
            }
            set
            {
                _timerInetrval = value;
            }
        }
        public void AssignTasks()
        {
            // Get a list of unassigned tasks from the db
            new DataLayer().ProcessTasks();
        }       

        public void MessageLog(string message)
        {
            //throw new NotImplementedException();
        }

        public void ProcessRequest()
        {
            AssignTasks();            
        }


    }
}
