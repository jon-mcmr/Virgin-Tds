using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Kids;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.KidsShared;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubKids : System.Web.UI.UserControl
    {

        protected List<KidsFeatureItem> sharedClubV = new List<KidsFeatureItem>();
        protected List<KidsFeatureItem> sharedActive = new List<KidsFeatureItem>();
        protected ClubVSharedItem sharedClubVLanding;
        protected ActiveAcademySharedItem sharedActiveALanding;
        protected ClubItem currentClub;

        protected void Page_Load(object sender, EventArgs e)
        {
            /* ************* SECTION REMOVED 29/09/2011 **********
            try
            {
                currentClub = SitecoreHelper.GetCurrentClub(Sitecore.Context.Item);
                ClubFacilitiesLandingItem clubFacility = new ClubFacilitiesLandingItem(currentClub.InnerItem.Axes.SelectSingleItem(String.Format("descendant::*[@@tid='{0}']", ClubFacilitiesLandingItem.TemplateId)));

                //This club as Active Academy facilitles
                if (clubFacility.SharedFacilites.Raw.Contains(ItemPaths.ActiveAcademyFacilityId))
                {
                    
                    sharedActiveALanding = new ActiveAcademySharedItem(Sitecore.Context.Database.GetItem(ItemPaths.SharedActiveA));
                    sharedActive = sharedActiveALanding.InnerItem.Children.ToList().ConvertAll(X => new KidsFeatureItem(X));
                    ActiveAList.DataSource = sharedActive;
                    ActiveAList.DataBind();
                    ActiveSection.Visible = true;
                }

                //This club has ClubV facilities
                if (clubFacility.SharedFacilites.Raw.Contains(ItemPaths.ClubVFacilityId))
                {
                    sharedClubVLanding = new ClubVSharedItem(Sitecore.Context.Database.GetItem(ItemPaths.SharedClubV));
                    sharedClubV = sharedClubVLanding.InnerItem.Children.ToList().ConvertAll(X => new KidsFeatureItem(X));
                    ClubVList.DataSource = sharedClubV;
                    ClubVList.DataBind();
                    ClubVSection.Visible = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Could not Generate club kids section: {0}", ex.Message), this);
            } */
        }
    }
}