using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Web;
using mm.virginactive.screportdal.Models;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Links;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileEnquiryThanks : System.Web.UI.UserControl
    {
        protected string ClubName = "";
        protected string CustomerName = "";
        protected string EmailAddress = "";
        protected string Telephone = "";
        protected string SubscribeMessage = "";
        protected string ClubHomeUrl = "";
        protected string HomeUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Form Submission data back from session object
            if (Session["sess_FormSubmission"] != null)
            {
                Feedback objFeedback = (Feedback)Session["sess_FormSubmission"];

                //Populate labels on page
                ClubName = SitecoreHelper.GetClubNameOnClubId(objFeedback.PrimaryClubID);
                CustomerName = objFeedback.Customer.Firstname + " " + objFeedback.Customer.Surname;
                EmailAddress = objFeedback.Customer.EmailAddress;
                Telephone = objFeedback.Customer.TelephoneNumber;
                SubscribeMessage = objFeedback.Customer.SubscribeToNewsletter ? Translate.Text("You have opted to receive our newsletter") : Translate.Text("You have opted NOT to receive our newsletter");

                //Set home and club home urls
                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                urlOptions.AlwaysIncludeServerUrl = true;
                urlOptions.AddAspxExtension = false;
                urlOptions.LanguageEmbedding = LanguageEmbedding.Never; 

                ClubItem club = SitecoreHelper.GetClubOnClubId(objFeedback.PrimaryClubID);
                ClubHomeUrl = Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions);

                string startPath = Sitecore.Context.Site.StartPath.ToString();
                var homeItem = Sitecore.Context.Database.GetItem(startPath);
                HomeUrl = LinkManager.GetItemUrl(homeItem, urlOptions);

            }

        }
    }
}