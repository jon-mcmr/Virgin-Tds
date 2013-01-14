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
using mm.virginactive.common.Helpers;
using System.Text;
using Sitecore.Web;
using System.IO;
using Newtonsoft.Json;
using mm.virginactive.controls.Model;
using mm.virginactive.controls.Util;
using System.Web.Caching;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using Sitecore.Links;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
	public partial class ClubDetailsList : System.Web.UI.UserControl
    {
        protected string ClubDetails = "";
		private double? lat;
		private double? lng;
		
		private string searchTerm;
		protected List<Club> clubs;
		protected string matchingResults = "";

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

        public string LandingId
        {

            get;
            set;
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
            int NoOfResultsToShow = Convert.ToInt32(SitecoreHelper.GetSitecoreSetting("No of Club Results To Show"));

            Item currentItem = Sitecore.Context.Item;

            List<Club> clubs = null;

            clubs = GetClubListByLandingId(clubs);

            if(clubs!=null)
                clubs = ClubUtil.GetNearestClubs(Lat, Lng, NoOfResultsToShow, clubs);
            else
                clubs = ClubUtil.GetNearestClubs(Lat, Lng, NoOfResultsToShow);

           

			MatchingResults = String.Format(Translate.Text("{0} clubs matching your search criteria"), clubs != null ? clubs.Count : 0);

            StringWriter result = new StringWriter();

            using (JsonTextWriter w = new JsonTextWriter(result))
            {

                w.WriteStartArray();
                foreach (Club club in clubs)
                {
                    ClubItem clubItem = club.ClubItm;

                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = false;
                    urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                    string clubUrl = Sitecore.Links.LinkManager.GetItemUrl(currentItem, urlOptions) + "/enquiry?_clubId=" + clubItem.ClubId.Rendered;
                    string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();
					//WriteClubAsJson(w, clubLabel, club.DistanceFromSource.ToString(), clubItem.InnerItem.ID.ToString());
                    //WriteClubAsJson(w, clubLabel, club.DistanceFromSource.ToString(),  new PageSummaryItem(clubItem.InnerItem).Url);
					WriteClubAsJson(w, clubLabel, String.Format("{0:0.00}", club.DistanceFromSource), clubUrl);
                }

                w.WriteEndArray();
                ClubDetails = result.ToString();
            }
        }

        private List<Club> GetClubListByLandingId(List<Club> clubs)
        {
            if (!String.IsNullOrEmpty(LandingId))
            {
                Item item = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(LandingId));

                ProfileLandingPageItem profileItem = new ProfileLandingPageItem(item);



                if (profileItem != null)
                {
                    if (profileItem.LandingPage.LandingBase.Usecustomclublist.Checked)
                    {
                        //Use a custom list of clubs
                        clubs = profileItem.LandingPage.LandingBase.Customclublist.ListItems.ConvertAll(X => new Club(X));


                    }

                    if (clubs != null)
                    {
                        StringBuilder facilitiesList = new StringBuilder();

                        if (profileItem.FacilityorClass != null)
                        {
                            List<Item> clubFacs = profileItem.FacilityorClass.ListItems;

                            if (clubFacs != null)
                            {
                                string delimiter = "";
                                foreach (Item clubFacItem in clubFacs)
                                {
                                    facilitiesList.Append(delimiter);
                                    facilitiesList.Append(clubFacItem.ID);
                                    delimiter = ",";

                                }


                                clubs = ClubUtil.FilterClubs(clubs, facilitiesList.ToString());
                            }
                        }
                    }
                    else
                    {
                        StringBuilder facilitiesList = new StringBuilder();

                        if (profileItem.FacilityorClass != null)
                        {
                            List<Item> clubFacs = profileItem.FacilityorClass.ListItems;

                            if (clubFacs != null)
                            {
                                string delimiter = "";
                                foreach (Item clubFacItem in clubFacs)
                                {
                                    facilitiesList.Append(delimiter);
                                    facilitiesList.Append(clubFacItem.ID);
                                    delimiter = ",";
                                }

                                //Use a custom list of clubs
                                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                                ChildList clubLst = clubRoot.Children;
                                clubs = clubLst.ToList().ConvertAll(X => new Club(X));
                                //clubs.RemoveAll(x => x.IsHiddenFromMenu());

                                clubs = ClubUtil.FilterClubs(clubs, facilitiesList.ToString());
                            }
                        }
                    }


                }



            }
            return clubs;
        }

        private void WriteClubAsJson(JsonTextWriter writer, string clubName, string distanceFromSource, string clubURL)
        {

            writer.WriteStartObject();      //  {
            writer.WritePropertyName("clubname"); //      "value" : 
			writer.WriteValue(clubName);   //          "...", 
            writer.WritePropertyName("distanceFromSource");   //      "label" :
			writer.WriteValue(distanceFromSource);     //          "..."
            writer.WritePropertyName("clubURL"); //      "value" : 
            writer.WriteValue(clubURL);   //          "...", 
            writer.WriteEndObject();        //  }
        }

		public static string RenderToString(double? lat, double? lng, string landingId, string searchTerm)
		{
			return SitecoreHelper.RenderUserControl<ClubDetailsList>("~/layouts/virginactive/ajax/ClubDetailsList.ascx",
				uc =>
				{
					uc.Lat = lat;
					uc.Lng = lng;
                    uc.LandingId = landingId;
					uc.SearchTerm = searchTerm;
				});
		}
    }
}