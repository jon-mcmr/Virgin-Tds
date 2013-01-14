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

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingHeader : System.Web.UI.UserControl
    {
        public string HomeOrClubPageUrl
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Set featured promo link and home page link
                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                urlOptions.AlwaysIncludeServerUrl = true;
                urlOptions.AddAspxExtension = true;
                urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                //Set home page link (set as club home if home club set)
                string startPath = Sitecore.Context.Site.StartPath.ToString();
                var homeItem = Sitecore.Context.Database.GetItem(startPath);

                HomeOrClubPageUrl = Sitecore.Links.LinkManager.GetItemUrl(homeItem, urlOptions);
            }
       
        }

   }
}