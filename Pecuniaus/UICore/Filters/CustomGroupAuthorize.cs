using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace Pecuniaus.UICore.Filters
{
    //AuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    public class CustomGroupAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Error", action = "AccessDenied", area = "" }));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            bool authorized = base.AuthorizeCore(httpContext);

            if (authorized)
            {
                var routeData = httpContext.Request.RequestContext.RouteData;
                var controller = routeData.GetRequiredString("controller");
                var action = routeData.GetRequiredString("action");
                var area = routeData.DataTokens.ContainsKey("area") ? routeData.DataTokens["area"].ToString().ToLower() : "";

                //Get the current claims principal
                //var principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
                //Make sure they are authenticated
                //if (!principal.Identity.IsAuthenticated) // might be dupliate check
                //   return false;

                //long userID = principal.Identity.GetUserId<long>();

                bool canAccess = PageHelper.HasAccess(controller, action, area);

                if (!canAccess)
                {
                    //      httpContext.Items["BlackError"] = true;
                    return false;
                }

                return true;
            }
            else
            {
                return authorized;
            }
        }


        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

    }
}
