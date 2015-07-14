using Pecuniaus.ApiHelper;
using Pecuniaus.Contract.Models;
using Pecuniaus.Models;
using Pecuniaus.UICore.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Pecuniaus.Models.Contract;
using Pecuniaus.Contract.Repository;
using System;
using System.Web;

namespace Pecuniaus.Contract.Controllers
{
    public class BaseDEController : BaseController
    {
        protected OwnerSesssionRepository ownerSesssionRepository;
        protected ProcessorSessionRepository processorSessionRepository;
        protected TradeReferenceSessionRepository traderSessionRepository;
        protected BankStatementSessionRepository bankStatementRepository;
        public BaseDEController()
        {
            ownerSesssionRepository = new OwnerSesssionRepository();
            processorSessionRepository = new ProcessorSessionRepository();
            traderSessionRepository = new TradeReferenceSessionRepository();
            bankStatementRepository = new BankStatementSessionRepository();

        }
        #region  Owners
        [HttpGet]
        public ActionResult AddOwner()
        {
            return PartialView("_AddOwner", new OwnerModel
            {
                OwnerDOB = DateTime.Now.AddYears(-10),
                States = GetProvince()
            });
        }

        [HttpPost]
        public ActionResult AddOwner(OwnerModel model)
        {
            if (ModelState.IsValid)
            {
                ownerSesssionRepository.AddOwner(model);
                return RedirectToAction("AddOwner");
            }
            model.States = GetProvince();
            return PartialView("_AddOwner", model);
        }

        [HttpGet]
        public ActionResult EditOwner(long id)
        {
            var model = ownerSesssionRepository.GetByID(id);
            model.States = GetProvince();
            model.IsUpdate = true;
            return PartialView("_AddOwner", model);
        }

        [HttpPost]
        public ActionResult UpdateOwner(OwnerModel model)
        {
            if (ModelState.IsValid)
            {
                ownerSesssionRepository.Update(model);
                return RedirectToAction("AddOwner");
            }
            model.States = GetProvince();
            return PartialView("_AddOwner", model);
        }

        [HttpPost]
        public ActionResult DelOwner(long id)
        {
            ownerSesssionRepository.DelOwner(id);

            return Json("OK");
        }

        public ActionResult ListOwners()
        {
            var owners = ownerSesssionRepository.GetAll();
            return PartialView("_ListOwners", owners);
        }
        #endregion

        #region Processor

        [HttpGet]
        public ActionResult AddProcessor()
        {
            return PartialView("_AddProcessor", new ProcessorModel
            {
                FirstProcessedDate = DateTime.Now,
                ProcessorTypes = GetProcessors()
            });
        }

        [HttpPost]
        public ActionResult AddProcessor(ProcessorModel model)
        {
            if (ModelState.IsValid)
            {
                processorSessionRepository.Add(model);
                return RedirectToAction("AddProcessor");
            }
            model.ProcessorTypes = GetProcessors();
            return PartialView("_AddProcessor", model);
        }



        public ActionResult ListProcessors()
        {
            List<ProcessorModel> processors = processorSessionRepository.GetAll();
            return PartialView("_ListProcessors", processors);
        }

        [HttpGet]
        public ActionResult EditProcessor(long id)
        {
            var model = processorSessionRepository.GetByID(id);
            model.ProcessorTypes = GetProcessors();
            return PartialView("_EditProcessor", model);
        }

        [HttpPost]
        public ActionResult EditProcessor(ProcessorModel model)
        {
            if (ModelState.IsValid)
            {
                processorSessionRepository.Update(model);
                return RedirectToAction("AddProcessor");
            }
            model.ProcessorTypes = GetProcessors();
            return PartialView("_EditProcessor", model);
        }

        [HttpPost]
        public ActionResult DelProcessor(long id)
        {
            processorSessionRepository.Delete(id);

            return Json("OK");
        }
        #endregion

        #region Trade Reference

        [HttpGet]
        public ActionResult AddTradeReference()
        {
            return PartialView("_AddTradeReference", new TradeReferenceModel { });
        }

        [HttpPost]
        public ActionResult AddTradeReference(TradeReferenceModel model)
        {
            if (ModelState.IsValid)
            {
                traderSessionRepository.Add(model);
                return RedirectToAction("AddTradeReference");
            }

            return PartialView("_AddTradeReference", model);
        }



        public ActionResult ListTradeReference()
        {
            List<TradeReferenceModel> traderef = traderSessionRepository.GetAll();
            return PartialView("_ListTradeReference", traderef);
        }

        [HttpGet]
        public ActionResult EditTradeReference(long id)
        {
            var model = traderSessionRepository.GetByID(id);
            return PartialView("_EditTradeReference", model);
        }

        [HttpPost]
        public ActionResult EditTradeReference(TradeReferenceModel model)
        {
            if (ModelState.IsValid)
            {
                traderSessionRepository.Update(model);
                return RedirectToAction("AddTradeReference");
            }
            return PartialView("_EditTradeReference", model);
        }

        [HttpPost]
        public ActionResult DelTradeReference(long id)
        {
            traderSessionRepository.Delete(id);

            return Json("OK");
        }
        #endregion

        #region Bank Statement

        [HttpGet]
        public ActionResult AddStatement()
        {

            return PartialView("_AddBankStatement", new BankStatementModel
            {
                StatementMonths = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetMonthNames(),
                StatementYears = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetYearNames()

            });
        }

        [HttpPost]
        public ActionResult AddStatement(BankStatementModel model)
        {
            if (ModelState.IsValid)
            {
                bankStatementRepository.Add(model);
                return RedirectToAction("AddStatement");
            }

            model.StatementMonths = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetMonthNames();
            model.StatementYears = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetYearNames();
            return PartialView("_AddBankStatement", model);
        }

        public ActionResult ListStatement()
        {
            List<BankStatementModel> statement = bankStatementRepository.GetAll();
            return PartialView("_ListBankStatement", statement);
        }


        [HttpGet]
        public ActionResult EditStatement(long id)
        {
            var model = bankStatementRepository.GetByID(id);
            model.StatementMonths = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetMonthNames();
            model.StatementYears = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetYearNames();
            return PartialView("_EditBankStatement", model);
        }

        [HttpPost]
        public ActionResult EditStatement(BankStatementModel model)
        {
            if (ModelState.IsValid)
            {
                bankStatementRepository.Update(model);
                model.StatementMonths = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetMonthNames();
                model.StatementYears = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetYearNames();
                return RedirectToAction("AddStatement");
            }
            return PartialView("_EditBankStatement", model);
        }

        [HttpPost]
        public ActionResult DelStatement(long id)
        {
            bankStatementRepository.Delete(id);

            return Json("OK");
        }
        #endregion

        protected IEnumerable<SelectListItem> GetBanks()
        {
            ContractApi contractApi = new ContractApi();
            IEnumerable<BankNameModel> docTypes = contractApi.GetBankNames();

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.BankName,
                       Value = c.BankId.ToString()
                   };
        }

        public ActionResult BankInfoVer()
        {
            var bankInfo = new ContractApi().GetBankDetails(1, ContractID, CurrentMerchantID);
            bankInfo.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.BankStatements;

            bankInfo.Banks = GetBanks();

            return PartialView("_VBankInfo", bankInfo);
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
            return PartialView("_VBankInfo", model);
        }

        public ActionResult LandLordVer()
        {
            var landLord = new ContractApi().GetLandLordVerification(ContractID, CurrentMerchantID);
            landLord.ScriptFilePath = Url.Content(GetLandLordScriptFilePath());
            return PartialView("_VLandLord", landLord);
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

            return PartialView("_VCorpDoc", corpDoc);
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

            return PartialView("_VCommName", commercialName);
        }
        [HttpPost]
        public ActionResult CommNameVer(CommercialNameVeriModel model)
        {
            if (ModelState.IsValid)
            {
               new  ContractApi().UpdateCommercialDetails(CurrentMerchantID, model);
                SetSuccessMessage("Updated.");

                return RedirectToAction("CommNameVer");
            }
            model.States = GetProvince();
            model.DocTypeId = (long)Pecuniaus.Contract.DocumentTypes.CommercialNameVerificationScreenshot;
            return View("_VCommName", model);
        }

        private string GetLandLordScriptFilePath()
        {
            return "~/Uploads/LandLordCall/";
        }

        public ActionResult VerificationTask()
        {

            return PartialView("_VerificationTask");
        }
    
    }
}