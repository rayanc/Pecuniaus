using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Text;
using Pecuniaus.Web.Models;
using Newtonsoft.Json;

namespace Pecuniaus.Web.HelperClasses
{
    public class CommonFunctions
    {
        public string ReturnJSONFromAPICall(string MethodRoute, string RequestType)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
            objRequest.Method = RequestType;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string JsonString = reader.ReadToEnd();
            }
            return "";
        }

        public IList<GeneralModel> RetrieveGeneralTypes(string query)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "gen/retrieve/" + query);
            objRequest.Method = "Get";

            IList<GeneralModel> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<GeneralModel>>(reader.ReadToEnd());
            }

            return objBE;
        }

        public IList<GeneralModel> RetrieveNotes(string query)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "gen/retrieve/" + query);
            objRequest.Method = "Get";

            IList<GeneralModel> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<GeneralModel>>(reader.ReadToEnd());
            }

            return objBE;
        }

        public IList<SalesRepresentativeModel> RetrieveSalesRep(string query)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "Sales?searchString=" + query);
            objRequest.Method = "Get";

            IList<SalesRepresentativeModel> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<SalesRepresentativeModel>>(reader.ReadToEnd());
            }

            return objBE;
        }
    }
}