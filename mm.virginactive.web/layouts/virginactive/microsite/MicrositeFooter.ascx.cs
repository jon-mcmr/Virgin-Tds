using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeFooter : System.Web.UI.UserControl
    {
        protected string TwitterUrl;
        protected string TwitterImage;

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

                items.AddRange(
                    homeItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x)).Where(
                        x => x.InnerItem.TemplateID.ToString() == ContentItem.TemplateId && !String.IsNullOrEmpty(x.NavigationTitle.Raw)));

                NavigationListView.DataSource = items;
                NavigationListView.DataBind();

                ClubMicrositeLandingItem landing = new ClubMicrositeLandingItem(landingItem);

                ClubItem club = new ClubItem(landing.Club.Item);

                OpeningHoursLiteral.Text = String.Format(Translate.Text("OPENING HOURS: {0}", club.Openinghours.Raw));
                TwitterUrl = landing.Twitterurl.Rendered;

                TwitterImage = "icon-twitter.png";
                if (Sitecore.Context.Item.TemplateID.ToString() == MicrositeHomeItem.TemplateId || Sitecore.Context.Item.TemplateID.ToString() == LocationItem.TemplateId)
                {
                    TwitterImage = "icon-white-twitter.png";
                }
            }
        }
    }
}