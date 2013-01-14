using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Collections;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;

namespace mm.virginactive.web.layouts.virginactive.indoortriathlon
{
    public partial class IndoorEvents : System.Web.UI.UserControl
    {
        protected IndoorEventsItem currentItem = new IndoorEventsItem(Sitecore.Context.Item);
        //protected IndoorFormItem registrationForm;
        protected LinkWidgetItem registrationForm;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get widget details
            //clubFinder = Sitecore.Context.Database.GetItem(ItemPaths.IndoorTriathlonClubFinderWidget);				
            registrationForm = Sitecore.Context.Database.GetItem(ItemPaths.IndoorTriathlonRegistrationForm);

            if (currentItem.Panel1title.Rendered == "")
            {
                panel1.Visible = false;
            }
            if (currentItem.Panel2title.Rendered == "")
            {
                panel2.Visible = false;
            }
            if (currentItem.Panel3title.Rendered == "")
            {
                panel3.Visible = false;
            }
            if (currentItem.Panel4title.Rendered == "")
            {
                panel4.Visible = false;
            }

            //Add dynamic content
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/indoortriathlon/indoor-tri.js""></script>"));

            //Add club list
            System.Text.StringBuilder markupBuilder;
            markupBuilder = new System.Text.StringBuilder();

            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = true;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            foreach (Item item in currentItem.Clubs.ListItems)
            {
                ClubItem club = (ClubItem)item;
                if (!club.IsHiddenFromMenu())
                {

                    if (club.PlaceholderCampaign.Item == null)
                    {
                        markupBuilder.Append(@"<li><a href=""" + Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions) + @""">" + club.Clubname.Rendered + @"</a></li>");
                    }
                    else
                    {
                        Item campaign = new IndoorEventsItem(club.PlaceholderCampaign.Item);
                        markupBuilder.Append(@"<li><a href=""" + Sitecore.Links.LinkManager.GetItemUrl(campaign, urlOptions) + @""">" + club.Clubname.Rendered + @"</a></li>");
                    }
                }
            }


            clubList.Text = markupBuilder.ToString();
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Store Feedback Entitiy Details
        //        Feedback objFeedback = new Feedback();

        //        //Store Customer Entitiy Details
        //        Customer objCustomer = new Customer();

        //        objCustomer.EmailAddress = txtEmail.Value;
        //        objCustomer.SubscribeToNewsletter = false;

        //        objFeedback.Customer = objCustomer;
        //        objFeedback.FeedbackSubject = "Indoor Triathlon Sign Up";
        //        objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
        //        objFeedback.FeedbackTypeID = Constants.FeedbackType.IndoorTriathlon;
        //        objFeedback.PrimaryClubID = "";
        //        objFeedback.SubmissionDate = DateTime.Now;

        //        string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
        //        DataAccessLayer dal = new DataAccessLayer(connection);
        //        int successFlag = dal.SaveFeedback(Context.User.Identity.Name, currentItem.DisplayName, objFeedback);

        //        //Data is sent to client via email
        //        SendAdminEmail();


        //        //Data is sent to customer via email
        //        SendConfirmationEmail();

        //        formToComplete.Visible = false;
        //        formCompleted.Visible = true;

        //        //System.Threading.Thread.Sleep(5000);
        //        pnlForm.Update();
        //        //ClearFormFields();

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(String.Format("Error storing campaign details: {0}", ex.Message), this);
        //    }
        //}

        //private Boolean SendAdminEmail()
        //{
        //    bool blnEmailSent = false;

        //    try
        //    {
        //        string strEmailSubject = EmailSubjects.IndoorTriathlonFormAdmin;
        //        string strEmailFromAddress = Settings.WebsiteMailFromText;

        //        string strEmailToAddress = "";
        //        if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
        //        {
        //            //Use test
        //            strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
        //        }
        //        else
        //        {
        //            strEmailToAddress = Settings.FeedbackCampaignEmailToListShort;
        //        }

        //        //Populate email text variables
        //        Hashtable objTemplateVariables = new Hashtable();
        //        objTemplateVariables.Add("CustomerEmail", txtEmail.Value);
        //        objTemplateVariables.Add("Comments", "N/A");
        //        objTemplateVariables.Add("SubscribeToNewsletter", chkSubscribe.Checked ? "Y" : "N");
        //        objTemplateVariables.Add("PublishDetails", "N/A");
        //        objTemplateVariables.Add("SubmissionDate", DateTime.Now.ToShortDateString());

        //        objTemplateVariables.Add("EmailSubject", strEmailSubject);
        //        objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
        //        objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

        //        //Parser objParser = new Parser(Server.MapPath(EmailTemplates.FeedbackCampaignFormAdmin), objTemplateVariables);
        //        Parser objParser = new Parser(objTemplateVariables);
        //        objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.IndoorTriathlonFormAdmin)).Emailhtml.Text);

        //        string strMessageBody = objParser.Parse();

        //        mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
        //        mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

        //        string strAttachments = "";

        //        //now call messaging service
        //        objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", "", strEmailSubject, true, strMessageBody, strAttachments);


        //        if (objSendResult.Success == true)
        //        {
        //            blnEmailSent = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(String.Format("Error sending club feedback data email: {0}", ex.Message), this);
        //    }

        //    return blnEmailSent;
        //}

        //private Boolean SendConfirmationEmail()
        //{
        //    bool blnEmailSent = false;

        //    try
        //    {
        //        string strEmailSubject = EmailSubjects.IndoorTriathlonConfirmation;
        //        string strEmailFromAddress = Settings.WebsiteMailFromText;
        //        string strEmailToAddress = txtEmail.Value.Trim();

        //        //Populate email text variables
        //        Hashtable objTemplateVariables = new Hashtable();
        //        objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
        //        objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));
        //        objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesAndClasses));
        //        objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealth));
        //        objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.Memberships));
        //        objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
        //        objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));


        //        objTemplateVariables.Add("EmailSubject", strEmailSubject);
        //        objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
        //        objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
        //        objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
        //        objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
        //        objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
        //        objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
        //        objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());

        //        System.Text.StringBuilder markupBuilder;
        //        markupBuilder = new System.Text.StringBuilder();

        //        //Parser objParser = new Parser(Server.MapPath(EmailTemplates.ContactUsFeedbackConfirmation), objTemplateVariables);
        //        Parser objParser = new Parser(objTemplateVariables);
        //        objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.IndoorTriathlonConfirmation)).Emailhtml.Text);
        //        string strMessageBody = objParser.Parse();

        //        mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
        //        mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

        //        string strAttachments = "";

        //        //now call messaging service
        //        objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strMessageBody, strAttachments);

        //        if (objSendResult.Success == true)
        //        {
        //            blnEmailSent = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.Error(String.Format("Error sending contact us email confirmation {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
        //    }

        //    return blnEmailSent;
        //}
    }
}