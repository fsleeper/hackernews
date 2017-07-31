using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using Common.Logging;

namespace HackerNewsWeb
{
    public class BundleConfig
    {
        private static readonly ILog Log = LogManager.GetLogger<BundleConfig>();

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            Log.Debug("Bundler Wiring Up");

            // TBD if I need Angular UI Bootstrap

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/angular*.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/angular/app.js",
                "~/angular/controller/*.js",
                "~/angular/service/*.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/bootstrap-theme.css",
                 "~/Content/Site.css"));
        }
    }
}
