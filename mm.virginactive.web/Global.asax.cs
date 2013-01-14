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
using System.Text;
using System.Net.Mail; 

namespace mm.virginactive.web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    // dont email error if HttpException or local copy
        //    //if (Server.GetLastError().GetType() != typeof(HttpException) && Request.Url.Host.ToLower() != "localhost")
        //    if (Request.Url.Host.ToLower() != "localhost")
        //    {
        //        Exception ex = Context.Error.GetBaseException();
               
               
        //    try
        //    {
        //        string strEmailSubject = EmailSubjects.CorporateEnquiryConfirmation;
        //        string strEmailFromAddress = Settings.WebsiteMailFromText;
        //        string strEmailToAddress = txtEmail.Value.Trim();

        //        //Populate email text variables
        //        Hashtable objTemplateVariables = new Hashtable();
        //        objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
        //        objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesAndClasses));
        //        objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealth));
        //        objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.Memberships));
        //        objTemplateVariables.Add("PrivacyPolicyLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy));
        //        objTemplateVariables.Add("TermsAndConditionsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions));
        //        objTemplateVariables.Add("Copyright", Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved."));
        //        objTemplateVariables.Add("CustomerName", txtName.Value.Trim());
        //        if (manager != null)
        //        {
        //            objTemplateVariables.Add("ClubManagerName", manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered);
        //        }
        //        else
        //        {
        //            objTemplateVariables.Add("ClubManagerName", "");
        //        }
        //        objTemplateVariables.Add("ClubName", currentClub.Clubname.Rendered);
        //        objTemplateVariables.Add("EmailSubject", strEmailSubject);
        //        objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
        //        objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
        //        objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
        //        objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
        //        objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
        //        objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
        //        objTemplateVariables.Add("ImageRootUrl", WebUtil.GetServerUrl());
        //        objTemplateVariables.Add("ClubPhoneNumber", currentClub.Salestelephonenumber.Rendered);

        //        System.Text.StringBuilder markupBuilder;
        //        markupBuilder = new System.Text.StringBuilder();

        //        markupBuilder.Append(currentClub.Addressline1.Rendered);
        //        markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline2.Rendered) ? "<br />" + currentClub.Addressline2.Rendered : "");
        //        markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline3.Rendered) ? "<br />" + currentClub.Addressline3.Rendered : "");
        //        markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline4.Rendered) ? "<br />" + currentClub.Addressline4.Rendered : "");
        //        markupBuilder.Append("<br />" + currentClub.Postcode.Rendered);

        //        objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());

        //        //Parser objParser = new Parser(Server.MapPath(EmailTemplates.CorporateEnquiryConfirmation), objTemplateVariables);
        //        Parser objParser = new Parser(objTemplateVariables);
        //        objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.CorporateEnquiryConfirmation)).Emailhtml.Text);
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

           
                
         
        //}
        //        try
        //        {
        //            smtp.Send(msg);
        //        }
        //        catch (Exception expt) { }
        //    }
        //    //Server.ClearError();
        //}

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                HttpContext ctx = HttpContext.Current;

                Exception exception = ctx.Server.GetLastError();

                if (exception is ArgumentOutOfRangeException)
                {
                    // don't report these exceptions - they occur frequently in Sitecore when errorneous URLs are processed
                    return;
                }

                string errorEmail = Settings.SystemsErrorToMailNotification; // read the "To Email address" from config
                string strEmailToAddress = errorEmail;
                string strEmailFromAddress = Settings.SystemsErrorFromMailNotification;
                string strEmailSubject = EmailSubjects.AnyErrorNotification;

                if (!string.IsNullOrEmpty(strEmailToAddress))
                {
                    StringBuilder body = new StringBuilder();

                    body.Append("<b>Absolute Uri:</b><br />");
                    body.Append(string.Concat(Request.Url.AbsoluteUri, "<br />"));

                    body.Append("<b>Path and Query:</b><br />");
                    body.Append(string.Concat(Request.Url.PathAndQuery, "<br />"));

                    body.Append("<b>Sitecore Raw Url:</b><br />");
                    body.Append(string.Concat(Sitecore.Context.RawUrl, "<br />"));

                    body.Append("<b>Url:</b><br />");
                    body.Append(string.Format("{0}://{1}{2}<br />", Request.Url.Scheme, Request.Url.Host, Sitecore.Context.RawUrl));

                    body.Append("<b>User:</b><br />");
                    body.Append(Sitecore.Context.User.Name);

                    body.Append("<br />");

                    do
                    {
                        body.Append("<b>Message:</b><br />");
                        body.Append(string.Concat(exception.Message, "<br />"));
                        body.Append("<b>Stack Trace:</b><br />");
                        body.Append(string.Concat(exception.StackTrace, "<br />"));
                        body.Append("<b>Source:</b><br />");
                        body.Append(string.Concat(exception.Source, "<br /><br />"));
                        exception = exception.InnerException;
                    } while (exception != null);

                    string strMessageBody = Convert.ToString(body);

                    mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                    mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                    string strAttachments = "";

                    //now call messaging service
                    //objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strMessageBody, strAttachments);

                    if (objSendResult.Success == false)
                    {
                        Log.Error(String.Format("Error sending notification email"), this);
                    }

                    using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(strEmailFromAddress, strEmailToAddress))
                    {
                        message.Subject = strEmailSubject;
                        message.IsBodyHtml = true;
                        message.Priority = MailPriority.High;
                        message.Body = strMessageBody;
                        Sitecore.MainUtil.SendMail(message);
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}