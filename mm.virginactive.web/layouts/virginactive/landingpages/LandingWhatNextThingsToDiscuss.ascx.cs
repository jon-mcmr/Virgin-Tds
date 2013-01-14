using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingWhatNextThingsToDiscuss : System.Web.UI.UserControl
    {
        protected LandingWhatsNextItem currentItem = new LandingWhatsNextItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetupSitecoreData();
                BindData();             
            }

        }


        /// <summary>
        /// To setup data from Sitecore
        /// </summary>
        private void SetupSitecoreData()
        {
            
        }

        /// <summary>
        /// Bind the highlight panel items to the repeater
        /// </summary>
        private void BindData()
        {
            List<HighlightPanelItem> highlightItems = null;


            Item[] items = currentItem.InnerItem.Axes.SelectItems("*");

            if (items != null)
            {
                highlightItems = items.ToList().ConvertAll(X => new HighlightPanelItem(X));
            }

            if (highlightItems != null)
            {
                rpDiscuss.DataSource = highlightItems;
                rpDiscuss.DataBind();
            }

        }
    }
}