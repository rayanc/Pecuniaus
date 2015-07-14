using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.UICore;
using Pecuniaus.UICore.Controllers;
using Pecuniaus.Web.Models;
using Pecuniaus.Web.Repository;

namespace Pecuniaus.Web.Controllers
{
    public class MerchantCreditVolumeController : UiBaseController
    {
        //
        // GET: /MerchantCreditVolume/
        public ActionResult Index()
        {
            var apiMethod = string.Format("merchants/info/{0}", CurrentMerchantID);
            var apiMethodProcessor = string.Format("merchants/ccprofiles?merchantId={0}", CurrentMerchantID);
            var model = BaseApiData.GetAPIResult<CreditVolumesModel>(apiMethod, () => new CreditVolumesModel()) ?? new CreditVolumesModel { };
            //Bind the list of data
            model.ProcessorByMerchants = GetProcessorByMerchants();
            model.ProcessorTypes = GetProcessors();
            //Bind the processors of a merchant
            var modelProcessor = BaseApiData.GetAPIResult<List<CreditVolumesModel>>(apiMethodProcessor, () => new List<CreditVolumesModel>());
            CreditVolumeRepository.Set(modelProcessor);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(CreditVolumesModel model, FormCollection obj, string submit)
        {
            if (ModelState.IsValid)
            {
                List<CreditVolumesModel> volume = CreditVolumeRepository.GetAll();
                var apiMethod = "";
                var counts = volume.GroupBy(x => x.processorId)
                      .Select(g => g.Count());
           
                var isValid4Monts = false;
                foreach (var c in counts)
                {
                    isValid4Monts = true;
                    if (c < 4)
                    {
                        isValid4Monts = false;
                        break;
                    }
                }


                if (!CheckProcessorDetails(volume))
                {
                    ModelState.AddModelError("Err", "Please enter processor information of all the processors");
                    //    TempData["SuccessMsg"] = "Please enter processor information of all the processors";
                }
                else if (!isValid4Monts)
                {
                    ModelState.AddModelError("Err", "Please enter 4 months data");
                }
                else
                {
                    if (submit == "Save")
                    {
                        apiMethod = string.Format("merchants/profiles/create?merchantid={0}&iscompleted={1}&contractid={2}", CurrentMerchantID, 0, ContractID);
                        BaseApiData.PostAPIData<List<CreditVolumesModel>>(apiMethod, volume);
                        TempData["SuccessMsg"] = "Data Updated.";
                    }
                    else if (submit == "Complete")
                    {
                        apiMethod = string.Format("merchants/profiles/create?merchantid={0}&iscompleted={1}&contractid={2}", CurrentMerchantID, 0, ContractID);
                        BaseApiData.PostAPIData<List<CreditVolumesModel>>(apiMethod, volume);

                        string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
            CurrentMerchantID, (int)TaskTypes.PQMonthlyCreditCardVolumes, 1, ContractID);
                        BaseApiData.GetAPIResult<object>(apiData, () => new object());
                        //  TempData["SuccessMsg"] = "Data Updated.";
                        base.SetSuccessMessage("Data Updated.");
                    }
                    else if (submit == "Decline")
                    {
                        //apiMethod = string.Format("merchants/profiles/create?merchantid={0}&iscompleted=1", CurrentMerchantID);
                        // BaseApiData.PostAPIData<List<CreditVolumesModel>>(apiMethod, volume);
                        base.SetSuccessMessage("Data Updated.");
                        //    TempData["SuccessMsg"] = "Data Updated.";
                    }
                    return RedirectToAction("Index");

                }
            }

            model.ProcessorByMerchants = GetProcessorByMerchants();
            model.ProcessorTypes = GetProcessors();

            return View("Index", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SendRequesttoProcessor(int merchantid)
        {
            var apiMethod = string.Format("merchants/profiles/requestprocessor?merchantid={0}&processorId={1}", CurrentMerchantID, 0);
            var model = BaseApiData.PostAPIData<CreditVolumesModel>(apiMethod, new CreditVolumesModel());
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        #region Private Members


        public bool CheckProcessorDetails(List<CreditVolumesModel> volume)
        {
            //var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.Processor);
            var query = string.Format("merchants/ccprofiles/processorbyMerchants/{0}", CurrentMerchantID);
            List<ProcessorModel> processor = BaseApiData.GetAPIResult<List<ProcessorModel>>(query, () => new List<ProcessorModel>());
            //List<GeneralModel> gen = BaseApiData.GetAPIResult<List<GeneralModel>>(query, () => new List<GeneralModel>());
            foreach (var items in processor)
            {
                List<CreditVolumesModel> volumelistFiltered = new List<CreditVolumesModel>();
                volumelistFiltered = volume.Where(c => c.processorId == items.processorId && c.isActive == 1).ToList();
                if (volumelistFiltered.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        [HttpGet]
        public ActionResult EditCreditVolume(long id)
        {
            var model = CreditVolumeRepository.GetByID(id);
            model.ProcessorTypes = GetProcessors();
            model.ListMonths = GetMonths();
            model.ListYears = GetYears();
            model.ProcessorByMerchants = GetProcessorByMerchants();
            model.ProcessorTypes = GetProcessors();
            return PartialView("EditCredit", model);
        }
        [HttpPost]
        public ActionResult EditCreditVolume(CreditVolumesModel model)
        {
            if (ModelState.IsValid)
            {
                CreditVolumeRepository.Update(model);
                return RedirectToAction("AddCredit");
            }
            model.ProcessorTypes = GetProcessors();
            return PartialView("EditCredit", model);
        }


        public ActionResult AddCredit()
        {
            return PartialView("AddCredit", new CreditVolumesModel
            {
                ListMonths = GetMonths(),
                ListYears = GetYears(),
                ProcessorByMerchants = GetProcessorByMerchants(),
                ProcessorTypes = GetProcessors()

            });
        }
        [HttpPost]
        public ActionResult AddCredit(CreditVolumesModel model)
        {
            if (ModelState.IsValid)
            {
                CreditVolumeRepository.Add(model);
                return RedirectToAction("AddCredit");
            }
            /*model.ProcessorTypes = GetProcessors();
             model.ListMonths = GetMonths();
             model.ListYears = GetYears();
             model.ProcessorByMerchants = GetProcessorByMerchants();*/
            return PartialView("AddCredit", model);
        }

        [HttpPost]
        public ActionResult DeleteCreditVolume(long id)
        {
            CreditVolumeRepository.Deleted(id);
            return Json("OK");
        }

        public ActionResult ListCreditVolumes()
        {
            List<CreditVolumesModel> creditVolume = CreditVolumeRepository.GetAll();
            if (creditVolume.Count > 0)
            {
                List<CreditVolumesModel> volumelistFiltered = CreditVolumeRepository.GetListAfterDelete();
                return PartialView("ListCreditVolumes", volumelistFiltered);
            }
            else
                return PartialView("ListCreditVolumes", creditVolume);
        }

        [HttpPost]
        public ActionResult ListCreditVolumesprocessortype(int processorTypeId)
        {
            List<CreditVolumesModel> creditVolume = CreditVolumeRepository.GetSelect(processorTypeId);
            return PartialView("ListCreditVolumes", creditVolume);
        }



        #endregion

        #region FillDropDown
        private IEnumerable<SelectListItem> GetProcessors()
        {
            var query = string.Format("merchants/ccprofiles/processorbyMerchants/{0}", CurrentMerchantID);
            var docTypes = BaseApiData.GetAPIResult<IList<ProcessorModel>>(query, () => new List<ProcessorModel>());
            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.processorName,
                       Value = c.ProcessorTypeId.ToString()
                   };
        }

        private IEnumerable<SelectListItem> GetProcessorByMerchants()
        {
            var query = string.Format("merchants/ccprofiles/processorbyMerchants/{0}", CurrentMerchantID);
            var docTypes = BaseApiData.GetAPIResult<IList<ProcessorModel>>(query, () => new List<ProcessorModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.processorRNC.ToString(),
                       Value = c.processorId.ToString()
                   };
        }
        [HttpGet]
        public JsonResult GetProcessorbyProcessorType(string name)
        {

            var query = string.Format("merchants/ccprofiles/processorbyMerchants/{0}", CurrentMerchantID);
            List<ProcessorModel> processorlist = BaseApiData.GetAPIResult<List<ProcessorModel>>(query, () => new List<ProcessorModel>());
            List<ProcessorModel> processorlistFiltered = processorlist.Where(c => c.processorName == name).ToList();
            if (string.IsNullOrEmpty(name))
                return Json(processorlist, JsonRequestBehavior.AllowGet);
            else
                return Json(processorlistFiltered, JsonRequestBehavior.AllowGet);

        }
        private IEnumerable<SelectListItem> GetMonths()
        {
            List<SelectListItem> itemsmonth = new List<SelectListItem>();
            itemsmonth.Add(new SelectListItem { Text = "January", Value = "1" });
            itemsmonth.Add(new SelectListItem { Text = "February", Value = "2" });
            itemsmonth.Add(new SelectListItem { Text = "March", Value = "3" });
            itemsmonth.Add(new SelectListItem { Text = "April", Value = "4" });
            itemsmonth.Add(new SelectListItem { Text = "May", Value = "5" });
            itemsmonth.Add(new SelectListItem { Text = "June", Value = "6" });
            itemsmonth.Add(new SelectListItem { Text = "July", Value = "7" });
            itemsmonth.Add(new SelectListItem { Text = "August", Value = "8" });
            itemsmonth.Add(new SelectListItem { Text = "September", Value = "9" });
            itemsmonth.Add(new SelectListItem { Text = "October", Value = "10" });
            itemsmonth.Add(new SelectListItem { Text = "November", Value = "11" });
            itemsmonth.Add(new SelectListItem { Text = "December", Value = "12" });
            return itemsmonth;

        }
        private IEnumerable<SelectListItem> GetYears()
        {
            List<SelectListItem> itemsyear = new List<SelectListItem>();
            for (int year = 2000; year <= DateTime.Now.Year; year++)
            {
                itemsyear.Add(new SelectListItem { Text = Convert.ToString(year), Value = Convert.ToString(year) });
            }
            return itemsyear;

        }
        #endregion
    }
}