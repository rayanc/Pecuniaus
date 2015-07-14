using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models;
using Bridge.BusinessTier;

namespace Bridge.Controllers.Finance
{
    [RoutePrefix("finance")]
    public class FinanceController : ApiController
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SearchFinance")]
        public HttpResponseMessage SearchFinanceActivity(SearchFinanceModel model)
        {
            IList<FinanceModel> response;
            try
            {
                
                using (FinanceTier fT = new FinanceTier())
                {
                    response = fT.RetrieveFinanceDetails(model);
                    
                }
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                //throw ex.Message;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Insert Finance detials in DB
        /// </summary>
        /// <param name="financeModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("SaveFinanceActivity")]
        public HttpResponseMessage SaveFinanceActivity(SearchFinanceModel financeModel)
        {
            IList<FinanceModel> response;
            using (FinanceTier fT = new FinanceTier())
            {
                response = fT.InsertFinanceDetails(financeModel);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RetrieveActivityType")]
        public HttpResponseMessage RetrieveActivityType()
        {
            IList<FinanceDD> response;
            using (FinanceTier fT = new FinanceTier())
            {
                response = fT.ListActivityType();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RetrieveFundedContract")]
        public HttpResponseMessage RetrieveFundedContract(Int64 MerchantId)
        {
            IList<AddFinanceModel> response;
            using (FinanceTier fT = new FinanceTier())
            {
                response = fT.RetrieveFundedContract(MerchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RetrieveProcessorName")]
        public HttpResponseMessage RetrieveProcessorName()
        {
            IList<FinanceDD> response;
            using (FinanceTier fT = new FinanceTier())
            {
                response = fT.ListProcessorName();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// Delete Activity
        /// </summary>
        /// <param name="financeModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("DeleteActivity/{actityID}")]
        public HttpResponseMessage DeleteActivity(long actityID)
        {            
            using (FinanceTier fT = new FinanceTier())
            {
                bool response = fT.DeleteActivity(actityID);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        #endregion        
    }
}
