using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CMS.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/allscript/")
                .Include("~/scripts/jquery/v3.1.1/jquery-3.1.1.min.js")
                .Include("~/scripts/jqueryui/v1.12.1/jquery-ui.min.js")
                .Include("~/scripts/jqueryui/v1.12.1/jquery.ui.widget.js")
                .Include("~/scripts/bootstrap/v3.3.7/bootstrap.min.js")
                .Include("~/scripts/datetimepicker/v2.5.4/jquery.datetimepicker.full.min.js")
                .Include("~/scripts/colorbox/v1.6.4/jquery.colorbox-min.js")
                .Include("~/scripts/autosize/v3.0.20/autosize.min.js")
                .Include("~/scripts/inputmask/v3.3.6/jquery.inputmask.bundle.min.js")
                .Include("~/scripts/inputmask/v3.3.6/inputmask.binding.js")
                .Include("~/scripts/html2canvas/v1.0.0/html2canvas.js")
                .Include("~/scripts/angularjs/v1.6.6/angular.js")
                .Include("~/scripts/angularfilter/v0.5.17/angular-filter.js")
                .Include("~/modules/sails/admin/app.module.js")
                .Include("~/scripts/datatable/v1.10.16/jquery.datatables.min.js")
                .Include("~/scripts/datatable/v1.10.16/datatables.bootstrap.min.js")
                .Include("~/scripts/jqueryvalidation/v1.17.0/jquery.validate.min.js")
                .Include("~/scripts/jqueryvalidation/v1.17.0/additional-methods.min.js")
                .Include("~/scripts/scrollsneak/scrollsneak.js")
                .Include("~/scripts/filesaver/filesaver.js")
                .Include("~/scripts/blob/blob.js")
                .Include("~/scripts/sticky/v1.12.3/jquery.sticky-kit.js")
                .Include("~/scripts/sticky/v1.0.4/jquery.sticky.js")
                .Include("~/scripts/readmore/readmore.js"));
        }
    }
}