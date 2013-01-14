using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Data.Items;
using Sitecore.Web;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingAccreditations : System.Web.UI.UserControl
    {
        protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetupSitecoreData();
                BindData();
            }

        }


        private void SetupSitecoreData()
        {
            CheckIfProfileProvided();
        }

        private void BindData()
        {
            List<ImageCarouselItem> imageCarouselItems = null;
            string sectionName = Settings.LandingPagesAccreditationsSection;

            //string path = currentItem.InnerItem.Paths.ContentPath;

            Item whyItem = currentItem.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@name='{0}']", sectionName));

            Item[] items = whyItem.Axes.SelectItems("*");

            if (items != null)
            {
                imageCarouselItems = items.ToList().ConvertAll(X => new ImageCarouselItem(X));
            }

            if (imageCarouselItems != null)
            {
                rpAccreditations.DataSource = imageCarouselItems;
                rpAccreditations.DataBind();
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
    }
}