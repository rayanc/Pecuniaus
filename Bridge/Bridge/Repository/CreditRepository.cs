using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.BusinessTier;
using Bridge.Models;
using Bridge.DataAccess;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Data;
using Bridge.Models.General;
using System.IO;

namespace Bridge.Repository
{
    public class CreditRepository : ICreditReport, IDisposable
    {
        #region Base Methods
        public bool Create(CreditReportModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(CreditReportModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        public bool InsertCreditreport(string dataxml, long merchantId, long contractId, string isCompleted, byte[] image, string rawdata, int isavailable)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spInsertCreditReports",
                    new
                    {
                        merchantId = merchantId,
                        contractId = contractId,
                        dataxml = dataxml,
                        image = image,
                        rawData = rawdata,
                        isavailable = isavailable

                    });
        }


        public IList<CreditReportModel> RetrieveCreditReport(long merchantId, long contractId)
        {
            IList<CreditReportModel> model = new DataAccess.DataAccess().ExecuteReader<CreditReportModel>("avz_cc_spRetrieveCreditReports", new { merchantId = merchantId, contractId = contractId });

            return model;
        }


        public bool OfferAcceptance(OfferModel model, Int64 merchantId, Int64 contractId, int iscompleted)
        {
            #region Insert list of offers
            string profileXml = string.Empty;

            if (model != null)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = false;
                StringBuilder sbXml = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sbXml, settings);
                writer.WriteStartElement("merchantinfo");



                writer.WriteStartElement("offers");

                writer.WriteAttributeString("insertuserId", Convert.ToString(model.insertuserId));
                writer.WriteAttributeString("loanAmount", Convert.ToString(model.loanAmount));
                writer.WriteAttributeString("monthlyPayment", Convert.ToString(model.monthlyPayment));
                writer.WriteAttributeString("offerAcceptanceDate", Convert.ToString(model.offerAcceptanceDate));
                writer.WriteAttributeString("offerCreationDate", Convert.ToString(model.offerCreationDate));
                writer.WriteAttributeString("offerexpirationDate", Convert.ToString(model.offerexpirationDate));
                writer.WriteAttributeString("offerId", Convert.ToString(model.offerId));
                writer.WriteAttributeString("ownedAmount", Convert.ToString(model.ownedAmount));
                writer.WriteAttributeString("proportion", Convert.ToString(model.proportion));
                writer.WriteAttributeString("retention", Convert.ToString(model.retention));
                writer.WriteAttributeString("salestaken", Convert.ToString(model.salestaken));
                writer.WriteAttributeString("turn", Convert.ToString(model.turn));
                writer.WriteAttributeString("yearly", Convert.ToString(model.yearly));
                writer.WriteAttributeString("IsSelected", Convert.ToString(model.IsSelected));
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.Flush();
                profileXml = sbXml.ToString();

                Logger.LogMessage(profileXml);
                Logger.LogMessage("Contract Id: " + contractId);
                Logger.LogMessage("Merchant Id: " + merchantId);
            }
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_OfferAcceptance", new
            {
                dataxml = profileXml,
                merchantid = merchantId,
                contractid = contractId

            }
                );

            #endregion
        }

        public bool InsertSelectOffers(MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int iscompleted)
        {

            #region Insert list of offers
            string profileXml = string.Empty;

            if (model.offers.Count > 0)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = false;
                StringBuilder sbXml = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sbXml, settings);
                writer.WriteStartElement("merchantinfo");


                foreach (var item in model.offers)
                {
                    writer.WriteStartElement("offers");

                    writer.WriteAttributeString("insertuserId", Convert.ToString(item.insertuserId));
                    writer.WriteAttributeString("loanAmount", Convert.ToString(item.loanAmount));
                    writer.WriteAttributeString("monthlyPayment", Convert.ToString(item.monthlyPayment));
                    writer.WriteAttributeString("offerAcceptanceDate", Convert.ToString(item.offerAcceptanceDate));
                    writer.WriteAttributeString("offerCreationDate", Convert.ToString(item.offerCreationDate));
                    writer.WriteAttributeString("offerexpirationDate", Convert.ToString(item.offerexpirationDate));
                    writer.WriteAttributeString("offerId", Convert.ToString(item.offerId));
                    writer.WriteAttributeString("ownedAmount", Convert.ToString(item.ownedAmount));
                    writer.WriteAttributeString("proportion", Convert.ToString(item.proportion));
                    writer.WriteAttributeString("retention", Convert.ToString(item.retention));
                    writer.WriteAttributeString("salestaken", Convert.ToString(item.salestaken));
                    writer.WriteAttributeString("turn", Convert.ToString(item.turn));
                    writer.WriteAttributeString("yearly", Convert.ToString(item.yearly));
                    writer.WriteAttributeString("IsSelected", Convert.ToString(item.IsSelected));

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
                profileXml = sbXml.ToString();
            }
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_InsertSelectGeneratedOffers", new
            {
                dataxml = profileXml,
                merchantid = merchantId,
                contractid = contractId,
                annualSales = model.yearlysales,
                avgmccv = model.avgmccv,
                reason = string.IsNullOrEmpty(model.reason) ? "" : model.reason,
                ismccvupdated = model.ismccvupdated
            }
                );

            #endregion
        }


        /// <summary>
        /// Insert the offers in the system
        /// </summary>
        /// <param name="model"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public bool InsertOffers(MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int iscompleted)
        {

            #region Insert list of offers
            string profileXml = string.Empty;

            if (model.offers.Count > 0)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = false;
                StringBuilder sbXml = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sbXml, settings);
                writer.WriteStartElement("merchantinfo");


                foreach (var item in model.offers)
                {
                    writer.WriteStartElement("offers");

                    writer.WriteAttributeString("insertuserId", Convert.ToString(item.insertuserId));
                    writer.WriteAttributeString("loanAmount", Convert.ToString(item.loanAmount));
                    writer.WriteAttributeString("monthlyPayment", Convert.ToString(item.monthlyPayment));
                    //writer.WriteAttributeString("offerAcceptanceDate", Convert.ToString(item.offerAcceptanceDate));
                    //writer.WriteAttributeString("offerCreationDate", Convert.ToString(item.offerCreationDate));
                    //writer.WriteAttributeString("offerexpirationDate", Convert.ToString(item.offerexpirationDate));
                    writer.WriteAttributeString("offerId", Convert.ToString(item.offerId));
                    writer.WriteAttributeString("ownedAmount", Convert.ToString(item.ownedAmount));
                    writer.WriteAttributeString("proportion", Convert.ToString(item.proportion));
                    writer.WriteAttributeString("retention", Convert.ToString(item.retention));
                    writer.WriteAttributeString("salestaken", Convert.ToString(item.salestaken));
                    writer.WriteAttributeString("turn", Convert.ToString(item.turn));
                    writer.WriteAttributeString("yearly", Convert.ToString(item.yearly));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
                profileXml = sbXml.ToString();
            }
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_InsertGeneratedOffers", new
            {
                dataxml = profileXml,
                merchantid = merchantId,
                contractid = contractId,
                annualSales = model.yearlysales,
                avgmccv = model.avgmccv,
                reason = string.IsNullOrEmpty(model.reason) ? "" : model.reason,
                ismccvupdated = model.ismccvupdated
            }
                );

            #endregion


        }
        /// <summary>
        /// Retrieve the generated offers
        /// </summary>
        /// <param name="model"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public IList<OfferModel> RetrieveOffers(long merchantId, Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<OfferModel>("avz_cc_spRetrieveOffers", new
            {
                merchantId = merchantId,
                contractId = contractId

            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="merhantId"></param>
        /// <returns></returns>
        public MerchantInformationOfferModel RetrieveMerchantCreditInformation(Int64 merchantId, Int64 contractId)
        {
            MerchantInformationOfferModel model = new MerchantInformationOfferModel();
            DataSet merchantInformation;
            long maxtrun = 0;
            merchantInformation = new DataAccess.DataAccess().ExecuteDataSet("avz_cc_spRetrieveMerchantCreditInformation", new { merchantId = merchantId, contractId = contractId });
            if (merchantInformation.Tables[0].Rows.Count > 0)
            {
                Address adr = new Address();
                if (!string.IsNullOrEmpty(merchantInformation.Tables[0].Rows[0]["averagemccv"].ToString()))
                    model.avgmccv = Convert.ToDouble(merchantInformation.Tables[0].Rows[0]["averagemccv"].ToString());
                if (!string.IsNullOrEmpty(merchantInformation.Tables[0].Rows[0]["grossAnnualSales"].ToString()))
                    model.yearlysales = Convert.ToDouble(merchantInformation.Tables[0].Rows[0]["grossAnnualSales"].ToString());
                if (!string.IsNullOrEmpty(merchantInformation.Tables[0].Rows[0]["score"].ToString()))
                    model.score = Convert.ToDouble(merchantInformation.Tables[0].Rows[0]["score"].ToString());
                if (!string.IsNullOrEmpty(merchantInformation.Tables[0].Rows[0]["maxprice"].ToString()))
                {
                    model.maxturn = Convert.ToInt64(merchantInformation.Tables[0].Rows[0]["maxprice"].ToString());
                    maxtrun = Convert.ToInt64(merchantInformation.Tables[0].Rows[0]["maxprice"].ToString());
                }
                if (!string.IsNullOrEmpty(merchantInformation.Tables[0].Rows[0]["salestaken"].ToString()))
                {
                    model.salestaken = Convert.ToInt64(merchantInformation.Tables[0].Rows[0]["salestaken"].ToString());
                }
                model.reason = merchantInformation.Tables[0].Rows[0]["reason"].ToString();

                model.finalscore = merchantInformation.Tables[0].Rows[0]["finalscore"].ToString();
                model.businessName = merchantInformation.Tables[0].Rows[0]["businessName"].ToString();
                if (!string.IsNullOrEmpty(merchantInformation.Tables[0].Rows[0]["IsOffersSent"].ToString()))
                {
                    model.IsOffersEmailSent = Convert.ToBoolean(merchantInformation.Tables[0].Rows[0]["IsOffersSent"]);
                }
                adr.addressLine1 = merchantInformation.Tables[0].Rows[0]["address1"].ToString();
                adr.addressLine2 = merchantInformation.Tables[0].Rows[0]["address2"].ToString();
                adr.country = merchantInformation.Tables[0].Rows[0]["country"].ToString();
                adr.state = merchantInformation.Tables[0].Rows[0]["state"].ToString();
                adr.city = merchantInformation.Tables[0].Rows[0]["city"].ToString();
                model.address = adr;
            }
            if (merchantInformation.Tables[1].Rows.Count > 0)
            {
                List<SalesRepresentative> sales = new List<SalesRepresentative>();
                for (int i = 0; i < merchantInformation.Tables[1].Rows.Count; i++)
                {
                    SalesRepresentative rep = new SalesRepresentative();
                    rep.email = merchantInformation.Tables[1].Rows[0]["email"].ToString();
                    rep.salesrepname = merchantInformation.Tables[1].Rows[0]["name"].ToString();
                    sales.Add(rep);
                }
                model.salesRepresentative = sales;
            }

            if (merchantInformation.Tables[2].Rows.Count > 0)
            {
                List<MinPrice> price = new List<MinPrice>();
                for (int i = 0; i < merchantInformation.Tables[2].Rows.Count; i++)
                {
                    MinPrice pri = new MinPrice();
                    pri.score = merchantInformation.Tables[2].Rows[i]["score"].ToString();
                    pri.maxtime = Convert.ToInt64(merchantInformation.Tables[2].Rows[i]["MaxTime"].ToString());
                    pri.minprice = Convert.ToDouble(merchantInformation.Tables[2].Rows[i]["Minprice"].ToString());
                    price.Add(pri);
                }
                model.turnminprice = price;
            }

            IList<OfferModel> offers = RetrieveOffers(merchantId, contractId);
            //foreach (var item in offers)
            //{
            //    item.maxprice = maxtrun;
            //}
            //if (offers.Count == 0)
            //{
            //    OfferModel of = new OfferModel();
            //    of.maxprice = maxtrun;
            //    offers.Add(of);
            //}
            model.offers = offers.ToList();


            return model;
        }

        public bool SelectOffer(Int64 offerId, Int64 merchantId, Int64 contractId, int isCompleted)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spSelectOffer", new
            {
                offerId = offerId,
                merchantId = merchantId,
                contractId = contractId,
                isCompleted = isCompleted

            });
        }
        public RetrieveMerchantInformation RetrieveMerchantInformation(Int64 merchantId, Int64 contractId)
        {
            return new DataAccess.DataAccess().ExecuteReader<RetrieveMerchantInformation>("avz_cc_spRetriveReportInformation", new { merchantId = merchantId, contractId = contractId }).FirstOrDefault();
        }
        public DataSet RetrieveInformationforScorin(Int64 merchantId)
        {

            return new DataAccess.DataAccess().ExecuteDataSet("avz_mrc_spretrieveMerchantEvaluation", new { merchantId = merchantId });
        }

        public bool CompleteTask(RetrieveMerchantInformation model, Int64 tasktypeid, Int64 merchantId, Int64 contractId, int iscompleted, int workflowid, string rawdata, string requestxml)
        {

            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spSaveEvaluationTask", new
            {
                tasktypeid = tasktypeid,
                merchantId = merchantId,
                contractId = contractId,
                score = Convert.ToDouble(model.score),
                roundedscore = Convert.ToDouble(model.roundedscore),
                finalletter = model.finalletter,
                iscompleted = iscompleted,
                workflowid = workflowid,
                rawdata = string.IsNullOrEmpty(rawdata) ? "" : rawdata,
                requestxml = requestxml
            });
        }
        public bool CompleteTask(double score, double roundedscore, string finalletter, Int64 tasktypeid, Int64 merchantId, Int64 contractId, int iscompleted, int workflowid, string rawdata, string requestxml)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spSaveEvaluationTask", new
            {
                tasktypeid = tasktypeid,
                merchantId = merchantId,
                contractId = contractId,
                score = score,
                roundedscore = roundedscore,
                finalletter = finalletter,
                iscompleted = iscompleted,
                workflowid = workflowid,
                rawdata = rawdata,
                requestxml = requestxml
            });
        }

        public bool UpdateOfferEmailFlag(bool isEmailSent, long offerId, long merchantId, long contractId)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spUpdateOfferEamilFlag", new
            {
                isEmailSent = isEmailSent,
                offerId = offerId,
                merchantId = merchantId,
                contractId = contractId,
            });
        }

        public bool UpdateOfferCreationEmailFlag(bool isEmailSent, long merchantId, long contractId)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spUpdateOfferCreationEamilFlag", new
            {
                isEmailSent = isEmailSent,
                merchantId = merchantId,
                contractId = contractId,
            });
        }



    }
}