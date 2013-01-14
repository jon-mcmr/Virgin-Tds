using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;


namespace mm.virginactive.web.layouts.virginactive
{
    public partial class ArticleBody : System.Web.UI.UserControl
    {

        protected YourHealthArticleItem article = new YourHealthArticleItem( Sitecore.Context.Item );
        protected YourHealthListingItem listing;
        protected Boolean showSocial = false;
        protected string articleUrl;

        public Boolean ShowSocial
        {
            get { return showSocial; }
            set
            {
                showSocial = value;
            }
        }

        public string ArticleUrl
        {
            get { return articleUrl; }
            set
            {
                articleUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                listing = new YourHealthListingItem( article.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", YourHealthListingItem.TemplateId)));

                if (Session["sess_User"] != null)
                {
                    User objUser = (User)Session["sess_User"];

                    if (objUser.Preferences.SocialCookies)
                    {
                        Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                        urlOptions.AlwaysIncludeServerUrl = true;
                        urlOptions.AddAspxExtension = true;
                        urlOptions.LanguageEmbedding = LanguageEmbedding.Never;
                        articleUrl = Sitecore.Links.LinkManager.GetItemUrl(article, urlOptions);

                        //Have permission to load in Social
                        showSocial = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Could not get Your health listing ancestor: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}