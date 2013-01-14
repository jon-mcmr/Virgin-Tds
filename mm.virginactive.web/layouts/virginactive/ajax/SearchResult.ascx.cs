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
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.common.Helpers;
using System.Text;
using Sitecore.Web;
using Sitecore.Caching;
using System.IO;
using Newtonsoft.Json;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class SearchResult : System.Web.UI.UserControl
    {

        protected Item resultItem;
        private string cssClass = "";
        private string displayName = "";
        private string resultCategory = "";
        private string description = "";
        private string query;
        private string resultImageSrc = "/virginactive/images/placeholders/predictive-search-1.jpg";

        public string Query
        {
            get { return query; }
            set { query = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string ResultCategory
        {
            get { return resultCategory; }
            set { resultCategory = value; }
        }

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public string ResultImageSrc
        {
            get { return resultImageSrc; }
            set { resultImageSrc = value; }
        }

        public Item ResultItem
        {
            get { return resultItem; }
            set
            {
                resultItem = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (resultItem != null)
            {
                System.Text.StringBuilder markupBuilder;

                //highlight the search term match in name field
                displayName = !String.IsNullOrEmpty(Query) ? SearchHelper.HighlightSearchTerm(Query, resultItem.DisplayName) : resultItem.DisplayName;


                switch (resultItem.TemplateName)
                {
                    case Templates.ClassicClub:
                        cssClass = "Club";
                        resultCategory = "CLUB";

                        ClubItem classicClub = (ClubItem)resultItem;

                        //Format Contact Address
                        markupBuilder = new System.Text.StringBuilder();

                        //markupBuilder.Append(classicClub.Addressline1.Rendered != "" ? classicClub.Addressline1.Rendered : "");
                        //markupBuilder.Append(classicClub.Addressline2.Rendered != "" ? " " + classicClub.Addressline2.Rendered : "");
                        //markupBuilder.Append(classicClub.Addressline3.Rendered != "" ? " " + classicClub.Addressline3.Rendered : "");
                        //markupBuilder.Append("<br />");
                        //markupBuilder.Append(classicClub.Addressline4.Rendered != "" ? classicClub.Addressline4.Rendered + " " : "");
                        //markupBuilder.Append(classicClub.Postcode.Rendered);

                        markupBuilder.Append(classicClub.Addressline4.Rendered != "" ? classicClub.Addressline4.Rendered : classicClub.Addressline3.Rendered);

                        //Check if postcode match
                        if (classicClub.Postcode.Rendered.ToUpper().IndexOf(query.ToUpper()) != -1)
                        {
                            markupBuilder.Append(markupBuilder.Length == 0 ? classicClub.Postcode.Rendered : ", " + classicClub.Postcode.Rendered);
                        }

                        ResultImageSrc = classicClub.Clubimage.RenderCrop("70x45");
                        //Show formatted address
                        description = markupBuilder.ToString();

                        //highlight the search term match in description field
                        description = !String.IsNullOrEmpty(Query) ? SearchHelper.HighlightSearchTerm(Query, description) : description;

                        break;
                    case Templates.LifeCentre:
                        cssClass = "Club";
                        resultCategory = "CLUB";

                        ClubItem club = (ClubItem)resultItem;

                        //Format Contact Address
                        markupBuilder = new System.Text.StringBuilder();

                        //markupBuilder.Append(club.Addressline1.Rendered != "" ? club.Addressline1.Rendered : "");
                        //markupBuilder.Append(club.Addressline2.Rendered != "" ? " " + club.Addressline2.Rendered : "");
                        //markupBuilder.Append(club.Addressline3.Rendered != "" ? " " + club.Addressline3.Rendered : "");
                        //markupBuilder.Append("<br />");
                        //markupBuilder.Append(club.Addressline4.Rendered != "" ? club.Addressline4.Rendered + " " : "");
                        //markupBuilder.Append(club.Postcode.Rendered);

                        markupBuilder.Append(club.Addressline4.Rendered != "" ? club.Addressline4.Rendered : club.Addressline3.Rendered);

                        //Check if postcode match
                        if (club.Postcode.Rendered.ToUpper().IndexOf(query.ToUpper()) != -1)
                        {
                            markupBuilder.Append(markupBuilder.Length == 0 ? club.Postcode.Rendered : ", " + club.Postcode.Rendered);
                        }
                        
                        ResultImageSrc = club.Clubimage.RenderCrop("70x45");

                        //Show formatted address
                        description = markupBuilder.ToString();

                        //highlight the search term match in description field
                        description = !String.IsNullOrEmpty(Query) ? SearchHelper.HighlightSearchTerm(Query, description) : description;

                        break;
                    case Templates.ClassModule:
                        cssClass = "Class";
                        resultCategory = "FACILITIES & CLASSES/CLASSES";

                        ClassModuleItem classModule = (ClassModuleItem)resultItem;
                        description = classModule.Searchsummary.Rendered;

                        //highlight the search term match in description field
                        description = !String.IsNullOrEmpty(Query) ? SearchHelper.HighlightSearchTerm(Query, description) : description;

                        Item classParent = classModule.InnerItem.Parent;
                        if (classParent.TemplateID.ToString() == ClassesSubListingItem.TemplateId)
                        {
                            ResultImageSrc = new ClassesSubListingItem(classParent).Abstract.Image.RenderCrop("70x45");
                        }
                        break;
                    case Templates.FacilityModule:
                        cssClass = "Facility";
                        resultCategory = "FACILITIES & CLASSES/FACILITIES";

                        FacilityModuleItem facilityModule = (FacilityModuleItem)resultItem;
                        description = facilityModule.Searchsummary.Rendered;

                        //highlight the search term match in description field
                        description = !String.IsNullOrEmpty(Query) ? SearchHelper.HighlightSearchTerm(Query, description) : description;

                        ResultImageSrc = facilityModule.Abstract.Image.RenderCrop("70x45");
                        break;
                    case Templates.SearchTerm:
                        //Display "more club results exist for this search" message
                        displayName = "";
                        cssClass = "searchTerm";
                        ResultImageSrc = "";

                        //Build link url
                        string DisplayText = String.Format(@Translate.Text("We have loads of clubs in {0}."), resultItem.DisplayName);

                        //if(DisplayText.IndexOf("<") != -1 && DisplayText.IndexOf(">") != -1)
                        //{
                        //    string LinkText = DisplayText.Substring(DisplayText.IndexOf("<") + 1, DisplayText.IndexOf(">") - DisplayText.IndexOf("<") - 1);

                        //    //Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                        //    //urlOptions.AlwaysIncludeServerUrl = true;
                        //    string clubFinderUrl = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClubResults)).Url;

                        //    System.Text.StringBuilder urlLink = new System.Text.StringBuilder();
                        //    urlLink.Append(@"<a href=""" + clubFinderUrl);

                        //    if (resultItem.Fields["Lat"] != null && resultItem.Fields["Long"] != null)
                        //    {
                        //        urlLink.Append("?lat=" + resultItem.Fields["Lat"].Value);
                        //        urlLink.Append("&lng=" + resultItem.Fields["Long"].Value);
                        //        urlLink.Append("&searchloc=" + resultItem.DisplayName);
                        //        urlLink.Append("&ste=" + Query);
                        //        urlLink.Append("&sty=2");
                        //    }
                        //    urlLink.Append(@""" >" + LinkText + "</a>");
                        //    DisplayText = DisplayText.Replace("<" + LinkText + ">", urlLink.ToString());
                        //}

                        description = DisplayText;

                        ResultWrap.Attributes.Add("class", cssClass);
                        ResultWrap.Attributes.Add("data-seeallclubs", Translate.Text("Find your nearest club") + " >");
                        break;
                }
            }
            else
            {
            }
        }

        public static string RenderToString(Item resultItem, string query)
        {
            return SitecoreHelper.RenderUserControl<SearchResult>("~/layouts/virginactive/ajax/SearchResult.ascx",
                uc =>
                {
                    uc.ResultItem = resultItem;
                    uc.Query = query;
                });
        }

    }
}