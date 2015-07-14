using System.Web.Optimization;

namespace Pecuniaus.Contract
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Contract").Include(
                      "~/Areas/Contract/Scripts/Contract.js"));

        }
    }
}