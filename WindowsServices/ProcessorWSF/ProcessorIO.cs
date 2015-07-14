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
        NameValueCollection AppSettings = ConfigurationManager.GetSection("Pecuniaus.ProcessorWSF") as NameValueCollection;
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
      
        //private string _filePath;
      
        #endregion
        private void CreateWSF()
        {
            string dateTime = DateTime.Now.ToString("yyyyMMdd");
            string requestedDate = DateTime.Now.ToString("yyMMdd");
            //_filePath = System.Configuration.ConfigurationManager.AppSettings["ftpPathWriteProcessorWSF"].ToString();
            string folderPath = System.Configuration.ConfigurationManager.AppSettings["ftpPathWriteProcessorWSF"].ToString();
            double totalBalance = 0d;
            string processorCode = string.Empty;
            Int64 rowCount = 0;

            //Get the list of datasource to be processed
            DataSet ds = new DataLayer().RetrieveWSFInformation();
            logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Creation  of the WSF files starts........." + DateTime.Now.ToString());
            //Get the list of processors to process the data file
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {

                    processorCode = ds.Tables[1].Rows[i]["processorId"].ToString();

                    #region Process the files
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rowCount = ds.Tables[0].Rows.Count;
                        //_filePath = string.Concat(processorCode.Substring(0, 2), requestedDate.Substring(0, 6), ".wsf");
                        string fileName= string.Concat(processorCode.Substring(0, 2), requestedDate.Substring(0, 6), ".wsf");
                    
                    //    using (MRFWriter writer = new MRFWriter(_filePath))
                        using (MRFWriter writer = new MRFWriter(Path.Combine(folderPath, fileName)))
                        {
                            ProcessorRow rowHeader = new ProcessorRow();
                            rowHeader.Add(String.Format("HEADER"));
                            rowHeader.Add(String.Format(dateTime));
                            rowHeader.Add(String.Format(processorCode));
                            writer.WriteRow(rowHeader);
                            for (int count = 0; count <= rowCount - 1; count++)
                            {
                                /// Need to add logic to create the file based on the values from the file/db
                                ProcessorRow row = new ProcessorRow();
                                row.Add(String.Format(dateTime));
                                row.Add(String.Format(processorCode));
                                row.Add(String.Format(ds.Tables[0].Rows[count]["merchantId"].ToString()));
                                row.Add(String.Format(ds.Tables[0].Rows[count]["processorNumber"].ToString()));
                                row.Add(String.Format(ds.Tables[0].Rows[count]["balance"].ToString()));
                                row.Add(String.Format(ds.Tables[0].Rows[count]["rate"].ToString()));
                               totalBalance+=Convert.ToDouble(ds.Tables[0].Rows[count]["balance"].ToString());
                                writer.WriteRow(row);
                            }
                            ProcessorRow rowTrailer = new ProcessorRow();
                            rowTrailer.Add(String.Format("TRAILER"));
                            rowTrailer.Add(String.Format(dateTime));
                            rowTrailer.Add(String.Format(processorCode));
                            rowTrailer.Add(String.Format(rowCount.ToString()));
                            rowTrailer.Add(String.Format(totalBalance.ToString()));
                            writer.WriteRow(rowTrailer);
                 
                        }
                    }

                }
                    #endregion
                logger.Log(NLog.LogLevel.Info, "<br/><font color=red>Reading of the files Ends........." + DateTime.Now.ToString());
            }
        }


        public void MessageLog(string message)
        {
            //throw new NotImplementedException();
        }
       

        public void ProcessRequest()
        {

            CreateWSF();
           
        }


    }
}
