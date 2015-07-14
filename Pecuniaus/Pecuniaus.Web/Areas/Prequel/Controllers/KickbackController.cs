using Pecuniaus.UICore;
using Pecuniaus.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.Models;
using Pecuniaus.ApiHelper;
using Pecuniaus.UICore.Controllers;

namespace Pecuniaus.Prequel.Controllers
{
    public class KickbackController : UiBaseController
    {
        MerchantApi merchantApi;

        public KickbackController()
        {
            merchantApi = new MerchantApi();
        }
        //
        // GET: /Kickback/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Popup(int taskTypeId)
        {
            var model = new KickBack();
            var modules = new List<SelectListItem>();

            modules.Add(new SelectListItem
            {
                Text = TaskTypes.PQScanDocument.GetDescription(),
                Value = ((int)TaskTypes.PQScanDocument).ToString()
            });

            if (taskTypeId == (int)TaskTypes.PQMonthlyCreditCardVolumes)
            {
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQDataEntry.GetDescription(),
                    Value = ((int)TaskTypes.PQDataEntry).ToString()
                });
            }

            if (taskTypeId == (int)TaskTypes.PQMerchantEvaluation)
            {

                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQDataEntry.GetDescription(),
                    Value = ((int)TaskTypes.PQDataEntry).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQMonthlyCreditCardVolumes.GetDescription(),
                    Value = ((int)TaskTypes.PQMonthlyCreditCardVolumes).ToString()
                });
            }

            if (taskTypeId == (int)TaskTypes.PQOfferCreation)
            {

                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQDataEntry.GetDescription(),
                    Value = ((int)TaskTypes.PQDataEntry).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQMonthlyCreditCardVolumes.GetDescription(),
                    Value = ((int)TaskTypes.PQMonthlyCreditCardVolumes).ToString()
                });

                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.PQMerchantEvaluation).ToString()
                });
            }

            if (taskTypeId == (int)TaskTypes.PQOfferAcceptance)
            {

                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQDataEntry.GetDescription(),
                    Value = ((int)TaskTypes.PQDataEntry).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQMonthlyCreditCardVolumes.GetDescription(),
                    Value = ((int)TaskTypes.PQMonthlyCreditCardVolumes).ToString()
                });

                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.PQMerchantEvaluation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.PQOfferCreation.GetDescription(),
                    Value = ((int)TaskTypes.PQOfferCreation).ToString()
                });
            }
            model.TaskTypes = new SelectList(modules, "Value", "Text");

            return PartialView("_Popup", model);
        }

        [HttpPost]
        public ActionResult PopupSubmit(int taskTypeId, string d)
        {
            merchantApi.KickBack(base.CurrentMerchantID, taskTypeId, base.ContractID);

            if (Request.IsAjaxRequest())
            {
                var link = base.GetPageLink(taskTypeId);
                //   perviousTask 
                return Json(new { url = link });
            }
            // call method for decline

            return PartialView("_Popup");
        }
    }
}