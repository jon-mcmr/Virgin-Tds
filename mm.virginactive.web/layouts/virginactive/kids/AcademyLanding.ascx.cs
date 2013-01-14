using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.KidsShared;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Kids;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Diagnostics;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive.kids
{
    public partial class AcademyLanding : System.Web.UI.UserControl
    {
        protected List<KidsFeatureItem> featureList;
        protected ActiveLandingItem Landing;
        protected string MoreLink = @"<a class=""overlay-clubfilterlist"" href=""#"" data-list-title=""{0}"" data-list-label=""{0}"" data-form-submit=""true"" data-list-url=""/clubs/clubname{1}"" data-list-intro=""{2}"" data-filter=""{3}"">{4}</a>";
        protected string MoreBtnLink = @"<a class=""btn btn-cta-big overlay-clubfilterlist"" href=""#"" data-list-title=""{0}"" data-form-submit=""true"" data-list-label=""{0}"" data-list-url=""/clubs/clubname{1}.aspx"" data-list-intro=""{2}"" data-filter=""{3}"">{4}</a>";

        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.ActiveAcademyFacilityId, "ActiveAcademyFacilityId facility Id missing");
            Landing = new ActiveLandingItem(Sitecore.Context.Item);

            if (Landing.InnerItem.HasChildren)
            {
                PanelList.DataSource = Landing.InnerItem.Children.ToList().ConvertAll(X => new KidsFeatureItem(X));
                PanelList.DataBind();
            }


            /* Format the overlay links */
            MoreLink = String.Format(MoreLink,
                Translate.Text("Kids facilities in your club"),
                "/feedback?page=kids",
                Translate.Text("To find what's available near you, please tell us your nearest club"),
                ItemPaths.ActiveAcademyFacilityId,
                Translate.Text("Find out more")
             );

            MoreBtnLink = String.Format(MoreBtnLink,
                Translate.Text("Kids facilities in your club"),
                "/feedback?page=kids",
                Translate.Text("To find what's available near you, please tell us your nearest club"),
                ItemPaths.ActiveAcademyFacilityId,
                Translate.Text("Find out more")
             );
        }
    }
}