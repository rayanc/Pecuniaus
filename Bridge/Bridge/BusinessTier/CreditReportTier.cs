using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Bridge.Models;
using Bridge.Models.General;
using Bridge.Repository;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
namespace Bridge.BusinessTier
{

    public class CreditReportTier : IDisposable
    {
        #region Private Variables
        private ICreditReport _creditRepository;
        #endregion


        #region Contructors
        public CreditReportTier() : this(new CreditRepository()) { }
        public CreditReportTier(ICreditReport creditRepository)
        {
            this._creditRepository = creditRepository;
        }
        #endregion

        //public bool InsertCreditreport(CreditReportModel model, long merchantId, long contractId, string isCompleted) { return false; }
        public bool InsertCreditreport(CreditReportModel model, long merchantId, long contractId, string isCompleted)
        {
            string profileXml;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = false;
            StringBuilder sbXml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sbXml, settings);

            writer.WriteStartElement("merchantinfo");
            writer.WriteStartElement("merchantbasicinfo");
            writer.WriteAttributeString("rnc", Convert.ToString(model.rnc));
            writer.WriteAttributeString("creditscore", Convert.ToString(model.creditscore));
            writer.WriteAttributeString("firstName", Convert.ToString(model.firstName));
            writer.WriteAttributeString("lastName", Convert.ToString(model.lastName));
            writer.WriteAttributeString("name", Convert.ToString(model.name));
            writer.WriteAttributeString("middleName", Convert.ToString(model.middleName));
            writer.WriteAttributeString("probabilityOfDefault", Convert.ToString(model.probabilityOfDefault));

            writer.WriteAttributeString("merchantId", Convert.ToString(model.merchantId));
            writer.WriteAttributeString("contractId", Convert.ToString(model.contractId));
            writer.WriteAttributeString("occupation", Convert.ToString(model.occupation));

            writer.WriteAttributeString("nationality", Convert.ToString(model.nationality));

            writer.WriteAttributeString("timeofreport", string.Format("{0:yyyy-MM-dd}", model.timeofreport));
            writer.WriteAttributeString("monthEvaualted", string.Format("{0:yyyy-MM-dd}", model.monthEvaualted));

            writer.WriteAttributeString("numberofCreditCards", Convert.ToString(model.numberofCreditCards));
            writer.WriteAttributeString("numberofLoans", Convert.ToString(model.numberofLoans));
            writer.WriteAttributeString("numberofOthers", Convert.ToString(model.numberofOthers));
            writer.WriteAttributeString("errors", Convert.ToString(model.errors));
            writer.WriteAttributeString("type", Convert.ToString(model.type));
            writer.WriteAttributeString("commercialname", Convert.ToString(model.commercialname));
            writer.WriteAttributeString("commercialactivity", Convert.ToString(model.commercialactivity));

            writer.WriteEndElement();
            if (model.creditAnalysis != null)
            {
                if (model.creditAnalysis.Count > 0)
                {
                    writer.WriteStartElement("creditReport");
                    foreach (var item in model.creditAnalysis)
                    {

                        writer.WriteStartElement("creditanalysis");
                        writer.WriteAttributeString("approvedcredit", Convert.ToString(item.approvedcredit));
                        writer.WriteAttributeString("firstcreditdate", Convert.ToString(item.firstcreditdate));
                        writer.WriteAttributeString("loanamountinlegal", Convert.ToString(item.loanamountinlegal));
                        writer.WriteAttributeString("loanlateamount", Convert.ToString(item.loanlateamount));
                        writer.WriteAttributeString("loancredit", Convert.ToString(item.loancredit));
                        writer.WriteAttributeString("loanmonthlypayment", Convert.ToString(item.loanmonthlypayment));
                        writer.WriteAttributeString("monthconsolidated", Convert.ToString(item.consolidateMonth));
                        writer.WriteAttributeString("loanowedamount", Convert.ToString(item.loanowedamount));
                        // writer.WriteAttributeString("numberofloans", Convert.ToString(item.numberofloans));
                        writer.WriteAttributeString("reportId", Convert.ToString(item.reportId));
                        writer.WriteAttributeString("typeofActivity", Convert.ToString(item.typeofActivity));
                        writer.WriteAttributeString("currency", Convert.ToString(item.currency));
                        writer.WriteEndElement();

                    }
                    writer.WriteEndElement();
                }
            }

            writer.WriteEndElement();
            writer.Flush();
            profileXml = sbXml.ToString();
            // long rndNum = new Random().Next();
            string fileName = string.Concat(merchantId, "_", contractId, "_", model.type, DateTime.Now.ToString("yyMMddHHmmss"), ".txt");
            string fileLoc = Path.Combine(HttpContext.Current.Server.MapPath("~/CreditFiles/"), fileName);

            if (!File.Exists(fileLoc))
            {
                using (StreamWriter sw = File.CreateText(fileLoc))
                {
                    sw.Write(model.rawData);
                }

            }
            else if (File.Exists(fileLoc))
            {
                using (StreamWriter sw = File.CreateText(fileLoc))
                {
                    sw.Write(model.rawData);
                }

            }
            return _creditRepository.InsertCreditreport(profileXml, merchantId, contractId, isCompleted, model.image, fileName, model.isavailable);
        }
        public IList<CreditReportModel> RetrieveCreditReport(Int64 merchantId, Int64 contractId)
        {
            string filePath = string.Empty;
            IList<CreditReportModel> model = _creditRepository.RetrieveCreditReport(merchantId, contractId);
            /* foreach (var item in model)
             {
                 filePath = string.Empty;
                 filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/CreditFiles/"), item.rawData);
                 using (StreamReader sr = File.OpenText(filePath))
                 {
                     item.rawData = sr.ReadToEnd(); 
                 }
                
             }*/
            return model;
        }
        public IList<OfferModel> RetrieveOffers(Int64 merchantId, Int64 contractId)
        {
            return _creditRepository.RetrieveOffers(merchantId, contractId);
        }
        public bool InsertOffers(MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int iscompleted)
        {
            return _creditRepository.InsertOffers(model, merchantId, contractId, iscompleted);
        }

        public bool InsertSelectOffers(MerchantInformationOfferModel model, Int64 merchantId, Int64 contractId, int iscompleted)
        {
            return _creditRepository.InsertSelectOffers(model, merchantId, contractId, iscompleted);
        }

        public bool OfferAcceptance(OfferModel model, Int64 merchantId, Int64 contractId, int iscompleted)
        {
            return _creditRepository.OfferAcceptance(model, merchantId, contractId, iscompleted);
        }

        public MerchantInformationOfferModel RetrieveMerchantCreditInformation(Int64 merchantId, Int64 contractId)
        {
            return _creditRepository.RetrieveMerchantCreditInformation(merchantId, contractId);
        }
        public bool SelectOffer(Int64 offerId, Int64 merchantId, Int64 contractId, int iscompleted)
        {
            return _creditRepository.SelectOffer(offerId, merchantId, contractId, iscompleted);
        }
        public RetrieveMerchantInformation RetrieveMerchantInformation(Int64 merchantId, Int64 contractid)
        {

            return _creditRepository.RetrieveMerchantInformation(merchantId, contractid);
        }
        public void Dispose()
        {

        }
        public DataSet RetrieveInformationforScorin(Int64 merchantId)
        {
            return _creditRepository.RetrieveInformationforScorin(merchantId);
        }

        public bool CompleteTask(RetrieveMerchantInformation model, Int64 tasktypeid, Int64 merchantId, Int64 contractId, int iscompleted, int workflowid, string rawdata, string requestxml)
        {
            return _creditRepository.CompleteTask(model, tasktypeid, merchantId, contractId, iscompleted, workflowid, rawdata, requestxml);
        }
        public bool CompleteTask(double score, double roundedscore, string finalletter, Int64 tasktypeid, Int64 merchantId, Int64 contractId, int iscompleted, int workflowid, string rawdata, string requestxml)
        {
            return _creditRepository.CompleteTask(score, roundedscore, finalletter, tasktypeid, merchantId, contractId, 0, workflowid, rawdata, requestxml);
        }
        #region Report Data and manipulation

        public bool PullReportbyId(int reporttypeId, Int64 merchantId, Int64 contractId, string isCompleted, int reportAvailable)
        {
            string strgetjson = "";
            string UserName = ConfigurationManager.AppSettings["UserName"];
            string PassWord = ConfigurationManager.AppSettings["PassWord"];
            string xmlParameters = string.Empty;
            string TipoId = string.Empty;
            string Id = string.Empty;
            if (reportAvailable == 0)
            {
                CreditReportModel analysisModel = new CreditReportModel();
                analysisModel.isavailable = 0;
                analysisModel.image = Encoding.ASCII.GetBytes("");
                // string dateString = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime dt = DateTime.Now;
                //string.Concat(dateString.Split('-')[2].Substring(0, 4), "-", dateString.Split('-')[1], "-", dateString.Split('-')[0], " 00:00:00");
                IFormatProvider culture2 = new System.Globalization.CultureInfo("es-DO", true);
                analysisModel.timeofreport = dt;
                analysisModel.monthEvaualted = dt;

                if (reporttypeId == 1)
                {
                    analysisModel.type = "O";
                }
                //Company Report
                else if (reporttypeId == 2)
                {

                    analysisModel.type = "C";
                }
                return InsertCreditreport(analysisModel, merchantId, contractId, isCompleted);
            }
            else
            {
                //Get the values on the basis of the parameters
                CreditReportModel analysisModel = new CreditReportModel();
                if (ConfigurationManager.AppSettings["Testing"] == "0")
                {
                    RetrieveMerchantInformation modelReport = new RetrieveMerchantInformation();
                    using (CreditReportTier mt = new CreditReportTier())
                    {
                        modelReport = mt.RetrieveMerchantInformation(merchantId, contractId);
                    }
                    // Owner Report
                    if (reporttypeId == 1)
                    {
                        TipoId = "C";
                        analysisModel.type = "O";
                        Id = modelReport.ownerId;
                    }
                    //Company Report
                    else if (reporttypeId == 2)
                    {

                        TipoId = "R";
                        analysisModel.type = "C";
                        Id = modelReport.rnc;
                        //  Id = "101088575";
                    }
                    AcmeRuby.clsCaltecServiceSoapClient objServiceReference_AcmeRuby = new AcmeRuby.clsCaltecServiceSoapClient();
                    strgetjson = objServiceReference_AcmeRuby.GetXmlJsonStr(UserName, PassWord, TipoId, Id, xmlParameters);
                    //strgetjson = compJson;
                }
                else
                {
                    //Owner Report
                    if (reporttypeId == 1)
                    {
                        TipoId = "C";
                        Id = "22400033001";
                        //Id = "00113569248";
                        analysisModel.type = "O";

                    }
                    //Company Report
                    else if (reporttypeId == 2)
                    {
                        //Id = "103157531";
                        Id = "101874139";
                        TipoId = "R";
                        analysisModel.type = "C";

                    }
                    AcmeRuby.clsCaltecServiceSoapClient objServiceReference_AcmeRuby = new AcmeRuby.clsCaltecServiceSoapClient();
                    strgetjson = objServiceReference_AcmeRuby.GetXmlJsonStr(UserName, PassWord, TipoId, Id, xmlParameters);
                    // strgetjson = compJson;
                }
                var sr = Newtonsoft.Json.JsonConvert.DeserializeObject(strgetjson);
                JObject obj = (JObject)sr;
                var baseToken = obj.SelectToken("c").SelectToken("DCR");
                var errorToken = baseToken.SelectToken("ErrorHandling");
                if (errorToken.SelectToken("Id").ToString() == "0")
                {

                    var individualToken = baseToken.SelectToken("Individuo");
                    analysisModel.isavailable = 1;
                    var securityToken = baseToken.SelectToken("Seguridad");
                    var creditAnalysisToken = baseToken.SelectToken("AnalisisCrediticio");
                    var breakDownCreditsToken = baseToken.SelectToken("DesgloseCreditos");
                    var productToken = breakDownCreditsToken.SelectToken("Producto");

                    //var accountToken = productToken.SelectToken("Cuenta");
                    var companyToken = baseToken.SelectToken("Empresa");
                    #region Company Information
                    if (companyToken != null)
                    {
                        analysisModel.commercialname = companyToken.SelectToken("NombreComercial").ToString();
                        analysisModel.name = companyToken.SelectToken("Nombre").ToString();
                        analysisModel.rnc = companyToken.SelectToken("RNC").ToString();

                        analysisModel.commercialactivity = companyToken.SelectToken("ActividadComercial").ToString();
                    }
                    else
                    {
                        analysisModel.commercialname = string.Empty;
                        analysisModel.name = string.Empty;
                        analysisModel.rnc = string.Empty;
                        analysisModel.commercialactivity = string.Empty;
                    }
                    #endregion
                    #region Owner Information
                    if (individualToken != null)
                    {
                        analysisModel.firstName = individualToken.SelectToken("Nombres").ToString();
                        analysisModel.lastName = individualToken.SelectToken("Apellidos").ToString();
                        analysisModel.name = string.Concat(analysisModel.firstName, analysisModel.lastName);
                        analysisModel.nationality = individualToken.SelectToken("Nacionalidad").ToString();
                        analysisModel.occupation = individualToken.SelectToken("Ocupacion").ToString();
                        analysisModel.passport = individualToken.SelectToken("Pasaporte").ToString();
                        analysisModel.image = Encoding.ASCII.GetBytes(baseToken.SelectToken("Foto").ToString());
                        if (individualToken.SelectToken("Categoria") != null)
                        {
                            analysisModel.riskCategory = individualToken.SelectToken("Categoria").ToString();
                        }
                    }
                    else
                    {

                        analysisModel.nationality = string.Empty;
                        analysisModel.occupation = string.Empty;
                        analysisModel.passport = string.Empty;
                        analysisModel.image = Encoding.ASCII.GetBytes(baseToken.SelectToken("Foto").ToString());
                    }
                    #endregion
                    #region Genral Information
                    if (securityToken.SelectToken("FechaHora") != null)
                    {
                        string dateString = securityToken.SelectToken("FechaHora").ToString();
                        // DateTime dateTime = new DateTime(Convert.ToInt32(dateString.Split('-')[2].Substring(0, 4)), Convert.ToInt32(dateString.Split('-')[1]), Convert.ToInt32(dateString.Split('-')[0]));
                        dateString = string.Concat(dateString.Split('-')[2].Substring(0, 4), "-", dateString.Split('-')[1], "-", dateString.Split('-')[0], " 00:00:00");
                        IFormatProvider culture2 = new System.Globalization.CultureInfo("es-DO", true);
                        analysisModel.timeofreport = Convert.ToDateTime(dateString);
                        analysisModel.monthEvaualted = Convert.ToDateTime(dateString);
                    }

                    //  analysisModel.rawData = Encoding.ASCII.GetBytes(strgetjson);
                    analysisModel.rawData = strgetjson;
                    analysisModel.probabilityOfDefault = 0d;
                    if (errorToken != null)
                    {
                        analysisModel.errors = errorToken.SelectToken("Description").ToString();
                    }

                    #endregion

                    #region Credit Analysis
                    string code = string.Empty;
                    JToken accounts = string.Empty;

                    if (productToken != null)
                    {
                        List<JToken> productList;
                        List<CreditAnalysis> crreport = new List<CreditAnalysis>();
                        if (productToken.Type == JTokenType.Array)
                        {
                            productList = productToken.ToList();
                        }
                        else
                        {
                            productList = new List<JToken>() { productToken };
                        }

                        int iotherAccount = 0;

                        foreach (var item in productList)
                        {
                            if (productToken.Type == JTokenType.Array)
                            {

                                code = item.SelectToken("@Id").ToString();
                                accounts = item.SelectToken("Cuenta");
                            }
                            else
                            {
                                code = productToken.SelectToken("@Id").ToString();
                                accounts = productToken.SelectToken("Cuenta");
                            }
                            var accountsList = accounts;

                            if (accountsList.Type == JTokenType.Array)
                            {


                                foreach (var ceunta in accountsList)
                                {
                                    CreditAnalysis analysis = new CreditAnalysis();

                                    //If loan then
                                    if (code.Equals("Préstamo"))
                                    {
                                        analysis.typeofActivity = "1";
                                        analysisModel.numberofLoans = accountsList.Count();
                                    }
                                    else if (code.Equals("Tarjeta de Crédito"))
                                    {
                                        //Credit Card
                                        analysis.typeofActivity = "2";
                                        analysisModel.numberofCreditCards = accountsList.Count();

                                    }
                                    else
                                    {
                                        iotherAccount += 1;
                                        //Others
                                        analysis.typeofActivity = "3";
                                        analysisModel.numberofOthers = iotherAccount;

                                    }

                                    string status = ceunta.SelectToken("Estatus").SelectToken("#text").ToString();
                                    //  if (status == "1")
                                    {
                                        string affliate = ceunta.SelectToken("Afiliado").ToString();
                                        analysis.currency = ceunta.SelectToken("Moneda").SelectToken("@Des_Moneda").ToString();
                                        string dateString = string.Empty;
                                        if (ceunta.SelectToken("FechaApertura") != null)
                                        {
                                            dateString = string.Concat("01-", ceunta.SelectToken("FechaApertura").ToString());
                                            dateString = string.Concat(dateString.Split('-')[2].Substring(0, 4), "-", dateString.Split('-')[1], "-", dateString.Split('-')[0], " 00:00:00");
                                            analysis.firstcreditdate = dateString;
                                        }
                                        analysis.consolidateMonth = string.Concat("01-", (ceunta.SelectToken("MesConsolidado").ToString()));
                                        if (ceunta.SelectToken("TotalAdeudado") != null)
                                        {
                                            if (!String.IsNullOrEmpty(ceunta.SelectToken("TotalAdeudado").ToString())
                                                //  && ceunta.SelectToken("TotalAdeudado").ToString() != "(1)" 
                                            )
                                            {
                                                double value;
                                                double.TryParse(ceunta.SelectToken("TotalAdeudado").ToString(), out value);
                                                analysis.loanowedamount = value;
                                                //    analysis.loanowedamount =  Double.Parse((ceunta.SelectToken("TotalAdeudado").ToString()));
                                            }
                                        }
                                        if (ceunta.SelectToken("CreditoAprobado") != null)
                                        {
                                            if (!String.IsNullOrEmpty(ceunta.SelectToken("CreditoAprobado").ToString()))
                                            {
                                                analysis.approvedcredit = Double.Parse(((ceunta.SelectToken("CreditoAprobado").ToString())));
                                            }
                                        }
                                        if (ceunta.SelectToken("Cuota") != null)
                                        {
                                            if (!String.IsNullOrEmpty(ceunta.SelectToken("Cuota").ToString()))
                                            {
                                                analysis.loanmonthlypayment = Double.Parse(((ceunta.SelectToken("Cuota").ToString())));
                                            }
                                        }
                                        if (ceunta.SelectToken("TotalAtraso") != null)
                                        {
                                            if (!String.IsNullOrEmpty(ceunta.SelectToken("TotalAtraso").ToString()))
                                            {
                                                analysis.loanlateamount = (((ceunta.SelectToken("TotalAtraso").ToString())));
                                            }
                                        }

                                    }
                                    crreport.Add(analysis);
                                }
                            }
                            else
                            {
                                string dateString = string.Empty;

                                CreditAnalysis analysis = new CreditAnalysis();
                                if (code.Equals("Préstamo"))
                                {
                                    analysis.typeofActivity = "1";
                                    analysisModel.numberofLoans = accountsList.Count();
                                }
                                else if (code.Equals("Tarjeta de Crédito"))
                                {
                                    //Credit Card
                                    analysis.typeofActivity = "2";
                                    analysisModel.numberofCreditCards = accountsList.Count();

                                }
                                else
                                {
                                    //Others
                                    iotherAccount += 1;
                                    analysis.typeofActivity = "3";
                                    analysisModel.numberofOthers = iotherAccount;

                                }
                                if (productToken.Type == JTokenType.Array)
                                    analysis.currency = item.SelectToken("Cuenta").SelectToken("Moneda").SelectToken("@Des_Moneda").ToString();
                                else
                                    analysis.currency = productToken.SelectToken("Cuenta").SelectToken("Moneda").SelectToken("@Des_Moneda").ToString();
                                string status = accountsList.SelectToken("Estatus").SelectToken("#text").ToString();
                                //  if (status == "1")
                                {
                                    string affliate = accountsList.SelectToken("Afiliado").ToString();
                                    analysis.currency = accountsList.SelectToken("Moneda").SelectToken("@Des_Moneda").ToString();

                                    if (accountsList.SelectToken("FechaApertura") != null)
                                    {
                                        dateString = string.Concat("01-", accountsList.SelectToken("FechaApertura").ToString());
                                        dateString = string.Concat(dateString.Split('-')[2].Substring(0, 4), "-", dateString.Split('-')[1], "-", dateString.Split('-')[0], " 00:00:00");

                                        IFormatProvider culture2 = new System.Globalization.CultureInfo("es-DO", true);
                                        analysis.firstcreditdate = dateString;
                                    }
                                    analysis.consolidateMonth = (accountsList.SelectToken("MesConsolidado").ToString());
                                    if (accountsList.SelectToken("TotalAdeudado") != null)
                                    {
                                        if (!String.IsNullOrEmpty(accountsList.SelectToken("TotalAdeudado").ToString()))
                                        {
                                            // analysis.loanowedamount +=Double.Parse((ceunta.SelectToken("TotalAdeudado").ToString()));
                                            analysis.loanowedamount = Double.Parse((accountsList.SelectToken("TotalAdeudado").ToString()));
                                        }
                                    }
                                    if (accountsList.SelectToken("CreditoAprobado") != null)
                                    {
                                        if (!String.IsNullOrEmpty(accountsList.SelectToken("CreditoAprobado").ToString()))
                                        {
                                            //analysis.approvedcredit += Double.Parse(((ceunta.SelectToken("CreditoAprobado").ToString())));
                                            analysis.approvedcredit = Double.Parse(((accountsList.SelectToken("CreditoAprobado").ToString())));
                                        }
                                    }
                                    if (accountsList.SelectToken("Cuota") != null)
                                    {
                                        if (!String.IsNullOrEmpty(accountsList.SelectToken("Cuota").ToString()))
                                        {
                                            analysis.loanmonthlypayment = Double.Parse(((accountsList.SelectToken("Cuota").ToString())));
                                        }
                                    }
                                    if (accountsList.SelectToken("TotalAtraso") != null)
                                    {
                                        if (!String.IsNullOrEmpty(accountsList.SelectToken("TotalAtraso").ToString()))
                                        {
                                            analysis.loanlateamount = (((accountsList.SelectToken("TotalAtraso").ToString())));
                                        }
                                    }
                                    crreport.Add(analysis);
                                }


                            }

                        }



                        analysisModel.creditAnalysis = crreport;

                    }
                    #endregion

                    return InsertCreditreport(analysisModel, merchantId, contractId, isCompleted);
                }
                else
                {
                    analysisModel.errors = errorToken.SelectToken("Description").ToString();
                    analysisModel.rawData = strgetjson;
                    analysisModel.isavailable = 0;
                    analysisModel.image = Encoding.ASCII.GetBytes("");
                    var securityToken = baseToken.SelectToken("Seguridad");
                    if (securityToken.SelectToken("FechaHora") != null)
                    {
                        string dateString = securityToken.SelectToken("FechaHora").ToString();
                        // DateTime dateTime = new DateTime(Convert.ToInt32(dateString.Split('-')[2].Substring(0, 4)), Convert.ToInt32(dateString.Split('-')[1]), Convert.ToInt32(dateString.Split('-')[0]));
                        dateString = string.Concat(dateString.Split('-')[2].Substring(0, 4), "-", dateString.Split('-')[1], "-", dateString.Split('-')[0], " 00:00:00");
                        IFormatProvider culture2 = new System.Globalization.CultureInfo("es-DO", true);
                        analysisModel.timeofreport = Convert.ToDateTime(dateString);
                        analysisModel.monthEvaualted = Convert.ToDateTime(dateString);
                    }
                    return InsertCreditreport(analysisModel, merchantId, contractId, isCompleted);
                }
            }

        }
        public string CreateXML(DataSet ds)
        {
            try
            {

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = false;
                StringBuilder sbXml = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sbXml, settings);
                writer.WriteStartElement("Request");
                //

                #region Merchantbasicinfo

                DataTable MerchantInfo = ds.Tables["MerchantInfo"];
                foreach (DataRow row in MerchantInfo.Rows)
                {

                    foreach (DataColumn dataColumn in MerchantInfo.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }

                }

                #endregion
                #region Owners
                DataTable Owners = ds.Tables["Owners"];
                writer.WriteStartElement("Owners");
                foreach (DataRow row in Owners.Rows)
                {
                    writer.WriteStartElement("Owner");
                    foreach (DataColumn dataColumn in Owners.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();


                #endregion
                #region Processor Information
                DataTable Processor = ds.Tables["Processor"];
                writer.WriteStartElement("Processors");
                foreach (DataRow row in Processor.Rows)
                {
                    writer.WriteStartElement("Processor");
                    foreach (DataColumn dataColumn in Processor.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();


                #endregion
                #region BankStatements Information
                DataTable BankStatements = ds.Tables["BankStatements"];
                writer.WriteStartElement("BankStatements");
                foreach (DataRow row in BankStatements.Rows)
                {
                    writer.WriteStartElement("BankStatement");
                    foreach (DataColumn dataColumn in BankStatements.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();


                #endregion
                #region OwnerCreditInfo
                DataTable OwnerCreditInfo = ds.Tables["OwnerCreditInfo"];
                //Write owner credit information starts here

                writer.WriteStartElement("OwnerCreditInfo");
                foreach (DataRow row in OwnerCreditInfo.Rows)
                {
                    foreach (DataColumn dataColumn in OwnerCreditInfo.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                }

                //Write loan
                DataTable OwnerLoans = ds.Tables["OwnerLoans"];
                writer.WriteStartElement("Loans");
                foreach (DataRow row in OwnerLoans.Rows)
                {
                    foreach (DataColumn dataColumn in OwnerLoans.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                }
                writer.WriteEndElement();

                //Other element starts
                DataTable Other = ds.Tables["Other"];
                writer.WriteStartElement("Other");
                foreach (DataRow row in Other.Rows)
                {
                    foreach (DataColumn dataColumn in Other.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                }
                writer.WriteEndElement();
                //Other ends Credit card starts
                DataTable CreditCard = ds.Tables["CreditCard"];
                writer.WriteStartElement("CreditCard");
                foreach (DataRow row in CreditCard.Rows)
                {

                    foreach (DataColumn dataColumn in CreditCard.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }

                }
                writer.WriteEndElement();
                //Credit card ends next line Owner credit ends
                writer.WriteEndElement();
                #endregion
                #region  Company CreditInfo
                DataTable CompanyCreditInfo = ds.Tables["CompanyCreditInfo"];
                //Write Company credit information starts here

                writer.WriteStartElement("CompanyCreditInfo");
                foreach (DataRow row in CompanyCreditInfo.Rows)
                {
                    foreach (DataColumn dataColumn in CompanyCreditInfo.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                }

                //Write loan
                DataTable CompanyLoans = ds.Tables["CompanyLoans"];
                writer.WriteStartElement("CompanyLoans");
                foreach (DataRow row in CompanyLoans.Rows)
                {
                    foreach (DataColumn dataColumn in CompanyLoans.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                }
                writer.WriteEndElement();

                //Other element starts
                DataTable CompanyOthers = ds.Tables["CompanyOthers"];
                writer.WriteStartElement("CompanyOthers");
                foreach (DataRow row in CompanyOthers.Rows)
                {
                    foreach (DataColumn dataColumn in CompanyOthers.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                }
                writer.WriteEndElement();
                //Other ends Credit card starts
                DataTable CompanyCreditCard = ds.Tables["CompanyCreditCard"];
                writer.WriteStartElement("CompanyCreditCard");
                foreach (DataRow row in CompanyCreditCard.Rows)
                {

                    foreach (DataColumn dataColumn in CompanyCreditCard.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }

                }
                writer.WriteEndElement();
                //Credit card ends next line Owner credit ends
                writer.WriteEndElement();
                #endregion
                #region  Credit CardActivity
                DataTable CreditCardActivity = ds.Tables["CreditCardActivity"];
                //Write Company credit information starts here
                writer.WriteStartElement("CreditCardActivity");
                foreach (DataRow row in CreditCardActivity.Rows)
                {
                    foreach (DataColumn dataColumn in CreditCardActivity.Columns)
                    {
                        writer.WriteElementString(dataColumn.ColumnName, row[dataColumn].ToString());
                    }
                }

                //Write total activity
                DataTable CCACCVTotalAmount = ds.Tables["CCACCVTotalAmount"];
                writer.WriteStartElement("CCACCVTotalAmount");
                foreach (DataRow row in CCACCVTotalAmount.Rows)
                {
                    writer.WriteStartElement("M");
                    writer.WriteAttributeString("order", row["orders"].ToString());
                    writer.WriteElementString("CCACCVPeriod", row["CCACCVPeriod"].ToString());
                    writer.WriteElementString("CCACCVAmount", row["CCACCVAmount"].ToString());
                    writer.WriteElementString("CCACCVTickets", row["CCACCVTickets"].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                DataTable CCACCVTotalAmount1 = ds.Tables["CCACCVTotalAmount"];
                writer.WriteStartElement("CCACCVEstimatesCreditCardSales");
                foreach (DataRow row in CCACCVTotalAmount1.Rows)
                {
                    writer.WriteStartElement("M");
                    writer.WriteAttributeString("order", row["orders"].ToString());
                    writer.WriteElementString("CCACCVPeriod", row["CCACCVPeriod"].ToString());
                    writer.WriteElementString("CCACCVAmount", row["CCACCVAmount"].ToString());
                    writer.WriteElementString("CCACCVTickets", row["CCACCVTickets"].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();


                writer.WriteEndElement();
                #endregion
                writer.WriteEndElement();
                writer.Flush();
                return sbXml.ToString();
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }
        #endregion

        public bool UpdateOfferEmailFlag(bool isEmailSent, long offerId, long merchantId, long contractId)
        {
            return _creditRepository.UpdateOfferEmailFlag(isEmailSent,offerId, merchantId, contractId);
        }
        public bool UpdateOfferCreationEmailFlag(bool isEmailSent, long merchantId, long contractId)
        {
            return _creditRepository.UpdateOfferCreationEmailFlag(isEmailSent, merchantId, contractId);
        }

    }
}