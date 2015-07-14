using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using Pecuniaus.UICore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Contract.Controllers
{
    public class DocumentScanController : BaseController
    {
        ContractApi contractApi = null;
        DocumentsApi documentsApi = null;

        public DocumentScanController()
        {
            contractApi = new ContractApi();
            documentsApi = new DocumentsApi();
        }

        public ActionResult Index()
        {
            var model = new ScanDocumentModel();
            model.DocumentTypes = GetDocTypes();
            return View(model);
        }

        public ActionResult ScanDoc()
        {
            
            var model = new ScanDocumentModel();
            model.DocumentTypes = GetDocTypes();
            var saleModel= contractApi.RetrieveAnnualSalesFile( base.CurrentMerchantID, base.ContractID);
            var rv = new Tuple<ScanDocumentModel, AnnualSaleModel>(model, saleModel);
            return View(rv);


            //   return PartialView("_ScanDoc", model);
        }

        [HttpPost]
        public void Index(ScanDocumentModel mod, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var docModel = new DocumentModel();
                string fileName = "";
                string fileType = "";
                if (file != null && file.ContentLength > 0)
                {
                    fileName = "doc_" + mod.DocumentID + mod.DocumentTypeID + "_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                    file.SaveAs(path);
                    fileType = file.ContentType;
                    // Save Data
                    // TempData["SuccessMsg"] = "Document Updated.";
                    // return RedirectToAction("Index");
                }

                docModel.DocumentId = mod.DocumentID;
                docModel.ContractId = ContractID;
                docModel.MerchantId = CurrentMerchantID;
                docModel.DocumentTypeId = mod.DocumentTypeID;
                docModel.FileName = fileName;
                docModel.FileDetails = fileType;
                docModel.UploadUserId = CurrentUserID;

                if (mod.IsPending)
                {
                    docModel.StatusId = (long)StatusTypes.DocPending;
                }
                else
                {
                    docModel.StatusId = (long)StatusTypes.DocUploaded;
                }
                documentsApi.UpdateDocuments(docModel);

            }

            //var model = new ScanDocumentModel();
            //mod.DocumentTypes = GetDocTypes();

            //return View("ScanDocument", mod);
        }

        public JsonResult IsDocPending(int docType)
        {
            var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, docType);
            if (doc.Count > 0)
            {
                return Json(doc[0].StatusId == (long)StatusTypes.DocPending, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetDocList(int docType)
        {
            var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, docType);
            return PartialView("_ListDocs", doc);
        }

        public ActionResult GetDocListModal(int docType, long merchantId, long contractId)
        {
            var doc = documentsApi.RetriveDocument(merchantId, contractId, docType);
            return PartialView("_ListDocsModal", doc);
        }

        public ActionResult GetImage(int docType)
        {

            var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, docType);
            if (Request.IsAjaxRequest())
            {
                //return Json(new
                //{
                //    FilePath = "/ScanDocuments/" + doc.FileName,
                //    DocumentID = doc.DocumentId,
                //    DocStatus = (doc.StatusId == (long)StatusTypes.DocPending)
                //    //DocStatus="Pending"
                //},
                //JsonRequestBehavior.AllowGet);
                return Json(doc, JsonRequestBehavior.AllowGet);

            }
            return View();

        }

        [HttpPost]
        public ActionResult Complete()
        {
            //Validate 
            if (ValidateComplete())
            {

                contractApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.CWScanDocument, ContractID);

                base.SetSuccessMessage("Scan Doc - Task Completed");
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "VerificationCall") });
                }
                return RedirectToAction("Index", "VerificationCall");
            }
            else
            {
                base.SetErrorMessage("Please upload Legal document of company.");
                if (Request.IsAjaxRequest())
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "DocumentScan") });
                }
                return RedirectToAction("Index", "DocumentScan");
            }
        }

        private bool ValidateComplete()
        {
            var rv = false;
            var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, (int)Pecuniaus.Contract.DocumentTypes.LegalDocumentsOfTheCompany);

            foreach (var d in doc)
            {
                if (!string.IsNullOrEmpty(d.FileName))
                {
                    rv = true;
                }
            }
            return rv;
        }

        private IEnumerable<SelectListItem> GetDocTypes()
        {
            var docTypes = contractApi.GetDocument(WorkflowID);
            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.DocumentName,
                       Value = c.DocumentTypeId.ToString()
                   };
        }


        public ActionResult GetDocListUpload(int docType)
        {
            var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, docType);
            return PartialView("_ListDocsUpload", doc);
        }
        [HttpPost]
        public void UploadDoc(int docTypeId, int docId, HttpPostedFileBase file)
        {
            if (docTypeId > 0)
            {
                var docModel = new DocumentModel();
                string fileName = "";
                string fileType = "";
                if (file != null && file.ContentLength > 0)
                {
                    fileName = "doc_" + docId + docTypeId + "_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                    file.SaveAs(path);
                    fileType = file.ContentType;
                }

                docModel.DocumentId = docId;
                docModel.ContractId = ContractID;
                docModel.MerchantId = CurrentMerchantID;
                docModel.DocumentTypeId = docTypeId;
                docModel.FileName = fileName;
                docModel.FileDetails = fileType;
                docModel.UploadUserId = CurrentUserID;

                docModel.StatusId = (long)StatusTypes.DocUploaded;
                documentsApi.UpdateDocuments(docModel);
            }
        }
    }
}