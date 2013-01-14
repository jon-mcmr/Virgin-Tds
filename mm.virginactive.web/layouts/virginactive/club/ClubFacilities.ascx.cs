using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubFacilities : System.Web.UI.UserControl
    {
        protected ClubItem club;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClubFacilitiesLandingItem context = new ClubFacilitiesLandingItem(Sitecore.Context.Item);
            club = SitecoreHelper.GetCurrentClub(context.InnerItem);

            List<string> sharedFacilities = context.SharedFacilites.Raw.Split('|').ToList();
            List<FacilityItem> clubSpecificFacilities = new List<FacilityItem>();
            if (context.InnerItem.HasChildren)
            {
                clubSpecificFacilities = context.InnerItem.Children.ToList().ConvertAll(X => new FacilityItem(X));
            }

            //Fetch all the facility lists
            List<FacilitiesListingItem> facilityLists = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities).Children.ToList().ConvertAll(X => new FacilitiesListingItem(X));
            //facilityLists.RemoveAll(x => x.PageSummary.Hidefrommenu.Checked);

            List<FacilitiesListingItem> facilityListsToRemove = new List<FacilitiesListingItem>();

            facilityLists.ForEach(delegate(FacilitiesListingItem fac)
            {
                //Intially set FacilityList of each facility type to all shared facilities
                fac.FacilityList = fac.InnerItem.Children.ToList().ConvertAll(Y => new FacilityModuleItem(Y));
                //weed out all the shared facilities that are not available in this club
                fac.FacilityList = (from facItem in fac.FacilityList
                                    where sharedFacilities.Contains(facItem.InnerItem.ID.ToString())
                                    select facItem).ToList();

                //Include any club specific facilities
                if (clubSpecificFacilities.Count > 0)
                {
                    List<FacilityItem> facItemsToRemove = new List<FacilityItem>();
                    clubSpecificFacilities.ForEach(delegate(FacilityItem clubSpecificFac)
                    {
                        if (clubSpecificFac.FacilityArea.Raw == fac.InnerItem.ID.ToString())
                        {
                            fac.FacilityList.Insert(0,clubSpecificFac.FacilityModule); //Add the club specific facilities to top of lists
                            facItemsToRemove.Add(clubSpecificFac);
                        }
                    });
                    clubSpecificFacilities.RemoveAll(X => facItemsToRemove.Contains(X)); //Remove the club specific facilities we have already added
                }

                //Remove any un-used facility types
                if (fac.FacilityList.Count == 0)
                    facilityListsToRemove.Add(fac);
            });

            //Remove any un-used facility types
            facilityLists.RemoveAll(X => facilityListsToRemove.Contains(X));

            FacilityListing.DataSource = facilityLists;
            FacilityListing.DataBind();
            JumpLinkList.DataSource = facilityLists;
            JumpLinkList.DataBind();

        }
    }
}