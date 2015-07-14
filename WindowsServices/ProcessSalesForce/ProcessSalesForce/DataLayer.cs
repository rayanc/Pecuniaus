using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Bridge.DataAccess;
using sf = ProcessSalesForce.SFEnterpriseSoapService;
namespace ProcessSalesForce
{
    public class DataLayer
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Retrieve the sales force account and then insert in the system
        /// </summary>
        /// <param name="results"></param>
        public void CreatSalesForceMerchant(ProcessSalesForce.SFEnterpriseSoapService.QueryResult results)
        {
          
            for (int i = 0; i < results.size; i++)
            {
             
                string email = string.Empty;
                string jobtitle = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;
                string homePhone = string.Empty;
                string cellPhone = string.Empty;
                string merchantstatus = string.Empty;
                string merchanttype = string.Empty;
                DateTime dob = DateTime.MinValue;
                string salesRepId = string.Empty;
                string entitytype = string.Empty;
                string owneremail = string.Empty;
                string businessType =string.Empty;
                string accountId = ((sf.Account)(results.records[i])).Id;
                string legalName = ((sf.Account)(results.records[i])).Razon_Social__c == null ? "" : ((sf.Account)(results.records[i])).Razon_Social__c;
                string businessName = ((sf.Account)(results.records[i])).Name == null ? "" : ((sf.Account)(results.records[i])).Name;
                string rnc = ((sf.Account)(results.records[i])).RNC__c == null ? "" : ((sf.Account)(results.records[i])).RNC__c;
                string cardNetNbr = ((sf.Account)(results.records[i])).CardNet__c == null ? "" : ((sf.Account)(results.records[i])).CardNet__c;
                string vNetnbr = ((sf.Account)(results.records[i])).VisaNet__c == null ? "" : ((sf.Account)(results.records[i])).VisaNet__c;
                string telephone1 = ((sf.Account)(results.records[i])).Phone == null ? "" : ((sf.Account)(results.records[i])).Phone;
                string telephone2 = ((sf.Account)(results.records[i])).Telefono_2__c == null ? "" : ((sf.Account)(results.records[i])).Telefono_2__c;
                string city = ((sf.Account)(results.records[i])).Ciudad__c == null ? "" : ((sf.Account)(results.records[i])).Ciudad__c;
                string province = ((sf.Account)(results.records[i])).Provincia__c == null ? "" : ((sf.Account)(results.records[i])).Provincia__c;
                string country = ((sf.Account)(results.records[i])).Pais__c == null ? "" : ((sf.Account)(results.records[i])).Pais__c;
                //string businessType = ((sf.Account)(results.records[i])).Tipo_Entidad__c == null ? "" : ((sf.Account)(results.records[i])).Tipo_Entidad__c;
                string industryType = ((sf.Account)(results.records[i])).Industria__c == null ? "" : ((sf.Account)(results.records[i])).Industria__c;
                string salesRepName = ((sf.Account)(results.records[i])).Owner.Name == null ? "" : ((sf.Account)(results.records[i])).Owner.Name;
                salesRepId = ((sf.Account)(results.records[i])).Owner.Id == null ? "" : ((sf.Account)(results.records[i])).Owner.Id;
                merchantstatus = ((sf.Account)(results.records[i])).Merchant_Status__c == null ? "" : ((sf.Account)(results.records[i])).Merchant_Status__c;
               // entitytype = ((sf.Account)(results.records[i])).Tipo_Entidad__c == null ? "" : ((sf.Account)(results.records[i])).Tipo_Entidad__c;
                owneremail = ((sf.Account)(results.records[i])).Owner.Email == null ? "" : ((sf.Account)(results.records[i])).Owner.Email;


                DateTime businessStartDate = DateTime.MinValue;
                bool? isowner = false;
                string ssn = String.Empty;
                string contactId = string.Empty;
                int industryTypeId = 1, businessTypeId = 1, insertUserId = 10001;

                DateTime insertDate = DateTime.UtcNow;
                try
                {
                    if (((ProcessSalesForce.SFEnterpriseSoapService.Account)(results.records[i])).Contacts.size > 0)
                    {
                        email = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Email == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Email;
                        jobtitle = "Sales Rep";
                        firstname = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).FirstName == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).FirstName;
                        lastname = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).LastName == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).LastName;
                        ssn = "";
                     // isowner = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Owner__c == null ? false : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Owner__c;
                        contactId = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Id == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Id;
                        homePhone = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Tel_Residencial_Propietario__c == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Tel_Residencial_Propietario__c;
                        cellPhone = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Celular_Propietario__c == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Celular_Propietario__c;


                    }
                }
                catch (Exception ex)
                {

                    email = String.Empty;
                    jobtitle = String.Empty;
                    firstname = String.Empty;
                    lastname = String.Empty;
                    ssn = String.Empty;
                  
                }
                try
                {
                    businessStartDate = (DateTime)((sf.Account)(results.records[i])).Fecha_Inicio_Act__c;
                }
                catch (Exception)
                {
                    businessStartDate = DateTime.MinValue;
                }



                new DataAccess().ExecuteNonQuery("avz_sf_spInsertSalesForce",
                     new
                     {
                         legalName = legalName,
                         businessName = businessName,
                         salesRepId = salesRepId,
                         businessStartDate = businessStartDate,
                         industryTypeId = industryTypeId,
                         businessTypeId = businessTypeId,
                         insertUserId = insertUserId,
                         insertDate = insertDate,
                         rnc = rnc,
                         cardNetNbr = cardNetNbr,
                         vNetnbr = vNetnbr,
                         telephone1 = telephone1,
                         telephone2 = telephone2,
                         city = city,
                         province = province,
                         country = country,
                         businessType = businessType,
                         industryType = industryType,
                         email = email,
                         jobtitle = "",
                         firstname = firstname,
                         lastname = lastname,
                         ssn = ssn,
                         accountId = accountId,
                         homePhone = homePhone,
                         cellPhone = cellPhone,
                         isowner = isowner,
                         sfid = contactId,
                         salesRepName = salesRepName
                     }
                 );
            }
        }

        /// <summary>
        /// Update the records in the system to the sales force account
        /// </summary>
        public IList<Merchants> RetrieveRecordstoSalesForce()
        {
            IList<Merchants> results;
            results = new DataAccess().ExecuteReader<Merchants>("avz_sf_spRetrieveAccountstoUpdateSalesForce", null);
            return results;

        }
        /// <summary>
        /// Update the records in the system to the sales force account
        /// </summary>
        public DataSet RetrieveDataSetSalesForce()
        {

            return new DataAccess().ExecuteDataSet("avz_sf_spRetrieveAccountstoUpdateSalesForce", null);
            

        }
        /// <summary>
        /// Assigns the newly created account's id to the temprorary merchants table
        /// </summary>
        public void AssignSalesForceAccountId(string acountId, Int64 merchantIdTemp)
        {
            new DataAccess().ExecuteNonQuery("avz_mrc_spInsertSalesForcetoMerchant", new { accountId = acountId, merchantId_tmp = merchantIdTemp });

        }
        public void MapOfferSalesForceAccountId(string acountId, Int64 offerId)
        {
            new DataAccess().ExecuteNonQuery("avz_mrc_spUpdateAccountIdSFtoMerchant", new { accountId = acountId, offerId = offerId });

        }
        public void UpdateSyncStatus(string acountId, Int64 merchantId)
        {
            new DataAccess().ExecuteNonQuery("avz_mrc_spUpdateSyncStatusMerchant", new { accountId = acountId, merchantId = merchantId });

        }
        public void UpdateSyncStatusContact(string acountId, Int64 contactId)
        {
            new DataAccess().ExecuteNonQuery("avz_mrc_spUpdateStatusContact", new { accountId = acountId, contactId = contactId });

        }
        
        public void CreateMerchantfromSalesForce(ProcessSalesForce.SFEnterpriseSoapService.QueryResult results)
        {
            logger.Log(NLog.LogLevel.Info, "<br/><font color=orange>Reading the leads from the sales force starts........." + DateTime.Now.ToString());
            logger.Log(NLog.LogLevel.Info, "<br/><font color=orange>Total counts to be read " + results.size);
            string profileXml;
            for (int i = 0; i < results.size; i++)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = false;
                StringBuilder sbXml = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sbXml, settings);
                #region Variables Decalaration
                string merchantstatus = string.Empty;
                string email = String.Empty;
                string jobtitle = String.Empty;
                string firstname = String.Empty;
                string lastname = String.Empty;
                string homePhone = string.Empty;
                string cellPhone = string.Empty;
                DateTime dob = DateTime.MinValue;
                string salesRepId = string.Empty;
                string accountId = string.Empty;
                string legalName = string.Empty;
                string businessName = string.Empty;
                string rnc = string.Empty;
                string cardNetNbr = string.Empty;
                string vNetnbr = string.Empty;
                string telephone1 = string.Empty;
                string telephone2 = string.Empty;
                string city = string.Empty;
                string province = string.Empty;
                string country = string.Empty;
                string businessType = string.Empty;
                string industryType = string.Empty;
                string salesRepName = string.Empty;
                string merchantaddress = string.Empty;
                string merchantcity = string.Empty;
                string merchantcountry = string.Empty;
                string merchantstate = string.Empty;
                string owneremail = string.Empty;
                string merchantzipcode = string.Empty;
                string ComercialReference1 = string.Empty;
                string ComercialReference2 = string.Empty;
                string ComercialReferenceTele1 = string.Empty;
                string ComercialReferenceTele2 = string.Empty;
                DateTime creditDate = DateTime.MinValue;
                string OtherProcessor = string.Empty;
                #endregion
                 accountId = ((sf.Account)(results.records[i])).Id;
                 legalName = ((sf.Account)(results.records[i])).Razon_Social__c == null ? "" : ((sf.Account)(results.records[i])).Razon_Social__c;
                businessName = ((sf.Account)(results.records[i])).Name == null ? "" : ((sf.Account)(results.records[i])).Name;
                 rnc = ((sf.Account)(results.records[i])).RNC__c == null ? "" : ((sf.Account)(results.records[i])).RNC__c;
                 cardNetNbr = ((sf.Account)(results.records[i])).CardNet__c == null ? "" : ((sf.Account)(results.records[i])).CardNet__c;
                 vNetnbr = ((sf.Account)(results.records[i])).VisaNet__c == null ? "" : ((sf.Account)(results.records[i])).VisaNet__c;
                 telephone1 = ((sf.Account)(results.records[i])).Phone == null ? "" : ((sf.Account)(results.records[i])).Phone;
                 telephone2 = ((sf.Account)(results.records[i])).Telefono_2__c == null ? "" : ((sf.Account)(results.records[i])).Telefono_2__c;
                 city = ((sf.Account)(results.records[i])).Ciudad__c == null ? "" : ((sf.Account)(results.records[i])).Ciudad__c;
                 province = ((sf.Account)(results.records[i])).Provincia__c == null ? "" : ((sf.Account)(results.records[i])).Provincia__c;
                 country = ((sf.Account)(results.records[i])).Pais__c == null ? "" : ((sf.Account)(results.records[i])).Pais__c;
                 //businessType = ((sf.Account)(results.records[i])).Tipo_Entidad__c == null ? "" : ((sf.Account)(results.records[i])).Tipo_Entidad__c;
                 industryType = ((sf.Account)(results.records[i])).Industria__c == null ? "" : ((sf.Account)(results.records[i])).Industria__c;
                 salesRepName = ((sf.Account)(results.records[i])).Owner.Name == null ? "" : ((sf.Account)(results.records[i])).Owner.Name;
                salesRepId = ((sf.Account)(results.records[i])).Owner.Id == null ? "" : ((sf.Account)(results.records[i])).Owner.Id;
                 owneremail = ((sf.Account)(results.records[i])).Owner.Email == null ? "" : ((sf.Account)(results.records[i])).Owner.Email;
                merchantstatus = ((sf.Account)(results.records[i])).Merchant_Status__c == null ? "" : ((sf.Account)(results.records[i])).Merchant_Status__c;
                ComercialReference1 = ((sf.Account)(results.records[i])).Referencia_Comercial_1__c == null ? "" : ((sf.Account)(results.records[i])).Referencia_Comercial_1__c;
                ComercialReference2 = ((sf.Account)(results.records[i])).Referencia_Comercial_2__c == null ? "" : ((sf.Account)(results.records[i])).Referencia_Comercial_2__c;
                ComercialReferenceTele1 = ((sf.Account)(results.records[i])).Telefono_Referencia_1__c == null ? "" : ((sf.Account)(results.records[i])).Telefono_Referencia_1__c;
                ComercialReferenceTele2 = ((sf.Account)(results.records[i])).Telefono_Referencia_2__c == null ? "" : ((sf.Account)(results.records[i])).Telefono_Referencia_2__c;
                OtherProcessor = ((sf.Account)(results.records[i])).Otro_Procesador__c == null ? "" : ((sf.Account)(results.records[i])).Otro_Procesador__c;
                //try
                //{
                //    creditDate = (DateTime)((sf.Account)(results.records[i])).Fecha_Procesamiento_TC__c;
                //}
                //catch (Exception)
                //{
                    creditDate = DateTime.MinValue;
                //}
               
                //if (((sf.Account)(results.records[i])).BillingAddress != null)
                //{
                //    merchantaddress = ((sf.Account)(results.records[i])).BillingAddress.street == null ? "" : ((sf.Account)(results.records[i])).BillingAddress.street;
                //    merchantcity = ((sf.Account)(results.records[i])).BillingAddress.city == null ? "" : ((sf.Account)(results.records[i])).BillingAddress.city;
                //    merchantcountry = ((sf.Account)(results.records[i])).BillingAddress.country == null ? "" : ((sf.Account)(results.records[i])).BillingAddress.country;
                //    merchantstate = ((sf.Account)(results.records[i])).BillingAddress.state == null ? "" : ((sf.Account)(results.records[i])).BillingAddress.state;
                //    merchantzipcode = ((sf.Account)(results.records[i])).BillingAddress.postalCode == null ? "" : ((sf.Account)(results.records[i])).BillingAddress.postalCode;

                //}
                DateTime businessStartDate = DateTime.MinValue;
                bool? isowner = false;
                string ssn = String.Empty;
                string contactId = string.Empty;
                int insertUserId = 11000;
                logger.Log(NLog.LogLevel.Info, "<b> legal Name" + legalName + "</b><br/><br/>");
                DateTime insertDate = DateTime.UtcNow;
                writer.WriteStartElement("merchantinfo");
                writer.WriteStartElement("merchantbasicinfo");
                writer.WriteAttributeString("accountId", accountId);
                writer.WriteAttributeString("legalName", legalName);
                writer.WriteAttributeString("businessName", businessName);
                writer.WriteAttributeString("rnc", rnc);
                writer.WriteAttributeString("cardNetNbr", cardNetNbr);
                writer.WriteAttributeString("vNetnbr", vNetnbr);
                writer.WriteAttributeString("telephone1", telephone1);
                writer.WriteAttributeString("telephone2", telephone2);
                writer.WriteAttributeString("city", city);
                writer.WriteAttributeString("province", province);
                writer.WriteAttributeString("country", country);
                writer.WriteAttributeString("businessType", businessType);
                writer.WriteAttributeString("industryType", industryType);
                writer.WriteAttributeString("salesRepName", salesRepName);
                writer.WriteAttributeString("salesRepId", salesRepId);
                writer.WriteAttributeString("insertUserId", Convert.ToString(insertUserId));
                writer.WriteAttributeString("owneremail", owneremail);
                writer.WriteAttributeString("merchantstatus", merchantstatus);
                writer.WriteAttributeString("merchantaddress", merchantaddress);
                writer.WriteAttributeString("merchantcity", merchantcity);
                writer.WriteAttributeString("merchantcountry", merchantcountry);
                writer.WriteAttributeString("merchantstate", merchantstate);
                writer.WriteAttributeString("mComercialReference1", ComercialReference1);
                writer.WriteAttributeString("mComercialReference2", ComercialReference2);
                writer.WriteAttributeString("mComercialReferenceTele1", ComercialReferenceTele1);
                writer.WriteAttributeString("mComercialReferenceTele2", ComercialReferenceTele2);
                writer.WriteAttributeString("otherProcessor", OtherProcessor);
                writer.WriteAttributeString("creditDate", creditDate.ToString("yyyy-MM-dd"));


                //
                try
                {
                    businessStartDate = (DateTime)((sf.Account)(results.records[i])).Fecha_Inicio_Act__c;
                }
                catch (Exception)
                {
                    businessStartDate = DateTime.MinValue;
                }
                writer.WriteAttributeString("businessStartDate", businessStartDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                try
                {
                    writer.WriteStartElement("contacts");
                    if (((ProcessSalesForce.SFEnterpriseSoapService.Account)(results.records[i])).Contacts.size > 0)
                    {
                        logger.Log(NLog.LogLevel.Info,"<br/>Reading contacts" );
                        for (int count = 0; count < ((ProcessSalesForce.SFEnterpriseSoapService.Account)(results.records[i])).Contacts.size; count++)
                        {


                            email = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).Email == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Email;
                            jobtitle =string.Empty;
                            firstname = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).FirstName == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).FirstName;
                            lastname = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).LastName == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).LastName;
                            ssn = "";
                           // isowner = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).Owner__c == null ? false : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Owner__c;
                            contactId = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).Id == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Id;
                            homePhone = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).Tel_Residencial_Propietario__c == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Tel_Residencial_Propietario__c;
                            cellPhone = ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).Celular_Propietario__c == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).Celular_Propietario__c;

                            writer.WriteStartElement("contact");
                            writer.WriteAttributeString("email", email);
                            writer.WriteAttributeString("jobtitle", jobtitle);
                            writer.WriteAttributeString("firstname", firstname);
                            writer.WriteAttributeString("lastname", lastname);
                            writer.WriteAttributeString("ssn", ssn);

                            if (isowner == true)
                            {
                                writer.WriteAttributeString("isowner", "1");
                            }
                            else
                            {
                                writer.WriteAttributeString("isowner", "0");
                            }

                            writer.WriteAttributeString("contactId", contactId);
                            writer.WriteAttributeString("homePhone", homePhone);
                            writer.WriteAttributeString("cellPhone", cellPhone);
                            //if (((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).MailingAddress!=null)
                            //{


                            //    writer.WriteAttributeString("contactaddress", ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).MailingAddress.street == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).MailingAddress.street);
                            //    writer.WriteAttributeString("contactcity", ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).MailingAddress.city == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).MailingAddress.city);
                            //    writer.WriteAttributeString("contactcountry", ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).MailingAddress.country == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).MailingAddress.country);
                            //    writer.WriteAttributeString("contactstate", ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).MailingAddress.state == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).MailingAddress.state);
                            //    writer.WriteAttributeString("contactzipcode", ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[count])).MailingAddress.postalCode == null ? "" : ((sf.Contact)(((sf.Account)(results.records[i])).Contacts.records[0])).MailingAddress.postalCode);
                            //}
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
                catch (Exception ex)
                {

                    email = String.Empty;
                    jobtitle = String.Empty;
                    firstname = String.Empty;
                    lastname = String.Empty;
                    ssn = String.Empty;
                    writer.WriteEndElement();
                    logger.Log(NLog.LogLevel.Error, ex.Message);
                }

                writer.WriteEndElement();
                writer.Flush();
                profileXml = sbXml.ToString();
                logger.Log(NLog.LogLevel.Info, profileXml);
                try
                {
                    new DataAccess().ExecuteNonQuery("avz_sf_spCreateMerchantfromSalesForce",
                 new
                 {
                     dataxml = profileXml
                 });
                    //Update issync back to the Sales force
                    new SalesForce().UpdateSalesForce(accountId);
                }
                catch (Exception ex)
                {
                    logger.Log(NLog.LogLevel.Error, ex.Message);
                }
            }
        }

        public DataSet UpdateMerhantsOffertoSalesForce()
        {
            return new DataAccess().ExecuteDataSet("avz_mrc_spRetrieveOffersInformation", null);
        }


    }
}
