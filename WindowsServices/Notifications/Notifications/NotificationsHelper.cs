using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using glib.Email;
using System.Net.Mail;
using System.IO;
using Pecuniaus.ServiceContract;
using System.Collections.Specialized;
using System.Configuration;

namespace Notifications
{
    public class NotificationsHelper:IWindowsService
    {
        NameValueCollection AppSettings = ConfigurationManager.GetSection("Pecuniaus.Notifications") as NameValueCollection;
        private int _timerInetrval;
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        RestClient Client = null;
        public NotificationsHelper()
        {
            Client = new RestClient(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
            if (AppSettings.Get("TimeInterval") != null)
            {
                _timerInetrval = Convert.ToInt16(AppSettings.Get("TimeInterval"));
            }
            else
            {
                _timerInetrval = 1000;
            }
        }
        public void ProcessNotifications()
        {
            MimeReader mime = new MimeReader();
            SmtpClient client = new SmtpClient();
            try
            {                
                var request = new RestRequest("notification/retrieve", Method.GET);
                logger.Log(NLog.LogLevel.Info, "<br/><font color=Orange>Send notifications starts........" + DateTime.Now.ToString());
                IRestResponse<List<NotificationModel>> response = Client.Execute<List<NotificationModel>>(request);

                if (response!=null&& response.Data!=null)
                {


                    foreach (NotificationModel not in response.Data)
                    {
                        if (File.Exists(not.NotificationFileName))
                        {
                            logger.Log(NLog.LogLevel.Info, "<br/><font color=Orange>Indivivdual notifications starts........");
                            RxMailMessage mm = mime.GetEmail(not.NotificationFileName);
                            client.Send(mm);

                            File.Delete(not.NotificationFileName);

                            request = new RestRequest("notification/update", Method.PUT);
                            request.RequestFormat = DataFormat.Json;
                            request.AddBody(new NotificationModel() { NotificationQueueId = not.NotificationQueueId, NotificationStatus = "2" });
                            var response2 = Client.Execute(request);
                        }
                    }
                }
                else
                {
                    logger.Log(NLog.LogLevel.Info, "<br/><font color=Orange>No response found for notifications");
                }
            }
            catch(Exception ex)
            {
                logger.Log(NLog.LogLevel.Info, "<br/><font color=Red>"+ ex.StackTrace);
                throw;
            }
            finally
            {
                client.Dispose();
            }
        }

        public void MessageLog(string message)
        {
           // throw new NotImplementedException();
        }

        public void ProcessRequest()
        {
            ProcessNotifications();
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
    }
}
