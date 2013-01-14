using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.EviBlog;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using System.Text;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal;
using Sitecore.Diagnostics;
using System.Collections;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonHome : System.Web.UI.UserControl
    {
        protected string clubFinderUrl;
        protected string eventPageUrl;
        protected BlogEntryItem campaignBlogEntry;
        protected PageSummaryItem linkedBlogEntry;
        protected AbstractItem PeopleSection;
        protected AbstractItem TrainingSection;
        protected string privacyPolicyUrl;
        protected string termsConditionsUrl;

        protected BespokeCampaignItem campaign = new BespokeCampaignItem(Sitecore.Context.Item);

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

        public string ClubFinderUrl
        {
            get { return clubFinderUrl; }
            set
            {
                clubFinderUrl = value;
            }
        }

        public string EventPageUrl
        {
            get { return eventPageUrl; }
            set
            {
                eventPageUrl = value;
            }
        }

        public BlogEntryItem CampaignBlogEntry
        {
            get { return campaignBlogEntry; }
            set
            {
                campaignBlogEntry = value;
            }
        }

        public PageSummaryItem LinkedBlogEntry
        {
            get { return linkedBlogEntry; }
            set
            {
                linkedBlogEntry = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            privacyPolicyUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy);
            //termsConditionsUrl = new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl;
            termsConditionsUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions);
            clubFinderUrl = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClubFinder)).QualifiedUrl;
            eventPageUrl = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.TriathlonEventPage)).QualifiedUrl;

            //Get blog content to display in page
            //List<Item> itemList = campaign.InnerItem.Children.ToList();

            //foreach (Item item in itemList)
            //{
            //    campaignBlogEntry = (BlogEntryItem)item;
            //    break;
            //}

            string[] sectionMicrositeIds = ItemPaths.MicrositeSectionTemplates.Split('|');

            if (campaign.InnerItem.HasChildren)
            {
                //Check if we have child sections (by seeing if the item inherits the abstract template)

                if (sectionMicrositeIds.Count() > 0)
                {

                    StringBuilder query = new StringBuilder();
                    query.Append("child::*[");

                    foreach (string sectionTemplateId in sectionMicrositeIds)
                    {
                        query.Append(String.Format("@@tid='{0}' or ", sectionTemplateId));
                    }
                    query.Remove(query.Length - 4, 4);
                    query.Append("]");

                    //Check if we have child sections to show (by seeing if the item inherits the abstract template)
                    if (campaign.InnerItem.Axes.SelectItems(query.ToString()) != null)
                    {
                        List<PageSummaryItem> childSectionList = campaign.InnerItem.Axes.SelectItems(query.ToString()).ToList().ConvertAll(X => new PageSummaryItem(X));
                        childSectionList.RemoveAll(x => x.Hidefrommenu.Checked);

                        List<AbstractItem> sectionList = new List<AbstractItem>();
                        //grab first two child sections only
                        int i = 0;
                        foreach(PageSummaryItem childSection in childSectionList)
                        {
                            if (i < 2)
                            {
                                //Convert To Abstract Item
                                sectionList.Add((AbstractItem)childSection.InnerItem);
                            }
                            else
                            {
                                break;
                            }
                            i++;
                        }

                        sectionList.First().IsFirst = true;

                        if (sectionList.Count > 0)
                        {
                            ChildSectionListing.DataSource = sectionList;
                            ChildSectionListing.DataBind();
                        }
                    }

                    //Get blog entry item
                    if (campaign.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@tid='{0}']", BlogEntryItem.TemplateId)) != null)
                    {
                        campaignBlogEntry = (BlogEntryItem)campaign.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@tid='{0}']", BlogEntryItem.TemplateId));
                    }
                }
            }


            //Get blog entry or article entry to link to (show first article only)
            List<Item> linkedBlogItemList = campaign.CampaignBase.Blogarticles.ListItems.ToList();

            foreach (Item linkedBlogItem in linkedBlogItemList)
            {
                linkedBlogEntry = (PageSummaryItem)linkedBlogItem;
                break;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                //Store Feedback Entitiy Details
                Feedback objFeedback = new Feedback();

                //Store Customer Entitiy Details
                Customer objCustomer = new Customer();

                objCustomer.EmailAddress = email.Value;
                //objCustomer.Firstname = "";
                //objCustomer.Surname = "";
                //objCustomer.HomeClubID = "";
                //objCustomer.SubscribeToNewsletter = false;
                //objCustomer.TelephoneNumber = "";
                //objCustomer.MembershipNumber = "";
                //objCustomer.Gender = "";
                //objCustomer.AddressLine1 = "";
                //objCustomer.AddressLine2 = "";
                //objCustomer.AddressLine3 = "";
                //objCustomer.AddressLine4 = "";
                //objCustomer.Postcode = "";

                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = campaign.CampaignBase.Campaigntype.Raw;
                objFeedback.FeedbackSubjectDetail = campaign.CampaignBase.Campaignname.Rendered;
                objFeedback.FeedbackTypeID = Convert.ToInt32(campaign.CampaignBase.CampaignId.Rendered);
                objFeedback.PrimaryClubID = "";
                objFeedback.SubmissionDate = DateTime.Now;

                string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                DataAccessLayer dal = new DataAccessLayer(connection);
                int successFlag = dal.SaveFeedback(Context.User.Identity.Name, campaign.DisplayName, objFeedback);

                //Data is sent to client via email
                SendAdminEmail();


                if (campaign.CampaignBase.Sendconfirmationemail.Checked == true)
                {
                    //Data is sent to customer via email
                    SendConfirmationEmail();
                }

                formToComplete.Visible = false;
                formCompleted.Visible = true;

                //System.Threading.Thread.Sleep(5000);
                pnlForm.Update();
                //ClearFormFields();

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error storing campaign details: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

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
                    //objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(campaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                    objTemplateVariables.Add("CampaignUrl", campaign.CampaignBase.QualifiedUrl);

                    //objTemplateVariables.Add("ClubManagerName", manager != null ? manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered : "");

                    objTemplateVariables.Add("EmailSubject", strEmailSubject);
                    objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                    objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                    objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                    objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                    objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                    objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                    objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());

                    System.Text.StringBuilder markupBuilder;
                    markupBuilder = new System.Text.StringBuilder();


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
                        objParser.SetTemplate(campaignEmail.Classichtml.Raw);
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
                //Log.Error(String.Format("Error sending contact us email confirmation {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
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
                objTemplateVariables.Add("CustomerEmail", email.Value);
                //objTemplateVariables.Add("HomeClubID", txtClubCode.Value);
                objTemplateVariables.Add("Comments", "N/A");
                objTemplateVariables.Add("SubscribeToNewsletter", "Yes");
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
                objParser.SetTemplate(campaign.CampaignBase.GetAdminTemplateHtml());

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
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }


    }
}