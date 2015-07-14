using System.Web.Mvc;
using System.Web.Optimization;
using Pecuniaus.MerchantProfile;

namespace MerchantProfile
{
    public class MerchantProfileRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MerchantProfile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "MerchantProfile_default",
                url: "MerchantProfile/{controller}/{action}/{id}",
                defaults: new { controller = "Merchant", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.MerchantProfile.Controllers" }

            );
            RegisterBundles();
        }

        private void RegisterBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }   
    }
}