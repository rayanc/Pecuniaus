using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Bridge.BusinessTier;
using System.Net;
using Bridge.Models;
using System.Data;
namespace Bridge.Controllers
{
    [RoutePrefix("renewals")]
    public class RenewalsController : ApiController
    {

        [HttpGet]
        [Route("")]
        public HttpResponseMessage RetriveRenewalsMerchantsList(string filter)
        {
            if (filter == null)
                filter = string.Empty;
            DataSet response;
            using (RenewalsTier mt = new RenewalsTier())
            {
                response = mt.RetrieveRenewalsList(filter.Trim());
                if (response.Tables[0].Rows.Count > 0)
                {

                    var renewalList = (from DataRow dr in response.Tables[0].Rows
                                       select new
                                       {
                                           merchantId = Convert.ToInt32(dr["merchantId"]),
                                           merchantName = Convert.ToString(dr["legalName"]),
                                           taskName = Convert.ToString(dr["taskName"]),
                                           taskstatus = Convert.ToString(dr["taskStatus"]),
                                           merchantStatus = Convert.ToString(dr["statusname"]),
                                           //contractStatus = Convert.ToString( dr["ContractStatus"]),
                                           loanAmount = Convert.ToDouble(dr["loanAmount"]),
                                           ownedAmount = Convert.ToDouble(dr["ownedAmount"]),
                                           paidamount = Convert.ToDouble(dr["paidAmount"]),
                                           expectedturn = Convert.ToDouble(dr["expectedTurn"]),
                                           actualturn = Convert.ToDouble(dr["actualTurn"]),
                                           contractId = Convert.ToInt64(dr["contractId"]),
                                           everInCollection = Convert.ToDouble(dr["everinCollection"]),
                                           paidpercent = Convert.ToDouble(dr["percentpaid"]),
                                           fundedDate = Convert.ToString(dr["fundedDate"])
                                       }).ToList();
                    return this.Request.CreateResponse(HttpStatusCode.OK, renewalList);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
            }

        }

        [HttpGet]
        [Route("complete/{contractId}")]
        public HttpResponseMessage CompleteRenewal(Int64 contractId)
        {
            using (RenewalsTier mt = new RenewalsTier())
            {
                long ContractID = mt.CompleteRenewal(contractId);
                if (ContractID > 0)
                {
                    var respose = new { Value = ContractID };
                    return this.Request.CreateResponse(HttpStatusCode.OK, respose);
                }
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }

        }

        [HttpPut]
        [Route("Snooze/")]
        public HttpResponseMessage Snooze(int Contractid, DateTime snooze, int PercentPaid)
        {
            using (RenewalsTier mt = new RenewalsTier())
            {
                mt.Snooze(Contractid, snooze, PercentPaid);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpGet]
        [Route("snooze/{contractid}")]
        public HttpResponseMessage RetrieveSnooze(long Contractid)
        {
            using (RenewalsTier mt = new RenewalsTier())
            {
                SnoozeModel obj = mt.RetreiveSnooze(Contractid);
                return this.Request.CreateResponse(HttpStatusCode.OK, obj);
            }
        }


        [HttpGet]
        [Route("declinereasons/")]
        public IList<GeneralModel> RetreiveDeclineReasons()
        {
            using (RenewalsTier mt = new RenewalsTier())
            {
                return mt.RetreiveDeclineReasons();
            }
        }

        [HttpGet]
        [Route("contractInformation/{contractid}")]
        public HttpResponseMessage RetrieveContractDetails(long contractid)
        {
            DataSet response;
            using (RenewalsTier mt = new RenewalsTier())
            {
                response = mt.RetrieveContractDetatis(contractid);
                if (response.Tables[0].Rows.Count > 0)
                {

                    var renewalList = response.Tables[0].AsEnumerable().Select(data => new
                    {

                        merchantId = data.Field<Int64>("MerchantId"),
                        merchantName = data.Field<string>("merchantName"),
                        businessName = data.Field<string>("BusinessName"),
                        taskName = data.Field<string>("taskName"),
                        taskstatus = data.Field<string>("taskStatus"),
                        merchantStatus = data.Field<string>("MerchantStatus"),
                        contractStatus = data.Field<string>("ContractStatus"),
                        loanAmount = Convert.ToDouble(response.Tables[0].Rows[0]["loanAmount"]),
                        ownedAmount = Convert.ToDouble(response.Tables[0].Rows[0]["ownedAmount"]),
                        paidamount = Convert.ToDouble(response.Tables[0].Rows[0]["paidAmount"]),
                        expectedturn = Convert.ToDouble(response.Tables[0].Rows[0]["expectedTurn"]),
                        actualturn = Convert.ToDouble(response.Tables[0].Rows[0]["actualTurn"]),
                        contractId = Convert.ToInt64(response.Tables[0].Rows[0]["contractId"]),
                        //statusid = Convert.ToInt32(response.Tables[0].Rows[0]["statusid"]),
                        everInCollection = Convert.ToDouble(response.Tables[0].Rows[0]["everinCollection"]),
                        paidpercent = Convert.ToDouble(response.Tables[0].Rows[0]["percentpaid"]),
                        fundedDate = data.Field<string>("fundedDate"),
                        pendingAmount = Convert.ToDouble(response.Tables[0].Rows[0]["pendingAmount"])

                    }).ToList();
                    return this.Request.CreateResponse(HttpStatusCode.OK, renewalList.FirstOrDefault());
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
            }

        }


        [HttpPost]
        [Route("decline/{contractid}")]
        public HttpResponseMessage DeclineContract([FromBody] DeclineModel entity, Int64 contractid)
        {
            using (RenewalsTier obj = new RenewalsTier())
            {
                if (obj.DeclineContract(contractid, entity.declineId, entity.workflowId, entity.declineNotes) == 1)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }
        }

        [HttpGet]
        [Route("GetFundingDetails")]
        public HttpResponseMessage GetFundingDetails(Int64 contractId, Int64 merchantId)
        {
            using (RenewalsTier dT = new RenewalsTier())
            {
                var response = dT.RetrieveFundingDetails(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPost]
        [Route("Funded")]
        public HttpResponseMessage RenewalFunded(FundingModel model)
        {
            try
            {
                using (RenewalsTier obj = new RenewalsTier())
                {
                    if (obj.RenewalFunded(model))
                        return Request.CreateResponse(HttpStatusCode.OK);
                    else
                        return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }
            catch (Exception ex)
            { }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("RetrieveFinalValidation")]
        public HttpResponseMessage RetrieveFinalValidation(long MerchantId, long ContractId)
        {
            IList<FinalVerificationModel> model;
            using (RenewalsTier obj = new RenewalsTier())
            {
                model = obj.RetrieveFinalValidation(MerchantId, ContractId);
                return Request.CreateResponse(HttpStatusCode.OK, model.FirstOrDefault());
            }
        }

        [HttpPut]
        [Route("CompleteFinalValidation/{ContractId}/action/{str}")]
        public HttpResponseMessage SaveFinalValidation(long ContractId, string str)
        {

            using (RenewalsTier obj = new RenewalsTier())
            {
                if (obj.SaveFinalValidation(ContractId, str))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }
        }

        [HttpGet]
        [Route("GetActiveContract")]
        public HttpResponseMessage GetActiveContractID(long merchantId, long contractId)
        {
            using (RenewalsTier obj = new RenewalsTier())
            {
                long activeContractId = obj.GetActiveContractID(merchantId, contractId);
                return Request.CreateResponse(HttpStatusCode.OK, activeContractId);

            }

        }
    }
}
