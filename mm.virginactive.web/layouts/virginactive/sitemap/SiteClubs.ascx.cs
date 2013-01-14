using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Collections.Specialized;
using mm.virginactive.controls.Model;
using Sitecore.Data.Items;
using Sitecore.Links;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.sitemap
{
    public partial class SiteClubs : System.Web.UI.UserControl
    {
        protected string clubFinderUrl = "";
        public string ClubFinderUrl
        {
            get { return clubFinderUrl; }
            set
            {
                clubFinderUrl = value;
            }
        }

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
            //Get Club Finder Details
            clubFinderUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.ClubFinder);

            Assert.ArgumentNotNullOrEmpty(ItemPaths.Clubs, "Club root is not correct in ItemPaths");
            //Bind the clubs to ClubList
            List<ClubItem> clubs = Sitecore.Context.Database.GetItem(ItemPaths.Clubs).Children.ToList().ConvertAll(X => new ClubItem(X));
            clubs.RemoveAll(x => x.IsHiddenFromMenu());
            List<Club> clubL = clubs.ConvertAll(X => new Club(X));

            if (clubL != null)
            {
                ClubList.DataSource = clubL;
                ClubList.DataBind();
            }

            //Set Header type param if available
            string rawParameters = Attributes["sc_parameters"];
            NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
            if (!String.IsNullOrEmpty(parameters["HeaderIsH2"]))
            {
                HeaderIsH2 = Convert.ToBoolean(parameters["HeaderIsH2"]);
            }
        }

        protected void ClubList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var club = dataItem.DataItem as Club;

                var ltrClubLink = e.Item.FindControl("ltrClubLink") as System.Web.UI.WebControls.Literal;

                string ClubLinkUrl = new PageSummaryItem(club.ClubItm.InnerItem).Url;

                //if (club.ClubItm.IsPlaceholder.Checked == true)
                //{
                //    Item campaign;

                //    if (club.ClubItm.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                //    {
                //        campaign =
                //            club.ClubItm.PlaceholderCampaign.Item.Axes.SelectSingleItem(
                //                String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId));
                //    }
                //    else
                //    {

                //        //redirect to campaign
                //        campaign = club.ClubItm.PlaceholderCampaign.Item;

                //    }

                //    if (campaign != null)
                //    {
                //        UrlOptions opt = new UrlOptions();
                //        opt.AddAspxExtension = false;
                //        opt.LanguageEmbedding = LanguageEmbedding.Never;
                //        opt.AlwaysIncludeServerUrl = true;

                //        ClubLinkUrl = LinkManager.GetItemUrl(campaign, opt);
                //    }
                //}

                string ClubName = club.ClubItm.Clubname.Raw;
                if(club.IsClassic == true)
                {
                    ClubName += String.Format(@" <span class=""small-font"">({0} {1})</span>", Translate.Text("by"), Translate.Text("VIRGIN ACTIVE CLASSIC"));
                }
                else if(club.ClubCentric == true)
                {
                    ClubName += String.Format(@" <span class=""small-font"">({0})</span>", Translate.Text("previously ESPORTA"));
                }

                ltrClubLink.Text = @"<a href=""" + ClubLinkUrl + @""">" + ClubName + @"</a>";

            }
        }
    }
}