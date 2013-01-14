using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive_mobile.landingpages
{
    public partial class MobileLandingPagePromo : System.Web.UI.UserControl
    {
        protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);
        protected string ClubListUrl = "";
        protected string ClubResultsUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            ClubResultsUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.ClubResults);
            ClubListUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.ClubFinder) + "?action=list";
            //Display the correct panel -this page is used for 

        }
    }
}