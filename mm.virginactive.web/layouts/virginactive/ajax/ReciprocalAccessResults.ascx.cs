using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Links;
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
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Globalization;
using Sitecore.Diagnostics;
using System.Text.RegularExpressions;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class ReciprocalAccessResults : System.Web.UI.UserControl
    {
        protected ReciprocalAccessItem currentItem = new ReciprocalAccessItem(Sitecore.Context.Item);
        protected string clubName = "";
        protected string memberName = "";
        protected string unlimitedAccessMarkup = "";
        protected string fourVisitsMarkup = "";
        protected string oneVisitMarkup = "";
        protected string guestFeeMarkup = "";
        protected string weekendsMarkup = "";
        protected string membershipNumber = "";
        private string dateOfBirth = "";
        protected string errorMessage = "''";

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public string MembershipNumber
        {
            get { return membershipNumber; }
            set { membershipNumber = value; }
        }

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string ClubName
        {
            get { return clubName; }
            set
            {
                clubName = value;
            }
        }
        public string MemberName
        {
            get { return memberName; }
            set
            {
                memberName = value;
            }
        }

        private void ClearResults()
        {

            // Clear reult tables
            UnrestrictedAccess.Visible = false;
            FourVisitsAccess.Visible = false;
            OneVisitAccess.Visible = false;
            GuestFeeAccess.Visible = false;
            WeekendsAccess.Visible = false;
            NoAccessMessage.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Do validation
            bool isValid = true;
            string val = Translate.Text("Please enter {0}");

            dateOfBirth = dateOfBirth.Replace("-", "/");

            if (isValid == true)
            {
                try
                {

                    ClearResults();
                    //Authenticate member on membership id and date of birth
                    mm.virginactive.webservices.virginactive.reciprocalaccess.RecipAccess vs = new mm.virginactive.webservices.virginactive.reciprocalaccess.RecipAccess();
                    mm.virginactive.webservices.virginactive.reciprocalaccess.Member[] member = vs.authenticateMember(membershipNumber, dateOfBirth);

                    if (member != null && member.Length == 1)
                    //if (member != null && member[0] != null)
                    {
                        //member has been authenticated successfully
                        MemberName = member[0].MemberFirstName;

                        //Set link options.
                        //Set url 
                        Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                        urlOptions.AlwaysIncludeServerUrl = true;
                        urlOptions.AddAspxExtension = true;
                        urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                        //now get the lists of clubs

                        //service returns 7 data tables

                        //Dt1: Home Club
                        //Dt2: Unlimited Access
                        //Dt3: One Visit
                        //Dt4: Guest Fee
                        //Dt5: Four Visits
                        //Dt6: Weekends
                        //Dt7: No Access

                        System.Data.DataSet clubsAccess = vs.getClubAccess(member[0].MembershipNumber, member[0].SourceID);

                        bool ResultsFound = false;

                        if (clubsAccess != null && clubsAccess.Tables.Count > 0)
                        {
                            //Set home club
                            if (clubsAccess.Tables[0].Rows.Count > 0)
                            {
                                ClubName = clubsAccess.Tables[0].Rows[0].ItemArray[0].ToString();
                            }
                            //Set Unlimited Access
                            if (clubsAccess.Tables[1].Rows.Count > 0)
                            {
                                System.Text.StringBuilder markupBuilder;
                                markupBuilder = new System.Text.StringBuilder();

                                foreach (System.Data.DataRow row in clubsAccess.Tables[1].Rows)
                                {
                                    //Get club from club id
                                    ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
                                    if (club != null)
                                    {
                                        markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
                                        UnrestrictedAccess.Visible = true;
                                        ResultsFound = true;
                                    }
                                    unlimitedAccessMarkup = markupBuilder.ToString();
                                }
                            }
                            //One visit
                            if (clubsAccess.Tables[2].Rows.Count > 0)
                            {
                                System.Text.StringBuilder markupBuilder;
                                markupBuilder = new System.Text.StringBuilder();

                                foreach (System.Data.DataRow row in clubsAccess.Tables[2].Rows)
                                {
                                    //Get club from club id
                                    ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
                                    if (club != null)
                                    {
                                        markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
                                        OneVisitAccess.Visible = true;
                                        ResultsFound = true;
                                    }
                                    oneVisitMarkup = markupBuilder.ToString();

                                }
                            }
                            //Guest Fee
                            if (clubsAccess.Tables[3].Rows.Count > 0)
                            {
                                System.Text.StringBuilder markupBuilder;
                                markupBuilder = new System.Text.StringBuilder();

                                foreach (System.Data.DataRow row in clubsAccess.Tables[3].Rows)
                                {
                                    //Get club from club id
                                    ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
                                    if (club != null)
                                    {
                                        markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
                                        GuestFeeAccess.Visible = true;
                                        ResultsFound = true;
                                    }
                                    guestFeeMarkup = markupBuilder.ToString();
                                }
                            }
                            //Four Visits
                            if (clubsAccess.Tables[4].Rows.Count > 0)
                            {
                                System.Text.StringBuilder markupBuilder;
                                markupBuilder = new System.Text.StringBuilder();

                                foreach (System.Data.DataRow row in clubsAccess.Tables[4].Rows)
                                {
                                    //Get club from club id
                                    ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
                                    if (club != null)
                                    {
                                        markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
                                        FourVisitsAccess.Visible = true;
                                        ResultsFound = true;
                                    }
                                    fourVisitsMarkup = markupBuilder.ToString();
                                }
                            }
                            //Weekends
                            if (clubsAccess.Tables[5].Rows.Count > 0)
                            {
                                System.Text.StringBuilder markupBuilder;
                                markupBuilder = new System.Text.StringBuilder();

                                foreach (System.Data.DataRow row in clubsAccess.Tables[5].Rows)
                                {
                                    //Get club from club id
                                    ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
                                    if (club != null)
                                    {
                                        markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
                                        WeekendsAccess.Visible = true;
                                        ResultsFound = true;
                                    }
                                    weekendsMarkup = markupBuilder.ToString();
                                }
                            }
                        }

                        if (ResultsFound == false)
                        {
                            NoAccessMessage.Visible = true;
                        }

                        formCompleted.Visible = true;
                    }
                    else
                    {
                        isValid = false;
                        errorMessage = String.Format("<p>{0}</p>", Translate.Text("Ah, sorry, bit of a glitch identifying your membership package there. We're sure it's not a problem - just talk to the team at your club who will help you out in double-quick time."));
                    }
                }
                catch (Exception ex)
                {
                        Log.Error(String.Format("Error retrieving reciprocal access data from web service: {0}", ex.Message), this);
                        errorMessage = String.Format("<p>{0}</p>", Translate.Text("Ah, sorry, bit of a glitch identifying your membership package there. We're sure it's not a problem - just talk to the team at your club who will help you out in double-quick time."));
                        mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                }
            }
            phErrorMessage.Value = errorMessage;
        }

        public static string RenderToString(string membershipNumber, string dateOfBirth)
        {
            return SitecoreHelper.RenderUserControl<ReciprocalAccessResults>("~/layouts/virginactive/ajax/ReciprocalAccessResults.ascx",
                uc =>
                {
                    uc.MembershipNumber = membershipNumber;
                    uc.DateOfBirth = dateOfBirth;
                });
        }
    }
}