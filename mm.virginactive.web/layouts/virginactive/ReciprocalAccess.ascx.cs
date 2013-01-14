using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Web;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Globalization;
using Sitecore.Diagnostics;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class ReciprocalAccess : System.Web.UI.UserControl
    {
        protected ReciprocalAccessItem currentItem = new ReciprocalAccessItem(Sitecore.Context.Item);
        protected string clubName = "";
        protected string memberName = "";
        protected string membershipNumber = "''";
        protected string dateOfBirth = "''";
        protected string unlimitedAccessMarkup = "";
        protected string fourVisitsMarkup = "";
        protected string oneVisitMarkup = "";
        protected string guestFeeMarkup = "";
        protected string weekendsMarkup = "";

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
        
        //protected ClubItem membersHomeClub = null;

        //public ClubItem MembersHomeClub
        //{
        //    get { return membersHomeClub; }
        //    set
        //    {
        //        membersHomeClub = value;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

            //Set controls
            txtDateOfBirth.Attributes.Add("placeholder", Translate.Text("Date of birth") + " (dd/mm/yyyy)");
            txtMembershipNumber.Attributes.Add("placeholder", Translate.Text("Membership No."));

            string val = Translate.Text("Please enter {0}");
            rfvDateOfBirth.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a date of birth.")));
            revDateOfBirth.ErrorMessage = String.Format("<p>{0}</p>", Translate.Text("Whoops.  Your date of birth must be in the format dd/mm/yyyy. Please try again."));
            rfvMembershipNumber.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a membership number.")));
            revMembershipNumber.ErrorMessage = String.Format("<p>{0}</p>", Translate.Text("Whoops.  Your membership number is not valid. Please try again."));
            cvMemberNotFound.ErrorMessage = String.Format("<p>{0}</p>", Translate.Text("Sorry, that combination of membership number and date of birth doesn't match our records. Please enter your details carefully again and resubmit. <br /> If you are sure they are correct, please contact your club - we're sure they can help you out."));
            revDateOfBirth.ValidationExpression = Settings.DateRegularExpression;
            revMembershipNumber.ValidationExpression = Settings.MembershipNumberRegularExpression;

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
	            $(function(){
	                $.va_init.functions.setupForms();
                    ViewReciprocalAccessResults();
	            });
                </script>"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ClearResults();

            //    if (Page.IsValid == true)
            //    {
            //        //Authenticate member on membership id and date of birth
            //        mm.virginactive.webservices.virginactive.reciprocalaccess.RecipAccess vs = new mm.virginactive.webservices.virginactive.reciprocalaccess.RecipAccess();

            //        mm.virginactive.webservices.virginactive.reciprocalaccess.Member[] member = vs.authenticateMember(txtMembershipNumber.Value.Trim(), txtDateOfBirth.Value.Trim());

            //        if (member != null && member.Length > 0)
            //        {
            //            //member has been authenticated successfully
            //            MemberName = member[0].MemberFirstName;

            //            //Set link options.
            //            //Set url 
            //            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            //            urlOptions.AlwaysIncludeServerUrl = true;
            //            urlOptions.AddAspxExtension = true;

            //            //now get the lists of clubs

            //            //service returns 7 data tables

            //            //Dt1: Home Club
            //            //Dt2: Unlimited Access
            //            //Dt3: One Visit
            //            //Dt4: Guest Fee
            //            //Dt5: Four Visits
            //            //Dt6: Weekends
            //            //Dt7: No Access

            //            System.Data.DataSet clubsAccess = vs.getClubAccess(member[0].MembershipNumber, member[0].SourceID);

            //            bool ResultsFound = false;

            //            if (clubsAccess != null && clubsAccess.Tables.Count > 0)
            //            {
            //                //Set home club
            //                if (clubsAccess.Tables[0].Rows.Count > 0)
            //                {
            //                    ClubName = clubsAccess.Tables[0].Rows[0].ItemArray[0].ToString();
            //                }
            //                //Set Unlimited Access
            //                if (clubsAccess.Tables[1].Rows.Count > 0)
            //                {
            //                    System.Text.StringBuilder markupBuilder;
            //                    markupBuilder = new System.Text.StringBuilder();

            //                    foreach (System.Data.DataRow row in clubsAccess.Tables[1].Rows)
            //                    {
            //                        //Get club from club id
            //                        ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
            //                        if (club != null)
            //                        {
            //                            markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
            //                            UnrestrictedAccess.Visible = true;
            //                            ResultsFound = true;
            //                        }
            //                        unlimitedAccessMarkup = markupBuilder.ToString();
            //                    }
            //                }
            //                //One visit
            //                if (clubsAccess.Tables[2].Rows.Count > 0)
            //                {
            //                    System.Text.StringBuilder markupBuilder;
            //                    markupBuilder = new System.Text.StringBuilder();

            //                    foreach (System.Data.DataRow row in clubsAccess.Tables[2].Rows)
            //                    {
            //                        //Get club from club id
            //                        ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
            //                        if (club != null)
            //                        {
            //                            markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
            //                            OneVisitAccess.Visible = true;
            //                            ResultsFound = true;
            //                        }
            //                        oneVisitMarkup = markupBuilder.ToString();

            //                    }
            //                }
            //                //Guest Fee
            //                if (clubsAccess.Tables[3].Rows.Count > 0)
            //                {
            //                    System.Text.StringBuilder markupBuilder;
            //                    markupBuilder = new System.Text.StringBuilder();

            //                    foreach (System.Data.DataRow row in clubsAccess.Tables[3].Rows)
            //                    {
            //                        //Get club from club id
            //                        ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
            //                        if (club != null)
            //                        {
            //                            markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
            //                            GuestFeeAccess.Visible = true;
            //                            ResultsFound = true;
            //                        }
            //                        guestFeeMarkup = markupBuilder.ToString();
            //                    }
            //                }
            //                //Four Visits
            //                if (clubsAccess.Tables[4].Rows.Count > 0)
            //                {
            //                    System.Text.StringBuilder markupBuilder;
            //                    markupBuilder = new System.Text.StringBuilder();

            //                    foreach (System.Data.DataRow row in clubsAccess.Tables[4].Rows)
            //                    {
            //                        //Get club from club id
            //                        ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
            //                        if (club != null)
            //                        {
            //                            markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
            //                            FourVisitsAccess.Visible = true;
            //                            ResultsFound = true;
            //                        }
            //                        fourVisitsMarkup = markupBuilder.ToString();
            //                    }
            //                }
            //                //Weekends
            //                if (clubsAccess.Tables[5].Rows.Count > 0)
            //                {
            //                    System.Text.StringBuilder markupBuilder;
            //                    markupBuilder = new System.Text.StringBuilder();

            //                    foreach (System.Data.DataRow row in clubsAccess.Tables[5].Rows)
            //                    {
            //                        //Get club from club id
            //                        ClubItem club = SitecoreHelper.GetClubOnClubId(row.ItemArray[1].ToString());
            //                        if (club != null)
            //                        {
            //                            markupBuilder.Append(@"<li><a href=" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + ">" + club.Clubname.Rendered + "</a></li>");
            //                            WeekendsAccess.Visible = true;
            //                            ResultsFound = true;
            //                        }
            //                        weekendsMarkup = markupBuilder.ToString();
            //                    }
            //                }
            //            }

            //            if (ResultsFound == false)
            //            {
            //                NoAccessMessage.Visible = true;
            //            }

            //            formCompleted.Visible = true;
            //            //pnlForm.Update();
            //        }
            //        else
            //        {
            //            cvMemberNotFound.IsValid = false;
            //            NoAccessMessage.Visible = true;
            //        }


            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(String.Format("Error sending email confirmation {1}: {0}", ex.Message, "Reciprocal Access Service"), this);
            //    //return false;
            //}
        }

        private void ClearResults()
        {

            //// Clear result tables
            //UnrestrictedAccess.Visible = false;
            //FourVisitsAccess.Visible = false;
            //OneVisitAccess.Visible = false;
            //GuestFeeAccess.Visible = false;
            //WeekendsAccess.Visible = false;
            //NoAccessMessage.Visible = false;
        }
    }
}