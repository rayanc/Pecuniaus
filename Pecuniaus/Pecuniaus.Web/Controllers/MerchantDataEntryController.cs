using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Pecuniaus.ApiHelper;
using Pecuniaus.UICore;
using Pecuniaus.UICore.Controllers;
using Pecuniaus.Web.Models;
using Pecuniaus.Web.Repository;

namespace Pecuniaus.Web.Controllers
{
    public class MerchantDataEntryController : UiBaseController
    {
        OwnerSesssionRepository ownerSesssionRepository;
        ProcessorSessionRepository processorSessionRepository;

        public MerchantDataEntryController()
        {
            ownerSesssionRepository = new OwnerSesssionRepository();
            processorSessionRepository = new ProcessorSessionRepository();
        }
        //
        // GET: /DataEntry/
        public ActionResult Index()
        {
            var apiMethod = string.Format("merchants/info/{0}", CurrentMerchantID);

            var model = BaseApiData.GetAPIResult<DataEntryModel>(apiMethod, () => new DataEntryModel()) ?? new DataEntryModel { };
            ownerSesssionRepository.Set(model.Owners);
            processorSessionRepository.Set(model.Processor);

            model.IndustryTypes = GetIndustryTypes();
            model.Provinces = GetProvince();
            model.ProcessorCompar = GetProcessors();
            model.TypeOfProperties = GetPropertyTypes();
            model.BusinessType = GetBusinessTypes();
            IEnumerable<SalesRepresentativeModel> SalesRep = RetrieveSalesRep();
            model.SalesReps = new SelectList(SalesRep, "salesRepId", "fullName");

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DataEntryModel model, FormCollection obj, string button)
        {
            var isComplete = 0;
            if (button == "Complete")
            {
                isComplete = 1;
            }
            //model.TypeOfPropertyID = 1;
            if (ModelState.IsValid)
            {
                //Save Data      
                //dataentry/{merchantId}

                model.Owners = ownerSesssionRepository.GetAll();
                model.Processor = processorSessionRepository.GetAll();
                var proceedtoSave = true;
                if (isComplete == 1)
                {
                    if (model.Owners.Count < 1)
                    {
                        proceedtoSave = false;
                        base.SetErrorMessage("Please add at least one owner.");
                    }
                    if (model.Processor.Count < 1)
                    {
                        proceedtoSave = false;
                        base.SetErrorMessage("Please add lat least one Processor.");
                    }
                }

                if (proceedtoSave)
                {
                    if (isComplete == 0)
                    {
                        var apiMethod = string.Format("merchants/dataentry/{0}?isCompleted={1}", CurrentMerchantID, isComplete);
                        BaseApiData.PutAPIData(apiMethod, model);
                        if (ContractID == 0)
                        {
                            apiMethod = string.Format("merchants/merchantinfo/{0}?tasktypeId={1}&contractId={2}", CurrentMerchantID, 0, 0);
                            var mModel = BaseApiData.GetAPIResult<Pecuniaus.Models.MerchantModel>(apiMethod, () => new Pecuniaus.Models.MerchantModel()) ?? new Pecuniaus.Models.MerchantModel();
                            Pecuniaus.UICore.SessionHelper.SetContractID(mModel.ContractId);
                        }
                    }
                    else
                    {
                        var apiMethod = string.Format("merchants/dataentry/{0}?isCompleted={1}", CurrentMerchantID, isComplete);
                        BaseApiData.PutAPIData(apiMethod, model);

                        if (ContractID == 0)
                        {
                            apiMethod = string.Format("merchants/merchantinfo/{0}?tasktypeId={1}&contractId={2}", CurrentMerchantID, 0, 0);
                            var mModel = BaseApiData.GetAPIResult<Pecuniaus.Models.MerchantModel>(apiMethod, () => new Pecuniaus.Models.MerchantModel()) ?? new Pecuniaus.Models.MerchantModel();
                            Pecuniaus.UICore.SessionHelper.SetContractID(mModel.ContractId);
                        }

                        string apiData = string.Format("Contracts/CompContractTask?merchantId={0}&taskTypeId={1}&workflowId={2}&contractId={3}",
                                CurrentMerchantID, (int)TaskTypes.PQDataEntry, 1, ContractID);
                        BaseApiData.GetAPIResult<object>(apiData, () => new object());
                    }

                    if (isComplete == 1)
                    {
                        base.SetSuccessMessage("Data Entry - Task Completed.");
                    }
                    else
                    {
                        base.SetSuccessMessage("Data Updated.");
                        // return RedirectToAction("Index");
                    }
                }
            }
            model.IndustryTypes = GetIndustryTypes();
            model.Provinces = GetProvince();
            model.ProcessorCompar = GetProcessors();
            model.TypeOfProperties = GetPropertyTypes();
            IEnumerable<SalesRepresentativeModel> SalesRep = RetrieveSalesRep();
            model.SalesReps = new SelectList(SalesRep, "salesRepId", "fullName");
            return RedirectToAction("Index");

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
        #endregion
        #region Processor

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

        public ActionResult ListOwners()
        {
            var owners = ownerSesssionRepository.GetAll();
            return PartialView("_ListOwners", owners);
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

        private IEnumerable<SelectListItem> GetIndustryTypes()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.Industry);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }

        private IEnumerable<SelectListItem> GetProvince()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.Province);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }

        private IEnumerable<SelectListItem> GetProcessors()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.Processor);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }

        public IList<SalesRepresentativeModel> RetrieveSalesRep()
        {
            string query = "";
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + "Sales?searchString=" + query);
            objRequest.Method = "Get";

            IList<SalesRepresentativeModel> objBE;
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<SalesRepresentativeModel>>(reader.ReadToEnd());
            }

            return objBE;
        }

        private IEnumerable<SelectListItem> GetPropertyTypes()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.PropertyType);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetBusinessTypes()
        {
            var query = string.Format("gen/retrieve/{0}", (int)DropDownTypes.BusinessEntity);
            var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(query, () => new List<GeneralModel>());

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.description,
                       Value = c.keyId.ToString()
                   };
        }

    }
}