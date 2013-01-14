using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Resources.Media;
using Sitecore;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using Sitecore.Web;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingDemo : System.Web.UI.UserControl
    {
        protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);
        private bool _useVideo;  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _useVideo = true;
                
                SetSitecoreData();
            }

        }

        private void SetSitecoreData()
        {
            CheckIfProfileProvided();
            ltHeading.Text = currentItem.Panel1Heading.Rendered;
            ltText.Text = currentItem.Panel1Text.Rendered;
			ltIntroHeading.Text = currentItem.Panel1IntroHeading.Rendered;
			ltIntro.Text = currentItem.Panel1Intro.Rendered;

            videoLink.Attributes.Add("src", currentItem.Panel1Video.Rendered+"?wmode=transparent");
            _useVideo = currentItem.UseVideo.Checked;

            if (_useVideo)
            {
				phImage.Visible = false;
                videoLink.Visible = true;
            }
            else
            {
				phImage.Visible = true;
                videoLink.Visible = false;
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