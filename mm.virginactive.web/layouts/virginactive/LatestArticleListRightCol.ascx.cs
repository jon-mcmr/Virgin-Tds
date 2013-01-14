using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class LatestArticleListRightCol : System.Web.UI.UserControl
    {
        protected YourHealthLandingItem landing = new YourHealthLandingItem(Sitecore.Context.Database.GetItem(ItemPaths.YourHealthArticles));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (landing != null)
            {
                if (!String.IsNullOrEmpty(landing.Latestarticles.Raw))
                {
                    List<YourHealthArticleItem> articles = landing.Latestarticles.ListItems.ConvertAll(X => new YourHealthArticleItem(X));
                    ArticleList.DataSource = articles;
                    ArticleList.DataBind();
                }
            }
        }
    }
}