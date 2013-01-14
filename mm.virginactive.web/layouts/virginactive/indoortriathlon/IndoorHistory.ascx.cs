using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using System.Text;
using System.Collections;
using mm.virginactive.common.Helpers;
using Sitecore.Diagnostics;
using mm.virginactive.screportdal;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.common.Globalization;
using Sitecore.Links;
using Sitecore.Resources.Media;
using mm.virginactive.controls.Model;

namespace mm.virginactive.web.layouts.virginactive.indoortriathlon
{
    public partial class IndoorHistory : System.Web.UI.UserControl
    {
        protected IndoorHistoryItem currentItem = new IndoorHistoryItem(Sitecore.Context.Item);
        //protected IndoorFormItem registrationForm;
        protected LinkWidgetItem registrationForm;
        protected string pageUrl;
        protected bool showSocial = false;

        public Boolean ShowSocial
        {
            get { return showSocial; }
            set
            {
                showSocial = value;
            }
        }

        public string PageUrl
        {
            get { return pageUrl; }
            set
            {
                pageUrl = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            //Get widget details
            registrationForm = Sitecore.Context.Database.GetItem(ItemPaths.IndoorTriathlonRegistrationForm);	

            //Add dynamic content
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/indoortriathlon/indoor-tri.js""></script>"));

            if (Session["sess_User"] != null)
            {
                User objUser = (User)Session["sess_User"];

                if (objUser.Preferences.SocialCookies)
                {

                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = true;
                    urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                    pageUrl = Sitecore.Links.LinkManager.GetItemUrl(currentItem, urlOptions);

                    //Have permission to load in Social
                    showSocial = true;
                }
            }

            if (currentItem.InnerItem.HasChildren)
            {

                //Check if we have Testimonials
                if (currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", TestimonialItem.TemplateId)) != null)
                {
                    List<TestimonialItem> linkList = currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", TestimonialItem.TemplateId)).ToList().ConvertAll(Y => new TestimonialItem(Y));

                    if (linkList.Count > 0)
                    {
                        Testimonials.DataSource = linkList;
                        Testimonials.DataBind();
                    }
                }


                //Check if we have Images
                if (currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", ImageCarouselItem.TemplateId)) != null)
                {
                    List<ImageCarouselItem> linkList = currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", ImageCarouselItem.TemplateId)).ToList().ConvertAll(Y => new ImageCarouselItem(Y));

                    if (linkList.Count > 0)
                    {
                        OverlayImages.DataSource = linkList;
                        OverlayImages.DataBind();
                    }

                    System.Text.StringBuilder markupBuilder;

                    markupBuilder = new System.Text.StringBuilder();
                    
                   // markupBuilder.Append("<li>");
					
                    int i = 0;
                    bool firstItem = true;

                    foreach(ImageCarouselItem item in linkList)
                    {
                    
                        if (!string.IsNullOrEmpty(currentItem.Imagesperpage.Raw))
                        {
                           
	                        if (i % Convert.ToInt32(currentItem.Imagesperpage.Raw) == 0)
	                        {
	                        	if(firstItem){
	                        		markupBuilder.Append("<li>");
	                        		firstItem = false;
	                        	}
	                        	else
	                        	{
	                            	markupBuilder.Append("</li><li>");
	                            }
	                        }
                            
                        }

                        markupBuilder.Append(@"<a href=""#overlay-" + i + @""">" + item.Image.RenderCrop("80x80") + @"</a>");
                        i++;
                    }

                    markupBuilder.Append("</li>");

                    ltrThumbnailItems.Text = markupBuilder.ToString();
                        
                }
            }

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