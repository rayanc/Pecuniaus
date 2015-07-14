using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Bridge.BusinessTier;
using System.Net;
using Bridge.Models;
using Bridge.Handlers;
using System.Data;

namespace Bridge.Controllers.Merchants
{
    [RoutePrefix("collections")]
    public class CollectionsController : ApiController
    {
        #region Prevention and Collections
        /// <summary>
        /// Data for the prevention screen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("retriveprevention")]
        public HttpResponseMessage RetrivePrevention(Int64 AssignedUserId, decimal? percentage = null)
        {
            IList<PreventionModel> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.RetrivePrevention((decimal)percentage, AssignedUserId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// Days Drop down
        /// </summary>
        /// <param name="Config"></param>
        /// <param name="ConfigSystem"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetValuesForDays")]
        public HttpResponseMessage GetValuesForDays()
        {
            IList<DropDown> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.ListValuesForDays("CollectionScreen", "COL");
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// Data for the collections screen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("inactivityDays")]
        public HttpResponseMessage RetriveCollectionDays(Int64 AssignedUserId, int days)
        {
            IList<CollectionDaysModel> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.ListCollectionbyInactiveDays(days, AssignedUserId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Collection Workflow
        /// <summary>
        /// To Retrieve Contract Info
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RetriveContractInformation")]
        public HttpResponseMessage RetriveContractInformation(Int64 merchantId, Int64 contractId)
        {
            IList<CollectionContractInformation> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.RetrieveContractInfo(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// To Retrieve Owner Info
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RetriveOwnerInfo")]
        public HttpResponseMessage RetriveOwnerInfo(Int64 merchantId, Int64 contractId)
        {
            IList<OwnerColModel> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.ListOwnerInfo(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// TO return check while activity notes
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ColActivityCheck")]
        public HttpResponseMessage ColActivityCheck(Int64 merchantId, Int64 contractId)
        {
            IList<CollectionActivities> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.ColActivityCheck(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Affiliations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="isRNC"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetActiveContracts")]
        public HttpResponseMessage GetActiveContracts(Int64 merchantId, bool isRNC) 
        {
            IList<ActiveContracts> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.ListActiveContractAffiliations(merchantId,isRNC);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="isRNC"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNonActiveContracts")]
        public HttpResponseMessage GetNonActiveContracts(Int64 merchantId, bool isRNC)
        {
            IList<InActiveContracts> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.ListAffiliationsNonActiveContracts(merchantId,isRNC);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }     
        
        #endregion

        #region Documents

        [HttpGet]
        [Route("DeleteMerchantDocs")]
        public HttpResponseMessage DeleteMerchantDocs(string DocumentIds)
        {
            using (CollectionsTier ct = new CollectionsTier())
            {
                if (ct.MrcDeleteDocuments(DocumentIds))                
                    return this.Request.CreateResponse(HttpStatusCode.OK);                
                else                
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);                
            }
        }

        [HttpGet]
        [Route("RetriveDocument")]
        public HttpResponseMessage RetriveDocument(Int64 merchantId, Int64 contractId, int documentTypeId)
        {
            IList<DocumentsModel> response;
            using (CollectionsTier ct = new CollectionsTier())
            {
                response = ct.RetrieveDocuments(merchantId, contractId, documentTypeId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("RetrivePreviewDocument")]
        public HttpResponseMessage RetrivePreviewDocument(int documentId)
        {
            IList<DocumentsModel> response;
            using (CollectionsTier ct = new CollectionsTier())
            {
                response = ct.RetrieveDocuments(documentId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Landlords

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLandLordDetails")]
        public HttpResponseMessage GetLandLordDetails(Int64 merchantId)
        {
            IList<CollectionLandlordModel> response;
            using (CollectionsTier collection = new CollectionsTier())
            {
                response = collection.ListLandLordDetails(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateLandlords")]
        public HttpResponseMessage UpdateLandlords(CollectionLandlordModel model)
        {
            using (CollectionsTier ct = new CollectionsTier())
            {
                if (ct.UpdateMerchantLandlordInformation(model))
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }
        }

        #endregion

        #region Legal

        [HttpGet]
        [Route("GetAssignedLawyers")]
        public HttpResponseMessage GetAssignedLawyers(Int64 merchantId, Int64 contractId)
        {
            IList<LawyerModel> response;
            using (CollectionsTier dT = new CollectionsTier())
            {
                response = dT.RetrieveAssignedLawyers(merchantId,contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("GetAllLawyersToAssign")]
        public HttpResponseMessage GetAllLawyersToAssign(Int64 merchantId, Int64 contractId)
        {
            IList<LawyerModel> response;
            using (CollectionsTier dT = new CollectionsTier())
            {
                response = dT.RetrieveAllLawyersToAssign(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("GetLegalDocuments")]
        public HttpResponseMessage GetLegalDocuments(Int64 merchantId, Int64 contractId)
        {
            IList<LegalDocuments> response;
            using (CollectionsTier dT = new CollectionsTier())
            {
                response = dT.RetrieveLegalDocuments(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("AssignLawyer")]
        public HttpResponseMessage AssignLawyer(LawyerModel model)
        {
            using (CollectionsTier ct = new CollectionsTier())
            {
                if (ct.AssignLawyersToCollection(model))
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }
        }

        [HttpGet]
        [Route("GetAllLawyers")]
        public HttpResponseMessage GetAllLawyers()
        {
            using (CollectionsTier dT = new CollectionsTier())
            {
                var response = dT.RetrieveAllLawyers();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("InsUpdateLawyers")]
        public HttpResponseMessage InsUpdateLawyers(CollectionsModel model)
        {
            using (CollectionsTier mt = new CollectionsTier())
            {
                if (mt.InsUpdateLawyers(model.AssignedLawyers))
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

        #region Payment Agreement

        [HttpGet]
        [Route("GetPaymentDetails")]
        public HttpResponseMessage GetPaymentDetails(Int64 merchantId, Int64 contractId)
        {
            IList<CollectionPaymentAgreementModel> response;
            using (CollectionsTier dT = new CollectionsTier())
            {
                response = dT.RetrievePaymentDetails(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("SavePaymentDetails")]
        public HttpResponseMessage SavePaymentDetails(CollectionPaymentAgreementModel model)
        {
            using (CollectionsTier ct = new CollectionsTier())
            {
                if (ct.InsertPaymentAgreement(model))
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }
        }

        #endregion

        #region Generate Letters

        [HttpGet]
        [Route("SendCollectionLetter")]
        public HttpResponseMessage SendCollectionLetter(Int64 merchantId, Int64 contractId)
        {
            IList<GenerateLetter> response;
            using (CollectionsTier ct = new CollectionsTier())
            {
                response=ct.SendCollectionLetter(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }            
        }

        #endregion

        #region Collection Activities

        [HttpGet]
        [Route("GetCollectionActivities")]
        public HttpResponseMessage GetCollectionActivities()
        {
            IList<DropDown> response;
            using (CollectionsTier dT = new CollectionsTier())
            {
                response = dT.ListCollectionActivities();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("InsertCollectionActivity")]
        public HttpResponseMessage InsertCollectionActivity(CollectionActivityModel model)
        {
            using (CollectionsTier collection = new CollectionsTier())
            {
                if (collection.InsertCollectionActivity(model))                
                    return this.Request.CreateResponse(HttpStatusCode.OK);                
                else                
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}
