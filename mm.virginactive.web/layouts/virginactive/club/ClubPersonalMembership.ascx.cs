using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Offers;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubPersonalMembership : System.Web.UI.UserControl
    {
        protected MembershipLandingItem SharedLanding;
        protected PageSummaryItem enqForm;
        protected ClubItem currentClub;
        
        MembershipListingItem Context = new MembershipListingItem(Sitecore.Context.Item);

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
                
                List<MembershipStaticPageItem> sections = Context.Products.ListItems.ConvertAll(X => new MembershipStaticPageItem(X));
                int counter = 1;                

                sections.ForEach((delegate(MembershipStaticPageItem section)
                {
                    //section.FormUrl = EnqFormUrl;
                    if (Context.InnerItem.Fields[String.Format("Product {0} price", counter.ToString())] != null)
                    {
                        section.Price = Context.InnerItem.Fields[String.Format("Product {0} price", counter.ToString())].Value;
                        counter++;
                    }
                }));
                MemberSectionList.DataSource = sections;
                MemberSectionList.DataBind();
            }
        }

        protected void MemberSectionList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var module = dataItem.DataItem as MembershipStaticPageItem;

                if (module != null)
                {
                    string EnqFormUrl;
                    if (module.Offer.Item != null)
                    {
                        //Get offer
                        OfferItem offer = (OfferItem)module.Offer.Item;
                        EnqFormUrl = enqForm.Url + "?sc_trk=enq&page=" + offer.GetOfferTypeValue() + "&offerId=" + offer.OfferId.Raw + "&clubId=" + currentClub.ClubId.Raw;
                    }
                    else
                    {
                        EnqFormUrl = enqForm.Url + "?sc_trk=enq&c=" + currentClub.InnerItem.ID.ToShortID().ToString();
                    }
                    module.FormUrl = EnqFormUrl;

                    Literal ltrFormLink = (Literal)e.Item.FindControl("ltrFormLink");
                    ltrFormLink.Text =  "<a href=\"" + EnqFormUrl + "\" class=\"btn btn-cta-big gaqTag\" data-gaqcategory=\"CTA\" data-gaqaction=\"EnquireNow\" data-gaqlabel=\"Membership\">" + Translate.Text("Enquire now") + "</a>";
                }

            }
        }
    }
}