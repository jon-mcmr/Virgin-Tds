using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class IFrame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Item currentItem = Sitecore.Context.Item;
            PageSummaryItem item = new PageSummaryItem(currentItem);
            string canonicalTag = item.GetCanonicalTag();

            string metaDescription = item.GetMetaDescription();

            //Add page title //todo: add club name
            string title = Translate.Text("Virgin Active");
            string browserPageTitle = item.GetPageTitle();

            string section = Sitecore.Web.WebUtil.GetQueryString("section");

            if (!String.IsNullOrEmpty(section))
            {
                PageSummaryItem listing = null;
                if (currentItem.TemplateID.ToString() == ClassesListingItem.TemplateId)
                {
                    //Get classes listing browser page title
                    listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses + "/" + section);
                }
                else if (currentItem.TemplateID.ToString() == FacilitiesListingItem.TemplateId)
                {
                    //Get facility listing browser page title
                    listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities + "/" + section);
                }

                if (listing != null)
                {
                    browserPageTitle = listing.GetPageTitle();
                    canonicalTag = String.IsNullOrEmpty(Request.QueryString["section"]) ? listing.GetCanonicalTag() : listing.GetCanonicalTag(Request.QueryString["section"]);
                    metaDescription = listing.GetMetaDescription();
                }
            }


            if (!String.IsNullOrEmpty(browserPageTitle))
            {
                title = String.Format("{0} | {1}", browserPageTitle, title);
            }

            Page.Title = title;
            //Add canonical
            Page.Header.Controls.Add(new Literal() { Text = canonicalTag });
            //Add meta description
            Page.Header.Controls.Add(new Literal() { Text = metaDescription });
        }
    }
}