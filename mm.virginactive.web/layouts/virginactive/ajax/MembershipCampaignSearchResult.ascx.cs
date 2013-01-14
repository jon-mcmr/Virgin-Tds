using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.controls.Model;
using mm.virginactive.controls.Util;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class MembershipCampaignSearchResult : System.Web.UI.UserControl
    {
        private double? lat;
        private double? lng;
        protected List<Club> clubs;
        protected string targetUrl = "";

        public string TargetUrl
        {
            get { return targetUrl; }
            set { targetUrl = value; }
        }

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


        protected void Page_Load(object sender, EventArgs e)
        {
            clubs = ClubUtil.GetNearestClubs(Lat, Lng, 5);


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
                    }
                    else if (club.ClubItm.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric) //Set Esporta Flag
                    {
                        var EsportaFlag = e.Item.FindControl("EsportaFlag") as System.Web.UI.WebControls.Literal;
                        if (EsportaFlag != null)
                        {
                            EsportaFlag.Text = String.Format(@"<span>{0}</span> ESPORTA", Translate.Text("Previously"));
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
                }
            }
        }


        public static string RenderToString(double? lat, double? lng, string target)
        {
            return SitecoreHelper.RenderUserControl<MembershipCampaignSearchResult>("~/layouts/virginactive/ajax/MembershipCampaignSearchResult.ascx",
            uc =>
            {
                uc.Lat = lat;
                uc.Lng = lng;
                uc.TargetUrl = target;
            });
        }
    }
}