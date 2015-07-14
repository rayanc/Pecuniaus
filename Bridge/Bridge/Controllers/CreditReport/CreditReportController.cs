using System;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Bridge.BusinessTier;
using System.Net;
using Bridge.Models;
using Bridge.Models.CreditReport;
using Bridge.Handlers;
using System.Data;
using Bridge.Models.General;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace Bridge.Controllers.CreditReport
{
    [RoutePrefix("creditreport")]
    [HandleException]
    public class CreditReportController : ApiController
    {
        #region CR
        [HttpPut]
        [Route("{merchantId}")]
        public HttpResponseMessage InsertCreditReport(Int64 merchantId, Int64 contractId, int reporttype, string reportAvailable, string isCompleted = null)
        {
            CreditReportModel model = new CreditReportModel();
            using (CreditReportTier mt = new CreditReportTier())
            {
                if (mt.PullReportbyId(reporttype, merchantId, contractId, isCompleted, reportAvailable == "true" ? 0 : 1))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpGet]
        [Route("{merchantId}/{contractId}")]
        public HttpResponseMessage RetrieveCreditReport(Int64 merchantId, Int64 contractId)
        {
            IList<CreditReportModel> model;
            using (CreditReportTier mt = new CreditReportTier())
            {
                model = (mt.RetrieveCreditReport(merchantId, contractId));

                return this.Request.CreateResponse(HttpStatusCode.OK, model);
            }
        }

        [HttpGet]
        [Route("score/{merchantId}/{contractId}")]
        public HttpResponseMessage RetrieveMerchantScore(Int64 merchantId, Int64 contractId, Int32 workflowid, Int32 tasktypeid)
        {
            DataSet ds;
            using (CreditReportTier mt = new CreditReportTier())
            {
                ds = (mt.RetrieveInformationforScorin(merchantId));
                DataTable MerchantInfo = ds.Tables[0];
                MerchantInfo.TableName = "MerchantInfo";
                DataTable OwnerInfo = ds.Tables[1];
                OwnerInfo.TableName = "Owners";
                DataTable Processor = ds.Tables[2];
                Processor.TableName = "Processor";
                DataTable OwnerCreditInfo = ds.Tables[3];
                OwnerCreditInfo.TableName = "OwnerCreditInfo";
                DataTable OwnerLoans = ds.Tables[4];
                OwnerLoans.TableName = "OwnerLoans";
                DataTable OwnerCreditCard = ds.Tables[5];
                OwnerCreditCard.TableName = "CreditCard";
                DataTable OwnerOthers = ds.Tables[6];
                OwnerOthers.TableName = "Other";
                DataTable BankStatements = ds.Tables[11];
                BankStatements.TableName = "BankStatements";

                DataTable CompanyCreditInfo = ds.Tables[7];
                CompanyCreditInfo.TableName = "CompanyCreditInfo";
                DataTable CompanyLoans = ds.Tables[8];
                CompanyLoans.TableName = "CompanyLoans";
                DataTable CompanyCreditCard = ds.Tables[9];
                CompanyCreditCard.TableName = "CompanyCreditCard";
                DataTable CompanyOthers = ds.Tables[10];
                CompanyOthers.TableName = "CompanyOthers";
                DataTable CCActvity = ds.Tables[12];
                CCActvity.TableName = "CreditCardActivity";

                DataTable CCVolumes = ds.Tables[13];
                CCVolumes.TableName = "CCACCVTotalAmount";
                //GenXML(ds);
                XmlDocument doc = new XmlDocument();
                string requestxml = mt.CreateXML(ds);
                doc.LoadXml(requestxml);
                XmlElement root = doc.DocumentElement;

                //Call the service to fetch the report
                DataviewService.DataviewServiceSoapClient clinet = new DataviewService.DataviewServiceSoapClient();
                var report = clinet.ProcessApplicationXML(root);
                var sr = JsonConvert.SerializeObject(report, Newtonsoft.Json.Formatting.Indented);
                JObject obj = JObject.Parse(sr);

                string Score = obj.SelectToken("Response").SelectToken("Score").ToString();
                string Roundedscore = obj.SelectToken("Response").SelectToken("ScoreRedondeado").ToString();
                string Finalletter = obj.SelectToken("Response").SelectToken("LetraFinal").ToString();

                if (mt.CompleteTask(Convert.ToDouble(Score), Convert.ToDouble(Score), Finalletter, tasktypeid, merchantId, contractId, 0, workflowid, sr, requestxml))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, mt.RetrieveMerchantInformation(merchantId, contractId));
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, mt.RetrieveMerchantInformation(merchantId, contractId));
                }
            }
        }

        [HttpGet]
        [Route("information/{merchantId}")]
        public HttpResponseMessage RetrieveMerchantInformation(Int64 merchantId, Int64 contractId)
        {
            RetrieveMerchantInformation model;
            using (CreditReportTier mt = new CreditReportTier())
            {
                model = (mt.RetrieveMerchantInformation(merchantId, contractId));
                return this.Request.CreateResponse(HttpStatusCode.OK, model);
            }
        }
        #endregion
        #region Offers
        [Route("offers/create")]
        public HttpResponseMessage InsertOffers([FromBody] MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int isCompleted)
        {
            using (CreditReportTier mt = new CreditReportTier())
            {
                if (mt.InsertOffers(model, merchantId, contractId, isCompleted))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [Route("offers/CreateSelect")]
        public HttpResponseMessage InsertSelectOffers([FromBody] MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int isCompleted)
        {
            using (CreditReportTier mt = new CreditReportTier())
            {
                if (mt.InsertSelectOffers(model, merchantId, contractId, isCompleted))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [Route("offers/OfferAcceptance")]
        public HttpResponseMessage OfferAcceptance([FromBody] OfferModel model, Int64 merchantId, Int64 contractId, int isCompleted)
        {
            using (CreditReportTier mt = new CreditReportTier())
            {
                if (mt.OfferAcceptance(model, merchantId, contractId, isCompleted))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpGet]
        [Route("offers/{merchantId}/{contractId}")]
        public HttpResponseMessage RetrieveOffers(Int64 merchantId, Int64 contractId)
        {
            IList<OfferModel> offers;
            using (CreditReportTier mt = new CreditReportTier())
            {
                offers = (mt.RetrieveOffers(merchantId, contractId));

                return this.Request.CreateResponse(HttpStatusCode.OK, offers);

            }
        }

        [HttpGet]
        [Route("offers/{merchantId}")]
        public HttpResponseMessage RetrieveMerchantCreditInformation(Int64 merchantId, Int64 contractId)
        {
            MerchantInformationOfferModel model;
            using (CreditReportTier mt = new CreditReportTier())
            {
                model = (mt.RetrieveMerchantCreditInformation(merchantId, contractId));

                return this.Request.CreateResponse(HttpStatusCode.OK, model);
            }
        }

        [HttpPost]
        [Route("select/{offerId}")]
        public HttpResponseMessage InsertOffers(Int64 offerId, Int64 merchantId, Int64 contractId, int isCompleted)
        {
            using (CreditReportTier mt = new CreditReportTier())
            {
                if (mt.SelectOffer(offerId, merchantId, contractId, isCompleted))
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
        [Route("save")]
        public HttpResponseMessage CompleteTask([FromBody]RetrieveMerchantInformation model, Int64 tasktypeid, Int64 merchantId, Int64 contractId, int iscompleted, int workflowid)
        {
            using (CreditReportTier mt = new CreditReportTier())
            {
                if (mt.CompleteTask(model, tasktypeid, merchantId, contractId, iscompleted, workflowid, model.rawData, string.Empty))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }
        #endregion

        [HttpPost]
        [Route("offers/UpdateOfferEmailFlag/{merchantId}/{contractId}")]
        public HttpResponseMessage UpdateOfferEmailFlag(bool isEmailSent, long offerId, long merchantId, long contractId)
        {
            using (CreditReportTier mt = new CreditReportTier())
            {
                var r = mt.UpdateOfferEmailFlag(isEmailSent, offerId, merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, r);
            }
        }
 
        [HttpPost]
        [Route("offers/UpdateOfferCreationEmailFlag/{merchantId}/{contractId}")]
        public HttpResponseMessage UpdateOfferCreationEmailFlag(bool isEmailSent, long merchantId, long contractId)
        {
            using (CreditReportTier mt = new CreditReportTier())
            {
                var r = mt.UpdateOfferCreationEmailFlag(isEmailSent,  merchantId, contractId);
                return this.Request.CreateResponse(HttpStatusCode.OK, r);
            }
        }
 
    }
}
