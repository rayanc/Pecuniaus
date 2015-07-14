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

namespace Pecuniaus.User.Controllers
{
    public class UserProfileController : BaseController
    {
        //
        // GET: /UserProfile/
        #region Actions
       
        public ActionResult Index(int? userId=null)
        {
            UsertimeTableModel usertimeTableModel = new UsertimeTableModel();
            usertimeTableModel.WorkFlows = GetWorkFlowTypes();
            usertimeTableModel.Users = GetUsers();
            if (userId!=null)
            { 
                usertimeTableModel.UserID = System.Convert.ToInt64(userId);
            }

            return View(usertimeTableModel);
        }


        public ActionResult TasksListing(int workflowID, long userID)
        {
            UsertimeTableModel usertimeTableModel = GetAllTimeTasksWithDetails(workflowID, userID);          
            return PartialView("_TasksListing", usertimeTableModel); 
        }

        
        #endregion

        #region Methods

        //List<UserProfileModel> list need to save workflow tasks for user 
        public ActionResult AddUpdateUserTimeTable(UsertimeTableModel model)
        {

            model.InsertUserId =model.ModifyUserId = base.CurrentUserID;
          
            var apiMethod = "users/addUpdateUserTimeTableWithDetails";
            var response = BaseApiData.PutAPIData<UsertimeTableModel>(apiMethod, model);
            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserProfileUpdateSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.UserProfileUpdateSuccess);
            return PartialView("_TasksListing", model);
        }

        public UsertimeTableModel GetAllTimeTasksWithDetails(int workflowID, long userID)
        {
            UsertimeTableModel usertimeTableModel = null; 
            var apiMethod = "users/GetAllTimeTasksWithDetails/" + workflowID + "/" + userID;
            UsertimeTableModel objBE;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + apiMethod);
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<UsertimeTableModel>(reader.ReadToEnd());
                usertimeTableModel = objBE;
            }

            return usertimeTableModel;
        }
        #endregion
    }
}