using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using System.Collections;
using mm.virginactive.screportdal.Models;
using mm.virginactive.controls.Model;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using Sitecore.Collections;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;


namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{

    public partial class ReferAFriendII : System.Web.UI.UserControl
    {
        protected ManagerItem primaryManager;
        protected ManagerItem secondaryManager;
        protected ClubItem primaryClub;
        protected ClubItem secondaryClub;
        protected string privacyPolicyUrl;
        protected string homePageUrl;
        protected string termsConditionsUrl;

        protected BespokeCampaignItem campaign = new BespokeCampaignItem(Sitecore.Context.Item);

        public ClubItem PrimaryClub
        {
            get { return primaryClub; }
            set
            {
                primaryClub = value;
            }
        }

        public ClubItem SecondaryClub
        {
            get { return secondaryClub; }
            set
            {
                secondaryClub = value;
            }
        }

        public string PrivacyPolicyUrl
        {
            get { return privacyPolicyUrl; }
            set
            {
                privacyPolicyUrl = value;
            }
        }

        public string TermsConditionsUrl
        {
            get { return termsConditionsUrl; }
            set
            {
                termsConditionsUrl = value;
            }
        }

        public string HomePageUrl
        {
            get { return homePageUrl; }
            set
            {
                homePageUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkReferAnotherFriend.Text = campaign.Whatsnextbuttontext.Rendered;
            if (!Page.IsPostBack)
            {
                List<ClubItem> clubs = null;

                if (campaign.CampaignBase.Usecustomclublist.Checked)
                {
                    //Use a custom list of clubs
                    clubs = campaign.CampaignBase.Customclublist.ListItems.ConvertAll(X => new ClubItem(X));
                }
                else
                {
                    //Use a custom list of clubs
                    Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                    ChildList clubLst = clubRoot.Children;
                    clubs = clubLst.ToList().ConvertAll(X => new ClubItem(X));
                    clubs.RemoveAll(x => x.IsHiddenFromMenu());
                }
                if (clubs != null)
                {
                    clubFindSelect1.Items.Add(new ListItem(Translate.Text("Select a club"), "default"));
                    clubFindSelect2.Items.Add(new ListItem(Translate.Text("Select a club"), "default"));

                    foreach (ClubItem clubItem in clubs)
                    {
                        if (clubItem != null)
                        {
                            if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                            {
                                string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                                //clubFindSelect1.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                                clubFindSelect1.Items.Add(new ListItem(clubLabel, clubItem.ID.ToString()));
                                //clubFindSelect2.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                                clubFindSelect2.Items.Add(new ListItem(clubLabel, clubItem.ID.ToString()));
                            }
                        }
                    }

                }
            }

            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = true;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath.ToString());
            homePageUrl = Sitecore.Links.LinkManager.GetItemUrl(homeItem, urlOptions);

            privacyPolicyUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy);
            termsConditionsUrl = new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl;

            //add the item specific css stylesheet
            //string classNames = article.Attributes["class"];
            //article.Attributes.Add("class", classNames.Length > 0 ? classNames + " campaign-" + campaign.CampaignBase.Cssstyle.Raw : "campaign-" + campaign.CampaignBase.Cssstyle.Raw);

            //Add dynamic content to header
            HtmlHead head = (HtmlHead)Page.Header;

            //Add Open Tag
            if (Session["sess_User"] != null)
            {
                User objUser = (User)Session["sess_User"];
                if (objUser.Preferences != null)
                {
                    if (objUser.Preferences.MarketingCookies && objUser.Preferences.MetricsCookies)
                    {
                        head.Controls.Add(new LiteralControl(OpenTagHelper.OpenTagVirginActiveUK));
                    }
                }
            }

 	
            //Add Page Title
	  	
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
            markupBuilder.Append(@"<meta name='viewport' content='width=1020'>");	  	
            markupBuilder.Append(@"<link rel='apple-touch-icon' href='/virginactive/images/apple-touch-icon.png'>");
            markupBuilder.Append(@"<link rel='shortcut icon' href='/virginactive/images/favicon.ico'>");
            markupBuilder.Append(@"<link href='/virginactive/css/fonts.css' rel='stylesheet'>");
            markupBuilder.Append(@"<link href='/va_campaigns/Bespoke/Refer-A-Friend/css/styles.css' rel='stylesheet'>");  	
            head.Controls.Add(new LiteralControl(markupBuilder.ToString()));

            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"<script src='/va_campaigns/Bespoke/Refer-A-Friend/js/plugins.js'></script>
                    <script src='/va_campaigns/Bespoke/Refer-A-Friend/js/scripts.js'></script>
                    "));
            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (clubFindSelect1.SelectedIndex != 0 && clubFindSelect2.SelectedIndex != 0)
                {
                    primaryClub = (ClubItem)Sitecore.Context.Database.GetItem(clubFindSelect1.SelectedValue);
                    secondaryClub = (ClubItem)Sitecore.Context.Database.GetItem(clubFindSelect2.SelectedValue);				

                    if (primaryClub != null && secondaryClub != null)
                    {
                        //Store Feedback Entitiy Details
                        Feedback objFeedback = new Feedback();

                        //Store Customer (Referer's) Entitiy Details
                        Customer objCustomer = new Customer();

                        objCustomer.EmailAddress = email1.Value;
                        objCustomer.Firstname = firstName1.Value;
                        objCustomer.Surname = surname1.Value;
                        objCustomer.HomeClubID = primaryClub.ClubId.Rendered;
                        objCustomer.SubscribeToNewsletter = subscribe.Checked;
                        objCustomer.TelephoneNumber = number1.Value.Trim();
                        objCustomer.MembershipNumber = memberNo.Value.Trim();

                        objFeedback.Customer = objCustomer;
                        objFeedback.FeedbackSubject = campaign.CampaignBase.Campaigntype.Raw;
                        objFeedback.FeedbackSubjectDetail = campaign.CampaignBase.Campaignname.Rendered;
                        objFeedback.FeedbackTypeID = Convert.ToInt32(campaign.CampaignBase.CampaignId.Rendered);
                        objFeedback.PrimaryClubID = primaryClub.ClubId.Rendered;
                        objFeedback.SubmissionDate = DateTime.Now;

                        //Store Referee's Details
                        List<Comment> objComments = new List<Comment>();
                        //Add Comment
                        Comment objComment = new Comment();
                        objComment.CommentDetail = secondaryClub.ClubId.Rendered;
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 1;
                        objComment.Subject = "Friend's Preferred Club";

                        objComments.Add(objComment);

                        objComment = new Comment();
                        objComment.CommentDetail = firstName2.Value.Trim();
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 2;
                        objComment.Subject = "Friend's First Name";

                        objComments.Add(objComment);

                        objComment = new Comment();
                        objComment.CommentDetail = surname2.Value.Trim();
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 3;
                        objComment.Subject = "Friend's Surname";

                        objComments.Add(objComment);

                        objComment = new Comment();
                        objComment.CommentDetail = email2.Value.Trim();
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 4;
                        objComment.Subject = "Friend's Email";

                        objComments.Add(objComment);

                        objComment = new Comment();
                        objComment.CommentDetail = number2.Value.Trim();
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 5;
                        objComment.Subject = "Friend's Number";

                        objComments.Add(objComment);

                        objComment = new Comment();
                        objComment.CommentDetail = permission.Checked == true? "Y" : "N" ;
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 6;
                        objComment.Subject = "Permission to be contacted";

                        objComments.Add(objComment);

                        objFeedback.Comments = objComments;

                        string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                        DataAccessLayer dal = new DataAccessLayer(connection);
                        int successFlag = dal.SaveFeedback(Context.User.Identity.Name, campaign.DisplayName, objFeedback);

                        if (successFlag > 0)
                        {

                            //Data is sent to client via email
                            SendAdminEmail();

                            if (campaign.CampaignBase.Isweblead.Checked == true)
                            {
                                //Data is sent to client via service
                                SendEnquiryDataService();
                            }

                            //get managers info                        
                            List<ManagerItem> staffMembers = null;
                            if (primaryClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                            {
                                staffMembers = primaryClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));
                                primaryManager = staffMembers.First();
                            }                   
                            staffMembers = null;
                            if (secondaryClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                            {
                                staffMembers = secondaryClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));
                                secondaryManager = staffMembers.First();
                            }

                            if (campaign.CampaignBase.Sendconfirmationemail.Checked == true)
                            {
                                //Data is sent to referer via email
                                SendPrimaryConfirmationEmail();

                                if (campaign.CampaignBase.Secondaryemailtemplate.Item != null)
                                {
                                    //Data is sent to referee via email
                                    SendSecondaryConfirmationEmail();
                                }
                            }

                            pnlForm.Attributes.Add("style", "display:none;");
                            pnlThanks.Attributes.Add("style", "display:block;");

                            string classNames = wrapper.Attributes["class"];
                            wrapper.Attributes.Add("class", "wrapper-thanks");
                        }
                        else
                        {

                            pnlForm.Attributes.Add("style", "display:block;");
                            pnlThanks.Attributes.Add("style", "display:none;");

                            Log.Error(String.Format("Error storing campaign details"), this);
                        }
                    }
                    else
                    {

                        pnlForm.Attributes.Add("style", "display:block;");
                        pnlThanks.Attributes.Add("style", "display:none;");

                        //string classNames = article.Attributes["class"];
                        //article.Attributes.Add("class", classNames.Length > 0 ? classNames + " ImageOverlay" : "ImageOverlay");
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error storing campaign details: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

        }


        private void ClearFormFields()
        {
            //Clear Referee's details
            clubFindSelect2.SelectedIndex = 0;
            firstName2.Value = string.Empty;
            surname2.Value = string.Empty;
            email2.Value = string.Empty;
            number2.Value = string.Empty;
            subscribe.Checked = false;
            permission.Checked = false;
            terms.Checked = false;
        }

        private Boolean SendEnquiryDataService()
        {
            bool blnEnquiryDataSent = false;

            try
            {
                //Enter Referee's details
                mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();

                virginActiveService.InsertWebLeadAndReferral(
                    firstName2.Value.Trim() + " " + surname2.Value.Trim(),
                    secondaryClub.ClubId.Rendered,
                    number2.Value.Trim(),
                    "", //postcode
                    email2.Value.Trim(),
                    "", //preferred time
                    "", //memberFlag
                    Settings.WebLeadsSourceID, //sourceId
                    Settings.CampaignWebLeadsSource, //source
                    //campaign.CampaignBase.Campaignname.Rendered != "" ? campaign.CampaignBase.Campaignname.Rendered : "Unspecified Website Campaign", //source (campaignname)
                    campaign.CampaignBase.Campaigncode.Rendered != "" ? campaign.CampaignBase.Campaigncode.Rendered : "Virgin Active Website Campaign", //campaignId (campaigncode)
                    false,
                    memberNo.Value.Trim(),//referral membeship no.
                    primaryClub.ClubId.Rendered,//referral club code
                    firstName1.Value.Trim(),//referral first name
                    surname1.Value.Trim(),//referral surname
                    email1.Value.Trim(),//referral email
                    subscribe.Checked);//referral newsletter

                //var test = virginActiveService.ReadWebLead();

                if (secondaryClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", secondaryClub.Clubname.Raw), this);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending enquiry webservice data {1}: {0}", ex.Message, secondaryClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEnquiryDataSent;
        }

        private Boolean SendAdminEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.FeedbackCampaignFormAdmin;
                string strEmailFromAddress = Settings.WebsiteMailFromText;

                string strEmailToAddress = "";
                if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                {
                    //Use test
                    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                }
                else
                {
                    strEmailToAddress = Settings.FeedbackCampaignEmailToListShort;
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("CustomerName", firstName2.Value + " " + surname2.Value);
                objTemplateVariables.Add("CustomerEmail", email2.Value);
                objTemplateVariables.Add("HomeClubID", secondaryClub.ClubId.Rendered);
                objTemplateVariables.Add("Telephone", number2.Value);
                objTemplateVariables.Add("Comments", "N/A");
                objTemplateVariables.Add("SubscribeToNewsletter", "N");
                objTemplateVariables.Add("PublishDetails", "N/A");
                objTemplateVariables.Add("FeedbackSubject", campaign.CampaignBase.Campaigntype.Raw);
                objTemplateVariables.Add("FeedbackSubjectDetail", campaign.CampaignBase.Campaignname.Rendered);
                objTemplateVariables.Add("SubmissionDate", DateTime.Now.ToShortDateString());
                objTemplateVariables.Add("ImageSubmitted", "N/A");

                objTemplateVariables.Add("ReferrerName", firstName1.Value + " " + surname1.Value);
                objTemplateVariables.Add("ReferrerEmail", email1.Value);
                objTemplateVariables.Add("ReferrerClubID", primaryClub.ClubId.Rendered);
                objTemplateVariables.Add("ReferrerMembershipNo", memberNo.Value);
                objTemplateVariables.Add("ReferrerTelephone", number1.Value);
                objTemplateVariables.Add("ReferrerSubscribeToNewsletter", subscribe.Checked == true ? "Y" : "N");

                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(campaign.CampaignBase.GetAdminTemplateHtml());

                string strMessageBody = objParser.Parse();

                //Parser objParser = new Parser(objTemplateVariables);
                //objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.FeedbackCampaignFormAdmin)).Emailhtml.Text);
                //string strMessageBody = objParser.Parse();

                mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                string strAttachments = "";

                //now call messaging service
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", "", strEmailSubject, true, strMessageBody, strAttachments);


                if (objSendResult.Success == true)
                {
                    blnEmailSent = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending club feedback data email: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }

        private Boolean SendPrimaryConfirmationEmail()
        {
            //Sent to Referrer
            bool blnEmailSent = false;

            try
            {
                if (campaign.CampaignBase.Emailtemplate.Item != null)
                {
                    //Get Campaign Email object
                    EmailBaseItem emailItem = campaign.CampaignBase.Emailtemplate.Item;

                    //string strEmailSubject = campaign.Campaign.CampaignBase.Emailsubject.Rendered;
                    string strEmailSubject = emailItem.Subject.Raw;
                    //string strEmailFromAddress = Settings.WebsiteMailFromText;
                    string strEmailFromAddress = emailItem.Fromaddress.Raw;

                    string strEmailToAddress = email1.Value.Trim();

                    //Populate email text variables
                    Hashtable objTemplateVariables = new Hashtable();
                    objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
                    objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));
                    objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesAndClasses));
                    objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealth));
                    objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.Memberships));
                    objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
                    objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));

                    if (campaign.CampaignBase.Termsandconditionslink != null)
                    {
                        objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                    }

                    objTemplateVariables.Add("CustomerName", firstName1.Value.Trim() + " " + surname1.Value.Trim());
                    objTemplateVariables.Add("FriendsName", firstName2.Value.Trim() + " " + surname2.Value.Trim());
                    objTemplateVariables.Add("FriendsFirstName", firstName1.Value.Trim());
                    objTemplateVariables.Add("ClubManagerName", primaryManager != null ? primaryManager.Person.Firstname.Rendered + " " + primaryManager.Person.Lastname.Rendered : "");

                    objTemplateVariables.Add("ClubName", primaryClub != null ? primaryClub.Clubname.Rendered : "");
                    objTemplateVariables.Add("EmailSubject", strEmailSubject);
                    objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                    objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                    objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                    objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                    objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                    objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                    objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                    objTemplateVariables.Add("ClubPhoneNumber", primaryClub != null ? primaryClub.Salestelephonenumber.Rendered : "");

                    System.Text.StringBuilder markupBuilder;
                    markupBuilder = new System.Text.StringBuilder();

                    if (primaryClub != null)
                    {
                        markupBuilder.Append(primaryClub.Addressline1.Rendered);
                        markupBuilder.Append(!String.IsNullOrEmpty(primaryClub.Addressline2.Rendered) ? "<br />" + primaryClub.Addressline2.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(primaryClub.Addressline3.Rendered) ? "<br />" + primaryClub.Addressline3.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(primaryClub.Addressline4.Rendered) ? "<br />" + primaryClub.Addressline4.Rendered : "");
                        markupBuilder.Append("<br />" + primaryClub.Postcode.Rendered);

                        objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());
                    }
                    else
                    {
                        objTemplateVariables.Add("ClubAddress", "");
                    }

                    //Parser objParser = new Parser(campaign.Campaign.CampaignBase.GetTemplateHtml(), objTemplateVariables);
                    Parser objParser = new Parser(objTemplateVariables);
                    if (emailItem.InnerItem.TemplateName == Templates.CampaignEmail)
                    {
                        CampaignEmailItem campaignEmail = (CampaignEmailItem)emailItem.InnerItem;
                        //First parse the Email Body text
                        objParser.SetTemplate(campaignEmail.Emailbody.Raw);
                        string strEmailBody = objParser.Parse();

                        objTemplateVariables.Add("EmailBody", strEmailBody);

                        //Second parse the full html -substituting in title and preheader
                        objTemplateVariables.Add("EmailTitle", campaignEmail.Emailtitle.Raw);
                        objTemplateVariables.Add("EmailPreheader", campaignEmail.Emailpreheader.Raw);

                        objParser = new Parser(objTemplateVariables);
                        if (primaryClub.InnerItem.TemplateName == Templates.ClassicClub)
                        {
                            objParser.SetTemplate(campaignEmail.Classichtml.Raw);
                        }
                        else
                        {
                            objParser.SetTemplate(campaignEmail.Standardhtml.Raw);
                        }
                    }
                    else
                    {
                        EmailItem standardEmail = (EmailItem)emailItem.InnerItem;
                        //Standard email
                        objParser.SetTemplate(standardEmail.Emailhtml.Raw);
                    }
                    //Parser objParser = new Parser(Server.MapPath(EmailTemplates.FreeGuestPassConfirmation), objTemplateVariables);
                    string strEmailHtml = objParser.Parse();

                    mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                    mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                    string strAttachments = "";

                    //now call messaging service
                    objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strEmailHtml, strAttachments);

                    if (objSendResult.Success == true)
                    {
                        blnEmailSent = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending campaign email confirmation {1}: {0}", ex.Message, campaign.CampaignBase.CampaignId.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }

        private Boolean SendSecondaryConfirmationEmail()
        {
            //Sent to Referree
            bool blnEmailSent = false;

            try
            {
                if (campaign.CampaignBase.Secondaryemailtemplate.Item != null)
                {
                    //Get Campaign Email object
                    EmailBaseItem emailItem = campaign.CampaignBase.Secondaryemailtemplate.Item;

                    //string strEmailSubject = campaign.Campaign.CampaignBase.Emailsubject.Rendered;
                    string strEmailSubject = emailItem.Subject.Raw;
                    //string strEmailFromAddress = Settings.WebsiteMailFromText;
                    string strEmailFromAddress = emailItem.Fromaddress.Raw;
                    string strEmailToAddress = email2.Value.Trim();

                    //Populate email text variables
                    Hashtable objTemplateVariables = new Hashtable();
                    objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
                    objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));
                    objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesAndClasses));
                    objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealth));
                    objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.Memberships));
                    objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
                    objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));

                    objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                    objTemplateVariables.Add("TriathlonLinkUrl", new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                    objTemplateVariables.Add("TriathlonTrainingLinkUrl", new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);

                    objTemplateVariables.Add("CustomerName", firstName2.Value.Trim() + " " + surname2.Value.Trim());
                    objTemplateVariables.Add("FriendsName", firstName1.Value.Trim() + " " + surname1.Value.Trim());
                    objTemplateVariables.Add("FriendsFirstName", firstName1.Value.Trim());
                    objTemplateVariables.Add("ClubManagerName", secondaryManager != null ? secondaryManager.Person.Firstname.Rendered + " " + secondaryManager.Person.Lastname.Rendered : "");

                    objTemplateVariables.Add("ClubName", secondaryClub != null ? secondaryClub.Clubname.Rendered : "");
                    objTemplateVariables.Add("EmailSubject", strEmailSubject);
                    objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                    objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                    objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                    objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                    objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                    objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                    objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                    objTemplateVariables.Add("ClubPhoneNumber", secondaryClub != null ? secondaryClub.Salestelephonenumber.Rendered : "");

                    System.Text.StringBuilder markupBuilder;
                    markupBuilder = new System.Text.StringBuilder();

                    if (secondaryClub != null)
                    {
                        markupBuilder.Append(secondaryClub.Addressline1.Rendered);
                        markupBuilder.Append(!String.IsNullOrEmpty(secondaryClub.Addressline2.Rendered) ? "<br />" + secondaryClub.Addressline2.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(secondaryClub.Addressline3.Rendered) ? "<br />" + secondaryClub.Addressline3.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(secondaryClub.Addressline4.Rendered) ? "<br />" + secondaryClub.Addressline4.Rendered : "");
                        markupBuilder.Append("<br />" + secondaryClub.Postcode.Rendered);

                        objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());
                    }
                    else
                    {
                        objTemplateVariables.Add("ClubAddress", "");
                    }

                    Parser objParser = new Parser(objTemplateVariables);
                    if (emailItem.InnerItem.TemplateName == Templates.CampaignEmail)
                    {
                        CampaignEmailItem campaignEmail = (CampaignEmailItem)emailItem.InnerItem;
                        //First parse the Email Body text
                        objParser.SetTemplate(campaignEmail.Emailbody.Raw);
                        string strEmailBody = objParser.Parse();

                        objTemplateVariables.Add("EmailBody", strEmailBody);

                        //Second parse the full html -substituting in title and preheader
                        objTemplateVariables.Add("EmailTitle", campaignEmail.Emailtitle.Raw);
                        objTemplateVariables.Add("EmailPreheader", campaignEmail.Emailpreheader.Raw);

                        objParser = new Parser(objTemplateVariables);
                        if (secondaryClub.InnerItem.TemplateName == Templates.ClassicClub)
                        {
                            objParser.SetTemplate(campaignEmail.Classichtml.Raw);
                        }
                        else
                        {
                            objParser.SetTemplate(campaignEmail.Standardhtml.Raw);
                        }
                    }
                    else
                    {
                        EmailItem standardEmail = (EmailItem)emailItem.InnerItem;
                        //Standard email
                        objParser.SetTemplate(standardEmail.Emailhtml.Raw);
                    }
                    //Parser objParser = new Parser(Server.MapPath(EmailTemplates.FreeGuestPassConfirmation), objTemplateVariables);
                    string strEmailHtml = objParser.Parse();

                    mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                    mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                    string strAttachments = "";

                    //now call messaging service
                    objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strEmailHtml, strAttachments);

                    if (objSendResult.Success == true)
                    {
                        blnEmailSent = true;
                    }
                }
                else
                {
                    Log.Error(String.Format("No secondary email template assigned for campaign: {0}", campaign.CampaignBase.CampaignId.Raw), this);
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending campaign email confirmation {1}: {0}", ex.Message, campaign.CampaignBase.CampaignId.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }

        protected void lnkReferAnotherFriend_Click(object sender, EventArgs e)
        {
            ClearFormFields();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "anchor", "location.hash = '#formSection';", true);

            //Reset styles
            outerWrap.Attributes.Remove("class");
            outerWrap.Attributes.Add("class", "outer-wrap");

            wrapper.Attributes.Remove("class");
            wrapper.Attributes.Add("class", "wrapper-form");

            pnlForm.Attributes.Add("style", "display:block;");
            pnlThanks.Attributes.Add("style", "display:none;");

        }
    }
}