using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models;
using Bridge.BusinessTier;
using System.Web.Script.Serialization;

namespace Bridge.Controllers.Contracts
{
    [RoutePrefix("contracts")]
    public class ContractsController : ApiController
    {
        #region Methods
        /// <summary>
        /// To get the list of Bank Names - Bank Information Verification
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBankNames")]
        public HttpResponseMessage GetBankNames()
        {
            IList<BankNameModel> response;
            using (ContractTier dT = new ContractTier())
            {
                response = dT.ListBankNames();
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// To get the details of Bank for a Merchant - Bank Information Verification
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBankDetails")]
        public HttpResponseMessage GetBankDetails(Int64 contractId, Int64 merchantId, int bankId = 0)
        {
            IList<BankDetailModel> response;
            using (ContractTier dT = new ContractTier())
            {
                response = dT.RetrieveBankDetails(contractId, merchantId, bankId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// To decline the contract
        /// </summary>
        /// <param name="contractid"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("decline/{contractid}")]
        public HttpResponseMessage DeclineContract([FromBody] DeclineModel entity, Int64 contractid)
        {
            using (ContractTier contracttier = new ContractTier())
            {

                contracttier.DeclineContract(contractid, entity.declinereason, entity.workflowId, entity.declineNotes, entity.IsReEvaluvate, entity.merchantId, entity.Screen);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
        }

        /// <summary>
        /// To Insert or Update the bank details for a Merchant, everything will be updated/inserted irrespective of file details - Bank Information Verification
        /// </summary>
        /// <param name="bankDetail"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateBankDetails")]
        public HttpResponseMessage UpdateBankDetails(BankDetailModel bankDetail, Int16 isCompleted)
        {
            using (ContractTier ct = new ContractTier())
            {
                if (ct.UpdateBankDetails(bankDetail, isCompleted))
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
        [Route("UpdateCommercialDetails")]
        public HttpResponseMessage UpdateCommercialDetails(Int64 merchantId, CommercialVerification cm)
        {
            using (ContractTier ct = new ContractTier())
            {
                ct.UpdateCommercialDetails(merchantId, cm);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPut]
        [Route("UpdateCommercialOwnerDetails")]
        public HttpResponseMessage UpdateCommericalOwnerDetails(Int64 contractId, Int64 merchantId, List<OwnerModel> cm)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.UpdateCommericalOwnerDetails(contractId, merchantId, cm);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }


        /// <summary>
        /// To verify Corporate document of a Merchant - Corporate Document Verfication
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCorporateDocumemts")]
        public HttpResponseMessage GetCorporateDocumemts(Int64 contractId, Int64 merchantId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.RetrieveCorpDetails(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// To Retrieve Commercial Bank Information of a Merchant - Commercial Name Verification
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCommercialDocVerification")]
        public HttpResponseMessage GetCommercialDocVerification(Int64 contractId, Int64 merchantId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.RetrieveCommercialBankInfo(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }



        /// <summary>
        /// To Retrieve BLA Details for a particular Contract - Generate Contract Screen
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBLADetails")]
        public HttpResponseMessage GetBLADetails(Int64 contractId, Int64 merchantId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.RetrieveBLADetails(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// To Retrieve IOU Details for a particular Contract - Generate Contract Screen
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIOUDetails")]
        public HttpResponseMessage GetIOUDetails(Int64 contractId, Int64 merchantId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.RetrieveIOUDetails(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        //[HttpGet]
        //[Route("GetMerchantDetailsForQuestion")]
        //public HttpResponseMessage GetMerchantDetailsForQuestion(Int64 merchantId)
        //{
        //    using (ContractTier dT = new ContractTier())
        //    {
        //        var response = dT.GetMerchantDetailsForQuestion(merchantId);
        //        return this.Request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //}


        /// <summary>
        /// To complete the tasks of Contract
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="taskTypeId"></param>
        /// <param name="workflowId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CompContractTask")]
        public HttpResponseMessage CompContractTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.CompleteContractTask(merchantId, taskTypeId, workflowId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }


        /// <summary>
        /// To complete the tasks of Contract
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="taskTypeId"></param>
        /// <param name="workflowId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("OfferAccpContractTask")]
        public HttpResponseMessage OfferAccpContractTask(Int64 merchantId, Int64 taskTypeId, Int64 workflowId, Int64 contractId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.CompleteOfferAcceptanceTask(merchantId, taskTypeId, workflowId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// Get Administrative Expenses 
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("GetAdminExp/{ContractId}")]
        public HttpResponseMessage GetAdminExp(Int64 ContractId)
        {
            using (ContractTier dt = new ContractTier())
            {
                var response = dt.GetAdminExp(ContractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }

        /// <summary>
        /// To retrieve funding details - Funding Task Screen
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFundingDetails")]
        public HttpResponseMessage GetFundingDetails(Int64 contractId, Int64 merchantId)
        {
            //  IList<FundingModel> response;
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.RetrieveFundingDetails(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        /// <summary>
        /// Retrieve Verification Call 
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetVerificationCall")]
        public HttpResponseMessage GetVerificationCall(Int64 contractId, Int64 merchantId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.RetrieveVerificationCall(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("GetLandLordVerification")]
        public HttpResponseMessage GetLandLordVerification(Int64 contractId, Int64 merchantId)
        {
            using (ContractTier dT = new ContractTier())
            {
                var response = dT.RetrieveLandLordVerification(contractId, merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("UpdateVerificationCall")]
        public HttpResponseMessage UpdateVerificationCall(VerificationModel call, long contractId, Int16 isCompleted)
        {
            using (ContractTier dT = new ContractTier())
            {
                if (dT.UpdateVeriCall(call, contractId, isCompleted, call.ScriptFile))
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
        [Route("UpdateLandlordVerification")]
        public HttpResponseMessage UpdateLandlordVerification(VerificationModel model, long contractId, Int16 isCompleted)
        {
            using (ContractTier dT = new ContractTier())
            {

                if (dT.UpdateLandLordVeri(model, contractId, isCompleted, model.ScriptFile))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
                }
            }

            //using (QuestionTier mt = new QuestionTier())
            //{
            //    long workflowId = 2;
            //    Int64 taskTypeId = 14;
            //    string entity = "Landlord";
            //    var listAnswers = new List<QuestionsModel>();

            //    //foreach (var c in call.questions)
            //    //{
            //    //    //      c.scriptFile = call.ScriptFile;
            //    //    c.entity = entity;
            //    //    listAnswers.Add(c);
            //    //}

            //    if (mt.InsUpdateAnswers(listAnswers, taskTypeId, workflowId, isCompleted, entity, call.ScriptFile))
            //    {
            //        return this.Request.CreateResponse(HttpStatusCode.OK);
            //    }
            //    else
            //    {
            //        return this.Request.CreateResponse(HttpStatusCode.NotModified);
            //    }
            //}

        }

        [HttpPost]
        [Route("Funded")]
        public HttpResponseMessage ContractFunded(FundingModel model)
        {
            using (ContractTier obj = new ContractTier())
            {
                if (obj.ContractFunded(model))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }
        }

        [HttpGet]
        [Route("RetrieveFinalValidation")]
        public HttpResponseMessage RetrieveFinalValidation(long MerchantId, long ContractId)
        {
            IList<FinalVerificationModel> model;
            using (ContractTier obj = new ContractTier())
            {
                model = obj.RetrieveFinalValidation(MerchantId, ContractId);
                return Request.CreateResponse(HttpStatusCode.OK, model.FirstOrDefault());
            }
        }

        [HttpPut]
        [Route("CompleteFinalValidation/{ContractId}/action/{str}")]
        public HttpResponseMessage SaveFinalValidation(long ContractId, string str)
        {

            using (ContractTier obj = new ContractTier())
            {
                if (obj.SaveFinalValidation(ContractId, str))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return this.Request.CreateResponse(HttpStatusCode.NotModified);
            }
        }

        [HttpGet]
        [Route("RetrieveAnnualSalesFile")]
        public HttpResponseMessage RetrieveAnnualSalesFile(long merchantId, long contractId)
        {
            using (ContractTier obj = new ContractTier())
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj.RetrieveAnnualSalesFile(merchantId, contractId));
            }
        }

        #endregion
    }
}


