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
using Bridge.Models.General;
namespace Bridge.Controllers.Merchants
{
    [RoutePrefix("merchants")]
    [HandleException]
    public class MerchantsController : ApiController
    {

        #region Merchants
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="merchantId"></param>
        /// <param name="isCompleted"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("dataentry/{merchantId}")]
        public HttpResponseMessage SaveDE([FromBody] MerchantDEModel model, Int64 merchantId, string isCompleted = null)
        {

            using (MerchantTier mt = new MerchantTier())
            {
                if (mt.SaveDE(model, merchantId, isCompleted))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }





        /// <summary>
        /// RetriveMerchants for specified user
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage RetriveMerchants(Int64 merchantId)
        {
            MerchantsModel response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetrieveMerchants(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("GetEmail")]
        public HttpResponseMessage RetriveEmails(Int64 SalesRepId)
        {
            IList<EmailModel> response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetriveEmails(SalesRepId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }


        /// <summary>
        /// Retrieve merchant  from the merchant's information
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        [Route("search")]
        [HttpGet]
        public HttpResponseMessage RetriveMerchants(string businessName = null, string rnc = null, string legalName = null, string ownerName = null,
            Int64? merchantId = null, Int64? contractId = null,
            Int64? workflowId = null, Int64? statusId = null, Int64? processornbr = null, string processorName = null, Int64? tasktype = null)
        {
            IList<MerchantsModel> response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetrieveMerchants(businessName, rnc, legalName, ownerName, merchantId, contractId,
             workflowId, statusId, processornbr, processorName, tasktype);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// Retrieve merchant  from the merchant's information
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        [Route("searchResult")]
        [HttpGet]
        public HttpResponseMessage RetriveMerchantsSearchResults(string businessName = "", string rnc = "", string legalName = "", string ownerName = "",
            Int64 merchantId = 0, Int64 contractId = 0,
            Int64 workflowId = 0, Int64 statusId = 0, Int64 processornbr = 0, string processorName = "", Int64 tasktype = 0, Int16 showTemp = 0, string SearchType = "", Int64 assignedUserId = 0)
        {
            IList<SearchResultsModel> response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetrieveMerchantsSeachResults(businessName, rnc, legalName, ownerName, merchantId, contractId,
             workflowId, statusId, processornbr, processorName, tasktype, showTemp, SearchType, assignedUserId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }


        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody] MerchantsModel model)
        {

            using (MerchantTier mt = new MerchantTier())
            {
                if (mt.Update(model))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
        }
        [HttpPut]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] MerchantsModel model)
        {

            using (MerchantTier mt = new MerchantTier())
            {
                if (mt.Create(model))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }
        [HttpPost]
        [Route("remove/{merchantId}")]
        public HttpResponseMessage Remove(int merchantId)
        {
            using (MerchantTier mt = new MerchantTier())
            {
                if (mt.Remove(merchantId))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }
        [Route("info/{merchantId}")]
        [HttpGet]
        public HttpResponseMessage RetriveMerchantsCompleteInfo(Int64 merchantId)
        {
            MerchantsAdditionalInfo response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetrieveMerchantDataEntry(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [Route("merchantinfo/{merchantId}")]
        [HttpGet]
        public HttpResponseMessage RetriveMerchantsInfo(Int64 merchantId, Int64 tasktypeId = 0, Int64 contractId = 0)
        {
            MerchantsModel response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetrieveMerchantInfo(merchantId, tasktypeId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        #endregion

        #region Temp Merchants
        [HttpGet]
        [Route("queue")]
        public HttpResponseMessage RetriveQueue()
        {
            IList<MerchantTempModel> response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetrieveQueue();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        /// <summary>
        /// Save the temporary values and if the parameter contains isCompleted then the system would complete the task
        /// </summary>
        /// <param name="merchantid"></param>
        /// <param name="model"></param>
        /// <param name="isCompleted"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("temp/{merchantid}")]
        public HttpResponseMessage Save([FromBody] MerchantTempModel model, Int64 merchantid = 0, string isCompleted = null)
        {
            Int64 _merchantId = 0;
            using (MerchantTier mt = new MerchantTier())
            {
                _merchantId = mt.Save(model, merchantid, isCompleted);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { merchantid = _merchantId });

            }
        }

        [HttpGet]
        [Route("temp/sf")]
        public HttpResponseMessage RetriveMerchantSalesForce(string businessName = null, string legalName = null, string ownerName = null, string rnc = null)
        {
            IList<MerchantTempModel> response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetriveMerchantSalesForce(businessName, rnc, legalName, ownerName);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        #endregion

        #region Owners
        [HttpGet]
        [Route("owner")]
        public HttpResponseMessage RetriveOwner(Int64 merchantId)
        {
            IList<OwnerModel> response;
            using (MerchantTier owner = new MerchantTier())
            {
                response = owner.RetrieveOwner(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Profiles
        [HttpGet]
        [Route("ccprofiles")]
        public HttpResponseMessage RetriveCreditCardProfiles(Int64 merchantId)
        {
            IList<Bridge.Models.MerchantProfile> response;
            using (MerchantTier profiles = new MerchantTier())
            {
                response = profiles.RetriveCreditCardProfiles(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPost]
        [Route("profiles/create")]
        public HttpResponseMessage ManageCreditCardProfiles(Int64 merchantid, [FromBody] IList<Bridge.Models.MerchantProfile> profiles, int iscompleted, Int64 contractId)
        {
            int response = 0;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.ManageCreditCardProfile(merchantid, profiles, iscompleted, contractId);
                if (response != 0)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        [HttpPost]
        [Route("profiles/requestprocessor")]
        public HttpResponseMessage SendRequesttoProcessor(Int64 merchantid, Int64 processorId)
        {

            using (MerchantTier mt = new MerchantTier())
            {

                if (mt.SendRequesttoProcessor(merchantid, processorId))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
        [HttpGet]
        [Route("ccprofiles/processorbyMerchants/{merchantId}")]
        public HttpResponseMessage RetriveProcessorbyMerchant(Int64 merchantId)
        {
            IList<ProcessorModel> response;
            using (MerchantTier profiles = new MerchantTier())
            {
                response = profiles.RetrieveProcessorbyMerchant(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region renewals
        [HttpGet]
        [Route("renewals")]
        public HttpResponseMessage RetriveMerchants()
        {
            DataSet response;
            using (MerchantTier mt = new MerchantTier())
            {
                response = mt.RetrieveRenewalsList();
                if (response.Tables[0].Rows.Count > 0)
                {
                    var str = response.Tables[0].Rows[0]["percentPaid"];
                    var renewalList = response.Tables[0].AsEnumerable().Select(data => new
                    {
                        merchantId = data.Field<Int64>("merchantId"),
                        legalName = data.Field<string>("legalName"),
                        taskName = data.Field<string>("taskName"),
                        taskstatus = data.Field<Int32>("statusId"),
                        merchantStatus = "",
                        loanamount = data.Field<double>("loanamount"),
                        ownedAmount = data.Field<double>("ownedAmount"),
                        paidamount = data.Field<double>("paidamount"),
                        // fundedDate = data.Field<DateTime>("fundedDate", DataRowVersion.Current),
                        paidpercent = 0D,
                        expectedturn = data.Field<Int64>("expectedTurn", DataRowVersion.Current),
                        actualturn = data.Field<Int64>("actualTurn", DataRowVersion.Current),
                        contractId = data.Field<Int64>("contractId")
                    }).ToList();
                    return this.Request.CreateResponse(HttpStatusCode.OK, renewalList);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }

        [Route("renewals/{contractid}/complete")]
        public HttpResponseMessage CompleteRenewalTask(Int64 contractId)
        {
            using (MerchantTier mt = new MerchantTier())
            {
                mt.CompleteRenewalsTask(contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }

        }
        #endregion

        #region MerchantOffer
        [HttpGet]
        [Route("Offer/{merchantId}/{contractId}")]
        public HttpResponseMessage CompleteRenewalTask(Int64 merchantId, Int64 contractId)
        {
            using (MerchantTier mt = new MerchantTier())
            {
                var mo = mt.RetrieveMerchantOffer(merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, mo);
            }

        }
        #endregion

    }
}
