using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Pecuniaus.Contract.Models;
using Pecuniaus.Contract.Repository;
using Pecuniaus.UICore;

namespace Pecuniaus.Contract.Controllers
{
    public class ReviewTaskController : BaseDEController
    {
        ContractApi contractApi;
        MerchantApi merchantApi;

        //OwnerCorpSessionRepository _OwnerCorpSessionRepository;

        public ReviewTaskController()
        {
            contractApi = new ContractApi();
            merchantApi = new MerchantApi();
      //      _OwnerCorpSessionRepository = new OwnerCorpSessionRepository();

        }

        private string GetScriptFilePath()
        {
            return "~/Uploads/VerifyCall/";
        }

      
        //
        // GET: /ReviewTask/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string button)
        {
            contractApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.CWReview, ContractID);
            base.SetSuccessMessage("Task Completed");
            return RedirectToAction("Index");

        }

        public ActionResult Documents()
        {
            var model = new ScanDocumentModel();
            var docTypes = contractApi.GetDocument(WorkflowID);
            model.DocumentTypes = from c in docTypes
                                  select new SelectListItem
                                  {
                                      Text = c.DocumentName,
                                      Value = c.DocumentTypeId.ToString()
                                  };
            return PartialView("_documents", model);
        }

        public ActionResult VerificationCall()
        {
            var model = contractApi.GetVerificationCall(ContractID, CurrentMerchantID);
            model.AdvanceTypes = base.GetTypeOfAdvances("");
            model.ScriptFilePath = Url.Content(GetScriptFilePath());
            return PartialView("_VerificationCall", model);
        }

        #region DataEntry
        public ActionResult DataEntry()
        {
            var model = merchantApi.Info(CurrentMerchantID);

            ownerSesssionRepository.Set(model.Owners);
            processorSessionRepository.Set(model.Processor);
            traderSessionRepository.Set(model.TradeReference);
            bankStatementRepository.Set(model.BankStatements);

            model.StatementMonths = Utilities.Common.GetMonthNames();
            model.StatementYears = Utilities.Common.GetYearNames();
            model.IndustryTypes = GetIndustryTypes();
            model.Provinces = GetProvince();
            model.ProcessorCompar = GetProcessors();
            model.TypeOfAdvances = GetTypeOfAdvances();
            model.TypeOfProperties = GetPropertyTypes();
            model.BusinessType = GetBusinessTypes();
            model.Banks = GetBanks();
            IEnumerable<SalesRepresentativeModel> SalesRep = RetrieveSalesRep();
            model.SalesReps = new SelectList(SalesRep, "salesRepId", "fullName");

            return PartialView("_DataEntry", model);
        }



        #endregion

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

    }
}