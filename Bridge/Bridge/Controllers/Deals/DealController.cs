using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;

namespace Bridge.Controllers
{
    [RoutePrefix("deals")]
    public class DealController : ApiController
    {
       [HttpGet]
        public HttpResponseMessage RetrieveMerchants(string userid)
        {
            return null;
        }
    }
}
