using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using Sitecore.Web;
using mm.virginactive.web.layouts.virginactive.ajax;
using System.Text;
using System.IO;
using Sitecore.Data;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using Sitecore.Diagnostics;
using Newtonsoft.Json;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class AjaxHandler : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string command = WebUtil.GetQueryString("cmd");
  
            switch (command) {
                case "MapResults" :
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetClubSearchMapResult());
                    break;
                case "ReciprocalAccessResults":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetReciprocalAccessResults());
                    break;
                case "ClubList" :
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetClubSearchResultList());
                    break;
                case "GetMemberClubListView" :
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetMemberClubSearchResultList());
                    break;
                case "GetMemberClubMapView":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetMemberClubSearchMapResult());
                    break;
                case "ClubTimetable":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetClubTimetables());
                    break;
                case "ClubNamesList":
                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write(GetClubNamesList());
                    break;
				case "ClubDetailsList":
					Response.Clear();
					Response.ContentType = "application/json";
					Response.Write(GetClubDetailsList());
					break;
                case "GetClubUrlForId":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetClubUrlForId());
                    break;
                case "GetClubResultsUrl":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetClubResultsUrl());
                    break;
                case "SiteSearchResults":
                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write(GetSiteSearchResults());
                    break;   
                case "XmlSitemap":
                    Response.Clear();
                    Response.ContentType = "text/xml";
                    GetSitemapIndex();
                    break;
                case "sitemap" :
                    Response.Clear();
                    Response.ContentType = "text/xml";
                    GetSitemap();
                    break;
                case "GetWorkoutSection":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetWorkoutSection());
                    break;
                case "SetHomeClub":
                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write(SetHomeClub());
                    break;
                case "KeepAlive":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write("I live!");
                    break;
                case "GetSuggestions":
                     Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetLocationSuggestions());
                    break;
                case "ClubContactDetailsResults":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetClubContactDetailsResults());
                    break;
                case "GetMemberDetails":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(GetMemberDetails());
                    break;
                case "SetDisableCookies":
                    Response.Clear();
                    Response.ContentType = "text/html";
                    Response.Write(SetDisableCookies());
                    break;
                case "SetGalleryPreference":
                    Response.Clear();
                    Response.ContentType = "text/htm";
                    Response.Write(SetGalleryPreference());
                    break;
            }
                
            Response.End();

        }
 

        #region Control instanciations

        private string GetClubSearchResultList()
        {
            string lat = WebUtil.GetQueryString("lat");
            string lng = WebUtil.GetQueryString("lng");
            string flt = WebUtil.GetQueryString("flt");
            string loc = WebUtil.GetQueryString("loc");
            
            double dLat;
            double dLng;
            Double.TryParse(lat, out dLat);
            Double.TryParse(lng, out dLng);

            return ClubSearchResultsList.RenderToString((double?)dLat, (double?)dLng, flt, loc);
        }

        private string GetClubSearchMapResult()
        {
            string lat = WebUtil.GetQueryString("lat");
            string lng = WebUtil.GetQueryString("lng");
            string flt = WebUtil.GetQueryString("flt");
            string loc = WebUtil.GetQueryString("loc");

            double dLat;
            double dLng;
            Double.TryParse(lat, out dLat);
            Double.TryParse(lng, out dLng);

            return ClubSearchResultsMap.RenderToString((double?)dLat, (double?)dLng, flt, loc);
        }

        private string GetMemberClubSearchResultList()
        {
            string lat = WebUtil.GetQueryString("lat");
            string lng = WebUtil.GetQueryString("lng");
            string loc = WebUtil.GetQueryString("loc");

            double dLat;
            double dLng;
            Double.TryParse(lat, out dLat);
            Double.TryParse(lng, out dLng);

            return MembershipCampaignSearchResult.RenderToString((double?)dLat, (double?)dLng, loc);
        }

        private string GetMemberClubSearchMapResult()
        {
            string lat = WebUtil.GetQueryString("lat");
            string lng = WebUtil.GetQueryString("lng");
            string loc = WebUtil.GetQueryString("loc");

            double dLat;
            double dLng;
            Double.TryParse(lat, out dLat);
            Double.TryParse(lng, out dLng);

            return MembershipCampaignMapResult.RenderToString((double?)dLat, (double?)dLng, loc);
        }

        private string GetReciprocalAccessResults()
        {
            string membershipNumber = WebUtil.GetQueryString("memNo");
            string dateOfBirth = WebUtil.GetQueryString("dob");

            return ReciprocalAccessResults.RenderToString(membershipNumber, dateOfBirth);
        }

        private string GetClubTimetables()
        {
            string clubId = WebUtil.GetQueryString("clubId");
            string timetableType = WebUtil.GetQueryString("timetableType");
            bool showBookClassTooltip = WebUtil.GetQueryString("showBookClassTooltip") == "true" ? false : true;
            string bookClassTooltip = WebUtil.GetQueryString("bookClassTooltip");
            return ClubTimetableResult.RenderToString(clubId, timetableType, showBookClassTooltip, bookClassTooltip);
        }

        private string GetClubNamesList()
        {
            string query = WebUtil.GetQueryString("term");
            string filter = WebUtil.GetQueryString("filter");
            //bool includeExporta = WebUtil.GetQueryString("excludeEx") == "true" ? false : true;
            //return ClubNameList.RenderToString(query, filter, includeExporta);
            return ClubNameList.RenderToString(query, filter);	   
        }

		private string GetClubDetailsList()
		{
			string lat = WebUtil.GetQueryString("lat");
			string lng = WebUtil.GetQueryString("lng");
			string landingid = WebUtil.GetQueryString("landingid");
			string loc = WebUtil.GetQueryString("loc");

			double dLat;
			double dLng;
			Double.TryParse(lat, out dLat);
			Double.TryParse(lng, out dLng);

            return ClubDetailsList.RenderToString((double?)dLat, (double?)dLng, landingid, loc);
		}

        private string GetSiteSearchResults()
        {
            string query = WebUtil.GetQueryString("term");
            return SiteSearchResults.RenderToString(query);
        }

        private string GetWorkoutSection()
        {
            string categoryGuid = WebUtil.GetQueryString("catGuid");
            //return "<strong>blh blah blah</strong>";
            return WorkoutZones.RenderToString(categoryGuid);
        }

        private string GetLocationSuggestions()
        {
            string url = WebUtil.GetQueryString("loc");
            string term = WebUtil.GetQueryString("term");
            return RegionSuggestions.RenderToString(term, url);
        }

        private string GetClubContactDetailsResults()
        {
            string clubId = WebUtil.GetQueryString("clubId");
            return ClubContactDetails.RenderToString(clubId);
        }

        private string GetMemberDetails()
        {
            string swipeNo = WebUtil.GetQueryString("swipeNo");
            string dateOfBirth = WebUtil.GetQueryString("dateOfBirth");
            string postcode = WebUtil.GetQueryString("postcode");
            return MemberDetails.RenderToString(swipeNo, dateOfBirth, postcode);
        }



        private string SetHomeClub()
        {
            try
            {
                string clubId = WebUtil.GetQueryString("clubId");
                bool clubIsHomeClub = false;
                if (WebUtil.GetQueryString("clubIsHomeClub") == "true")
                {
                    clubIsHomeClub = true;
                }

                User objUser = new User();
                //Set user session variable
                if (Session["sess_User"] != null)
                {
                    objUser = (User)Session["sess_User"];
                }

                if (!clubIsHomeClub)
                {
                    //Set Home Club ID User Session
                    objUser.HomeClubID = clubId;
                    Session["sess_User"] = objUser;

                    //Set Home Club ID Cookie
                    CookieHelper.AddUpdateCookie(CookieKeyNames.HomeClub, clubId);

                    //Get club url
                    ClubItem club = SitecoreHelper.GetClubOnClubId(clubId);
                    if (club != null)
                    {
                        Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                        urlOptions.AlwaysIncludeServerUrl = true;
                        urlOptions.AddAspxExtension = true;
                        urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                        StringWriter result = new StringWriter();

                        using (JsonTextWriter writer = new JsonTextWriter(result))
                        {
                            writer.WriteStartArray();
                            writer.WriteStartObject();
                            writer.WritePropertyName("resultUrl");
                            writer.WriteValue(Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions));
                            writer.WriteEndObject();
                            writer.WriteEndArray();
                        }

                        return result.ToString();
                    }
                }
                else
                {
                    //Set Home Club ID User Session
                    objUser.HomeClubID = "";
                    Session["sess_User"] = objUser;

                    //Set Home Club ID Cookie
                    CookieHelper.AddUpdateCookie(CookieKeyNames.HomeClub, "");
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Unable to set club home page cookie: {0}", ex.Message), null);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
            return "";
        }

        private string SetDisableCookies()
        {
            try
            {
                User objUser = new User();
                //Set user session variable
                if (Session["sess_User"] != null)
                {
                    objUser = (User)Session["sess_User"];
                }

                //Set Preferences in User Session
                Preferences preferences = new Preferences();

                preferences.MarketingCookies = false;
                preferences.MetricsCookies = false;
                preferences.PersonalisedCookies = false;
                preferences.SocialCookies = false;

                objUser.Preferences = preferences;
                Session["sess_User"] = objUser;

                //Set Cookie Preferences Cookie
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MarketingCookies, "N");
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MetricsCookies, "N");
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.PersonalisedCookies, "N");
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.SocialCookies, "N");

                //Delete Existing Personalisation Cookie
                CookieHelper.DeleteCookie();

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Unable to set preferences cookie: {0}", ex.Message), null);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
            return "";
        }

        private string SetGalleryPreference()
        {
            try
            {
                bool showGallery = false;
                if (WebUtil.GetQueryString("show") == "true")
                {
                    showGallery = true;
                }
                User objUser = new User();
                //Set user session variable
                if (Session["sess_User"] != null)
                {
                    objUser = (User)Session["sess_User"];
                }

                //Set Gallery Preferences in User Session
                objUser.ShowGallery = showGallery;
                
                Session["sess_User"] = objUser;

                if (objUser.Preferences != null)
                {
                    if (objUser.Preferences.PersonalisedCookies)
                    {
                        //Set Cookie Preferences Cookie
                        CookieHelper.AddUpdateCookie(CookieKeyNames.ShowGallery, showGallery ? "Y" : "N");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Unable to set preferences cookie: {0}", ex.Message), null);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
            return "";
        }

        #endregion


        #region Direct Functions

        private void GetSitemapIndex()
        {
            SitemapGenerator generator = new SitemapGenerator();
            generator.GenerateIndex(Response.Output);
            Response.Output.Flush();
        }

        private void GetSitemap()
        {
            string sitemapName = WebUtil.GetQueryString("name");

            if (!String.IsNullOrEmpty(sitemapName))
            {
                Item sitemapRoot = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.RootPath + "/Sitemap Module/" + sitemapName);

                if (sitemapRoot != null)
                {
                    SitemapGenerator generator = new SitemapGenerator(sitemapRoot);
                    generator.Generate(Response.Output);
                    Response.Output.Flush();
                }
            }
        }

        private string GetClubUrlForId()
        {
            string clubId = WebUtil.GetQueryString("clubid");
            ShortID clubShortId;

            if (ShortID.TryParse(clubId, out clubShortId))
            {
                return new PageSummaryItem(Sitecore.Context.Database.GetItem(clubShortId.ToID())).Url;
            }
            else
            {
                return clubId;
            }           
        }


        private string GetClubResultsUrl()
        {

            if (!String.IsNullOrEmpty(ItemPaths.ClubResults))
            {
                return new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClubResults)).Url;
            }

            return "";
        }

        #endregion
    }
}