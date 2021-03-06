﻿using System.Web.Optimization;
using System.Web.Optimization.React;

namespace Swappler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //
            // Script Bundles
            //

            // Swappler
            bundles.Add(new ScriptBundle("~/bundles/swappler")
                .Include(
                    "~/Public/Scripts/swappler.js",
                    "~/Public/Scripts/swappler.utils.js",
                    "~/Public/Scripts/swappler.register.js",
                    "~/Public/Scripts/swappler.login.js",
                    "~/Public/Scripts/swappler.index.js",
                    "~/Public/Scripts/swappler.publishitem.js",
                    "~/Public/Scripts/swappler.editprofile.js",
                    "~/Public/Scripts/swappler.search.js"
                ));

            // JQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                    "~/Public/Scripts/jquery-{version}.js",
                    "~/Public/Scripts/jquery.validate.js",
                    "~/Public/Scripts/additional-methods.js"
                ));

            // Modernizr
            //
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include(
                    "~/Public/Scripts/modernizr-*"
                ));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include(
                    "~/Public/Scripts/bootstrap.js",
                    "~/Public/Scripts/respond.js"
                ));

            // React JS
            bundles.Add(new ScriptBundle("~/bundles/react")
                .Include(
                    "~/Public/Scripts/react/react-{version}.js",
                    "~/Public/Scripts/react/react-dom-{version}.js",
                    "~/Public/Scripts/react/react-with-addons-{version}.js"
                ));

            // Buble Browser
            bundles.Add(new ScriptBundle("~/bundles/babel")
                .Include(
                    "~/Public/Scripts/Babel/browser.js"
                ));

            bundles.Add(new BabelBundle("~/bundles/swappler.jsx")
                .Include("~/Public/Scripts/Swappler.UI.jsx")
                );

            //
            // Css Bundles
            //
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(
                    "~/Public/Css/bootstrap.css",
                    "~/Public/Css/site.css",
                    "~/Public/Css/font-awesome.css",
                    "~/Public/Css/animate.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/signalr")
                .Include(
                    "~/Public/Scripts/jquery.signalR-2.2.0.js",
                    "~/signalr/hubs"
                ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
