
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pecuniaus.Utilities.Email
{
    public class Emailer
    {
        public void Send(string receiverEmail, string subject, string body, string attachmentFilePath)
        {
            string FileName = string.Empty;
            SmtpClient smtp = new SmtpClient();

            FileSystemWatcher fswWatcher = null;
            String sTempFileNameFromSmtpClient = null;
            string root, pickupRoot=string.Empty;

            try
            {
                if (smtp.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory && smtp.PickupDirectoryLocation.StartsWith("~"))
                {
                    root = AppDomain.CurrentDomain.BaseDirectory;
                    pickupRoot = smtp.PickupDirectoryLocation.Replace("~/", root);
                    pickupRoot = pickupRoot.Replace("/", @"\");
                    smtp.PickupDirectoryLocation = pickupRoot;
                }
                else if (smtp.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory && !smtp.PickupDirectoryLocation.StartsWith("~"))
                {
                    pickupRoot = smtp.PickupDirectoryLocation;
                }
                fswWatcher = new FileSystemWatcher(pickupRoot, "*.eml");
                fswWatcher.NotifyFilter = System.IO.NotifyFilters.FileName;
                fswWatcher.Created += new FileSystemEventHandler((sndr, fswEvntArgs) => sTempFileNameFromSmtpClient = fswEvntArgs.FullPath);
                
                fswWatcher.EnableRaisingEvents = true;

                MailMessage mm = new MailMessage();
                mm.To.Add(receiverEmail);

                mm.Body = body;
                mm.Subject = subject;
                mm.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(attachmentFilePath))
                    mm.Attachments.Add(new Attachment(attachmentFilePath));

                smtp.Send(mm);
                smtp.Dispose();

                fswWatcher.EnableRaisingEvents = false;

                if (!String.IsNullOrEmpty(sTempFileNameFromSmtpClient))
                {
                    FileName = sTempFileNameFromSmtpClient;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != fswWatcher)
                {
                    fswWatcher.EnableRaisingEvents = false;
                }
            }

            //return FileName;

            NotificationModel nm = new NotificationModel() { NotificationFileName=FileName };
            Uri BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage msg = client.PostAsJsonAsync("notification/add", nm).Result;
            }
        }

        //public static void SendEmailVerificationAdmin(string name, string login, string userEmail)
        //{
        //    EmailTemplate em = GetEmailTemplate("jjj.txt");

        //    if (em != null)
        //    {
        //         string body = em.Body.Replace("<!Name!>", name)
        //            .Replace("<!Login!>", login)
        //            .Replace("<!UserEmail!>", userEmail);
        //        SendEmail(adminEmail, em.Subject, body);
        //    }
        //}

        #region Helper Methods

        //private static EmailTemplate GetEmailTemplate(string fileName)
        //{
        //    EmailTemplate em = null;
        //    string filePath = HttpContext.Current.Server.MapPath("~/App_Data/EmailTemplates/" + fileName);
        //    if (File.Exists(filePath))
        //    {
        //        bool readSubject = true;
        //        string subject = string.Empty;
        //        StringBuilder body = new StringBuilder();

        //        string line;
        //        using (var reader = File.OpenText(filePath))
        //        {
        //            while ((line = reader.ReadLine()) != null)
        //            {
        //                if (readSubject)
        //                {
        //                    subject = line;
        //                    readSubject = false;
        //                }
        //                else
        //                {
        //                    body.Append(line);
        //                }
        //            }
        //        }
        //        return new EmailTemplate(subject, body.ToString());
        //    }
        //    return em;
        //}

        #endregion
    }
}
