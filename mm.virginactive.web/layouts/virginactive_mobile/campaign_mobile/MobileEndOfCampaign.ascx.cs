using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Globalization;
using mm.virginactive.controls.Model;
using Sitecore.Links;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.Mobile.Campaigns
{
    public partial class MobileEndOfCampaign : System.Web.UI.UserControl
    {
        protected string homePageUrl;
        protected string ClubFinderUrl = "";
        protected string TimetablesUrl = "";
        protected string MembershipEnquiryUrl = "";	
        protected CampaignBaseItem campaign = new CampaignBaseItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            //Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            //urlOptions.AlwaysIncludeServerUrl = true;
            //urlOptions.AddAspxExtension = true;
            //urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            //var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath.ToString());
            //homePageUrl = Sitecore.Links.LinkManager.GetItemUrl(homeItem, urlOptions);

            ClubFinderUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.ClubFinder);
            TimetablesUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.ClubFinder) + "?action=timetables";
            MembershipEnquiryUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.EnquiryForm);

        }
    }
}