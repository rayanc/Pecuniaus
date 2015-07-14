using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Pecuniaus.Collection.Models;
using System.Web.Script.Serialization;
using System.Net.Http;
using Pecuniaus.ApiHelper;
using Pecuniaus.UICore.Controllers;
using System.Net.Http.Headers;
using System.Web;
using Pecuniaus.Collection.Repository;
using Pecuniaus.Utilities;
using System.Configuration;
using Pecuniaus.Utilities.Email;
using System.Reflection;

namespace Pecuniaus.Collection.Controllers
{
    public class WorkflowController : UiBaseController
    {
        #region Collections

        #region  Constants
        private string GetScriptFilePath()
        {
            return "~/Uploads/LandLordCall/";
        }
        LawyerSessionRepository lawyerSessionRepository;
        CollectionSessionRepository collectionSessionRepository;
        #endregion

        #region Index
        public WorkflowController()
        {
            lawyerSessionRepository = new LawyerSessionRepository();
            collectionSessionRepository = new CollectionSessionRepository();
        }
        public ActionResult Index()
        {
            Session["RetrivePreventionmodel"] = null;
            Session["CollectionsListingmodel"] = null;
            collectionSessionRepository.ClearAll();
            var model = new CollectionsModel();
            IList<PreventionModel> preventionModel;
            Int64 intUserId = CurrentUserID;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "collections/retriveprevention?AssignedUserId=" + intUserId + "&percentage=0");
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                preventionModel = JsonConvert.DeserializeObject<IList<PreventionModel>>(reader.ReadToEnd());
                model.Prevention = preventionModel;
            }
            Session["RetrivePreventionmodel"] = model;

            IList<CollectionDaysModel> objBE;
            objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "collections/inactivityDays?AssignedUserId=" + intUserId + "&days=0");
            objRequest.Method = "Get";
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<CollectionDaysModel>>(reader.ReadToEnd());
                model.CollectionDays = objBE;
            }
            model.listDays = GetValuesForDays();
            Session["CollectionsListingmodel"] = model;
            return View(model);
        }
        #endregion

        #region Prevention and Collection
        /// <summary>
        /// Data for the prevention screen
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public PartialViewResult RetrivePrevention()
        {
            var model = new CollectionsModel();
            if (Session["RetrivePreventionmodel"] != null)
            {
                model = (CollectionsModel)Session["RetrivePreventionmodel"];
            }
            return (PartialView("_RetrivePrevention", model));
        }

        [HttpPost]       
        public ActionResult Index(CollectionsModel model, string command)
        {
            collectionSessionRepository.ClearAll();
            model.listDays = GetValuesForDays();
            ModelState.Clear();
            Int64 intUserId = CurrentUserID;
            switch (command)
            {
                case "search":
                    model.isCollectionGrid = false;
                    if (model.PreventionSearch != null)
                    {
                        IList<PreventionModel> preventionModel;
                        HttpWebRequest objReq = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "collections/retriveprevention?AssignedUserId=" + intUserId + "&percentage=" + model.PreventionSearch);

                        using (Stream responseStream = objReq.GetResponse().GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            preventionModel = JsonConvert.DeserializeObject<IList<PreventionModel>>(reader.ReadToEnd());
                            if (preventionModel != null && preventionModel.Count > 0 && preventionModel[0].merchantName != null)
                                model.Prevention = preventionModel;
                            else
                                model.Prevention = new List<PreventionModel>();
                            
                        }
                        Session["RetrivePreventionmodel"] = model;
                    }
                    return View(model);
                case "show":
                    model.isCollectionGrid = true;
                    if (model.listDaysActivity != null)
                    {
                        //anchit 
                        IList<CollectionDaysModel> objBE;
                        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "collections/inactivityDays?AssignedUserId=" + intUserId + "&days=" + model.listDaysActivity);
                        objRequest.Method = "Get";
                        using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            objBE = JsonConvert.DeserializeObject<IList<CollectionDaysModel>>(reader.ReadToEnd());
                            if (objBE != null && objBE.Count > 0 && objBE[0].merchantName != null)
                                model.CollectionDays = objBE;
                            else
                                model.CollectionDays = new List<CollectionDaysModel>();

                        }
                        Session["CollectionsListingmodel"] = model;
                    }
                    return View(model);
            }
            return View();
        }

        public PartialViewResult CollectionsListing()
        {
            var model = new CollectionsModel();
            if (Session["CollectionsListingmodel"] != null)
            {
                model = (CollectionsModel)Session["CollectionsListingmodel"];
            }
            return (PartialView("_CollectionsListing", model));
        }
        
        public ActionResult Collections(Int64 merchantID, Int64 contractID, string merchantName, double realTurn)
        {
            collectionSessionRepository.ClearAll();
            MerchantsDetail objMerchant = new MerchantsDetail();
            objMerchant.merchantName = merchantName;
            objMerchant.merchantId = merchantID;
            objMerchant.contractId = contractID;
            objMerchant.realTurn = realTurn;
            collectionSessionRepository.Set(objMerchant);
            return RedirectToAction("CollectionWorkflow", "Workflow");

        }

        private IEnumerable<SelectListItem> GetValuesForDays()
        {
            var docTypes = BaseApiData.GetAPIResult<IList<DropDown>>("collections/GetValuesForDays", () => new List<DropDown>());

            return from iList in docTypes
                   select new SelectListItem
                   {
                       Text = iList.value,
                       Value = iList.keyId.ToString()
                   };
        }

        #endregion

        #region Collection Workflow
        /// <summary>
        /// Contract and Owner Info
        /// </summary>
        /// <param name="merchantID"></param>
        /// <param name="contractID"></param>
        /// <returns></returns>
        //    public ActionResult CollectionWorkflow(Int64 merchantID, Int64 contractID)
        public ActionResult CollectionWorkflow()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            IList<CollectionContractInformation> contractInformation;
            IList<OwnerColModel> owner;
            IList<CollectionActivities> colActivities;
            IEnumerable<SelectListItem> listActivities;            
            if (objCol.MerchantsDetail != null)
            {
                UICore.SessionHelper.SetCurrentMerchant(objCol.MerchantsDetail.merchantId);
                UICore.SessionHelper.SetContractID(objCol.MerchantsDetail.contractId);
                contractInformation = GetContractInformation(objCol.MerchantsDetail.merchantId, objCol.MerchantsDetail.contractId);
                if (contractInformation.Count > 0 && !string.IsNullOrEmpty(contractInformation[0].collectionStatus))
                    objCol.ContractInfo = contractInformation;

                owner = GetOwnerInformation(objCol.MerchantsDetail.merchantId, objCol.MerchantsDetail.contractId);
                if (owner.Count > 0 && !string.IsNullOrEmpty(owner[0].ownerName))
                    objCol.OwnerInfo = owner;

                listActivities = GetActivitiesList();
                objCol.listActivities = listActivities;

                //TODO : response in true/false
                colActivities = GetActivityCheck(objCol.MerchantsDetail.merchantId, objCol.MerchantsDetail.contractId);
                objCol.ColActivities = colActivities;

            }
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            TempData["SuccessMsg"] = "";
            return View(objCol);
        }

        public MerchantsDetail GetMerchantInfo(long merchantId)
        {
            MerchantsDetail model = new MerchantsDetail();
            if (merchantId > 0)
            {
                var apiMethod = string.Format("merchants/merchantinfo/{0}", merchantId);
                model = BaseApiData.GetAPIResult<MerchantsDetail>(apiMethod, () => new MerchantsDetail()) ?? new MerchantsDetail();             
            }
            return model;
        }

        private IList<CollectionContractInformation> GetContractInformation(Int64 merchantID, Int64 contractID)
        {
            var methodAndQuery = "collections/RetriveContractInformation?merchantid=" + merchantID + "&contractID=" + contractID;
            var contractInformation = BaseApiData.GetAPIResult<IList<CollectionContractInformation>>(methodAndQuery, () => new List<CollectionContractInformation>());
            return contractInformation;
        }

        private IList<OwnerColModel> GetOwnerInformation(Int64 merchantID, Int64 contractID)
        {
            var methodAndQuery = "collections/RetriveOwnerInfo?merchantid=" + merchantID + "&contractID=" + contractID;
            var merchantInformation = BaseApiData.GetAPIResult<IList<OwnerColModel>>(methodAndQuery, () => new List<OwnerColModel>());
            return merchantInformation;
        }

        private IEnumerable<SelectListItem> GetActivitiesList()
        {
            var docTypes = BaseApiData.GetAPIResult<IList<DropDown>>("collections/GetCollectionActivities", () => new List<DropDown>());

            return from iList in docTypes
                   select new SelectListItem
                   {
                       Text = iList.value,
                       Value = iList.keyId.ToString()
                   };
        }

        private IList<CollectionActivities> GetActivityCheck(Int64 merchantID, Int64 contractID)
        {
            var methodAndQuery = "collections/ColActivityCheck?merchantid=" + merchantID + "&contractID=" + contractID;
            var collectionInfo = BaseApiData.GetAPIResult<IList<CollectionActivities>>(methodAndQuery, () => new List<CollectionActivities>());
            return collectionInfo;
        }

        [HttpPost]
        public ActionResult SaveCollectionDetails(CollectionsModel model)
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            IList<CollectionPaymentAgreementModel> lstPayment = new List<CollectionPaymentAgreementModel>();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    using (var client = new HttpClient())
                    {
                        if (model.CollectionActivity != null)
                        {
                            model.CollectionActivity.insertUserId = base.CurrentUserID;
                            model.CollectionActivity.merchantId = objCol.MerchantsDetail.merchantId;
                            model.CollectionActivity.contractId = objCol.MerchantsDetail.contractId;
                            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PutAsJsonAsync("collections/InsertCollectionActivity", model.CollectionActivity).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                TempData["SuccessMsg"] = Pecuniaus.Resources.Collection.Collection.CollectionActivities;
                                return RedirectToAction("CollectionWorkflow", "Workflow");
                            }
                        }
                    }
                }
            }
            return RedirectToAction("CollectionWorkflow", "Workflow");
        }
        #endregion

        #region DataCredito
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantID"></param>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult DataCredito()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            objCol.CreditPull=new CreditPullModel();
            objCol.CreditPull.Type = GetCreditPullType();
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            TempData["SuccessMsg"] = "";
            return View(objCol);
        }
        public ActionResult ListCreditReport()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
           // objCol.MerchantsDetail.merchantId = 34;
           // objCol.MerchantsDetail.contractId = 1;
            var apiMethodProcessor = string.Format("creditreport/" + objCol.MerchantsDetail.merchantId + "/" + objCol.MerchantsDetail.contractId);
            IList<CreditReportModel> listcreditreport = BaseApiData.GetAPIResult<IList<CreditReportModel>>(apiMethodProcessor, () => new List<CreditReportModel>());            
            //System.Web.HttpContext.Current.Session["listcreditreport"] = listcreditreport;
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
                return PartialView("_ListCreditReport", listcreditreport);
            }
            return PartialView("_ListCreditReport", new List<CreditReportModel>());
        }

        [HttpPost]
        public ContentResult InsertCreditReport(int type, string chkNoReport)
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            var apiMethod = string.Format("creditreport/{0}?contractId={1}&reporttype={2}&reportAvailable={3}&isCompleted={4}", objCol.MerchantsDetail.merchantId, objCol.MerchantsDetail.contractId, type,chkNoReport, "0");
            var model = BaseApiData.PutAPIData<CreditReportModel>(apiMethod, new CreditReportModel());
            if (model.StatusCode == System.Net.HttpStatusCode.OK)
                SetSuccessMessage(@Pecuniaus.Resources.Collection.Collection.CreditReportPulled);
              //  return RedirectToAction("DataCredito", "Workflow", new { area = "Collection" });                    
            else
                SetSuccessMessage(@Pecuniaus.Resources.Collection.Collection.CreditReportError);
               // return RedirectToAction("DataCredito", "Workflow", new { area = "Collection" }); 
            return Content("True") ;
        }

        private IEnumerable<SelectListItem> GetCreditPullType()
        {
            List<SelectListItem> itemstype = new List<SelectListItem>();
            itemstype.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });
            itemstype.Add(new SelectListItem { Text = "Owner", Value = "1" });
            itemstype.Add(new SelectListItem { Text = "Company", Value = "2" });
            return itemstype;
        }        
        #endregion

        #region Affiliations
        /// <summary>
        /// Affiliations
        /// </summary>
        /// <returns></returns>
        public ActionResult Affiliations()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            TempData["SuccessMsg"] = "";
            return View(objCol);
        }

        /// <summary>
        /// RNC Active Contract
        /// </summary>
        /// <returns></returns>
        public ActionResult RNCActiveContracts()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0)
                {
                    IList<ActiveContracts> objRNCActive;
                    var queryActive = "collections/GetActiveContracts?merchantId=" + objCol.MerchantsDetail.merchantId + "&isRNC=true";
                    objRNCActive = BaseApiData.GetAPIResult<IList<ActiveContracts>>(queryActive, () => new List<ActiveContracts>());
                    objCol.RNCActiveContracts = objRNCActive;
                }
            }
            return PartialView("_RNCActiveContracts", objCol.RNCActiveContracts);
        }

        /// <summary>
        /// RNC In-Active Contract
        /// </summary>
        /// <returns></returns>
        public ActionResult RNCInActiveContracts()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0)
                {
                    IList<InActiveContracts> objRNCInActive;
                    var queryInActive = "collections/GetNonActiveContracts?merchantid=" + objCol.MerchantsDetail.merchantId + "&isRNC=true";
                    objRNCInActive = BaseApiData.GetAPIResult<IList<InActiveContracts>>(queryInActive, () => new List<InActiveContracts>());
                    objCol.RNCInActiveContracts = objRNCInActive;
                }
            }
            return PartialView("_RNCInActiveContracts", objCol.RNCInActiveContracts);
        }

        /// <summary>
        /// Owner Active Contract
        /// </summary>
        /// <returns></returns>
        public ActionResult OwnerActiveContracts()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0)
                {
                    IList<ActiveContracts> objOwnerActive;
                    var queryOwner = "collections/GetActiveContracts?merchantId=" + objCol.MerchantsDetail.merchantId + "&isRNC=false";
                    objOwnerActive = BaseApiData.GetAPIResult<IList<ActiveContracts>>(queryOwner, () => new List<ActiveContracts>());
                    objCol.OwnerActiveContracts = objOwnerActive;
                }
            }
            return PartialView("_OwnerActiveContracts", objCol.OwnerActiveContracts);
        }

        /// <summary>
        /// Owner In-Active Contract
        /// </summary>
        /// <returns></returns>
        public ActionResult OwnerInActiveContracts()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0)
                {
                    IList<InActiveContracts> objOwnerInActive;
                    var queryInActiveOwner = "collections/GetNonActiveContracts?merchantid=" + objCol.MerchantsDetail.merchantId + "&isRNC=false";
                    objOwnerInActive = BaseApiData.GetAPIResult<IList<InActiveContracts>>(queryInActiveOwner, () => new List<InActiveContracts>());
                    objCol.OwnerInActiveContracts = objOwnerInActive;
                }
            }
            return PartialView("_OwnerInActiveContracts", objCol.OwnerInActiveContracts);
        }

        public JsonResult SendRequesttoProcessor(int merchantId)
        {
            var apiMethod = string.Format("merchants/profiles/requestprocessor?merchantid={0}&processorId={1}", merchantId, 0);
            var model = BaseApiData.PostAPIData<CreditVolumesModel>(apiMethod, new CreditVolumesModel());
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Merchant Documents
        /// <summary>
        /// For RetriveAllDocuments
        /// </summary>
        /// <param name="merchantID"></param>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public ActionResult MerchantDocuments()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            TempData["SuccessMsg"] = "";
            return View(objCol);
        }

        public ActionResult GetDocList(int docType)
        {
             CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            MerchantDocumentModel lstMerchantDocument=new MerchantDocumentModel();
            string apiQuery = string.Format("collections/RetriveDocument?merchantId={0}&contractId={1}&documentTypeId={2}",
               objCol.MerchantsDetail.merchantId, objCol.MerchantsDetail.contractId, docType);
            var docs = BaseApiData.GetAPIResult<IList<MerchantDocumentModel>>(apiQuery, () => new List<MerchantDocumentModel>());
            string noImage = "NoImage.jpg";
            foreach (var d in docs)
            {
                if (string.IsNullOrEmpty(d.FileName))
                {
                    d.FileName = noImage;
                    d.FileDetails = "image/jpg";
                }
            }
            lstMerchantDocument = docs.ToList().Last();
            return PartialView("_ListDocs", lstMerchantDocument);
        }

        public ActionResult GetPreviewDoc(int documentId)
        {
             CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            MerchantDocumentModel lstMerchantDocument = new MerchantDocumentModel();
            string apiQuery = string.Format("collections/RetrivePreviewDocument?documentId={0}", documentId);

            var docs = BaseApiData.GetAPIResult<IList<MerchantDocumentModel>>(apiQuery, () => new List<MerchantDocumentModel>());

            string noImage = "NoImage.jpg";
            foreach (var d in docs)
            {
                if (string.IsNullOrEmpty(d.FileName))
                {
                    d.FileName = noImage;
                    d.FileDetails = "image/jpg";
                }
            }
            lstMerchantDocument = docs.ToList().Last();
            return PartialView("_ListDocs", lstMerchantDocument);
        }
        public ActionResult LstMerchantDocument()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            IList<MerchantDocumentModel> lstMerchantDocument;
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "documents/RetriveAllDocuments?merchantId=" + objCol.MerchantsDetail.merchantId + "&contractId=" + objCol.MerchantsDetail.contractId);
                    objRequest.Method = "Get";
                    using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        lstMerchantDocument = JsonConvert.DeserializeObject<IList<MerchantDocumentModel>>(reader.ReadToEnd());
                        objCol.MerchantDoc = lstMerchantDocument;
                    }
                }
            }
            return PartialView("_LstMerchantDocument", objCol.MerchantDoc);
        }

        /// <summary>
        /// Delete Merchant Documents
        /// </summary>
        /// <param name="chckedList"></param>
        /// <returns></returns>
        public JsonResult DeleteMerchantDocuments(string chckedList)
        {
            if (!string.IsNullOrEmpty(chckedList))
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("collections/DeleteMerchantDocs?DocumentIds=" + chckedList.TrimEnd(',')).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                        base.SetSuccessMessage(Pecuniaus.Resources.Collection.Collection.MerchantDocumentsDeleted);
                    else
                        base.SetSuccessMessage(Pecuniaus.Resources.Collection.Collection.MerchantDocumentsDeletedError);
                }
            // TempData["SuccessMsg"] = Pecuniaus.Resources.Collection.Collection.MerchantDocumentsDeleted;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Upload Merchant Document
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadDocument(string UploadSignedDocument)
        {
            var model = new MDScanDocumentModel();
            model.isUploadSignedDoc = true;
            if (UploadSignedDocument != "btUploadSignedDocument")
            {
                model.DocumentTypes = GetDocumentTypes();
                model.isUploadSignedDoc = false;
            }

            return PartialView("_DocumentUpload", model);
        }


        /// <summary>
        /// Get the Document Types
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetDocumentTypes()
        {
            var query = "gen/retrieve/1013";
            var docTypes = BaseApiData.GetAPIResult<IList<MDGeneralModel>>(query, () => new List<MDGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }

        /// <summary>
        /// Save the Uploaded Document
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUploadedDocument(MerchantDocumentModel mod, HttpPostedFileBase file, MDScanDocumentModel model)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    CollectionsModel objCol = new CollectionsModel();
                    objCol.MerchantsDetail = collectionSessionRepository.GetAll();
                    if (objCol.MerchantsDetail != null)
                    {
                        if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                        {
                            if (model.isUploadSignedDoc)
                                mod.DocumentTypeId = 12;

                            var fileName = "doc_" + mod.DocumentId + mod.DocumentTypeId + "_" + objCol.MerchantsDetail.merchantId + "_" + objCol.MerchantsDetail.contractId + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                            file.SaveAs(path);

                            var docModel = new MerchantDocumentModel()
                            {
                                DocumentId = mod.DocumentId,
                                ContractId = objCol.MerchantsDetail.contractId,
                                MerchantId = objCol.MerchantsDetail.merchantId,
                                DocumentTypeId = mod.DocumentTypeId,
                                FileName = fileName,
                                FileDetails = file.ContentType,
                                UploadUserId = base.CurrentUserID,

                            };
                            BaseApiData.PutAPIData<MerchantDocumentModel>("documents/InsertContDocument", docModel);                            
                            SetSuccessMessage(Pecuniaus.Resources.Collection.Collection.DocumentUpdated);                           
                        }
                    }

                }
            }
            else
            {
                SetSuccessMessage(Pecuniaus.Resources.Collection.Collection.ErrorDocumentUpdated);
            }
       
            if (model.isUploadSignedDoc)
                return RedirectToAction("PaymentAgreement", "Workflow", new { area = "Collection" });
            else
                return RedirectToAction("MerchantDocuments", "Workflow", new { area = "Collection" });
        }

        public ActionResult GetImage(int docType)
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    string apiQuery = string.Format("documents/RetriveDocument?merchantId={0}&contractId={1}&documentTypeId={2}", objCol.MerchantsDetail.merchantId, objCol.MerchantsDetail.contractId, docType);
                    var doc = BaseApiData.GetAPIResult<IList<MerchantDocumentModel>>(apiQuery, () => new List<MerchantDocumentModel>());
                    string fileName = "NoImage.jpg";
                    long documentId = 0;
                    if (doc.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(doc[0].FileName))
                            fileName = doc[0].FileName;
                        documentId = doc[0].DocumentId;
                    }
                    return Json(new { FilePath = "/ScanDocuments/" + fileName, DocumentID = documentId }, JsonRequestBehavior.AllowGet);
                }
            }           
            return Json("", JsonRequestBehavior.AllowGet);

          

        }
        #endregion

        #region Landlords        
        public ActionResult Landlord()
        {
            CollectionsModel objCol = new CollectionsModel();
            
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            
            if (objCol.MerchantsDetail != null)
             {
                 if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                 {
                     objCol.PropertyTypes = GetPropertyTypes();
                     objCol.CollectionLandLord = new CollectionLandLordModel();
                     IList<CollectionLandLordModel> obj = new List<CollectionLandLordModel>();
                     var apiMethod = "collections/GetLandLordDetails?merchantId=" + objCol.MerchantsDetail.merchantId;
                     obj = BaseApiData.GetAPIResult<IList<CollectionLandLordModel>>(apiMethod, () => new List<CollectionLandLordModel>());
                     if (obj.Count > 0 && !string.IsNullOrEmpty(obj[0].landlordFirstName))
                         objCol.CollectionLandLord = obj[0];
                     if (objCol.CollectionLandLord != null)
                     {
                         LandlordQuestionsModel questions = new LandlordQuestionsModel();
                         objCol.CollectionLandLord.Questions = new LandlordQuestionsModel();
                         var query = string.Format("contracts/GetLandLordVerification?contractId={0}&merchantid={1}", objCol.MerchantsDetail.contractId, objCol.MerchantsDetail.merchantId);
                         questions = BaseApiData.GetAPIResult<LandlordQuestionsModel>(query, () => new LandlordQuestionsModel());
                         questions.Answers = ConvertQuestionsToModel<LLRightWrong>(questions.Questions);
                         questions.ScriptFilePath = Url.Content(GetScriptFilePath());
                         if (questions != null && !string.IsNullOrEmpty(questions.ScriptFile))
                         {
                             objCol.CollectionLandLord.Questions.ScriptFile = questions.ScriptFile;
                             objCol.CollectionLandLord.Questions.ScriptFilePath = questions.ScriptFilePath;
                         }
                         objCol.CollectionLandLord.Questions = questions;
                     }
                 }
            }
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            TempData["SuccessMsg"] = "";
            return View(objCol);
        }

        private IEnumerable<SelectListItem> GetPropertyTypes()
        {
            var query = "gen/retrieve/1012";
            var docTypes = BaseApiData.GetAPIResult<IList<ColGeneralModel>>(query, () => new List<ColGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }

        private T ConvertQuestionsToModel<T>(IEnumerable<QuestionModel> questions) where T : new()
        {
            var vcAnswers = new T();

            //     var vcAnswers = new T();
            PropertyInfo property = null;
            Type entityType = vcAnswers.GetType();

            if (questions != null)
                foreach (var q in questions)
                {
                    string propName = q.QuestionDesc;
                    try
                    {
                        property = entityType.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    }
                    catch (AmbiguousMatchException)
                    {
                        //If it is an Ambiguous Match, that is we have both properties on this object then get the specific match
                        property = entityType.GetProperty(propName);
                    }

                    if (property != null && q.AnswerDesc != null && property.CanWrite)
                    {
                        property.SetValue(vcAnswers, Convertor.ChangeType(q.AnswerDesc, property.PropertyType), null);                       
                    }
                }
            return vcAnswers;
        }

        [HttpPost]
        public ActionResult UpdateLandlord(CollectionsModel obj, FormCollection formCollection)
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();           
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && obj.CollectionLandLord!=null && !string.IsNullOrEmpty(obj.CollectionLandLord.landlordCompanyName))
                {
                    obj.CollectionLandLord.MerchantID = objCol.MerchantsDetail.merchantId;
                    obj.CollectionLandLord.userId = base.CurrentUserID;
                    var response = BaseApiData.PutAPIData<CollectionLandLordModel>("collections/UpdateLandlords", obj.CollectionLandLord);                   
                    
                   // Commented as Landlord Answers will not be saved from here
                   // var model = new LandlordQuestionsModel();

                   // var questions = new List<QuestionModel>();

                   // foreach (var key in formCollection.AllKeys)
                   // {
                   //     var value = formCollection[key];

                   //     if (key.ToLower() == "scriptfile")
                   //         model.ScriptFile = value;

                   //     if (key.StartsWith("q_"))
                   //     {
                   //         var qaid = key.Substring(2).Split('-');
                   //         var questionId = Convert.ToInt64(qaid[0]);
                   //         var answerId = Convert.ToInt64(qaid[1]);

                   //         questions.Add(new QuestionModel
                   //         {
                   //             QuestionId = questionId,
                   //             AnswerId = answerId,
                   //             AnswerDesc = value,
                   //             ContractId = ContractID,
                   //             MerchantId = CurrentMerchantID
                   //         });
                   //     }
                   // }

                   // model.questions = questions;                   
                   // if (string.IsNullOrEmpty(model.ScriptFile))
                   //     model.ScriptFile = string.Empty;
                   // var updateQuery = string.Format("contracts/UpdateLandlordVerification?isCompleted={0}", 0);
                   // BaseApiData.PutAPIData(updateQuery, model);

                    SetSuccessMessage(@Pecuniaus.Resources.Collection.Collection.LandlordUpdated);
                }
            }
            objCol.PropertyTypes = GetPropertyTypes();
            //return RedirectToAction("Landlord");
            return View();
        }        

        #endregion

        #region Payment Agreement
        public ActionResult PaymentAgreement()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            objCol.CollectionPaymentAgreement = new CollectionPaymentAgreementModel();
            objCol.CollectionPaymentAgreement.merchantId = objCol.MerchantsDetail.merchantId;
            objCol.CollectionPaymentAgreement.contractId = objCol.MerchantsDetail.contractId;
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            TempData["SuccessMsg"] = "";
            return View(objCol);
        }

        public ActionResult ListCollectionPaymentAgreement()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            IList<CollectionPaymentAgreementModel> lstPayment = new List<CollectionPaymentAgreementModel>();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    var method = "collections/GetPaymentDetails?merchantid=" + objCol.MerchantsDetail.merchantId + "&contractID=" + objCol.MerchantsDetail.contractId;
                    lstPayment = BaseApiData.GetAPIResult<IList<CollectionPaymentAgreementModel>>(method, () => new List<CollectionPaymentAgreementModel>());
                    objCol.LstCollectionPaymentAgreement = lstPayment;
                }
            }
            return PartialView("_ListCollectionPaymentAgreement", objCol);
        }

        [HttpPost]
        public ActionResult SavePaymentDetails(CollectionsModel model)
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            IList<CollectionPaymentAgreementModel> lstPayment = new List<CollectionPaymentAgreementModel>();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    using (var client = new HttpClient())
                    {
                        if (model.CollectionPaymentAgreement != null)
                        {
                            model.CollectionPaymentAgreement.insertUserId = base.CurrentUserID;
                            model.CollectionPaymentAgreement.merchantId = objCol.MerchantsDetail.merchantId;
                            model.CollectionPaymentAgreement.contractId = objCol.MerchantsDetail.contractId;
                            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["APIURI"]);
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PutAsJsonAsync("collections/SavePaymentDetails", model.CollectionPaymentAgreement).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                TempData["SuccessMsg"] = Pecuniaus.Resources.Collection.PaymentAgreement.PaymentDetailsAddedSuccessfully;
                                return RedirectToAction("PaymentAgreement", "Workflow");
                            }
                        }
                    }
                }
            }
            return RedirectToAction("PaymentAgreement", "Workflow");
        }

        #endregion

        #region Lawyers
        public ActionResult Legal()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            TempData["SuccessMsg"] = "";
            return View(objCol);
        }

        /// <summary>
        ///  Get all the Assigned Lawyers List
        /// </summary>
        /// <returns></returns>
        public ActionResult ListAssignedLawyers()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    IList<LawyerModel> assignedLawyers;
                    var method = "collections/GetAssignedLawyers?merchantid=" + objCol.MerchantsDetail.merchantId + "&contractID=" + objCol.MerchantsDetail.contractId;
                    assignedLawyers = BaseApiData.GetAPIResult<IList<LawyerModel>>(method, () => new List<LawyerModel>());
                    objCol.AssignedLawyers = assignedLawyers;
                }
            }
            return PartialView("_ListAssignedLawyers", objCol.AssignedLawyers);
        }

        /// <summary>
        /// Get all the Lawyers List
        /// </summary>
        /// <returns></returns>
        public ActionResult ListAssignNewLawyer()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    IList<LawyerModel> lawyerstoAssign;
                    var methodAndQuery = "collections/GetAllLawyersToAssign?merchantid=" + objCol.MerchantsDetail.merchantId + "&contractID=" + objCol.MerchantsDetail.contractId;
                    lawyerstoAssign = BaseApiData.GetAPIResult<IList<LawyerModel>>(methodAndQuery, () => new List<LawyerModel>());
                    objCol.LawyersToAssign = lawyerstoAssign;
                }
            }
            return PartialView("_ListAssignNewLawyer", objCol.LawyersToAssign);
        }
        /// <summary>
        /// List the documents 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListDocuments()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    IList<LegalDocuments> lawyerdocuments;
                    var methodAndQuery = "collections/GetLegalDocuments?merchantid=" + objCol.MerchantsDetail.merchantId + "&contractID=" + objCol.MerchantsDetail.contractId;
                    lawyerdocuments = BaseApiData.GetAPIResult<IList<LegalDocuments>>(methodAndQuery, () => new List<LegalDocuments>());
                    objCol.LawyerDocuments = lawyerdocuments;
                }
            }
            return PartialView("_ListDocuments", objCol.LawyerDocuments);
        }

        /// <summary>
        /// Assgin New lawyer & document
        /// </summary>
        /// <param name="chckedList"></param>
        /// <param name="merchantID"></param>
        /// <param name="contractID"></param>
        /// <param name="lawyerID"></param>
        /// <returns></returns>
        public JsonResult AssignNewLawyer(string chckedList, Int64 merchantID, Int64 contractID, Int64 lawyerID)
        {
            LawyerModel model = new LawyerModel();
            model.merchantId = merchantID;
            model.contractId = contractID;
            model.lawyerId = lawyerID;
            model.documentType = chckedList;
            model.dateOfAssignment = new DateTime();
            model.insertUserId = CurrentUserID;
            var updateQuery = string.Format("collections/AssignLawyer");
            BaseApiData.PutAPIData<LawyerModel>(updateQuery, model);
            SetSuccessMessage(Pecuniaus.Resources.Collection.Collection.LawyerAssigned);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNewLawyer()
        {
            lawyerSessionRepository.ClearAll();
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();

            
           if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    IList<LawyerModel> lawyerstoAssign;
                    var methodAndQuery = "collections/GetAllLawyersToAssign?merchantid=" + objCol.MerchantsDetail.merchantId + "&contractID=" + objCol.MerchantsDetail.contractId;
                    lawyerstoAssign = BaseApiData.GetAPIResult<IList<LawyerModel>>(methodAndQuery, () => new List<LawyerModel>());
                    foreach (var model in lawyerstoAssign)
                    {
                        lawyerSessionRepository.Add(model);
                    }
                    objCol.LawyersToAssign = lawyerstoAssign;
                }
            }

           
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            return View(objCol);
        }

        public ActionResult ListLawyers()
        {
            List<LawyerModel> lawyers = lawyerSessionRepository.GetAll();
            if (lawyers.Count > 0)
            {
                lawyers=lawyers.Where(a=>a.isDeleted==false).ToList();
 
            }
            return PartialView("_ListLawyers", lawyers);
        }

        /// <summary>
        ///  Add New Lawyer
        /// </summary>
        /// <returns></returns>
        public ActionResult AddLawyer()
        {
            return PartialView("_AddLawyer");
        }

        /// <summary>
        ///  Add New Lawyer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddLawyer(LawyerModel model)
        {
            if (ModelState.IsValid)
            {
                lawyerSessionRepository.Add(model);
                return RedirectToAction("AddLawyer");
            }
            return PartialView("_AddLawyer", model);
        }

        /// <summary>
        ///  Edit New Lawyer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditLawyer(long Id)
        {
            var model = lawyerSessionRepository.GetByID(Id);
            return PartialView("_EditLawyer", model);
        }

        /// <summary>
        /// Edit & update New Lawyer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditLawyer(LawyerModel model)
        {
            if (ModelState.IsValid)
            {
                lawyerSessionRepository.Update(model);
                return RedirectToAction("AddLawyer");
            }
            return PartialView("_EditLawyer", model);
        }

        /// <summary>
        /// Delete the lawyer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelLawyer(long id)
        {
            lawyerSessionRepository.Delete(id);
            return Json("OK");
        }

        /// <summary>
        /// Add new lawyer
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUpdateLawyers()
        {
            CollectionsModel model = new CollectionsModel();
            model.AssignedLawyers = lawyerSessionRepository.GetAll();            
            var updateQuery = string.Format("collections/InsUpdateLawyers");
            BaseApiData.PutAPIData<CollectionsModel>(updateQuery, model);
            SetSuccessMessage(Pecuniaus.Resources.Collection.Collection.LawyerAdded);
            return RedirectToAction("CreateNewLawyer", "Workflow");

        }
        #endregion

        #region Generate Letters

        public JsonResult GenerateLetter()
        {
            CollectionsModel objCol = new CollectionsModel();
            objCol.MerchantsDetail = collectionSessionRepository.GetAll();
            objCol.CollectionPaymentAgreement = new CollectionPaymentAgreementModel();
            string emailBody = string.Empty;
            if (objCol.MerchantsDetail != null)
            {
                if (objCol.MerchantsDetail.merchantId > 0 && objCol.MerchantsDetail.contractId > 0)
                {
                    var method = "collections/SendCollectionLetter?merchantid=" + objCol.MerchantsDetail.merchantId + "&contractID=" + objCol.MerchantsDetail.contractId;
                    IList<GenerateLetter> lstLetters= BaseApiData.GetAPIResult<IList<GenerateLetter>>(method, () => new List<GenerateLetter>());
                    if (lstLetters.Count > 0)
                    {
                        emailBody = CollectionLetterDetails(lstLetters);
                        new Emailer().Send(lstLetters[0].toEmail, Pecuniaus.Resources.Collection.PaymentAgreement.CollectionLetter, emailBody, "");
                        base.SetSuccessMessage(Pecuniaus.Resources.Collection.PaymentAgreement.GenerateLetter);
                    }
                    else
                        base.SetSuccessMessage(Pecuniaus.Resources.Collection.PaymentAgreement.GenerateLetterError);
                }
            }
            //TempData["SuccessMsg"] = Pecuniaus.Resources.Collection.PaymentAgreement.GenerateLetter;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private string CollectionLetterDetails(IList<GenerateLetter> letters)
        {
            StringBuilder strCol = new StringBuilder();
            if (letters.Count > 0 && !string.IsNullOrEmpty(letters[0].merchantName))
            {               
                strCol.Append(GetEmailContents("CollectionLetter.html"));
                strCol.Replace("$$CurrentDate$$", Convert.ToString(letters[0].currentDate));
                strCol.Replace("$$LegalCompanyName$$", letters[0].legalName);
                strCol.Replace("$$Authorizedowner$$", letters[0].ownerName);
                strCol.Replace("$$Address$$", letters[0].address);
                strCol.Replace("$$Province$$", letters[0].province);
                strCol.Replace("$$ContractNumber$$", letters[0].contractNumber);
                strCol.Replace("$$MerchantName$$", letters[0].merchantName);
                strCol.Replace("$$Dateoflastactivity$$", Convert.ToString(letters[0].dateLastActivity));
                strCol.Replace("$$PendingAmount$$", Convert.ToString(letters[0].pendingAmount));               
            }
            return strCol.ToString();
        }

        /// <summary>
        /// Get Contents
        /// </summary>
        /// <param name="emailTemplate">Email Template</param>
        /// <returns></returns>
        /// use this method in UI
        /// StringBuilder body = new StringBuilder();
        /// body.Append(Contents.GetEmailContents(EmailTemplates.CollectionLetter));
        /// body.Replace("$$UserName$$", "abcd");
        /// body.Replace("$$Password$$", "1233");
        public static string GetEmailContents(string fileName)
        {
           
            string fileContents = string.Empty;
            string templateFolder = System.Web.HttpContext.Current.Request.MapPath("~/Templates");  
            string filePath = Path.Combine(templateFolder, fileName);
            if (System.IO.File.Exists((filePath)))
                fileContents = System.IO.File.ReadAllText(filePath);
            return fileContents;
        }       

        #endregion

        #endregion
    }


}