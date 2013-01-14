using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Collections;
using mm.virginactive.common.Constants.SitecoreConstants;


namespace mm.virginactive.web.layouts.virginactive
{
    public partial class FeaturedPromotionEvent : System.Web.UI.UserControl
    {
        protected string headerItemPath = ItemPaths.Header;
        protected FeaturedPromotionEventWidgetItem fpew;
        protected string featuredLinks = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ChildList allFeatures = Sitecore.Context.Database.GetItem(headerItemPath).GetChildren();

            if (allFeatures != null)
            {
                List<FeaturedPromotionEventWidgetItem> features = allFeatures.ToList().ConvertAll(X => new FeaturedPromotionEventWidgetItem(X));
                fpew = features.First();

                features.Remove(fpew); //Remove the first child (Featured item)   
                System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

                FeatureList.DataSource = features;
                FeatureList.DataBind();
            }
            
        }
    }
}