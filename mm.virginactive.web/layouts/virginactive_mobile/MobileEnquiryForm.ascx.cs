using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.common.Globalization;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Collections;
using Sitecore.Data.Items;
using Sitecore.Web;
using Sitecore.Collections;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileEnquiryForm : System.Web.UI.UserControl
    {
        protected ManagerItem manager;
        protected EnquiryFormItem enquiryItem = new EnquiryFormItem(Sitecore.Context.Item);
        protected ClubItem currentClub;
        protected string ClubSalesNumber = "";
        protected string ClubName = "";

        public ClubItem CurrentClub
        {
            get { return currentClub; }
            set
            {
                currentClub = value;
            }
        }

        public string ClubResponseTapCode { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //drpClubSelect.Attributes.Add("data-required", Translate.Text("Please choose a club"));
            Assert.ArgumentNotNullOrEmpty(ItemPaths.Clubs, "Club root path not set");

            if (!Page.IsPostBack)
            {
                BindDropDownLists();
            }
        }

        private void BindDropDownLists()
        {
            try
            {
                //If this form is being called froma  club area, we will get a ShortGuid of the 
                //originating club within the QS
                string clubQry = String.Empty;
                if (WebUtil.GetQueryString("c") != null)
                {
                    clubQry = WebUtil.GetQueryString("c");
                }

                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                ChildList clubLst = clubRoot.Children;
                Item[] clubs = clubLst.ToArray();

                //Populate the club selector
                List<ClubItem> clubList = clubs.ToList().ConvertAll(X => new ClubItem(X));
                clubList.RemoveAll(x => x.IsHiddenFromMenu());
                clubList.RemoveAll(x => x.IsPlaceholder.Checked && !x.ShowInClubSelect.Checked);
                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));

                foreach (Item club in clubList)
                {
                    ClubItem clubItem = new ClubItem(club);
                    if (clubItem != null)
                    {
                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToString()));

                            bool isSelected = clubQry.Equals(clubItem.ID.ToShortID().ToString());
                            if (isSelected)
                            {
                                clubFindSelect.SelectedValue = clubItem.ID.ToString();

                                //Set club details
                                ClubSalesNumber = clubItem.Salestelephonenumber.Raw;
                                clubDetails.Visible = true;
                                ClubResponseTapCode = clubItem.ResponseTapCode.Rendered;

                                ClubName =  HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Rendered);
                                //txtClubName.Value = ClubName;
                                //txtClubName.Visible = true;
                                clubFindSelect.Visible = false;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error binding drop down lists {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid == true)
                {
                    //check if we have a valid club selected
                    Boolean blnClubSuccessfullySelected = clubFindSelect.SelectedIndex != 0;

                    if (blnClubSuccessfullySelected == true)
                    {
                        currentClub = (ClubItem)Sitecore.Context.Database.GetItem(clubFindSelect.SelectedValue);

                        if (currentClub != null)
                        {
                            //get managers info                        
                            List<ManagerItem> staffMembers = null;
                            if (currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                            {
                                staffMembers = currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));

                                manager = staffMembers.First();
                            }

                            //Data is sent to client via service
                            SendEnquiryDataService();

                            //Save feedback to report DB
                            SaveDataToReportDB();

                            //Send confirmation email
                            SendConfirmationEmail();

                            Response.Redirect(enquiryItem.PageSummary.Url + "?action=confirm");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error processing club enquiry form {0}", ex.Message), this);              
            }
        }

        private Boolean SaveDataToReportDB()
        {
            bool blnDataSaved = false;

            try
            {
                string FirstName = "";
                string Surname = "";

                if (txtName.Value.IndexOf(" ") != -1)
                {
                    FirstName = txtName.Value.Substring(0, txtName.Value.IndexOf(" ")).Trim();
                    Surname = txtName.Value.Substring(txtName.Value.IndexOf(" ") + 1).Trim();
                }
                else
                {
                    FirstName = txtName.Value.Trim();
                }

                //Store Feedback Entitiy Details
                Feedback objFeedback = new Feedback();

                //Store Customer Entitiy Details
                Customer objCustomer = new Customer();
                objCustomer.EmailAddress = txtEmail.Value.Trim();
                objCustomer.Firstname = FirstName;
                objCustomer.Surname = Surname;
                objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                objCustomer.SubscribeToNewsletter = chkSubscribe.Checked;
                objCustomer.TelephoneNumber = txtPhone.Value.Trim();
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = "Club Enquiry";
                objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
                objFeedback.FeedbackTypeID = Constants.FeedbackType.ClubEnquiry;
                objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                objFeedback.SubmissionDate = DateTime.Now;

                //Add Comment
                Comment objComment = new Comment();

                objComment.CommentDetail = Settings.MobileWebLeadsSource;
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 1;
                objComment.Subject = "How did you find out about us";

                List<Comment> objComments = new List<Comment>();
                objComments.Add(objComment);
                objFeedback.Comments = objComments;

                //Save to session (required for confirmation page)
                Session["sess_FormSubmission"] = objFeedback;

                string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                DataAccessLayer dal = new DataAccessLayer(connection);
                if (dal.SaveFeedback(Context.User.Identity.Name, enquiryItem.DisplayName, objFeedback) > 0)
                {
                    blnDataSaved = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error saving form data to reports db {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }

            return blnDataSaved;
        }

        private Boolean SendEnquiryDataService()
        {
            bool blnEnquiryDataSent = false;


            try
            {
                mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();

                virginActiveService.InsertWebLead(
                    txtName.Value.Trim(),
                    currentClub.ClubId.Rendered,
                    txtPhone.Value.Trim(),
                    "", //postcode
                    txtEmail.Value.Trim(),
                    "", //preferred time
                    "", //memberFlag
                    Settings.MobileWebLeadsSourceID, //sourceId
                    Settings.MobileWebLeadsSource, //source
                    Settings.WebLeadsDefaultCampaignID, //campaignId
                    chkSubscribe.Checked ? true : false); //newsletter

                //var test = virginActiveService.ReadWebLead();

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending enquiry webservice data {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }

            return blnEnquiryDataSent;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.EnquiryConfirmation;
                string strEmailFromAddress = Settings.WebsiteMailFromText;
                string strEmailToAddress = txtEmail.Value.Trim();

                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                urlOptions.AlwaysIncludeServerUrl = true;
                urlOptions.AddAspxExtension = true;
                urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
                objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesLanding));
                objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealthArticles));
                objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.MembershipOptions));
                objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
                objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));
                objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));
                objTemplateVariables.Add("CustomerName", txtName.Value.Trim());
                if (manager != null)
                {
                    objTemplateVariables.Add("ClubManagerName", manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered);
                }
                else
                {
                    objTemplateVariables.Add("ClubManagerName", "");
                }
                objTemplateVariables.Add("ClubName", currentClub.Clubname.Rendered);
                objTemplateVariables.Add("ClubHomePageUrl", Sitecore.Links.LinkManager.GetItemUrl(currentClub, urlOptions));
                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                objTemplateVariables.Add("ImageRootUrl", WebUtil.GetServerUrl());
                objTemplateVariables.Add("ClubPhoneNumber", currentClub.Salestelephonenumber.Rendered);

                System.Text.StringBuilder markupBuilder;
                markupBuilder = new System.Text.StringBuilder();

                markupBuilder.Append(currentClub.Addressline1.Rendered);
                markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline2.Rendered) ? "<br />" + currentClub.Addressline2.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline3.Rendered) ? "<br />" + currentClub.Addressline3.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline4.Rendered) ? "<br />" + currentClub.Addressline4.Rendered : "");
                markupBuilder.Append("<br />" + currentClub.Postcode.Rendered);

                objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.EnquiryConfirmation), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.EnquiryConfirmation)).Emailhtml.Text);
                string strMessageBody = objParser.Parse();

                mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                string strAttachments = "";

                //now call messaging service
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strMessageBody, strAttachments);

                if (objSendResult.Success == true)
                {
                    blnEmailSent = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending confirmation email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }

            return blnEmailSent;
        }
    }
}