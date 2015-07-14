using Pecuniaus.ApiHelper;
using Pecuniaus.Contract.Models;
using Pecuniaus.Models;
using Pecuniaus.UICore.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Pecuniaus.Contract.Controllers
{
    public class BaseController : UiBaseController
    {
        public const int WorkflowID = 2;

        protected IEnumerable<SelectListItem> GetIndustryTypes()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.Industry);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetTypeOfAdvances(string selected="" )
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.Advances);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString(),
                       Selected = selected == c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetProvince()
        {
            var docTypes = CommonFunctions.GetStates();

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetProcessors()
        {
            var docTypes = CommonFunctions.GetProcessors();
            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetPropertyTypes()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.PropertyType);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetBusinessTypes()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.BusinessEntity);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        public IList<SalesRepresentativeModel> RetrieveSalesRep()
        {
            string query = "";
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


        public string GetLLScriptFilePath()
        {
            return "~/Uploads/LandLordCall/";
        }
    }
}