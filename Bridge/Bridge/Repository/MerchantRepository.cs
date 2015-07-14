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
using Bridge.Models.Merchant;

namespace Bridge.Repository
{
    public class MerchantRepository : IMerchant, IDisposable
    {
        #region Merchants

        public MerchantsModel ListMerchants(Int64 merchantId)
        {

            IList<MerchantsModel> merchantsList = new DataAccess.DataAccess().ExecuteReader<MerchantsModel>("avz_mrc_spRetrieveMerchant", new { merchantId = merchantId });
            MerchantsModel merchant = merchantsList.FirstOrDefault();
            return merchant;

        }

        public IList<EmailModel> RetriveEmails(Int64 SalesRepId)
        {
            IList<EmailModel> email = new DataAccess.DataAccess().ExecuteReader<EmailModel>("AVZ_spGetEmails", new
            {
                SalesRep = SalesRepId
            });
            return email;

        }

        public IList<SearchResultsModel> ListMerchantsSeach(string businessName, string rnc, string legalName, string ownerName, long? merchantId, long? contractId, long? workflowId, long? statusId, long? processornbr, string processorName, long? tasktype, Int16? showTemp, string SearchType, Int64 assignedUserId)
        {
            IList<SearchResultsModel> merchantsList = new DataAccess.DataAccess().ExecuteReader<SearchResultsModel>("avz_mrc_spRetrievemerchantSearchResults", new
            {
                merchantId = merchantId,
                businessName = businessName,
                rnc = rnc,
                legalName = legalName,
                ownerName = ownerName,
                contractId = contractId,
                workflowId = workflowId,
                statusId = statusId,
                processornbr = processornbr,
                processorName = processorName,
                tasktype = tasktype,
                showTemp = showTemp,
                SearchType = SearchType,
                AssignedUserId = assignedUserId
            });
            return merchantsList;

        }

        /// <summary>
        /// Retrieve merchant from the db
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        public IList<MerchantsModel> ListMerchants(string businessName, string rnc, string legalName, string ownerName, Int64? merchantId, Int64? contractId,
            Int64? workflowId, Int64? statusId, Int64? processornbr, string processorName, Int64? tasktype)
        {
            IList<MerchantsModel> merchantsList = new DataAccess.DataAccess().ExecuteReader<MerchantsModel>("avz_mrc_spRetrievemerchantSearch",
                 new
                 {
                     rnc = rnc == null ? string.Empty : rnc,
                     businessName = businessName == null ? string.Empty : businessName,
                     legalName = legalName == null ? string.Empty : legalName,
                     ownerName = ownerName == null ? string.Empty : ownerName,
                     merchantId = merchantId == null ? 0 : merchantId,
                     contractId = contractId == null ? 0 : contractId,
                     workflowId = workflowId == null ? 0 : workflowId,
                     statusId = statusId == null ? 0 : statusId,
                     processornbr = processornbr == null ? 0 : processornbr,
                     processorName = processorName == null ? string.Empty : processorName,
                     taskType = tasktype == null ? 0 : tasktype,


                 });

            return merchantsList;
        }
        public IList<MerchantTempModel> RetriveMerchantSalesForce(string businessName, string rnc, string legalName, string ownerName)
        {
            //avz_mrc_spRetrieveSalesForce

            IList<MerchantTempModel> merchantsList = new DataAccess.DataAccess().ExecuteReader<MerchantTempModel>(" avz_mrc_spRetrievemerchantSearch",
                new
                {
                    rnc = rnc == null ? string.Empty : rnc,
                    businessName = businessName == null ? string.Empty : businessName,
                    legalName = legalName == null ? string.Empty : legalName,
                    ownerName = ownerName == null ? string.Empty : ownerName,
                    merchantId = 0,
                    contractId = 0,
                    workflowId = 0,
                    statusId = 0,
                    processornbr = 0,
                    processorName = string.Empty,
                    taskType = 0
                });

            return merchantsList;
        }
        /// <summary>
        /// Retrieve merchant from the db
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        public IList<MerchantTempModel> ListMerchantQueue()
        {
            IList<MerchantTempModel> merchantsList = new DataAccess.DataAccess().ExecuteReader<MerchantTempModel>("avz_mrc_spRetrieveTempQueue", null);

            return merchantsList;

        }
        /// <summary>
        /// Retrieve merchant from the db
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        public IList<OwnerModel> ListMerchantOwner(Int64 merchantId)
        {
            IList<OwnerModel> ownerList = new DataAccess.DataAccess().ExecuteReader<OwnerModel>("avz_mrc_spRetrieveOwner", new { merchantId = merchantId });

            return ownerList;

        }

        /// <summary>
        /// Retrieve merchant from the db
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="businessName"></param>
        /// <param name="rnc"></param>
        /// <returns></returns>
        public IList<MerchantProfile> ListCreditCardProfiles(Int64 merchantId)
        {
            IList<MerchantProfile> ccList = new DataAccess.DataAccess().ExecuteReader<MerchantProfile>("avz_mrc_spretrieveCreditCardActivity", new { merchantId = merchantId });

            return ccList;

        }

        /// <summary>
        /// Create a model based merchant.....
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Create(MerchantsModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_mrc_spInsertMerchant", new
               {
                   companyId = entity.companyId,
                   legalName = entity.legalName,
                   businessName = entity.businessName,
                   workflowId = 1,
                   salesRepId = entity.salesRepId,
                   businessStartDate = entity.businessStartDate,
                   industryTypeId = entity.industryTypeId,
                   businessTypeId = entity.businessTypeId,
                   insertUserId = 1,
                   insertDate = DateTime.Now,
                   ownerId = 1,
                   cNetProcessorId = entity.CNetProcessorId,
                   vNetProcessoId = entity.VNetProcessoId,
                   businessWebSite = entity.businessWebSite,
                   rentFlag = entity.rentFlag,
                   adressTypeId = 1,
                   address1 = entity.address.addressLine1,
                   address2 = entity.address.addressLine2,
                   country = "",
                   city = entity.address.city,
                   State = entity.address.state,
                   phone1 = entity.address.phone1,
                   phone2 = entity.address.phone2,
                   phone3 = string.Empty,
                   email1 = entity.address.email,
                   email2 = "",
                   email3 = ""
               }
                   );

        }

        public bool Update(MerchantsModel entity)
        {
            return true;
        }
        /// <summary>
        /// Save the merchant in the temp table and also if isCompleted is 1 then a new merchant would be created and the status would be inactive.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="merchantId"></param>
        /// <param name="isCompleted"></param>
        /// <returns></returns>
        public Int64 Save(MerchantTempModel entity, Int64 merchantId, string isCompleted)
        {
            Int64 _merchantId = 0;
            if (merchantId != 0)
            {
                _merchantId = new DataAccess.DataAccess().ExecuteNonQuery("avz_mrc_spUpdateMerchantTemp", new
               {
                   merchantId = merchantId,
                   businessName = entity.businessName,
                   businessStartDate = entity.businessStartDate,
                   legalName = entity.legalName,
                   industryTypeid = entity.industryTypeid,
                   businessTypeId = entity.businessTypeId,
                   rnc = entity.rnc,
                   salesRepId = entity.salesRepId,
                   modifyuserId = entity.userId,
                   isCompleted = isCompleted,
                   isDuplicate = entity.IsDuplicate == true ? "1" : "0",
               }, new { merchantId = 0 });
            }
            else
            {


                _merchantId = new DataAccess.DataAccess().ExecuteNonQuery("avz_mrc_spInsertMerchantTemp", new
              {
                  merchantId = 0,
                  businessName = entity.businessName,
                  businessStartDate = entity.businessStartDate,
                  legalName = entity.legalName,
                  industryTypeid = entity.industryTypeid,
                  businessTypeId = entity.businessTypeId,
                  rnc = entity.rnc,
                  salesRepId = entity.salesRepId,
                  insertDate = DateTime.Now,
                  insertUserId = entity.userId,
                  isCompleted = isCompleted,
              }, new { merchantId = 0 });

            }
            return _merchantId;
        }
        public bool SaveDE(MerchantDEModel model, long merchantId, string isCompleted)
        {

            // Create an xml for the request recieved and send it to the dbfor processing

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;
            StringBuilder sbXml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sbXml, settings);
            writer.WriteStartElement("merchantinfo");
            writer.WriteStartElement("merchantbasicinfo");
            writer.WriteAttributeString("merchantId", Convert.ToString(merchantId));
            //writer.WriteAttributeString("merchantName", Convert.ToString(model.merchantName));
            writer.WriteAttributeString("mrnc", Convert.ToString(model.rnc));
            writer.WriteAttributeString("mbusinessName", model.businessName);
            writer.WriteAttributeString("mlegalName", Convert.ToString(model.legalName));
            writer.WriteAttributeString("mbusinessStartDate", String.Format("{0:yyyy-MM-dd}", model.businessStartDate));
            writer.WriteAttributeString("mfirstProcessedDate", String.Format("{0:yyyy-MM-dd}", model.firstProcessedDate));
            //writer.WriteAttributeString("businessWebSite", Convert.ToString(model.businessWebSite));            
            //writer.WriteAttributeString("mccCode", Convert.ToString(model.industryTypeId));            
            writer.WriteAttributeString("mrentAmount", Convert.ToString(model.rentAmount));
            writer.WriteAttributeString("mannualSales", Convert.ToString(model.annualSales));
            writer.WriteAttributeString("mindustryTypeId", Convert.ToString(model.industryTypeId));
            writer.WriteAttributeString("mbusinessTypeId", Convert.ToString(model.businessTypeId));
            writer.WriteAttributeString("mpropertyType", Convert.ToString(model.propertyType));
            // writer.WriteAttributeString("processorCompany", Convert.ToString(model.processorCompany));
            writer.WriteAttributeString("maffiliationNumber", Convert.ToString(model.affiliationNumber));
            //writer.WriteAttributeString("cNetProcessorId", Convert.ToString(model.cNetProcessorId));
            //writer.WriteAttributeString("vNetProcessoId", Convert.ToString(model.vNetProcessoId));
            writer.WriteAttributeString("mbankId", Convert.ToString(model.BankID));
            writer.WriteAttributeString("maccountNumber", Convert.ToString(model.accountNumber));
            writer.WriteAttributeString("maccountName", Convert.ToString(model.accountName));
            writer.WriteAttributeString("maddressId", Convert.ToString(model.address.addressId));
            writer.WriteAttributeString("maddressLine1", Convert.ToString(model.address.addressLine1));
            writer.WriteAttributeString("maddressLine2", Convert.ToString(model.address.addressLine2));
            writer.WriteAttributeString("mcity", Convert.ToString(model.address.city));
            writer.WriteAttributeString("mcountry", Convert.ToString(model.address.country));
            writer.WriteAttributeString("mphone1", Convert.ToString(model.address.phone1));
            writer.WriteAttributeString("mphone2", Convert.ToString(model.address.phone2));
            writer.WriteAttributeString("memail", Convert.ToString(model.address.email));
            writer.WriteAttributeString("mzipcode", Convert.ToString(model.address.zipId));
            writer.WriteAttributeString("mstateId", Convert.ToString(model.address.stateId));
            writer.WriteAttributeString("mloanAmountRequired", Convert.ToString(model.loanAmountRequired));
            writer.WriteAttributeString("mcontractId", Convert.ToString(model.contractId));
            writer.WriteAttributeString("mprimarySalesRepId", Convert.ToString(model.PsalesRepId));
            writer.WriteAttributeString("msecondarySalesRepId", Convert.ToString(model.SecsalesRepId));
            writer.WriteAttributeString("mbankcode", Convert.ToString(model.BankCode));
            writer.WriteAttributeString("mtypeofadvanceid", Convert.ToString(model.TypeOfAdvanceId));
            writer.WriteAttributeString("mAnnualSalesCalcFile", Convert.ToString(model.AnnualSalesCalcFile));
            if (model.LandlordInformation != null)
            {
                writer.WriteAttributeString("mcompanyname", Convert.ToString(model.LandlordInformation.CompanyName));
                writer.WriteAttributeString("mfirstname", Convert.ToString(model.LandlordInformation.FirstName));
                writer.WriteAttributeString("mlastname", Convert.ToString(model.LandlordInformation.LastName));
                writer.WriteAttributeString("mphonenumber", Convert.ToString(model.LandlordInformation.PhoneNumber));
            }
            else
            {
                writer.WriteAttributeString("mcompanyname", "");
                writer.WriteAttributeString("mfirstname", "");
                writer.WriteAttributeString("mlastname", "");
                writer.WriteAttributeString("mphonenumber", "");

            }
            writer.WriteEndElement();

            writer.WriteStartElement("owners");

            foreach (var item in model.owners)
            {

                writer.WriteStartElement("owner");
                writer.WriteAttributeString("ownerId", Convert.ToString(item.ownerId));
                writer.WriteAttributeString("contactId", Convert.ToString(item.contactId));
                writer.WriteAttributeString("ownerFirstName", Convert.ToString(item.ownerFirstName));
                writer.WriteAttributeString("ownerLastName", Convert.ToString(item.ownerLastName));
                writer.WriteAttributeString("ssn", Convert.ToString(item.ssn));
                writer.WriteAttributeString("ownerDOB", String.Format("{0:yyyy-MM-dd}", item.ownerDOB));
                writer.WriteAttributeString("cityId", Convert.ToString(item.cityId));
                writer.WriteAttributeString("city", Convert.ToString(item.city));
                writer.WriteAttributeString("stateId", Convert.ToString(item.stateId));
                writer.WriteAttributeString("state", Convert.ToString(item.state));
                writer.WriteAttributeString("country", Convert.ToString(item.country));
                writer.WriteAttributeString("zipId", Convert.ToString(item.zipId));
                writer.WriteAttributeString("zip", Convert.ToString(item.zip));
                writer.WriteAttributeString("phone1", Convert.ToString(item.phone1));
                writer.WriteAttributeString("phone2", Convert.ToString(item.CellNumber));
                writer.WriteAttributeString("fax", Convert.ToString(item.fax));
                writer.WriteAttributeString("addressLine1", Convert.ToString(item.addressLine1));
                writer.WriteAttributeString("addressLine2", Convert.ToString(item.addressLine2));
                writer.WriteAttributeString("addressId", Convert.ToString(item.addressId));
                writer.WriteAttributeString("email", Convert.ToString(item.email));
                writer.WriteAttributeString("passportNumber", Convert.ToString(item.PassportNumber));
                writer.WriteAttributeString("authorised", Convert.ToString(item.Authorized));


                writer.WriteEndElement();

            }

            writer.WriteEndElement();

            //Processor

            writer.WriteStartElement("processors");

            foreach (var item in model.processor)
            {

                writer.WriteStartElement("processor");

                writer.WriteAttributeString("processorId", Convert.ToString(item.processorId));
                writer.WriteAttributeString("processorNumber", Convert.ToString(item.processorNumber));
                writer.WriteAttributeString("processorTypeId", Convert.ToString(item.processorTypeId));
                writer.WriteAttributeString("firstProcessedDate", String.Format("{0:yyyy-MM-dd}", item.firstprocessedDate));
                writer.WriteEndElement();

            }

            writer.WriteEndElement();

            //processor ends


            //Trade Reference


            if (model.TradeReference != null)
            {
                writer.WriteStartElement("tradereferences");

                foreach (var item in model.TradeReference)
                {

                    writer.WriteStartElement("tradereference");

                    writer.WriteAttributeString("referenceid", Convert.ToString(item.ReferenceId));
                    writer.WriteAttributeString("referencename", Convert.ToString(item.ReferenceName));
                    writer.WriteAttributeString("rphonenumber", Convert.ToString(item.PhoneNumber));
                    writer.WriteEndElement();

                }

                writer.WriteEndElement();
            }

            //Trade Reference ends

            //Bank Statement
            if (model.BankStatements != null)
            {
                writer.WriteStartElement("bankstatements");

                foreach (var item in model.BankStatements)
                {

                    writer.WriteStartElement("bankstatement");

                    writer.WriteAttributeString("statementid", Convert.ToString(item.StatementId));
                    writer.WriteAttributeString("statementmonthid", Convert.ToString(item.StatementMonthId));
                    writer.WriteAttributeString("statementyear", Convert.ToString(item.StatementYear));
                    writer.WriteAttributeString("bsamount", Convert.ToString(item.Amount));
                    writer.WriteEndElement();

                }

                writer.WriteEndElement();
            }

            //Bank Statement ends

            writer.WriteEndElement();
            writer.Flush();
            return new DataAccess.DataAccess().ExecuteNonQuery("avz_mrc_spSaveDataEntryTask", new
        {
            providedXml = sbXml.ToString(),
            merchantId = merchantId,
            iscompleted = isCompleted == null ? "0" : isCompleted
        });

        }

        public bool Remove(int id)
        {
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public int Complete(MerchantTempModel model, long merchantId)
        {
            throw new NotImplementedException();
        }
        public MerchantsAdditionalInfo RetrieveMerchantDataEntry(Int64 merchantId)
        {
            MerchantsAdditionalInfo merchantsdemodel = null;
            DataSet merchantsinfo = (new DataAccess.DataAccess()).ExecuteDataSet("avz_mrc_spretrieveMerchantDataEntry",
                               new
                               {
                                   merchantId = merchantId
                               });
            if (merchantsinfo.Tables[0].Rows.Count > 0)
            {
                merchantsdemodel = new MerchantsAdditionalInfo();
                merchantsdemodel.merchantName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["merchantName"]);
                merchantsdemodel.legalName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["legalName"]);
                merchantsdemodel.businessName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["businessName"]);
                merchantsdemodel.businessTypeId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["businessTypeId"]);
                merchantsdemodel.businessWebSite = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["businessWebSite"]);
                merchantsdemodel.rentAmount = Convert.ToDouble(merchantsinfo.Tables[0].Rows[0]["rentAmount"]);
                merchantsdemodel.annualSales = string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["annualSales"].ToString()) ? 0 : Convert.ToDouble(merchantsinfo.Tables[0].Rows[0]["annualSales"]);
                merchantsdemodel.loanAmountRequired = string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["loanAmountRequired"].ToString()) ? 0 : Convert.ToDouble(merchantsinfo.Tables[0].Rows[0]["loanAmountRequired"]);
                merchantsdemodel.rnc = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["rnc"]);
                merchantsdemodel.industryTypeId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["industryTypeId"]);
                merchantsdemodel.assignedSales = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["assignedSales"]);
                merchantsdemodel.salesRepId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["salesRepId"]);
                merchantsdemodel.taskName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["taskName"]);
                merchantsdemodel.workFlowName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["workFlowName"]);
                if (merchantsinfo.Tables[0].Rows[0]["workflowId"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["workflowId"].ToString() != "")
                    merchantsdemodel.workflowId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["workflowId"]);
                merchantsdemodel.assigneduserId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["assigneduserId"]);
                if (merchantsinfo.Tables[0].Rows[0]["assignedDate"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["assignedDate"].ToString() != "")
                    merchantsdemodel.assignedDate = Convert.ToDateTime(merchantsinfo.Tables[0].Rows[0]["assignedDate"]);
                if (merchantsinfo.Tables[0].Rows[0]["businessStartDate"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["businessStartDate"].ToString() != "")
                    merchantsdemodel.businessStartDate = Convert.ToDateTime(merchantsinfo.Tables[0].Rows[0]["businessStartDate"]);
                if (merchantsinfo.Tables[0].Rows[0]["merchantId"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["merchantId"].ToString() != "")
                    merchantsdemodel.merchantId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["merchantId"]);
                if (merchantsinfo.Tables[0].Rows[0]["bankId"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["bankId"].ToString() != "")
                    merchantsdemodel.BankID = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["bankId"]);
                merchantsdemodel.propertyType = string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["propertyType"].ToString()) ? 0 : Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["propertyType"]);
                merchantsdemodel.accountNumber = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["accountNumber"]);
                merchantsdemodel.accountName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["accountName"]);
                merchantsdemodel.BankCode = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["bankcode"]);
                merchantsdemodel.AnnualSalesCalcFile = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["AnnualSalesCalcFile"]);
                merchantsdemodel.contractId = Convert.ToInt64(string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["contractId"].ToString()) == false ? merchantsinfo.Tables[0].Rows[0]["contractId"].ToString() : "0");
                if (merchantsinfo.Tables[0].Rows[0]["primarySalesRepId"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["primarySalesRepId"].ToString() != "")
                    merchantsdemodel.PsalesRepId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["primarySalesRepId"]);
                if (merchantsinfo.Tables[0].Rows[0]["secondarySalesRepId"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["secondarySalesRepId"].ToString() != "")
                    merchantsdemodel.SecsalesRepId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["secondarySalesRepId"]);
                if (merchantsinfo.Tables[0].Rows[0]["typeofadvances"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["typeofadvances"].ToString() != "")
                    merchantsdemodel.TypeOfAdvanceId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["typeofadvances"]);


                Address merchantsaddress = new Address();
                merchantsaddress.phone1 = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["phone1"]);
                merchantsaddress.phone2 = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["phone2"]);
                merchantsaddress.addressLine1 = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["addressLine1"]);
                merchantsaddress.addressLine2 = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["addressLine2"]);
                merchantsaddress.country = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["country"]);
                merchantsaddress.city = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["city"]);
                merchantsaddress.state = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["state"]);
                merchantsaddress.stateId = string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["stateId"].ToString()) ? 0 : Convert.ToInt64(merchantsinfo.Tables[0].Rows[0]["stateId"]);
                merchantsaddress.email = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["email"]);
                merchantsaddress.zipId = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["zipId"]);
                if (merchantsinfo.Tables[0].Rows[0]["addressId"].ToString() != null && merchantsinfo.Tables[0].Rows[0]["addressId"].ToString() != "")
                    merchantsaddress.addressId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["addressId"]);
                merchantsdemodel.address = merchantsaddress;


                // Owner's Information
                if (merchantsinfo.Tables[1].Rows.Count > 0)
                {
                    List<OwnerModel> ownermodellist = new List<OwnerModel>();
                    for (int i = 0; i < merchantsinfo.Tables[1].Rows.Count; i++)
                    {
                        OwnerModel ownermodel = new OwnerModel();
                        if (merchantsinfo.Tables[1].Rows[i]["contactId"].ToString() != null && merchantsinfo.Tables[1].Rows[i]["contactId"].ToString() != "")
                            ownermodel.contactId = Convert.ToInt64(merchantsinfo.Tables[1].Rows[i]["contactId"]);
                        if (merchantsinfo.Tables[1].Rows[i]["ownerId"].ToString() != null && merchantsinfo.Tables[1].Rows[i]["ownerId"].ToString() != "")
                            ownermodel.ownerId = Convert.ToInt64(merchantsinfo.Tables[1].Rows[i]["ownerId"]);
                        if (merchantsinfo.Tables[1].Rows[i]["addressId"].ToString() != null && merchantsinfo.Tables[1].Rows[i]["addressId"].ToString() != "")
                            ownermodel.addressId = Convert.ToInt64(merchantsinfo.Tables[1].Rows[i]["addressId"]);
                        ownermodel.merchantId = merchantId;
                        ownermodel.ownerFirstName = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["ownerFirstName"]);
                        ownermodel.ownerLastName = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["ownerLastName"]);
                        if (merchantsinfo.Tables[1].Rows[i]["ownerDOB"].ToString() != null && merchantsinfo.Tables[1].Rows[i]["ownerDOB"].ToString() != "")
                            ownermodel.ownerDOB = Convert.ToDateTime(merchantsinfo.Tables[1].Rows[i]["ownerDOB"]);
                        ownermodel.PassportNumber = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["passportnbr"]);
                        ownermodel.phone1 = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["phone1"]);
                        ownermodel.CellNumber = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["phone2"]);
                        ownermodel.ssn = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["ssn"]);
                        ownermodel.addressLine1 = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["addressLine1"]);
                        ownermodel.addressLine2 = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["addressLine2"]);
                        ownermodel.country = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["country"]);
                        ownermodel.city = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["city"]);
                        ownermodel.state = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["state"]);
                        ownermodel.stateId = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["stateId"]);
                        ownermodel.zip = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["zip"]);
                        ownermodel.email = Convert.ToString(merchantsinfo.Tables[1].Rows[i]["email"]);
                        ownermodel.Authorized = Convert.ToBoolean(merchantsinfo.Tables[1].Rows[i]["IsAuthorized"]);
                        ownermodellist.Add(ownermodel);


                    }
                    merchantsdemodel.owners = ownermodellist;
                }
                //Processor Information
                if (merchantsinfo.Tables[2].Rows.Count > 0)
                {
                    List<MerchantProcessorModel> processors = new List<MerchantProcessorModel>();
                    for (int i = 0; i < merchantsinfo.Tables[2].Rows.Count; i++)
                    {
                        MerchantProcessorModel processormodel = new MerchantProcessorModel();
                        if (merchantsinfo.Tables[2].Rows[i]["processorId"].ToString() != null && merchantsinfo.Tables[2].Rows[i]["processorId"].ToString() != "")
                            processormodel.processorId = Convert.ToInt32(merchantsinfo.Tables[2].Rows[i]["processorId"]);
                        if (merchantsinfo.Tables[2].Rows[i]["processorTypeId"].ToString() != null && merchantsinfo.Tables[2].Rows[i]["processorTypeId"].ToString() != "")
                            processormodel.processorTypeId = Convert.ToInt32(merchantsinfo.Tables[2].Rows[i]["processorTypeId"]);
                        processormodel.processorName = Convert.ToString(merchantsinfo.Tables[2].Rows[i]["processorname"]);
                        if (merchantsinfo.Tables[2].Rows[i]["processorNumber"].ToString() != null && merchantsinfo.Tables[2].Rows[i]["processorNumber"].ToString() != "")
                            processormodel.processorNumber = Convert.ToString(merchantsinfo.Tables[2].Rows[i]["processorNumber"]);
                        if (merchantsinfo.Tables[2].Rows[i]["firstprocessedDate"].ToString() != null && merchantsinfo.Tables[2].Rows[i]["firstprocessedDate"].ToString() != "")
                            processormodel.firstprocessedDate = Convert.ToDateTime(merchantsinfo.Tables[2].Rows[i]["firstprocessedDate"]);
                        processors.Add(processormodel);

                    }
                    merchantsdemodel.processor = processors;
                }

                if (merchantsinfo.Tables[3].Rows.Count > 0)
                {
                    List<MerchantBankStatement> statements = new List<MerchantBankStatement>();
                    for (int i = 0; i < merchantsinfo.Tables[3].Rows.Count; i++)
                    {
                        MerchantBankStatement statementmodel = new MerchantBankStatement();
                        if (merchantsinfo.Tables[3].Rows[i]["StatementId"].ToString() != null && merchantsinfo.Tables[3].Rows[i]["StatementId"].ToString() != "")
                            statementmodel.StatementId = Convert.ToInt32(merchantsinfo.Tables[3].Rows[i]["StatementId"]);
                        if (merchantsinfo.Tables[3].Rows[i]["StatementMonthId"].ToString() != null && merchantsinfo.Tables[3].Rows[i]["StatementMonthId"].ToString() != "")
                            statementmodel.StatementMonthId = Convert.ToInt32(merchantsinfo.Tables[3].Rows[i]["StatementMonthId"]);
                        statementmodel.StatementYear = Convert.ToString(merchantsinfo.Tables[3].Rows[i]["Statementyear"]);
                        if (merchantsinfo.Tables[3].Rows[i]["amount"].ToString() != null && merchantsinfo.Tables[3].Rows[i]["amount"].ToString() != "")
                            statementmodel.Amount = Convert.ToDouble(merchantsinfo.Tables[3].Rows[i]["amount"]);

                        statements.Add(statementmodel);

                    }
                    merchantsdemodel.BankStatements = statements;
                }

                if (merchantsinfo.Tables[4].Rows.Count > 0)
                {
                    int i = 0;
                    MerchantLandLord LandlordInformationmodel = new MerchantLandLord();
                    if (merchantsinfo.Tables[4].Rows[i]["landlordcompany"].ToString() != null && merchantsinfo.Tables[4].Rows[i]["landlordcompany"].ToString() != "")
                        LandlordInformationmodel.CompanyName = Convert.ToString(merchantsinfo.Tables[4].Rows[i]["landlordcompany"]);
                    if (merchantsinfo.Tables[4].Rows[i]["landlordId"].ToString() != null && merchantsinfo.Tables[4].Rows[i]["landlordId"].ToString() != "")
                        LandlordInformationmodel.LandlordId = Convert.ToInt32(merchantsinfo.Tables[4].Rows[i]["landlordId"]);
                    LandlordInformationmodel.FirstName = Convert.ToString(merchantsinfo.Tables[4].Rows[i]["firstname"]);
                    LandlordInformationmodel.LastName = Convert.ToString(merchantsinfo.Tables[4].Rows[i]["lastname"]);
                    LandlordInformationmodel.PhoneNumber = Convert.ToString(merchantsinfo.Tables[4].Rows[i]["telephone"]);

                    merchantsdemodel.LandlordInformation = LandlordInformationmodel;
                }

                if (merchantsinfo.Tables[5].Rows.Count > 0)
                {
                    List<MerchantTradeReference> traderefs = new List<MerchantTradeReference>();
                    for (int i = 0; i < merchantsinfo.Tables[5].Rows.Count; i++)
                    {
                        MerchantTradeReference traderef = new MerchantTradeReference();
                        if (merchantsinfo.Tables[5].Rows[i]["TradeRefId"].ToString() != null && merchantsinfo.Tables[5].Rows[i]["TradeRefId"].ToString() != "")
                            traderef.ReferenceId = Convert.ToInt32(merchantsinfo.Tables[5].Rows[i]["TradeRefId"]);
                        traderef.ReferenceName = Convert.ToString(merchantsinfo.Tables[5].Rows[i]["Name"]);
                        traderef.PhoneNumber = Convert.ToString(merchantsinfo.Tables[5].Rows[i]["PhoneNumber"]);

                        traderefs.Add(traderef);

                    }
                    merchantsdemodel.TradeReference = traderefs;
                }

            }
            return merchantsdemodel;

        }


        public MerchantsModel RetrieveMerchantInfo(Int64 merchantId, Int64 tasktypeId, Int64 contractId)
        {
            MerchantsModel merchantsmodel = null;
            DataSet merchantsinfo = (new DataAccess.DataAccess()).ExecuteDataSet("avz_mrc_spRetrievemerchantInfo",
                               new
                               {
                                   merchantId = merchantId,
                                   tasktypeId = tasktypeId,
                                   contractId = contractId
                               });
            if (merchantsinfo.Tables[0].Rows.Count > 0)
            {
                merchantsmodel = new MerchantsModel();
                merchantsmodel.merchantName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["legalName"]);
                merchantsmodel.legalName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["legalName"]);
                merchantsmodel.businessName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["businessName"]);
                merchantsmodel.businessTypeId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["BusinessTypeId"]);
                merchantsmodel.businessWebSite = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["businessWebSite"]);
                merchantsmodel.rnc = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["rnc"]);
                merchantsmodel.industryTypeId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["industryTypeId"]);
                merchantsmodel.assignedSalesRep = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["assignedSalesRep"]);
                merchantsmodel.salesRepId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["salesRepId"]);
                merchantsmodel.ownerName = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["ownerName"]);
                merchantsmodel.businessStartDate = Convert.ToDateTime(merchantsinfo.Tables[0].Rows[0]["businessStartDate"]);
                merchantsmodel.SSN = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["SSN"]);
                merchantsmodel.phoneNumber = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["phoneNumber"]);
                merchantsmodel.TypeofBusinessentity = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["TypeofBusinessentity"]);
                merchantsmodel.OwnerAddress = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["address"]);
                merchantsmodel.merchantId = Convert.ToInt32(merchantsinfo.Tables[0].Rows[0]["merchantId"]);
                merchantsmodel.ContractId = Convert.ToInt64(string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["contractid"].ToString()) == false ? merchantsinfo.Tables[0].Rows[0]["contractid"].ToString() : "0");
                merchantsmodel.taskTypeId = Convert.ToInt64(string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["taskTypeId"].ToString()) == false ? merchantsinfo.Tables[0].Rows[0]["taskTypeId"].ToString() : "0");
                merchantsmodel.taskStatusId = Convert.ToInt64(string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["taskStatusId"].ToString()) == false ? merchantsinfo.Tables[0].Rows[0]["taskStatusId"].ToString() : "0");
                merchantsmodel.merchantStatusId = Convert.ToInt64(string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["merchantStatusId"].ToString()) == false ? merchantsinfo.Tables[0].Rows[0]["merchantStatusId"].ToString() : "0");
                merchantsmodel.contractStatusId = Convert.ToInt64(string.IsNullOrEmpty(merchantsinfo.Tables[0].Rows[0]["contractStatusId"].ToString()) == false ? merchantsinfo.Tables[0].Rows[0]["contractStatusId"].ToString() : "0");
                merchantsmodel.merchantStatus = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["merchantStatus"]);
                merchantsmodel.contractStatus = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["contractStatus"]);
                merchantsmodel.ownedamount = Convert.ToDouble(merchantsinfo.Tables[0].Rows[0]["owedAmount"]);
                merchantsmodel.paidamount = Convert.ToDouble(merchantsinfo.Tables[0].Rows[0]["paidAmount"]);
                merchantsmodel.balanceamount = Convert.ToDouble(merchantsinfo.Tables[0].Rows[0]["balanceAmount"]);
                merchantsmodel.assignedUser = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["UserName"]);
                merchantsmodel.industrytype = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["IndustryType"]);
                merchantsmodel.merchantpropertytype = Convert.ToString(merchantsinfo.Tables[0].Rows[0]["propertytype"]);
            }
            return merchantsmodel;
        }

        #endregion

        #region Credit Card
        public int ManageCreditCardProfile(long merchantId, IList<MerchantProfile> merchantProfile, int iscompleted, Int64 contractId)
        {
            string profileXml;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;
            StringBuilder sbXml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sbXml, settings);

            writer.WriteStartElement("profiles");

            foreach (var item in merchantProfile)
            {
                DateTime strDate = item.startDate;
                string strStartDate = strDate.ToString("yyyy-MM-dd");
                DateTime strDate1 = item.endDate;
                string strEndDate = strDate1.ToString("yyyy-MM-dd");

                writer.WriteStartElement("monthlyprofile");
                writer.WriteAttributeString("merchantId", Convert.ToString(merchantId));
                writer.WriteAttributeString("processorId", Convert.ToString(item.processorId));
                writer.WriteAttributeString("retrievalSource", Convert.ToString(""));
                writer.WriteAttributeString("totalAmount", Convert.ToString(item.totalAmount));
                writer.WriteAttributeString("totalTickets", Convert.ToString(item.totalTickets));
                writer.WriteAttributeString("startDate", Convert.ToString(strStartDate));
                writer.WriteAttributeString("endDate", Convert.ToString(strEndDate));
                writer.WriteAttributeString("year", Convert.ToString(item.year));
                writer.WriteAttributeString("month", Convert.ToString(item.month));
                writer.WriteAttributeString("creditcardActivityId", Convert.ToString(item.creditcardActivityId));
                writer.WriteAttributeString("isactive", Convert.ToString(item.isActive));
                writer.WriteEndElement();

            }

            writer.WriteEndElement();
            writer.Flush();

            #region Creating XML of merchantsinfomodel
            /* var xml = new XElement("merchantsinformation", from info in merchantProfile
                                                           select new XElement("monthlyprofile",
                                                           new XElement("totalTickets", info.totalTickets),
                                                           new XElement("retrievalSource", info.retrievalSource),
                                                           new XElement("totalAmount", info.totalAmount),
                                                           new XElement("startDate", info.startDate),
                                                           new XElement("endDate", info.endDate)));*/

            #endregion
            profileXml = sbXml.ToString();

            new DataAccess.DataAccess().ExecuteNonQuery("avz_mrc_spInsertCreditProfiles", new { merchantId = merchantId, profileXml = profileXml, iscompleted = iscompleted, contractId = contractId });
            return 1;
        }
        public bool SendRequesttoProcessor(Int64 merchantId, Int64 processorId)
        {

            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spInsertProcessorRequest", new { merchantId = merchantId, processorId = processorId });

        }
        public IList<ProcessorModel> RetrieveProcessorbyMerchant(Int64 merchantId)
        {

            return new DataAccess.DataAccess().ExecuteReader<ProcessorModel>("avz_cc_spRetrieveProcessorbyMerchant", new { merchantId = merchantId });

        }
        public bool InsertMerchantCreditCardVolumes(ProcessorModel model, Int64 merchantId)
        {

            return new DataAccess.DataAccess().ExecuteNonQuery("avz_cc_spInsertMerchantCreditCardVolumes", new { merchantId = merchantId });

        }

        #endregion

        #region Renewals
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet RetrieveRenewalsList()
        {
            DataSet renewalsList = null;
            renewalsList = new DataAccess.DataAccess().ExecuteDataSet("avz_ren_spRetrievRenewalsList", null);
            return renewalsList;



        }
        public bool CompleteRenewalsTask(Int64 contractId)
        {

            return new DataAccess.DataAccess().ExecuteNonQuery("avz_ren_spCompleteRenewalsTask", new { contractId = contractId });




        }
        #endregion


        public int ManageCreditCardProfile(long merchantId, IList<MerchantProfile> merchantProfile)
        {
            throw new NotImplementedException();
        }

        public MerchantOffer RetrieveMerchantOffer(Int64 merchantId, Int64 contractId)
        {
            DataSet dsMoffer = new DataAccess.DataAccess().ExecuteDataSet("avz_mcr_merchantOffer", new { merchantId = merchantId, contractId = contractId });

            var merchantOffer = new MerchantOffer();

            if (dsMoffer.Tables[0].Rows.Count > 0)
                DataHelper.FillObjectFromDataRow(merchantOffer.Merchant, dsMoffer.Tables[0].Rows[0]);

            if (dsMoffer.Tables[1].Rows.Count > 0)
                DataHelper.FillObjectFromDataRow(merchantOffer.Merchant.Address, dsMoffer.Tables[1].Rows[0]);

            if (dsMoffer.Tables[2].Rows.Count > 0)
                DataHelper.FillObjectFromDataRow(merchantOffer.Owner, dsMoffer.Tables[2].Rows[0]);

            if (dsMoffer.Tables[3].Rows.Count > 0)
                DataHelper.FillObjectFromDataRow(merchantOffer.Owner.Address, dsMoffer.Tables[3].Rows[0]);

            if (dsMoffer.Tables[4].Rows.Count > 0)
                DataHelper.FillObjectFromDataRow(merchantOffer.Contract, dsMoffer.Tables[4].Rows[0]);

            if (dsMoffer.Tables[5].Rows.Count > 0)
                foreach (DataRow r in dsMoffer.Tables[5].Rows)
                {
                    var offer = new OfferModel();
                    DataHelper.FillObjectFromDataRow(offer, r);
                    merchantOffer.Offers.Add(offer);
                }
            return merchantOffer;
        }
    }
}