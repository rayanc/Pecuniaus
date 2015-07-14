using Pecuniaus.ApiHelper;
using Pecuniaus.Contract.Models;
using Pecuniaus.Contract.Repository;
using Pecuniaus.Models.Contract;
using Pecuniaus.UICore;
using Pecuniaus.Utilities.PDF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Pecuniaus.Contract.Controllers
{
    public class ContractController : BaseDEController
    {
        ContractApi contractApi;
        MerchantApi merchantApi;
        OwnerSesssionRepository _OwnerCorpSessionRepository;

        public ContractController()
        {
            contractApi = new ContractApi();
            merchantApi = new MerchantApi();
            _OwnerCorpSessionRepository = new OwnerSesssionRepository();
        }

        private string GetScriptFilePath()
        {
            return "~/Uploads/VerifyCall/";
        }

        private string GetLandLordScriptFilePath()
        {
            return "~/Uploads/LandLordCall/";
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
            contractApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.CWContract, ContractID);
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
            model.AdvanceTypes = base.GetTypeOfAdvances();
            model.ScriptFilePath = Url.Content(GetScriptFilePath());
            return PartialView("_VerificationCall", model);
        }

        public ActionResult DataEntry()
        {
            //var model = merchantApi.Info(CurrentMerchantID);
            //model.IndustryTypes = GetIndustryTypes();
            //model.Provinces = GetProvince();
            //model.ProcessorCompar = GetProcessors();
            //model.TypeOfProperties = GetPropertyTypes();
            //model.BusinessType = GetBusinessTypes();

            //return PartialView("_DataEntry", model);
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

        public ActionResult Contracts()
        {
            var model = contractApi.GetAdminExp(ContractID);
            model.FileName = string.Format("Cont_{0}_{1}.pdf", CurrentMerchantID, ContractID);

            return PartialView("_Contracts", model);
        }

        [HttpPost]
        public ActionResult GenerateBLA(ContractModel model, string button)
        {
            string blafileName = string.Format("Cont_{0}_{1}.pdf", CurrentMerchantID, ContractID);

            if (button == "BLA")
            {
                var bla = contractApi.GetBLADetails(CurrentMerchantID, ContractID);
                
                bla.ExpenseAmount = model.AdminExp.ToString();

                string tempFilePath = System.IO.Path.GetTempFileName();

                string destPdf = Path.Combine(Server.MapPath("~/Docs/Contract"), blafileName);
                string termsPdf = Server.MapPath(ConfigurationManager.AppSettings["ContractTermsPDF"]);

                PdfHelper pdfHelper = new PdfHelper();

                pdfHelper.CreatePDF(bla, Server.MapPath(ConfigurationManager.AppSettings["BLATemplate"]), tempFilePath);

                pdfHelper.CombineMultiplePDFs(new string[] { tempFilePath, termsPdf }, destPdf);
            }
            else if (button == "IOU")
            {


            }
            model.FileName = blafileName;
            return PartialView("_Contracts", model);
        }

        public FileResult DownloadIOU(string id)
        {
            string fileName = string.Format("IOU_{0}_{1}.pdf", CurrentMerchantID, ContractID);
            var iou = contractApi.GetIOUDetails(CurrentMerchantID, ContractID);

            string templateFile = string.Empty;


            if (iou.BusinesTypeId == 11001) 	//Persona Fisica
            {
                if (iou.StateId == 30) //Santo Domingo
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUPersonaFisicaSantoDomingo2Owner";
                    }
                    else
                    {
                        templateFile = "IOUPersonaFisicaSantoDomingo1Owner";
                    }

                }
                else
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUPersonaFisica2Owner";
                    }
                    else
                    {
                        templateFile = "IOUPersonaFisica1Owner";
                    }
                }

            }
            else
            {
                if (iou.StateId == 30) //Santo Domingo
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUSantoDomingo2Owner";
                    }
                    else
                    {
                        templateFile = "IOUSantoDomingo1Owner";
                    }

                }
                else
                {
                    if (iou.OwnerList.Count > 1)
                    {
                        templateFile = "IOUOther2Owner";
                    }
                    else
                    {
                        templateFile = "IOUOther1Owner";
                    }
                }
            }



            string destPdf = Path.Combine(Server.MapPath("~/Docs/Contract"), fileName);

            PdfHelper pdfHelper = new PdfHelper();

            pdfHelper.CreatePDF(iou, Server.MapPath(ConfigurationManager.AppSettings[templateFile]), destPdf);

            return File(destPdf, "application/pdf", "IOU.pdf");
        }

    }
}