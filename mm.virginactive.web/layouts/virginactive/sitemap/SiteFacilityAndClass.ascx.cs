using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System.Collections.Specialized;

namespace mm.virginactive.web.layouts.virginactive.sitemap
{
    public partial class SiteFacilityAndClass : System.Web.UI.UserControl
    {
        protected PageSummaryItem facilityAndClass = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesAndClasses));
        protected PageSummaryItem classLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClassesLanding));
        protected PageSummaryItem classListing = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClassesListing));
        protected PageSummaryItem facilityLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesLanding));
        protected PageSummaryItem facilityListing = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesListing));

        private bool headerIsH2 = true;

        public bool HeaderIsH2
        {
            get { return headerIsH2; }
            set
            {
                headerIsH2 = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Item sharedFacilities = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities);
            List<PageSummaryItem> facilitySections = sharedFacilities.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
            facilitySections.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

            foreach (PageSummaryItem section in facilitySections)
            {
                StringBuilder sb = GetSectionHtml(section, facilityListing);
                FacilityPanels.Controls.Add(new LiteralControl(sb.ToString()));
            }


            //Generate the class links, no need to worry about multiple levels
            Item sharedClasses = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses);
            if (sharedClasses != null)
            {
                List<PageSummaryItem> classSections = sharedClasses.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                foreach (PageSummaryItem section in classSections)
                {
                    StringBuilder sb = GetSectionHtml(section, classListing);
                    ClassPanels.Controls.Add(new LiteralControl(sb.ToString()));
                }
            }

            //Set Header type param if available
            string rawParameters = Attributes["sc_parameters"];
            NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
            if (!String.IsNullOrEmpty(parameters["HeaderIsH2"]))
            {
                HeaderIsH2 = Convert.ToBoolean(parameters["HeaderIsH2"]);
            }

        }

        private StringBuilder GetSectionHtml(PageSummaryItem section, PageSummaryItem landing)
        {

            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendFormat(@"<h4><a href=""{0}?section={2}"">{1}</a></h4>", landing.Url, section.NavigationTitle.Text, section.Name.ToLower());

                List<PageSummaryItem> childItems = section.InnerItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                StringBuilder children = new StringBuilder();
                foreach (PageSummaryItem child in childItems)
                {
                    children.AppendFormat(@"<li><a href=""{0}?section={2}#{3}"">{1}</a></li>", landing.Url, child.NavigationTitle.Text, section.Name.ToLower(), child.Name.ToLower());
                }

                sb.AppendFormat(@"<ul>{0}</ul>", children.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error printing Facilities and classes Nav: {0}" + ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
            return sb;
        }
    }
}