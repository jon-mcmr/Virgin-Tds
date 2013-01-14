using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using Sitecore.Resources.Media;
using Sitecore.Data.Fields;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using Sitecore.Collections;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using Sitecore.Web;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingFooter : System.Web.UI.UserControl
    {
        protected string footerLinks;
        protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);
      

        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!Page.IsPostBack)
            {
                CheckIfProfileProvided();
                SetFooterLinks();
            }
        }

        private void CheckIfProfileProvided()
        {
            string profile = string.Empty;
            profile = WebUtil.GetQueryString("_profile");

            //Get User from Session
            //Check Session
            User objUser = new User();
			if (Session["sess_User_landing"] != null)
            {
				objUser = (User)Session["sess_User_landing"];
                profile = objUser.BrowsingHistory.LandingProfile;
            }


            if (!String.IsNullOrEmpty(profile))
            {
                Item profileItem = SitecoreHelper.GetContextLandingItem(currentItem, profile);

                if (profileItem != null)
                {
                    currentItem = profileItem;
                }
            }
        }

        private void SetFooterLinks()
        {
           
            List<Item> links = currentItem.FooterLinks.ListItems;

            if (links == null || links.Count < 1)
            {
                links = new LandingPageItem(currentItem.InnerItem.Parent).FooterLinks.ListItems;
            }

            System.Text.StringBuilder markupBuilderSub = new System.Text.StringBuilder();

            foreach (Item item in links)
            {
                PageSummaryItem psItem = new PageSummaryItem(item);

                if (!string.IsNullOrEmpty(psItem.NavigationTitle.Text))
                {
                    string url = Sitecore.Links.LinkManager.GetItemUrl(psItem.InnerItem);
                    markupBuilderSub.AppendFormat(@" <strong>/</strong> <a href=""{0}"">{1}</a>", url, psItem.NavigationTitle.Rendered);
                }
            }

            if (markupBuilderSub.Length > 0)
            {
                footerLinks = markupBuilderSub.ToString();
            }
        }

    }
}