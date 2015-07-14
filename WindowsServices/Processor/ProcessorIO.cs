using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Processor;
using Bridge.DataAccess;
using System.Data;
using System.Globalization;
using System.Configuration;
using Pecuniaus.ServiceContract;
using System.Collections.Specialized;
using System.IO;
using System.Net;
namespace Processor
{
    public class ProcessorIO : IWindowsService
    {
        #region Variable
        NameValueCollection AppSettings = ConfigurationManager.GetSection("Pecuniaus.Processor") as NameValueCollection;
        private int _timerInetrval;
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public ProcessorIO()
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

        private string _filePath;

        #endregion


        /// <summary>
        /// Writes a processor file
        /// </summary>
        private void CreateMRF()
        {
            try
            { 
            string dateTime = DateTime.Now.ToString("yyyyMMdd");
            string requestedDate = DateTime.Now.ToString("yyMMdd");

            string fileLocation = System.Configuration.ConfigurationManager.AppSettings["systemPathWrite"].ToString();
            string fileName = string.Empty;
            //fileLocation;
            string processorCode = string.Empty;
            string processorName = string.Empty;
            Int64 rowCount = 0;
            Int64 individualRows = 0;
            #region Load File

            //Get the list of datasource to be processed
            DataSet ds = new DataLayer().RetrieveProcessorQueue();
            logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Creation  of the MRF files starts........." + DateTime.Now.ToString());
            //Get the list of processors to process the data file
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    processorCode = ds.Tables[1].Rows[i]["processorId"].ToString();
                    processorName = ds.Tables[1].Rows[i]["name"].ToString();
                    #region Process the files
                    logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Total Rows Count in MRF files Creation........." + ds.Tables[0].Rows.Count.ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        fileName = string.Concat(processorCode.Substring(0, 2), requestedDate.Substring(0, 6), ".mrf");
                        rowCount = ds.Tables[0].Rows.Count;
                        _filePath = Path.Combine(fileLocation, fileName);
                        logger.Log(NLog.LogLevel.Info, "<br/><font color=red>File Path:   " + _filePath);
                        individualRows = 0;
                        using (MRFWriter writer = new MRFWriter(_filePath))
                        {
                            logger.Log(NLog.LogLevel.Info, "<br/><font color=red> Processor Code:   " + processorCode);
                            ProcessorRow rowHeader = new ProcessorRow();
                            rowHeader.Add(String.Format("HEADER"));
                            rowHeader.Add(String.Format(dateTime));
                            rowHeader.Add(String.Format(processorCode));
                            writer.WriteRow(rowHeader);
                            for (int count = 0; count <= rowCount - 1; count++)
                            {

                                if (processorCode == String.Format(ds.Tables[0].Rows[count]["ProcessorId"].ToString()))
                                {
                                    /// Need to add logic to create the file based on the values from the file/db
                                    ProcessorRow row = new ProcessorRow();
                                    row.Add(String.Format(dateTime));
                                    row.Add(String.Format(ds.Tables[0].Rows[count]["ProcessorId"].ToString()));
                                    row.Add(String.Format(ds.Tables[0].Rows[count]["merchantId"].ToString()));
                                    logger.Log(NLog.LogLevel.Info, "<br/><font color=red> Merchant Id :   " + String.Format(ds.Tables[0].Rows[count]["merchantId"].ToString()));
                                    row.Add(String.Format(ds.Tables[0].Rows[count]["processorNumber"].ToString()));

                                    writer.WriteRow(row);
                                    new DataLayer().UpdateProcessorQueue(Convert.ToInt64(ds.Tables[0].Rows[count]["merchantId"].ToString()), ds.Tables[0].Rows[count]["processorNumber"].ToString());
                                    individualRows++;
                                }
                            }
                            ProcessorRow rowTrailer = new ProcessorRow();
                            rowTrailer.Add(String.Format("TRAILER"));
                            rowTrailer.Add(String.Format(dateTime));
                            rowTrailer.Add(String.Format(processorCode));
                            rowTrailer.Add(String.Format(individualRows.ToString()));
                            writer.WriteRow(rowTrailer);


                        }

                        /* if (System.Configuration.ConfigurationManager.AppSettings["isftp"].ToString() == "1")
                         {
                             FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(Path.Combine(System.Configuration.ConfigurationManager.AppSettings["ftpPathWrite"].ToString(), fileName));
                             ftp.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["username"].ToString(), System.Configuration.ConfigurationManager.AppSettings["password"].ToString());
                             ftp.Method = WebRequestMethods.Ftp.UploadFile;
                             FileStream fs = File.OpenRead(_filePath);
                             byte[] buffer = new byte[fs.Length];
                             fs.Read(buffer, 0, buffer.Length);
                             fs.Close();

                             Stream ftpstream = ftp.GetRequestStream();
                             ftpstream.Write(buffer, 0, buffer.Length);
                             ftpstream.Close();
                         }*/
                    }
                }

                    #endregion

                   
                }
            
            }
            catch(Exception ex)
            {

                logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Error while creating MRF........." + ex.InnerException.ToString());
            }
            logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Creation  of the MRF Ends........." + DateTime.Now.ToString());
            #endregion
        }


        /// <summary>
        /// Reads a processor file
        /// </summary>
        private void ReadSPF()
        {
            try
            {

            
            string pathtoRead = string.Empty;
            string[] files;
            // Read sample data from CSV file
           /* if (System.Configuration.ConfigurationManager.AppSettings["isftp"].ToString() == "1")
            {
                logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Reading of the files from ftp starts........." + DateTime.Now.ToString());
                files = GetFilesfromFTP();
                logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Read  files from ftp server........." + files.Count() + "to be downloaded");
                foreach (string item in files)
                {
                    logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Downloading  files from ftp server........." + item + "");
                    DownloadFilesfromFTP(item);
                }
            }*/
            pathtoRead = System.Configuration.ConfigurationManager.AppSettings["systemPathRead"].ToString();
            files = System.IO.Directory.GetFiles(pathtoRead, "*.spf");


            logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Reading of the files starts........." + DateTime.Now.ToString());

            logger.Log(NLog.LogLevel.Info, "<br/>Total # of files........." + files.Count());
            foreach (var item in files)
            {
                #region Read files from a location
                using (SPFReader reader = new SPFReader(item))
                {

                    int count = 0;
                    //   double mccvBaseValue = 0D;
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.CloseOutput = true;
                    settings.OmitXmlDeclaration = false;
                    StringBuilder sbXml = new StringBuilder();
                    XmlWriter writer = XmlWriter.Create(sbXml, settings);

                    ProcessorRow row = new ProcessorRow();
                    writer.WriteStartElement("profiles");
                    while (reader.ReadRow(row))
                    {
                        if (count > 0)
                        {
                            /*
                                 * [0]- Merchant Number,* [1]-Processor Merchant Number* [2]- Processor Code,* [3]-Activity type
                                 * [4]-Retrieval source,* [5]-Currency,* [6]-Total Amount,* [7]-Total Tickets,* [8]-Start date
                                 * [9]-End date,* [10]-Processor Processed Date,* [11]-Processor Merchant Category
                                 * [12]-Processor Merchant Status, * [13]-Processor Merchant Business Start Date
                                 * [14]-Legal Company Name,* [15]-RNC,* [16]-Bank Account Number,* [17]-Authorized Owner Name
                                 * */

                            if (row.Count > 4)
                            {

                                try
                                {
                                    DateTime processedDate;
                                    processedDate = DateTime.ParseExact(Convert.ToString(row[10]), "yyyyMMdd", CultureInfo.InvariantCulture);


                                    writer.WriteStartElement("monthlyprofile");
                                    writer.WriteAttributeString("merchantId", Convert.ToString(row[0]));
                                    writer.WriteAttributeString("pMerchantNumber", Convert.ToString(row[1]));
                                    writer.WriteAttributeString("processorCode", row[2]);
                                    writer.WriteAttributeString("activityType", Convert.ToString(row[3]));
                                    writer.WriteAttributeString("retrievalSource", Convert.ToString(row[4]));
                                    writer.WriteAttributeString("currencyId", Convert.ToString(row[5]));
                                    writer.WriteAttributeString("totalAmount", Convert.ToString(row[6]));
                                    writer.WriteAttributeString("totalTickets", Convert.ToString(row[7]));
                                    writer.WriteAttributeString("startDate", Convert.ToString(row[8]));
                                    writer.WriteAttributeString("endDate", Convert.ToString(row[9]));
                                    writer.WriteAttributeString("processedDate", Convert.ToString(row[10]));
                                    writer.WriteAttributeString("month", Convert.ToString(processedDate.Month));
                                    writer.WriteAttributeString("year", Convert.ToString(processedDate.Year));


                                    writer.WriteAttributeString("processorMerchantCategory", Convert.ToString(row[11]) ?? string.Empty);
                                    if (Convert.ToString(row[12]) == "A")
                                    {
                                        writer.WriteAttributeString("processorMerchantStatus", "1");
                                    }
                                    else
                                    {
                                        writer.WriteAttributeString("processorMerchantStatus", "0");
                                    }

                                    writer.WriteAttributeString("processorMerchantBusinessStartDate", Convert.ToString(row[13]));
                                    writer.WriteAttributeString("legalName", row.ElementAtOrDefault(14));
                                    writer.WriteAttributeString("rnc", row.ElementAtOrDefault(15));
                                    writer.WriteAttributeString("bankAccountNumber", row.ElementAtOrDefault(16));
                                    writer.WriteAttributeString("authorizedOwnerName", row.ElementAtOrDefault(17));
                                    writer.WriteEndElement();
                                }
                                catch (Exception ex)
                                {

                                    logger.Log(NLog.LogLevel.Error, ex.StackTrace);
                                    break;
                                }
                            }
                        }
                        count++;
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                   
                   new DataLayer().InsertCreditProfiles(sbXml.ToString());
                   
                }

                #endregion
                logger.Log(NLog.LogLevel.Info, "<br/>Completed insertion of the data to the system</font>");
                MovetoArchive(item);
            }
            }
            catch (Exception ex)
            {

                logger.Log(NLog.LogLevel.Error, ex.StackTrace);
            }
        }

        private void MovetoArchive( string filePath)
        {
            try
            { 
                string strPath =new FileInfo(filePath).Name;
            logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Move to archive starts to path ........" + System.Configuration.ConfigurationManager.AppSettings["archivePath"].ToString()+"......" + DateTime.Now.ToString());
            File.Move(filePath, Path.Combine(System.Configuration.ConfigurationManager.AppSettings["archivePath"].ToString(),strPath));
            logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Monthly volumes calculation starts........" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {

                logger.Log(NLog.LogLevel.Error, ex.StackTrace);
            }
        }

        private void CopyFilestoProcessor(string filePath)
        {
            try
            {
                if (File.Exists(Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Scandocument"].ToString(), filePath)))
                {
                    string strPath = new FileInfo(filePath).Name;
                    logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Copy file starts........" + System.Configuration.ConfigurationManager.AppSettings["archivePath"].ToString() + "......" + DateTime.Now.ToString());
                    File.Copy(Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Scandocument"].ToString(), strPath), Path.Combine(System.Configuration.ConfigurationManager.AppSettings["systemPathWrite"].ToString(), strPath), true);
                }
                else
                {
                    logger.Log(NLog.LogLevel.Info, "<br/><font color=green>File doesnot exists" + filePath);
                }
            }
            catch (Exception ex)
            {

                logger.Log(NLog.LogLevel.Error, ex.StackTrace);
            }
        }
        
        public void MessageLog(string message)
        {
            //throw new NotImplementedException();
        }
        private void CalculateMOnthlyVolumes()
        {
            try
            {
            logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Calculate monthly volumes." + DateTime.Now.ToString());
            new DataLayer().CalculateCreditVolumes();
            logger.Log(NLog.LogLevel.Info, "<br/></font>");

            }
            catch (Exception ex)
            {

                logger.Log(NLog.LogLevel.Error, ex.StackTrace);
            }
        }

        public void ProcessRequest()
        {

            DateTime timeZone = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["TimeZone"]);//DateTime.UtcNow.AddHours(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeZone"].ToString()));
            if (DateTime.Now.Hour==timeZone.Hour && DateTime.Now.Minute==timeZone.Minute)
            {
                UploadScanDocuments();
                 logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Start creating the mrf files........" + DateTime.Now.ToString());
                 CreateMRF();
            }
                logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Start creating the mrf ends........" + DateTime.Now.ToString());

                 logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Start reading the spf files........" + DateTime.Now.ToString());
                 ReadSPF();
                 logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Read SPF ends ........" + DateTime.Now.ToString());

                 logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Monthly volumes calculation starts........" + DateTime.Now.ToString());
                 CalculateMOnthlyVolumes();
                 logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Monthly volumes calculation ends........" + DateTime.Now.ToString());            
        }
        private void UploadScanDocuments()
        {


            //Get the list of datasource to be processed
            DataSet ds = new DataLayer().RetrieveScanDocuments();
            logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Creation  of scan documents........." + DateTime.Now.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string fileName = ds.Tables[0].Rows[i]["fileName"].ToString();
                    CopyFilestoProcessor(fileName);
                   // DownloadFilesfromFTP();
                }
            }
         
        }
        private void DownloadFilesfromFTP(string file)
        {
            

                string pathtoRead = string.Empty;
                pathtoRead = System.Configuration.ConfigurationManager.AppSettings["ftpPathRead"].ToString();
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(pathtoRead);
                reqFTP.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["username"].ToString(), System.Configuration.ConfigurationManager.AppSettings["password"].ToString());
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;


                Uri serverUri = new Uri(pathtoRead);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return;
                }
               // reqFTP.KeepAlive = false;

                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
               // reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(string.Concat(System.Configuration.ConfigurationManager.AppSettings["systemPathWrite"].ToString(), "/", file), FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
            
            
        }
        private string[] GetFilesfromFTP()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            string pathtoRead = string.Empty;
            try
            {
                pathtoRead = System.Configuration.ConfigurationManager.AppSettings["ftpPathRead"].ToString();
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(pathtoRead);
                reqFTP.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["username"].ToString(), System.Configuration.ConfigurationManager.AppSettings["password"].ToString());
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');

            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }


    }
}
