using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Bridge
{
    public class Logger
    {
        public static void LogMessage(string msg)
        {
            string dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string logFileName = DateTime.Now.ToString("MMM-dd-yyyy") + ".txt";
            try
            {
                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(Path.Combine(dirName, logFileName)))
                {
                    string logLine = System.String.Format("{0:G}: {1}.", System.DateTime.Now, msg);
                    sw.WriteLine(logLine);
                }
            }
            finally
            {
            }
        }
    }
}