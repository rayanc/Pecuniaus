using System;
using System.Web.Optimization;

namespace Pecuniaus.MerchantProfile
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/MerchantProfile").Include(
                      "~/Areas/MerchantProfile/Scripts/MerchantProfile.js",
                      "~/Areas/MerchantProfile/Scripts/jquery.base64.js",
                      "~/Areas/MerchantProfile/Scripts/CustomValidation.js",
                      "~/Areas/MerchantProfile/Scripts/tableExport.js"));

        }
    }
}