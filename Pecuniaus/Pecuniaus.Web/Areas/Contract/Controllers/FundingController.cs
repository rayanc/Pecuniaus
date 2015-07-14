using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using Pecuniaus.UICore;

namespace Pecuniaus.Contract.Controllers
{
    public class FundingController : BaseController
    {

        #region local properties
        RenewalApi renewaltApi;
        ContractApi contractApi;
        public const int WireTransferDocumentTypeId = 17;
        #endregion

        # region Constructor
        public FundingController()
        {
            contractApi = new ContractApi();
            //  renewaltApi = new RenewalApi();

        }
        #endregion

        public ActionResult Index()
        {
            var query = string.Format("contracts/GetFundingDetails?contractId={0}&merchantId={1}", ContractID, CurrentMerchantID);
            FundingModel funding = BaseApiData.GetAPIResult<FundingModel>(query, () => new FundingModel());

            if (!string.IsNullOrEmpty(funding.fileName))
                funding.filePath = "/ScanDocuments/" + funding.fileName;

            //ContractApi contractApi = new ContractApi();
            var docTypes = contractApi.GetBankNames();
            funding.Banklist = from c in docTypes
                               select new SelectListItem
                               {
                                   Text = c.BankName,
                                   Value = c.BankId.ToString()
                               };
            if (funding.contractStatusId == 20007)
            {
                funding.contractFunded = true;
                funding.contractReviewed = true;
            }
            else if (funding.contractStatusId == 20004)
            {
                funding.contractReviewed = true;
            }

            Session["Banklist"] = funding.Banklist;
            return View(funding);
        }

        [HttpPost]
        public ActionResult Index(FundingModel model, HttpPostedFileBase file, string Command)
        {
            if (ModelState.IsValid)
            {
                model.ContractId = (int)ContractID;
                model.MerchantId = (int)CurrentMerchantID;
                model.accountName = string.IsNullOrEmpty(model.accountName) ? string.Empty : model.accountName;
                model.action = Command;
                model.UserId = base.CurrentUserID;

                UploadDocuments(model, file);
                base.SetSuccessMessage("Data Updated.");
                if (Command == "complete")
                {
                    if (model.documentId == 0)
                    {
                        base.SetErrorMessage("Can not complete the task, Please Upload proof of Wire Transfer.");
                        return RedirectToAction("Index", "Funding");
                    }
                    else if (!model.contractFunded || !model.contractReviewed)
                    {
                        base.SetErrorMessage("Can not complete the task, Please select Funded and Review checkboxes.");
                        return RedirectToAction("Index", "Funding");
                    }
                }

                var apiMethod = string.Format("contracts/funded");
                var response = BaseApiData.PostAPIData(apiMethod, model);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    base.SetSuccessMessage("Data Updated.");
                    if (Command == "complete")
                    {
                        base.SetSuccessMessage("Task Completed.");
                        return RedirectToAction("Index", "FinalVerification");
                    }
                    else
                    {
                        base.SetSuccessMessage("Data Updated.");
                        return RedirectToAction("Index", "Funding");
                    }
                }
                else
                {
                    base.SetErrorMessage("Update Failure");
                    return RedirectToAction("Index");
                }
            }
            if (Session["Banklist"] != null)
                model.Banklist = (IEnumerable<SelectListItem>)Session["Banklist"];
            return View(model);
        }
        private void UploadDocuments(FundingModel mod, HttpPostedFileBase file)
        {
            var docModel = new DocumentModel();
            string fileName = "";
            string fileType = "";


            if (file != null && file.ContentLength > 0)
            {
                fileName = "doc_" + mod.documentId + WireTransferDocumentTypeId + "_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                file.SaveAs(path);
                fileType = file.ContentType;

                docModel.DocumentId = mod.documentId;
                docModel.ContractId = ContractID;
                docModel.MerchantId = CurrentMerchantID;
                docModel.DocumentTypeId = WireTransferDocumentTypeId;
                docModel.FileName = fileName;
                docModel.FileDetails = fileType;
                docModel.UploadUserId = CurrentUserID;

                if (mod.documentId > 0)
                {
                    ApiHelper.BaseApiData.PutAPIData<DocumentModel>("documents/UpdateDocuments", docModel);
                }
                else
                {
                    ApiHelper.BaseApiData.PutAPIData<DocumentModel>("documents/InsertContDocument", docModel);
                    mod.documentId = 999999;
                }
            }

        }
    }
}