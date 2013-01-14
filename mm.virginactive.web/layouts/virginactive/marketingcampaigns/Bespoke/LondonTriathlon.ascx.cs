using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

    public partial class LondonTriathlon : System.Web.UI.UserControl
    {
        protected ManagerItem manager;
        protected ClubItem currentClub;
        protected string privacyPolicyUrl;
        protected string termsConditionsUrl;

        protected BespokeCampaignItem campaign = new BespokeCampaignItem(Sitecore.Context.Item);

        public ClubItem CurrentClub
        {
            get { return currentClub; }
            set
            {
                currentClub = value;
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

        protected void Page_Load(object sender, EventArgs e)
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

            clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), "default"));

            if (clubs != null)
            {

                //Sort clubs alphabetically
                /*
                clubList.Sort(delegate(ClubItem c1, ClubItem c2)
                {
                    return c1.Clubname.Raw.CompareTo(c2.Clubname.Raw);

                });*/

                foreach (ClubItem clubItem in clubs)
                {
                    if (clubItem != null)
                    {
                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            //clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                            clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToString()));
                        }
                    }
                }

                if (!campaign.CampaignBase.Usecustomclublist.Checked)
                {
                    clubFindSelect.Items.Insert(51, new ListItem("Head Office", "Head Office"));
                }
            }

            if (!Page.IsPostBack)
            {
                BindDropDownLists();
            }            

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

            //markupBuilder.Append(@"<title> " + campaign.PageSummary.NavigationTitle.Raw + " | " + Translate.Text("Virgin Active") + "</title>");
            //HtmlTitle title = (HtmlTitle)Page.FindControl("title");
            //title.Text = campaign.PageSummary.NavigationTitle.Raw + " | " + Translate.Text("Virgin Active");

            //markupBuilder.Append(@"<meta name='description' content=''>");
            markupBuilder.Append(@"<meta name='viewport' content='width=1020'>");
            markupBuilder.Append(@"<link rel='apple-touch-icon' href='/virginactive/images/apple-touch-icon.png'>");
            markupBuilder.Append(@"<link rel='shortcut icon' href='/virginactive/images/favicon.ico'>");
            markupBuilder.Append(@"<link href='/virginactive/css/fonts.css' rel='stylesheet'>");
            markupBuilder.Append(@"<link href='/va_campaigns/Bespoke/LondonTriathlon/css/styles.css' rel='stylesheet'>");
            head.Controls.Add(new LiteralControl(markupBuilder.ToString()));

            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"<script src='/va_campaigns/Bespoke/LondonTriathlon/js/plugins.js'></script>
                    <script src='/va_campaigns/Bespoke/LondonTriathlon/js/scripts.js'></script>
                    "));
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //check if we have a valid club selected
                Boolean blnClubSuccessfullySelected = clubFindSelect.SelectedIndex != 0;

                if (blnClubSuccessfullySelected == true)
                {
                    if (clubFindSelect.SelectedValue != "Head Office")
                    {
                        currentClub = (ClubItem)Sitecore.Context.Database.GetItem(clubFindSelect.SelectedValue);				

                        if (currentClub != null)
                        {

                            //Store Feedback Entitiy Details
                            Feedback objFeedback = new Feedback();

                            //Store Customer Entitiy Details
                            Customer objCustomer = new Customer();

                            objCustomer.EmailAddress = email.Value;
                            objCustomer.Firstname = firstName.Value;
                            objCustomer.Surname = surname.Value;
                            objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                            objCustomer.SubscribeToNewsletter = subscribe.Checked;
                            objCustomer.TelephoneNumber = number.Value.Trim();
                            objCustomer.MembershipNumber = memberNo.Value.Trim();
                            objCustomer.Gender = male.Checked == true ? "M" : "F";
                            objCustomer.AddressLine1 = address1.Value.Trim();
                            objCustomer.AddressLine2 = address2.Value.Trim();
                            objCustomer.AddressLine3 = "";
                            objCustomer.AddressLine4 = address3.Value.Trim();
                            objCustomer.Postcode = postcode.Value.Trim();

                            if (dob.Value.Trim() != "")
                            {
                                objCustomer.DateOfBirth = Convert.ToDateTime(dob.Value.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                            }

                            objFeedback.Customer = objCustomer;
                            objFeedback.FeedbackSubject = campaign.CampaignBase.Campaigntype.Raw;
                            objFeedback.FeedbackSubjectDetail = campaign.CampaignBase.Campaignname.Rendered;
                            objFeedback.FeedbackTypeID = Convert.ToInt32(campaign.CampaignBase.CampaignId.Rendered);
                            objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                            objFeedback.SubmissionDate = DateTime.Now;

                            List<Comment> objComments = new List<Comment>();
                            //Add Comment
                            Comment objComment = new Comment();
                            objComment.CommentDetail = member.Checked == true ? "Member" : "Staff";
                            objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                            objComment.SortOrder = 1;
                            objComment.Subject = "Entrant Type";

                            objComments.Add(objComment);

                            objComment = new Comment();
                            //objComment.CommentDetail = drpEvent.Items[drpEvent.SelectedIndex].Value;
                            objComment.CommentDetail = drpEvent.SelectedValue;
                            objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                            objComment.SortOrder = 2;
                            objComment.Subject = "Event";

                            objComments.Add(objComment);

                            objComment = new Comment();
                            objComment.CommentDetail = drpTopsize.SelectedValue;
                            objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                            objComment.SortOrder = 3;
                            objComment.Subject = "Top Size";

                            objComments.Add(objComment);

                            objComment = new Comment();
                            objComment.CommentDetail = teamname.Value.Trim();
                            objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                            objComment.SortOrder = 4;
                            objComment.Subject = "Team Name";

                            objComments.Add(objComment);

                            objComment = new Comment();
                            objComment.CommentDetail = nextofkin.Value.Trim();
                            objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                            objComment.SortOrder = 5;
                            objComment.Subject = "Next Of Kin";

                            objComments.Add(objComment);

                            objComment = new Comment();
                            objComment.CommentDetail = nextofkinnumber.Value.Trim();
                            objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                            objComment.SortOrder = 6;
                            objComment.Subject = "Next Of Kin Contact No.";

                            objComments.Add(objComment);

                            objFeedback.Comments = objComments;

                            string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                            DataAccessLayer dal = new DataAccessLayer(connection);
                            int successFlag = dal.SaveFeedback(Context.User.Identity.Name, campaign.DisplayName, objFeedback);

                            if (successFlag > 0)
                            {

                                //get managers info                        
                                List<ManagerItem> staffMembers = null;
                                if (currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                                {
                                    staffMembers = currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));

                                    manager = staffMembers.First();
                                }

                                if (campaign.CampaignBase.Sendconfirmationemail.Checked == true)
                                {
                                    //Data is sent to customer via email
                                    SendConfirmationEmail();
                                }

                                pnlForm.Attributes.Add("style", "display:none;");
                                pnlThanks.Attributes.Add("style", "display:block;");

                                string classNames = wrapper.Attributes["class"];
                                wrapper.Attributes.Add("class", "wrapper-thanks");
                                //ClearFormFields();
                            }
                            else
                            {
                                pnlForm.Attributes.Add("style", "display:block;");
                                pnlThanks.Attributes.Add("style", "display:none;");

                                //string classNames = article.Attributes["class"];
                                //article.Attributes.Add("class", classNames.Length > 0 ? classNames + " ImageOverlay" : "ImageOverlay");

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
                else
                {
                    //Store Feedback Entitiy Details
                    Feedback objFeedback = new Feedback();

                    //Store Customer Entitiy Details
                    Customer objCustomer = new Customer();

                    objCustomer.EmailAddress = email.Value;
                    objCustomer.Firstname = firstName.Value;
                    objCustomer.Surname = surname.Value;
                    objCustomer.HomeClubID = "N/A";
                    objCustomer.SubscribeToNewsletter = subscribe.Checked;
                    objCustomer.TelephoneNumber = number.Value.Trim();
                    objCustomer.MembershipNumber = memberNo.Value.Trim();
                    objCustomer.Gender = male.Checked == true ? "M" : "F";
                    objCustomer.AddressLine1 = address1.Value.Trim();
                    objCustomer.AddressLine2 = address2.Value.Trim();
                    objCustomer.AddressLine3 = "";
                    objCustomer.AddressLine4 = address3.Value.Trim();
                    objCustomer.Postcode = postcode.Value.Trim();

                    if (dob.Value.Trim() != "")
                    {
                        objCustomer.DateOfBirth = Convert.ToDateTime(dob.Value.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                    }

                    objFeedback.Customer = objCustomer;
                    objFeedback.FeedbackSubject = campaign.CampaignBase.Campaigntype.Raw;
                    objFeedback.FeedbackSubjectDetail = campaign.CampaignBase.Campaignname.Rendered;
                    objFeedback.FeedbackTypeID = Convert.ToInt32(campaign.CampaignBase.CampaignId.Rendered);
                    objFeedback.PrimaryClubID = "N/A";
                    objFeedback.SubmissionDate = DateTime.Now;

                    List<Comment> objComments = new List<Comment>();
                    //Add Comment
                    Comment objComment = new Comment();
                    objComment.CommentDetail = member.Checked == true ? "Member" : "Staff";
                    objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                    objComment.SortOrder = 1;
                    objComment.Subject = "Entrant Type";

                    objComments.Add(objComment);

                    objComment = new Comment();
                    //objComment.CommentDetail = drpEvent.Items[drpEvent.SelectedIndex].Value;
                    objComment.CommentDetail = drpEvent.SelectedValue;
                    objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                    objComment.SortOrder = 2;
                    objComment.Subject = "Event";

                    objComments.Add(objComment);

                    objComment = new Comment();
                    objComment.CommentDetail = drpTopsize.SelectedValue;
                    objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                    objComment.SortOrder = 3;
                    objComment.Subject = "Top Size";

                    objComments.Add(objComment);

                    objComment = new Comment();
                    objComment.CommentDetail = teamname.Value.Trim();
                    objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                    objComment.SortOrder = 4;
                    objComment.Subject = "Team Name";

                    objComments.Add(objComment);

                    objComment = new Comment();
                    objComment.CommentDetail = nextofkin.Value.Trim();
                    objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                    objComment.SortOrder = 5;
                    objComment.Subject = "Next Of Kin";

                    objComments.Add(objComment);

                    objComment = new Comment();
                    objComment.CommentDetail = nextofkinnumber.Value.Trim();
                    objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                    objComment.SortOrder = 6;
                    objComment.Subject = "Next Of Kin Contact No.";

                    objComments.Add(objComment);

                    objFeedback.Comments = objComments;

                    string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                    DataAccessLayer dal = new DataAccessLayer(connection);
                    int successFlag = dal.SaveFeedback(Context.User.Identity.Name, campaign.DisplayName, objFeedback);

                    if (successFlag > 0)
                    {
                        if (campaign.CampaignBase.Sendconfirmationemail.Checked == true)
                        {
                            //Data is sent to customer via email
                            SendConfirmationEmail();
                        }

                        pnlForm.Attributes.Add("style", "display:none;");
                        pnlThanks.Attributes.Add("style", "display:block;");

                        string classNames = wrapper.Attributes["class"];
                        wrapper.Attributes.Add("class", "wrapper-thanks");
                        //ClearFormFields();
                    }
                    else
                    {
                        pnlForm.Attributes.Add("style", "display:block;");
                        pnlThanks.Attributes.Add("style", "display:none;");

                        //string classNames = article.Attributes["class"];
                        //article.Attributes.Add("class", classNames.Length > 0 ? classNames + " ImageOverlay" : "ImageOverlay");

                        Log.Error(String.Format("Error storing campaign details"), this);
                    }
                }
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error storing campaign details: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

        }

        private void BindDropDownLists()
        {
            try
            {
                // Populate Query dropdown
                this.drpEvent.Items.Clear();

                //Need to store these in config as can not change if using VA webservice
                ListItem itm = new ListItem(Translate.Text("Select"), Translate.Text("default"));
                itm.Selected = true;
                drpEvent.Items.Add(itm);
                drpEvent.Attributes.Add("data-individual", "Individual event - Swim, Bike, Run");
                drpEvent.Attributes.Add("data-team", "Team event - Swim, Bike, Run");

                itm = new ListItem("Super Sprint (400m/10km/2.5km)");
                itm.Attributes.Add("data-optgroup", "individual");
                drpEvent.Items.Add(itm);

                itm = new ListItem("Sprint (750m/20km/5km)");
                itm.Attributes.Add("data-optgroup", "individual");
                drpEvent.Items.Add(itm);

                itm = new ListItem("Olympic (1500m/40km/10km)");
                itm.Attributes.Add("data-optgroup", "individual");
                drpEvent.Items.Add(itm);

                itm = new ListItem("Sprint Relay (750m/20km/5km)");
                itm.Attributes.Add("data-optgroup", "team");
                drpEvent.Items.Add(itm);

                itm = new ListItem("Olympic Relay (1500m/40km/10km)");
                itm.Attributes.Add("data-optgroup", "team");
                drpEvent.Items.Add(itm);

                // Populate Query dropdown
                this.drpTopsize.Items.Clear();

                //Need to store these in config as can not change if using VA webservice
                ListItem itm2 = new ListItem(Translate.Text("Select"), Translate.Text("default"));
                itm2.Selected = true;
                drpTopsize.Items.Add(itm2);

                itm2 = new ListItem("Small");
                drpTopsize.Items.Add(itm2);

                itm2 = new ListItem("Medium");
                drpTopsize.Items.Add(itm2);

                itm2 = new ListItem("Large");
                drpTopsize.Items.Add(itm2);

                itm2 = new ListItem("X-Large");
                drpTopsize.Items.Add(itm2);
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending data email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

        }

        private void ClearFormFields()
        {
            clubFindSelect.SelectedIndex = 0;
            //txtClubCode.Value = string.Empty;
            //txtClubGUID.Value = string.Empty;
            firstName.Value = string.Empty;
            surname.Value = string.Empty;
            email.Value = string.Empty;
            number.Value = string.Empty;
            subscribe.Checked = false;
        }

        private Boolean SendConfirmationEmail()
        {
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
                    string strEmailToAddress = email.Value.Trim();

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

                    objTemplateVariables.Add("CustomerName", firstName.Value.Trim() + " " + surname.Value.Trim());
                    objTemplateVariables.Add("ClubManagerName", manager != null ? manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered : "");

                    objTemplateVariables.Add("ClubName", currentClub != null ? currentClub.Clubname.Rendered : "");
                    objTemplateVariables.Add("EmailSubject", strEmailSubject);
                    objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                    objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                    objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                    objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                    objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                    objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                    objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                    objTemplateVariables.Add("ClubPhoneNumber", currentClub != null ? currentClub.Salestelephonenumber.Rendered : "");

                    System.Text.StringBuilder markupBuilder;
                    markupBuilder = new System.Text.StringBuilder();

                    if (currentClub != null)
                    {
                        markupBuilder.Append(currentClub.Addressline1.Rendered);
                        markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline2.Rendered) ? "<br />" + currentClub.Addressline2.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline3.Rendered) ? "<br />" + currentClub.Addressline3.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline4.Rendered) ? "<br />" + currentClub.Addressline4.Rendered : "");
                        markupBuilder.Append("<br />" + currentClub.Postcode.Rendered);

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
                        if (currentClub.InnerItem.TemplateName == Templates.ClassicClub)
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
                Log.Error(String.Format("Error sending contact us email confirmation {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }
    }
}