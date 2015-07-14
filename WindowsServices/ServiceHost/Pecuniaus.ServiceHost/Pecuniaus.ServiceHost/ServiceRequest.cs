using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Timers;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;


namespace Pecuniaus.ServiceHost
{
    public class ServiceRequest
    {
        #region [ Decalration of variables ]
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        NameValueCollection ServiceHostAppSettings = ConfigurationManager.GetSection("ServiceHost.appSettings") as NameValueCollection;


        #endregion

        public ServiceRequest()
        {
            
        }
        public void ProcessServiceRequest()
        {
            //ExecutionMode 
            //1:Sequential 
            //2:Parallel

            short executionMode = 2;
            if(ServiceHostAppSettings["ExecutionMode"]!=null)
                executionMode=Convert.ToInt16(ServiceHostAppSettings["ExecutionMode"]);

            List<Assembly> allAssemblies = new List<Assembly>();

           string includePath = System.IO.Path.GetDirectoryName(
               System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\include";

            string sequencialXmlFilePath = System.IO.Path.GetDirectoryName(
               System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\sequential.xml";

          string path = ServiceHostAppSettings["AssemblyFolderPath"];

          foreach (string dll in Directory.GetFiles(path, "*.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));

            if (executionMode == 2)
            {
                foreach (Assembly assembly in allAssemblies)
                {
                    foreach (Type t in assembly.GetTypes())
                    {
                        if (t.GetInterface("IWindowsService") != null)
                        {
                            ServiceTimer serviceTimer = new ServiceTimer();
                            serviceTimer.Timer = new Timer();
                            serviceTimer.IsBusy = false;

                            Pecuniaus.ServiceContract.IWindowsService executor = Activator.CreateInstance(t) as Pecuniaus.ServiceContract.IWindowsService;

                            serviceTimer.Timer.Elapsed += new ElapsedEventHandler(delegate(object source, ElapsedEventArgs e)
                        {
                            if (serviceTimer.IsBusy)
                            {
                                logger.Log(NLog.LogLevel.Info, "Service Timer Busy for the assembly =" + t.FullName + " for dll= " + t.Module.Name);

                                executor.MessageLog("Service Timer Busy for the assembly =" + t.FullName + " for dll= " + t.Module.Name);
                            }
                            else
                            {
                                try
                                {
                                    serviceTimer.IsBusy = true;
                                    logger.Log(NLog.LogLevel.Info, "Timer started for assembly=" + t.FullName + " for dll= " + t.Module.Name);
                                    executor.ProcessRequest();
                                    serviceTimer.IsBusy = false;
                                }
                                catch (Exception ex)
                                {
                                    serviceTimer.IsBusy = false;
                                    executor.MessageLog(ex.ToString());
                                }
                            }
                        }
                        );
                            serviceTimer.Timer.Interval = executor.TimerInterval * 1000;
                            serviceTimer.Timer.Enabled = true;


                        }
                    }
                }
            }
            else
            {
                ServiceTimer sequentialTimer = new ServiceTimer();
                sequentialTimer.Timer = new Timer();
                sequentialTimer.IsBusy = false;

                XmlDocument doc = new XmlDocument();
                //doc.Load(ServiceHostAppSettings["SequencialXmlFilePath"].ToString());
                doc.Load(sequencialXmlFilePath);
                XmlNodeList nodeList = doc.SelectNodes("/Sequential/assembly");
                Dictionary<string, int> assemblyList = new Dictionary<string, int>();

                foreach (XmlNode node in nodeList)
                {
                    assemblyList.Add(node.ChildNodes[1].InnerText, Convert.ToInt32(node.ChildNodes[0].InnerText));
                }

                SortedList sortedCollection = new SortedList();
                foreach (Assembly assembly in allAssemblies)
                {
                    foreach (Type t in assembly.GetTypes())
                    {
                        if (t.GetInterface("IWindowsService") != null)
                        {
                            Pecuniaus.ServiceContract.IWindowsService executor = Activator.CreateInstance(t) as Pecuniaus.ServiceContract.IWindowsService;
                            if (assemblyList.ContainsKey(t.Module.Name))
                            {
                                sortedCollection.Add(assemblyList[t.Module.Name], executor);
                            }
                        }
                    }
                }

                sequentialTimer.Timer.Elapsed += new ElapsedEventHandler(delegate(object source, ElapsedEventArgs e)
                {
                    IDictionaryEnumerator ide;
                    Pecuniaus.ServiceContract.IWindowsService executorItem;
                    if (sequentialTimer.IsBusy)
                    {
                        logger.Log(NLog.LogLevel.Info, "Service Sequential Timer is busy ");

                        ide = sortedCollection.GetEnumerator();
                        //you to iterate and get all the key value pairs. 
                        foreach (DictionaryEntry key in sortedCollection)
                        {
                            executorItem = (Pecuniaus.ServiceContract.IWindowsService)key.Value;
                            executorItem.MessageLog("Service Sequential Timer is busy");
                        }
                    }
                    else
                    {
                        sequentialTimer.IsBusy = true;
                        //iterate thru the SortedList object 
                        ide = sortedCollection.GetEnumerator();
                        //you to iterate and get all the key value pairs. 
                        foreach (DictionaryEntry key in sortedCollection)
                        {
                            executorItem = (Pecuniaus.ServiceContract.IWindowsService)key.Value;
                            try
                            {
                                executorItem.ProcessRequest();
                            }
                            catch (Exception ex)
                            {
                                executorItem.MessageLog(ex.ToString());
                            }
                        }
                        sequentialTimer.IsBusy = false;
                    }
                }
                );
                int seqTimer = 60000;//1 min default
                if (ServiceHostAppSettings["tmrSequential"] != null)
                {
                    seqTimer = Convert.ToInt32(ServiceHostAppSettings["tmrSequential"]) * 1000;
                }
                sequentialTimer.Timer.Interval = seqTimer;
                sequentialTimer.Timer.Enabled = true;
            }
        }
    }
}
