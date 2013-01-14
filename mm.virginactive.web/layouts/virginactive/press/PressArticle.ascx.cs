using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Press;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive.press
{
    public partial class PressArticle : System.Web.UI.UserControl
    {
        protected PressArticleItem article = new PressArticleItem(Sitecore.Context.Item);
        protected string articleListingUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            Item articleList = article.InnerItem.Axes.SelectSingleItem(String.Format("ancestor::*[@@tid='{0}']", PressLandingItem.TemplateId));

            if (articleList != null)
            {
                articleListingUrl = new PageSummaryItem(articleList).Url;
            }
        }
    }
}