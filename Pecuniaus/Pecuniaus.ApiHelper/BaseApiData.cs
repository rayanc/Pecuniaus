using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pecuniaus.ApiHelper
{
    public static class BaseApiData
    {
        private static readonly Uri BaseAddress;

        static BaseApiData()
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURI"]);
        }

        public static T GetAPIResult<T>(string methodAndQuery, Func<T> ctor) where T : class
        {
            T item = ctor();
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var response = client.GetAsync(methodAndQuery).Result;
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    item = response.Content.ReadAsAsync<T>().Result;
                }
            }
            return item;
        }

        public static Task<T> GetAPIResultAsync<T>(string methodAndQuery, Func<T> ctor) where T : class
        {
            T item = ctor();
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var response = client.GetAsync(methodAndQuery).Result;
                return response.Content.ReadAsAsync<T>();
            }
        }

        public static HttpResponseMessage PutAPIData(string methodAndQuery, object obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                return client.PutAsJsonAsync(methodAndQuery, obj).Result;
            }
        }

        /// <summary>
        /// It is being used in many places
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodAndQuery"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage PutAPIData<T>(string methodAndQuery, T obj) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                return client.PutAsJsonAsync(methodAndQuery, obj).Result;
            }
        }

        /// <summary>
        /// It is being used in many places
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodAndQuery"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage PostAPIData<T>(string methodAndQuery, T obj) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                return client.PostAsJsonAsync(methodAndQuery, obj).Result;
            }
        }

        public static HttpResponseMessage PostAPIData(string methodAndQuery, object obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                return client.PostAsJsonAsync(methodAndQuery, obj).Result;
            }
        }

        public static HttpResponseMessage DeleteAPIData(string methodAndQuery)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                return client.DeleteAsync(methodAndQuery).Result;
            }
        }

    }
}
