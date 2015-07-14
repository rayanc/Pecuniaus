using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using Pecuniaus.Renewal.Models;
using Pecuniaus.Renewal.Repository;
using Pecuniaus.UICore;

namespace Pecuniaus.Renewal.Controllers
{
    public class ReviewController : BaseController
    {
        # region Local Variable
        private RenewalApi renewalApi;
        OfferSesssionRepository offerSesssionRepository;
        private ContractApi contractApi;
        MerchantApi merchantApi;
        MerchantInformationOfferModel _mrcInformation;


        #endregion

        #region Constructor
        public ReviewController()
        {
            renewalApi = new RenewalApi();
            contractApi = new ContractApi();
            offerSesssionRepository = new OfferSesssionRepository();
            merchantApi = new MerchantApi();
        }
        #endregion

        public ActionResult Index()
        {
            MerchantInformationOfferModel model = merchantApi.GetOfferDetails(CurrentMerchantID, ContractID);
            if (!string.IsNullOrEmpty(model.reason))
                model.AddManulMccv = true;

            offerSesssionRepository.Set(model.offers);
            SetOfferModifiedFlag(false);
            return View(model);

        }

        [HttpPost]
        public ActionResult Index(string button)
        {
            renewalApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.RWRenewalReview, (int)WorkflowTypes.Renewal, ContractID);            
            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Review") });
            }
            base.SetSuccessMessage("Task Completed");
            return RedirectToAction("Index", "Review");
        }

        public ActionResult Documents()
        {
            var model = new Pecuniaus.Renewal.Models.ScanDocumentModel();
            renewalApi = new RenewalApi();
            var docTypes = renewalApi.GetDocument((int)Pecuniaus.UICore.WorkflowTypes.Contract);
            model.DocumentTypes = from c in docTypes
                                  select new SelectListItem
                                  {
                                      Text = c.DocumentName,
                                      Value = c.DocumentTypeId.ToString()
                                  };
            return PartialView("_ScanDoc", model);
        }        

        #region Offer

        public ActionResult MCCVInformation()
        {
            //Offer List
            MerchantInformationOfferModel model = merchantApi.GetOfferDetails(CurrentMerchantID, ContractID);
            if (!string.IsNullOrEmpty(model.reason))
                model.AddManulMccv = true;

            offerSesssionRepository.Set(model.offers);
            return PartialView("_MCCVInformation", model);

        }

        [HttpPost]
        public ActionResult SaveOffer(MerchantInformationOfferModel model)
        {
            if (ModelState.IsValid)
            {
                model.offers = offerSesssionRepository.GetAll();
                var apiMethod = string.Format("CreditReport/offers/create?merchantId={0}&contractId={1}&isCompleted={2}", CurrentMerchantID, ContractID, 0);
                BaseApiData.PostAPIData(apiMethod, model);
            }
            return PartialView("OfferIndex", model);
        }


        [HttpPost]
        public ActionResult GenratePDF(string email)
        {
            List<OfferModel> offers = offerSesssionRepository.GetAll();
            return new Rotativa.PartialViewAsPdf("_ListOffer", offers) { FileName = "OfferPdf.pdf" };
        }


        public ActionResult ListOffers()
        {
            List<OfferModel> offers = offerSesssionRepository.GetAll();

            if (Session["OferModifiedFlag"] != null)
            {
                if ((bool)Session["OferModifiedFlag"] == true)
                    ViewBag.OfferModifed = true;
            }
            return PartialView("_ListOffer", offers);
        }

        [HttpGet]
        public ActionResult AddOffer()
        {
            OfferModel mod = new OfferModel();
            //mod.MaxAnnualSalesAmount = Convert.ToDouble(Session["maxvalue"].ToString());
            //mod.MaxOwnedAmount = Convert.ToDouble(Session["maxvalue"].ToString()) * 2;
            return PartialView("_AddOffer", mod);
        }

        [HttpPost]
        public ActionResult AddOffer(OfferModel model)
        {
            if (ModelState.IsValid)
            {

                SetOfferModifiedFlag(true);
                offerSesssionRepository.AddOffer(model);
                return RedirectToAction("AddOffer");
            }
            return PartialView("_AddOffer", model);
        }

        [HttpGet]
        public ActionResult EditOffer(long id)
        {
            var model = offerSesssionRepository.GetByID(id);
            //model.MaxAnnualSalesAmount = Convert.ToDouble(Session["maxvalue"].ToString());
            //model.MaxOwnedAmount = Convert.ToDouble(Session["maxvalue"].ToString()) * 2;
            model.IsUpdate = true;
            return PartialView("_AddOffer", model);
        }

        private void SetOfferModifiedFlag(bool yes)
        {
            Session["OferModifiedFlag"] = yes;
        }
        [HttpPost]
        public ActionResult UpdateOffer(OfferModel model)
        {
            if (ModelState.IsValid)
            {
                SetOfferModifiedFlag(true);
                offerSesssionRepository.Update(model);
                return RedirectToAction("AddOffer");
            }

            return PartialView("_AddOffer", model);
        }

        [HttpPost]
        public ActionResult DelOffer(long id)
        {
            offerSesssionRepository.DelOffer(id);

            return Json("OK");
        }
        #endregion

        #region Merchant Evaluation

        public ActionResult Evaluluation()
        {
            var apiMethodProcessor = string.Format("creditreport/information/" + CurrentMerchantID + "?contractId=" + ContractID);
            var modelmerchantinfo = BaseApiData.GetAPIResult<RetrieveMerchantInformationModel>(apiMethodProcessor, () => new RetrieveMerchantInformationModel()) ?? new RetrieveMerchantInformationModel();
            RetrieveMerchantInformationModel MerchantInformation = modelmerchantinfo;
            return PartialView("_Evaluluation", modelmerchantinfo);

        }
        /// <summary>
        /// Calling Credit Pull Action Result
        /// </summary>
        /// <returns></returns>
        public ActionResult CreditPull()
        {
            return PartialView("CreditPull", new CreditPullModel
            {
                Type = GetType()

            });
        }
        /// <summary>
        /// Fill Credit Pull Type Dropdown
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetType()
        {
            List<SelectListItem> itemstype = new List<SelectListItem>();
            itemstype.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });
            itemstype.Add(new SelectListItem { Text = "Owner", Value = "1" });
            itemstype.Add(new SelectListItem { Text = "Company", Value = "2" });
            return itemstype;
        }

        // <summary>
        /// Parse byte array to json
        /// </summary>
        /// <param name="rawdata"></param>
        /// <returns></returns>
        public static CreditReportBase Data(string rawdata)
        {
            CreditReportBase cbase = new CreditReportBase();

            string filePath = string.Empty;
            string reportBolb = string.Empty;
            filePath = Path.Combine(ConfigurationManager.AppSettings["ReportPhysicalPath"], rawdata);
            using (StreamReader sr = System.IO.File.OpenText(filePath))
            {
                reportBolb = sr.ReadToEnd();
            }
            cbase = CreateJsonActual(reportBolb);
            return cbase;

        }
        public static CreditReportBase CreateJsonActual(string jsondata)
        {
            CreditReportBase cbase = new CreditReportBase();
            cbase = GenerateJson(jsondata);
            return cbase;
        }
        /// <summary>
        /// Parse Json to the entity
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static CreditReportBase GenerateJson(string jsonData)
        {
            CreditReportBase cbase = new CreditReportBase();
            Individual ind = new Individual();
            Company com = new Company();

            var sr = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);

            //  var sr = Newtonsoft.Json.JsonConvert.DeserializeObject(compJson);//
            JObject obj = (JObject)sr;

            var baseToken = obj.SelectToken("c").SelectToken("DCR");
            var individualToken = baseToken.SelectToken("Individuo");
            var securityToken = baseToken.SelectToken("Seguridad");
            var creditAnalysisToken = baseToken.SelectToken("AnalisisCrediticio");
            var breakDownCreditsToken = baseToken.SelectToken("DesgloseCreditos");
            //var contactsToken = baseToken.SelectToken("PersonasRelacionadas");
            var addressesToken = baseToken.SelectToken("Direcciones");
            var telephonesToken = baseToken.SelectToken("Telefonos");
            var productToken = breakDownCreditsToken.SelectToken("Producto");
            //  var accountToken = productToken.SelectToken("Cuenta");
            var companyToken = baseToken.SelectToken("Empresa");
            var personRelationToken = baseToken.SelectToken("PersonasRelacionadas");
            var errorHandling = baseToken.SelectToken("ErrorHandling");
            //var foto = baseToken.SelectToken("Foto");
            cbase.Photo = baseToken.SelectToken("Foto").ToObject<string>();
            cbase.Security = securityToken.ToObject<Security>();

            if (errorHandling != null)
            {
                cbase.ErrorHandling = errorHandling.ToObject<ErrorHandling>();
            }

            if (individualToken != null)
            {
                cbase.Individual = individualToken.ToObject<Individual>();
            }
            if (companyToken != null)
            {
                cbase.Company = companyToken.ToObject<Company>();
            }

            if (creditAnalysisToken != null)
            {
                cbase.creditAnalysis = new CreditAnalysis();

                var cantidadTotalCuentasToken = creditAnalysisToken.SelectToken("CantidadTotalCuentas");
                if (cantidadTotalCuentasToken != null)
                {
                    var moneda = cantidadTotalCuentasToken.SelectToken("Moneda");


                    TotalAmountAccounts totalAmtAcc = new TotalAmountAccounts();

                    if (moneda != null)
                    {
                        if (moneda.Type == JTokenType.Array)
                            totalAmtAcc.Currencies = moneda.ToObject<List<TotalAmountAccounts.Currency>>();
                        else
                        {
                            totalAmtAcc.Currencies = new List<TotalAmountAccounts.Currency>() { moneda.ToObject<TotalAmountAccounts.Currency>() };
                        }
                    }
                    var jTotSale = cantidadTotalCuentasToken.SelectToken("Totales");
                    if (jTotSale != null)
                        totalAmtAcc.Totals = jTotSale.ToObject<Totales>();

                    cbase.creditAnalysis.TotalAmountAccount = totalAmtAcc;

                }
            }

            if (productToken != null)
            {
                List<Product> prodList = new List<Product>();
                List<JToken> productList;
                if (productToken.Type == JTokenType.Array)
                {
                    productList = productToken.ToList();
                    foreach (var item in productList)
                    {
                        var pro = ParseProduct(item);
                        if (pro != null)
                        {
                            prodList.Add(pro);
                        }
                    }
                }
                else
                {
                    var pro = ParseProduct(productToken);
                    if (pro != null)
                    {
                        prodList.Add(pro);
                    }
                }

                cbase.Products = prodList;
                cbase.CurrencyWiseProducts = GetCurrencyWiseProducts(prodList);
            }

            if (telephonesToken != null)
            {
                var teldData = telephonesToken.SelectToken("Tel");
                if (teldData != null)
                {
                    if (teldData.Type == JTokenType.Array)
                        cbase.Telphones = teldData.ToObject<List<Telephone>>();
                    else
                        cbase.Telphones = new List<Telephone>() { teldData.ToObject<Telephone>() };

                }
            }

            if (addressesToken != null)
            {
                var jDir = addressesToken.SelectToken("Dir");
                if (jDir.Type == JTokenType.Array)
                    cbase.Addresses = jDir.ToObject<List<Addresses>>();
                else
                    cbase.Addresses = new List<Addresses>() { jDir.ToObject<Addresses>() };
            }

            if (personRelationToken != null)
            {
                var personRelationList = personRelationToken.SelectToken("Persona");
                if (personRelationList != null)
                {
                    if (personRelationList.Type == JTokenType.Array)
                    {
                        cbase.Persons = personRelationList.ToObject<List<PersonRelation>>();
                    }
                    else
                    {
                        cbase.Persons = new List<PersonRelation> {
                            personRelationList.ToObject<PersonRelation>() };
                    }
                }
            }
            return cbase;
        }

        private static Product ParseProduct(JToken item)
        {
            Product prod = new Product();

            if (item.SelectToken("@Id") != null)
                prod.code = item.SelectToken("@Id").ToString();
            var accountsList = item.SelectToken("Cuenta");
            if (accountsList != null)
            {
                var accnts = new List<Account>();
                if (accountsList.Type == JTokenType.Array)
                {
                    foreach (var ceunta in accountsList)
                    {
                        var acc = ParseAccount(ceunta);
                        accnts.Add(acc);
                    }
                }
                else
                {
                    var acc = ParseAccount(accountsList);
                    accnts.Add(acc);
                }
                prod.Accounts = accnts;
                return prod;
                //   prodList.Add(prod);
            }
            return null;
        }

        private static Account ParseAccount(JToken ceunta)
        {
            //  var acc =  new Account();
            var acc = ceunta.ToObject<Account>();

            acc.currency = ceunta.SelectToken("Moneda").SelectToken("@Des_Moneda").ToString();
            acc.CurrencyId = ceunta.SelectToken("Moneda").SelectToken("#text").ToString();

            return acc;

        }

        private static List<Product> GetCurrencyWiseProducts(List<Product> products)
        {
            var prodList = new List<Product>();
            foreach (var prod in products)
            {
                //   var distAff = accnts.Select(o => o.affiliates).Distinct().ToList();

                var currList = prod.Accounts.Select(p => p.CurrencyId).Distinct().ToList();
                if (currList.Count > 1)
                {
                    foreach (var c in currList)
                    {
                        var product = prod.ShallowCopy();
                        product.CurrencyId = c;
                        product.Accounts = prod.Accounts.Where(m => m.CurrencyId == c).ToList();

                        var firstProduct = product.Accounts.FirstOrDefault();
                        product.Currency = firstProduct.currency;
                        prodList.Add(product);
                    }
                }
                else
                {
                    var firstProduct = prod.Accounts.FirstOrDefault();
                    if (firstProduct != null)
                    {
                        prod.Currency = firstProduct.currency;
                        prod.CurrencyId = firstProduct.CurrencyId;
                    }
                    prodList.Add(prod);
                }

            }
            return prodList;
        }
        /// <summary>
        /// Calling List Credit Report Action
        /// </summary>
        /// <returns></returns>
        public ActionResult ListCreditReport()
        {
            var apiMethodProcessor = string.Format("creditreport/" + CurrentMerchantID + "/" + ContractID);
            var modelProcessor = BaseApiData.GetAPIResult<List<CreditReportModel>>(apiMethodProcessor, () => new List<CreditReportModel>());
            List<CreditReportModel> listcreditreport = modelProcessor;
            //RetrieveMerchantInformationModel retrievemerchantmodel = new RetrieveMerchantInformationModel();
            List<CreditReportModel> volumelistCompany = listcreditreport.Where(c => c.type == "C").ToList();
            List<CreditReportModel> volumelistOwner = listcreditreport.Where(c => c.type == "O").ToList();
            if (listcreditreport != null && listcreditreport.Count > 0)
            {
                if (volumelistCompany.Count > 0)
                    listcreditreport[0].IsCompany = "1";
                else
                    listcreditreport[0].IsCompany = "";
                if (volumelistOwner.Count > 0)
                    listcreditreport[0].IsOwner = "1";
                else
                    listcreditreport[0].IsOwner = "";
                for (int k = 0; k < listcreditreport.Count; k++)
                {
                    listcreditreport[k].timeofreport = DateTime.Parse(listcreditreport[k].timeofreport).ToString("yyyy-MM-dd");
                    if (listcreditreport[k].type == "C")
                        listcreditreport[k].type = "Company";
                    else
                        listcreditreport[k].type = "Owner";
                }
                return PartialView("ListCreditReport", listcreditreport);
            }
            return PartialView("ListCreditReport", new List<CreditReportModel>());
        }

        [HttpPost]
        public ActionResult SaveMerchantEvaluation(RetrieveMerchantInformationModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.score))
                {
                    TempData["SuccessMsg"] = "Can not complete the task without score";
                }
                else
                {
                    var apiMethod = string.Format("creditreport/save?tasktypeid={0}&merchantId={1}&contractId={2}&score={3}&iscompleted={4}&workflowid={5}", TaskTypes.RWRenewalReview, CurrentMerchantID, ContractID, model.score, "0", (int)Pecuniaus.UICore.WorkflowTypes.Renewal);
                    var response = BaseApiData.PostAPIData(apiMethod, null);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["SuccessMsg"] = "Task saved successfully";
                    }

                }
            }
            return RedirectToAction("Evaluluation");
        }

        [HttpPost]
        public ActionResult InsertCreditReport(int type, string chkNoReport)
        {
            var apiMethod = string.Format("creditreport/{0}?contractId={1}&reporttype={2}&reportAvailable={3}&isCompleted={4}", CurrentMerchantID, ContractID, type, chkNoReport, "0");
            var model = BaseApiData.PutAPIData<CreditReportModel>(apiMethod, new CreditReportModel());

            if (model.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("ListCreditReport");
            }
            else
            {
                return RedirectToAction("ListCreditReport");
            }
        }
        /// <summary>
        /// Get Score of the merchant
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetScore()
        {

            var apiMethodProcessor = string.Format("creditreport/" + CurrentMerchantID + "/" + ContractID);
            var reports = BaseApiData.GetAPIResult<List<CreditReportModel>>(apiMethodProcessor, () => new List<CreditReportModel>());
            var ownCnt = reports.Count(a => a.type == "O");
            var comCnt = reports.Count(a => a.type == "C");
            if (ownCnt > 0 && comCnt > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURI"]);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiMethod = string.Format("creditreport/score/" + CurrentMerchantID + "/" + ContractID + "?workflowid=" + WorkflowID + "&tasktypeid=0");
                var model = BaseApiData.GetAPIResult<RetrieveMerchantInformationModel>(apiMethod, () => new RetrieveMerchantInformationModel()) ?? new RetrieveMerchantInformationModel { };

                return Json(new { Error = "", score = model.score, roundedscore = model.roundedscore, finalletter = model.finalletter, rawData = model.rawData }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Error = "NoPull" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult RawScoring()
        {
            return View();
        }

        /// <summary>
        /// Generate PDF Report
        /// </summary>
        /// <param name="creditReportId"></param>
        /// <returns></returns>
        public ActionResult GeneratePDF(long creditReportId, long merchantid, long contractid)
        {
            return new Rotativa.ActionAsPdf("CreatePDF", routeValues: new { creditReportId = creditReportId, merchantid = merchantid, contractid = contractid });
        }

        public ActionResult GetScoringData()
        {
            var apiMethodProcessor = string.Format("creditreport/information/" + CurrentMerchantID + "?contractId=" + ContractID);
            var modelmerchantinfo = BaseApiData.GetAPIResult<RetrieveMerchantInformationModel>(apiMethodProcessor, () => new RetrieveMerchantInformationModel()) ?? new RetrieveMerchantInformationModel();

            if (!string.IsNullOrEmpty(modelmerchantinfo.rawData))
            {
                var users = JObject.Parse(modelmerchantinfo.rawData).SelectToken("Response");
                var data = users.ToObject<ScoringData>();
                return View("ScoringData", data);
            }
            return View("ScoringData");
            //return Content(modelmerchantinfo.rawData);
        }
        /// <summary>
        /// Calling About Action
        /// </summary>
        /// <param name="creditReportId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CreatePDF(long creditReportId, long merchantid, long contractid)
        {
            var apiMethodProcessor = string.Format("creditreport/" + merchantid + "/" + contractid);
            var modelProcessor = BaseApiData.GetAPIResult<List<CreditReportModel>>(apiMethodProcessor, () => new List<CreditReportModel>());
            List<CreditReportModel> listcreditreport = modelProcessor;
            List<CreditReportModel> volumelistFiltered = listcreditreport.Where(c => c.creditReportId == creditReportId).ToList();
            if (volumelistFiltered.Count > 0 && volumelistFiltered[0].isavailable)
            {
                string rawdata = volumelistFiltered[0].rawData;
                CreditReportBase objbase = Data(rawdata);
                return PartialView("CreatePDF", objbase);
            }
            return Content("Reports not available.");
        }

        #endregion

        # region Renewal Contract
        public ActionResult ListOffersContract()
        {
            MerchantInformationOfferModel model = new MerchantApi().GetOfferDetails(CurrentMerchantID, ContractID); 
            for (int i = 0 ; i<= model.offers.Count-1; i++ )
            {
                model.offers[i].Id =i+1;
            }

            return PartialView("_ListOffer", model.offers);
        }
        #endregion
    }
}