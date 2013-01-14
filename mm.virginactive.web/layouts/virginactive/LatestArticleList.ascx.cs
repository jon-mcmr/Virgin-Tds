using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;
using Sitecore.Web;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class LatestArticleList : System.Web.UI.UserControl
    {
        protected YourHealthLandingItem landing = new YourHealthLandingItem(Sitecore.Context.Item);
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
            if (landing != null)
            {
                if (landing.InnerItem.HasChildren)
                {
                    List<YourHealthListingItem> articles = landing.InnerItem.Children.ToList().ConvertAll(X => new YourHealthListingItem(X));
                    ListingList.DataSource = articles;
                    ListingList.DataBind();

                }

                if (!String.IsNullOrEmpty(landing.Latestarticles.Raw))
                {
                    List<YourHealthArticleItem> articles = landing.Latestarticles.ListItems.ConvertAll(X => new YourHealthArticleItem(X));
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

        protected void ArticleList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var detailPage = dataItem.DataItem as YourHealthArticleItem;

                if (detailPage != null)
                {
                    //Get parent listing page url
                    YourHealthListingItem ParentListingItem = detailPage.InnerItem.Parent;

                    var lnkParentListingUrl = e.Item.FindControl("lnkParentListingUrl") as System.Web.UI.HtmlControls.HtmlAnchor;
                    if (ParentListingItem != null)
                    {
                        lnkParentListingUrl.HRef = ParentListingItem.PageSummary.Url;
                    }
                }
            }
        }
    }
}