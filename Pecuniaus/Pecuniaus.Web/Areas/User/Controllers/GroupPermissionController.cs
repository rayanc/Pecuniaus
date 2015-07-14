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

namespace Pecuniaus.User.Controllers
{
    public class GroupPermissionController : BaseController
    {


        #region Actions
        // GET: /GroupPermission/
        public ActionResult Index(int? moduleID=null,int? groupID=null)
        {
            GroupPermissionModel groupPermissionModel = new GroupPermissionModel();
                groupPermissionModel.Groups = GetGroups();
                groupPermissionModel.ParentModules = GetParentModules();
                if (moduleID!=null &&  groupID != null)
                {
                groupPermissionModel.GroupID = groupID;
                groupPermissionModel.lstGroupRights = GetAllModuleRightsByGroupID((int)moduleID,(int)groupID);
                }
                //  groupPermissionModel.lstGroupRights = GetAllModuleRights();
                return View("_GroupPermissions", groupPermissionModel);
            }

        
        //ListGroupRights
        public ActionResult ListGroupRights(int moduleID, int groupID)
        {
            List<GroupPermissionModel> lstGroupRights = GetAllModuleRightsByGroupID(moduleID,groupID);
            return PartialView("_GroupPermissionsListing", lstGroupRights);
        }
        public ActionResult BindModulePermissionsByGroupID(int moduleID,int groupID)
        {
            GroupPermissionModel groupPermissionModel = new GroupPermissionModel();
            groupPermissionModel.Groups = GetGroups();
            groupPermissionModel.lstGroupRights = GetAllModuleRightsByGroupID(moduleID,groupID);
            return View("_GroupPermissions", groupPermissionModel);
        }
        
      
        public ActionResult SaveModulePermissions(List<GroupPermissionModel> groupPermissionData)
        {
            AddUpdateGroupPermissions(groupPermissionData);
            base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.GroupPermissionsSuccess);
            GroupPermissionModel groupPermissionModel = new GroupPermissionModel();
            groupPermissionModel.Groups = GetGroups();
            groupPermissionModel.lstGroupRights = GetAllModuleRightsByGroupID((int)groupPermissionData[0].ModuleID,(int)groupPermissionData[0].GroupID);
            if (groupPermissionModel!=null)
            return View("_GroupPermissions", groupPermissionModel);
            return PartialView("_GroupPermissions");
        }

        #endregion

        #region Methods

        //List need to save group permissions
        public void AddUpdateGroupPermissions(List<GroupPermissionModel> model) 
        {
            var apiMethod = "users/addUpdateGroupPermissions";
           var response = BaseApiData.PutAPIData<List<GroupPermissionModel>>(apiMethod, model);
            if (response.StatusCode == HttpStatusCode.OK)
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.GroupPermissionsSuccess);
            else
                base.SetSuccessMessage(Pecuniaus.Resources.User.Messages.GroupPermissionsFailure);
        }

        public List<GroupPermissionModel> GetAllModuleRightsByGroupID(int moduleID, int groupID)
        {
            List<GroupPermissionModel> lstGroupPermissions= null;
            var apiMethod = "users/GetAllRightsforGroups/" + moduleID + "/" + groupID; 
            IList<GroupPermissionModel> objBE;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["APIURI"] + apiMethod);
            using (Stream responseStream = objRequest.GetResponse().GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                objBE = JsonConvert.DeserializeObject<IList<GroupPermissionModel>>(reader.ReadToEnd());
                lstGroupPermissions = objBE.ToList();
            }

            return lstGroupPermissions;
        }
        #endregion
    }
}