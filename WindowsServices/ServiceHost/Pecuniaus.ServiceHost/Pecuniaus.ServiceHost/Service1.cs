using System.ServiceProcess;
using System.Configuration;

namespace Pecuniaus.ServiceHost
{
    public partial class Service1 : ServiceBase
    {
        #region [ Decalration of variables ]
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        #endregion

        public Service1()
        {


            ///for debug
            logger.Log(NLog.LogLevel.Info, "Service Started");
            ServiceRequest obj = new ServiceRequest();
            obj.ProcessServiceRequest();
            ///for debug
            ///

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            
            logger.Log(NLog.LogLevel.Info, "Service Host Started");

            ServiceRequest obj = new ServiceRequest();
            obj.ProcessServiceRequest();

        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            logger.Log(NLog.LogLevel.Info, "Service Host Stopped");

        }
    }
}
