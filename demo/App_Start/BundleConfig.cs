using System.Web;
using System.Web.Optimization;

namespace demo
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery.min.js"));
			bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
						"~/Scripts/jquery-ui.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.min.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.min.css",
					  "~/Content/site.css",
					  "~/Content/scrollbar.css",
					  "~/Content/style.css",
					  "~/Content/jquery-ui.min.css"));

			bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
					  "~/Content/font-awesome.css"));

			bundles.Add(new ScriptBundle("~/bundles/scrollbar").Include(
				  "~/Scripts/jquery.scrollbar.js"));
			bundles.Add(new ScriptBundle("~/bundles/notify").Include(
				  "~/Scripts/notify.js"));


		}
	}
}
