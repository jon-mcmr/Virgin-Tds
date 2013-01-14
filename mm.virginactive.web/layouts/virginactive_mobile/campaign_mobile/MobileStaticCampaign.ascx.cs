using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using System.Collections;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.Mobile.Campaigns
{
    public partial class MobileStaticCampaign : System.Web.UI.UserControl
    {
        protected ManagerItem manager;
        protected ClubItem currentClub;
        protected CampaignItem campaign = new CampaignItem(Sitecore.Context.Item);

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
            }

            if (clubs != null)
            {
                clubs.RemoveAll(x => x.IsHiddenFromMenu());
                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));

                /*
                //Sort clubs alphabetically
                clubList.Sort(delegate(ClubItem c1, ClubItem c2)
                {
                    return c1.Clubname.Raw.CompareTo(c2.Clubname.Raw);

                });*/
                bool ShowAllClubs = false;
                DropDownItem item = Sitecore.Context.Database.GetItem(ItemPaths.FreeGuestPassAllClubs);
                if (item != null)
                {
                    ShowAllClubs = campaign.CampaignBase.Campaigntype.Raw == item.Name;
                }

                if (clubFindSelect.Items.Count < 2)
                {
                    foreach (ClubItem clubItem in clubs)
                    {
                        if (clubItem != null)
                        {
                            if (!String.IsNullOrEmpty(clubItem.Clubname.Text) && !String.IsNullOrEmpty(campaign.CampaignBase.Campaigntype.Raw))
                            {
                                //Check if we need to just show classic clubs
                                if (!ShowAllClubs && campaign.CampaignBase.Usecustomclublist.Checked == false)
                                {
                                    //Only show Classic clubs //TODO: Make this a setting in Sitecore
                                    if (clubItem.InnerItem.TemplateName != Templates.ClassicClub)
                                    {
                                        string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                                        //clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ClubId.Raw));
                                        clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.InnerItem.ID.ToString()));
                                    }
                                }
                                else
                                {
                                    string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                                    //clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ClubId.Raw));
                                    clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.InnerItem.ID.ToString()));
                                }
                            }
                        }
                    }
                }
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
                        //objCustomer.HomeClubID = txtClubCode.Value;
                        objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                        objCustomer.SubscribeToNewsletter = chkSubscribe.Checked;
                        objCustomer.TelephoneNumber = txtPhone.Value;

                        objFeedback.Customer = objCustomer;
                        //objFeedback.FeedbackSubject = campaign.CampaignBase.Campaigntype.Raw;
                        objFeedback.FeedbackSubject = !String.IsNullOrEmpty(campaign.CampaignBase.Campaigntype.Raw) ? campaign.CampaignBase.Campaigntype.Raw : "";
                        objFeedback.FeedbackSubjectDetail = campaign.CampaignBase.Campaignname.Rendered;
                        objFeedback.FeedbackTypeID = Convert.ToInt32(campaign.CampaignBase.CampaignId.Rendered);
                        objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                        objFeedback.SubmissionDate = DateTime.Now;

                        //Save to session (required for confirmation page)
                        Session["sess_FormSubmission"] = objFeedback;

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
            catch (Exception ex)
            {
                Log.Error(String.Format("Error submitting campaign form (mobile): {0}", ex.Message), this);
            }

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
                objTemplateVariables.Add("CustomerName", txtName.Value);
                objTemplateVariables.Add("CustomerEmail", txtEmail.Value);
                //objTemplateVariables.Add("HomeClubID", txtClubCode.Value);
                objTemplateVariables.Add("HomeClubID", currentClub.ClubId.Rendered);
                objTemplateVariables.Add("Telephone", txtPhone.Value);
                objTemplateVariables.Add("Comments", "N/A");
                objTemplateVariables.Add("SubscribeToNewsletter", chkSubscribe.Checked ? "Yes" : "No");
                objTemplateVariables.Add("PublishDetails", "N/A");
                //objTemplateVariables.Add("FeedbackSubject", campaign.CampaignBase.Campaigntype.Raw);
                objTemplateVariables.Add("FeedbackSubject", !String.IsNullOrEmpty(campaign.CampaignBase.Campaigntype.Raw) ? campaign.CampaignBase.Campaigntype.Raw : "");
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
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", "", strEmailSubject, true, strMessageBody, strAttachments);


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

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                if (campaign.CampaignBase.Emailtemplate.Item != null)
                {
                    //Get Campaign Email object
                    //CampaignEmailItem campaignEmail = (CampaignEmailItem)campaign.CampaignBase.Emailtemplate.Item;
                    EmailBaseItem emailItem = campaign.CampaignBase.Emailtemplate.Item;

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

                    if (campaign.CampaignBase.Termsandconditionslink.Item != null)
                    {
                        objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                    }
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
            }

            return blnEmailSent;
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
                    //campaign.CampaignBase.Campaignname.Rendered != "" ? campaign.CampaignBase.Campaignname.Rendered : "Unspecified Website Campaign", //source (campaignname)
                    campaign.CampaignBase.Campaigncode.Rendered != "" ? campaign.CampaignBase.Campaigncode.Rendered : "Virgin Active Website Campaign", //campaignId (campaigncode)
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
    }
}