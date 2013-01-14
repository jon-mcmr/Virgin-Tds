using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;
using Sitecore.Web;
using mm.virginactive.controls.Model;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class YourHealthListing : System.Web.UI.UserControl
    {
        protected YourHealthListingItem listing = new YourHealthListingItem(Sitecore.Context.Item);
        protected Boolean showSocial = false;

        public Boolean ShowSocial
        {
            get { return showSocial; }
            set
            {
                showSocial = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (listing != null)
            {
                if (listing.InnerItem.HasChildren)
                {
                    List<YourHealthArticleItem> articles = listing.InnerItem.Children.ToList().ConvertAll(X => new YourHealthArticleItem(X));
                    ArticleList.DataSource = articles;
                    ArticleList.DataBind();
                }
            }

            if (Session["sess_User"] != null)
            {
                User objUser = (User)Session["sess_User"];

                if (objUser.Preferences.SocialCookies)
                {
                    //Have permission to load in Social
                    if (WebUtil.GetQueryString("showSocial") == "true")
                    {
                        showSocial = true;
                    }
                }
            }
        }
    }
}