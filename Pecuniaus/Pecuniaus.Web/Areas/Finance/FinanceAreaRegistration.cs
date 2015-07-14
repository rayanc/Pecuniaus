using System.Web.Mvc;

namespace Finance
{
    public class FinanceAreaRegistration:AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Finance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "Finance_default",
                url: "Finance/{controller}/{action}/{id}",
                defaults: new { controller = "Merchant", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.Finance.Controllers" }

            );
        }
    }
}