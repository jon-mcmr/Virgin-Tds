using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Globalization;
using mm.virginactive.controls.Model;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class EndOfCampaign : System.Web.UI.UserControl
    {
        protected string homePageUrl;
        protected CampaignBaseItem campaign = new CampaignBaseItem(Sitecore.Context.Item);

        public string HomePageUrl
        {
            get { return homePageUrl; }
            set
            {
                homePageUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = true;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath.ToString());
            homePageUrl = Sitecore.Links.LinkManager.GetItemUrl(homeItem, urlOptions);

            //Add dynamic content to header
            HtmlHead head = (HtmlHead)Page.Header;

            //Add Open Tag
            if (Session["sess_User"] != null)
            {
                User objUser = (User)Session["sess_User"];
                if (objUser.Preferences != null)
                {
                    if (objUser.Preferences.MarketingCookies && objUser.Preferences.MetricsCookies)
                    {
                        head.Controls.Add(new LiteralControl(OpenTagHelper.OpenTagVirginActiveUK));
                    }
                }
            }

            //Add Page Title	  	
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
            markupBuilder.Append(@"<meta name='viewport' content='width=1020'>");
            markupBuilder.Append(@"<link rel='apple-touch-icon' href='/virginactive/images/apple-touch-icon.png'>");
            markupBuilder.Append(@"<link rel='shortcut icon' href='/virginactive/images/favicon.ico'>");
            markupBuilder.Append(@"<link href='/virginactive/css/fonts.css' rel='stylesheet'>");
            markupBuilder.Append(@"<link href='/va_campaigns/css/campaign.css' rel='stylesheet'>");
            head.Controls.Add(new LiteralControl(markupBuilder.ToString()));


        }
    }
}