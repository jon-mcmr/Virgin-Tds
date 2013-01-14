using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.web.layouts.virginactive.ajax;
using Sitecore.Data.Items;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Diagnostics;
using Sitecore.Web;
using mm.virginactive.common.Helpers;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.virginactive.clubfinder
{
    public partial class ClubResults : System.Web.UI.UserControl
    {
        protected string location = "";
        protected double Lat = 0.00;
        protected double Lng = 0.00;
        protected string resultsStr = "";
        protected string Filter = "";
        protected string thisUrl = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {         
            
            //Item[] clubsItms = Sitecore.Context.Database.SelectItems(String.Format("fast:{0}/*", ItemPaths.Clubs));
            Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
            ChildList clubLst = clubRoot.Children;
            Item[] clubsItms = clubLst.ToArray();

            ClubFinderResultsItem current = new ClubFinderResultsItem(Sitecore.Context.Item);
            thisUrl = current.PageSummary.QualifiedUrl;

            /* Populate club finder selector */
            if (clubFindSelect.Items.Count == 0 && clubsItms != null)
            { //If this is not 0, then we have already filled this list
                List<ClubItem> clubs = clubsItms.ToList().ConvertAll(X => new ClubItem(X));


                clubs.RemoveAll(x => x.IsHiddenFromMenu());
                //clubs.RemoveAll(x => x.IsPlaceholder.Checked);

                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));
                foreach (ClubItem clubItem in clubs)
                {                    
                    if (clubItem != null)
                    {
                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            //check if club is just a placeholder for a campaign
                            if (clubItem.IsPlaceholder.Checked == true)
                            {
                                if (clubItem.PlaceholderCampaign.Item != null)
                                {
                                    if (clubItem.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                                    {
                                        clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.PlaceholderCampaign.Item.Axes.SelectSingleItem(
                                                String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId)).ID.ToShortID().ToString()));
                                    }
                                    else
                                    {
                                        clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.PlaceholderCampaign.Item.ID.ToShortID().ToString()));
                                    }
                                }
                                else
                                {
                                    clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                                }
                            }
                            else
                            {
                                clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                            }
                        }
                    }
                }
            }
                //handle  search query
            ClubSearchResultsList resultListView = this.Page.LoadControl("~/layouts/virginactive/ajax/ClubSearchResultsList.ascx") as  ClubSearchResultsList;

            try
            {

                LocationPointerItem pointer = null;
                if(Sitecore.Context.Item.TemplateID.ToString() == LocationPointerItem.TemplateId) {
                    pointer = new LocationPointerItem(Sitecore.Context.Item);
                }


                location = WebUtil.GetQueryString("searchLoc", pointer != null ? pointer.LocationName.Raw : "");


                string sLat = WebUtil.GetQueryString("lat", pointer != null ? pointer.Lat.Raw : "0.00");

                Double.TryParse(sLat, out Lat);

                string sLng = WebUtil.GetQueryString("lng", pointer != null? pointer.Long.Raw : "0.00");


                Double.TryParse(sLng, out Lng);

                string sFltr = WebUtil.GetQueryString("filter", "");
                Sitecore.Data.ShortID guid = null;

                if (Sitecore.Data.ShortID.TryParse(sFltr, out guid))
                {
                    Filter = sFltr;
                }
                            
                resultListView.Lat = Lat;
                resultListView.Lng = Lng;
                resultListView.SearchTerm = location;
                if (!String.IsNullOrEmpty(Filter))
                {
                    resultListView.Filters = Filter;
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error handling club finder request for values loc:{0}, lat:{1}, lng:{2} error:{3}", location, Lat.ToString(), Lng.ToString(), ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
            
            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {

                if (!String.IsNullOrEmpty(location))
                {
                    scriptPh.Controls.Add(new LiteralControl(@"<script>
		            $(function(){
                        showOnMap();
                        clubFinderAutocomplete();
                        GetSuggestions('" + location + @"');
                        viewClubFinderResults();
                        $.va_init.functions.setupAccordions();
                        $.va_init.functions.setupClubFilter();
	                    $.va_init.functions.setupForms();
                    });
                </script>
                <script src='/virginactive/scripts/infobox_packed.js'></script>
                "));
                }
                

            }
            
            resultPh.Controls.Add(resultListView);
            resultsStr = resultListView.MatchingResults;

        }
    }
}