using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Data;
using Sitecore.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeHeader : System.Web.UI.UserControl
    {
        protected string micrositeUrl;
        protected string micrositeLogo;
        protected string micrositeHeading;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Item landingItem =
                    Sitecore.Context.Item.Axes.SelectSingleItem(String.Format("ancestor-or-self::*[@@tid='{0}']",
                                                                              ClubMicrositeLandingItem.TemplateId));

                if (landingItem == null)
                {
                    return;
                }

                Item homeItem =
                    landingItem.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId));

                if (homeItem == null)
                {
                    return;
                }

                List<PageSummaryItem> items = new List<PageSummaryItem>();

                items.Add(new PageSummaryItem(homeItem));

                items.AddRange(
                    homeItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x)).Where(
                        x => x.Hidefrommenu.Checked == false && !String.IsNullOrEmpty(x.NavigationTitle.Raw)));

                NavigationListView.DataSource = items;
                NavigationListView.DataBind();

                micrositeUrl = new PageSummaryItem(homeItem).Url;

                ClubMicrositeLandingItem club = new ClubMicrositeLandingItem(landingItem);

                micrositeLogo = club.Logo.MediaUrl;
                micrositeHeading = club.Hiddentitle.Rendered;
            }
        }
    }
}