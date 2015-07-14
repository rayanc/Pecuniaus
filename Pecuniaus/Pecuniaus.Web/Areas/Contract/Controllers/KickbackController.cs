using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using Pecuniaus.UICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.Utilities.Extensions;

namespace Pecuniaus.Contract.Controllers
{
    public class KickbackController : BaseController
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
            var cwModules = new List<SelectListItem> {new SelectListItem
                    {
                        Text = TaskTypes.CWScanDocument.GetDescription(),
                        Value = ((int)TaskTypes.CWScanDocument).ToString()
                    } ,
                    new SelectListItem
                    {
                        Text = TaskTypes.CWVerificationCall.GetDescription(),
                        Value = ((int)TaskTypes.CWVerificationCall).ToString()
                    } ,
                    new SelectListItem
                    {
                        Text = TaskTypes.CWDataEntry.GetDescription(),
                        Value = ((int)TaskTypes.CWDataEntry).ToString()
                    } ,
                    new SelectListItem
                    {
                        Text = TaskTypes.CWVerificationTask.GetDescription(),
                        Value = ((int)TaskTypes.CWVerificationTask).ToString()
                    } ,
                    new SelectListItem
                    {
                        Text = TaskTypes.CWReview.GetDescription(),
                        Value = ((int)TaskTypes.CWReview).ToString()
                    }, 
                    new SelectListItem
                    {
                        Text = TaskTypes.CWContract.GetDescription(),
                        Value = ((int)TaskTypes.CWContract).ToString()
                    } ,
                    new SelectListItem
                    {
                        Text = TaskTypes.CWFunding.GetDescription(),
                        Value = ((int)TaskTypes.CWFunding).ToString()
                    },
                    new SelectListItem
                    {
                        Text = TaskTypes.CWFinalValidation.GetDescription(),
                        Value = ((int)TaskTypes.CWFinalValidation).ToString()
                    } 
                    
            };

            var modules = new List<SelectListItem>();
            var addNext = false;

            for (int i = cwModules.Count-1; i >= 0; i--)
            {
                if (addNext)
                {
                    modules.Add(cwModules[i]);
                }
                if (cwModules[i].Value == taskTypeId.ToString())
                {
                    addNext = true;
                }
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