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
using mm.virginactive.controls.Util;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
	public partial class LandingPromoPanel : System.Web.UI.UserControl
    {
        protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);
        protected ProfileLandingPageItem contextItem;

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
            ltHeading.Text = currentItem.Heading.Rendered;
            ltSubHeading.Text = currentItem.Subheading.Rendered;
            ltButtonText.Text = currentItem.Buttontext.Rendered;
            ltCallToAction.Text = currentItem.Calltoactiontext.Rendered;
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

            HtmlGenericControl BodyTag = (HtmlGenericControl)this.Page.FindControl("BodyTag");
            BodyTag.Attributes.Add("data-landing", currentItem.ID.ToShortID().ToString());
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
                    ltButtonText.Text = String.Format(ltButtonText.Text, "in " + pointerItem.LocationName.Rendered + " ");
                    ltCallToAction.Text = String.Format(ltCallToAction.Text, pointerItem.LocationName.Rendered + " ");
                }
                else
                {
                    ltButtonText.Text = String.Format(ltButtonText.Text, "");
                    ltCallToAction.Text = String.Format(ltCallToAction.Text, "");
                }
            }
            else
            {
                ltButtonText.Text = String.Format(ltButtonText.Text, "");
                ltCallToAction.Text = String.Format(ltCallToAction.Text, "");
            }
        }
   }
}