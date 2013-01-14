using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using Sitecore.Web;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Collections;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Globalization;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class MembershipCampaign : System.Web.UI.UserControl
    {

        protected NewMemberCampaignLandingItem current = new NewMemberCampaignLandingItem(Sitecore.Context.Item);
        protected string thisUrl = "";
        protected string location = "";
        protected Club club = null;
        protected ManagerItem manager;

        protected void Page_Load(object sender, EventArgs e)
        {
            thisUrl = current.PageSummary.QualifiedUrl;

            if (!String.IsNullOrEmpty(WebUtil.GetQueryString("searchloc")))
            {
                location = WebUtil.GetQueryString("searchloc", "");
            }


            //display the club info.
            if (!String.IsNullOrEmpty(WebUtil.GetQueryString("cid")))
            {
                try
                {

                    Sitecore.Data.ID id = new Sitecore.Data.ID(WebUtil.GetQueryString("cid"));
                    Item c = Sitecore.Context.Database.GetItem(id);

                    if (c != null)
                    {
                        ClubInfoPh.Visible = true;
                        club = new Club(c);
                        txtClubCode.Value = club.ClubItm.ClubId.Raw;
                    }

                }
                catch (Exception ex)
                {
                    Log.Warn(String.Format("Could not fetch club for 12 for 10 offer from qs: {0} error: {1}", WebUtil.GetQueryString("cid"), ex.Message), this);
                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                }
            }

            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"      
                <script src='/va_campaigns/js/ajax.js'></script>
                <script src='/virginactive/scripts/infobox_packed.js'></script>
                "));

                if (!String.IsNullOrEmpty(WebUtil.GetQueryString("searchloc")))
                {
                    scriptPh.Controls.Add(new LiteralControl(String.Format(@"<script>
		            GetSuggestions('{0}');
                    viewClubFinderResults();
                    </script>", WebUtil.GetQueryString("searchloc"))));
                    ResultsPh.Visible = true;
                }
            }

            //add the item specific css stylesheet
            string classNames = article.Attributes["class"];
            article.Attributes.Add("class", classNames.Length > 0 ? classNames + " membs-" + current.CampaignBase.Cssstyle.Raw : "membs-" + current.CampaignBase.Cssstyle.Raw);

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
            HtmlTitle title = (HtmlTitle)Page.FindControl("title");
            if (title != null)
            {
                title.Text = current.PageSummary.NavigationTitle.Raw + " | " + Translate.Text("Virgin Active");
            }

            //Add Css Style Sheet
            HtmlGenericControl link = new HtmlGenericControl("LINK");
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("href", "/va_campaigns/css/membership.css");

            ////Add to the page header
            head.Controls.Add(link);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnIsValid = true;

                if (blnIsValid == true)
                {
                    if (club != null)
                    {

                        //Store Feedback Entitiy Details
                        Feedback objFeedback = new Feedback();

                        //Store Customer Entitiy Details
                        Customer objCustomer = new Customer();

                        string FirstName = "";
                        string Surname = "";

                        if (yourname.Value.IndexOf(" ") != -1)
                        {
                            FirstName = yourname.Value.Substring(0, yourname.Value.IndexOf(" ")).Trim();
                            Surname = yourname.Value.Substring(yourname.Value.IndexOf(" ") + 1).Trim();
                        }
                        else
                        {
                            FirstName = yourname.Value.Trim();
                        }

                        objCustomer.EmailAddress = email.Value;
                        objCustomer.Firstname = FirstName;
                        objCustomer.Surname = Surname;
                        objCustomer.HomeClubID = club.ClubItm.ClubId.Rendered;
                        objCustomer.SubscribeToNewsletter = subscribe.Checked;
                        objCustomer.TelephoneNumber = contact.Value;

                        objFeedback.Customer = objCustomer;
                        objFeedback.FeedbackSubject = current.CampaignBase.Campaigntype.Raw;
                        objFeedback.FeedbackSubjectDetail = current.CampaignBase.Campaignname.Rendered;
                        objFeedback.FeedbackTypeID = Convert.ToInt32(current.CampaignBase.CampaignId.Rendered);
                        objFeedback.PrimaryClubID = club.ClubItm.ClubId.Rendered;
                        objFeedback.SubmissionDate = DateTime.Now;

                        string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                        DataAccessLayer dal = new DataAccessLayer(connection);
                        int successFlag = dal.SaveFeedback(Context.User.Identity.Name, current.DisplayName, objFeedback);

                        if (successFlag > 0)
                        {
                            //Data is sent to client via email
                            SendAdminEmail();

                            if (current.CampaignBase.Isweblead.Checked == true)
                            {
                                //Data is sent to client via service
                                SendEnquiryDataService();
                            }

                            //get managers info                        
                            List<ManagerItem> staffMembers = null;
                            if (club.ClubItm.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                            {
                                staffMembers = club.ClubItm.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));

                                manager = staffMembers.First();
                            }

                            if (current.CampaignBase.Sendconfirmationemail.Checked == true)
                            {
                                //Data is sent to customer via email
                                SendConfirmationEmail();
                            }

                            //imageStatic.Attributes.Add("style", "display:none;");
                            innerform.Attributes.Add("style", "display:none;");
                            formCompleted.Attributes.Add("style", "display:block;");
                            ClearFormFields();
                            pnlForm.Update();
                        }
                        else
                        {
                            //imageStatic.Attributes.Add("style", "display:none;");
                            innerform.Attributes.Add("style", "display:block;");
                            formCompleted.Attributes.Add("style", "display:none;");
                            pnlForm.Update();

                            Log.Error(String.Format("Error storing campaign details"), this);
                        }
                    }
                    else
                    {
                        //imageStatic.Attributes.Add("style", "display:none;");
                        innerform.Attributes.Add("style", "display:block;");
                        formCompleted.Attributes.Add("style", "display:none;");
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
                objTemplateVariables.Add("CustomerName", yourname.Value);
                objTemplateVariables.Add("CustomerEmail", email.Value);
                objTemplateVariables.Add("HomeClubID", club.ClubItm.ClubId.Rendered);
                objTemplateVariables.Add("Telephone", contact.Value);
                objTemplateVariables.Add("Comments", "N/A");
                objTemplateVariables.Add("SubscribeToNewsletter", subscribe.Checked ? "Yes" : "No");
                objTemplateVariables.Add("PublishDetails", "N/A");
                objTemplateVariables.Add("FeedbackSubject", current.CampaignBase.Campaigntype.Raw);
                objTemplateVariables.Add("FeedbackSubjectDetail", current.CampaignBase.Campaignname.Rendered);
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
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }

        private void ClearFormFields()
        {
            yourname.Value = string.Empty;
            email.Value = string.Empty;
            contact.Value = string.Empty;
            subscribe.Checked = false;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                if (current.CampaignBase.Emailtemplate.Item != null)
                {
                    //Get Campaign Email object
                    //CampaignEmailItem campaignEmail = (CampaignEmailItem)campaign.CampaignBase.Emailtemplate.Item;
                    EmailBaseItem emailItem = current.CampaignBase.Emailtemplate.Item;

                    //string strEmailSubject = campaign.CampaignBase.Emailsubject.Rendered;
                    string strEmailSubject = emailItem.Subject.Raw;
                    //string strEmailFromAddress = Settings.WebsiteMailFromText;
                    string strEmailFromAddress = emailItem.Fromaddress.Raw;
                    string strEmailToAddress = email.Value.Trim();

                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = true;
                    urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                    //Populate email text variables
                    Hashtable objTemplateVariables = new Hashtable();
                    objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
                    objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesAndClasses));
                    objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealth));
                    objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.Memberships));
                    objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
                    objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));
                    objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));
                    if (!String.IsNullOrEmpty(current.CampaignBase.Termsandconditionslink.Raw))
                    {
                        objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(current.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                    }
                    //objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(current.CampaignBase.Termsandconditionslink.Field.TargetID.ToString()));
                    objTemplateVariables.Add("ClubHomePageUrl", Sitecore.Links.LinkManager.GetItemUrl(club.ClubItm, urlOptions));
                    objTemplateVariables.Add("CustomerName", yourname.Value.Trim());
                    if (manager != null)
                    {
                        objTemplateVariables.Add("ClubManagerName", manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered);
                    }
                    else
                    {
                        objTemplateVariables.Add("ClubManagerName", "");
                    }
                    objTemplateVariables.Add("ClubName", club.ClubItm.Clubname.Rendered);
                    objTemplateVariables.Add("EmailSubject", strEmailSubject);
                    objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                    objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                    objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                    objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                    objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                    objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                    objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                    objTemplateVariables.Add("ClubPhoneNumber", club.ClubItm.Salestelephonenumber.Rendered);

                    System.Text.StringBuilder markupBuilder;
                    markupBuilder = new System.Text.StringBuilder();

                    markupBuilder.Append(club.ClubItm.Addressline1.Rendered);
                    markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline2.Rendered) ? "<br />" + club.ClubItm.Addressline2.Rendered : "");
                    markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline3.Rendered) ? "<br />" + club.ClubItm.Addressline3.Rendered : "");
                    markupBuilder.Append(!String.IsNullOrEmpty(club.ClubItm.Addressline4.Rendered) ? "<br />" + club.ClubItm.Addressline4.Rendered : "");
                    markupBuilder.Append("<br />" + club.ClubItm.Postcode.Rendered);

                    objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());

                    //Parser objParser = new Parser(current.CampaignBase.GetTemplateHtml(), objTemplateVariables);
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
                        if (current.InnerItem.TemplateName == Templates.ClassicClub)
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
                Log.Error(String.Format("Error sending contact us email confirmation {1}: {0}", ex.Message, club.ClubItm.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
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
                    yourname.Value.Trim(),
                    club.ClubItm.ClubId.Rendered,
                    contact.Value.Trim(),
                    "", //postcode
                    email.Value.Trim(),
                    "", //preferred time
                    "", //memberFlag
                    Settings.WebLeadsSourceID, //sourceId
                    Settings.CampaignWebLeadsSource, //source
                    //current.CampaignBase.Campaignname.Rendered != "" ? current.CampaignBase.Campaignname.Rendered : "Unspecified Website Campaign", //source (campaignname)
                    current.CampaignBase.Campaigncode.Rendered != "" ? current.CampaignBase.Campaigncode.Rendered : "Virgin Active Website Campaign", //campaignId (campaigncode)
                    subscribe.Checked ? true : false); //newsletter

                //var test = virginActiveService.ReadWebLead();

                if (club.ClubItm.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", club.ClubItm.Clubname.Raw), this);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending enquiry webservice data {1}: {0}", ex.Message, club.ClubItm.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEnquiryDataSent;
        }

    }

}