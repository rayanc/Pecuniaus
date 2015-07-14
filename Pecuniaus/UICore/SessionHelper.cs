using Pecuniaus.ApiHelper;
using Pecuniaus.Models;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Pecuniaus.Models.User;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Pecuniaus.UICore
{
    public class SessionHelper
    {
        public static long ContractID
        {
            get
            {
                long _cid = 0;

                if (HttpContext.Current.Session["_Cur_ContractID"] != null)
                {
                    _cid = (long)HttpContext.Current.Session["_Cur_ContractID"];
                }
                return _cid;
            }
        }

        public static long CurrentMerchantID
        {
            get
            {
                long _mid = 0;
                if (HttpContext.Current.Session["_Cur_MerchantID"] != null)
                {
                    _mid = (long)HttpContext.Current.Session["_Cur_MerchantID"];
                }
                return _mid;
            }
        }

        public static long CurrentGroupID
        {
            get
            {
                long _gid = 0;
                if (HttpContext.Current.Session["_Cur_GroupID"] != null)
                {
                    _gid = (long)HttpContext.Current.Session["_Cur_GroupID"];
                }
                return _gid;
            }
        }

        public static long CurrentUserID
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return HttpContext.Current.User.Identity.GetUserId<long>();
                }
                return -1;
            }
        }

        public static void SetCurrentGroup()
        {
            HttpContext.Current.Session["_Cur_GroupID"] = GetUserGroup();
        }

        public static void SetCurrentMerchant(long merchantID)
        {
            HttpContext.Current.Session["_Cur_MerchantID"] = merchantID;
        }

        public static void SetContractID(long contractId)
        {
            HttpContext.Current.Session["_Cur_ContractID"] = contractId;
        }


        public static int GetWorkFlowID()
        {
            var routeData = HttpContext.Current.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");
            var area = routeData.DataTokens.ContainsKey("area") ? routeData.DataTokens["area"].ToString().ToLower() : "";


            return PageHelper.GetWorkflowId(action, controller, area);
        }

        public static string GetScreenName()
        {
            var routeData = HttpContext.Current.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");
            var area = routeData.DataTokens.ContainsKey("area") ? routeData.DataTokens["area"].ToString().ToLower() : "";

            return PageHelper.GetPageName(action, controller, area);
        }

        public static bool IsFormDisabled(TaskTypes? taskType = null)
        {
            long merchantID = CurrentMerchantID;
            long contractId = ContractID;
            if (merchantID <= 0)
            {
                return true;
            }

            var tasktyp = HttpContext.Current.Request["TaskType"] ?? "";
            if (tasktyp == "0")
            {
                return false;
            }

            if (taskType.HasValue)
            {

                var apiMethod = string.Format("merchants/merchantinfo/{0}?tasktypeId={1}", merchantID, (long)taskType);
                var model = BaseApiData.GetAPIResult<MerchantModel>(apiMethod, () => new MerchantModel()) ?? new MerchantModel();
                HttpContext.Current.Items["MerchantInfo"] = model;

                return model.TaskStatusId != (long)StatusTypes.Assigned;
            }
            return false;
        }
        public static bool IsMerchantPropertyRented
        {
            get
            {
                if (HttpContext.Current.Items["MerchantInfo"] == null)
                {
                    var apiMethod = string.Format("merchants/merchantinfo/{0}", CurrentMerchantID);
                    var model = BaseApiData.GetAPIResult<MerchantModel>(apiMethod, () => new MerchantModel()) ?? new MerchantModel();
                    HttpContext.Current.Items["MerchantInfo"] = model;
                }
                return ((MerchantModel)HttpContext.Current.Items["MerchantInfo"]).merchantpropertytype == "Rented";
            }
        }

        public static GroupPermission GetPagePermissionsByPage(int pageId)
        {
            var permission = GetPermisssions().FirstOrDefault(a => a.PageId == pageId);
            if (permission != null)
                return permission;
            else
                return new GroupPermission { Edit = false, Read = false, Write = false };
        }

        public static GroupPermission GetPagePermissionsByPage(Modules module)
        {
            var allPermissions = GetPermisssions();
            var permission = allPermissions.FirstOrDefault(a => a.PageId == (int)module);
            if (permission != null)
                return permission;
            else
                return new GroupPermission { Edit = false, Read = false, Write = false };
        }

        //public static GroupPermission GetPagePermissionsByModule(Modules module)
        //{
        //    var allPermissions = GetPermisssions();
        //    var permission = allPermissions.FirstOrDefault(a => a.ModuleId == (int)module);
        //    if (permission != null)
        //        return permission;
        //    else
        //        return new GroupPermission { Edit = false, Read = false, Write = false };
        //}

        public static long GetUserGroup()
        {
            long groupId = new ApplicationUserApi().GetUserGroup(CurrentUserID).GroupID;
            return groupId;
        }

        public static IList<GroupPermission> GetPermisssions()
        {
            var sessionName = "_Permissions_" + CurrentUserID;
            if (HttpContext.Current.Session[sessionName] != null)
                return (IList<GroupPermission>)HttpContext.Current.Session[sessionName];

            var permissions = new ApplicationUserApi().GetPermissions(CurrentUserID);
            HttpContext.Current.Session[sessionName] = permissions;
            return permissions;
        }
    }
}
