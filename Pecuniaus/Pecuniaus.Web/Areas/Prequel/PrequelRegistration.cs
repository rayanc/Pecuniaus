using System.Web.Mvc;

namespace Prequel
{
    public class PrequelRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Prequel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Prequel_default",
                url: "Prequel/{controller}/{action}/{id}",
                defaults: new { controller = "OfferCreation", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Pecuniaus.Prequel.Controllers" }

            );
        }
    }
}