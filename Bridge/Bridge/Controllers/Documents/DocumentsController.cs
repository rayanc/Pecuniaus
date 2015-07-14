using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models;
using Bridge.BusinessTier;

namespace Bridge.Controllers.Documents
{
    [RoutePrefix("documents")]
    public class DocumentsController : ApiController
    {
        #region Methods
        /// <summary>
        /// To retrieve all documents types related to a WorkflowId - Scan Document Screen
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route ("DocumentTypes")]
        public HttpResponseMessage ListDocumentTypes(int workflowId)
        {
            IList<DocumentsModel> response;
            using (DocumentTier dT = new DocumentTier())
            {
                response = dT.ListDocumentTypes(workflowId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// To retrieve documents related to a MerchantId and Document Type Id- Scan Document Screen
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <param name="documentTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RetriveDocument")]
        public HttpResponseMessage RetriveDocument(Int64 merchantId,Int64 contractId, int documentTypeId)
        {
            IList<DocumentsModel> response;
            using (DocumentTier dT = new DocumentTier())
            {
                response = dT.RetrieveDocuments(merchantId,contractId, documentTypeId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// TO Retrieve all documents for a Contract of a respective Merchant - Review Screen
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RetriveAllDocuments")]
        public HttpResponseMessage RetriveAllDocuments(Int64 merchantId, Int64 contractId)
        {
            IList<DocumentsModel> response;
            using (DocumentTier dT = new DocumentTier())
            {
                response = dT.RetrieveAllDocs(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// Update Document for a Merchant - Scan Document Screen
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateDocuments")]
        public HttpResponseMessage UpdateDocuments([FromBody] DocumentsModel model)
        {

            using (DocumentTier mt = new DocumentTier())
            {
                if (mt.UpdateDocument(model))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
        }

        /// <summary>
        /// To insert scanned document from Merchants Screen
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("InsertContDocument")]
        public HttpResponseMessage InsertContDocument([FromBody] DocumentsModel model)
        {
            using (DocumentTier mt = new DocumentTier())
            {
                if (mt.InsertDocuments(model))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
        }
        #endregion
    }
}
