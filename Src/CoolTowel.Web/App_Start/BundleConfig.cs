using System.Web;
using System.Web.Optimization;

namespace CoolTowel.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jslibs")
                    .Include("~/Scripts/jquery-{version}.js")
                    .Include("~/Scripts/knockout-{version}.js")
                    .Include("~/Scripts/knockout.mapping-{version}.js")
                    .Include("~/Scripts/modernizr-*")
                    .Include("~/Scripts/q.js")
                    .Include("~/Scripts/breeze.js")
                    .Include("~/Scripts/toastr.js")
                    .Include("~/Scripts/bootstrap.js")
                );


            bundles.Add(
                new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-responsive.css",
                      "~/Content/durandal.css",
                      "~/Content/toastr.css",
                      //always keep the side css at the bottom
                      "~/Content/site.css"));
        }
    }
}
