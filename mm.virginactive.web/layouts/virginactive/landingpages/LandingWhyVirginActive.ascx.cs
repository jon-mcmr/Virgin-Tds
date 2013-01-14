using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Web;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Util;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingWhyVirginActive : System.Web.UI.UserControl
    {
        protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);

        protected string Panel2Heading  { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                SetupSitecoreData();
                BindData();
                CheckIfRegionProvided();
            }
               
        }


        private void SetupSitecoreData()
        {
            CheckIfProfileProvided();
            Panel2Heading = currentItem.Panel2Heading.Rendered;
        }

        private void BindData()
        {
            List<HighlightPanelItem> highlightItems = null;
            string sectionName = Settings.LandingPagesWhyVirginActiveSection;
            
            //string path = currentItem.InnerItem.Paths.ContentPath;

            Item whyItem = currentItem.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@name='{0}']", sectionName));
          
            Item[] items = whyItem.Axes.SelectItems("*");

            if (items != null)
            {
                highlightItems = items.ToList().ConvertAll(X => new HighlightPanelItem(X));
            }

            if (highlightItems != null)
            {
                rpWhy.DataSource = highlightItems;
                rpWhy.DataBind();
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
                    Panel2Heading = String.Format(Panel2Heading, pointerItem.LocationName.Rendered + " ");

                }
                else
                {

                    Panel2Heading = String.Format(Panel2Heading, "");

                }
            }
            else
            {
                Panel2Heading = String.Format(Panel2Heading, "");

            }
        }
    }
}