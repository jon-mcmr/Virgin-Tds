using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Kids;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive.kids
{
    public partial class ClubVLanding : System.Web.UI.UserControl
    {
        protected ClubVLandingItem ClubV = new ClubVLandingItem(Sitecore.Context.Item);
        protected PageSummaryItem enqForm;
		//protected string MoreLink = @"<p class=""moreinfo""><a class=""overlay-clubfilterlist"" href=""#"" data-list-title=""{0}"" data-list-label=""{0}"" data-form-submit=""true"" data-list-type=""ClubNamesList"" data-list-url=""/clubs/clubname{1}"" data-list-intro=""{2}"" data-filter=""{3}"" data-excludeex=""true"">{4}</a></p>";
		protected string MoreLink = @"<p class=""moreinfo""><a class=""overlay-clubfilterlist"" href=""#"" data-list-title=""{0}"" data-list-label=""{0}"" data-form-submit=""true"" data-list-type=""ClubNamesList"" data-list-url=""/clubs/clubname{1}"" data-list-intro=""{2}"" data-filter=""{3}"">{4}</a></p>";
		//protected string MoreBtnLink = @"<a class=""btn btn-cta-big overlay-clubfilterlist"" href=""#"" data-list-title=""{0}"" data-list-label=""{0}"" data-form-submit=""true"" data-list-url=""/clubs/clubname{1}.aspx"" data-list-intro=""{2}"" data-filter=""{3}"" data-excludeex=""true"">{4}</a>";
		protected string MoreBtnLink = @"<a class=""btn btn-cta-big overlay-clubfilterlist"" href=""#"" data-list-title=""{0}"" data-list-label=""{0}"" data-form-submit=""true"" data-list-url=""/clubs/clubname{1}.aspx"" data-list-intro=""{2}"" data-filter=""{3}"">{4}</a>";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.EnquiryForm, "Enquiry form path missing");
			//Assert.ArgumentNotNullOrEmpty(ItemPaths.ClubVFacilityId, "ClubV facility Id missing");
			Assert.ArgumentNotNullOrEmpty(ItemPaths.KidsFacilityListing, "ClubV facility Id missing");

			//Get child kid facilities from the Kids Facility Listings
			Item listing = Sitecore.Context.Database.GetItem(ItemPaths.KidsFacilityListing);

			string kidFacilities = "";
			foreach (Item kidsFacility in listing.Children)
			{
				kidFacilities = kidFacilities + kidsFacility.ID.ToString() + "|";
			}

			kidFacilities = kidFacilities.Length > 0 ? kidFacilities.Remove(kidFacilities.Length - 1) : "";
            
            enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));

            /* Format the overlay links */
            MoreLink = String.Format(MoreLink,
                Translate.Text("Kids facilities in your club"),
                "/feedback?page=kids",
                Translate.Text("To find what's available near you, please tell us your nearest club"),
				//ItemPaths.ClubVFacilityId,
			    kidFacilities,
                Translate.Text("Find out more")
             );

            MoreBtnLink = String.Format(MoreBtnLink,
                Translate.Text("Kids facilities in your club"),
                "/feedback?page=kids",
                Translate.Text("To find what's available near you, please tell us your nearest club"),
				//ItemPaths.ClubVFacilityId,
				kidFacilities,
                Translate.Text("Find out more")
             );

            HeroSectionPlaceholder.Visible = ClubV.ShowHeroSection.Checked;
        }
    }
}