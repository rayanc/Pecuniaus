using Pecuniaus.ApiHelper;
using Pecuniaus.AudioConvertor;
using Pecuniaus.Models.Contract;
using Pecuniaus.UICore;
using Pecuniaus.UICore.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Contract.Controllers
{
    public class VerificationCallController : BaseController
    {
        ContractApi contractApi;

        private string GetScriptFilePath()
        {
            return "~/Uploads/VerifyCall/";
        }

        public VerificationCallController()
        {
            contractApi = new ApiHelper.ContractApi();
        }

        // GET: /VerificationCall/
        public ActionResult Index()
        {
            var model = contractApi.GetVerificationCall(ContractID, CurrentMerchantID);

            model.AdvanceTypes = base.GetTypeOfAdvances("");
            model.ScriptFilePath = Url.Content(GetScriptFilePath());
            model.UserFullName = GetUserFullName();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(VerificationCallModel model, HttpPostedFileBase file, string button)
        {
            var isComplete = false;

            if (button == "Complete")
            {
                isComplete = true;
            }

            var oldValModel = contractApi.GetVerificationCall(ContractID, CurrentMerchantID);

            if (model.VcAnswers.TypeOfAdvanceId)
            {
                model.MerchantDetails.TypeOfAdvanceId = oldValModel.MerchantDetails.TypeOfAdvanceId;
            }
            //model.questions = questions;
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var tempFile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    file.SaveAs(tempFile);

                    //var fileName = "VerCal_" + CurrentMerchantID + "_" + ContractID + Path.GetExtension(file.FileName);
                    var fileName = "VerCal_" + CurrentMerchantID + "_" + ContractID + ".mp3";
                    var fullPath = Path.Combine(Server.MapPath(GetScriptFilePath()), fileName);

                    model.ScriptFile = fileName;

                    AudioHelper.ConvertToMP3(tempFile, fullPath);
                }

                //var updateQuery = string.Format("contracts/UpdateVerificationCall?isCompleted={0}", complete);
                //BaseApiData.PutAPIData(updateQuery, model);
                contractApi.UpdateVerificationCall(model, base.ContractID, false);
                if (isComplete)
                {
                    var savedModel = contractApi.GetVerificationCall(ContractID, CurrentMerchantID);
                    if (string.IsNullOrEmpty(savedModel.ScriptFile))
                    {
                        SetErrorMessage("Can't complete,  Script file is not uploaded.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        contractApi.CompContractTask(CurrentMerchantID, (int)TaskTypes.CWVerificationCall, ContractID);

                        SetSuccessMessage("Verification Call - Task Completed.");
                        return RedirectToAction("Index", "DataEntry");
                    }
                }
                else
                {
                    SetSuccessMessage("Updated.");
                    if (Request.IsAjaxRequest())
                    {
                        return new JsonResult { Data = "OK" };
                    }
                    return RedirectToAction("Index");
                }
            }

            model.AdvanceTypes = base.GetTypeOfAdvances("");
            model.ScriptFilePath = Url.Content(GetScriptFilePath());
            return View(model);

        }
    }
}