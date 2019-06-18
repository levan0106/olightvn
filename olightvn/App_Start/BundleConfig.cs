using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace olightvn
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycookie").Include(
                        "~/Scripts/jquery.cookie*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryextend").Include(
                      "~/Content/Jqueryextend/chosen.jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Content/js/Plugin.js",
                        "~/Content/js/Angular/Angular.min.js",
                        "~/Content/js/Angular/angular-route.js",
                        "~/Content/js/Angular/angular-animate.js",
                        "~/Content/js/Angular/angular-touch.js",
                        "~/Content/js/lazy/jquery.lazyloading.js",
                        "~/Content/js/olightvn.js",
                        "~/Content/js/olightvn.Routings.js",
                        "~/Content/js/olightvn.Controllers.js"));

            bundles.Add(new ScriptBundle("~/bundles/lvslider").Include(
                "~/Content/lvslider/lvslider.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryextend/css").Include(
                      "~/Content/Jqueryextend/chosen.css"));

            bundles.Add(new StyleBundle("~/Content/lvslider").Include(
                 "~/Content/lvslider/lvslider.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                        "~/Content/css/font-awesome.css", 
                      "~/Content/css/master.css"));
            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.css", 
                      "~/Content/admin/css/master.css"));
        }
    }
}