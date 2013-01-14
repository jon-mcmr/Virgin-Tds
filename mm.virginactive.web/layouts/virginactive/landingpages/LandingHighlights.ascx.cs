using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingHighlights : System.Web.UI.UserControl
    {
		protected LandingEnquiryItem currentItem = new LandingEnquiryItem(Sitecore.Context.Item);
		protected LandingPageItem currentLanding = null;
		protected ClubItem clubItem = null;
		protected string clubId;
		protected string profile;
		protected string region;

        protected void Page_Load(object sender, EventArgs e)
        {

			if (!Page.IsPostBack)
			{
				SetSitecoreData();
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
				//Get the club highlights
				//List<HighlightPanelItem> highlights = clubItem.ClubHighlights.ListItems.ConvertAll(X => new HighlightPanelItem(X));

				List<HighlightPanelItem> highlights = new List<HighlightPanelItem>();

				Item parentHighlight = clubItem.InnerItem.Axes.SelectSingleItem(String.Format("descendant::*[@@tid='{0}']", Settings.LandingPagesHighlightContainerTemplate));

				Item[] childHighlights = null;
				if (parentHighlight != null)
				{
					childHighlights = parentHighlight.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", Settings.LandingPagesHighlightTemplate));

					HighlightPanelItem highlight;

					if (childHighlights != null)
					{
						foreach (Item item in childHighlights)
						{
							highlight = new HighlightPanelItem(item);
							highlights.Add(highlight);
						}
					}
				}

				List<HighlightPanelItem> sharedHighlights = clubItem.ClubHighlights.ListItems.ConvertAll(X => new HighlightPanelItem(X));

				if (sharedHighlights != null && sharedHighlights.Count>0)
				{
					foreach (HighlightPanelItem item in sharedHighlights)
					{
						highlights.Add(item);
					}
				}


				if (highlights != null && highlights.Count > 0)
				{
					HighlightPanels.DataSource = highlights;
					HighlightPanels.DataBind();
				}
			}



		}
    }
}