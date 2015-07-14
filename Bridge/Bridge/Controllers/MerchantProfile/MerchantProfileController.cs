using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bridge.Models;
using Bridge.BusinessTier;

namespace Bridge.Controllers.MerchantProfile
{
    [RoutePrefix("merchantprofile")]
    public class MerchantProfileController : ApiController
    {
        #region Search
        [HttpGet]
        [Route("status")]
        public HttpResponseMessage RetriveMerchantStatus(string statusType = "mrc")
        {
            IList<GeneralModel> response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveStatus(statusType);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Business
        [HttpGet]
        [Route("{merchantId}/business")]
        public HttpResponseMessage RetrieveMerchantBusinessInformation(int merchantId)
        {
            MPMerchantBusinessModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantBusinessInformation(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/business/processor")]
        public HttpResponseMessage RetrieveMerchantBusinessProcessorInformation(int merchantId)
        {
            IList<MPMerchantProcessorInfoModel> response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantBusinessProcessorInformation(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPut]
        [Route("{merchantId}/business/Update")]
        public HttpResponseMessage UpdateMerchantBusinessInformation(int merchantId, [FromBody]MPMerchantBusinessModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateMerchantBusinessInformation(obj));
                }
                catch(Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        [HttpPut]
        [Route("{merchantId}/business/Update/processor")]
        public HttpResponseMessage UpdateMerchantBusinessProcessorInformation(Int64 merchantId, [FromUri]int Terminal, [FromUri]int processorTypeId, [FromUri]string processorNumber, [FromUri]DateTime dateGracePeriod, [FromUri]int industryTypeID, [FromUri]DateTime firstProcessedDate, [FromUri]int processorId)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateMerchantBusinessProcessorInformation(merchantId, Terminal, processorTypeId, processorNumber, dateGracePeriod, industryTypeID, firstProcessedDate, processorId));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        [HttpPut]
        [Route("{merchantId}/business/Processor/Update")]
        public HttpResponseMessage UpdateMerchantBusinessProcessorInformation([FromBody] MPMerchantProcessorInfoModel entity)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateMerchantBusinessProcessorInformation(entity));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        #endregion

        #region Landlord
        [HttpGet]
        [Route("{merchantId}/landlord")]
        public HttpResponseMessage RetrieveMerchantLandlordInformation(int merchantId)
        {
            MPMerchantLandlordModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantLandlordInformation(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/landlord/questions")]
        public HttpResponseMessage RetrieveMerchantLandlordQuestionInformation(int merchantId)
        {
            MPMerchantLandlordQuestionsModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantLandlordQuestionInformation(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPut]
        [Route("{merchantId}/landlord/Update")]
        public HttpResponseMessage UpdateMerchantLandlordInformation([FromBody]MPMerchantLandlordModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateMerchantLandlordInformation(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        #endregion

        #region Contacts
        [HttpGet]
        [Route("{merchantId}/contacts")]
        public HttpResponseMessage RetrieveMerchantContacts(int merchantId)
        {
            IList<MPMerchantContactsModel> response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantContacts(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/contacts/details")]
        public HttpResponseMessage RetrieveMerchantContactsInformation(int contactId)
        {
            MPMerchantContactDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantContactsInformation(contactId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPut]
        [Route("{merchantId}/contact/update")]
        public HttpResponseMessage UpdateMerchantContactInformation([FromBody]MPMerchantContactDetailModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateMerchantContactInformation(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        [HttpPut]
        [Route("{merchantId}/contact/add")]
        public HttpResponseMessage AddMerchantContactInformation([FromBody]MPMerchantContactDetailModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.AddMerchantContactInformation(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }

        [HttpDelete]
        [Route("{merchantId}/contact/delete/{contactId}")]
        public HttpResponseMessage DeleteMerchantContacts(int merchantId, int contactId)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                return this.Request.CreateResponse(HttpStatusCode.OK, mt.DeleteMerchantContacts(contactId));
            }
        }
        #endregion

        #region Owners
        [HttpGet]
        [Route("{merchantId}/Owners")]
        public HttpResponseMessage RetrieveMerchantOwners(int merchantId)
        {
            IList<MPMerchantOwnersModel> response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantOwners(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/Owners/details")]
        public HttpResponseMessage RetrieveMerchantOwnersInformation(int OwnerId)
        {
            MPMerchantOwnerDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantOwnersInformation(OwnerId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPut]
        [Route("{merchantId}/Owner/update")]
        public HttpResponseMessage UpdateMerchantOwnerInformation([FromBody]MPMerchantOwnerDetailModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateMerchantOwnerInformation(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        [HttpPut]
        [Route("{merchantId}/Owner/add")]
        public HttpResponseMessage AddMerchantOwnerInformation([FromBody]MPMerchantOwnerDetailModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.AddMerchantOwnerInformation(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        [HttpDelete]
        [Route("{merchantId}/Owner/delete/{ownerId}")]
        public HttpResponseMessage DeleteMerchantOwners(int merchantId, int ownerId)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                return this.Request.CreateResponse(HttpStatusCode.OK, mt.DeleteMerchantOwners(ownerId));
            }
        }
        #endregion

        #region Profiles
        [HttpGet]
        [Route("{merchantId}/profiles")]
        public HttpResponseMessage RetrieveMerchantProfiles(int merchantId, int ProcessorId, string ProcessorNumber)
        {
            MPMerchantProfileDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantProfiles(merchantId, ProcessorId, ProcessorNumber);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/profiles/details")]
        public HttpResponseMessage RetrieveMerchantProfileInformation(int ProfileId)
        {
            MPMerchantProfileDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantProfile(ProfileId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPut]
        [Route("{merchantId}/profile/update")]
        public HttpResponseMessage UpdateMerchantProfileInformation([FromBody]MPMerchantProfileDetailModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateMerchantProfileInformation(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        #endregion

        #region Documents
        [HttpGet]
        [Route("{merchantId}/documents")]
        public HttpResponseMessage RetrieveMerchantDocuments(int merchantId, int DocumentTypeId)
        {
            MPMerchantDocumentsDetailModel response = new MPMerchantDocumentsDetailModel(); ;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response.Documents = mt.RetrieveMerchantDocuments(merchantId, DocumentTypeId);                
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpDelete]
        [Route("{merchantId}/documents/delete/{documentId}")]
        public HttpResponseMessage DeleteMerchantDocuments(int merchantId, int documentId)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                return this.Request.CreateResponse(HttpStatusCode.OK, mt.DeleteMerchantDocuments(documentId));
            }
        }
        #endregion

        #region Activities
        [HttpGet]
        [Route("{merchantId}/activitiesmonthly")]
        public HttpResponseMessage RetrieveMerchanActivitiesMonthly(int merchantId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate)
        {
            MPMerchantActivityDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchanActivitiesMonthly(merchantId, ProcessorTypeId, ProcessorNumber, ActivityStartDate, ActivityEndDate);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/activities")]
        public HttpResponseMessage RetrieveMerchanActivities(int merchantId, int ProcessorTypeId, string MonthName)
        {
            MPMerchantActivityDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantActivities(merchantId, ProcessorTypeId, MonthName);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Statements
        [HttpGet]
        [Route("{merchantId}/statements")]
        public HttpResponseMessage RetrieveMerchantStatements(int merchantId, DateTime StatementsFrom, DateTime StatementsTo)
        {
            MPMerchantStatementsDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantStatements(merchantId, StatementsFrom, StatementsTo);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/allstatements")]
        public HttpResponseMessage RetrieveAllMerchantStatements(int merchantId, DateTime StatementsFrom, DateTime StatementsTo)
        {
            IList<MPMerchantStatementsDetailModel> response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveAllMerchantStatements(merchantId, StatementsFrom, StatementsTo);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region History
        [HttpGet]
        [Route("{merchantId}/history")]
        public HttpResponseMessage RetrieveMerchanHistory(int merchantId, DateTime HistoryStartDate, DateTime HistoryEndDate)
        {
            MPMerchantHistoryDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantHistory(merchantId, HistoryStartDate, HistoryEndDate);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Risk Evaluation
        [HttpGet]
        [Route("{merchantId}/risk")]
        public HttpResponseMessage RetrieveMerchanRiskEvaluation(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            MPMerchantRiskEvaluationDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantRiskEvaluation(merchantId, StartDate, EndDate);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Contracts
        [HttpGet]
        [Route("{merchantId}/contracts")]
        public HttpResponseMessage RetrieveMerchanContracts(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            MPMerchantContractDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantContracts(merchantId, StartDate, EndDate);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("{merchantId}/contracts/{contractid}/generalinfo")]
        public HttpResponseMessage RetrieveContractGeneralInformation(int contractId)
        {
            MPMerchantContractModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveContractGeneralInformation(contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpPut]
        [Route("{merchantId}/contracts/{contractid}/generalinfo/update")]
        public HttpResponseMessage UpdateContractGeneralInformation([FromBody]MPMerchantContractModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateContractGeneralInformation(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("{merchantId}/contracts/{contractid}/history")]
        public HttpResponseMessage RetrieveContractHistory(int contractId, DateTime HistoryStartDate, DateTime HistoryEndDate)
        {
            MPMerchantContractModel response= new MPMerchantContractModel();
            MPMerchantHistoryDetailModel innerresponse;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                innerresponse = mt.RetrieveContractHistory(contractId, HistoryStartDate, HistoryEndDate);
                response.HistoryDetail = innerresponse;
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        
        [HttpGet]
        [Route("{merchantId}/contracts/{contractid}/activities")]
        public HttpResponseMessage RetrieveContractActivities(int contractId, int ProcessorTypeId, string ProcessorNumber, DateTime ActivityStartDate, DateTime ActivityEndDate)
        {
            MPMerchantContractModel response = new MPMerchantContractModel();
            MPMerchantActivityDetailModel innerresponse;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                innerresponse = mt.RetrieveContractActivities(contractId, ProcessorTypeId, ProcessorNumber, ActivityStartDate, ActivityEndDate);
                response.ActivityDetail = innerresponse;
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/contracts/{contractid}/salesrep")]
        public HttpResponseMessage RetrieveContractSalesRepresentative(int contractId)
        {
            MPMerchantContractModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveContractSalesRepresentative(contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/contracts/{contractid}/salesrep/{salesrepid}")]
        public HttpResponseMessage RetrieveContractSalesRepresentativeDetail(int salesRepId)
        {
            MPMerchantContractSalesRepresentativeModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveContractSalesRepresentativeDetail(salesRepId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpPut]
        [Route("{merchantId}/contracts/{contractid}/salesrep/update")]
        public HttpResponseMessage UpdateContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.UpdateContractSalesRepresentative(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        [HttpPut]
        [Route("{merchantId}/contracts/{contractid}/salesrep/add")]
        public HttpResponseMessage AddContractSalesRepresentative(MPMerchantContractSalesRepresentativeModel obj)
        {
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                try
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.AddContractSalesRepresentative(obj));
                }
                catch (Exception ex)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        #endregion

        #region Collection
        [HttpGet]
        [Route("{merchantId}/collections")]
        public HttpResponseMessage RetrieveMerchanCollection(int merchantId, DateTime StartDate, DateTime EndDate)
        {
            MPMerchantCollectionDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchantCollection(merchantId, StartDate, EndDate);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Affiliations
        [HttpGet]
        [Route("{merchantId}/affiliations")]
        public HttpResponseMessage RetrieveMerchanAffiliations(int merchantId, string RequestType)
        {
            MPMerchantAffiliationDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchanAffiliations(merchantId, RequestType);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion

        #region Credit Report
        [HttpGet]
        [Route("{merchantId}/creditreport/contracts")]
        public HttpResponseMessage RetriveMerchantContracts(int merchantId)
        {
            IList<GeneralModel> response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetriveMerchantContracts(merchantId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/creditreport/contracts/{contractId}/creditreports")]
        public HttpResponseMessage RetriveMerchantContractCreditReports(int contractId)
        {
            IList<GeneralModel> response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetriveMerchantContractCreditReports(contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        [HttpGet]
        [Route("{merchantId}/creditreport/contracts/{contractId}/creditreports/{reportId}")]
        public HttpResponseMessage RetrieveMerchanCreditReport(int reportId, Int64 contractId, Int64 merchantId)
        {
            MPMerchantCreditReportDetailModel response;
            using (MerchantProfileTier mt = new MerchantProfileTier())
            {
                response = mt.RetrieveMerchanCreditReport(reportId);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        //[HttpGet]
        //[Route("{merchantId}/creditreport/details")]
        //public HttpResponseMessage RetrieveMerchanCreditReportInformation(int merchantId, int contractId)
        //{
        //    MPMerchantCreditReportDetailModel response;
        //    using (MerchantProfileTier mt = new MerchantProfileTier())
        //    {
        //        response = mt.RetrieveMerchanCreditReportInformation(contractId);
        //        return this.Request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //}
        #endregion
    }
}
