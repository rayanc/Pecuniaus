using System;
using System.Web.Optimization;

namespace Pecuniaus.Reports
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Reports").Include(
                      "~/Areas/Reports/Scripts/globalize.js",
                      "~/Areas/Reports/Scripts/globalize.cultures.js",
                      "~/Areas/Reports/Scripts/knockout-3.0.0.js"));

        }
    }
}