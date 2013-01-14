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
using mm.virginactive.wrappers.VirginActive.PageTemplates.HealthAndBeauty;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using System.Collections.Specialized;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class HealthAndBeautyLanding : System.Web.UI.UserControl
    {
        protected HealthAndBeautyLandingItem currentItem = new HealthAndBeautyLandingItem(Sitecore.Context.Item);
        protected string MoreLink = @"<a class=""overlay-clubfilterlist"" href=""#"" data-list-title=""{0}"" data-form-submit=""true"" data-list-label=""{0}"" data-list-type=""ClubNamesList"" data-list-url=""/clubs/clubname{1}"" data-list-intro=""{2}"" data-filter=""{3}"">{4}</a>";
        protected string MoreButtonLink = @"<a class=""overlay-clubfilterlist btn btn-cta-big"" href=""#"" data-list-title=""{0}"" data-form-submit=""true"" data-list-label=""{0}"" data-list-type=""ClubNamesList"" data-list-url=""/clubs/clubname{1}"" data-list-intro=""{2}"" data-filter=""{3}"">{4}</a>";
        private string cssClass = "";

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieve CSS class
            cssClass = currentItem.Abstract.GetPanelCssClass();

            if (currentItem.InnerItem.HasChildren)
            {
                foreach (Item child in currentItem.InnerItem.Children)
                {
                    HealthAndBeautyModule panel = this.Page.LoadControl("~/layouts/virginactive/HealthAndBeautyModule.ascx") as HealthAndBeautyModule;
                    panel.HealthAndBeautyModuleInstance = new HealthAndBeautyModuleItem(child);
                    phPanels.Controls.Add(panel);
                }
            }

            /* Format the overlay links */
            MoreLink = String.Format(MoreLink,
                Translate.Text("Spa treatments in your club"),
                "/health-and-beauty/treatments",
                Translate.Text("To find what's available near you, please tell us your nearest club"),
                ItemPaths.SpaFacilityIds,
                Translate.Text("Find out what's available at your club")
             );

            MoreButtonLink = String.Format(MoreButtonLink,
                Translate.Text("Spa treatments in your club"),
                "/health-and-beauty/treatments",
                Translate.Text("To find what's available near you, please tell us your nearest club"),
                ItemPaths.SpaFacilityIds,
                Translate.Text("Find your club")
             );
        }
                
    }
}