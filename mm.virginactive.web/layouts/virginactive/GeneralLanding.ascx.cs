using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.web.layouts.virginactive.navigation;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class GeneralLanding : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Item panelRoot = Sitecore.Context.Item;

            if (panelRoot.HasChildren)
            {
                foreach (Item childPanel in panelRoot.GetChildren())
                {
                    ImagePanel panel = this.Page.LoadControl("~/layouts/virginactive/navigation/ImagePanel.ascx") as ImagePanel;
                    panel.ContextItem = new PageSummaryItem(childPanel);
                    if (!panel.ContextItem.Hidefrommenu.Checked)
                    {
                        phPanels.Controls.Add(panel);
                    }
                }
            }
        }
    }
}