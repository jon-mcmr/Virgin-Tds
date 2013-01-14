using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class MemberBenefits : System.Web.UI.UserControl
    {
        protected MemberPromotionListingItem PromoListing = new MemberPromotionListingItem(Sitecore.Context.Item);
        protected PageSummaryItem enqForm;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PromoListing.InnerItem != null)
            {
                Assert.ArgumentNotNullOrEmpty(ItemPaths.EnquiryForm, "Enquiry form path missing");

                enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
                List<MemberPromotionItem> promos = PromoListing.InnerItem.Children.ToList().ConvertAll(X => new MemberPromotionItem(X));
                promos.First().IsFirst = true;
                OfferList.DataSource = promos;
                OfferList.DataBind();
            }
        }
    }
}