using Pecuniaus.UICore.Controllers;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Pecuniaus.Web.Controllers
{
    public class BaseController : UiBaseController
    {
        public const int WorkflowID = 1;
        public const long MerchantID = 1000;
        public const long ContractID = 1000;
        public T GetAPIResult<T>(string methodAndQuery, Func<T> ctor) where T : class
        {
            T item = ctor();
            using (var client = new HttpClient())
            {
                string baseAddress = ConfigurationManager.AppSettings["APIURI"];
       
                client.BaseAddress = new Uri(baseAddress);
                var response = client.GetAsync(methodAndQuery).Result;
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    item = response.Content.ReadAsAsync<T>().Result;
                }
            }
            return item;
        }

        public void PutAPIData<T>(string methodAndQuery, T obj) where T : class
        {
            using (var client = new HttpClient())
            {
                string baseAddress = ConfigurationManager.AppSettings["APIURI"];

                client.BaseAddress = new Uri(baseAddress);
                var response = client.PutAsJsonAsync(methodAndQuery, obj).Result;
            }
        }

        public void PostAPIData<T>(string methodAndQuery, T obj) where T : class
        {
            using (var client = new HttpClient())
            {
                string baseAddress = ConfigurationManager.AppSettings["APIURI"];

                client.BaseAddress = new Uri(baseAddress);
                var response = client.PostAsJsonAsync(methodAndQuery, obj).Result;
            }
        }
       
    }
}