
To run a functionality under this service
1.create DLL implement ServiceContract
	e.g
	public class DeleteRecording : ServiceContract.IWindowsService 
	 {
        #region IWindowsService Members

        void ServiceContract.IWindowsService.MessageLog(string message)
        {
            throw new NotImplementedException();
        }

        void ServiceContract.IWindowsService.ProcessRequest()
        {
            throw new NotImplementedException();
        }

        int ServiceContract.IWindowsService.TimerInterval
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    	}
	
===================================================================================
	a)To read config values define a <configSections> and get values from it.
	
	e.g
	
	System.Collections.Specialized.NameValueCollection config = (System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("Pecuniaus.DeleteRecording");
        string logPath = config["LogFilePath"];
           


2. put the dll in 'Include'  folder in the ServiceManager.
	 

	add <configSections> under <configuration> in config file
	  <configSections>
        	<section name ="Pecuniaus.DeleteContent" type="System.Configuration.NameValueSectionHandler,System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
    	</configSections>

    <Pecuniaus.DeleteContent>
        <add key = "LogFilePath" value = "D:\Logs\Pecuniaus_ServiceManager\Pecuniaus.DeleteContentLogs"/>
    </Pecuniaus.DeleteContent>


