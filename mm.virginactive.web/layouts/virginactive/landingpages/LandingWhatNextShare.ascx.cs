using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using Sitecore.Links;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingWhatNextShare : System.Web.UI.UserControl
    {
        protected LandingWhatsNextItem currentItem = new LandingWhatsNextItem(Sitecore.Context.Item);
		
		protected string OriginalReferer { get; set; }
		protected string TwitterShareText { get; set; }
		protected string TwitterUsername { get; set; }
		protected string FacebookShareText { get; set; }
		protected string GoogleShareText { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
			LandingPageItem currentLanding = null;
			
			if(currentItem!=null)
				currentLanding = new LandingPageItem(currentItem.InnerItem.Parent);

			if (currentLanding != null)
			{
				Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
				urlOptions.AlwaysIncludeServerUrl = true;
				urlOptions.AddAspxExtension = false;
				urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

				OriginalReferer = currentLanding.UrlToTweet.Rendered;
				TwitterShareText = currentItem.TwitterShareText.Rendered;
				FacebookShareText = currentItem.FacebookShareText.Rendered;
				GoogleShareText = currentItem.GooglePlusShareText.Rendered;
				
				if (currentItem.InnerItem.Parent != null)
				{
					TwitterUsername = new LandingPageItem(currentItem.InnerItem.Parent).Username.Rendered;
				}
			}
        }
    }
}