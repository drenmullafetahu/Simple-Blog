using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace authcontroller.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/admin/styles").Include("~/content/styles/Admin.css").Include("~/content/styles/bootstrap.css"));
            bundles.Add(new StyleBundle("~/styles").Include("~/content/styles/Site.css").Include("~/content/styles/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/admin/javascript").Include("~/content/scripts/jquery-1.11.2.min.js").Include("~/content/scripts/bootstrap.min.js").Include("~/content/scripts/Form.js"));



        }
    }
}