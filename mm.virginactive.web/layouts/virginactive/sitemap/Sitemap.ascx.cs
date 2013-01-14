using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates;

namespace mm.virginactive.web.layouts.virginactive.sitemap
{
    public partial class Sitemap : System.Web.UI.UserControl
    {
        protected PageSummaryItem Home;
        protected PageSummaryItem MemberEnq;
        protected PageSummaryItem ClubFinder;
        protected PageSummaryItem Contact;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Assert.ArgumentNotNullOrEmpty(ItemPaths.HomePage, "home is not correct in ItemPaths");
            Assert.ArgumentNotNullOrEmpty(ItemPaths.ClubFinder, "Club finder is not correct in ItemPaths");
            Assert.ArgumentNotNullOrEmpty(ItemPaths.EnquiryForm, "Enq form not correct in ItemPaths");

            try
            {
                //Set misc pages
                MemberEnq = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
                Home = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.HomePage));
                ClubFinder = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClubFinder));
                Contact = new PageSummaryItem(Home.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@tid='{0}']", ContactFormItem.TemplateId)));
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Issues encountered when building sitemap: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }


        }
    }
}