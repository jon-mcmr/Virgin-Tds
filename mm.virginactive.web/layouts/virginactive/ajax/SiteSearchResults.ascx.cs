using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.common.Helpers;
using System.Text;
using Sitecore.Web;
using System.IO;
using Newtonsoft.Json;
using Sitecore.SharedSource.Search;
using Sitecore.SharedSource.Search.Parameters;
using Sitecore.SharedSource.Search.Utilities;
using System.Diagnostics;
using Sitecore.Caching;
using Sitecore.SharedSource.Search.Scripts;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using System.Text.RegularExpressions;
using Sitecore.Links;


namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class SiteSearchResults : System.Web.UI.UserControl
    {
        private string query;

        public string Query
        {
            get { return query; }
            set { query = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean specificSearchMatch = false;
            SkinnyItem SearchPrompt = null;

            //Check for special search term matches
            Item componentBase = Sitecore.Context.Database.GetItem(ItemPaths.Components);

            if (componentBase != null)
            {
                Item[] terms = componentBase.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", SearchTermItem.TemplateId));
                if (terms != null)
                {
                    foreach (Item term in terms)
                    {
                        SearchTermItem itm = new SearchTermItem(term);

                        if (itm.Predictiveterm.Raw.Trim() != "")
                        {
                            Regex match = new Regex(itm.Predictiveterm.Raw);
                            if (match.IsMatch(Query))
                            {
                                specificSearchMatch = true;
                                SearchPrompt = new SkinnyItem(itm.InnerItem.Uri);
                                break;
                            }
                        }

                    }
                }
            }

            if (!String.IsNullOrEmpty(Query))
            {
                StringWriter result = new StringWriter();

                using (JsonTextWriter w = new JsonTextWriter(result))
                {
                    w.WriteStartArray();

                    //Advanced Database Search
                    var resultItems = new List<SkinnyItem>();
                    //var stopwatch = new Stopwatch();
                    try
                    {
                        //stopwatch.Start();
                        resultItems.AddRange(GetItems());
                        //stopwatch.Stop();

                        //Categorise the results
                        var clubResultList = new List<SkinnyItem>();
                        var classResultList = new List<SkinnyItem>();
                        var facilityResultList = new List<SkinnyItem>();
                        
                        foreach (var resultItem in resultItems)
                        {
                            //check if hidden from menu
                            if (resultItem.Fields["_ishiddenfrommenu"] != null)
                            {
                                if (resultItem.Fields["_ishiddenfrommenu"].ToString() == "false" || resultItem.Fields["_showinsearchbar"].ToString() == "true")
                                {
                                    switch (resultItem.TemplateName)
                                    {
                                        case Templates.ClassicClub:
                                            clubResultList.Add(resultItem);
                                            break;
                                        case Templates.LifeCentre:
                                            clubResultList.Add(resultItem);
                                            break;
                                        case Templates.ClassModule:
                                            classResultList.Add(resultItem);
                                            break;
                                        case Templates.FacilityModule:
                                            facilityResultList.Add(resultItem);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                switch (resultItem.TemplateName)
                                {
                                    case Templates.ClassicClub:
                                        clubResultList.Add(resultItem);
                                        break;
                                    case Templates.LifeCentre:
                                        clubResultList.Add(resultItem);
                                        break;
                                    case Templates.ClassModule:
                                        classResultList.Add(resultItem);
                                        break;
                                    case Templates.FacilityModule:
                                        facilityResultList.Add(resultItem);
                                        break;
                                }
                            }
                        }

                        var filteredSortedList = new List<SkinnyItem>();

                        int j = 0; //total search result count
                        int i = 0; //category search result count

                        int categorySearchResultLimit = Settings.MaxCategorySearchResults;

                        int totalSearchResultLimit = Settings.MaxTotalSearchResults;

                        //filter and reorder the result set
                        foreach (var item in clubResultList)
                        {
                            //only read top x results
                            if (i < categorySearchResultLimit && j < totalSearchResultLimit)
                            {
                                filteredSortedList.Add(item);
                            }
                            else { break; }
                            i++;
                            j++;
                        }
                        //If specific search match found then add "more club results exist for this search" message and link (e.g. searching for London, Manchester)
                        if (i == categorySearchResultLimit && SearchPrompt != null)
                        {
                            filteredSortedList.Add(SearchPrompt);
                        }


                        i = 0;
                        foreach (var item in classResultList)
                        {
                            //only read top x results
                            if (i < categorySearchResultLimit && j < totalSearchResultLimit)
                            {
                                filteredSortedList.Add(item);
                            }
                            else { break; }
                            i++;
                            j++;
                        }
                        i = 0;

                        foreach (var item in facilityResultList)
                        {
                            //Only show in results if facility exists as a GUID in MultiList in main site area -N.B. potentially take out if taking too long

                            //Get the facilities from landing page item's facility list
                            FacilitiesLandingItem facilitiesLandingItem = Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesLanding);
                            MultilistField facilityLandingList = facilitiesLandingItem.InnerItem.Fields["Facility List"];

                            foreach (Item facilityModuleItem in facilityLandingList.GetItems())
                            {
                                if (facilityModuleItem.Name == item.Name)
                                {
                                    //Facility is in main site -Add to results list

                                    //only read top x results
                                    if (i < categorySearchResultLimit && j < totalSearchResultLimit)
                                    {
                                        filteredSortedList.Add(item);
                                    }
                                    else { break; }
                                    i++;
                                    j++;                                    
                                }
                            }
                        }

                        //write out results
                        foreach (var filteredSortedItem in filteredSortedList)
                        {
                            Item resultItem = filteredSortedItem.GetItem();
                            //Get the markup for presenting the result
                            string resultHTML = SearchResult.RenderToString(resultItem, Query);
                            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                            urlOptions.AddAspxExtension = false;
                            urlOptions.AlwaysIncludeServerUrl = true;
                            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;
                            string resultUrl = "";

                            switch (resultItem.TemplateName)
                            {
                                case Templates.SearchTerm:
                                    string clubFinderUrl = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClubResults)).Url;
                                    System.Text.StringBuilder url = new System.Text.StringBuilder();
                                    url.Append(clubFinderUrl);
                                    if (resultItem.Fields["Lat"] != null && resultItem.Fields["Long"] != null)
                                    {
                                        url.Append("?lat=" + resultItem.Fields["Lat"].Value);
                                        url.Append("&lng=" + resultItem.Fields["Long"].Value);
                                        url.Append("&searchloc=" + resultItem.DisplayName);
                                        url.Append("&ste=" + Query);
                                        url.Append("&sty=2");
                                    }
                                    resultUrl = url.ToString();
                                    break;
                                case Templates.ClassicClub:
                                    ClubItem ClubItm = new ClubItem(resultItem);

                                    //check if club is just a placeholder for a campaign
                                    if (ClubItm.IsPlaceholder.Checked == true)
                                    {
                                        Item campaign;

                                        if (ClubItm.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                                        {
                                            campaign =
                                                ClubItm.PlaceholderCampaign.Item.Axes.SelectSingleItem(
                                                    String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId));
                                        }
                                        else
                                        {

                                            //redirect to campaign
                                            campaign = ClubItm.PlaceholderCampaign.Item;

                                        }

                                        if (campaign != null)
                                        {
                                            resultUrl = LinkManager.GetItemUrl(campaign, urlOptions);
                                        }
                                        else
                                        {
                                            resultUrl = Sitecore.Links.LinkManager.GetItemUrl(resultItem, urlOptions);
                                        }
                                    }
                                    else
                                    {
                                        resultUrl = Sitecore.Links.LinkManager.GetItemUrl(resultItem, urlOptions);
                                    }
                                    break;
                                case Templates.LifeCentre:
                                    CheckboxField IsPlaceholderLife = resultItem.Fields["Is Placeholder"];
                                    if (IsPlaceholderLife.Checked == true)
                                    {
                                        //redirect to campaign
                                        Item campaign = Sitecore.Context.Database.GetItem(resultItem.Fields["Placeholder Campaign"].Value);

                                        if (campaign != null)
                                        {
                                            UrlOptions opt = new UrlOptions();
                                            opt.AddAspxExtension = false;
                                            opt.LanguageEmbedding = LanguageEmbedding.Never;
                                            opt.AlwaysIncludeServerUrl = true;

                                            resultUrl = LinkManager.GetItemUrl(campaign, opt);
                                        }
                                        else
                                        {
                                            resultUrl = Sitecore.Links.LinkManager.GetItemUrl(resultItem, urlOptions);
                                        }
                                    }
                                    else
                                    {
                                        resultUrl = Sitecore.Links.LinkManager.GetItemUrl(resultItem, urlOptions);
                                    }
                                    break;
                                default:
                                    resultUrl = Sitecore.Links.LinkManager.GetItemUrl(resultItem, urlOptions);
                                    break;
                            }

                            //Write out html and result into JSON object
                            WriteResultAsJson(w, resultHTML, resultUrl);

                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(String.Format("Unable to load search results: {0}", ex.Message), null);
                        mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                    }
                    finally
                    {
                        //stopwatch.Stop();
                    }


                    w.WriteEndArray();

                    Response.Write(result.ToString());
                }
            }
            else
            {
                Log.Error(String.Format("Unable to load search results: No query passed in"), null);
            }
        }


        public static string RenderToString(string searchTerm)
        {
            return SitecoreHelper.RenderUserControl<SiteSearchResults>("~/layouts/virginactive/ajax/SiteSearchResults.ascx",
                uc =>
                {
                    uc.Query = searchTerm;
                });
        }

        private void WriteResultAsJson(JsonTextWriter writer, string resultHTML, string resultUrl)
        {
            writer.WriteStartObject();      //  {
            //writer.WritePropertyName("requestTerm");   //      "term" :
            //writer.WriteValue(Query);     //          "..."
            writer.WritePropertyName("resultUrl"); //      "url" : 
            writer.WriteValue(resultUrl);   //          "...", 
            writer.WritePropertyName("label");   //      "label" :
            writer.WriteValue(resultHTML);     //          "..."
            writer.WriteEndObject();        //  }
        }

        private List<SkinnyItem> GetItems()
        {
            return GetItems("fullSiteSearch", Sitecore.Context.Language.Name, "", "", Query);
        }

        private List<SkinnyItem> GetItems(string indexName,
                                                  string language,
                                                  string templateFilter,
                                                  string locationFilter,
                                                  string fullTextQuery)
        {
            var searchParam = new SearchParam
            {
                Language = language,
                TemplateIds = templateFilter,
                LocationIds = locationFilter,
                FullTextQuery = fullTextQuery,
                ShowAllVersions = false
            };

            using (var searcher = new Searcher(indexName))
            {
                return searcher.GetItems(searchParam);
            }
        }

    }
}