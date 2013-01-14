using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Collections;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class FacebookLikeButton : System.Web.UI.UserControl
    {
        protected string ScriptSourceURL = "";
        protected FacebookLikeButtonItem facebookLikeButton;

        public FacebookLikeButtonItem ContextFacebookButtonItem
        {
            get { return facebookLikeButton; }
            set
            {
                facebookLikeButton = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (facebookLikeButton != null)
            {
                //Get Facebook Settings
                ScriptSourceURL = Sitecore.Configuration.Settings.GetSetting("Virgin.FacebookScriptURL");
                string AdministratorIDs = Sitecore.Configuration.Settings.GetSetting("Virgin.FacebookAdministratorIDs");

                //Get like button (and Open Graph Meta Data) details (this dictates the content shown in the facebook user's feed).
                string LinkTitle = facebookLikeButton.Linktitle.Rendered;
                string LinkURL = facebookLikeButton.Linkurl.Rendered;
                string ImageURL = facebookLikeButton.Thumbnailimageurl.Rendered;
                string LinkDescription = facebookLikeButton.Linkdescription.Rendered;
                string SiteName = facebookLikeButton.Sitename.Rendered;

                System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

                markupBuilder.Append(@"<!--Facebook Open Graph Meta Data for like button --> ");
                markupBuilder.AppendFormat(@"<meta property=""og:title"" content=""{0}"" /> ", LinkTitle);
                markupBuilder.Append(@"<meta property=""og:type"" content=""sport"" /> ");
                markupBuilder.AppendFormat(@"<meta property=""og:url"" content=""{0}"" /> ", LinkURL);
                markupBuilder.AppendFormat(@"<meta property=""og:image"" content=""{0}"" /> ", ImageURL);
                markupBuilder.AppendFormat(@"<meta property=""og:description"" content=""{0}"" /> ", LinkDescription);
                markupBuilder.AppendFormat(@"<meta property=""og:site_name"" content=""{0}"" /> ", SiteName);
                markupBuilder.AppendFormat(@"<meta property=""og:admins"" content=""{0}"" />", AdministratorIDs);

                //Add to the page haeder
                HtmlHead head = (HtmlHead)Page.Header;
                head.Controls.Add(new LiteralControl(markupBuilder.ToString()));
            }
        }
    }
}