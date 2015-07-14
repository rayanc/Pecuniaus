using System.Web;
using System.Web.Optimization;

namespace Pecuniaus.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/bundles/scriptsbundle", "http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js").Include(
                "~/Scripts/pages/tablesdatatables.js",
                      "~/Scripts/vendor/bootstrap.min.js",
                      "~/Scripts/vendor/jquery-1.11.1.min.js",
                      "~/Scripts/vendor/jquery-ui-1.11.2.js",
                      "~/Scripts/app.js",
                      "~/Scripts/plugins.js",
                      "~/Scripts/Common.js",
                      "~/Scripts/gridmvc.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/pages/uiProgress.js",
                      "~/Scripts/jquery.timepicker.js",
                      "~/Scripts/readmore.min.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrapbundle").Include(

                      "~/Scripts/vendor/bootstrap.min.js",
                      "~/Scripts/vendor/jquery-ui-1.11.2.js",
                      "~/Scripts/app.js",
                      "~/Scripts/plugins.js",
                      "~/Scripts/gridmvc.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/jquery.timepicker.js",
                      "~/Scripts/readmore.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                      "~/Scripts/requiredIf.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxform").Include(
                        "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizrbundle").Include(
                      "~/Scripts/vendor/modernizr-2.7.1-respond-1.4.2.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                                    "~/Scripts/jquery.inputmask/jquery.inputmask.js",
                                    "~/Scripts/jquery.inputmask/jquery.inputmask.extensions.js",
                                    "~/Scripts/jquery.inputmask/jquery.inputmask.date.extensions.js",
                                    "~/Scripts/jquery.inputmask/jquery.inputmask.numeric.extensions.js",
                                     "~/Scripts/jquery.inputmask/init.js"));

            //bundles.Add(new ScriptBundle("~/bundles/GridMvc").Include(
            //          "~/Scripts/gridmvc.min.js"
            //    ));

            bundles.Add(new ScriptBundle("~/bundles/jHtmlArea").Include(
                         "~/Scripts/jHtmlArea-0.8.js"
                         
                         ));

            bundles.Add(new StyleBundle("~/bundles/jHtmlAreaCSS").Include(
                "~/content/jHtmlArea.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/stylesbundle").Include(
                "~/content/bootstrap.min.css",
                "~/content/main.css",
                "~/content/plugins.css",
                 "~/content/Site.css",
                "~/content/jquery-ui.css",
                 "~/content/timepicki.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/GridMvc").Include(
                "~/content/Gridmvc.css"
                ));
            bundles.Add(new StyleBundle("~/bundles/Common").Include(
             "~/content/Common.css"
        ));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            BundleTable.EnableOptimizations = false;
        }
    }
}
