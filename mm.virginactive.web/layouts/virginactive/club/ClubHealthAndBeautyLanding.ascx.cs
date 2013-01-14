using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.HealthAndBeauty;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubHealthAndBeautyLanding : System.Web.UI.UserControl
    {
        protected HealthAndBeautyLandingItem currentItem = new HealthAndBeautyLandingItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            ////load the first listing page as the default for the section

            //Get the descendant
            ClubHealthAndBeautyListingItem listingItem = currentItem.InnerItem.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", ClubHealthAndBeautyListingItem.TemplateId)).ToList().ConvertAll(Y => new ClubHealthAndBeautyListingItem(Y))[0];
            Sitecore.Context.Item = listingItem;
            string url = Sitecore.Links.LinkManager.GetItemUrl(listingItem);
            Response.Redirect(url);

        }
    }
}