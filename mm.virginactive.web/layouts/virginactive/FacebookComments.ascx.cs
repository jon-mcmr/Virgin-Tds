using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Collections;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class FacebookComments : System.Web.UI.UserControl
    {
        protected string ScriptSourceURL = "";
        protected string SiteURL = "";
        protected string NumberOfPosts = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            //Get number of posts shown
            Item FacebookComments = Sitecore.Context.Item;
            NumberOfPosts = FacebookComments.Fields["Number of posts shown"].Value;

            //Get Facebook Settings
            ScriptSourceURL = Sitecore.Configuration.Settings.GetSetting("Virgin.FacebookScriptURL");
            SiteURL = Sitecore.Configuration.Settings.GetSetting("Virgin.SiteURL");

        }
    }
}