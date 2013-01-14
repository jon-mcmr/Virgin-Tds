using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class PopularArticleList : System.Web.UI.UserControl
    {
        protected YourHealthLandingItem landing = new YourHealthLandingItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (landing != null)
            {
                if (!String.IsNullOrEmpty(landing.Mostpopulararticles.Raw))
                {
                    List<YourHealthArticleItem> articles = landing.Mostpopulararticles.ListItems.ConvertAll(X => new YourHealthArticleItem(X));
                    ArticleList.DataSource = articles;
                    ArticleList.DataBind();
                }
            }

        }
    }
}