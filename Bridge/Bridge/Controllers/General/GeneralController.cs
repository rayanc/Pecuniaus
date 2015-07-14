using Bridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.BusinessTier;

namespace Bridge.Controllers
{
    [RoutePrefix("gen")]
    public class GeneralController : ApiController
    {

        [Route("retrieve/{type}")]
        public HttpResponseMessage GetDocumentTypes(int type)
        {
            using (GenerelService generalservice = new GenerelService())
            {
                IList<GeneralModel> GeneralModellist = generalservice.FindBy(type);
                if (GeneralModellist != null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, GeneralModellist);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, new { });
                }

            }
        }


        [Route("setsnooze")]
        [HttpPost]
        public HttpResponseMessage SetSnooze([FromBody] SnoozeModel model)
        {
            using (GenerelService generalservice = new GenerelService())
            {
                generalservice.SetSnooze(model.contractId, model.snoozePercent, model.snoozeDate);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { });
            }
           

        }
        [Route("kickback")]
        [HttpPost]
        public HttpResponseMessage KickbackContracts(Int64 merchantId,Int64 tasktypeId,Int64 workflowId,Int64 contractid)
        {
            using (GenerelService generalservice = new GenerelService())
            {
                generalservice.Kickback(merchantId,tasktypeId,workflowId,contractid);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { });
            }


        }


    }
}