using System.Web.Optimization;

namespace Backoffice0._1.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            bundles.Add(new StyleBundle("~/bundles/css").
               Include(
               "~/Content/production/css/bootstrap.min.css",
               "~/Content/production/css/icheck/flat/green.css",
               "~/Content/production/fonts/css/font-awesome.css",
               "~/Content/production/css/custom.css",
               "~/Content/production/fonts/css/font-awesome.css",
               "~/Content/production/css/maps/jquery-jvectormap-2.0.3.css",
               "~/Content/production/css/select/select2.min.css",
               "~/Content/production/css/floatexamples.css",
               "~/Content/production/css/animate.min.css"
           ));
            bundles.Add(new ScriptBundle("~/bundles/js").
                Include(
                "~/Content/production/js/jquery.min.js",
                "~/Content/production/js/select/select2.full.js",
                "~/Content/production/js/bootstrap.min.js",
                "~/Content/production/js/custom.js",
                "~/Content/production/js/custom2.js",
                "~/Content/production/js/input_mask/jquery.inputmask.js",
                "~/Content/production/js/alert/sweetalert.min.js",
                "~/Content/production/js/wizard/jquery.smartWizard.js",
                "~/Content/production/js/nprogress.js",
                "~/Content/production/js/pace/pace.min.js",
                "~/Content/production/js/gauge/gauge.min.js",
                "~/Content/production/js/pace/pace.min.js",
                "~/Content/production/js/gauge/gauge.min.js",
                "~/Content/production/js/gauge/gauge_demo.js",
                "~/Content/production/js/progressbar/bootstrap-progressbar.min.js",
                "~/Content/production/js/nicescroll/jquery.nicescroll.min.js",
                "~/Content/production/js/icheck/icheck.min.js",
                "~/Content/production/js/moment/moment.min.js",
                "~/Content/production/js/datepicker/daterangepicker.js",
                "~/Content/production/js/chartjs/chart.min.js",
                "~/Content/production/js/excanvas.min.js",
                "~/Content/production/js/flot/jquery.flot.js",
                "~/Content/production/js/flot/jquery.flot.pie.js",
                "~/Content/production/js/flot/jquery.flot.orderBars.js",
                "~/Content/production/js/flot/jquery.flot.time.min.js",
                "~/Content/production/js/flot/date.js",
                "~/Content/production/js/flot/jquery.flot.spline.js",
                "~/Content/production/js/flot/jquery.flot.stack.js",
                "~/Content/production/js/flot/curvedLines.js",
                "~/Content/production/js/flot/jquery.flot.resize.js",
             
                "~/Content/production/js/datatables/jquery.dataTables.min.js",
                "~/Content/production/js/datatables/dataTables.bootstrap.js",
                "~/Content/production/js/datatables/dataTables.buttons.min.js",
                "~/Content/production/js/datatables/buttons.bootstrap.min.js",
                "~/Content/production/js/datatables/jszip.min.js",
                "~/Content/production/js/datatables/pdfmake.min.js",
                "~/Content/production/js/datatables/vfs_fonts.js",
                "~/Content/production/js/datatables/buttons.html5.min.js",
                "~/Content/production/js/datatables/buttons.print.min.js",
                "~/Content/production/js/datatables/dataTables.fixedHeader.min.js",
                "~/Content/production/js/datatables/dataTables.keyTable.min.js",
                "~/Content/production/js/datatables/dataTables.responsive.min.js",
                "~/Content/production/js/datatables/responsive.bootstrap.min.js",
                "~/Content/production/js/datatables/dataTables.scroller.min.js",
                "~/Content/production/js/progressbar/bootstrap-progressbar.min.js",
                "~/Content/production/js/skycons/skycons.min.js"
                ));
        }
    }
}
  
   






    