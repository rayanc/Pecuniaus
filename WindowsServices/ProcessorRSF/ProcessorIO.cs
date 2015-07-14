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
        private void ReadRSF()      
        {
            // Read sample data from CSV file

            string pathtoRead = System.Configuration.ConfigurationManager.AppSettings["ftpPathReadProcessorRSF"].ToString();
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

                            if (row.Count > 4)
                            {

                                try
                                {
                                    DateTime processedDate;
                                    processedDate = DateTime.ParseExact(Convert.ToString(row[0]), "yyyyMMdd", CultureInfo.InvariantCulture);


                                    writer.WriteStartElement("processoractivity");
                                    writer.WriteAttributeString("processedDate", Convert.ToString(row[0]));
                                    writer.WriteAttributeString("processorCode", row[1]);
                                    writer.WriteAttributeString("pMerchantId", Convert.ToString(row[2]));
                                    writer.WriteAttributeString("pMerchantNumber", Convert.ToString(row[2]));
                                    writer.WriteAttributeString("balance", Convert.ToString(row[3]));
                                    writer.WriteAttributeString("rate", Convert.ToString(row[4]));
                                    writer.WriteAttributeString("terminal", Convert.ToString(row[5]));
                                    //Set up status 0 Suucessfull,1- failed,2-No change
                                    writer.WriteAttributeString("setupstatus", Convert.ToString(row[6]));
                                    //Percent withheld. (1.00 means 100%, 0.50 means 50% and so on)
                                    writer.WriteAttributeString("info", Convert.ToString(row[7]));
                                    
                                    writer.WriteEndElement();
                                    logger.Log(NLog.LogLevel.Info, "Finished reading row");
                                }
                                catch (Exception ex)
                                {
                                    logger.Log(NLog.LogLevel.Info, "Exception "  + ex.ToString());
                                    break;
                                }
                            }
                        }
                        count++;
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                   new DataLayer().InsertProcessorResponsesetup(sbXml.ToString());
                }
                #endregion
            }
        }

        public void MessageLog(string message)
        {
            //throw new NotImplementedException();
        }
       

        public void ProcessRequest()
        {
            ReadRSF();
            
           
        }


    }
}
