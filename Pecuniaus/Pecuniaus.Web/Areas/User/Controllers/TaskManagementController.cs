using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Pecuniaus.ApiHelper;
using Pecuniaus.User.Models;
using Pecuniaus.User.Repository;
using Newtonsoft.Json;
using System.Text;
using System.Security.Claims;
using System;
using Pecuniaus.Models;

namespace Pecuniaus.User.Controllers
{
    public class TaskManagementController : BaseController
    {
        #region Actions

        public ActionResult Index()
        {
            var apiMethod = "users/gettaskassignments";
            IList<TaskAssignmentModel> obj = BaseApiData.GetAPIResult<IList<TaskAssignmentModel>>(apiMethod, () => new List<TaskAssignmentModel>());

            //obj.WorkFlows = GetWorkFlowTypes();
            return View(obj);
        }

        public ActionResult Details(Int64 UserID)
        {
            var apiMethod = string.Format("users/{0}/gettaskassignmentdetails", UserID);
            IList<TaskAssignmentDetailModel> obj = BaseApiData.GetAPIResult<IList<TaskAssignmentDetailModel>>(apiMethod, () => new List<TaskAssignmentDetailModel>());

            string apiMethod2;
            foreach (TaskAssignmentDetailModel tsk in obj)
            {
                apiMethod2 = string.Format("users/{0}/gettaskassignmentusers", UserID);
                var docTypes = BaseApiData.GetAPIResult<IList<GeneralModel>>(apiMethod2, () => new List<GeneralModel>());

                tsk.UserstoAssign = from c in docTypes
                                    select new SelectListItem
                                    {
                                        Text = c.Description,
                                        Value = c.KeyId.ToString()
                                    };
            }

            //obj.WorkFlows = GetWorkFlowTypes();
            return PartialView("_TaskAssignmentDetails", obj);
        }

        [HttpPost]
        public ActionResult ProcessUser(string submitButton, List<TaskAssignmentDetailModel> obj, FormCollection objCollection)
        {
            switch (submitButton)
            {
                case "Update":
                    if (ModelState.IsValid)
                    {
                        UpdateUserAssignment(obj);
                    }
                    break;
            }
            var apiMethod = "users/gettaskassignments";
            IList<TaskAssignmentModel> objOutput = BaseApiData.GetAPIResult<IList<TaskAssignmentModel>>(apiMethod, () => new List<TaskAssignmentModel>());
            return View(objOutput);
        }
        public void UpdateUserAssignment(List<TaskAssignmentDetailModel> obj)
        {
            var apiMethod = string.Format("users/{0}/update", base.CurrentMerchantID);
            var response = BaseApiData.PutAPIData<List<TaskAssignmentDetailModel>>(apiMethod, obj);

            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContactUpdateSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.ApplicationMessages.MPContactUpdateFailure);
        }
        #endregion
	}
}