using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.controls.Model;
using mm.virginactive.controls.Util;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using Sitecore.Web;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileClubResults : System.Web.UI.UserControl
    {
        private double lat;
        private double lng;
        protected List<Club> clubs;
        protected string matchingResults = "10 clubs matching your search criteria";

        public double Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        public double Lng
        {
            get { return lng; }
            set { lng = value; }
        }

        public string MatchingResults
        {
            get { return matchingResults; }
            set { matchingResults = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int NoOfResultsToShow = 10;

            //get pointer
            LocationPointerItem pointer = null;
            if (Sitecore.Context.Item.TemplateID.ToString() == LocationPointerItem.TemplateId)
            {
                pointer = new LocationPointerItem(Sitecore.Context.Item);
            }

            //string location = WebUtil.GetQueryString("searchLoc", pointer != null ? pointer.LocationName.Raw : "");

            string sLat = WebUtil.GetQueryString("lat", pointer != null ? pointer.Lat.Raw : "0.00");
            Double.TryParse(sLat, out lat);
            string sLng = WebUtil.GetQueryString("lng", pointer != null ? pointer.Long.Raw : "0.00");
            Double.TryParse(sLng, out lng);

            clubs = ClubUtil.GetNearestClubs(Lat, Lng, NoOfResultsToShow);

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

                    //Get address
                    var Address = e.Item.FindControl("ltrAddress") as System.Web.UI.WebControls.Literal;
                    if (Address != null)
                    {
                        System.Text.StringBuilder markupBuilder;
                        markupBuilder = new System.Text.StringBuilder();

                        markupBuilder.Append(club.ClubItm.Addressline1.Text);
                        markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline2.Text) ? "<br />" + club.ClubItm.Addressline2.Text : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline3.Text) ? "<br />" + club.ClubItm.Addressline3.Text : "");
                        markupBuilder.Append("<br />");
                        markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline4.Text) ? club.ClubItm.Addressline4.Text + " " : "");
                        markupBuilder.Append(club.ClubItm.Postcode.Text);

                        Address.Text = markupBuilder.ToString();
                    }

                    //Get club Urls
                    var ltrNearestHeader = e.Item.FindControl("ltrNearestHeader") as System.Web.UI.WebControls.Literal;
                    //var ltrClubTitleLink = e.Item.FindControl("ltrClubTitleLink") as System.Web.UI.WebControls.Literal;
                    string ClubLinkUrl = new PageSummaryItem(club.ClubItm.InnerItem).Url;

                    string clubLat = club.ClubItm.Lat.Raw;
                    string clubLng = club.ClubItm.Long.Raw;

                    var ltrDistanceLink = e.Item.FindControl("ltrDistanceLink") as System.Web.UI.WebControls.Literal;

                    System.Text.StringBuilder markupBuilder2;
                    markupBuilder2 = new System.Text.StringBuilder();
                    markupBuilder2.Append(@"<a href=""https://maps.google.co.uk/?q=");
                    markupBuilder2.Append(clubLat);
                    markupBuilder2.Append("," + clubLng);
                    markupBuilder2.Append(@""">"+ club.DistanceFromSource.ToString() + " ");
                    markupBuilder2.Append(Translate.Text("miles") + "</a>");

                    ltrDistanceLink.Text = markupBuilder2.ToString();

                    //ltrClubTitleLink.Text = @"<a href=""" + ClubLinkUrl + @""">" + club.ClubItm.Clubname.Text + @"</a>";
                    ltrNearestHeader.Text = club.IsNearest ? "<h2>" + Translate.Text("Nearest club") + "</h2>" : "";

                }
            }
        }
    }
}