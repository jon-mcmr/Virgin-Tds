using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.MobileTemplates;
using mm.virginactive.common.Globalization;


namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileClubFinder : System.Web.UI.UserControl
    {
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