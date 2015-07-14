/**********************************************************************
/// <summary>
// Name of File     : Contents.cs
// Author           : Anchit Saroch
// Class Definition : 
// Version: 1.0
// Create date      : 16 february, 2012
// 
// Modification log:
// Date(mm/dd/yyyy) 	Author: 	    Change Request #	    Description       
///</summary>
************************************************************************/
using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.Text;
using Bridge.Utility;

namespace Bridge.Utility
{
    public class Email
    {

        /// <summary>
        /// Path of Html Templates Folder
        /// </summary>
        public static string EmailTemplatesFolder
        {
            get
            {
                return HttpContext.Current.Request.MapPath("~/EmailTemplates");
            }
        }

        /// <summary>
        /// Get Contents
        /// </summary>
        /// <param name="emailTemplate">Email Template</param>
        /// <returns></returns>
        /// use this method in UI
        /// StringBuilder body = new StringBuilder();
        /// body.Append(Contents.GetEmailContents(EmailTemplates.CollectionLetter));
        /// body.Replace("$$UserName$$", "abcd");
        /// body.Replace("$$Password$$", "1233");
        public static string GetEmailContents(Utilities.EmailTemplates emailTemplate)
        {
            string fileName = string.Empty;
            switch (emailTemplate)
            {
                case Utilities.EmailTemplates.CollectionLetter:
                    fileName = "CollectionLetter.html";
                    break;                
            }
            return GetContents(fileName);
        }


        /// <summary>
        /// Get Contents
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetContents(string fileName)
        {
            string contentFilePath;
            string fileContents = string.Empty;
            if (CheckAndGetFileName(EmailTemplatesFolder, fileName, out contentFilePath))
                fileContents = File.ReadAllText(contentFilePath);
            return fileContents;
        }


        /// <summary>
        /// Check And Get FileName
        /// </summary>
        /// <param name="templateFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool CheckAndGetFileName(string templateFolder, string fileName, out string filePath)
        {
            filePath = Path.Combine(templateFolder, fileName);
            if (File.Exists((filePath)))
                return true;
            return false;
        }

        /// <summary>
        /// To Send Email -- need to send email through below code - else best is to send email from database
        /// No reference of this method currently
        /// </summary>
        /// <returns></returns>        
        public static bool SendEmail(string to, string subject, StringBuilder body, string fromEmail = null)
        {
            MailMessage email = null;
            SmtpClient objSmtpClient = null;
            try
            {
                email = new MailMessage();
                objSmtpClient = new SmtpClient();
                if (!string.IsNullOrEmpty(fromEmail))
                {
                    email.From = new MailAddress(fromEmail);
                }
                email.Subject = subject;
                if (string.IsNullOrEmpty(to))
                    return false;
                string[] emailarray = to.Split(',');
                foreach (string t in emailarray)
                {
                    email.To.Add(t);
                }
                email.Body = body.ToString();
                email.IsBodyHtml = true;              
                objSmtpClient.Send(email);
                return true;
            }
            catch (Exception ex)
            {
               return false;
            }
            finally
            {
                email = null;
                objSmtpClient = null;
            }
        }
    }
}
