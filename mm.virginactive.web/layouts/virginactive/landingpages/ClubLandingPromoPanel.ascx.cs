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
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using Sitecore;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using Sitecore.Web;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
	public partial class ClubLandingPromoPanel : System.Web.UI.UserControl
    {
		protected LandingEnquiryItem currentItem = new LandingEnquiryItem(Sitecore.Context.Item);
		protected LandingPageItem currentLanding = null;
		protected ClubItem clubItem = null;
		protected string clubId;
		protected string profile;
		protected string region;
		protected string membershipTeaser;
        private string _landingUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _landingUrl = Sitecore.Links.LinkManager.GetItemUrl(currentItem);
            SetSitecoreData();
            ShowHideSearchAgainLink();

        }

        /// <summary>
        /// Check if landing page url is set in session. If yes then show the search again button
        /// </summary>
        private void ShowHideSearchAgainLink()
        {
            User objUser = new User();
			if (Session["sess_User_landing"] != null)
            {
				objUser = (User)Session["sess_User_landing"];

                if (objUser.BrowsingHistory != null)
                {
                    if (_landingUrl != objUser.BrowsingHistory.LandingPageUrl)
                    {
						phSearchAgain.Visible = true;
                    }
                }


            }
        }

        private void SetSitecoreData()
        {
			//clubId = WebUtil.GetQueryString("_clubid");
			//profile = WebUtil.GetQueryString("_profile");

			//Get User from Session
            //Check Session
			User objUser = new User();
			if (Session["sess_User_landing"] != null)
            {
				objUser = (User)Session["sess_User_landing"];

				clubId = objUser.BrowsingHistory.LandingClubID;
				profile = objUser.BrowsingHistory.LandingProfile;
				region = objUser.BrowsingHistory.LandingRegion;
            }

			//Get Club Data
			
			if (!String.IsNullOrEmpty(clubId))
				clubItem = SitecoreHelper.GetClubOnClubId(clubId);

			if (clubItem != null)
			{
				//Get Personal Membership Item
				MembershipListingItem membership = clubItem.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant::*[@@tid = '{0}']", MembershipListingItem.TemplateId));

				if (membership != null)
				{
					membershipTeaser = membership.MembershipTeaser.Rendered;
				}
				
				//ltButtonText.Text = String.Format(ltButtonText.Text, "in " + clubItem.Clubname.Rendered + " ");
				//ltCallToAction.Text = String.Format(ltCallToAction.Text, clubItem.Clubname.Rendered + " ");
			}

			if (!String.IsNullOrEmpty(profile))
			{
				//Get Profile Landing Data
				currentLanding = SitecoreHelper.GetContextLandingItem(currentItem, profile);
			}
			else
			{
				currentLanding = currentItem.InnerItem.Axes.SelectSingleItem(String.Format("ancestor-or-self::*[@@tid='{0}']", LandingPageItem.TemplateId));
			}

			HtmlGenericControl BodyTag = (HtmlGenericControl)this.Page.FindControl("BodyTag");
			BodyTag.Attributes.Add("data-landing", currentItem.ID.ToShortID().ToString());
	
        }
   }
}