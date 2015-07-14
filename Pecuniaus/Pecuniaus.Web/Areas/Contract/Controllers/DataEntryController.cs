using Pecuniaus.ApiHelper;
using Pecuniaus.Contract.Repository;
using Pecuniaus.Models.Contract;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Pecuniaus.Contract.Models;
using System.Linq;
using System.Web;
using System.IO;
using Pecuniaus.UICore;

namespace Pecuniaus.Contract.Controllers
{
    public class DataEntryController : BaseDEController
    {
        MerchantApi merchantApi;
        public DataEntryController()
        {
            merchantApi = new MerchantApi();
        }
        //
        // GET: /DataEntry/
        public ActionResult Index()
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
            return View(model);
        }

        public FileResult Download(string file)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath("~/ScanDocuments/"), file));
           // var response = new FileContentResult(fileBytes, "application/octet-stream");
            var response = new FileContentResult(fileBytes, "application/vnd.ms-excel");
            response.FileDownloadName = "AnnualSales_Calc" + CurrentMerchantID + "_" + ContractID;
            return response;
        }

        [HttpPost]
        public ActionResult Index(DataEntryModel model, string button, HttpPostedFileBase file)
        {
            var isComplete = 0;
            if (button == "Complete")
            {
                isComplete = 1;
            }

            string fileName = "";
            string fileType = "";
            if (file != null && file.ContentLength > 0)
            {
                fileName = "AnnualSalesCalc_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ScanDocuments/"), fileName);
                file.SaveAs(path);
                fileType = file.ContentType;
                model.AnnualSalesCalcFile = fileName;
            }

            model.LandlordInformation = new LandlordInformationModel
            {
                LandlordId = model.LandlordId,
                CompanyName = model.CompanyName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                MerchantId = CurrentMerchantID
            };

            //var errorList = new List<string>();
            //foreach (var key in ModelState.Keys)
            //{
            //    ModelState modelState = null;
            //    if (ModelState.TryGetValue(key, out modelState))
            //    {
            //        foreach (var error in modelState.Errors)
            //        {
            //            errorList.Add(
            //                key + "==" + error.ErrorMessage
            //           );
            //        }
            //    }
            //}

            //model.TypeOfPropertyID = 1;
            if (ModelState.IsValid)
            {
                ///Save Data      
                model.Owners = ownerSesssionRepository.GetAll();
                model.Processor = processorSessionRepository.GetAll();
                model.BankStatements = bankStatementRepository.GetAll();
                model.TradeReference = traderSessionRepository.GetAll();

             
                var apiMethod = string.Format("merchants/dataentry/{0}?isCompleted={1}", CurrentMerchantID, isComplete);
                BaseApiData.PutAPIData(apiMethod, model);

                if (isComplete == 1)
                {
                    string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
                    CurrentMerchantID, (int)TaskTypes.CWDataEntry, 2, ContractID);
                    BaseApiData.GetAPIResult<object>(apiData, () => new object());

                    base.SetSuccessMessage("Data Entry - Task Completed.");
                    //return RedirectToAction("BankInformation", "VerificationTask");
                }
                else
                {
                    base.SetSuccessMessage("Data Updated.");
                    if (Request.IsAjaxRequest())
                    {
                        return new JsonResult { Data = "OK" };
                    }
                }
                return RedirectToAction("Index");
            }


            model.IndustryTypes = GetIndustryTypes();
            model.Provinces = GetProvince();
            model.ProcessorCompar = GetProcessors();
            model.TypeOfProperties = GetPropertyTypes();
            model.BusinessType = GetBusinessTypes();
            model.TypeOfAdvances = GetTypeOfAdvances();
            model.Banks = GetBanks();
            model.StatementMonths = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetMonthNames();
            model.StatementYears = (IEnumerable<SelectListItem>)Pecuniaus.Utilities.Common.GetYearNames();
            IEnumerable<SalesRepresentativeModel> SalesRep = RetrieveSalesRep();
            model.SalesReps = new SelectList(SalesRep, "salesRepId", "fullName");
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = "Err" };
            }
            return View(model);

        }
        //public ActionResult LandlordInfo()
        //{
        //    return PartialView("_LandlordInformation", new LandlordInformationModel());
        //}

    }
}