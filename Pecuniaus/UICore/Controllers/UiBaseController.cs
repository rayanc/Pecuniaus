using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Pecuniaus.UICore.Filters;
using Pecuniaus.UICore.Identity;

namespace Pecuniaus.UICore.Controllers
{
    //   [Authorize]
    [CustomGroupAuthorize]
    public class UiBaseController : Controller
    {
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //}

        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //}

        public void SetSuccessMessage(string message)
        {
            TempData["SuccessMsg"] = message;
        }

        public void SetErrorMessage(string message)
        {
            TempData["ErrorMsg"] = message;
        }

        public long CurrentMerchantID
        {
            get
            {
                return SessionHelper.CurrentMerchantID;
            }
        }

        public long CurrentUserID
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return User.Identity.GetUserId<long>();
                }
                return -1;
            }
        }

        public string GetUserFullName()
        {
            if (User.Identity.IsAuthenticated)
            {
                var manager = new UserManager<Pecuniaus.UICore.Models.User>(new UserStore());
                var currentUser = manager.FindById(User.Identity.GetUserId());

                return currentUser.FullName;
            }
            return string.Empty;

        }

        public long ContractID
        {
            get
            {
                return SessionHelper.ContractID;
            }
        }

        public long WorkflowID { get; set; }

        public string GetPageLink(long taskTypeId)
        {
            return PageHelper.GetPageLink(taskTypeId, ControllerContext.RequestContext);
        }
    }
}
