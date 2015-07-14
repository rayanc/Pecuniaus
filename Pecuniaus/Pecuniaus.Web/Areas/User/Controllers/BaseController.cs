using Pecuniaus.ApiHelper;
using Pecuniaus.User.Models;
using Pecuniaus.Models;
using Pecuniaus.UICore.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;


namespace Pecuniaus.User.Controllers
{
    public class BaseController : UiBaseController
    {
       

        protected IEnumerable<SelectListItem> GetProvince()
        {
            var docTypes = CommonFunctions.GetStates();

            return from c in docTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetGroupTypes() 
        {
            var groupTypes = CommonFunctions.GetGroupTypes();
 
            return from c in groupTypes
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetGroups()
        {
            var groupNames = CommonFunctions.GetGroups();

            return from c in groupNames
                   select new SelectListItem
                   {
                       Text = c.Description,
                       Value = c.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetUsers() 
        {
            var userNames = CommonFunctions.GetUsers();

            return from usr in userNames
                   select new SelectListItem
                   {
                       Text = usr.Description,
                       Value = usr.KeyId.ToString()
                   };
        }

        protected IEnumerable<SelectListItem> GetWorkFlowTypes() 
        {
            var workFlowNames = CommonFunctions.GetWorkFlowTypes();
             
            return from wrk in workFlowNames
                   select new SelectListItem
                   {
                       Text = wrk.Description,
                       Value = wrk.KeyId.ToString()
                   };
        }
        protected IEnumerable<SelectListItem> GetParentModules() 
        {
            var moduleNames = CommonFunctions.GetParentModules();

            return from modu in moduleNames
                   select new SelectListItem
                   {
                       Text = modu.Description,
                       Value = modu.KeyId.ToString()
                   };
        }

    }
}