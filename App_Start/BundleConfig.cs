using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TMS.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //MasterPage Script Files
            bundles.Add(new ScriptBundle("~/Scripts/Master").Include("~/Library/Scripts/nprogress.js", "~/Library/Scripts/jquery.min.js", "~/Library/Scripts/bootstrap.bundle.min.js","~/Library/Scripts/simplebar.min.js", "~/Library/Scripts/hotkeys.min.js", "~/Library/Scripts/apexcharts.js", "~/Library/Scripts/moment.min.js", "~/Library/Scripts/daterangepicker.js", "~/Library/Scripts/quill.js", "~/Library/Scripts/toastr.min.js", "~/Library/Scripts/mono.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Master_Charts").Include("~/Library/Scripts/chart.js", "~/Library/Scripts/custom.js"));

            //MasterPage Css Files 
            bundles.Add(new StyleBundle("~/CSS/Master").Include("~/Library/CSS/font.css", "~/Library/CSS/Icons/material/css/materialdesignicons.min.css", "~/Library/CSS/simplebar.css", "~/Library/CSS/nprogress.css", "~/Library/CSS/daterangepicker.css", "~/Library/CSS/quill.snow.css", "~/Library/CSS/toastr.min.css", "~/Library/CSS/style.css"));

        }
    }
}