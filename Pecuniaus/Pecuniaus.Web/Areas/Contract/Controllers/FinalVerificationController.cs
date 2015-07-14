using Pecuniaus.Contract.Models;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using System.Web;
using System.Net;
using Pecuniaus.Models.Contract;
using System.IO;
using Pecuniaus.UICore;

namespace Pecuniaus.Contract.Controllers
{
    public class FinalVerificationController : BaseController
    {
        # region Local Variable
        //private RenewalApi renewalApi;
        public const int LegalDocumentTypeId = 19;
        public const int BLADocumentTypeId = 18;

        #endregion

        //
        // GET: /FinalVerification/
        public ActionResult Index()
        {
            var query = string.Format("contracts/RetrieveFinalValidation?contractId={0}&merchantId={1}", ContractID, CurrentMerchantID);
            FinalVerificationModel model = BaseApiData.GetAPIResult<FinalVerificationModel>(query, () => new FinalVerificationModel());

            //model.BLAFile = string.Format("Cont_{0}_{1}.pdf", CurrentMerchantID, ContractID);
            if (!string.IsNullOrEmpty(model.legalFile))
                model.legalFilePath = "/ScanDocuments/" + model.legalFile;

            if (!string.IsNullOrEmpty(model.BLAFile))
                model.BLAFilePath = "/ScanDocuments/" + model.BLAFile;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FinalVerificationModel model, HttpPostedFileBase file,HttpPostedFileBase BLAfile ,string Command)
        {

            if (ModelState.IsValid && Command == "complete")
            {
                model.contractId = (int)ContractID;
                model.merchantId = (int)CurrentMerchantID;
                model.accountName = string.IsNullOrEmpty(model.accountName) ? string.Empty : model.accountName;
                model.action = Command;
                UploadDocuments(model, file, LegalDocumentTypeId, model.documentId);
                UploadDocuments(model, BLAfile, BLADocumentTypeId, model.BLADocumentId);

                if (model.documentId == 0 )
                {
                    base.SetErrorMessage("Can not complete the task, Legal document is not uploaded");
                    return RedirectToAction("Index");
                
                }
                else if( model.BLADocumentId == 0)
                {
                    base.SetErrorMessage("Can not complete the task, BLA document is not uploaded");
                    return RedirectToAction("Index");
                }
                var apiMethod = string.Format("contracts/CompleteFinalValidation/{0}/action/{1}", base.ContractID, Command);
                var response = BaseApiData.PutAPIData(apiMethod, null);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    base.SetSuccessMessage("Task Completed.");
                }
                else
                {
                    base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPOwnerUpdateFailure);                    
                }
            }
            else if (ModelState.IsValid && Command == "save")
            {
                UploadDocuments(model, file, LegalDocumentTypeId, model.documentId);
                UploadDocuments(model, BLAfile, BLADocumentTypeId, model.BLADocumentId);
                base.SetSuccessMessage("Data Updated.");
                
            }
            return RedirectToAction("Index");
            //return View(model);
        }

        private void UploadDocuments(FinalVerificationModel mod, HttpPostedFileBase file, int documentTypeId, long documentId)
        {
            var docModel = new DocumentModel();
            string fileName = "";
            string fileType = "";


            if (file != null && file.ContentLength > 0)
            {
                fileName = "doc_" + documentId + documentTypeId + "_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                file.SaveAs(path);
                fileType = file.ContentType;


                docModel.DocumentId = documentId;
                docModel.ContractId = ContractID;
                docModel.MerchantId = CurrentMerchantID;
                docModel.DocumentTypeId = documentTypeId;
                docModel.FileName = fileName;
                docModel.FileDetails = fileType;
                docModel.UploadUserId = CurrentUserID;
                docModel.StatusId = (long)StatusTypes.DocUploaded;
         
                if (mod.documentId > 0)
                {
                    ApiHelper.BaseApiData.PutAPIData<DocumentModel>("documents/UpdateDocuments", docModel);
                }
                else
                {
                    ApiHelper.BaseApiData.PutAPIData<DocumentModel>("documents/InsertContDocument", docModel);
                    if (documentTypeId == BLADocumentTypeId)
                    {
                        mod.BLADocumentId= 99999;
                    }
                    else
                    mod.documentId = 99999;
                }
            }

        }
    }
}