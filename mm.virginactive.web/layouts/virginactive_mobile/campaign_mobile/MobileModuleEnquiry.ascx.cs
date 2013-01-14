﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.web.layouts.virginactive.clubfinder;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using Sitecore.Data.Items;
using Sitecore.Collections;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using System.Collections;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.Mobile.Campaigns
{
    public partial class MobileModuleEnquiry : System.Web.UI.UserControl
    {
        protected ModuleEnquiryItem currentItem = new ModuleEnquiryItem(Sitecore.Context.Item);
        protected ModuleEnquiryLandingItem currentCampaign;
        protected Boolean newSession = false;
        protected ClubItem currentClub;
        protected ManagerItem manager;
        protected string homePageUrl = "";

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

            //Get parent campaign
            currentCampaign = new ModuleEnquiryLandingItem(currentItem.InnerItem.Parent);
            homePageUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage);

            //Set page
            if (!Page.IsPostBack)
            {
                SetPage();
            }

        }

        private void SetPage()
        {
            try
            {

                //Get child carousel modules
                if (currentItem.InnerItem.HasChildren || currentCampaign.InnerItem.HasChildren)
                {
                    //Find Modules
                    List<ModuleItem> ModuleList = new List<ModuleItem>();

                    //Check if we have modules for the current campaign page 
                    if (currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", ModuleItem.TemplateId)) != null)
                    {
                        ModuleList.AddRange(currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", ModuleItem.TemplateId)).ToList().ConvertAll(Y => new ModuleItem(Y)));
                    }

                    //Check if we have shared modules
                    if (currentCampaign.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", ModuleItem.TemplateId)) != null)
                    {
                        ModuleList.AddRange(currentCampaign.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", ModuleItem.TemplateId)).ToList().ConvertAll(Y => new ModuleItem(Y)));
                    }

                    if (ModuleList.Count > 0)
                    {
                        ModuleList.First().IsFirst = true;

                        CarouselPanels.DataSource = ModuleList;
                        CarouselPanels.DataBind();
                    }
                }


				List<ClubItem> clubs = null;

				if (currentCampaign.CampaignBase.Usecustomclublist.Checked)
				{
					//Use a custom list of clubs
					clubs = currentCampaign.CampaignBase.Customclublist.ListItems.ConvertAll(X => new ClubItem(X));
				}
				else
				{
					//Use a custom list of clubs
					Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
					ChildList clubLst = clubRoot.Children;
					clubs = clubLst.ToList().ConvertAll(X => new ClubItem(X));
					clubs.RemoveAll(x => x.IsHiddenFromMenu());
					clubs.RemoveAll(x => x.IsPlaceholder.Checked && !x.ShowInClubSelect.Checked);
				}

                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));

				foreach (Item club in clubs)
                {
                    ClubItem clubItem = new ClubItem(club);
                    if (clubItem != null)
                    {
                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToString()));

                            //ListItem lst = new ListItem(clubLabel, clubItem.ID.ToString(), false);
                            //lst.Attributes.Add("data-email", clubItem.Salesemail.Raw);
                            //clubFindSelect.Items.Add(lst);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error loading campaign page: {0}", ex.Message), this);
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
                        //Guid id = new Guid(
                        currentClub = (ClubItem)Sitecore.Context.Database.GetItem(clubFindSelect.SelectedValue);

                        if (currentClub != null)
                        {

                            //Store Feedback Entitiy Details
                            Feedback objFeedback = new Feedback();

                            //Store Customer Entitiy Details
                            Customer objCustomer = new Customer();

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

                            objCustomer.EmailAddress = txtEmail.Value;
                            objCustomer.Firstname = FirstName;
                            objCustomer.Surname = Surname;
                            objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                            objCustomer.TelephoneNumber = txtPhone.Value;

                            objFeedback.Customer = objCustomer;
                            objFeedback.FeedbackSubject = currentCampaign.CampaignBase.Campaigntype.Raw;
                            objFeedback.FeedbackSubjectDetail = currentCampaign.CampaignBase.Campaignname.Rendered;
                            objFeedback.FeedbackTypeID = Convert.ToInt32(currentCampaign.CampaignBase.CampaignId.Rendered);
                            objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                            objFeedback.SubmissionDate = DateTime.Now;

                            //Save to session (required for confirmation page)
                            Session["sess_FormSubmission"] = objFeedback;

                            string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                            DataAccessLayer dal = new DataAccessLayer(connection);
                            int successFlag = dal.SaveFeedback(Context.User.Identity.Name, currentCampaign.DisplayName, objFeedback);

                            if (successFlag > 0)
                            {
                                //Data is sent to client via email
                                SendAdminEmail();

                                if (currentCampaign.CampaignBase.Isweblead.Checked == true)
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

                                if (currentCampaign.CampaignBase.Sendconfirmationemail.Checked == true)
                                {
                                    //Data is sent to customer via email
                                    SendConfirmationEmail();
                                }

                                //Get enquiries link
                                string ClubEnquiriesUrl = "";
                                PageSummaryItem enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
                                if (enqForm != null)
                                {
                                    ClubEnquiriesUrl = enqForm.Url;
                                }

                                Response.Redirect(ClubEnquiriesUrl + "?action=confirm");
                            }
                            else
                            {
                                Log.Error(String.Format("Error submitting campaign form (mobile) -save to db"), this);
                            }
                        }
                        else
                        {
                            Log.Error(String.Format("Error submitting campaign form (mobile) -setting current club"), this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error submitting campaign form (mobile): {0}", ex.Message), this);
            }
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
                    Settings.CampaignWebLeadsSource, //source
                    //currentCampaign.CampaignBase.Campaignname.Rendered != "" ? currentCampaign.CampaignBase.Campaignname.Rendered : "Unspecified Website Campaign", //source (campaignname)
                    currentCampaign.CampaignBase.Campaigncode.Rendered != "" ? currentCampaign.CampaignBase.Campaigncode.Rendered : "Virgin Active Website Campaign", //campaignId (campaigncode)
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
            }

            return blnEnquiryDataSent;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                if (currentCampaign.CampaignBase.Emailtemplate.Item != null)
                {
                    //Get Campaign Email object
                    EmailBaseItem emailItem = currentCampaign.CampaignBase.Emailtemplate.Item;

                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = true;

                    //string strEmailSubject = campaign.CampaignBase.Emailsubject.Rendered;
                    string strEmailSubject = emailItem.Subject.Raw;
                    //string strEmailFromAddress = Settings.WebsiteMailFromText;
                    string strEmailFromAddress = emailItem.Fromaddress.Raw;
                    string strEmailToAddress = txtEmail.Value.Trim();

                    //Populate email text variables
                    Hashtable objTemplateVariables = new Hashtable();
                    objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
                    objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesAndClasses));
                    objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealth));
                    objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.Memberships));
                    objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
                    objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));
                    objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));

                    //objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(currentCampaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
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
                    objTemplateVariables.Add("EmailSubject", strEmailSubject);
                    objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                    objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                    objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                    objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                    objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                    objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                    objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                    objTemplateVariables.Add("ClubPhoneNumber", currentClub.Salestelephonenumber.Rendered);
                    objTemplateVariables.Add("ClubHomePageUrl", Sitecore.Links.LinkManager.GetItemUrl(currentClub, urlOptions));

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
                Log.Error(String.Format("Error sending module enquiry email confirmation {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
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
                    //form sent to webleads -send confirmation to admin team
                    strEmailToAddress = Settings.FeedbackCampaignEmailToListShort;
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("CustomerName", txtName.Value);
                objTemplateVariables.Add("CustomerEmail", txtEmail.Value);
                objTemplateVariables.Add("HomeClubID", currentClub.ClubId.Rendered);
                objTemplateVariables.Add("ClubName", currentClub.Clubname.Rendered);
                objTemplateVariables.Add("Telephone", txtPhone.Value);
                objTemplateVariables.Add("Comments", "N/A");
                objTemplateVariables.Add("HowDidCustomerFindUs", Settings.MobileWebLeadsSource);
                objTemplateVariables.Add("SubscribeToNewsletter", chkSubscribe.Checked ? "Yes" : "No");
                objTemplateVariables.Add("PublishDetails", "N/A");
                objTemplateVariables.Add("FeedbackSubject", currentCampaign.CampaignBase.Campaigntype.Raw);
                objTemplateVariables.Add("FeedbackSubjectDetail", currentCampaign.CampaignBase.Campaignname.Rendered);
                objTemplateVariables.Add("SubmissionDate", DateTime.Now.ToShortDateString());
                objTemplateVariables.Add("ImageSubmitted", "N/A");
                objTemplateVariables.Add("EnquiryType", "General");

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
            }

            return blnEmailSent;
        }
    }
}