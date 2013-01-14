using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class FacilitiesClassesLanding : System.Web.UI.UserControl
    {

        protected Item currentItem = Sitecore.Context.Item;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Facility landing
            if (currentItem.TemplateID.ToString() == FacilitiesLandingItem.TemplateId)
            {
                Item sharedFacility = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities);
                LoadImagePanels(sharedFacility, new PageSummaryItem(currentItem.Children[0]).Url);
            }

            //Classes Landing
            if (currentItem.TemplateID.ToString() == ClassesLandingItem.TemplateId)
            {
                Item sharedClasses = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses);
                LoadImagePanels(sharedClasses, new PageSummaryItem(currentItem.Children[0]).Url);
            }

        }

        private void LoadImagePanels(Item panelRoot, string rootUrl)
        {
            if (panelRoot.HasChildren)
            {
                foreach (Item panelParent in panelRoot.GetChildren())
                {
                    PageSummaryItem current = new PageSummaryItem(panelParent);
                    if (!current.Hidefrommenu.Checked)
                    {
                        ImagePanel panel = this.Page.LoadControl("~/layouts/virginactive/navigation/ImagePanel.ascx") as ImagePanel;
                        panel.ContextItem = current;
                        panel.RootUrl = rootUrl; //Set a root URL because we want to generate jump links and section QS value
                        phPanels.Controls.Add(panel);
                    }
                }
            }
        }
    }
}