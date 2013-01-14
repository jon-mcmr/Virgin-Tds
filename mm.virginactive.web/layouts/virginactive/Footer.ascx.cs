using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using Sitecore.Data;
using Sitecore.Collections;
using CustomItemGenerator.Fields.ListTypes;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using Sitecore.Sites;
using Sitecore.Data.Items;
using Sitecore.Links;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class Footer : System.Web.UI.UserControl
    {
        protected string footerItemPath = ItemPaths.Footer;
        protected FooterItem footer;
        protected string markup = "";
        protected string subMenuMarkup = "";
        protected string mobileSiteLink = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            footer = Sitecore.Context.Database.GetItem(footerItemPath);

            //Main Menu items
            List<PageSummaryItem> items = footer.Mainlinks.ListItems.ConvertAll(x => new PageSummaryItem(x));
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
            int childcounter = 0;
            foreach (PageSummaryItem child in items)
            {
                if (!string.IsNullOrEmpty(child.NavigationTitle.Text))
                {
                    markupBuilder.AppendFormat(@"<li{2}><a href=""{0}"">{1}</a></li>", child.Url, child.NavigationTitle.Rendered, childcounter == 0 ? @" class=""first""" : "");
                    childcounter++;
                }
            }

            if (markupBuilder.Length > 0)
            {
                markupBuilder.Insert(0, "<ul>");
                markupBuilder.Insert(0, "<nav>");
                markupBuilder.AppendFormat(@"<li><a href=""{0}"" class=""external"" target=""_blank"">{1}</a></li>", Settings.CareersLink, Translate.Text("Careers"));
                markupBuilder.Append("</ul>");
                markupBuilder.Append("</nav>");
                markup = markupBuilder.ToString();
            }

            //Submenu items
            List<PageSummaryItem> subitems = footer.Sublinks.ListItems.ConvertAll(x => new PageSummaryItem(x));
            System.Text.StringBuilder markupBuilderSub = new System.Text.StringBuilder();
            childcounter = 0;
            //Set Link to Mobile Site -if we are using a Mobile device.

            if (SitecoreHelper.GetDeviceFromCookie().Equals(Settings.MobileDevice, StringComparison.OrdinalIgnoreCase))
            {
                string startPath = Sitecore.Context.Site.StartPath.ToString();
                var homeItem = Sitecore.Context.Database.GetItem(startPath);
                var options = new UrlOptions { AlwaysIncludeServerUrl = true, AddAspxExtension = false, LanguageEmbedding = LanguageEmbedding.Never };
                var homeUrl = LinkManager.GetItemUrl(homeItem, options);

                //Set return to main site link
                markupBuilderSub.AppendFormat(@"<li><a href=""{0}"">{1}</a></li>", homeUrl + "?sc_device=mobile&persisted=true", Translate.Text("Mobile Site"));
                childcounter++;
            }

            foreach (PageSummaryItem child in subitems)
            {
                if (!string.IsNullOrEmpty(child.NavigationTitle.Text))
                {
                    string url = Sitecore.Links.LinkManager.GetItemUrl(child.InnerItem);
                    markupBuilderSub.AppendFormat(@"<li><a href=""{0}"">{1}</a></li>", url, child.NavigationTitle.Rendered);
                    childcounter++;
                }
            }



            if (markupBuilderSub.Length > 0)
            {
                subMenuMarkup = markupBuilderSub.ToString();
            }


        }
    }
}