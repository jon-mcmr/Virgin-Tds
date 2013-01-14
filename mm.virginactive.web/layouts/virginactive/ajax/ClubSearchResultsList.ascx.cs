using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using mm.virginactive.controls.Util;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using System.Text.RegularExpressions;
using Sitecore.Links;
using TimetableItem = mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.TimetableItem;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class ClubSearchResultsList : System.Web.UI.UserControl
    {
        private double? lat;
        private double? lng;
        private string filters;
        private string searchTerm;
        protected List<Club> clubs;
        protected string matchingResults = "10 clubs matching your search criteria";

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

        public string MatchingResults
        {
            get { return matchingResults; }
            set { matchingResults = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            //Check for search term match -to see if there is match to specify specific number of results (e.g. Manchester)

            int NoOfResultsToShow = 10;

            Item componentBase = Sitecore.Context.Database.GetItem(ItemPaths.Components);

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

            MatchingResults = String.Format(Translate.Text("{0} clubs matching your search criteria"), clubs != null ? clubs.Count : 0);
	            
        }

        protected void ClubList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var club = dataItem.DataItem as Club;

                if (club != null)
                {
                    //Lookup if Club is classic or not!
                    //Classic overrides Esporta
                    if (club.IsClassic)
                    {
                        var ClassicClubFlag = e.Item.FindControl("ClassicClubFlag") as System.Web.UI.WebControls.Literal;
                        if (ClassicClubFlag != null)
                        {
                            ClassicClubFlag.Text = String.Format(@"<span>{0}</span> {1}", Translate.Text("by"), Translate.Text("VIRGIN ACTIVE CLASSIC"));
                        }
                    } else if (club.ClubItm.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric) //Set Esporta Flag
                    {
                        var EsportaFlag = e.Item.FindControl("EsportaFlag") as System.Web.UI.WebControls.Literal;
                        if (EsportaFlag != null)
                        {
                            EsportaFlag.Text =String.Format( @"<span>{0}</span> ESPORTA", Translate.Text("Previously"));
                        }
                    }

                    //Get address
                    var Address = e.Item.FindControl("Address") as System.Web.UI.WebControls.Literal;
                    if (Address != null)
                    {
                        System.Text.StringBuilder markupBuilder;
                        markupBuilder = new System.Text.StringBuilder();

                        //Check locality of address can be displayed all on one line
                        if (club.ClubItm.Addressline1.Text.Length + club.ClubItm.Addressline2.Text.Length + club.ClubItm.Addressline3.Text.Length < Settings.MaxNumberOfCharactersInSearchResultsList)
                        {
                            markupBuilder.Append(club.ClubItm.Addressline1.Text);
                            markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline2.Text) ? " " + club.ClubItm.Addressline2.Text : "");
                            markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline3.Text) ? " " + club.ClubItm.Addressline3.Text : "");
                            markupBuilder.Append("<br />");
                            markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline4.Text) ? club.ClubItm.Addressline4.Text + " " : "");
                            markupBuilder.Append(club.ClubItm.Postcode.Text);
                        }
                        else
                        {
                            markupBuilder.Append(club.ClubItm.Addressline1.Text);
                            markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline2.Text) ? " " + club.ClubItm.Addressline2.Text : "");
                            markupBuilder.Append("<br />");
                            markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline3.Text) ? club.ClubItm.Addressline3.Text + " " : "");
                            markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline4.Text) ? club.ClubItm.Addressline4.Text + " " : "");
                            markupBuilder.Append(club.ClubItm.Postcode.Text);
                        }

                        Address.Text = markupBuilder.ToString();
                    }

                    //Get club Urls
                    var ltrClubImageLink = e.Item.FindControl("ltrClubImageLink") as System.Web.UI.WebControls.Literal;
                    var ltrClubTitleLink = e.Item.FindControl("ltrClubTitleLink") as System.Web.UI.WebControls.Literal;
                    var ltrClubCTALink = e.Item.FindControl("ltrClubCTALink") as System.Web.UI.WebControls.Literal;
                    var ltrClubLinks = e.Item.FindControl("ltrClubLinks") as System.Web.UI.WebControls.Literal;
                    

                    string ClubLinkUrl = new PageSummaryItem(club.ClubItm.InnerItem).Url;

                    Boolean IsCampaignPlaceholder = false;
                    if (club.ClubItm.IsPlaceholder.Checked == true)
                    {
                        Item campaign;

                        if (club.ClubItm.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                        {
                            campaign =
                                club.ClubItm.PlaceholderCampaign.Item.Axes.SelectSingleItem(
                                    String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId));
                        }
                        else
                        {

                            //redirect to campaign
                            campaign = club.ClubItm.PlaceholderCampaign.Item;

                        }
                        if (campaign != null)
                        {
                            //UrlOptions opt = new UrlOptions();
                            //opt.AddAspxExtension = false;
                            //opt.LanguageEmbedding = LanguageEmbedding.Never;
                            //opt.AlwaysIncludeServerUrl = true;

                            //ClubLinkUrl = LinkManager.GetItemUrl(campaign, opt);
                            IsCampaignPlaceholder = true;
                        }
                    }

                    ltrClubImageLink.Text = @"<a href=""" + ClubLinkUrl + @""">" + club.ClubItm.Clubimage.RenderCrop("180x120") + @"</a>";
                    ltrClubTitleLink.Text = @"<a href=""" + ClubLinkUrl + @""">" + club.ClubItm.Clubname.Text + @"</a>";
                    ltrClubCTALink.Text = @"<a href=""" + ClubLinkUrl + @""" class=""btn btn-cta"">" + "Visit club page" + @"</a>";

                    List<string> links = new List<string>();

                    if (IsCampaignPlaceholder == false)
                    {
                        if (club.ClubItm.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableItem.TemplateId)) != null)
                        {
                            TimetableItem timetableItem = new TimetableItem(club.ClubItm.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableItem.TemplateId)));
                            //var TimetableLink = e.Item.FindControl("TimetableLink") as System.Web.UI.WebControls.Literal;
                            //TimetableLink.Text = @"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(timetableItem) + @""" title=""" + Translate.Text("Timetables") + @""">" + Translate.Text("Timetables") + @"</a>";
                            links.Add(@"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(timetableItem) + @""" title=""" + Translate.Text("Timetables") + @""">" + Translate.Text("Timetables") + @"</a>");
                        }
                        else
                        {
                            if (club.ClubItm.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)) != null)
                            {
                                TimetableDownloadItem timetableDownloadItem = new TimetableDownloadItem(club.ClubItm.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)));
                                //var TimetableLink = e.Item.FindControl("TimetableLink") as System.Web.UI.WebControls.Literal;
                                //TimetableLink.Text = @"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(timetableDownloadItem) + @""" title=""" + Translate.Text("Timetables") + @""">" + Translate.Text("Timetables") + @"</a>";
                                links.Add(@"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(timetableDownloadItem) + @""" title=""" + Translate.Text("Timetables") + @""">" + Translate.Text("Timetables") + @"</a>");
                            }
                        }
                        if (club.ClubItm.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", ClubMembershipLandingItem.TemplateId)) != null)
                        {
                            ClubMembershipLandingItem membershipItem = new ClubMembershipLandingItem(club.ClubItm.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", ClubMembershipLandingItem.TemplateId)));
                            //var MembershipLink = e.Item.FindControl("MembershipLink") as System.Web.UI.WebControls.Literal;
                            if (IsCampaignPlaceholder == false)
                            {
                                //MembershipLink.Text = @"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(membershipItem) + @""" title=""" + Translate.Text("Membership Options") + @""">" + Translate.Text("Membership Options") + @"</a>";
                                links.Add(@"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(membershipItem) + @""" title=""" + Translate.Text("Membership Options") + @""">" + Translate.Text("Membership Options") + @"</a>");
                            }
                        }
                    }
                    else if (club.ClubItm.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                    {
                        ClubMicrositeLandingItem microsite = new ClubMicrositeLandingItem(club.ClubItm.PlaceholderCampaign.Item);

                        wrappers.VirginActive.PageTemplates.ClubMicrosites.MicrositeTimetableItem timetableItem = new wrappers.VirginActive.PageTemplates.ClubMicrosites.MicrositeTimetableItem(microsite.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant::*[@@tid = '{0}']", wrappers.VirginActive.PageTemplates.ClubMicrosites.MicrositeTimetableItem.TemplateId)));
                        links.Add(@"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(timetableItem) + @""" title=""" + Translate.Text("Timetables") + @""">" + Translate.Text("Timetables") + @"</a>");

                        wrappers.VirginActive.PageTemplates.ClubMicrosites.MembershipItem membershipItem = new wrappers.VirginActive.PageTemplates.ClubMicrosites.MembershipItem(microsite.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant::*[@@tid = '{0}']", wrappers.VirginActive.PageTemplates.ClubMicrosites.MembershipItem.TemplateId)));
                        links.Add(@"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(membershipItem) + @""" title=""" + Translate.Text("Membership Options") + @""">" + Translate.Text("Membership Options") + @"</a>");
                    }
                    else
                    {
                        ClubMembershipLandingItem membershipItem = new ClubMembershipLandingItem(club.ClubItm.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", ClubMembershipLandingItem.TemplateId)));
                        //var MembershipLink = e.Item.FindControl("MembershipLink") as System.Web.UI.WebControls.Literal;
                        //MembershipLink.Text = @"<a href=""" + ClubLinkUrl + "?page=Interested" + @""" title=""" + Translate.Text("Membership Options") + @""">" + Translate.Text("Membership Options") + @"</a>";
                        links.Add(@"<a href=""" + Sitecore.Links.LinkManager.GetItemUrl(membershipItem) + @""" title=""" + Translate.Text("Membership Options") + @""">" + Translate.Text("Membership Options") + @"</a>");
                    }


                    System.Text.StringBuilder linkBuilder;
                    linkBuilder = new System.Text.StringBuilder();
                    int j = 0;

                    foreach(string link in links)
                    {
                        if (j == 0)
                        {
                            linkBuilder.Append(@"<li class=""club-link-first"">");
                        }
                        else
                        {
                            linkBuilder.Append(@"<li class=""club-link"">");
                        }
                        linkBuilder.Append(link);
                        linkBuilder.Append("</li>");

                        j++;
                    }

                    ltrClubLinks.Text = linkBuilder.ToString();
                }
            }
        }

        public static string RenderToString(double? lat, double? lng, string filter, string searchTerm)
        {
            return SitecoreHelper.RenderUserControl<ClubSearchResultsList>("~/layouts/virginactive/ajax/ClubSearchResultsList.ascx",
                uc =>
                {
                    uc.Lat =  lat;
                    uc.Lng = lng;
                    uc.Filters = filter;
                    uc.SearchTerm = searchTerm;
                });
        }
    }
}