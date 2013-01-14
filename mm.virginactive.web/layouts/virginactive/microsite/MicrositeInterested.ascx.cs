using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using mm.virginactive.screportdal;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal.Models;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeInterested : System.Web.UI.UserControl
    {
        protected ManagerItem manager;
        protected ClubItem currentClub;

        protected MembershipEnquiryItem campaign = new MembershipEnquiryItem(Sitecore.Context.Item);
        //protected BespokeCampaignItem campaign = new BespokeCampaignItem(Sitecore.Context.Item);

        public ClubItem CurrentClub
        {
            get { return currentClub; }
            set
            {
                currentClub = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack != true)
            {
                // Populate Enquiry dropdown
                drpHowDidYouFindUs.Items.Clear();

                string queryItemsString = campaign.Querytypes.Raw.Replace("\r\n", ",");
                queryItemsString = queryItemsString.Replace("\n", ",");
                queryItemsString = queryItemsString.TrimEnd(',');
                string[] queryItems = queryItemsString.Split(',');

                //drpHowDidYouFindUs.Items.Add(new ListItem(Translate.Text("Select"), ""));
                foreach (string item in queryItems)
                {
                    drpHowDidYouFindUs.Items.Add(new ListItem(item, item));
                }

                ListItem itm = new ListItem(Translate.Text("Select"), Translate.Text("default"));
                itm.Selected = true;
                drpHowDidYouFindUs.Items.Insert(0, itm);

                btnSubmit.Text = Translate.Text("Submit");

                //this.drpHowDidYouFindUs.Items.Insert(0, (Translate.Text("Select")));
            }

            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"<script src='/virginactive/scripts/microsites/form.validation.js' type='text/javascript'></script>"));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnIsValid = true;

                if (blnIsValid == true)
                {
                    ClubMicrositeLandingItem landing = new ClubMicrositeLandingItem(Sitecore.Context.Item.Parent.Parent);
                    currentClub = landing.Club.Item;
                    //currentClub = (ClubItem)Sitecore.Context.Database.GetItem("/sitecore/content/Home/clubs/Aldersgate");

                    if (currentClub != null)
                    {

                        //Store Feedback Entitiy Details
                        Feedback objFeedback = new Feedback();

                        //Store Customer Entitiy Details
                        Customer objCustomer = new Customer();

                        string FirstName = "";
                        string Surname = "";

                        if (name.Value.IndexOf(" ") != -1)
                        {
                            FirstName = name.Value.Substring(0, name.Value.IndexOf(" ")).Trim();
                            Surname = name.Value.Substring(name.Value.IndexOf(" ") + 1).Trim();
                        }
                        else
                        {
                            FirstName = name.Value.Trim();
                        }

                        objCustomer.EmailAddress = email.Value;
                        objCustomer.Firstname = FirstName;
                        objCustomer.Surname = Surname;
                        objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                        objCustomer.TelephoneNumber = tel.Value;

                        objFeedback.Customer = objCustomer;
                        objFeedback.FeedbackSubject = campaign.CampaignBase.Campaigntype.Raw;
                        objFeedback.FeedbackSubjectDetail = campaign.CampaignBase.Campaignname.Rendered;
                        objFeedback.FeedbackTypeID = Convert.ToInt32(campaign.CampaignBase.CampaignId.Rendered);
                        objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                        objFeedback.SubmissionDate = DateTime.Now;

                        //Add Comment
                        Comment objComment = new Comment();

                        //objComment.CommentDetail = source.Items[source.SelectedIndex].Value; 
                        objComment.CommentDetail = drpHowDidYouFindUs.SelectedValue;
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 1;
                        objComment.Subject = Translate.Text("How did you hear about us?");

                        List<Comment> objComments = new List<Comment>();
                        objComments.Add(objComment);

                        //Add Comment
                        objComment = new Comment();

                        objComment.CommentDetail = chkExistingMember.Checked == true ? "Y" : "N";
                        objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                        objComment.SortOrder = 2;
                        objComment.Subject = Translate.Text("Existing Virgin Active member?");

                        objComments.Add(objComment);

                        objFeedback.Comments = objComments;

                        string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                        DataAccessLayer dal = new DataAccessLayer(connection);
                        int successFlag = dal.SaveFeedback(Context.User.Identity.Name, campaign.DisplayName, objFeedback);

                        if (successFlag > 0)
                        {
                            //Data is sent to client via email
                            SendAdminEmail();

                            if (chkExistingMember.Checked == false && campaign.CampaignBase.Isweblead.Checked == true)
                            {
                                //Data is sent to client via service
                                SendEnquiryDataService();
                            }

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

                            //imageStatic.Attributes.Add("style", "display:none;");
                            formDetails.Attributes.Add("style", "display:none;");
                            formConfirmation.Attributes.Add("style", "display:block;");
                            //ClearFormFields();
                            pnlForm.Update();
                        }
                        else
                        {
                            //imageStatic.Attributes.Add("style", "display:none;");
                            formDetails.Attributes.Add("style", "display:block;");
                            formConfirmation.Attributes.Add("style", "display:none;");
                            pnlForm.Update();

                            Log.Error(String.Format("Error storing campaign details"), this);
                        }
                    }
                    else
                    {
                        //imageStatic.Attributes.Add("style", "display:none;");
                        formDetails.Attributes.Add("style", "display:block;");
                        formConfirmation.Attributes.Add("style", "display:none;");
                        pnlForm.Update();

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
            name.Value = string.Empty;
            email.Value = string.Empty;
            tel.Value = string.Empty;
            //source.SelectedIndex = 0;
            drpHowDidYouFindUs.SelectedIndex = 0;
        }

        private Boolean SendEnquiryDataService()
        {
            bool blnEnquiryDataSent = false;

            try
            {
                mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();

                virginActiveService.InsertWebLead(
                    name.Value.Trim(),
                    currentClub.ClubId.Rendered,
                    tel.Value.Trim(),
                    "", //postcode
                    email.Value.Trim(),
                    "", //preferred time
                    "", //memberFlag
                    Settings.WebLeadsSourceID, //sourceId
                    drpHowDidYouFindUs.SelectedValue.ToString() != "" && drpHowDidYouFindUs.SelectedValue.ToString() != Translate.Text("Select") ? drpHowDidYouFindUs.SelectedValue.ToString() : "", //source
                    //campaign.CampaignBase.Campaignname.Rendered != "" ? campaign.CampaignBase.Campaignname.Rendered : "Unspecified Website Campaign", //source (campaignname)
                    campaign.CampaignBase.Campaigncode.Rendered != "" ? campaign.CampaignBase.Campaigncode.Rendered : "Virgin Active Website Campaign", //campaignId (campaigncode)
                    false); //newsletter

                //var test = virginActiveService.ReadWebLead();

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending enquiry webservice data {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEnquiryDataSent;
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

                    //string strEmailSubject = campaign.CampaignBase.Emailsubject.Rendered;
                    string strEmailSubject = emailItem.Subject.Raw;
                    //string strEmailFromAddress = Settings.WebsiteMailFromText;
                    string strEmailFromAddress = emailItem.Fromaddress.Raw;
                    string strEmailToAddress = email.Value.Trim();

                    //Populate email text variables
                    Hashtable objTemplateVariables = new Hashtable();
                    objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
                    objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesAndClasses));
                    objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealth));
                    objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.Memberships));
                    objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
                    objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));
                    objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));

                    objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                    //objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", campaign.CampaignBase.QualifiedUrl + "?page=Terms");

                    objTemplateVariables.Add("CustomerName", name.Value.Trim());
                    if (manager != null)
                    {
                        objTemplateVariables.Add("ClubManagerName", manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered);
                    }
                    else
                    {
                        objTemplateVariables.Add("ClubManagerName", "");
                    }
                    objTemplateVariables.Add("ClubName", currentClub.Clubname.Rendered);
                    objTemplateVariables.Add("EmailSubject", strEmailSubject);
                    objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                    objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                    objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                    objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                    objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                    objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                    objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                    objTemplateVariables.Add("ClubPhoneNumber", currentClub.Salestelephonenumber.Rendered);

                    System.Text.StringBuilder markupBuilder;
                    markupBuilder = new System.Text.StringBuilder();

                    markupBuilder.Append(currentClub.Addressline1.Rendered);
                    markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline2.Rendered) ? "<br />" + currentClub.Addressline2.Rendered : "");
                    markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline3.Rendered) ? "<br />" + currentClub.Addressline3.Rendered : "");
                    markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline4.Rendered) ? "<br />" + currentClub.Addressline4.Rendered : "");
                    markupBuilder.Append("<br />" + currentClub.Postcode.Rendered);

                    objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());

                    //Parser objParser = new Parser(campaign.CampaignBase.GetTemplateHtml(), objTemplateVariables);
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

        private Boolean SendAdminEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.FeedbackCampaignFormAdmin;
                string strEmailFromAddress = Settings.WebsiteMailFromText;

                string strEmailToAddress = "";
                string strEmailBccAddress = "";
                if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                {
                    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                }
                else
                {
                    //Use live emails
                    if (chkExistingMember.Checked == false && campaign.CampaignBase.Isweblead.Checked == true)
                    {
                        //form sent to webleads -send confirmation to admin team
                        strEmailToAddress = Settings.FeedbackCampaignEmailToListShort;
                    }
                    else
                    {
                        //form NOT sent to webleads -send directly to club
                        strEmailToAddress = campaign.CampaignBase.Administratorsemaillist.Raw;
                        //Bcc admin personnel
                        strEmailBccAddress = Settings.FeedbackCampaignEmailToListShort;
                    }
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("CustomerName", name.Value);
                objTemplateVariables.Add("CustomerEmail", email.Value);
                objTemplateVariables.Add("HomeClubID", currentClub.ClubId.Rendered);
                objTemplateVariables.Add("Telephone", tel.Value);
                objTemplateVariables.Add("ExistingMember", chkExistingMember.Checked == true ? "Y" : "N");
                objTemplateVariables.Add("Comments", "N/A");
                objTemplateVariables.Add("SubscribeToNewsletter", "N/A");
                objTemplateVariables.Add("PublishDetails", "N/A");
                objTemplateVariables.Add("FeedbackSubject", campaign.CampaignBase.Campaigntype.Raw);
                objTemplateVariables.Add("FeedbackSubjectDetail", campaign.CampaignBase.Campaignname.Rendered);
                objTemplateVariables.Add("SubmissionDate", DateTime.Now.ToShortDateString());
                objTemplateVariables.Add("ImageSubmitted", "N/A");

                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.FeedbackCampaignFormAdmin), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.FeedbackCampaignFormAdmin)).Emailhtml.Text);
                string strMessageBody = objParser.Parse();

                mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                string strAttachments = "";

                //now call messaging service
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", strEmailBccAddress, strEmailSubject, true, strMessageBody, strAttachments);


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

    }
}