using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TravelGuide.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // CSS Bundle
            bundles.Add(new StyleBundle("~/css/all").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/validation-summary.css",
                "~/Content/clean-blog.min.css",
                "~/Content/hint.min.css",
                "~/Content/Site.css",
                "~/Content/Gridmvc.css",
                "~/Content/gridmvc.datepicker.min.css"
                ));

            // JS Bundle
            bundles.Add(new ScriptBundle("~/js/all").Include(
                 "~/Scripts/jquery-3.1.1.min.js",
                 "~/Scripts/bootstrap.min.js",
                 "~/Scripts/clean-blog.min.js",
                 "~/Scripts/jquery.validate.min.js",
                 "~/Scripts/jquery.validate.unobtrusive.min.js",
                 "~/Scripts/jqBootstrapValidation.js",
                 "~/Scripts/modernizr-2.6.2.js",
                 "~/Scripts/gridmvc.min.js",
                 "~/Scripts/gridmvc.customwidgets.js",
                 "~/Scripts/gridmvc-ext.js"
                ));
        }
    }
}