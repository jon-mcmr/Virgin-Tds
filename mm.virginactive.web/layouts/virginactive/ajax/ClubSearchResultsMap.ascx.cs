using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Util;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using System.Text.RegularExpressions;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Links;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class ClubSearchResultsMap : System.Web.UI.UserControl
    {
        private double? lat;
        private double? lng;
        private string filters;
        private string searchTerm;

        protected List<Club> clubs;
        //protected string matchingResults = "{0} clubs matching your search criteria";

        public double? Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        public double? Lng
        {
            get { return lng; }
            set { lng = value; }
        }

        public string Filters
        {
            get { return filters; }
            set { filters = value; }
        }

        public string SearchTerm
        {
            get { return searchTerm; }
            set { searchTerm = value; }
        }

        //public string MatchingResults
        //{
        //    get { return matchingResults; }
        //    set { matchingResults = value; }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check for search term match -to see if there is match to specify specific number of results (e.g. Manchester)

            int NoOfResultsToShow = 10;

            //Item componentBase = Sitecore.Context.Database.GetItem(ItemPaths.Components);

            //if (componentBase != null)
            //{
            //    Item[] terms = componentBase.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", SearchTermItem.TemplateId));
            //    if (terms != null)
            //    {
            //        foreach (Item term in terms)
            //        {
            //            SearchTermItem itm = new SearchTermItem(term);

            //            Regex match = new Regex(itm.Term.Raw);
            //            if (match.IsMatch(SearchTerm))
            //            {
            //                int resultsForSearch = 0;
            //                Int32.TryParse(itm.Numberofresultstoshow.Raw, out resultsForSearch);

            //                NoOfResultsToShow = resultsForSearch > 0 ? resultsForSearch : NoOfResultsToShow;
 
            //                break;
            //            }

            //        }
            //    }
            //}

            //Build list

            clubs = ClubUtil.GetNearestClubs(Lat, Lng, NoOfResultsToShow);
            if (!String.IsNullOrEmpty(Filters))
            {
                clubs = ClubUtil.FilterClubs(clubs, Filters);
            }

            if (clubs != null)
            {
                ClubList.DataSource = clubs;
                ClubList.DataBind();
            }
        }

        protected void ClubList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var club = dataItem.DataItem as Club;

                var ltrClubImageLink = e.Item.FindControl("ltrClubImageLink") as System.Web.UI.WebControls.Literal;
                var ltrClubTitleLink = e.Item.FindControl("ltrClubTitleLink") as System.Web.UI.WebControls.Literal;

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

                ltrClubImageLink.Text = @"<a href=""" + ClubLinkUrl + @""">" + club.ClubItm.Clubimage.RenderCrop("180x120") + @"</a>";
                ltrClubTitleLink.Text = @"<a href=""" + ClubLinkUrl + @""">" + club.ClubItm.Clubname.Text + @"</a>";
            }
        }

        public static string RenderToString(double? lat, double? lng, string filter, string searchTerm)
        {
            return SitecoreHelper.RenderUserControl<ClubSearchResultsMap>("~/layouts/virginactive/ajax/ClubSearchResultsMap.ascx",
                uc =>
                {
                    uc.Lat = lat;
                    uc.Lng = lng;
                    uc.Filters = filter;
                    uc.SearchTerm = searchTerm;
                });
        }
    }
}