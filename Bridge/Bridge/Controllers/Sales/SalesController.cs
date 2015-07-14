using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models;
using Bridge.BusinessTier;

namespace Bridge.Controllers
{
    [RoutePrefix("sales")]
    public class SalesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage RetriveSales(string searchString)
        {
            IList<SalesRepresentativeModel> response;
            using (SalesTier mt = new SalesTier())
            {
                response = mt.RetrieveSalesRep(searchString);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
    }
}
