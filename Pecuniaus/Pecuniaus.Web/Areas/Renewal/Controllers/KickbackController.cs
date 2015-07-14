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

namespace Pecuniaus.Renewal.Controllers
{
    public class KickbackController : BaseController
    {
        RenewalApi renewalApi;

        public KickbackController()
        {
            renewalApi = new RenewalApi();
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

            if (taskTypeId == (int)TaskTypes.RWDocumentVerification)
            {
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.RWMerchantEvaluation).ToString()
                });

            }

            if (taskTypeId == (int)TaskTypes.RWOfferCreation)
            {
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.RWMerchantEvaluation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWDocumentVerification.GetDescription(),
                    Value = ((int)TaskTypes.RWDocumentVerification).ToString()
                });

            }

            if (taskTypeId == (int)TaskTypes.RWRenewalReview)
            {
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.RWMerchantEvaluation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWDocumentVerification.GetDescription(),
                    Value = ((int)TaskTypes.RWDocumentVerification).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWOfferCreation.GetDescription(),
                    Value = ((int)TaskTypes.RWOfferCreation).ToString()
                });

            }

            if (taskTypeId == (int)TaskTypes.RWContract)
            {
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.RWMerchantEvaluation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWDocumentVerification.GetDescription(),
                    Value = ((int)TaskTypes.RWDocumentVerification).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWOfferCreation.GetDescription(),
                    Value = ((int)TaskTypes.RWOfferCreation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWRenewalReview.GetDescription(),
                    Value = ((int)TaskTypes.RWRenewalReview).ToString()
                });

            }
            if (taskTypeId == (int)TaskTypes.RWFunding)
            {
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.RWMerchantEvaluation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWDocumentVerification.GetDescription(),
                    Value = ((int)TaskTypes.RWDocumentVerification).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWOfferCreation.GetDescription(),
                    Value = ((int)TaskTypes.RWOfferCreation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWRenewalReview.GetDescription(),
                    Value = ((int)TaskTypes.RWRenewalReview).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWContract.GetDescription(),
                    Value = ((int)TaskTypes.RWContract).ToString()
                });

            }

            if (taskTypeId == (int)TaskTypes.RWFinalValidation)
            {
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWMerchantEvaluation.GetDescription(),
                    Value = ((int)TaskTypes.RWMerchantEvaluation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWDocumentVerification.GetDescription(),
                    Value = ((int)TaskTypes.RWDocumentVerification).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWOfferCreation.GetDescription(),
                    Value = ((int)TaskTypes.RWOfferCreation).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWRenewalReview.GetDescription(),
                    Value = ((int)TaskTypes.RWRenewalReview).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWContract.GetDescription(),
                    Value = ((int)TaskTypes.RWContract).ToString()
                });
                modules.Add(new SelectListItem
                {
                    Text = TaskTypes.RWFunding.GetDescription(),
                    Value = ((int)TaskTypes.RWFunding).ToString()
                });

            }
            model.TaskTypes = new SelectList(modules, "Value", "Text");
            return PartialView("_Popup", model);
        }


        [HttpPost]
        public ActionResult PopupSubmit(int taskTypeId, string d)
        {
            renewalApi.KickBack(base.CurrentMerchantID, taskTypeId, base.ContractID, (int)WorkflowTypes.Renewal);

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