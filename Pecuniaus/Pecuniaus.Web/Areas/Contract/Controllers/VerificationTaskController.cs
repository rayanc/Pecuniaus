using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using Pecuniaus.Contract.Repository;
using Pecuniaus.UICore.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.UICore;
using Pecuniaus.AudioConvertor;

namespace Pecuniaus.Contract.Controllers
{
    public class VerificationTaskController : BaseController
    {
        ContractApi contractApi;
        OwnerSesssionRepository _OwnerCorpSessionRepository;
        public VerificationTaskController()
        {
            contractApi = new ContractApi();
            _OwnerCorpSessionRepository = new OwnerSesssionRepository();
        }

        private const string Entity = "LandLord";


        // GET: /VerificationTask/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /VerificationTask/
        public ActionResult BankInformation()
        {
            var model = new BankInfoVerificationModel();
            model = contractApi.GetBankDetails(0, ContractID, CurrentMerchantID);
            model.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.BankStatements;
            model.merchantId = CurrentMerchantID;
            model.contractId = ContractID;
            model.Banks = GetBanks();
            return View(model);
        }

        private ActionResult CompleteVeriTask()
        {
            string err;
            if (IsValidData(out err))
            {
                contractApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.CWVerificationTask, ContractID);

                SetSuccessMessage("Task Completed.");
                return RedirectToAction("Index", "ReviewTask");
            }
            else
            {
                SetSuccessMessage("");
                if (string.IsNullOrEmpty(err))
                    SetErrorMessage("Cann't complete task. Please fill data in all screens.");
                else
                    SetErrorMessage("Cann't complete task. " + err);
            }

            return RedirectToAction("BankInformation", "VerificationTask");

        }

        private bool IsValidData(out string errMsg)
        {
            var documentsApi = new DocumentsApi();
            var rv = false;
            errMsg = string.Empty;
            var bankValidated = false;

            var bankModel = contractApi.GetBankDetails(0, ContractID, CurrentMerchantID);

            if (!string.IsNullOrEmpty(bankModel.AccountName) && !string.IsNullOrEmpty(bankModel.AccountNumber))
            {
                var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, (int)Pecuniaus.Contract.DocumentTypes.BankStatements);

                if (doc == null || doc.Count == 0)
                {
                    errMsg = "Bank Statement not uploaded.";
                }
                else
                {
                    bankValidated = true;
                }
            }

            var corpValiidated = false;

            if (bankValidated)
            {
                var corpModel = contractApi.GetCorporateDocumemts(ContractID, CurrentMerchantID);
                if (!string.IsNullOrEmpty(corpModel.NameOfCompany) && !string.IsNullOrEmpty(corpModel.RNCNumber))
                {
                    var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, (int)Pecuniaus.Contract.DocumentTypes.LegalDocumentsOfTheCompany);

                    if (doc == null || doc.Count == 0)
                    {
                        errMsg = "Corp Doc. not uploaded.";
                    }
                    else
                    {
                        corpValiidated = true;
                    }

                }

            }
            var commValidated = false;

            if (corpValiidated)
            {
                var commercialModel = contractApi.GetCommercialNameVerification(ContractID, CurrentMerchantID);
                if (!string.IsNullOrEmpty(commercialModel.BusinessName))
                {
                    var doc = documentsApi.RetriveDocument(CurrentMerchantID, ContractID, (int)Pecuniaus.Contract.DocumentTypes.CommercialNameVerificationScreenshot);

                    if (doc == null || doc.Count == 0)
                    {
                        errMsg = "Commertial Name Doc. not uploaded.";
                    }
                    else
                    {
                        commValidated = true;
                    }

                }
            }
            if (commValidated)
            {
                var llModel = contractApi.GetLandLordVerification(ContractID, CurrentMerchantID);
                if (llModel.MerchantDetails.RentFlag == 200001)
                {
                    if (string.IsNullOrEmpty(llModel.ScriptFile))
                    {
                        errMsg = "Landlord Script file not uploaded.";

                    }
                    else
                    {
                        rv = true;
                    }
                }
                else
                {
                    rv = true;
                }

            }
            return rv;
        }

        //POST METHODF 
        [HttpPost]
        public ActionResult BankInformation(BankInfoVerificationModel model, string button)
        {
            if (ModelState.IsValid)
            {
                model.merchantId = CurrentMerchantID;
                model.contractId = ContractID;

                var apiMethod = string.Format("contracts/UpdateBankDetails?isCompleted={0}", 0);
                BaseApiData.PutAPIData(apiMethod, model);
                base.SetSuccessMessage("Information Updated");
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { Data = "OK" };

                }
                if (button == "Complete")
                {
                    return CompleteVeriTask();
                }
                return RedirectToAction("BankInformation");
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = "Err" };
            }
            return View();
        }

        public JsonResult FilBankInformation(int BankId)
        {
            var model = contractApi.GetBankDetails(BankId, ContractID, CurrentMerchantID);

            return Json(model, JsonRequestBehavior.AllowGet);

            return new JsonResult { Data = null };

        }

        public ActionResult CorporateDocuments()
        {
            var model = contractApi.GetCorporateDocumemts(ContractID, CurrentMerchantID);
            model.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.LegalDocumentsOfTheCompany;

            _OwnerCorpSessionRepository.Set(model.OwnerList);
            return View(model);
        }

        [HttpPost]
        public ActionResult CorporateDocuments(CorporateDocVerificationModel model, string button)
        {
            List<OwnerModel> ownerList = new List<OwnerModel>();
            ownerList = _OwnerCorpSessionRepository.GetAll();
            contractApi.UpdateCommercialOwnerDetails(CurrentMerchantID, ContractID, ownerList);
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = "OK" };
            }

            if (button == "Complete")
            {
                return CompleteVeriTask();
            }
            base.SetSuccessMessage("Updated");
            return RedirectToAction("CorporateDocuments");

        }

        //Corporate Partail View for Add
        [HttpGet]
        public ActionResult AddCorpDoc()
        {
            //  return PartialView("_AddCorpDoc", new OwnerCorpModel { });
            return PartialView("_AddCorpDoc", new OwnerModel
            {
                States = GetProvince()
            });
        }

        [HttpPost]
        public ActionResult AddCorpDoc(OwnerModel _OwnerCorpModel)
        {
            if (ModelState.IsValid)
            {
                _OwnerCorpSessionRepository.AddOwner(_OwnerCorpModel);
                return RedirectToAction("AddCorpDoc");
            }
            _OwnerCorpModel.States = GetProvince();

            return PartialView("_AddCorpDoc", _OwnerCorpModel);
        }

        //Corporate Edit Method
        [HttpGet]
        public ActionResult UpdateCorpDoc(long id)
        {
            //var m = new MerchantProfileApi().GetOwnerDetails(CurrentMerchantID, id);

            //OwnerCorpModel _OwnerCorpModel = new OwnerCorpModel {
            //    IsUpdate = true,
            //    OwnerId = m.Owner.OwnerId,
            //    OwnerLastName = m.Owner.LastName,
            //    OwnerName =m.Owner.FirstName,
            //    IsAuthorized=m.Owner.IsAuthorized,
            //};
            var _OwnerCorpModel = _OwnerCorpSessionRepository.GetByID(id);
            _OwnerCorpModel.IsUpdate = true;
            _OwnerCorpModel.States = GetProvince();
            return PartialView("_AddCorpDoc", _OwnerCorpModel);
        }

        [HttpPost]
        public ActionResult UpdateCorpDoc(OwnerModel _OwnerCorpModel)
        {
            if (ModelState.IsValid)
            {
                _OwnerCorpSessionRepository.Update(_OwnerCorpModel);
                return RedirectToAction("AddCorpDoc");
            }

            _OwnerCorpModel.States = GetProvince();

            return PartialView("_AddCorpDoc", _OwnerCorpModel);
        }

        //Corporate Partail View for Listing
        public ActionResult ListCorpDoc()
        {
            //  var model = contractApi.GetCorporateDocumemts(ContractID, CurrentMerchantID);
            return PartialView("_ListCorpDoc", _OwnerCorpSessionRepository.GetAll());
        }

        public ActionResult CommercialName()
        {
            var commercial = contractApi.GetCommercialNameVerification(ContractID, CurrentMerchantID);
            commercial.States = GetProvince();
            commercial.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.CommercialNameVerificationScreenshot;

            return View(commercial);
        }

        [HttpPost]
        public ActionResult CommercialName(CommercialNameVeriModel model, string button)
        {

            if (ModelState.IsValid)
            {
                contractApi.UpdateCommercialDetails(CurrentMerchantID, model);

                if (button == "Complete")
                {
                    return CompleteVeriTask();
                }
                SetSuccessMessage("Updated.");
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { Data = "OK" };
                }
                return RedirectToAction("CommercialName");
            }

            model.States = GetProvince();
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = "OK" };
            }
            return View(model);
        }

        public ActionResult Landlord()
        {
            var model = contractApi.GetLandLordVerification(ContractID, CurrentMerchantID);
            //if (questions.questions == null)
            //    questions.questions = new List<QuestionModel>();
            model.UserFullName = base.GetUserFullName();
            model.ScriptFilePath = Url.Content(GetLLScriptFilePath());
            return View(model);
        }

        [HttpPost]
        public ActionResult LandLord(LandLordVeriModel model, HttpPostedFileBase file, string button)
        {
            if (file != null && file.ContentLength > 0)
            {
                var tempFile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                file.SaveAs(tempFile);

                //var fileName = "LLCal_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                var fileName = "LLCal_" + CurrentMerchantID + "_" + ContractID + ".mp3";
                var fullPath = Path.Combine(Server.MapPath(GetLLScriptFilePath()), fileName);
                model.ScriptFile = fileName;
                AudioHelper.ConvertToMP3(tempFile, fullPath);

            }
            contractApi.UpdateLandlordVerification(model, base.ContractID, false);

            if (button == "Complete")
            {
                return CompleteVeriTask();
            }
            SetSuccessMessage("Updated.");
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = "OK" };
            }
            return RedirectToAction("Landlord");
        }

        #region Helper Methods
        private IEnumerable<SelectListItem> GetBanks()
        {
            var docTypes = contractApi.GetBankNames();
            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.BankName,
                       Value = c.BankId.ToString()
                   };
        }

        #endregion

        #region PartialViews
        public ActionResult BankInfoVer()
        {
            var bankInfo = new ContractApi().GetBankDetails(1, ContractID, CurrentMerchantID);
            bankInfo.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.BankStatements;

            bankInfo.Banks = GetBanks();

            return PartialView("_BankInfo", bankInfo);
        }

        [HttpPost]
        public ActionResult BankInfoVer(BankInfoVerificationModel model)
        {
            if (ModelState.IsValid)
            {
                model.merchantId = CurrentMerchantID;
                model.contractId = ContractID;

                var apiMethod = string.Format("contracts/UpdateBankDetails?isCompleted={0}", 0);
                BaseApiData.PutAPIData(apiMethod, model);
                base.SetSuccessMessage("Information Updated");
                return RedirectToAction("BankInfoVer");
            }
            model.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.BankStatements;
            model.Banks = GetBanks();
            return PartialView("_BankInfo", model);
        }

        public ActionResult LandLordVer()
        {
            var landLord = new ContractApi().GetLandLordVerification(ContractID, CurrentMerchantID);
            landLord.ScriptFilePath = Url.Content(GetLandLordScriptFilePath());
            return PartialView("_LandLord", landLord);
        }

        [HttpPost]
        public ActionResult LandLordVer(LandLordVeriModel model, HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                var fileName = "LLCal_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(GetLLScriptFilePath()), fileName);
                file.SaveAs(path);
                model.ScriptFile = fileName;
            }
            new ContractApi().UpdateLandlordVerification(model, base.ContractID, false);

            SetSuccessMessage("Updated.");

            return RedirectToAction("LandLordVer");

        }

        public ActionResult CorpDocVer()
        {
            var corpDoc = new ContractApi().GetCorporateDocumemts(ContractID, CurrentMerchantID);
            corpDoc.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.LegalDocumentsOfTheCompany;
            new OwnerSesssionRepository().Set(corpDoc.OwnerList);

            return PartialView("_CorpDocs", corpDoc);
        }

        [HttpPost]
        public ActionResult CorpDocVer(CorporateDocVerificationModel model)
        {

            List<OwnerModel> ownerList = new List<OwnerModel>();
            ownerList = new OwnerSesssionRepository().GetAll();
            new ContractApi().UpdateCommercialOwnerDetails(CurrentMerchantID, ContractID, ownerList);

            base.SetSuccessMessage("Updated");
            return RedirectToAction("CorpDocVer");

        }

        public ActionResult CommNameVer()
        {
            var commercialName = new ContractApi().GetCommercialNameVerification(ContractID, CurrentMerchantID);
            commercialName.States = GetProvince();
            commercialName.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.CommercialNameVerificationScreenshot;

            return PartialView("_CommertialName", commercialName);
        }

        [HttpPost]
        public ActionResult CommNameVer(CommercialNameVeriModel model)
        {
            if (ModelState.IsValid)
            {
                new ContractApi().UpdateCommercialDetails(CurrentMerchantID, model);
                SetSuccessMessage("Updated.");

                return RedirectToAction("CommNameVer");
            }
            model.States = GetProvince();
            model.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.CommercialNameVerificationScreenshot;
            return View("_CommertialName", model);
        }

        private string GetLandLordScriptFilePath()
        {
            return "~/Uploads/LandLordCall/";
        }

        #endregion
    }
}