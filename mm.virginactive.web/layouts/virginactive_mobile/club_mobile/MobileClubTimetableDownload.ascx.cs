using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;
using Sitecore.Links;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileClubTimetableDownload : System.Web.UI.UserControl
    {
        protected TimetableDownloadItem currentItem = new TimetableDownloadItem(Sitecore.Context.Item);
        protected string ClubName = "";
        protected string ClubHomeUrl = "";
		protected ClubItem currentClub;
		protected string BookOnlineUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            currentClub = SitecoreHelper.GetCurrentClub(currentItem);

            if (currentClub != null)
            {
                ClubHomeUrl = LinkManager.GetItemUrl(currentClub.InnerItem);
                ClubName = currentClub.Clubname.Raw;

				//Set Book Online Link
				if (currentClub.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric || currentClub.GetCrmSystem() == ClubCrmSystemTypes.Vision)
				{
					//Show link
					pnlBookOnline.Visible = true;
					BookOnlineUrl = SitecoreHelper.GetMembershipLoginUrl(currentClub);
				}
            }

            if (currentItem.InnerItem.HasChildren)
            {
                List<TimetableDownloadModuleItem> moduleList = currentItem.InnerItem.Children.ToList().ConvertAll(X => new TimetableDownloadModuleItem(X));

                DownloadModuleListing.DataSource = moduleList;
                DownloadModuleListing.DataBind();
            }

            //Set club last visited 
            User objUser = new User();
            if (Session["sess_User"] != null)
            {
                objUser = (User)Session["sess_User"];
            }
            objUser.ClubLastVisitedID = currentClub.ClubId.Rendered;
            Session["sess_User"] = objUser;

            //Set club last visited cookie
            CookieHelper.AddClubsLastVisitedCookie(CookieKeyNames.ClubLastVisited, currentClub.ClubId.Rendered);


            //Add club name to page title     
            string clubNameTitle = String.Format(" - {0}", currentClub.Clubname.Raw);
            clubNameTitle = HtmlRemoval.StripTagsCharArray(clubNameTitle);

            Page.Title = Page.Title + clubNameTitle;

        }
    }
}