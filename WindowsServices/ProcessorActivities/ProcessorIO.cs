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
namespace Processor
{
    public class ProcessorActivities : IWindowsService
    {
        #region Variable
        NameValueCollection AppSettings = ConfigurationManager.GetSection("Pecuniaus.ProcessorActivities") as NameValueCollection;
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private int _timerInetrval;
        public ProcessorActivities()
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
        /// Reads a processor file
        /// </summary>
        private void ReadPAF()
        {
            // Read sample data from CSV file
            
            string pathtoRead = System.Configuration.ConfigurationManager.AppSettings["ftpPathReadProcessorActivity"].ToString();
            logger.Log(NLog.LogLevel.Info, "Reading of the files starts........."+ pathtoRead);
            string[] files = System.IO.Directory.GetFiles(pathtoRead, "*.paf");
            logger.Log(NLog.LogLevel.Info, "<br/>Number of files  to be read ..." +files.Count());
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
                    writer.WriteStartElement("processor");
                    while (reader.ReadRow(row))
                    {
                        if (count > 0)
                        {
                            /*
                                 * [0]- Processor Processed date,* [1]-AMI Processor Code* [2]- Processor Merchant Number,* [3]-Retrieval source
                                 * [4]-Total Amount,* [5]-Withdrawal Amount,* [6]-Activity type,* [7]-(Specified Percentage) Rate,* [8]-Start date
                                 * [9]-End date,* [10]-Processor Processed Date,* [11]-Processor Merchant Category
                                 * [12]-Processor Merchant Status, * [13]-Processor Merchant Business Start Date
                                 * [14]-Legal Company Name,* [15]-RNC,* [16]-Bank Account Number,* [17]-Authorized Owner Name
                                 * */
                            if (count==0)
                            {
                                string header = Convert.ToString(row[0]);
                                string processorDate = Convert.ToString(row[1]);
                                string amiprocessorcode = Convert.ToString(row[2]);
                                if (string.IsNullOrEmpty(header) || string.IsNullOrEmpty(processorDate) || string.IsNullOrEmpty(amiprocessorcode))
                                {
                                    logger.Log(NLog.LogLevel.Info, "Missing required paramters");
                                    break;
                                }

                            }

                            if (row.Count > 6)
                            {

                                try
                                {
                                    DateTime processedDate;
                                    processedDate = DateTime.ParseExact(Convert.ToString(row[0]), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    writer.WriteStartElement("processoractivity");
                                    writer.WriteAttributeString("processedDate", Convert.ToString(row[0]));
                                    writer.WriteAttributeString("processorCode", row[1]);
                                    writer.WriteAttributeString("pMerchantNumber", Convert.ToString(row[2]));
                                    writer.WriteAttributeString("retrievalSource", Convert.ToString(row[3]));
                                    writer.WriteAttributeString("totalAmount", Convert.ToString(row[4]));
                                    writer.WriteAttributeString("withdrawalAmount", Convert.ToString(row[5]));
                                    //Activity type 70 Withdrawal and 20 Adjustment
                                    writer.WriteAttributeString("activityType", Convert.ToString(row[6]));
                                    //Percent withheld. (1.00 means 100%, 0.50 means 50% and so on)
                                    writer.WriteAttributeString("specifiedRate", Convert.ToString(row[7]));
                                    writer.WriteAttributeString("startDate", Convert.ToString(row[8]));
                                    writer.WriteAttributeString("endDate", Convert.ToString(row[9]));
                                    writer.WriteEndElement();
                                }
                                catch (Exception ex)
                                {
                                    logger.Log(NLog.LogLevel.Info, "Exception " + ex.ToString() + "<br />" + ex.StackTrace + "<br/>" + Convert.ToString(row[0]));
                                    break;
                                }
                                logger.Log(NLog.LogLevel.Info, "Finished reading row");
                            }
                        }
                        count++;
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                   new DataLayer().InsertProcessorActivities(sbXml.ToString());
                }
                #endregion
                MovetoArchive(item);
            }
        }

        private void MovetoArchive(string filePath)
        {
            try
            {
                string strPath = new FileInfo(filePath).Name;
                logger.Log(NLog.LogLevel.Info, "<br/><font color=green>Move PAF to archive starts to path ........" + System.Configuration.ConfigurationManager.AppSettings["PAFArchive"].ToString() + "......" + DateTime.Now.ToString());
                File.Move(filePath, Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PAFArchive"].ToString(), strPath));
            }
            catch (Exception ex)
            {

                logger.Log(NLog.LogLevel.Error, ex.StackTrace);
            }
        }

        private void UpdateCollectionValues()
        {
            logger.Log(NLog.LogLevel.Info, "UpdateCollectionValues");
            new DataLayer().UpdateCollectionValues();
        }
        public void MessageLog(string message)
        {
            throw new NotImplementedException();
        }

        public void ProcessRenewals()
        {
            try
            {   //Process funded contract for Renewals
                new DataLayer().ProcessRenewals();
            }
            catch
            { }
        }

        public void ProcessRequest()
        {
            ReadPAF();
            UpdateCollectionValues();
            ProcessRenewals();
        }

    }
}
