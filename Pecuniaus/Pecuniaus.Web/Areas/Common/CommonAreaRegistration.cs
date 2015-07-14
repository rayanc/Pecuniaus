using System.Web.Mvc;
using Pecuniaus.Common;
using System.Web.Optimization;

namespace Common
{
    public class CommonAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Common";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Common_default",
                url: "Common/{controller}/{action}/{id}",
                defaults: new { controller = "Merchant", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.Common.Controllers" }

            );
            RegisterBundles();
        }

        private void RegisterBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }  
       
    }
}