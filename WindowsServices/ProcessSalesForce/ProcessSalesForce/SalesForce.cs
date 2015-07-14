using ProcessSalesForce.SFEnterpriseSoapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using sf = ProcessSalesForce.SFEnterpriseSoapService;
using System.Data;

namespace ProcessSalesForce
{
    public class SalesForce
    {

        #region Variables
        private string UserName { get { return System.Configuration.ConfigurationManager.AppSettings["SFUserName"]; } }
        private string Password { get { return System.Configuration.ConfigurationManager.AppSettings["SFPassword"]; } }
        private string SecurityToken { get { return System.Configuration.ConfigurationManager.AppSettings["SFToken"]; } }
        static SforceService binding;
        private sf.SforceService SFService = new sf.SforceService();
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        #endregion
        #region Constructors
        public SalesForce()
        {
        }
        #endregion
        #region Methods
        public bool Authenticate()
        {
            binding = new SforceService();
            LoginResult LResult = new LoginResult();
            try
            {
                LResult = binding.login(UserName, Password + SecurityToken);
            }
            catch (SoapException ex)
            {
                logger.Log(NLog.LogLevel.Error, ex.Message);
                return false;
            }
            if (LResult.passwordExpired)
            {
                return false;
            }

            //Change the binding to the new endpoint
            binding.Url = LResult.serverUrl;

            //Create a new session header object and set the session id to that returned by the login
            binding.SessionHeaderValue = new SessionHeader();
            binding.SessionHeaderValue.sessionId = LResult.sessionId;
            GetUserInfoResult userInfo = LResult.userInfo;



            return true;
        }

        public SaveResult[] CreateObjects(string ObjectName)
        {
            return binding.create(new sObject[1] { new Account() { Name = ObjectName } });
        }

        public SaveResult[] UpdateObjects(string ObjectID, string ObjectName)
        {
            return binding.update(new sObject[1] { new Account() { Name = ObjectName, Id = ObjectID } });
        }


        /// <summary>
        /// Retrieve accounts from sales force and insert into the system
        /// </summary>
        public void RetrieveSalesForceAccount()
        {

            if (!Authenticate())
                return;

            string soql =
                @"SELECT owner.id, owner.name,owner.email, id, Fecha_Inicio_Act__c, Name, Razon_Social__c, RNC__c, Industria__c, Direccion_Fisica__c, Ciudad__c, Provincia__c, Pais__c, Phone, Telefono_2__c, VisaNet__c, Merchantid__c,
                CardNet__c,Merchant_Status__c,Referencia_Comercial_1__c,Referencia_Comercial_2__c,Telefono_Referencia_1__c,Telefono_Referencia_2__c,(SELECT id, FirstName, LastName, Email, Celular_Propietario__c, Fecha_Nacimiento__c,
                Tel_Residencial_Propietario__c, Posicion__c FROM Contacts) from account  where Sync_Status__c='No'";
            //new DataLayer().CreatSalesForceMerchant(binding.query(soql));
            new DataLayer().CreateMerchantfromSalesForce(binding.query(soql));
        }
        /// <summary>
        /// Update the records from the system to Sales Force 
        /// </summary>
        public void UpdateSalesForceAccount()
        {

            // IList<Merchants> results = new DataLayer().RetrieveRecordstoSalesForce();
            DataSet results = new DataLayer().RetrieveDataSetSalesForce();
            if (!Authenticate())
            {
                return;
            }
            if (results.Tables[0].Rows.Count > 0)
            {

                for (int count = 0; count <= results.Tables[0].Rows.Count - 1; count++)
                {
                    if (string.IsNullOrEmpty(results.Tables[0].Rows[count]["accountId"].ToString()))
                    {
                        SaveResult[] result = binding.create(new sObject[1] { new Account() 
                    { 
                        Phone = results.Tables[0].Rows[count]["telePhone1"].ToString(),
                        Name=results.Tables[0].Rows[count]["businessName"].ToString(),
                        Telefono_2__c=results.Tables[0].Rows[count]["telePhone2"].ToString(),
                        
                        RNC__c=results.Tables[0].Rows[count]["rncNumber"].ToString(),
                        Razon_Social__c=results.Tables[0].Rows[count]["businessName"].ToString(),
                        Fecha_Inicio_Act__c=Convert.ToDateTime(results.Tables[0].Rows[count]["businessStartDate"].ToString()),
                        CardNet__c=results.Tables[0].Rows[count]["CNetProcessorId"].ToString(),
                        VisaNet__c=results.Tables[0].Rows[count]["VNetProcessoIdd"].ToString(),
                       // Website=results.Tables[0].Rows[count]["businessWebSite"].ToString(),
                       // Tipo_Entidad__c=results.Tables[0].Rows[count]["businessType"].ToString(),
                        Ciudad__c=results.Tables[0].Rows[count]["city"].ToString(),
                        Provincia__c=results.Tables[0].Rows[count]["state"].ToString(),
                        Pais__c=results.Tables[0].Rows[count]["country"].ToString(),
                        Merchant_Status__c=results.Tables[0].Rows[count]["merchantStatus"].ToString(),
                        Merchantid__c=results.Tables[0].Rows[count]["merchantId"].ToString(),
                       // Merchant_ID__cSpecified=true,
                       //    BillingCity= results.Tables[0].Rows[count]["city"].ToString(),
                       //BillingCountry=results.Tables[0].Rows[count]["country"].ToString(),
                       //BillingStreet=string.Concat(results.Tables[0].Rows[count]["address1"].ToString(),"", results.Tables[0].Rows[count]["address2"].ToString()),
                       //BillingState=results.Tables[0].Rows[count]["accountId"].ToString()
                    
                    } });
                        new DataLayer().UpdateSyncStatus(result[0].id, Convert.ToInt64(results.Tables[0].Rows[count]["merchantId"].ToString()));

                    }
                    else
                    {
                        binding.update(new sObject[1] { new Account(){ 
                      Phone = results.Tables[0].Rows[count]["telePhone1"].ToString(),
                      Name=results.Tables[0].Rows[count]["businessName"].ToString(),
                      Telefono_2__c=results.Tables[0].Rows[count]["telePhone2"].ToString(),
                      RNC__c=results.Tables[0].Rows[count]["rncNumber"].ToString(),
                      Razon_Social__c=results.Tables[0].Rows[count]["businessName"].ToString(),
                      Fecha_Inicio_Act__c=Convert.ToDateTime(results.Tables[0].Rows[count]["businessStartDate"].ToString()),
                      CardNet__c=results.Tables[0].Rows[count]["CNetProcessorId"].ToString(),
                      VisaNet__c=results.Tables[0].Rows[count]["VNetProcessoIdd"].ToString(),
                     // Website=results.Tables[0].Rows[count]["businessWebSite"].ToString(),
                     // Tipo_Entidad__c=results.Tables[0].Rows[count]["businessType"].ToString(),
                      Ciudad__c=results.Tables[0].Rows[count]["city"].ToString(),
                      Provincia__c=results.Tables[0].Rows[count]["state"].ToString(),
                      Pais__c=results.Tables[0].Rows[count]["country"].ToString(),
                      Merchant_Status__c=results.Tables[0].Rows[count]["merchantStatus"].ToString(),
                      Id=results.Tables[0].Rows[count]["accountId"].ToString(),
                       Merchantid__c=results.Tables[0].Rows[count]["merchantId"].ToString(),
                     //  Merchant_ID__cSpecified=true,
                       //BillingCity= results.Tables[0].Rows[count]["city"].ToString(),
                       //BillingCountry=results.Tables[0].Rows[count]["country"].ToString(),
                       //BillingStreet=string.Concat(results.Tables[0].Rows[count]["address1"].ToString(),"", results.Tables[0].Rows[count]["address2"].ToString()),
                       //BillingState=results.Tables[0].Rows[count]["accountId"].ToString()
                        
                        } });
                        new DataLayer().UpdateSyncStatus(results.Tables[0].Rows[count]["accountId"].ToString(), Convert.ToInt64(results.Tables[0].Rows[count]["merchantId"].ToString()));
                    }
                }

            }
            if (results.Tables[1].Rows.Count > 0)
            {
                for (int count = 0; count <= results.Tables[1].Rows.Count - 1; count++)
                {

                    if (string.IsNullOrEmpty(results.Tables[1].Rows[count]["accountId"].ToString()))
                    {
                        SaveResult[] result = binding.create(new sObject[1] { new Contact() 
                      {
                            FirstName =results.Tables[1].Rows[count]["firstname"].ToString(),
                            LastName = results.Tables[1].Rows[count]["lastname"].ToString(),
                            Birthdate =Convert.ToDateTime(results.Tables[1].Rows[count]["dateofBirth"].ToString()),
                            BirthdateSpecified=true,
                            Tel_Residencial_Propietario__c = results.Tables[1].Rows[count]["homephone"].ToString(),
                            Celular_Propietario__c = results.Tables[1].Rows[count]["cellphone"].ToString(),
                            //MailingCity=results.Tables[1].Rows[count]["city"].ToString(),
                            //MailingCountry=results.Tables[1].Rows[count]["country"].ToString(),
                            //MailingState=results.Tables[1].Rows[count]["state"].ToString(),
                            //MailingStreet=string.Concat(results.Tables[1].Rows[count]["address1"].ToString(),results.Tables[1].Rows[count]["address2"].ToString()),
                             AccountId=results.Tables[1].Rows[count]["maccountId"].ToString()
                      }
                            });
                        new DataLayer().UpdateSyncStatusContact(result[0].id, Convert.ToInt64(results.Tables[1].Rows[count]["contactId"].ToString()));
                    }
                    else
                    {
                        binding.update(new sObject[1] { new Contact() 
                      {
                            FirstName =results.Tables[1].Rows[count]["firstname"].ToString(),
                            LastName = results.Tables[1].Rows[count]["lastname"].ToString(),
                            Birthdate =Convert.ToDateTime(results.Tables[1].Rows[count]["dateofBirth"].ToString()),
                            BirthdateSpecified=true,
                          //  Owner__c = Convert.ToBoolean(results.Tables[1].Rows[count]["isowner"].ToString()),
                            Id = results.Tables[1].Rows[count]["accountId"].ToString(),
                            Tel_Residencial_Propietario__c = results.Tables[1].Rows[count]["homephone"].ToString(),
                            Celular_Propietario__c = results.Tables[1].Rows[count]["cellphone"].ToString(),
                            AccountId=results.Tables[1].Rows[count]["maccountId"].ToString(),
                            //MailingCity=results.Tables[1].Rows[count]["city"].ToString(),
                            //MailingCountry=results.Tables[1].Rows[count]["country"].ToString(),
                            //MailingState=results.Tables[1].Rows[count]["state"].ToString(),
                            //MailingStreet=string.Concat(results.Tables[1].Rows[count]["address1"].ToString(),results.Tables[1].Rows[count]["address2"].ToString()),
                      }
                        });
                        new DataLayer().UpdateSyncStatusContact(results.Tables[1].Rows[count]["accountId"].ToString(), Convert.ToInt64(results.Tables[1].Rows[count]["contactId"].ToString()));
                    }
                }
            }


        }
        

        public void UpdateSalesForce(string merchantID, string accountID)
        {
            // binding.update(new sObject[1] { new Account() { Merchant_ID__c = merchantID, Id = accountID } });
        }
        public void UpdateSalesForce(string accountID)
        {
            binding.update(new sObject[1] { new Account() { Sync_Status__c = "Yes", Id = accountID } });
        }

        public void UpdateMerhantsOffertoSalesForce()
        {
            if (!Authenticate())
                return;

            DataSet ds = new DataLayer().UpdateMerhantsOffertoSalesForce();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Opportunity ops = new Opportunity();
                //Score
                ops.Score__c = ds.Tables[0].Rows[i]["score"].ToString();

                //Average CC sales
                ops.Calculadora_MBP__c = Convert.ToDouble(ds.Tables[0].Rows[i]["avgcc"].ToString());
                ops.Calculadora_MBP__cSpecified = true;
                //Date of offer
                ops.Fecha_Pre_Oferta__c = Convert.ToDateTime(ds.Tables[0].Rows[i]["offerdate"].ToString());
                ops.Fecha_Pre_Oferta__cSpecified = true;
                //Yealry sales 
                ops.Ventas_Brutas_Anuales__c = Convert.ToDouble(ds.Tables[0].Rows[i]["yearlysales"].ToString());
                ops.Ventas_Brutas_Anuales__cSpecified = true;
                //MCA Amount
                ops.Amount = Convert.ToDouble(ds.Tables[0].Rows[i]["mcaamount"].ToString());
               // ops.Amount__cSpecified = true;
                //Owned amount
                ops.DAR_1__c = Convert.ToDouble(ds.Tables[0].Rows[i]["loanamount"].ToString());
                ops.DAR_1__cSpecified = true;
                //retention percentage 
                ops.IDP_1__c = Convert.ToDouble(ds.Tables[0].Rows[i]["retention"].ToString());
                ops.IDP_1__cSpecified = true;
                //turn
                ops.Retorno_Estimado_1__c = Convert.ToDouble(ds.Tables[0].Rows[i]["turn"].ToString());
                ops.Retorno_Estimado_1__cSpecified = true;
                //total owned amount
                ops.Precio_Oferta_Maxima__c = Convert.ToDouble(ds.Tables[0].Rows[i]["loanamount"].ToString());
                ops.Precio_Oferta_Maxima__cSpecified = true;
                ops.Contract_ID__c =ds.Tables[0].Rows[i]["contractid"].ToString();
                //ops.Contract_ID__cSpecified = true;
                //status
                ops.Contract_Status__c = ds.Tables[0].Rows[i]["status"].ToString();
                //decline reason 
                ops.Motivo_Rechazo__c = ds.Tables[0].Rows[i]["reason"].ToString();
                //decline date 
                ops.Fecha_Rechazo__c = DateTime.Now;
                ops.AccountId = ds.Tables[0].Rows[i]["accountid"].ToString();
                //Name
                ops.Name = ds.Tables[0].Rows[i]["name"].ToString();
                //Stage
                ops.StageName = "Active";
                //Close date
                ops.CloseDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["closedate"].ToString());
                ops.CloseDateSpecified = true;

                if (!string.IsNullOrEmpty((ds.Tables[0].Rows[i]["accountid"].ToString())))
                {
                    if (string.IsNullOrEmpty((ds.Tables[0].Rows[i]["offeraccountid"].ToString())))
                    {
                        SaveResult[] result = binding.create(new sObject[1] { ops });
                        new DataLayer().MapOfferSalesForceAccountId(result[0].id, Convert.ToInt64(ds.Tables[0].Rows[i]["offerId"].ToString()));
                    }
                    else
                    {
                        ops.Id = ds.Tables[0].Rows[i]["offeraccountid"].ToString();
                        SaveResult[] result = binding.update(new sObject[1] { ops });
                    }
                }
            }

        }

        #endregion
    }
}

