using System.Web;
using System.Web.Optimization;

namespace Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/BForms")
                //BForms CSS bundle
                .Include("~/Scripts/BForms/Bundles/css/*.css", new CssRewriteUrlTransform())
                //Site CSS bundle
                .Include("~/Content/StyleSheets/*.css", new CssRewriteUrlTransform())
                );
            bundles.Add(new StyleBundle("~/homePage")
                //BForms CSS bundle
                .Include("~/Scripts/BForms/Bundles/homeCSS/*.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/authPage")
                //BForms CSS bundle
                .Include("~/Scripts/BForms/Bundles/loginCss/*.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/profilePage")
                //BForms CSS bundle
                .Include("~/Scripts/BForms/Bundles/profileCss/*.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/BForms/Bundles/font-awesome/css/*.css", new CssRewriteUrlTransform()));

            //Site CSS bundle
            //.Include("~/Content/StyleSheets/*.css", new CssRewriteUrlTransform())
            //);
        }
    }
}
