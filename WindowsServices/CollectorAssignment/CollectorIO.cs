using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Collector;
using Bridge.DataAccess;
using System.Data;
using System.Globalization;
using System.Configuration;
using Pecuniaus.ServiceContract;
using System.Collections.Specialized;
namespace Collector
{
    public class CollectorAssignment : IWindowsService
    {
        #region Variable
        NameValueCollection AppSettings = ConfigurationManager.GetSection("Pecuniaus.CollectorAssignment") as NameValueCollection;
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private int _timerInetrval;
        public CollectorAssignment()
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
        #endregion

        #region Methods
        /// <summary>
        /// Reads a processor file
        /// </summary>
        private void AssignCollector()
        {            
            logger.Log(NLog.LogLevel.Info, "Assigning of Collector starts on timestamp: " + DateTime.Now);
            new DataLayer().AssignCollectorFromUsers();
            logger.Log(NLog.LogLevel.Info, "Assigning of Collector ends on timestamp: " + DateTime.Now);
        }
                
        public void MessageLog(string message)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region Main Method
        public void ProcessRequest()
        {
            AssignCollector();            
        }
        #endregion

    }
}
