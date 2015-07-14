using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.Web.Models;
using System.IO;
using Pecuniaus.UICore.Controllers;
using Pecuniaus.ApiHelper;
using Pecuniaus.UICore;

namespace Pecuniaus.Web.Controllers
{
    public class MerchantDocumentScanController : UiBaseController
    {
        public MerchantDocumentScanController()
        {
            base.WorkflowID = 1;
        }

        //
        // GET: /MerchantDocumentScan/
        public ActionResult Index()
        {
            string apiQuery = string.Format("documents/RetriveDocument?merchantId={0}&contractId={1}&documentTypeId={2}", CurrentMerchantID, ContractID, 11);
            var doc = ApiHelper.BaseApiData.GetAPIResult<IList<MerchantDocumentModel>>(apiQuery, () => new List<MerchantDocumentModel>());

            //MerchantScanDocumentModel model;

            string fileName = "NoImage.jpg";
            string fileDetails = "image/jpg";
            MerchantScanDocumentModel model = new MerchantScanDocumentModel();
            model.FileDetails = string.Empty;
    
            if (doc.Count > 0)
            {
                if (!string.IsNullOrEmpty(doc[0].FileName))
                {
                    fileName = doc[0].FileName;
                    fileDetails = doc[0].FileDetails;
                }
                model.DocumentID = doc[0].DocumentId;
            }

            model.FileName = fileName;
            model.FileDetails = fileDetails;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MerchantScanDocumentModel mod, HttpPostedFileBase file, string button)
        {
                      
                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = "doc_" + mod.DocumentID + mod.DocumentTypeID + "_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                        file.SaveAs(path);

                        var docModel = new MerchantDocumentModel()
                        {
                            DocumentId = mod.DocumentID,
                            ContractId = ContractID,
                            MerchantId = CurrentMerchantID,
                            DocumentTypeId = 11,
                            FileName = fileName,
                            FileDetails = file.ContentType,
                            UploadUserId = CurrentUserID,
                        };

                        if (mod.DocumentID > 0)
                        {
                            ApiHelper.BaseApiData.PutAPIData<MerchantDocumentModel>("documents/UpdateDocuments", docModel);
                        }
                        else
                        {
                            ApiHelper.BaseApiData.PutAPIData<MerchantDocumentModel>("documents/InsertContDocument", docModel);
                        }
                        // Save Data
                        base.SetSuccessMessage("Document Updated.");
                        //// TempData["SuccessMsg"] = "Document Updated.";
                        //return RedirectToAction("Index");
                    }

                }

                if (button == "Complete")
                {
                    string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
                  CurrentMerchantID, (int)TaskTypes.PQScanDocument, 1, ContractID);
                    BaseApiData.GetAPIResult<object>(apiData, () => new object());

                    base.SetSuccessMessage("Scan Doc - Task Completed");
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { redirectToUrl = Url.Action("Index", "MerchantDataEntry") });
                    }                 
                }
            
            return RedirectToAction("Index");
          
        //    return View("Index", mod);
        }

        //[HttpPost]
        //public ActionResult Complete()
        //{
        //    string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}", 
        //        CurrentMerchantID, (int)TaskTypes.ScanDocument, 1, ContractID);
        //    BaseApiData.GetAPIResult<object>(apiData, () => new object());

        //    base.SetSuccessMessage("Scan Doc - Task Completed");
        //    if (Request.IsAjaxRequest())
        //    {
        //        return Json(new { redirectToUrl = Url.Action("Index", "MerchantDataEntry") });
        //    }
        //    return RedirectToAction("Index", "MerchantDataEntry");
        //}


        public ActionResult GetImage()
        {
            string apiQuery = string.Format("documents/RetriveDocument?merchantId={0}&contractId={1}&documentTypeId={2}", CurrentMerchantID, ContractID, 11);
            var doc = ApiHelper.BaseApiData.GetAPIResult<IList<MerchantDocumentModel>>(apiQuery, () => new List<MerchantDocumentModel>());

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
}