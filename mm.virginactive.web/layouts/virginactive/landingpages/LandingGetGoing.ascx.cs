using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using Sitecore.Web;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Util;
using mm.virginactive.controls.Model;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingGetGoing : System.Web.UI.UserControl
    {
        protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetSitecoreData();
                CheckIfRegionProvided();
            }
        }

        private void SetSitecoreData()
        {
            CheckIfProfileProvided();
            litButtonText.Text = currentItem.Panel4ButtonText.Rendered;
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

        private void CheckIfRegionProvided()
        {
            string regionId = WebUtil.GetQueryString("_region");
            //Check Session
            User objUser = new User();
			if (Session["sess_User_landing"] != null)
            {
				objUser = (User)Session["sess_User_landing"];
                regionId = objUser.BrowsingHistory.LandingRegion;
            }

            if (!String.IsNullOrEmpty(regionId))
            {
                LocationPointerItem pointerItem = null;

                pointerItem = GymLocationManager.GetLocation(regionId);

                if (pointerItem != null)
                {
                    litButtonText.Text = String.Format(litButtonText.Text, "in " + pointerItem.LocationName.Rendered + " ");
                  
                }
                else
                {
                    litButtonText.Text = String.Format(litButtonText.Text, "");

                }
            }
            else
            {
                litButtonText.Text = String.Format(litButtonText.Text, "");
            
            }
        }
    }
}