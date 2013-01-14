using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.EviBlog;

namespace mm.virginactive.web.layouts.virginactive.blog
{
    public partial class BlogLatestPosts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.BlogTemplateId, "Blog template Id is null");
            LatstPostsPane.Visible = false;
            Item blogLanding = Sitecore.Context.Item.Axes.SelectSingleItem(String.Format("ancestor-or-self::*[@@tid='{0}']", ItemPaths.BlogTemplateId));

            if (blogLanding != null)
            {
                Item[] popPosts = blogLanding.Axes.SelectItems("descendant::*[@#popular post#='1']");

                if (popPosts != null)
                {
                    List<BlogEntryItem> popPostItems = popPosts.ToList().ConvertAll(X => new BlogEntryItem(X));
                    LatstPostsPane.Visible = true;
                    ArticleList.DataSource = popPostItems;
                    ArticleList.DataBind();
                }
            }

        }
    }
}