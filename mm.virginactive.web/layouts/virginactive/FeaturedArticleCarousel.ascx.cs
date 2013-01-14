using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class FeaturedArticleCarousel : System.Web.UI.UserControl
    {
        protected YourHealthLandingItem landing = new YourHealthLandingItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (landing != null)
            {
                if (!String.IsNullOrEmpty(landing.Featuredarticles.Raw))
                {
                    List<YourHealthArticleItem> articles = landing.Featuredarticles.ListItems.ConvertAll(X => new YourHealthArticleItem(X));
                    ImageList.DataSource = articles;
                    ImageList.DataBind();
                }
            }

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		        $(function(){
	                $.va_init.functions.setupImageRotator();
                });
            </script>"));
        }
    }
}