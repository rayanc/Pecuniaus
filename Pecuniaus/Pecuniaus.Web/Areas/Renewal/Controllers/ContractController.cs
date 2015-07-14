using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.Models.Contract;
using Pecuniaus.Renewal.Models;
using Pecuniaus.Renewal.Repository;
using Pecuniaus.UICore;
using Pecuniaus.Utilities.PDF;

namespace Pecuniaus.Renewal.Controllers
{
    public class ContractController : BaseController
    {

        # region Local Variable
        private RenewalApi renewalApi;
        private ContractApi contractApi;
        OfferSesssionRepository offerSesssionRepository;
        #endregion

        #region Constructor
        public ContractController()
        {
            renewalApi = new RenewalApi();
            contractApi = new ContractApi();
            offerSesssionRepository = new OfferSesssionRepository();
        }
        #endregion
        //
        // GET: /Contract/
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(string command)
        //{

        //    if (command == "complete")
        //    {
        //        contractApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.RWContract., ContractID);
        //        return RedirectToAction("Index", "funding");
        //    }
        //    else
        //        base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPOwnerUpdateFailure);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult Index(string button)
        {
            string blafileName = string.Format("Cont_{0}_{1}.pdf", CurrentMerchantID, ContractID);
           var path = Path.Combine(Server.MapPath("~/Docs/Contract/"), blafileName);           
           if (System.IO.File.Exists(path))
            {
                renewalApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.RWContract, (int)WorkflowTypes.Renewal, ContractID);
                base.SetSuccessMessage("Task Completed" );
            }
            else
               base.SetErrorMessage("Can not complete the task, Contract not generated");

            if (Request.IsAjaxRequest())
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Contract") });
            }
            return RedirectToAction("Index", "Contract");
        }

        public ActionResult Evaluluation()
        {
            var apiMethodProcessor = string.Format("creditreport/information/" + CurrentMerchantID);
            var modelmerchantinfo = BaseApiData.GetAPIResult<RetrieveMerchantInformationModel>(apiMethodProcessor, () => new RetrieveMerchantInformationModel());
            RetrieveMerchantInformationModel MerchantInformation = modelmerchantinfo;
            return PartialView("_Evaluluation", modelmerchantinfo);
        }        

        public ActionResult Contracts()
        {
            var model = contractApi.GetAdminExp(ContractID);
            model.FileName = string.Format("Cont_{0}_{1}.pdf", CurrentMerchantID, ContractID);
            return PartialView("_Contracts", model);
        }

        #region Contract
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
        #endregion
    }
}