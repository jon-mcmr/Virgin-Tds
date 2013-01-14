using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubDetails : System.Web.UI.UserControl
    {
        protected ClubItem currentClub;
        protected Club current;
        private ManagerItem staffMember;
        protected string parking = "";
        protected string transport = "";
        protected string openingHours = "";
        protected string fullname = "";
        protected bool clubIsHomeClub = false;
        protected PageSummaryItem enqForm;
        protected PageSummaryItem contactForm;
        protected string bookATourLinkTitle = "";
        protected string membershipEnquiryLinkTitle = "";
        

        public ManagerItem StaffMember
        {
            get { return staffMember; }
            set { staffMember = value; }
        }
        public bool ClubIsHomeClub
        {
            get { return clubIsHomeClub; }
            set { clubIsHomeClub = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.EnquiryForm, "Enquiry form path missing");

            bookATourLinkTitle = HttpUtility.UrlEncode(Translate.Text("Book a Visit"));
            membershipEnquiryLinkTitle = HttpUtility.UrlEncode(Translate.Text("Membership Enquiry"));

            //Get Enq form details 
            enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));            

            //get the details of the club
            currentClub = SitecoreHelper.GetCurrentClub(Sitecore.Context.Item);
            current = new Club(currentClub.InnerItem);

            hdnClubId.Value = currentClub.ClubId.Rendered;

            Item contactF = currentClub.InnerItem.Axes.SelectSingleItem(String.Format("descendant::*[@@tid='{0}']", FeedbackItem.TemplateId));
            if (contactF != null)
            {
                contactForm = new PageSummaryItem(contactF);
            }
            //get users details

            User objUser = new User();
            //Set user session variable
            if (Session["sess_User"] != null)
            {
                objUser = (User)Session["sess_User"];
            }

            //Check if club has been set as home page
            if (!String.IsNullOrEmpty(objUser.HomeClubID))
            {
                if(objUser.HomeClubID == currentClub.ClubId.Rendered)
                {
                    //Display 'log out of club'
                    clubIsHomeClub = true;
                }
            }

            if (clubIsHomeClub == true)
            {
                SetHomeClubCookieLiteral.Text = Translate.Text("Log out of club");
                hdnClubIsHomeClub.Value = "true";
            }
            else
            {
                SetHomeClubCookieLiteral.Text = Translate.Text("Make this your home club");
                hdnClubIsHomeClub.Value = "false";
            }

            //Hide show 'Set club as home club' depending on cookies preferences
            if (objUser.Preferences != null)
            {
                SetHomeClubCookie.Visible = objUser.Preferences.PersonalisedCookies;
            }
            

            //write mark up for club details
            if (currentClub.Parkingoptions.Rendered.Trim() != "")
            {
                //add parking details
                System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
                markupBuilder.Append(@"<h5>Parking</h5>");
                markupBuilder.Append(@"<p>");
                markupBuilder.Append(currentClub.Parkingoptions.Rendered);
                markupBuilder.Append(@"</p>");

                parking = markupBuilder.ToString();
            }

            if ((currentClub.Nearesttrainstation.Rendered.Trim() != "")||(currentClub.Nearesttubestation.Rendered.Trim() != ""))
            {
                //add transport details
                System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
                markupBuilder.Append(@"<h5>Transport</h5>");
                if (currentClub.Nearesttrainstation.Rendered.Trim() != "")
                {
                    markupBuilder.Append(@"<p class=""icon-train"">");
                    markupBuilder.Append(currentClub.Nearesttrainstation.Rendered);
                    if (currentClub.Distancetotrainstation.Rendered.Trim() != "")
                    {
                        markupBuilder.Append(@" (" + currentClub.Distancetotrainstation.Rendered + " mins)");
                    }
                    markupBuilder.Append(@"</p>");
                }
                if (currentClub.Nearesttubestation.Rendered.Trim() != "")
                {
                    markupBuilder.Append(@"<p class=""icon-tube"">");
                    markupBuilder.Append(currentClub.Nearesttubestation.Rendered);
                    if (currentClub.Distancetotubestation.Rendered.Trim() != "")
                    {
                        markupBuilder.Append(@" (" + currentClub.Distancetotubestation.Rendered + " mins)");
                    }
                    markupBuilder.Append(@"</p>");
                }

                transport = markupBuilder.ToString();
            }

            if (currentClub.Openinghours.Rendered.Trim() != "")
            {
                //add opening hours details
                System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
                markupBuilder.Append(@"<h5>Opening Hours</h5>");
                markupBuilder.Append(@"<p>");
                markupBuilder.Append(currentClub.Openinghours.Rendered);
                markupBuilder.Append(@"</p>");

                openingHours = markupBuilder.ToString();
            }

            //get the details of the first manager listed for the club (i.e. the manager)
            List<ManagerItem> staffMembers = null;
            Item[] mgrs = currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId));
            if (mgrs != null)
            {
                staffMembers = mgrs.ToList().ConvertAll(x => new ManagerItem(x));

                staffMember = staffMembers.First();

                fullname = StaffMember.Person.Firstname.Rendered + " " + StaffMember.Person.Lastname.Rendered;
            }
            else 
            { 
                manager.Visible = false; 
            }

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		            $(function(){
	                    SetHomeClub();
                    });
                </script>"));
        }

    }
}