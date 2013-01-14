using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using System.Text;
using System.Collections;
using mm.virginactive.common.Helpers;
using Sitecore.Diagnostics;
using mm.virginactive.screportdal;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.common.Globalization;
using Sitecore.Links;
using Sitecore.Resources.Media;
using mm.virginactive.controls.Model;
using Sitecore.Web;

namespace mm.virginactive.web.layouts.virginactive.indoortriathlon
{
    public partial class IndoorIFrame : System.Web.UI.UserControl
    {
        protected IndoorIFrameItem currentItem = new IndoorIFrameItem(Sitecore.Context.Item);
        //protected IndoorFormItem registrationForm;
        protected LinkWidgetItem registrationForm;
        protected string pageUrl;
        protected bool showSocial = false;

        public Boolean ShowSocial
        {
            get { return showSocial; }
            set
            {
                showSocial = value;
            }
        }

        public string PageUrl
        {
            get { return pageUrl; }
            set
            {
                pageUrl = value;
            }
        }

        public string Iframe
        {
            //get { return String.Format("<iframe src=\"{0}#http://local.virginactive\" id=\"Iframe\" width=\"{1}\" height=\"{2}\" frameborder=\"0\"></iframe>", currentItem.IFrameUrl.Text, currentItem.IFrameWidth.Text, currentItem.IFrameHeight.Text); }
            get { return String.Format("<iframe src=\"{0}#{3}\" id=\"Iframe\" width=\"{1}\" height=\"{2}\" frameborder=\"0\"></iframe>", currentItem.IFrameUrl.Text, currentItem.IFrameWidth.Text, currentItem.IFrameHeight.Text, "http://" + WebUtil.GetHostName()); }
			
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get widget details
            registrationForm = Sitecore.Context.Database.GetItem(ItemPaths.IndoorTriathlonRegistrationForm);

            //Add dynamic content
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/indoortriathlon/indoor-tri.js""></script>"));

            if (Session["sess_User"] != null)
            {
                User objUser = (User)Session["sess_User"];

                if (objUser.Preferences.SocialCookies)
                {

                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = true;
                    urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                    pageUrl = Sitecore.Links.LinkManager.GetItemUrl(currentItem, urlOptions);

                    //Have permission to load in Social
                    showSocial = true;
                }
            }

        }

    }
}