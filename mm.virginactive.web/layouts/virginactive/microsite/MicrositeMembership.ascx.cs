using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Diagnostics;
using Sitecore.Links;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Offers;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeMembership : System.Web.UI.UserControl
    {
        protected MembershipLandingItem SharedLanding;
        protected PageSummaryItem enqForm;
        protected ClubItem currentClub;

        protected MembershipItem ContextItem = new MembershipItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.SharedMemberships, "Shared club personal membership path missing");
            Assert.ArgumentNotNullOrEmpty(ItemPaths.EnquiryForm, "Enquiry form path missing");

            //get the details of the club
            currentClub = SitecoreHelper.GetCurrentClub(Sitecore.Context.Item);
            SharedLanding = new MembershipLandingItem(Sitecore.Context.Database.GetItem(ItemPaths.SharedMemberships));

            enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));

            //Databind the sections
            if (SharedLanding.InnerItem.HasChildren)
            {

                List<MembershipStaticPageItem> sections = ContextItem.Products.ListItems.ConvertAll(X => new MembershipStaticPageItem(X));
                int counter = 1;

                sections.ForEach((delegate(MembershipStaticPageItem section)
                {
                    //section.FormUrl = EnqFormUrl;
                    if (ContextItem.InnerItem.Fields[String.Format("Product {0} price", counter.ToString())] != null)
                    {
                        section.Price = ContextItem.InnerItem.Fields[String.Format("Product {0} price", counter.ToString())].Value;

                        switch(ContextItem.GetListingCssClass())
                        {
                            case "half-panel":
                                if(counter % 2 == 0)
                                {
                                    section.IsLast = true;
                                }
                                break;
                            case "third-panel":
                                if(counter % 3 == 0)
                                {
                                    section.IsLast = true;
                                }
                                break;
                        }
                        counter++;
                    }
                }));

                MemberSectionList.DataSource = sections;
                MemberSectionList.DataBind();
            }
        }

        protected string GetInterestedUrl()
        {
            var currentItem = Sitecore.Context.Item;

            var interestedItem = currentItem.Parent.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", MembershipEnquiryItem.TemplateId));
            if (interestedItem == null)
            {
                return String.Empty;
            }

            return LinkManager.GetItemUrl(interestedItem);
        }
    }
}