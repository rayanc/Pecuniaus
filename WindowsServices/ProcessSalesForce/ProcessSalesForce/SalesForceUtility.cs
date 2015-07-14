using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pecuniaus.ServiceContract;

namespace ProcessSalesForce
{
   public class SalesForceUtility:IWindowsService
    {
        NameValueCollection AppSettings = ConfigurationManager.GetSection("Pecuniaus.SalesForce") as NameValueCollection;
        private int _timerInetrval;
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public SalesForceUtility()
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
       /// <summary>
       /// Retrive the merchants and new lead from the sales force apis
       /// </summary>
       public  void RetrieveLeads()
       {
          new SalesForce().RetrieveSalesForceAccount();
          

       }
       /// <summary>
       /// Updates the Sales force for the merchant details
       /// </summary>
       public void UpdateSalesForceMerchantDetails()
       {
           new SalesForce().UpdateSalesForceAccount();


       }
       /// <summary>
       /// Update the offers from the system to the sales force 
       /// </summary>
       public void UpdateSalesForceOffersDetails()
       {
           new SalesForce().UpdateMerhantsOffertoSalesForce();


       }

       public void MessageLog(string message)
       {
           //throw new NotImplementedException();
           
       }

       public void ProcessRequest()
       {
          RetrieveLeads();
           UpdateSalesForceMerchantDetails();
          UpdateSalesForceOffersDetails();
       }

       
    }
}
