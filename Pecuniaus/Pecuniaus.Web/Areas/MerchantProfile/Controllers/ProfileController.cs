using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.MerchantProfile.Models;
using System.Net;
using System.IO;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using System.Net.Http;
using Pecuniaus.UICore.Controllers;
using Pecuniaus.UICore;
using System.Globalization;
using Pecuniaus.Utilities;
using Pecuniaus.Utilities.PDF;
using Pecuniaus.Utilities.Email;
using System.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;
using Pecuniaus.Models.Contract;
using System.Reflection;

namespace Pecuniaus.MerchantProfile.Controllers
{
    public class ProfileController : UiBaseController
    {
        #region  Constants
        private string GetScriptFilePath()
        {
            return "~/Uploads/LandLordCall/";
        }
        # endregion
        public static string RentFlag;
        #region PageLoad
        public ActionResult Index()
        {
            MPMerchantModel MPM = new MPMerchantModel();
            MPM.merchantStatuses = GetMerchantStatuses();
            MPM.Processors = GetProcessors();
            return View(MPM);
        }
        #endregion

        #region Search
        public ActionResult MerchantProfileSearch(string MerchantID, string MerchantStatus, string ProcessorName, string ProcessorNumber,
            string OwnerName, string LegalName, string BusinessName, string OwnerID)
        {
            //IList<MerchantsModel> objBE = new List<MerchantsModel>();
            var apiMethod = "merchants/searchResult?showTemp=0&searchtype=MP";

            if (!string.IsNullOrEmpty(MerchantID))
                apiMethod += "&merchantId=" + MerchantID;

            if (!string.IsNullOrEmpty(MerchantStatus))
                apiMethod += "&statusId=" + MerchantStatus;

            if (!string.IsNullOrEmpty(BusinessName))
                apiMethod += "&businessName=" + BusinessName;

            if (!string.IsNullOrEmpty(LegalName))
                apiMethod += "&legalName=" + LegalName;

            if (!string.IsNullOrEmpty(OwnerName))
                apiMethod += "&ownerName=" + OwnerName;

            if (!string.IsNullOrEmpty(ProcessorNumber))
                apiMethod += "&processornbr=" + ProcessorNumber;

            if (!string.IsNullOrEmpty(ProcessorName))
                apiMethod += "&processorName=" + ProcessorName;

            if (!string.IsNullOrEmpty(OwnerID))
                apiMethod += "&rnc=" + OwnerID;

            apiMethod += "&SearchType=P";
            apiMethod += "&assignedUserId=0";

            IList<SearchResultModel> model = BaseApiData.GetAPIResult<IList<SearchResultModel>>(apiMethod, () => new List<SearchResultModel>());

            if (model.Count > 0)
                RentFlag = model.FirstOrDefault().RentFlag;
            //foreach (SearchResultModel sm in model)
            //{
            //    objBE.Add(new MerchantsModel() { merchantId = sm.MerchantId, merchantName = sm.MerchantName });
            //}

            var DistinctModel = model.Select(o => new { o.MerchantId, o.merchantStatus, o.businessName, o.legalName }).Distinct().ToList();

            List<SearchResultModel> objModel = new List<SearchResultModel>();
            foreach(var obj in DistinctModel)
            {
                objModel.Add(new SearchResultModel() { MerchantId = obj.MerchantId, merchantStatus = obj.merchantStatus, legalName = obj.legalName, businessName = obj.businessName });
            }
            return PartialView(objModel);
        }
        #endregion

        #region Business
        public ActionResult Business(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/business", MerchantID);
            MPMerchantBusinessModel obj = BaseApiData.GetAPIResult<MPMerchantBusinessModel>(apiMethod, () => new MPMerchantBusinessModel());
            obj.Business.IndustryTypes = GetIndustries();
            obj.Business.EntityTypes = GetEntities();
            obj.LegalAddress.Provinces = GetProvinces();
            obj.PhysicalAddress.Provinces = GetProvinces();
            SessionHelper.SetCurrentMerchant(obj.MerchantID);
            return PartialView("_MerchantProfileDetails", obj);
        }

        public ActionResult BusinessProcessorInformation(string Trmnl, string PName, string PNo, DateTime? DGP, int? IndID, DateTime? FPDate, int? PId)
        {
            MPMerchantProcessorInfoModel pro = new MPMerchantProcessorInfoModel();
            pro.DateGracePeriod = DGP;
            pro.FirstProcessedDate = FPDate;
            pro.IndustryTypeID = Convert.ToInt32(IndID);
            pro.processorId = Convert.ToInt64(PId);
            pro.ProcessorName = PName;
            pro.ProcessorNumber = PNo;
            pro.Terminal = Convert.ToInt64(Trmnl);
            pro.Processors = GetProcessors();
            pro.IndustryTypes = GetIndustries();

            return PartialView("_BusinessProcessor", pro);
        }
        
        [HttpPost]
        public ActionResult UpdateBusinessInfo(MPMerchantBusinessModel obj, MPMerchantProcessorInfoModel pro)
        {
            //int CurrentMerchantID = Convert.ToInt32(TempData["CurrentMerchantID"]);
            //TempData.Keep();
            Int64 CurrentMerchantID = base.CurrentMerchantID;
            if (ModelState.IsValid)
            {
                obj.MerchantID = CurrentMerchantID;
                obj.UserId = base.CurrentUserID;
                obj.Business.businessStatus = "";
                var apiMethod = string.Format("merchantprofile/{0}/business/update", CurrentMerchantID);
                var response = BaseApiData.PutAPIData<MPMerchantBusinessModel>(apiMethod, obj);

                //Updattng Processor
                //string URL = string.Format("merchantprofile/{0}/business/processor/Update", CurrentMerchantID);
                //var responseProcessor = BaseApiData.PutAPIData<MPMerchantProcessorInfoModel>(URL, pro);

                if (response.StatusCode == HttpStatusCode.OK)
                    base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPBusinessUpdateSuccess);
                else
                    base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPBusinessUpdateFailure);
            }

            var apiMethod2 = string.Format("merchantprofile/{0}/business", CurrentMerchantID);
            MPMerchantBusinessModel objOutput = BaseApiData.GetAPIResult<MPMerchantBusinessModel>(apiMethod2, () => new MPMerchantBusinessModel());
            objOutput.Business.IndustryTypes = GetIndustries();
            objOutput.Business.EntityTypes = GetEntities();
            objOutput.LegalAddress.Provinces = GetProvinces();
            objOutput.PhysicalAddress.Provinces = GetProvinces();
            return PartialView("_MerchantProfileDetails", objOutput);
        }
        [HttpPost]
        public ActionResult UpdateBusinessProcessInfo(int Terminal, int processorTypeId, string processorNumber, DateTime dateGracePeriod, int industryTypeID, DateTime firstProcessedDate, int processorId)
        {
            string URL = System.Configuration.ConfigurationManager.AppSettings["APIURI"] + string.Format("merchantprofile/{0}/business/Update/processor?Terminal=" + Terminal + "&processorTypeId=" + processorTypeId + "&processorNumber=" + processorNumber + "&dateGracePeriod=" + dateGracePeriod + "&industryTypeID=" + industryTypeID + "&firstProcessedDate=" + firstProcessedDate + "&processorId=" + processorId, CurrentMerchantID);
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(URL);
            objRequest.Method = "Put";
            objRequest.ContentLength = 0;

            var response = objRequest.GetResponse();
            base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPBusinessProcessorUpdateSuccess);

            return Business(Convert.ToInt32(CurrentMerchantID));
        }
#endregion

        #region Landlord
        public ActionResult Landlord(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/landlord", MerchantID);
            MPMerchantLandlordModel obj = BaseApiData.GetAPIResult<MPMerchantLandlordModel>(apiMethod, () => new MPMerchantLandlordModel());
            obj.Questions.Answers = ConvertQuestionsToModel<LLRightWrong>(obj.Questions.Questions);
            if (obj != null && obj.Questions !=null)
            {
                if (obj.Questions.Questions != null)
                {
                    obj.Questions.scriptFile =(string)(from q in obj.Questions.Questions
                                               select q.scriptFile).FirstOrDefault();
                    obj.Questions.ScriptFilePath = Url.Content(GetScriptFilePath());
                }
            }
            obj.PropertyTypes = GetPropertyTypes();
            return PartialView("_Landlord", obj);
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
                        //var sValue = false;
                        //if (q.AnswerDesc.Equals("True") || q.AnswerDesc.Equals("Y"))
                        //{
                        //sValue =true;
                        //}
                        //property.SetValue(vcAnswers, sValue, null);
                    }
                }
            return vcAnswers;
        }

        [HttpPost]
        public ActionResult UpdateLandlord(MPMerchantLandlordModel obj)
        {
            Int64 CurrentMerchantID = base.CurrentMerchantID;
            obj.UserId = base.CurrentUserID;
            if (ModelState.IsValid)
            {
                obj.MerchantID = CurrentMerchantID;
                var apiMethod = string.Format("merchantprofile/{0}/landlord/update", CurrentMerchantID);
                var response = BaseApiData.PutAPIData<MPMerchantLandlordModel>(apiMethod, obj);

                if (response.StatusCode == HttpStatusCode.OK)
                    base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPLandlordUpdateSuccess);
                else
                    base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPLandlordUpdateFailure);
                
            }

            obj.PropertyTypes = GetPropertyTypes();
            return PartialView("_Landlord", obj);
            
        }
        #endregion

        #region Contacts
        public ActionResult Contacts(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/contacts", MerchantID);
            IList<MPMerchantContactsModel> obj = BaseApiData.GetAPIResult<IList<MPMerchantContactsModel>>(apiMethod, () => new List<MPMerchantContactsModel>());
            return PartialView("_Contacts", obj);
        }
        public ActionResult ContactsInformation(int ContactID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/contacts/details?contactId=" + ContactID, base.CurrentMerchantID);
            MPMerchantContactDetailModel obj = BaseApiData.GetAPIResult<MPMerchantContactDetailModel>(apiMethod, () => new MPMerchantContactDetailModel());
            obj.Address.Provinces = GetProvinces();
            return PartialView("_ContactDetails", obj);
        }
        public ActionResult AddContact()
        {
            MPMerchantContactDetailModel obj = new MPMerchantContactDetailModel();
            obj.Address.Provinces = GetProvinces();
            obj.MerchantID = base.CurrentMerchantID;
            obj.UserId = base.CurrentUserID;
            return PartialView("_ContactDetails", obj);
        }
        [HttpPost]
        public ActionResult ProcessContact(string submitButton, MPMerchantContactDetailModel obj, FormCollection objCollection)
        {
                switch (submitButton)
                {
                    case "Add":
                        if (ModelState.IsValid)
                        {
                            AddContactInformation(obj);
                        }
                        break;
                    case "Update":
                        if (ModelState.IsValid)
                        {
                            UpdateContactInformation(obj);
                        }
                        break;
                    case "Delete":
                        DeleteContact(objCollection);
                        ModelState.Clear();
                        break;
                }
            var apiMethod = string.Format("merchantprofile/{0}/contacts", base.CurrentMerchantID);
            IList<MPMerchantContactsModel> objOutput = BaseApiData.GetAPIResult<IList<MPMerchantContactsModel>>(apiMethod, () => new List<MPMerchantContactsModel>());
            return PartialView("_Contacts", objOutput);
        }

        public void DeleteContact(FormCollection obj)
        {
            string[] IDs = obj.GetValues("DeleteIt");
            foreach (string str in IDs)
            {
                var apiMethod = string.Format("merchantprofile/{0}/contact/delete/{1}", base.CurrentMerchantID, str);
                BaseApiData.DeleteAPIData(apiMethod);
            }
        }
        public void UpdateContactInformation(MPMerchantContactDetailModel obj)
        {
            obj.UserId = base.CurrentUserID;
            var apiMethod = string.Format("merchantprofile/{0}/contact/update", base.CurrentMerchantID);
            var response = BaseApiData.PutAPIData<MPMerchantContactDetailModel>(apiMethod, obj);
            //Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContactUpdateSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContactUpdateFailure);
        }
        public void AddContactInformation(MPMerchantContactDetailModel obj)
        {
            obj.MerchantID = base.CurrentMerchantID;
            obj.UserId = base.CurrentUserID;
            var apiMethod = string.Format("merchantprofile/{0}/contact/add", base.CurrentMerchantID);
            HttpResponseMessage response = BaseApiData.PutAPIData<MPMerchantContactDetailModel>(apiMethod, obj);
            //Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContactAddSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContactAddFailure);
        }
        #endregion

        #region Owners
        public ActionResult Owners(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/owners", MerchantID);
            IList<MPMerchantOwnersModel> obj = BaseApiData.GetAPIResult<IList<MPMerchantOwnersModel>>(apiMethod, () => new List<MPMerchantOwnersModel>());
            return PartialView("_Owners", obj);
        }
        public ActionResult OwnersInformation(int OwnerID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/Owners/details?OwnerId=" + OwnerID, base.CurrentMerchantID);
            MPMerchantOwnerDetailModel obj = BaseApiData.GetAPIResult<MPMerchantOwnerDetailModel>(apiMethod, () => new MPMerchantOwnerDetailModel());
            obj.Address.Provinces = GetProvinces();
            return PartialView("_OwnerDetails", obj);
        }
        public ActionResult AddOwner()
        {
            MPMerchantOwnerDetailModel obj = new MPMerchantOwnerDetailModel();
            obj.Address.Provinces = GetProvinces();
            obj.MerchantID = base.CurrentMerchantID;
            obj.UserId = base.CurrentUserID;
            return PartialView("_OwnerDetails", obj);
        }
        [HttpPost]
        public ActionResult ProcessOwner(string submitButton, MPMerchantOwnerDetailModel obj, FormCollection objCollection)
        {
            switch (submitButton)
            {
                case "Add":
                    if (ModelState.IsValid)
                    {
                        AddOwnerInformation(obj);
                    }
                    break;
                case "Update":
                    if (ModelState.IsValid)
                    {
                        UpdateOwnerInformation(obj);
                    }
                    break;
                case "Delete":
                    DeleteOwner(objCollection);
                    ModelState.Clear();
                    break;
            }
            var apiMethod2 = string.Format("merchantprofile/{0}/owners", base.CurrentMerchantID);
            IList<MPMerchantOwnersModel> objOutput = BaseApiData.GetAPIResult<IList<MPMerchantOwnersModel>>(apiMethod2, () => new List<MPMerchantOwnersModel>());
            return PartialView("_Owners", objOutput);
        }

        public void DeleteOwner(FormCollection obj)
        {
            string[] IDs = obj.GetValues("DeleteIt");
            foreach (string str in IDs)
            {
                var apiMethod = string.Format("merchantprofile/{0}/owner/delete/{1}", base.CurrentMerchantID, str);
                BaseApiData.DeleteAPIData(apiMethod);
            }
        }
        public void UpdateOwnerInformation(MPMerchantOwnerDetailModel obj)
        {
            obj.UserId = base.CurrentUserID;
            var apiMethod = string.Format("merchantprofile/{0}/Owner/update", base.CurrentMerchantID);
            var response = BaseApiData.PutAPIData<MPMerchantOwnerDetailModel>(apiMethod, obj);
            //Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPOwnerUpdateSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPOwnerUpdateFailure);
        }
        public void AddOwnerInformation(MPMerchantOwnerDetailModel obj)
        {
            obj.MerchantID = base.CurrentMerchantID;
            obj.UserId = base.CurrentUserID;
            var apiMethod = string.Format("merchantprofile/{0}/Owner/add", base.CurrentMerchantID);
            HttpResponseMessage response = BaseApiData.PutAPIData<MPMerchantOwnerDetailModel>(apiMethod, obj);
            //Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPOwnerAddSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPOwnerAddFailure);
        }
        #endregion

        #region Profiles
        public ActionResult Profiles(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/profiles?ProcessorId=0&ProcessorNumber=All", MerchantID);
            MPMerchantProfileDetailModel obj = BaseApiData.GetAPIResult<MPMerchantProfileDetailModel>(apiMethod, () => new MPMerchantProfileDetailModel());
            //IEnumerable<SelectListItem> Processors = GetProcessors();
            obj.Processors = GetProcessors().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable());
            return PartialView("_Profiles", obj);
        }
        public ActionResult ProfilesInformation(int ProfileId)
        {
            var apiMethod = string.Format("merchantprofile/{0}/profiles/details?profileid=" + ProfileId, base.CurrentMerchantID);
            MPMerchantProfileDetailModel obj = BaseApiData.GetAPIResult<MPMerchantProfileDetailModel>(apiMethod, () => new MPMerchantProfileDetailModel());
            //IEnumerable<SelectListItem> Processors = GetProcessors();
            obj.ProfileDetail[0].Processors = GetProcessors().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable());
            return PartialView("_ProfileDetails", obj);
        }
        [HttpPost]
        public ActionResult ProcessProfile(string submitButton, MPMerchantProfileDetailModel obj)
        {
            MPMerchantProfileDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Filter":
                        objOutput = FilterProfileInformation(obj);
                        break;
                    case "Update":
                        objOutput = UpdateProfileInformation(obj);
                        break;
                }
            }
            return PartialView("_Profiles", objOutput);
        }

        public MPMerchantProfileDetailModel FilterProfileInformation(MPMerchantProfileDetailModel obj)
        {
            var apiMethod = string.Format("merchantprofile/{0}/profiles?ProcessorId=" + obj.ProcessorId + "&ProcessorNumber=" + (string.IsNullOrEmpty(obj.ProcessorNumber) ? "All" : obj.ProcessorNumber), base.CurrentMerchantID);
            MPMerchantProfileDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantProfileDetailModel>(apiMethod, () => new MPMerchantProfileDetailModel());
            IEnumerable<SelectListItem> Processors = GetProcessors();
            objOutput.Processors = Processors.Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable());
            return objOutput;
        }

        public MPMerchantProfileDetailModel UpdateProfileInformation(MPMerchantProfileDetailModel obj)
        {
            //Update Profile Info
            var apiMethod = string.Format("merchantprofile/{0}/profile/update", base.CurrentMerchantID);
            var response = BaseApiData.PutAPIData<MPMerchantProfileDetailModel>(apiMethod, obj);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPProfileUpdateSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPProfileUpdateFailure);

            var apiMethodOutput = string.Format("merchantprofile/{0}/profiles?ProcessorId=0&ProcessorNumber=All", base.CurrentMerchantID);
            MPMerchantProfileDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantProfileDetailModel>(apiMethodOutput, () => new MPMerchantProfileDetailModel());
            IEnumerable<SelectListItem> Processors = GetProcessors();
            objOutput.Processors = Processors.Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable());
            return objOutput;
        }
        #endregion

        #region Documents
        public ActionResult Documents(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/Documents?DocumentTypeId=0", MerchantID);
            MPMerchantDocumentsDetailModel obj = BaseApiData.GetAPIResult<MPMerchantDocumentsDetailModel>(apiMethod, () => new MPMerchantDocumentsDetailModel());
            obj.DocumentTypes = GetDocumentTypes().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return PartialView("_Documents", obj);
        }
        [HttpPost]
        public ActionResult ProcessDocuments(string submitButton, MPMerchantDocumentsDetailModel obj,FormCollection objCollection)
        {
            MPMerchantDocumentsDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Search":
                        objOutput = FilterDocumentsInformation(obj);
                        break;
                    case "Delete":
                        objOutput = DeleteDocument(objCollection);
                        break;
                }
            }
            return PartialView("_Documents", objOutput);
        }

        public MPMerchantDocumentsDetailModel FilterDocumentsInformation(MPMerchantDocumentsDetailModel obj)
        {
            var apiMethod = string.Format("merchantprofile/{0}/Documents?DocumentTypeId="+obj.DocumentTypeId, base.CurrentMerchantID);
            MPMerchantDocumentsDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantDocumentsDetailModel>(apiMethod, () => new MPMerchantDocumentsDetailModel());
            objOutput.DocumentTypes = GetDocumentTypes().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return objOutput;
        }

        public MPMerchantDocumentsDetailModel DeleteDocument(FormCollection obj)
        {
            string[] IDs = obj.GetValues("DeleteIt");
            foreach (string str in IDs)
            {
                var apiMethod = string.Format("merchantprofile/{0}/Documents/delete/{1}", base.CurrentMerchantID, str);
                BaseApiData.DeleteAPIData(apiMethod);
            }

            var apiMethod2 = string.Format("merchantprofile/{0}/Documents?DocumentTypeId=0", base.CurrentMerchantID);
            MPMerchantDocumentsDetailModel obOutput = BaseApiData.GetAPIResult<MPMerchantDocumentsDetailModel>(apiMethod2, () => new MPMerchantDocumentsDetailModel());
            obOutput.DocumentTypes = GetDocumentTypes().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return obOutput;
        }

        public ActionResult UploadDocument()
        {
            var model = new MPScanDocumentModel();
            model.DocumentTypes = GetDocumentTypes();
            return PartialView("_DocumentUpload",model);
        }
        public FileResult DocumentDownload(string fileName)
        {
            var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);            
            return File(path, MimeMapping.GetMimeMapping(fileName));            
        }

        [HttpPost]
        public ActionResult SaveUploadedDocument(MPScanDocumentModel mod, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = "doc_" + mod.DocumentID + mod.DocumentTypeID + "_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                    file.SaveAs(path);

                    var docModel = new MPDocumentModel()
                    {
                        DocumentId = mod.DocumentID,
                        ContractId = ContractID,
                        MerchantId = CurrentMerchantID,
                        DocumentTypeId = mod.DocumentTypeID,
                        FileName = fileName,
                        FileDetails = file.ContentType,
                        UploadUserId = CurrentUserID,
                    };

                    var response = BaseApiData.PutAPIData<MPDocumentModel>("documents/UpdateDocuments", docModel);

                    if (response.StatusCode == HttpStatusCode.OK)
                        base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPDocumentUploadSuccess);
                    else
                        base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPDocumentUploadFailure);
                }
            }
            var apiMethod = string.Format("merchantprofile/{0}/Documents?DocumentTypeId=0", CurrentMerchantID);
            MPMerchantDocumentsDetailModel obj = BaseApiData.GetAPIResult<MPMerchantDocumentsDetailModel>(apiMethod, () => new MPMerchantDocumentsDetailModel());
            obj.DocumentTypes = GetDocumentTypes().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return PartialView("_Documents", obj);
        }

        public ActionResult GetImage(int docType)
        {
            string apiQuery = string.Format("documents/RetriveDocument?merchantId={0}&contractId={1}&documentTypeId={2}", CurrentMerchantID, ContractID, docType);
            var doc = BaseApiData.GetAPIResult<IList<MPDocumentModel>>(apiQuery, () => new List<MPDocumentModel>());

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
        #endregion

        #region Activities
        public ActionResult Activities(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/Activitiesmonthly?ProcessorTypeId=0&ProcessorNumber=All&ActivityStartDate=1900/01/01&ActivityEndDate=2100/01/01", MerchantID);
            MPMerchantActivityDetailModel obj = BaseApiData.GetAPIResult<MPMerchantActivityDetailModel>(apiMethod, () => new MPMerchantActivityDetailModel());
            obj.Processors = GetProcessors().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return PartialView("_Activities",obj);
        }
        [HttpPost]
        public ActionResult ProcessActivities(string submitButton, MPMerchantActivityDetailModel obj)
        {
            MPMerchantActivityDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Search":
                        objOutput = FilterActivitiesInformation(obj);
                        break;
                }
            }
            else
                objOutput = obj;

            objOutput.Processors = GetProcessors().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable());
            return PartialView("_Activities", objOutput);
        }

        public MPMerchantActivityDetailModel FilterActivitiesInformation(MPMerchantActivityDetailModel obj)
        {
            DateTime dt1, dt2;
            string[] Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };
            var apiMethod = string.Format("merchantprofile/{0}/Activitiesmonthly?ProcessorTypeId=" + obj.ProcessorTypeId + "&ProcessorNumber=" + (string.IsNullOrEmpty(obj.ProcessorNumber) ? "All" : obj.ProcessorNumber) + "&ActivityStartDate=" + (DateTime.TryParseExact(obj.ActivityFrom.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) ? DateTime.Parse("1900/01/01") : obj.ActivityFrom) + "&ActivityEndDate=" + (DateTime.TryParseExact(obj.ActivityTo.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2) ? DateTime.Parse("1900/01/01") : obj.ActivityTo), base.CurrentMerchantID);
            MPMerchantActivityDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantActivityDetailModel>(apiMethod, () => new MPMerchantActivityDetailModel());
            objOutput.Processors = GetProcessors().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return objOutput;
        }
        public ActionResult ActivitiesDetail(int ProcessorTypeId, string Month,string Year)
        {
            var apiMethod = string.Format("merchantprofile/{0}/Activities?ProcessorTypeId=" + ProcessorTypeId + "&MonthName=" + Month+"/"+Year + "", CurrentMerchantID);
            MPMerchantActivityDetailModel obj = BaseApiData.GetAPIResult<MPMerchantActivityDetailModel>(apiMethod, () => new MPMerchantActivityDetailModel());
            return PartialView("_ActivitiesDetail", obj.ActivityDetail);
        }
        #endregion

        #region Statements
        public ActionResult Statements(int MerchantID)
        {            
            return PartialView("_Statements");
        }
        [HttpPost]
        public ActionResult ProcessStatements(string submitButton, MPMerchantStatementsDetailModel obj)
        {
            string NFCStatementFileName = ConfigurationManager.AppSettings["NCFStatementFileName"];
            string Path = Server.MapPath("~") + @"Templates";
            string ReturnValue = string.Empty;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Generate":
                        ReturnValue = SearchStatements(obj, Path, NFCStatementFileName);
                        break;
                    case "Email":
                        EmailStatements(obj, Path, NFCStatementFileName);
                        break;
                }
            }

            if (string.IsNullOrEmpty(ReturnValue))
            {
                //return PartialView("_Statements");
                return Json(new { statementFilePath = @"Emailed" });
            }
            else
                return Json(new { statementFilePath = @"..\Templates\" + ReturnValue });
        }
        private string SearchStatements(MPMerchantStatementsDetailModel obj, string Path, string NFCStatementFileName)
        {
            MPMerchantStatementsDetailModel objOutput = FilterStatements(obj);
            string DestinationFileName = GenerateStatements(objOutput, Path, NFCStatementFileName);
            return DestinationFileName;            
        }
        private void EmailStatements(MPMerchantStatementsDetailModel obj, string Path, string NFCStatementFileName)
        {
            MPMerchantStatementsDetailModel objOutput = FilterStatements(obj);

            string DestinationFileName = GenerateStatements(objOutput, Path, NFCStatementFileName);
            new Emailer().Send(objOutput.AddressDetail.Email, "Statement", "Bi-Weekly Statements Attached", Path + "/" + DestinationFileName);

            base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPStatementEmailSuccess);
        }
        private MPMerchantStatementsDetailModel FilterStatements(MPMerchantStatementsDetailModel obj)
        {
            MPMerchantStatementsDetailModel objOutput= new MPMerchantStatementsDetailModel();
            DateTime dt1, dt2;
            string[] Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };
            if (ModelState.IsValid)
            {
                var apiMethod = string.Format("merchantprofile/{0}/statements?StatementsFrom=" +
                    (DateTime.TryParseExact(obj.StatementsFrom.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) ? DateTime.Parse("1900/01/01") : obj.StatementsFrom) + "&StatementsTo=" +
                    (DateTime.TryParseExact(obj.StatementsTo.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2) ? DateTime.Parse("1900/01/01") : obj.StatementsTo), base.CurrentMerchantID);
                objOutput = BaseApiData.GetAPIResult<MPMerchantStatementsDetailModel>(apiMethod, () => new MPMerchantStatementsDetailModel());
            }
            else
                objOutput = obj;

            return objOutput;
        }
        private string GenerateStatements(MPMerchantStatementsDetailModel objOutput, string Path, string NFCStatementFileName)
        {
            ExcelHelper EH = new ExcelHelper();
            string DestinationFileName = "NFC" + Guid.NewGuid() + ".xlsx";
            //string Path = @"D:\Development\DevelopmentWork\Projects2013\Pecuniaus\trunk\Pecuniaus\Pecuniaus.Web\Templates";
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, true, "E10", objOutput.TradeID.ToString());
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E11", objOutput.FirmName);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E12", objOutput.RNC);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E13", objOutput.TradeName);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E14", objOutput.AddressDetail.Address);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E15", objOutput.AddressDetail.City);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E16", Convert.ToString(objOutput.AddressDetail.Phone1));
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "E17", objOutput.AddressDetail.Email);

            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G8", objOutput.NFC);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G9", objOutput.StatementPeriod);
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G11", Convert.ToString(objOutput.TotalCashRecieved));
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G12", Convert.ToString(objOutput.TotalAdjustmentApplied));

            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.CurrencyNegativePattern = 1;
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G13", Convert.ToString(string.Format(culture,"{0:C}",
                //objOutput.TotalCashApplied
                Convert.ToDouble(objOutput.TotalCashRecieved == "" ? "0" : objOutput.TotalCashRecieved.Replace("$","").Replace(",","")) -
                Convert.ToDouble(objOutput.TotalAdjustmentApplied == "" ? "0" : objOutput.TotalAdjustmentApplied.Replace("$", "").Replace(",", ""))
                )));
            
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G14", Convert.ToString(objOutput.TotalPriceApplied));
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G15", Convert.ToString(objOutput.OutstandingBalance));
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G16", Convert.ToString(objOutput.RightstoRecieve));
            EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, "G17", Convert.ToString(objOutput.AdminCharges));

            int RowIndex = 22;

            foreach (MPMerchantStatementModel stmt in objOutput.StatementsDetail)
            {
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 4, stmt.TransactionDate.ToShortDateString());
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 5, stmt.ProcessorName);
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 6, stmt.TotalTransaction.ToString());
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 7, stmt.RetentionPercentage.ToString());
                EH.UpdateExcelTemplate(Path, NFCStatementFileName, DestinationFileName, false, RowIndex, 8, Convert.ToString(stmt.PaymentsReceived));

                RowIndex++;
            }
            return DestinationFileName;
        }
        #endregion

        #region History
        public ActionResult History(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/history?HistoryStartDate=1900/01/01&HistoryEndDate=2100/01/01", MerchantID);
            MPMerchantHistoryDetailModel obj = BaseApiData.GetAPIResult<MPMerchantHistoryDetailModel>(apiMethod, () => new MPMerchantHistoryDetailModel());
            return PartialView("_History", obj);
        }
        [HttpPost]
        public ActionResult ProcessHistory(string submitButton, MPMerchantHistoryDetailModel obj)
        {
            MPMerchantHistoryDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Search":
                        objOutput = FilterHistoryInformation(obj);
                        break;
                }
            }
            return PartialView("_History", objOutput);
        }

        public MPMerchantHistoryDetailModel FilterHistoryInformation(MPMerchantHistoryDetailModel obj)
        {
            DateTime dt1, dt2;
            string[] Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };
            //DateTime.TryParseExact("", new[] { "dd/MM/yyyy", "dd/M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
            var apiMethod = string.Format("merchantprofile/{0}/history?HistoryStartDate=" +
                (DateTime.TryParseExact(obj.HistoryStartDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) ? DateTime.Parse("1900/01/01") : obj.HistoryStartDate) + "&HistoryEndDate=" +
                (DateTime.TryParseExact(obj.HistoryEndDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2) ? DateTime.Parse("1900/01/01") : obj.HistoryEndDate), base.CurrentMerchantID);
            MPMerchantHistoryDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantHistoryDetailModel>(apiMethod, () => new MPMerchantHistoryDetailModel());
            return objOutput;
        }
        #endregion

        #region Risk Evalution
        public ActionResult RiskEvolution(Int64 MerchantID)
        {
            //SessionHelper.SetCurrentMerchant(MerchantID);
            var apiMethod = string.Format("merchantprofile/{0}/risk?StartDate=1900/01/01&EndDate=2100/01/01", MerchantID);
            MPMerchantRiskEvaluationDetailModel obj = BaseApiData.GetAPIResult<MPMerchantRiskEvaluationDetailModel>(apiMethod, () => new MPMerchantRiskEvaluationDetailModel());
            return PartialView("_RiskEvolution", obj);
        }
        [HttpPost]
        public ActionResult ProcessRisk(string submitButton, MPMerchantRiskEvaluationDetailModel obj)
        {
            MPMerchantRiskEvaluationDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Search":
                        objOutput = FilterRiskInformation(obj);
                        break;
                }
            }
            return PartialView("_RiskEvolution", objOutput);
        }
        public MPMerchantRiskEvaluationDetailModel FilterRiskInformation(MPMerchantRiskEvaluationDetailModel obj)
        {
            DateTime dt1, dt2;
            string[] Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };
            //DateTime.TryParseExact("", new[] { "dd/MM/yyyy", "dd/M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
            var apiMethod = string.Format("merchantprofile/{0}/risk?StartDate=" +
                (DateTime.TryParseExact(obj.StartDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) ? DateTime.Parse("1900/01/01") : obj.StartDate) + "&EndDate=" +
                (DateTime.TryParseExact(obj.EndDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2) ? DateTime.Parse("1900/01/01") : obj.EndDate), base.CurrentMerchantID);
            MPMerchantRiskEvaluationDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantRiskEvaluationDetailModel>(apiMethod, () => new MPMerchantRiskEvaluationDetailModel());
            return objOutput;
        }
        //[AllowAnonymous]
        public ActionResult LoadRiskEvaluationReport(long creditReportId, long contractId)
        {
            //return new Rotativa.ActionAsPdf("CreatePDF", routeValues: new { creditReportId = creditReportId, contractId = contractId });
            //return new Rotativa.PartialViewAsPdf("CreatePDF", routeValues: new { creditReportId = creditReportId, contractId = contractId });
            var apiMethodProcessor = string.Format("creditreport/" + base.CurrentMerchantID + "/" + contractId);
            var modelProcessor = BaseApiData.GetAPIResult<List<MPCreditReportModel>>(apiMethodProcessor, () => new List<MPCreditReportModel>());
            List<MPCreditReportModel> listcreditreport = modelProcessor;
            List<MPCreditReportModel> volumelistFiltered = listcreditreport.Where(c => c.creditReportId == creditReportId).ToList();
            byte[] rawdata = volumelistFiltered[0].rawData;
            CreditReportBase objbase = Data(rawdata);
            return PartialView("_CreatePDF", objbase);
            //return new Rotativa.PartialViewAsPdf("_CreatePDF", objbase) { FileName = "OfferPdf.pdf" };
        }
        //[AllowAnonymous]
        //public ActionResult CreatePDF(long creditReportId, long contractId)
        //{
        //    var apiMethodProcessor = string.Format("creditreport/" + base.CurrentMerchantID + "/" + contractId);
        //    var modelProcessor = BaseApiData.GetAPIResult<List<MPCreditReportModel>>(apiMethodProcessor, () => new List<MPCreditReportModel>());
        //    List<MPCreditReportModel> listcreditreport = modelProcessor;
        //    List<MPCreditReportModel> volumelistFiltered = listcreditreport.Where(c => c.creditReportId == creditReportId).ToList();
        //    byte[] rawdata = volumelistFiltered[0].rawData;
        //    CreditReportBase objbase = Data(rawdata);
        //    return PartialView("_CreatePDF", objbase);
        //}
        public static CreditReportBase Data(byte[] rawdata)
        {
            CreditReportBase cbase = new CreditReportBase();
            try
            {
                string jsonData = Encoding.ASCII.GetString(rawdata);
                cbase = CreateJson();
                return cbase;
            }
            catch (Exception ex)
            {
                cbase = CreateJson();
                return cbase;
            }

        }
        public static CreditReportBase CreateJson()
        {
            #region JSON Data
            string jsonData = @"{
	c: {
		'DCR': {
			'Seguridad': {
				'@Descripcion': 'Datos Relacionados a la consulta del usuario',
				'IdUsuario': 'PAOLA.CANARIO.AMD',
				'NombreUsuario': 'PAOLA VANESSA CANARIO POLANCO',
				'Afiliado': 'AVANZAME DOMINICANA, LTD',
				'Oficina': 'AVANZAME DOMINICANA, LTD                          ',
				'Departamento': 'AVANZAME DOMINICANA, LTD',
				'FechaHora': '24-10-2014 10:26:16',
				'DireccionIp': '14.98.75.175',
				'UsuarioSistema': null,
				'ConsultaId': '118955257',
				'InformacionConsultada': ' CÉDULA NUEVA: 001-1356924-8'
			},
			'ErrorHandling': {
				'Id': '0',
				'Description': 'Satisfactorio'
			},
			'Foto': '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',
			'Individuo': {
				'Nombres': 'ONAILIN ',
				'Apellidos': ' RAMIREZ NOVAS',
				'Apellidos1': 'RAMIREZ',
				'Apellidos2': 'NOVAS',
				'CedulaNueva': '00113569248',
				'CedulaVieja': '000000000',
				'Pasaporte': null,
				'Edad': '35.11',
				'FechaNacimiento': '04-11-1978',
				'LugarNacimiento': 'GALVAN, R.D.',
				'EstadoCivil': null,
				'Ocupacion': 'ESTILISTA',
				'Categoria': null,
				'Conyugue': null,
				'CedulaConyugue': null,
				'Sexo': 'FEMENINO',
				'Nacionalidad': 'DOMINICANA',
				'Madre': 'NILDA NOVAS',
				'Padre': 'LEONCIO RAMIREZ'
			},
			'AnalisisCrediticio': {
					'CantidadTotalCuentas': {
						'Moneda': {
							'@Id': 'RD',
							'Cuentas': '14',
							'Cerradas': '7',
							'Abiertas': '7',
							'Normal': '7',
							'EnAtraso': null,
							'AcuerdoPago': null,
							'EnLegal': null,
							'Castigado': null
						},
						'Totales': {
							'Cuentas': '14',
							'Cerradas': '7',
							'Abiertas': '7',
							'Normal': '7',
							'EnAtraso': null,
							'AcuerdoPago': null,
							'EnLegal': null,
							'Castigado': null
						}
					},
				'AnalisisCreditos': {
					'Moneda': {
						'@Id': 'RD',
						'MasReciente': {
							'Fecha': '09-2014',
							'Anos': '0.0',
							'Monto': '$13,925'
						},
						'MasAntiguo': {
							'Fecha': '03-2007',
							'Anos': '7.7',
							'Monto': '$550,000'
						},
						'MenorAprobado': {
							'Fecha': null,
							'Anos': null,
							'Monto': null
						},
						'MayorAdeudado': {
							'Fecha': '09-2013',
							'Anos': '1.1',
							'Monto': '$1,260,000'
						}
					}
				},
				'AnalisisAtrasos': {
					'Moneda': {
						'@Id': 'RD',
						'MasReciente': {
							'Fecha': '06-2014',
							'Anos': '0.4',
							'DiasAtraso': '30',
							'Monto': '$3,548'
						},
						'MasAntiguo': {
							'Fecha': '10-2010',
							'Anos': '4.0',
							'DiasAtraso': '30',
							'Monto': '$18,317'
						},
						'MayorMontoAtraso': {
							'Fecha': '07-2012',
							'Anos': '2.3',
							'DiasAtraso': '30',
							'Monto': '$20,104'
						}
					}
				},
				'PeorAtraso': {
					'Moneda': {
						'@Id': 'RD',
						'Fecha': '09-2011',
						'Anos': '3.1',
						'DiasAtraso': 'LEGAL',
						'CreditoAprobado': '$5,000',
						'MontoAtraso': '$3,760',
						'Producto': 'Producto de Telecomunicaciones'
					}
				}
			},
			'DesgloseCreditos': {
				'Producto': [{
					'@Id': 'Tarjeta de Crédito',
					'@Cod': 'TCR',
					'Cuenta': [{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '1'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'ASOCIACION POPULAR DE AHORROS Y PRESTAMOS [APAP]',
						'FechaApertura': '07-2013',
						'FechaReportado': '09-2014',
						'MesConsolidado': '09-2014',
						'FechaUltimaTransaccion': '08-2014',
						'CreditoAprobado': '50000',
						'TotalAdeudado': '52156',
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'Activa',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201307',
								'#text': '0'
							},
							'M11': {
								'@F': '201308',
								'#text': '0'
							},
							'M12': {
								'@F': '201309',
								'#text': '0'
							},
							'M13': {
								'@F': '201310',
								'#text': '0'
							},
							'M14': {
								'@F': '201311',
								'#text': '0'
							},
							'M15': {
								'@F': '201312',
								'#text': '0'
							},
							'M16': {
								'@F': '201401',
								'#text': '0'
							},
							'M17': {
								'@F': '201402',
								'#text': '0'
							},
							'M18': {
								'@F': '201403',
								'#text': '0'
							},
							'M19': {
								'@F': '201404',
								'#text': '0'
							},
							'M20': {
								'@F': '201405',
								'#text': '0'
							},
							'M21': {
								'@F': '201406',
								'#text': '0'
							},
							'M22': {
								'@F': '201407',
								'#text': '0'
							},
							'M23': {
								'@F': '201408',
								'#text': '0'
							},
							'M24': {
								'@F': '201409',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '0'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'BANCO POPULAR DOMINICANO [BPD]',
						'FechaApertura': '07-2007',
						'FechaReportado': '11-2013',
						'MesConsolidado': '10-2013',
						'FechaUltimaTransaccion': '08-2012',
						'CreditoAprobado': '4000',
						'TotalAdeudado': null,
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': '1',
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'CANCELADA POR GERENCIA',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201111',
								'#text': '0'
							},
							'M2': {
								'@F': '201112',
								'#text': '0'
							},
							'M3': {
								'@F': '201201',
								'#text': '0'
							},
							'M4': {
								'@F': '201202',
								'@PDV': 'L',
								'#text': '1'
							},
							'M5': {
								'@F': '201203',
								'@PDV': '1',
								'#text': '1'
							},
							'M6': {
								'@F': '201204',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201205',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201206',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201207',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201208',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201209',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M13': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M14': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M15': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M16': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M17': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M18': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M19': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M20': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M21': {
								'@F': '201307',
								'@PDV': 'C'
							},
							'M22': {
								'@F': '201308',
								'@PDV': 'C'
							},
							'M23': {
								'@F': '201309',
								'@PDV': 'C'
							},
							'M24': {
								'@F': '201310',
								'#text': '0'
							}
						}
					}]
				},
				{
					'@Id': 'Préstamo',
					'@Cod': 'PRE',
					'Cuenta': [{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '1'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'FINANCIERA LEASING CONFISA, S.A.',
						'FechaApertura': '08-2014',
						'FechaReportado': '10-2014',
						'MesConsolidado': '10-2014',
						'FechaUltimaTransaccion': '09-2014',
						'CreditoAprobado': '591627',
						'TotalAdeudado': '581169',
						'Cuota': '13446',
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'NORMAL',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201307',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201308',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201309',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201310',
								'@PDV': 'C'
							},
							'M13': {
								'@F': '201311',
								'@PDV': 'C'
							},
							'M14': {
								'@F': '201312',
								'@PDV': 'C'
							},
							'M15': {
								'@F': '201401',
								'@PDV': 'C'
							},
							'M16': {
								'@F': '201402',
								'@PDV': 'C'
							},
							'M17': {
								'@F': '201403',
								'@PDV': 'C'
							},
							'M18': {
								'@F': '201404',
								'@PDV': 'C'
							},
							'M19': {
								'@F': '201405',
								'@PDV': 'C'
							},
							'M20': {
								'@F': '201406',
								'@PDV': 'C'
							},
							'M21': {
								'@F': '201407',
								'@PDV': 'C'
							},
							'M22': {
								'@F': '201408',
								'#text': '0'
							},
							'M23': {
								'@F': '201409',
								'#text': '0'
							},
							'M24': {
								'@F': '201410',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '1'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'BANCO POPULAR DOMINICANO [BPD]',
						'FechaApertura': '09-2014',
						'FechaReportado': '10-2014',
						'MesConsolidado': '09-2014',
						'FechaUltimaTransaccion': null,
						'CreditoAprobado': '20000',
						'TotalAdeudado': '20083',
						'Cuota': '1392',
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'NORMAL',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201307',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201308',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201309',
								'@PDV': 'C'
							},
							'M13': {
								'@F': '201310',
								'@PDV': 'C'
							},
							'M14': {
								'@F': '201311',
								'@PDV': 'C'
							},
							'M15': {
								'@F': '201312',
								'@PDV': 'C'
							},
							'M16': {
								'@F': '201401',
								'@PDV': 'C'
							},
							'M17': {
								'@F': '201402',
								'@PDV': 'C'
							},
							'M18': {
								'@F': '201403',
								'@PDV': 'C'
							},
							'M19': {
								'@F': '201404',
								'@PDV': 'C'
							},
							'M20': {
								'@F': '201405',
								'@PDV': 'C'
							},
							'M21': {
								'@F': '201406',
								'@PDV': 'C'
							},
							'M22': {
								'@F': '201407',
								'@PDV': 'C'
							},
							'M23': {
								'@F': '201408',
								'@PDV': 'C'
							},
							'M24': {
								'@F': '201409',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '1'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'ASOCIACION POPULAR DE AHORROS Y PRESTAMOS [APAP]',
						'FechaApertura': '05-2014',
						'FechaReportado': '09-2014',
						'MesConsolidado': '09-2014',
						'FechaUltimaTransaccion': '08-2014',
						'CreditoAprobado': '160000',
						'TotalAdeudado': '156735',
						'Cuota': '4146',
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'Vigente',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201307',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201308',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201309',
								'@PDV': 'C'
							},
							'M13': {
								'@F': '201310',
								'@PDV': 'C'
							},
							'M14': {
								'@F': '201311',
								'@PDV': 'C'
							},
							'M15': {
								'@F': '201312',
								'@PDV': 'C'
							},
							'M16': {
								'@F': '201401',
								'@PDV': 'C'
							},
							'M17': {
								'@F': '201402',
								'@PDV': 'C'
							},
							'M18': {
								'@F': '201403',
								'@PDV': 'C'
							},
							'M19': {
								'@F': '201404',
								'@PDV': 'C'
							},
							'M20': {
								'@F': '201405',
								'@PDV': 'C'
							},
							'M21': {
								'@F': '201406',
								'#text': '0'
							},
							'M22': {
								'@F': '201407',
								'#text': '0'
							},
							'M23': {
								'@F': '201408',
								'#text': '0'
							},
							'M24': {
								'@F': '201409',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '1'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'ASOCIACION POPULAR DE AHORROS Y PRESTAMOS [APAP]',
						'FechaApertura': '08-2013',
						'FechaReportado': '09-2014',
						'MesConsolidado': '09-2014',
						'FechaUltimaTransaccion': '08-2014',
						'CreditoAprobado': '1260000',
						'TotalAdeudado': '1258532',
						'Cuota': '12453',
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'Vigente',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201307',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201308',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201309',
								'#text': '0'
							},
							'M13': {
								'@F': '201310',
								'#text': '0'
							},
							'M14': {
								'@F': '201311',
								'#text': '0'
							},
							'M15': {
								'@F': '201312',
								'#text': '0'
							},
							'M16': {
								'@F': '201401',
								'#text': '0'
							},
							'M17': {
								'@F': '201402',
								'#text': '0'
							},
							'M18': {
								'@F': '201403',
								'#text': '0'
							},
							'M19': {
								'@F': '201404',
								'#text': '0'
							},
							'M20': {
								'@F': '201405',
								'#text': '0'
							},
							'M21': {
								'@F': '201406',
								'#text': '0'
							},
							'M22': {
								'@F': '201407',
								'#text': '0'
							},
							'M23': {
								'@F': '201408',
								'#text': '0'
							},
							'M24': {
								'@F': '201409',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '1'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'ASOCIACION POPULAR DE AHORROS Y PRESTAMOS [APAP]',
						'FechaApertura': '08-2013',
						'FechaReportado': '09-2014',
						'MesConsolidado': '09-2014',
						'FechaUltimaTransaccion': '08-2014',
						'CreditoAprobado': '465000',
						'TotalAdeudado': '456222',
						'Cuota': '5201',
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'Vigente',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201307',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201308',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201309',
								'#text': '0'
							},
							'M13': {
								'@F': '201310',
								'#text': '0'
							},
							'M14': {
								'@F': '201311',
								'#text': '0'
							},
							'M15': {
								'@F': '201312',
								'#text': '0'
							},
							'M16': {
								'@F': '201401',
								'#text': '0'
							},
							'M17': {
								'@F': '201402',
								'#text': '0'
							},
							'M18': {
								'@F': '201403',
								'#text': '0'
							},
							'M19': {
								'@F': '201404',
								'#text': '0'
							},
							'M20': {
								'@F': '201405',
								'#text': '0'
							},
							'M21': {
								'@F': '201406',
								'#text': '0'
							},
							'M22': {
								'@F': '201407',
								'#text': '0'
							},
							'M23': {
								'@F': '201408',
								'#text': '0'
							},
							'M24': {
								'@F': '201409',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '0'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'BANCO DE AHORRO Y CREDITO BDA, S. A.',
						'FechaApertura': '12-2012',
						'FechaReportado': '06-2014',
						'MesConsolidado': '05-2014',
						'FechaUltimaTransaccion': '05-2014',
						'CreditoAprobado': '110000',
						'TotalAdeudado': null,
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'SALDADO',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201206',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201207',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201208',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201209',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201212',
								'#text': '0'
							},
							'M8': {
								'@F': '201301',
								'#text': '0'
							},
							'M9': {
								'@F': '201302',
								'#text': '0'
							},
							'M10': {
								'@F': '201303',
								'#text': '0'
							},
							'M11': {
								'@F': '201304',
								'#text': '0'
							},
							'M12': {
								'@F': '201305',
								'#text': '0'
							},
							'M13': {
								'@F': '201306',
								'#text': '0'
							},
							'M14': {
								'@F': '201307',
								'#text': '0'
							},
							'M15': {
								'@F': '201308',
								'#text': '0'
							},
							'M16': {
								'@F': '201309',
								'#text': '0'
							},
							'M17': {
								'@F': '201310',
								'#text': '0'
							},
							'M18': {
								'@F': '201311',
								'#text': '0'
							},
							'M19': {
								'@F': '201312',
								'#text': '0'
							},
							'M20': {
								'@F': '201401',
								'#text': '0'
							},
							'M21': {
								'@F': '201402',
								'#text': '0'
							},
							'M22': {
								'@F': '201403',
								'#text': '0'
							},
							'M23': {
								'@F': '201404',
								'#text': '0'
							},
							'M24': {
								'@F': '201405',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '0'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'KUARTOS, CREDITOS E INVERSIONES, S. A.',
						'FechaApertura': '01-2012',
						'FechaReportado': '09-2012',
						'MesConsolidado': '08-2012',
						'FechaUltimaTransaccion': null,
						'CreditoAprobado': '25000',
						'TotalAdeudado': null,
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'CANCELADO',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201009',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201010',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201011',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201012',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201101',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201102',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201103',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201104',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201105',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201106',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201107',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201108',
								'@PDV': 'C'
							},
							'M13': {
								'@F': '201109',
								'@PDV': 'C'
							},
							'M14': {
								'@F': '201110',
								'@PDV': 'C'
							},
							'M15': {
								'@F': '201111',
								'@PDV': 'C'
							},
							'M16': {
								'@F': '201112',
								'@PDV': 'C'
							},
							'M17': {
								'@F': '201201',
								'@PDV': 'C'
							},
							'M18': {
								'@F': '201202',
								'@PDV': 'C'
							},
							'M19': {
								'@F': '201203',
								'#text': '0'
							},
							'M20': {
								'@F': '201204',
								'#text': '0'
							},
							'M21': {
								'@F': '201205',
								'#text': '0'
							},
							'M22': {
								'@F': '201206',
								'@PDV': 'C'
							},
							'M23': {
								'@F': '201207',
								'#text': '0'
							},
							'M24': {
								'@F': '201208',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '0'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'KUARTOS, CREDITOS E INVERSIONES, S. A.',
						'FechaApertura': '05-2013',
						'FechaReportado': '01-2014',
						'MesConsolidado': '01-2014',
						'FechaUltimaTransaccion': null,
						'CreditoAprobado': '10000',
						'TotalAdeudado': null,
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'SALDADO',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201202',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201203',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201204',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201205',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201206',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201207',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201208',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201209',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M13': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M14': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M15': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M16': {
								'@F': '201305',
								'#text': '0'
							},
							'M17': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M18': {
								'@F': '201307',
								'#text': '0'
							},
							'M19': {
								'@F': '201308',
								'#text': '0'
							},
							'M20': {
								'@F': '201309',
								'@PDV': 'L',
								'#text': '1'
							},
							'M21': {
								'@F': '201310',
								'@PDV': 'L',
								'#text': '1'
							},
							'M22': {
								'@F': '201311',
								'@PDV': 'L',
								'#text': '1'
							},
							'M23': {
								'@F': '201312',
								'@PDV': 'L',
								'#text': '1'
							},
							'M24': {
								'@F': '201401',
								'@PDV': 'L',
								'#text': '1'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '0'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'BANCO DE AHORRO Y CREDITO UNION',
						'FechaApertura': '07-2013',
						'FechaReportado': '08-2014',
						'MesConsolidado': '07-2014',
						'FechaUltimaTransaccion': '07-2014',
						'CreditoAprobado': '33890',
						'TotalAdeudado': null,
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'SALDADO',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201208',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201209',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201212',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201302',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201303',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201304',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201305',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201306',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201307',
								'#text': '0'
							},
							'M13': {
								'@F': '201308',
								'#text': '0'
							},
							'M14': {
								'@F': '201309',
								'#text': '0'
							},
							'M15': {
								'@F': '201310',
								'#text': '0'
							},
							'M16': {
								'@F': '201311',
								'#text': '0'
							},
							'M17': {
								'@F': '201312',
								'#text': '0'
							},
							'M18': {
								'@F': '201401',
								'#text': '0'
							},
							'M19': {
								'@F': '201402',
								'#text': '0'
							},
							'M20': {
								'@F': '201403',
								'#text': '0'
							},
							'M21': {
								'@F': '201404',
								'#text': '0'
							},
							'M22': {
								'@F': '201405',
								'#text': '0'
							},
							'M23': {
								'@F': '201406',
								'#text': '0'
							},
							'M24': {
								'@F': '201407',
								'#text': '0'
							}
						}
					},
					{
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '0'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'ASOCIACION LA NACIONAL DE AHORROS Y PRESTAMOS',
						'FechaApertura': '03-2007',
						'FechaReportado': '08-2013',
						'MesConsolidado': '07-2013',
						'FechaUltimaTransaccion': '07-2013',
						'CreditoAprobado': '550000',
						'TotalAdeudado': null,
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': '10',
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'CANCELADA',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201108',
								'@PDV': '1',
								'#text': '1'
							},
							'M2': {
								'@F': '201109',
								'#text': '0'
							},
							'M3': {
								'@F': '201110',
								'@PDV': 'L',
								'#text': '1'
							},
							'M4': {
								'@F': '201111',
								'@PDV': '1',
								'#text': '1'
							},
							'M5': {
								'@F': '201112',
								'@PDV': 'L',
								'#text': '1'
							},
							'M6': {
								'@F': '201201',
								'@PDV': '1',
								'#text': '1'
							},
							'M7': {
								'@F': '201202',
								'#text': '0'
							},
							'M8': {
								'@F': '201203',
								'@PDV': 'L',
								'#text': '1'
							},
							'M9': {
								'@F': '201204',
								'@PDV': 'L',
								'#text': '1'
							},
							'M10': {
								'@F': '201205',
								'@PDV': 'L',
								'#text': '1'
							},
							'M11': {
								'@F': '201206',
								'@PDV': 'L',
								'#text': '1'
							},
							'M12': {
								'@F': '201207',
								'@PDV': '1',
								'#text': '1'
							},
							'M13': {
								'@F': '201208',
								'#text': '0'
							},
							'M14': {
								'@F': '201209',
								'#text': '0'
							},
							'M15': {
								'@F': '201210',
								'#text': '0'
							},
							'M16': {
								'@F': '201211',
								'#text': '0'
							},
							'M17': {
								'@F': '201212',
								'#text': '0'
							},
							'M18': {
								'@F': '201301',
								'#text': '0'
							},
							'M19': {
								'@F': '201302',
								'#text': '0'
							},
							'M20': {
								'@F': '201303',
								'#text': '0'
							},
							'M21': {
								'@F': '201304',
								'#text': '0'
							},
							'M22': {
								'@F': '201305',
								'#text': '0'
							},
							'M23': {
								'@F': '201306',
								'#text': '0'
							},
							'M24': {
								'@F': '201307',
								'#text': '0'
							}
						}
					}]
				},
				{
					'@Id': 'SERVICIOS',
					'@Cod': 'SRV',
					'Cuenta': {
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '1'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'EMPRESA DISTRIBUIDORA DE ELECTRICIDAD DEL ESTE, S.A.',
						'FechaApertura': '09-2014',
						'FechaReportado': '10-2014',
						'MesConsolidado': '09-2014',
						'FechaUltimaTransaccion': null,
						'CreditoAprobado': '13925',
						'TotalAdeudado': '13925',
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': null,
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': null,
						'EstatusUltimo': 'NORMAL',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '201210',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '201211',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201212',
								'#text': '0'
							},
							'M4': {
								'@F': '201301',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201302',
								'#text': '0'
							},
							'M6': {
								'@F': '201303',
								'#text': '0'
							},
							'M7': {
								'@F': '201304',
								'#text': '0'
							},
							'M8': {
								'@F': '201305',
								'#text': '0'
							},
							'M9': {
								'@F': '201306',
								'#text': '0'
							},
							'M10': {
								'@F': '201307',
								'#text': '0'
							},
							'M11': {
								'@F': '201308',
								'#text': '0'
							},
							'M12': {
								'@F': '201309',
								'#text': '0'
							},
							'M13': {
								'@F': '201310',
								'@PDV': 'C'
							},
							'M14': {
								'@F': '201311',
								'#text': '0'
							},
							'M15': {
								'@F': '201312',
								'#text': '0'
							},
							'M16': {
								'@F': '201401',
								'#text': '0'
							},
							'M17': {
								'@F': '201402',
								'#text': '0'
							},
							'M18': {
								'@F': '201403',
								'@PDV': 'C'
							},
							'M19': {
								'@F': '201404',
								'#text': '0'
							},
							'M20': {
								'@F': '201405',
								'#text': '0'
							},
							'M21': {
								'@F': '201406',
								'#text': '0'
							},
							'M22': {
								'@F': '201407',
								'#text': '0'
							},
							'M23': {
								'@F': '201408',
								'#text': '0'
							},
							'M24': {
								'@F': '201409',
								'#text': '0'
							}
						}
					}
				},
				{
					'@Id': 'Producto de Telecomunicaciones',
					'@Cod': 'TEL',
					'Cuenta': {
						'Estatus': {
							'@Des_Estatus': 'Activo',
							'#text': '0'
						},
						'Moneda': {
							'@Des_Moneda': 'R.D. $',
							'#text': 'RD'
						},
						'Afiliado': 'TRICOM',
						'FechaApertura': '01-2011',
						'FechaReportado': '10-2011',
						'MesConsolidado': '10-2011',
						'FechaUltimaTransaccion': '09-2011',
						'CreditoAprobado': '5000',
						'TotalAdeudado': null,
						'Cuota': null,
						'TotalAtraso': null,
						'VecesAtraso30': '4',
						'VecesAtraso60': null,
						'VecesAtraso90': null,
						'VecesAtraso120': '2',
						'EstatusUltimo': 'Registro Modificado en Virtud del Cumplimiento del Artículo 68, de la Ley 172-13.',
						'FDR': '0.0',
						'HistorialPago': {
							'M1': {
								'@F': '200911',
								'@PDV': 'C'
							},
							'M2': {
								'@F': '200912',
								'@PDV': 'C'
							},
							'M3': {
								'@F': '201001',
								'@PDV': 'C'
							},
							'M4': {
								'@F': '201002',
								'@PDV': 'C'
							},
							'M5': {
								'@F': '201003',
								'@PDV': 'C'
							},
							'M6': {
								'@F': '201004',
								'@PDV': 'C'
							},
							'M7': {
								'@F': '201005',
								'@PDV': 'C'
							},
							'M8': {
								'@F': '201006',
								'@PDV': 'C'
							},
							'M9': {
								'@F': '201007',
								'@PDV': 'C'
							},
							'M10': {
								'@F': '201008',
								'@PDV': 'C'
							},
							'M11': {
								'@F': '201009',
								'@PDV': 'C'
							},
							'M12': {
								'@F': '201010',
								'@PDV': '1',
								'#text': '1'
							},
							'M13': {
								'@F': '201011',
								'@PDV': '1',
								'#text': '1'
							},
							'M14': {
								'@F': '201012',
								'@PDV': 'L',
								'#text': '1'
							},
							'M15': {
								'@F': '201101',
								'#text': '0'
							},
							'M16': {
								'@F': '201102',
								'@PDV': 'L',
								'#text': '1'
							},
							'M17': {
								'@F': '201103',
								'@PDV': '1',
								'#text': '1'
							},
							'M18': {
								'@F': '201104',
								'@PDV': 'L',
								'#text': '1'
							},
							'M19': {
								'@F': '201105',
								'@PDV': '1',
								'#text': '1'
							},
							'M20': {
								'@F': '201106',
								'#text': '0'
							},
							'M21': {
								'@F': '201107',
								'@PDV': 'L',
								'#text': '1'
							},
							'M22': {
								'@F': '201108',
								'@PDV': '1',
								'#text': '1'
							},
							'M23': {
								'@F': '201109',
								'@PDV': 'J',
								'#text': '8'
							},
							'M24': {
								'@F': '201110',
								'@PDV': 'J',
								'#text': '8'
							}
						}
					}
				}]
			},
			'PersonasRelacionadas': {
				'Persona': {
					'Nombres': 'ONAYKA',
					'Apellidos': 'RAMIREZ NOVAS',
					'CedulaNueva': '00111583266',
					'CedulaVieja': null,
					'Edad': '37.5'
				}
			},
			'Direcciones': {
				'Dir': [{
					'@Fecha': '16-10-2014',
					'#text': 'C/ PRIMERA, RES. IDALIA  27  CHARLES DE GAULLE'
				},
				{
					'@Fecha': '16-10-2014',
					'#text': '1RA NO 27 RES IDALIA CHARLES DE GAUL '
				},
				{
					'@Fecha': '16-10-2014',
					'#text': 'CALLE 1ERA 27 IDALIA ENTRANDO POR EL RESIDENCIAL DON MIGUEL,(A083105MJ) Idalia CANCINO SANTO DOMINGO'
				},
				{
					'@Fecha': '13-10-2014',
					'#text': 'AV. TIRADENTES NO.5'
				},
				{
					'@Fecha': '03-10-2014',
					'#text': 'SALON MIGUEL DUENA AV  TIRADENTES PLAZA NACO LOCAL 5 DISTRITO NACIONAL D N    '
				},
				{
					'@Fecha': '01-10-2014',
					'#text': 'TERCERA (1) 8011 IDALIA DEL ESTE ZONA URBANA/SANTO DOMINGO ESTE '
				},
				{
					'@Fecha': '01-09-2014',
					'#text': 'Idalia CALLE 1ERA 27      IDALIA SANTO DOMINGO DISTRITO NACIONAL        '
				},
				{
					'@Fecha': '08-01-2013',
					'#text': 'RESIDENCIAL HIDALGISA AV CHARLES DESANTO DOMINGO ESTE '
				},
				{
					'@Fecha': '03-01-2013',
					'#text': 'C/ELIAS PI\\A NO. 77 ENS. ESPAILLAT'
				},
				{
					'@Fecha': '06-12-2011',
					'#text': 'PADRE CASTELLANOS NO 153 SANTO DOMINGO DN RD'
				},
				{
					'@Fecha': '23-05-2011',
					'#text': 'PRIMERA 27 CHARLE DE GAULLE DISTRITO NACIONAL'
				},
				{
					'@Fecha': '26-01-2009',
					'#text': 'AV.TIRADENTES NO.5'
				},
				{
					'@Fecha': '31-05-2008',
					'#text': 'SIN NOMBRE 2 8011 IDALIA DEL ESTE CANCINO SANTO DOMINGO ESTE SANTO DOMINGO'
				},
				{
					'@Fecha': '06-02-2007',
					'#text': 'ELIAS PINA 77 ESPAILLAT SANTO DOMINGO'
				},
				{
					'@Fecha': '31-10-2003',
					'#text': 'C/PADRE CASTELLANO #153 ENS. ESPAILLAT SANTO DOMINGO'
				}]
			},
			'Telefonos': {
				'Tel': [{
					'@Tipo': '',
					'@Suscriptor': 'ONAILIN RAMIREZ NOVAS',
					'@Direccion': 'AV. TIRADENTES NO.5',
					'#text': '8096078434'
				},
				{
					'@Tipo': 'TEL',
					'@Suscriptor': 'RODRIGUEZ TATIZ, ELVIO',
					'@Direccion': 'REPARTO ALMA ROSA',
					'#text': '8092369674'
				},
				{
					'@Tipo': '',
					'@Suscriptor': 'DUENAS ARANGO',
					'@Direccion': 'AV TIRADENTES PLAZA NACO',
					'#text': '8092271439'
				},
				{
					'@Tipo': 'RESIDENCIAL',
					'@Suscriptor': 'NOVA FELIZ, NILDA NEYDA',
					'@Direccion': '146 C\\ JOSE NICOLAS CA ESPAILLAT SANTO DOMINGODN',
					'#text': '8092455936'
				},
				{
					'@Tipo': 'ALAMBRICO',
					'@Suscriptor': 'NILDA NEYDA NOVAS FELIZ',
					'@Direccion': 'C\\\\ JOSE NICOLAS CASIMIRO',
					'#text': '8092455936'
				},
				{
					'@Tipo': 'ADSL',
					'@Suscriptor': 'ONABEL SRL, CENTRO DE BELLEZA',
					'@Direccion': 'C\\\\ MAX HENRIQUEZ URENA 21 ENS. NACO SANTO DOMINGO DN RD',
					'#text': '8095635130'
				},
				{
					'@Tipo': 'ALAMBRICO',
					'@Suscriptor': 'DUENAS ARANGO',
					'@Direccion': 'AV TIRADENTES PLAZA NACO',
					'#text': '8092271236'
				},
				{
					'@Tipo': 'ALAMBRICO',
					'@Suscriptor': 'HAROLIN PRESINAL CORPORAN',
					'@Direccion': 'C\\\\ JOSE NICOLAS CASIMIRO',
					'#text': '8092453625'
				},
				{
					'@Tipo': '',
					'@Suscriptor': 'BANESSA SANCHEZ ARIZA',
					'@Direccion': '',
					'#text': '8098014479'
				},
				{
					'@Tipo': 'RESIDENCIAL',
					'@Suscriptor': 'ESPEJO M,PURA MARGARITA',
					'@Direccion': 'COSTA RICA 64OZAMA SANTO DOMINGO',
					'#text': '8095953116'
				},
				{
					'@Tipo': 'NEGOCIOS',
					'@Suscriptor': 'MICROCOMPUTOS S A',
					'@Direccion': '18 C\\ MAX HENRIQUEZ U SANTO DOMINGO',
					'#text': '8095406081'
				},
				{
					'@Tipo': '',
					'@Suscriptor': 'VIRGINIA ROJAS GUZMAN',
					'@Direccion': 'C\\\\ 26 3 BO. LOS ALCARRIZOS, SANTO DOMINGO OESTE, SD, RD',
					'#text': '8093301604'
				}]
			}
		}
	}
}";
            #endregion

            CreditReportBase cbase = new CreditReportBase();
            cbase = generatejson(jsonData);
            return cbase;
        }

        public static CreditReportBase generatejson(string jsonData)
        {
            CreditReportBase cbase = new CreditReportBase();
            Individual ind = new Individual();
            var sr = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);

            //  var sr = Newtonsoft.Json.JsonConvert.DeserializeObject(compJson);//
            JObject obj = (JObject)sr;

            var baseToken = obj.SelectToken("c").SelectToken("DCR");
            var individualToken = baseToken.SelectToken("Individuo");
            var securityToken = baseToken.SelectToken("Seguridad");
            var creditAnalysisToken = baseToken.SelectToken("AnalisisCrediticio");
            var breakDownCreditsToken = baseToken.SelectToken("DesgloseCreditos");
            var contactsToken = baseToken.SelectToken("PersonasRelacionadas");
            var addressesToken = baseToken.SelectToken("Direcciones");
            var telephonesToken = baseToken.SelectToken("Telefonos");
            var productToken = breakDownCreditsToken.SelectToken("Producto");
            var accountToken = productToken.SelectToken("Cuenta");
            var companyToken = baseToken.SelectToken("Empresa");
            var personRelationToken = baseToken.SelectToken("PersonasRelacionadas");
            var foto = baseToken.SelectToken("Foto");

            cbase.photo = foto.ToString();

            ind.age = individualToken.SelectToken("Edad").ToString();
            ind.informationconsulted = securityToken.SelectToken("InformacionConsultada").ToString();
            ind.name = individualToken.SelectToken("Nombres").ToString();
            ind.lastName = individualToken.SelectToken("Apellidos").ToString();
            ind.placeofbirth = individualToken.SelectToken("LugarNacimiento").ToString();
            ind.dob = individualToken.SelectToken("FechaNacimiento").ToString();
            ind.nationality = individualToken.SelectToken("Nacionalidad").ToString();
            ind.gender = individualToken.SelectToken("Sexo").ToString();
            ind.occupation = individualToken.SelectToken("Ocupacion").ToString();
            ind.conyugue = individualToken.SelectToken("Conyugue").ToString();
            ind.timeofreport = securityToken.SelectToken("FechaHora").ToString();
            ind.informationconsulted = securityToken.SelectToken("InformacionConsultada").ToString();
            ind.consultationId = securityToken.SelectToken("ConsultaId").ToString();
            ind.motherName = individualToken.SelectToken("Madre").ToString();
            ind.fatherName = individualToken.SelectToken("Padre").ToString();
            cbase.ind = ind;
            CreditAnalysis cre = null;

            #region Credit Analysis
            // Credit Analysis
            if (creditAnalysisToken != null)
            {
                List<CreditAnalysis> creditList = new List<CreditAnalysis>();
                //Individual Total account 
                var cantidadTotalCuentasToken = creditAnalysisToken.SelectToken("CantidadTotalCuentas").SelectToken("Totales");
                cre = new CreditAnalysis();
                if (cantidadTotalCuentasToken != null)
                {
                    Totales total = new Totales();
                    total.TotalAccounts = cantidadTotalCuentasToken.SelectToken("Cuentas").ToString();
                    total.TotalClosed = cantidadTotalCuentasToken.SelectToken("Cerradas").ToString();
                    total.TotalNormal = cantidadTotalCuentasToken.SelectToken("Normal").ToString();
                    total.TotalOnDelay = cantidadTotalCuentasToken.SelectToken("Abiertas").ToString();
                    total.TotalOpen = cantidadTotalCuentasToken.SelectToken("Cuentas").ToString();
                    cbase.totales = total;
                }
                #region Individual credit
                //Individual credit analysis
                var analisisCreditosToken = creditAnalysisToken.SelectToken("AnalisisCreditos").SelectToken("Moneda");
                if (analisisCreditosToken != null)
                {
                    if (analisisCreditosToken.Type == JTokenType.Array)
                    {
                        List<CreditAnalysis> creditAnalysisList = new List<CreditAnalysis>();
                        List<JToken> currencyList = analisisCreditosToken.ToList();
                        foreach (var currencyItem in currencyList)
                        {
                            CreditAnalysis mon = new CreditAnalysis();

                            mon.mostRecentAmount = currencyItem.SelectToken("MasReciente").SelectToken("Monto").ToString();
                            mon.mostRecentDate = currencyItem.SelectToken("MasReciente").SelectToken("Fecha").ToString();
                            mon.mostRecentYears = currencyItem.SelectToken("MasReciente").SelectToken("Anos").ToString();

                            mon.oldestDateAmount = currencyItem.SelectToken("MasAntiguo").SelectToken("Monto").ToString();
                            mon.oldestDate = currencyItem.SelectToken("MasAntiguo").SelectToken("Fecha").ToString();
                            mon.oldestDateYears = currencyItem.SelectToken("MasAntiguo").SelectToken("Anos").ToString();

                            mon.retailApprovedDate = currencyItem.SelectToken("MenorAprobado").SelectToken("Monto").ToString();
                            mon.retailApprovedAmount = currencyItem.SelectToken("MenorAprobado").SelectToken("Fecha").ToString();
                            mon.retailApprovedYears = currencyItem.SelectToken("MenorAprobado").SelectToken("Anos").ToString();

                            mon.increasedDueAmount = currencyItem.SelectToken("MayorAdeudado").SelectToken("Monto").ToString();
                            mon.increasedDueDate = currencyItem.SelectToken("MayorAdeudado").SelectToken("Fecha").ToString();
                            mon.increasedDueYears = currencyItem.SelectToken("MayorAdeudado").SelectToken("Anos").ToString();
                            creditAnalysisList.Add(mon);
                        }
                        cbase.creditAnalysis = creditAnalysisList;
                    }
                    else
                    {
                        List<CreditAnalysis> creditAnalysisList = new List<CreditAnalysis>();
                        CreditAnalysis mon = new CreditAnalysis();

                        mon.mostRecentAmount = analisisCreditosToken.SelectToken("MasReciente").SelectToken("Monto").ToString();
                        mon.mostRecentDate = analisisCreditosToken.SelectToken("MasReciente").SelectToken("Fecha").ToString();
                        mon.mostRecentYears = analisisCreditosToken.SelectToken("MasReciente").SelectToken("Anos").ToString();

                        mon.oldestDateAmount = analisisCreditosToken.SelectToken("MasAntiguo").SelectToken("Monto").ToString();
                        mon.oldestDate = analisisCreditosToken.SelectToken("MasAntiguo").SelectToken("Fecha").ToString();
                        mon.oldestDateYears = analisisCreditosToken.SelectToken("MasAntiguo").SelectToken("Anos").ToString();

                        mon.retailApprovedDate = analisisCreditosToken.SelectToken("MenorAprobado").SelectToken("Monto").ToString();
                        mon.retailApprovedAmount = analisisCreditosToken.SelectToken("MenorAprobado").SelectToken("Fecha").ToString();
                        mon.retailApprovedYears = analisisCreditosToken.SelectToken("MenorAprobado").SelectToken("Anos").ToString();

                        mon.increasedDueAmount = analisisCreditosToken.SelectToken("MayorAdeudado").SelectToken("Monto").ToString();
                        mon.increasedDueDate = analisisCreditosToken.SelectToken("MayorAdeudado").SelectToken("Fecha").ToString();
                        mon.increasedDueYears = analisisCreditosToken.SelectToken("MayorAdeudado").SelectToken("Anos").ToString();
                        creditAnalysisList.Add(mon);
                        cbase.creditAnalysis = creditAnalysisList;
                    }
                }
                #endregion
                /// //Individual credit analysis Ends 
                #region Peor
                var PeorAtraso = creditAnalysisToken.SelectToken("PeorAtraso").SelectToken("Moneda");
                if (PeorAtraso != null)
                {
                    if (PeorAtraso.Type == JTokenType.Array)
                    {
                        List<Delays> currency = new List<Delays>();
                        List<JToken> currencyList = PeorAtraso.ToList();
                        foreach (var currencyItem in currencyList)
                        {
                            Delays mon = new Delays();
                            mon.worstDelaysDate = currencyItem.SelectToken("Fecha").ToString();
                            mon.worstDelaysDays = currencyItem.SelectToken("DiasAtraso").ToString();
                            mon.worstDelaysYears = currencyItem.SelectToken("Anos").ToString();
                            mon.creditApproved = currencyItem.SelectToken("CreditoAprobado").ToString();
                            mon.delayAmount = currencyItem.SelectToken("MontoAtraso").ToString();
                        }
                    }
                    else
                    {
                        List<Delays> currency = new List<Delays>();
                        Delays mon = new Delays();
                        mon.worstDelaysDate = PeorAtraso.SelectToken("Fecha").ToString();
                        mon.worstDelaysDays = PeorAtraso.SelectToken("DiasAtraso").ToString();
                        mon.worstDelaysYears = PeorAtraso.SelectToken("Anos").ToString();
                        mon.creditApproved = PeorAtraso.SelectToken("CreditoAprobado").ToString();
                        mon.delayAmount = PeorAtraso.SelectToken("MontoAtraso").ToString();
                        currency.Add(mon);


                    }
                }

                #endregion
                #region Product
                if (productToken != null)
                {
                    List<Product> prodList = new List<Product>();
                    List<JToken> productList = productToken.ToList();
                    foreach (var item in productList)
                    {

                        Product prod = new Product();
                        prod.code = item.SelectToken("@Id").ToString();
                        var accountsList = item.SelectToken("Cuenta");
                        List<Accounts> accnt = null;
                        if (accountsList.Type == JTokenType.Array)
                        {
                            accnt = new List<Accounts>();
                            foreach (var ceunta in accountsList)
                            {
                                Accounts acc = new Accounts();

                                acc.currency = ceunta.SelectToken("Moneda").SelectToken("@Des_Moneda").ToString();
                                acc.affiliates = ceunta.SelectToken("Afiliado").ToString();
                                if (ceunta.SelectToken("FechaApertura") != null)
                                {
                                    acc.openedDate = (ceunta.SelectToken("FechaApertura").ToString());
                                }
                                if (ceunta.SelectToken("FechaReportado") != null)
                                {
                                    acc.reportedDate = (ceunta.SelectToken("FechaReportado").ToString());
                                }
                                if (ceunta.SelectToken("FechaUltimaTransaccion") != null)
                                {
                                    acc.lastTransactionDate = (ceunta.SelectToken("FechaUltimaTransaccion").ToString());
                                }
                                acc.consolidateMonth = ceunta.SelectToken("MesConsolidado").ToString();
                                acc.totalOwed = ceunta.SelectToken("TotalAdeudado").ToString();
                                acc.totalDelay = ceunta.SelectToken("TotalAtraso").ToString();
                                acc.totalDelay30 = ceunta.SelectToken("VecesAtraso30").ToString();
                                acc.totalDelay120 = ceunta.SelectToken("VecesAtraso120").ToString();
                                acc.totalDelay60 = ceunta.SelectToken("VecesAtraso60").ToString();
                                acc.totalDelay90 = ceunta.SelectToken("VecesAtraso90").ToString();
                                acc.totalapproved = ceunta.SelectToken("CreditoAprobado").ToString();
                                acc.quota = ceunta.SelectToken("Cuota").ToString();

                                var paymentHistory = ceunta.SelectToken("HistorialPago");
                                List<PaymentHistory> history = new List<PaymentHistory>();
                                if (paymentHistory.Count() != 0)
                                {
                                    for (int i = 1; i <= 24; i++)
                                    {
                                        PaymentHistory his = new PaymentHistory();
                                        his.F = paymentHistory.SelectToken("M" + i).SelectToken("@F").ToString();
                                        if (paymentHistory.SelectToken("M" + i).SelectToken("@PDV") != null)
                                        {
                                            his.PDV = paymentHistory.SelectToken("M" + i).SelectToken("@PDV").ToString();
                                        }
                                        if (paymentHistory.SelectToken("M" + i).SelectToken("#text") != null)
                                        {
                                            his.text = paymentHistory.SelectToken("M" + i).SelectToken("#text").ToString();
                                        }

                                        history.Add(his);
                                    }
                                }
                                acc.paymentHistory = history;
                                accnt.Add(acc);
                                prod.account = accnt;

                            }
                        }
                        else
                        {
                            List<Accounts> singleaccnt = null;
                            Accounts acc = new Accounts();
                            singleaccnt = new List<Accounts>();
                            acc.currency = item.SelectToken("Cuenta").SelectToken("Moneda").SelectToken("@Des_Moneda").ToString();
                            acc.affiliates = item.SelectToken("Cuenta").SelectToken("Afiliado").ToString();
                            if (item.SelectToken("Cuenta").SelectToken("FechaApertura") != null)
                            {
                                acc.openedDate = (item.SelectToken("Cuenta").SelectToken("FechaApertura").ToString());
                            }
                            if (item.SelectToken("Cuenta").SelectToken("FechaReportado") != null)
                            {
                                acc.reportedDate = (item.SelectToken("Cuenta").SelectToken("FechaReportado").ToString());
                            }
                            if (item.SelectToken("Cuenta").SelectToken("FechaUltimaTransaccion") != null)
                            {
                                acc.lastTransactionDate = (item.SelectToken("Cuenta").SelectToken("FechaUltimaTransaccion").ToString());
                            }
                            acc.consolidateMonth = item.SelectToken("Cuenta").SelectToken("MesConsolidado").ToString();
                            acc.totalOwed = item.SelectToken("Cuenta").SelectToken("TotalAdeudado").ToString();
                            acc.totalDelay = item.SelectToken("Cuenta").SelectToken("TotalAtraso").ToString();
                            acc.totalDelay30 = item.SelectToken("Cuenta").SelectToken("VecesAtraso30").ToString();
                            acc.totalDelay120 = item.SelectToken("Cuenta").SelectToken("VecesAtraso120").ToString();
                            acc.totalDelay60 = item.SelectToken("Cuenta").SelectToken("VecesAtraso60").ToString();
                            acc.totalDelay90 = item.SelectToken("Cuenta").SelectToken("VecesAtraso90").ToString();


                            var paymentHistory = item.SelectToken("Cuenta").SelectToken("HistorialPago");

                            List<PaymentHistory> history = new List<PaymentHistory>();
                            if (paymentHistory.Count() != 0)
                            {
                                for (int i = 1; i <= 24; i++)
                                {
                                    PaymentHistory his = new PaymentHistory();
                                    his.F = paymentHistory.SelectToken("M" + i).SelectToken("@F").ToString();
                                    if (paymentHistory.SelectToken("M" + i).SelectToken("@PDV") != null)
                                    {
                                        his.PDV = paymentHistory.SelectToken("M" + i).SelectToken("@PDV").ToString();
                                    }
                                    if (paymentHistory.SelectToken("M" + i).SelectToken("#text") != null)
                                    {
                                        his.text = paymentHistory.SelectToken("M" + i).SelectToken("#text").ToString();
                                    }
                                    history.Add(his);
                                }
                            }
                            acc.paymentHistory = history;
                            singleaccnt.Add(acc);
                            prod.account = singleaccnt;

                        }
                        prodList.Add(prod);
                    }
                    cbase.product = prodList;

                    // List<Product> Product;
                }
                #endregion
                #region Address and Telephone
                // Phone list of the merchant
                if (telephonesToken != null)
                {
                    List<Telephone> phoneList = null;
                    var telePhoneList = telephonesToken.SelectToken("Tel");
                    List<JToken> phones = telePhoneList.ToList();
                    if (phones.Count > 0)
                    {
                        phoneList = new List<Telephone>();
                        foreach (var item in phones)
                        {
                            Telephone tel = new Telephone();
                            tel.telephone = item.SelectToken("#text").ToString();
                            tel.subscriptor = item.SelectToken("@Suscriptor").ToString();
                            tel.address = item.SelectToken("@Direccion").ToString();
                            phoneList.Add(tel);
                        }
                    }
                    cbase.telphones = phoneList;
                }
                // Addresses list of the merchant
                if (addressesToken != null)
                {
                    List<Addresses> addressList = null;
                    var address = addressesToken.SelectToken("Dir");
                    List<JToken> list = addressesToken.ToList();
                    if (list.Count > 0)
                    {
                        foreach (var item in address)
                        {
                            addressList = new List<Addresses>();
                            Addresses adr = new Addresses();
                            adr.DateFetched = item.SelectToken("@Fecha").ToString();
                            adr.Value = item.SelectToken("#text").ToString();
                            addressList.Add(adr);
                        }
                    }
                    cbase.address = addressList;
                }
                //Person relation of the user
                if (telephonesToken != null)
                {
                    List<PersonRelation> personList = null;
                    var personRelationList = personRelationToken.SelectToken("Persona");
                    if (personRelationList.Type == JTokenType.Array)
                    {

                        List<JToken> phones = personRelationList.ToList();
                        if (phones.Count > 0)
                        {
                            personList = new List<PersonRelation>();
                            foreach (var item in phones)
                            {
                                PersonRelation person = new PersonRelation();
                                person.age = item.SelectToken("Edad").ToString();
                                person.lastname = item.SelectToken("Apellidos").ToString();
                                person.name = item.SelectToken("Nombres").ToString();
                                person.newId = item.SelectToken("CedulaNueva").ToString();
                                person.oldId = item.SelectToken("CedulaVieja").ToString();
                                personList.Add(person);
                            }
                        }
                    }
                    else
                    {

                        personList = new List<PersonRelation>();
                        PersonRelation person = new PersonRelation();
                        person.age = personRelationList.SelectToken("Edad").ToString();
                        person.lastname = personRelationList.SelectToken("Apellidos").ToString();
                        person.name = personRelationList.SelectToken("Nombres").ToString();
                        person.newId = personRelationList.SelectToken("CedulaNueva").ToString();
                        person.oldId = personRelationList.SelectToken("CedulaVieja").ToString();
                        personList.Add(person);
                    }

                    cbase.person = personList;
                }
                #endregion



            }
            #endregion
            return cbase;
        }
        #endregion

        #region Contracts
        public ActionResult Contracts(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/contracts?StartDate=1900/01/01&EndDate=2100/01/01", MerchantID);
            MPMerchantContractDetailModel obj = BaseApiData.GetAPIResult<MPMerchantContractDetailModel>(apiMethod, () => new MPMerchantContractDetailModel());
            return PartialView("_Contracts", obj);
        }

        [HttpPost]
        public ActionResult SearchContract(string submitButton, MPMerchantContractDetailModel obj)
        {
            MPMerchantContractDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Search":
                        objOutput = FilterContractInformation(obj);
                        break;
                }
            }
            return PartialView("_Contracts", objOutput);
        }

        public MPMerchantContractDetailModel FilterContractInformation(MPMerchantContractDetailModel obj)
        {
            DateTime dt1, dt2;
            string[] Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };
            var apiMethod = string.Format("merchantprofile/{0}/contracts?StartDate=" +
                (DateTime.TryParseExact(obj.StartDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) ? DateTime.Parse("1900/01/01") : obj.StartDate) + "&EndDate=" +
                (DateTime.TryParseExact(obj.EndDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2) ? DateTime.Parse("1900/01/01") : obj.EndDate), base.CurrentMerchantID);
            MPMerchantContractDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantContractDetailModel>(apiMethod, () => new MPMerchantContractDetailModel());
            return objOutput;
        }
        public ActionResult ContractGeneralInformation(Int64 ContractID)
        {
            SessionHelper.SetContractID(ContractID);
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/generalinfo", base.CurrentMerchantID, ContractID);
            MPMerchantContractModel obj = BaseApiData.GetAPIResult<MPMerchantContractModel>(apiMethod, () => new MPMerchantContractModel());
            return PartialView("_ContractDetails", obj);
        }
        public void UpdateContractGeneralInformation(MPMerchantContractModel obj)
        {
            Int64 CurrentMerchantID = base.CurrentMerchantID;
            obj.UserId = base.CurrentUserID;
            if (ModelState.IsValid)
            {
                var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/generalinfo/update", base.CurrentMerchantID, ContractID);
                var response = BaseApiData.PutAPIData<MPMerchantContractModel>(apiMethod, obj);

                if (response.StatusCode == HttpStatusCode.OK)
                    base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContractGeneralInfoUpdateSuccess);
                else
                    base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContractGeneralInfoUpdateFailure);
            }
        }
        public ActionResult ContractActivities(Int64 ContractID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/Activities?ProcessorTypeId=0&ProcessorNumber=All&ActivityStartDate=1900/01/01&ActivityEndDate=2100/01/01", base.CurrentMerchantID, ContractID);
            MPMerchantContractModel obj = BaseApiData.GetAPIResult<MPMerchantContractModel>(apiMethod, () => new MPMerchantContractModel());
            obj.ActivityDetail.Processors = GetProcessors().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return PartialView("_ContractActivity", obj);
        }
        public ActionResult ContractHistory(Int64 ContractID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/history?HistoryStartDate=1900/01/01&HistoryEndDate=2100/01/01", base.CurrentMerchantID, ContractID);
            MPMerchantContractModel obj = BaseApiData.GetAPIResult<MPMerchantContractModel>(apiMethod, () => new MPMerchantContractModel());
            return PartialView("_ContractHistory", obj);
        }
        public ActionResult ContractSalesRep(Int64 ContractID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/salesrep", base.CurrentMerchantID, ContractID);
            MPMerchantContractModel obj = BaseApiData.GetAPIResult<MPMerchantContractModel>(apiMethod, () => new MPMerchantContractModel());
            return PartialView("_ContractSalesRep", obj);
        }
        public ActionResult ContractSalesRepDetails(int SalesRepId)
        {
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/salesrep/{2}", base.CurrentMerchantID, base.ContractID, SalesRepId);
            MPMerchantContractSalesRepresentativeModel obj = BaseApiData.GetAPIResult<MPMerchantContractSalesRepresentativeModel>(apiMethod, () => new MPMerchantContractSalesRepresentativeModel());
            obj.AllSalesReps = GetSalesReps();
            return PartialView("_ContractSalesRepDetails", obj);
        }
        public ActionResult AddContractSalesRep()
        {
            MPMerchantContractSalesRepresentativeModel obj = new MPMerchantContractSalesRepresentativeModel();
            obj.ContractId = base.ContractID;
            obj.UserId = base.CurrentUserID;
            obj.AllSalesReps = GetSalesReps();
            return PartialView("_ContractSalesRepDetails", obj);
        }

        [HttpPost]
        public ActionResult ProcessContractGeneralInformation(string submitButton, MPMerchantContractModel obj)
        {
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "UpdateGeneralInfo":
                        UpdateContractGeneralInformation(obj);
                        break;
                    case "ScannedContract":
                        DownloadContract();
                        break;
                    case "ScannedIOU":
                        DownloadIOU();
                        break;
                }
            }
            return PartialView("_ContractGeneralInfo", obj);
        }

        [HttpPost]
        public ActionResult ProcessContractHistory(string submitButton, MPMerchantContractModel obj)
        {
            MPMerchantContractModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "FilterHistory":
                        objOutput = FilterContractHistoryInformation(obj);
                        break;
                }
            }
            return PartialView("_ContractHistory", objOutput);
        }

        [HttpPost]
        public ActionResult ProcessContractActivity(string submitButton, MPMerchantContractModel obj)
        {
            MPMerchantContractModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "FilterActivities":
                        objOutput = FilterContractActivitiesInformation(obj);
                        break;
                }
            }
            return PartialView("_ContractActivity", objOutput);
        }

        [HttpPost]
        public ActionResult ProcessContractSalesRep(string submitButton, MPMerchantContractSalesRepresentativeModel obj)
        {
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "AddSalesRep":
                        AddSalesRepInformation(obj);
                        break;
                    case "UpdateSalesRep":
                        UpdateSalesRepInformation(obj);
                        break;
                }
            }
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/salesrep", base.CurrentMerchantID, ContractID);
            MPMerchantContractModel objOutput = BaseApiData.GetAPIResult<MPMerchantContractModel>(apiMethod, () => new MPMerchantContractModel());
            return PartialView("_ContractSalesRep", objOutput);
        }

        public MPMerchantContractModel FilterContractActivitiesInformation(MPMerchantContractModel obj)
        {
            DateTime dt1, dt2;
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/Activities?ProcessorTypeId=" + obj.ActivityDetail.ProcessorTypeId + "&ProcessorNumber=" + (string.IsNullOrEmpty(obj.ActivityDetail.ProcessorNumber) ? "All" : obj.ActivityDetail.ProcessorNumber) + "&ActivityStartDate=" + (DateTime.TryParse(obj.ActivityDetail.ActivityFrom.ToString(), out dt1) ? DateTime.Parse("1900/01/01") : obj.ActivityDetail.ActivityFrom) + "&ActivityEndDate=" + (DateTime.TryParse(obj.ActivityDetail.ActivityTo.ToString(), out dt2) ? DateTime.Parse("1900/01/01") : obj.ActivityDetail.ActivityTo), base.CurrentMerchantID, obj.ContractId);
            MPMerchantContractModel objOutputWrapped = BaseApiData.GetAPIResult<MPMerchantContractModel>(apiMethod, () => new MPMerchantContractModel());
            objOutputWrapped.ActivityDetail.Processors = GetProcessors().Union(new List<SelectListItem>() { new SelectListItem() { Text = "All", Value = "0" } }.AsEnumerable()); ;
            return objOutputWrapped;
        }

        public MPMerchantContractModel FilterContractHistoryInformation(MPMerchantContractModel obj)
        {
            DateTime dt1, dt2;
            string[] Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/history?HistoryStartDate=" +
                (DateTime.TryParseExact(obj.HistoryDetail.HistoryStartDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) ? DateTime.Parse("1900/01/01") : obj.HistoryDetail.HistoryStartDate) + "&HistoryEndDate=" +
                (DateTime.TryParseExact(obj.HistoryDetail.HistoryEndDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2) ? DateTime.Parse("1900/01/01") : obj.HistoryDetail.HistoryEndDate), base.CurrentMerchantID, obj.ContractId);
            MPMerchantContractModel objOutputWrapped = BaseApiData.GetAPIResult<MPMerchantContractModel>(apiMethod, () => new MPMerchantContractModel());
            return objOutputWrapped;
        }
        public void UpdateSalesRepInformation(MPMerchantContractSalesRepresentativeModel obj)
        {
            obj.ContractId = base.ContractID;
            obj.UserId = base.CurrentUserID;
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/salesrep/update", base.CurrentMerchantID, base.ContractID);
            var response = BaseApiData.PutAPIData<MPMerchantContractSalesRepresentativeModel>(apiMethod, obj);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContractSalesRepUpdateSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContractSalesRepUpdateFailure);
        }
        public void AddSalesRepInformation(MPMerchantContractSalesRepresentativeModel obj)
        {
            obj.ContractId = base.ContractID;
            obj.UserId = base.CurrentUserID;
            var apiMethod = string.Format("merchantprofile/{0}/contracts/{1}/salesrep/add", base.CurrentMerchantID, base.ContractID);
            HttpResponseMessage response = BaseApiData.PutAPIData<MPMerchantContractSalesRepresentativeModel>(apiMethod, obj);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContractSalesRepAddSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContractSalesRepAddFailure);
        }

        public FileResult DownloadIOU()
        {
            ContractApi contractApi = new ContractApi();
            string fileName = string.Format("IOU_{0}_{1}.pdf", CurrentMerchantID, ContractID);
            var iou = contractApi.GetIOUDetails(CurrentMerchantID, ContractID);

            string templateFile = string.Empty;


            if (iou.BusinesTypeId == 11001) 	//Persona Fisica
            {
                if (iou.StateId == 30) //Santo Domingo
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUPersonaFisicaSantoDomingo2Owner.xslt";
                    }
                    else
                    {
                        templateFile = "IOUPersonaFisicaSantoDomingo1Owner.xslt";
                    }

                }
                else
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUPersonaFisica2Owner.xslt";
                    }
                    else
                    {
                        templateFile = "IOUPersonaFisica1Owner.xslt";
                    }
                }

            }
            else
            {
                if (iou.StateId == 30) //Santo Domingo
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUSantoDomingo2Owner.xslt";
                    }
                    else
                    {
                        templateFile = "IOUSantoDomingo1Owner.xslt";
                    }

                }
                else
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUOther2Owner.xslt";
                    }
                    else
                    {
                        templateFile = "IOUOther1Comp.xslt";
                    }
                }
            }



            string destPdf = Path.Combine(Server.MapPath("~/Docs/Contract"), fileName);

            PdfHelper pdfHelper = new PdfHelper();

            pdfHelper.CreatePDF(iou, Server.MapPath("~/App_Data/Contract/" + templateFile), destPdf);

            return File(destPdf, "application/pdf", "IOU.pdf");
        }

        public FileResult DownloadContract()
        {
            ContractApi contractApi = new ContractApi();
            string blafileName = string.Format("Cont_{0}_{1}.pdf", CurrentMerchantID, ContractID);

            var bla = contractApi.GetBLADetails(CurrentMerchantID, ContractID);

            string tempFilePath = System.IO.Path.GetTempFileName();

            string destPdf = Path.Combine(Server.MapPath("~/Docs/Contract"), blafileName);
            string termsPdf = Server.MapPath("~/App_Data/Contract/Terms.pdf");

            PdfHelper pdfHelper = new PdfHelper();
            pdfHelper.CreatePDF(bla, Server.MapPath("~/App_Data/Contract/ContractTemplate.xslt"), tempFilePath);

            pdfHelper.CombineMultiplePDFs(new string[] { tempFilePath, termsPdf }, destPdf);

            return File(blafileName, "application/pdf");
        }
        #endregion

        #region Collections
        public ActionResult Collections(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/collections?StartDate=1900/01/01&EndDate=2100/01/01", MerchantID);
            MPMerchantCollectionDetailModel obj = BaseApiData.GetAPIResult<MPMerchantCollectionDetailModel>(apiMethod, () => new MPMerchantCollectionDetailModel());
            return PartialView("_Collections", obj);
        }
        [HttpPost]
        public ActionResult ProcessCollections(string submitButton, MPMerchantCollectionDetailModel obj)
        {
            MPMerchantCollectionDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Search":
                        objOutput = FilterCollectionInformation(obj);
                        break;
                }
            }
            return PartialView("_Collections", objOutput);
        }

        public MPMerchantCollectionDetailModel FilterCollectionInformation(MPMerchantCollectionDetailModel obj)
        {
            DateTime dt1, dt2;
            string[] Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "yyyy/MM/dd" };
            var apiMethod = string.Format("merchantprofile/{0}/collections?StartDate=" +
                (DateTime.TryParseExact(obj.StartDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) ? DateTime.Parse("1900/01/01") : obj.StartDate) + "&EndDate=" +
                (DateTime.TryParseExact(obj.EndDate.Value.ToShortDateString(), Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2) ? DateTime.Parse("1900/01/01") : obj.EndDate), base.CurrentMerchantID);
            MPMerchantCollectionDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantCollectionDetailModel>(apiMethod, () => new MPMerchantCollectionDetailModel());
            return objOutput;
        }
        #endregion

        #region Affiliations
        public ActionResult Affiliations(int MerchantID)
        {
            var apiMethod = string.Format("merchantprofile/{0}/affiliations?RequestType=RNC", MerchantID);
            MPMerchantAffiliationDetailModel obj = BaseApiData.GetAPIResult<MPMerchantAffiliationDetailModel>(apiMethod, () => new MPMerchantAffiliationDetailModel());
            return PartialView("_Affiliations", obj);
        }
        [HttpPost]
        public ActionResult ProcessAffiliations(string submitButton, MPMerchantAffiliationDetailModel obj)
        {
            MPMerchantAffiliationDetailModel objOutput = null;
            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "Search":
                        objOutput = FilterAffiliationsInformation(obj);
                        break;
                    case "MCCV":
                        SendRequesttoProcessor();
                        break;
                }
            }
            return PartialView("_Affiliations", objOutput);
        }

        public MPMerchantAffiliationDetailModel FilterAffiliationsInformation(MPMerchantAffiliationDetailModel obj)
        {
            var apiMethod = string.Format("merchantprofile/{0}/affiliations?RequestType=" + obj.RequestType, base.CurrentMerchantID);
            MPMerchantAffiliationDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantAffiliationDetailModel>(apiMethod, () => new MPMerchantAffiliationDetailModel());
            return objOutput;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendRequesttoProcessor()
        {
            var apiMethod = string.Format("merchants/profiles/requestprocessor?merchantid={0}&processorId={1}", base.CurrentMerchantID, 0);
            var model = BaseApiData.PostAPIData<MPCreditVolumesModel>(apiMethod, new MPCreditVolumesModel());
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CreditReports
        public ActionResult CreditReports(Int64 MerchantID)
        {
            MPMerchantCreditReportDetailModel obj = new MPMerchantCreditReportDetailModel();
            obj.AllContracts = GetMerchantContracts().Union(new List<SelectListItem>() { new SelectListItem() { Text = "Select", Value = "0" } }.AsEnumerable());
            return PartialView("_CreditReports", obj);
        }   
     
        public ActionResult LoadCreditReportDropdown(Int64 ContractId)
        {
            var returnValue = GetMerchantMerchantContractCreditReports(ContractId);
            return Json(returnValue, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult ProcessCreditReport(string submitButton, MPMerchantCreditReportDetailModel obj)
        //{
        //    MPMerchantCreditReportDetailModel objOutput = null;
        //    if (ModelState.IsValid)
        //    {
        //        switch (submitButton)
        //        {
        //            case "Search":
        //                objOutput = LoadCreditReportData(obj);
        //                break;
        //        }
        //    }
        //    return PartialView("_CreditReports", objOutput);
        //}

        public ActionResult LoadCreditReportData(Int64 MerchantId, Int64 ContractId, int ReportId)
        {
            var apiMethod = string.Format("merchantprofile/{0}/creditreport/contracts/{1}/creditreports/{2}", CurrentMerchantID, ContractId, ReportId);
            MPMerchantCreditReportDetailModel objOutput = BaseApiData.GetAPIResult<MPMerchantCreditReportDetailModel>(apiMethod, () => new MPMerchantCreditReportDetailModel());
            objOutput.AllContracts = GetMerchantContracts();
            return PartialView("_CreditReportDetails", objOutput);
        }
        #endregion

        #region Common Functions
        private IEnumerable<SelectListItem> GetMerchantContracts()
        {
            var query = "merchantprofile/" + CurrentMerchantID + "/creditreport/contracts";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetMerchantMerchantContractCreditReports(Int64 contractId)
        {
            var query = "merchantprofile/" + CurrentMerchantID + "/creditreport/contracts/" + contractId + "/creditreports";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetProcessors()
        {
            var query = "gen/retrieve/1010";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetEntities()
        {
            var query = "gen/retrieve/1002";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetIndustries()
        {
            var query = "gen/retrieve/1001";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetProvinces()
        {
            var query = "gen/retrieve/1009";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetPropertyTypes()
        {
            var query = "gen/retrieve/1012";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetDocumentTypes()
        {
            var query = "gen/retrieve/1013";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetSalesReps()
        {
            var query = "gen/retrieve/1015";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        private IEnumerable<SelectListItem> GetMerchantStatuses()
        {
            var query = "merchantprofile/status?strStatusType=mrc";
            var docTypes = BaseApiData.GetAPIResult<IList<MPGeneralModel>>(query, () => new List<MPGeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }
        #endregion
    }
}