using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Collections;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class TwitterFollowButton : System.Web.UI.UserControl
    {
        protected string ScriptSourceURL = "";
        protected TwitterFollowButtonItem twitterFollowButton;

        public TwitterFollowButtonItem ContextTwitterButtonItem
        {
            get { return twitterFollowButton; }
            set
            {
                twitterFollowButton = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (twitterFollowButton != null)
            {
                //Get Twitter Settings
                ScriptSourceURL = Sitecore.Configuration.Settings.GetSetting("Virgin.TwitterScriptURL");
            }
        }
    }
}