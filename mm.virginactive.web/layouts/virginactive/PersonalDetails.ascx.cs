using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using Sitecore.Collections;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Globalization;
using Sitecore.Diagnostics;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Collections;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using Newtonsoft.Json;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using mm.virginactive.web.layouts.virginactive.presentationstructures; 

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class PersonalDetails : System.Web.UI.UserControl
    {
        protected PersonalDetailsItem currentItem = new PersonalDetailsItem(Sitecore.Context.Item);
        protected string thisUrl = "";
        //protected List<FAQItem> faqItems = new List<FAQItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Populate drop downs

            //generate the image carousel
            List<ImageCarouselItem> imgCar = new List<ImageCarouselItem>();
            if (currentItem.InnerItem.HasChildren)
            {
                imgCar = currentItem.InnerItem.Children.ToList().ConvertAll(X => new ImageCarouselItem(X));
                ImageList.DataSource = imgCar;
                ImageList.DataBind();
            }

            if (!Page.IsPostBack)
            {
                pnlPanel1.Attributes.Add("style", "display:block;");
                BindDropDownLists();
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#thanksanchor';", true);
            }

            thisUrl = currentItem.PageSummary.QualifiedUrl;

            //Add dynamic content to header
            HtmlHead head = (HtmlHead)Page.Header;

            //Add page specific css
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
            markupBuilder.Append(@"<link href='/virginactive/css/membership/membership.css' rel='stylesheet' type='text/css' media='screen' />");
            head.Controls.Add(new LiteralControl(markupBuilder.ToString()));

            //Add page specific javascript
            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"
                <script src='/virginactive/scripts/membership/membership-plugins.js' type='text/javascript'></script>
                <script src='/virginactive/scripts/membership/validation.js' type='text/javascript'></script>
                <script src='/virginactive/scripts/membership/membership.js' type='text/javascript'></script>
                "));
            }
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		        $(function(){
	                $.va_init.functions.setupImageRotator();
                });
            </script>"));

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                string json = hdnReturnedData.Value;
                //check if values have changed

                JArray panel2Data = JArray.Parse(json);

                //Check if change has been made
                bool blnChangeDetected = false;

                if ((string)panel2Data[0]["firstName"] != txtFirstName.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["surname"] != txtSurname.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["email"] != txtEmail.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["homePhone"] != txtHomeNo.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["workPhone"] != txtWorkNo.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["mobilePhone"] != txtMobileNo.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["address1"] != txtAddress1.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["address2"] != txtAddress2.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["address3"] != txtAddress3.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["address4"] != txtAddress4.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["address5"] != txtAddress5.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["postcode"] != txtPostcode.Value.Trim())
                { blnChangeDetected = true; }
                if ((string)panel2Data[0]["marketingByMail"] != (chkContactByMarketing.Checked ? "True" : "False"))
                { blnChangeDetected = true; }


                bool blnIsValid = true;

                if (blnIsValid == true)
                {
                    //Send data back via webservice
                    if (blnChangeDetected)
                    {
                        SaveMemberDetailsService();
                    }

                    //Data is sent to client via email
                    SendAdminEmail();

                    //Save feedback to report DB
                    SaveDataToReportDB();

                    //Send confirmation email
                    //SendConfirmationEmail();

                    pnlIntroPanel.Attributes.Add("style", "display:none;");
                    pnlPanel1.Attributes.Add("style", "display:none;");
                    pnlConfirmation.Attributes.Add("style", "display:none;");
                    pnlThanks.Attributes.Add("style", "display:block;");

                    Literal centralLayout = (Literal)this.Parent.FindControl("layoutHeader");
                    /*if (centralLayout!= null)
                    {
                        centralLayout.Text = Translate.Text("Thanks!");
                    }*/


                    //pnlForm.Update();
                }
                else
                {
                    //cvClubName.IsValid = false;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending contact us email confirmation {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

        }

        private Boolean SaveDataToReportDB()
        {
            bool blnDataSaved = false;

            try
            {
                //Store Feedback Entitiy Details
                Feedback objFeedback = new Feedback();

                //Store Customer Entitiy Details
                Customer objCustomer = new Customer();
                objCustomer.EmailAddress = txtEmail.Value.Trim();
                objCustomer.Firstname = txtFirstName.Value.Trim();
                objCustomer.Surname = txtSurname.Value.Trim();
                objCustomer.MembershipNumber = txtMembership.Value.Trim();
                objCustomer.HomeClubID = "";

                DateTime datDateOfBirth; 
                if (DateTime.TryParse(drpDOBDay.SelectedValue + "/" + drpDOBMonth.SelectedValue + "/" + drpDOBYear.SelectedValue, out datDateOfBirth))
                {
                    objCustomer.DateOfBirth = datDateOfBirth;
                }
                objCustomer.AddressLine1 = txtAddress1.Value.Trim();
                objCustomer.AddressLine2 = txtAddress2.Value.Trim();
                objCustomer.AddressLine3 = txtAddress3.Value.Trim();
                objCustomer.AddressLine4 = (txtAddress4.Value.Trim() + " " + txtAddress5.Value.Trim()).Trim();
                objCustomer.Postcode = txtPostcode2.Value.Trim();
                //objCustomer.PublishDetails = false;
                //objCustomer.SubscribeToNewsletter = false;
                objCustomer.TelephoneNumber = txtHomeNo.Value.Trim();
                objCustomer.AlternativeTelephoneNumber = txtMobileNo.Value.Trim();
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = "Personal Details Update";
                objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
                objFeedback.FeedbackTypeID = Constants.FeedbackType.PersonalDetails;
                objFeedback.PrimaryClubID = "";
                objFeedback.SubmissionDate = DateTime.Now;

                //Add Comment
                Comment objComment = new Comment();

                objComment.CommentDetail = hdnRecordId.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 1;
                objComment.Subject = "Record ID";

                List<Comment> objComments = new List<Comment>();
                objComments.Add(objComment);

                //Add Comment
                objComment = new Comment();

                objComment.CommentDetail = txtHomeNo.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 2;
                objComment.Subject = "Home Telephone No.";

                objComments.Add(objComment);

                //Add Comment
                objComment = new Comment();

                objComment.CommentDetail = txtMobileNo.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 3;
                objComment.Subject = "Mobile Telephone No.";

                objComments.Add(objComment);

                //Add Comment
                objComment = new Comment();

                objComment.CommentDetail = txtWorkNo.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 4;
                objComment.Subject = "Work Telephone No.";

                objComments.Add(objComment);

                //Add Comment
                objComment = new Comment();

                objComment.CommentDetail = chkContactByMarketing.Checked == true ? "Y" : "N";
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 5;
                objComment.Subject = "Contact By Marketing";
                
                objComments.Add(objComment);

                objFeedback.Comments = objComments;

                string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                DataAccessLayer dal = new DataAccessLayer(connection);
                if (dal.SaveFeedback(Context.User.Identity.Name, currentItem.DisplayName, objFeedback) > 0)
                {
                    blnDataSaved = true;
                }


            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error saving form data to reports db: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnDataSaved;
        }

        private Boolean SaveMemberDetailsService()
        {
            bool blnEnquiryDataSent = false;

            try
            {
                //Authenticate member on membership id, postcode and date of birth
                mm.virginactive.webservices.virginactive.memberdetails.MemberDetails memberService = new mm.virginactive.webservices.virginactive.memberdetails.MemberDetails();
                mm.virginactive.webservices.virginactive.memberdetails.MemberData member = new mm.virginactive.webservices.virginactive.memberdetails.MemberData();

                member.MemberAddress1 = txtAddress1.Value.Trim();
                member.MemberAddress2 = txtAddress2.Value.Trim();
                member.MemberAddress3 = txtAddress3.Value.Trim();
                member.MemberAddress4 = txtAddress4.Value.Trim();
                member.MemberAddress5 = txtAddress5.Value.Trim();
                member.MemberPostCode = txtPostcode.Value.Trim();

                member.MemberEmail = txtEmail.Value.Trim();

                member.MemberFirstName = txtFirstName.Value.Trim();
                member.MemberSurname = txtSurname.Value.Trim();

                member.MemberHomePhone = txtHomeNo.Value.Trim();
                member.MemberMobilePhone = txtMobileNo.Value.Trim();                
                member.MemberWorkPhone = txtWorkNo.Value.Trim();

                member.PrefMarketingByMail = chkContactByMarketing.Checked;
                member.RecordID = Convert.ToInt32(hdnRecordId.Value.Trim());

                memberService.SaveMemberDetails(
                    member.RecordID, //hdnRecordID.Value.Trim(), //recordid
                    member.MemberFirstName, //firstname
                    member.MemberSurname, //surname,
                    member.MemberAddress1, //address1,
                    member.MemberAddress2, //address2,
                    member.MemberAddress3, //address3,
                    member.MemberAddress4, //address4,
                    member.MemberAddress5, //address5,
                    member.MemberPostCode, //postCode,
                    member.MemberHomePhone, //homeNo,
                    member.MemberMobilePhone, //mobileNo,
                    member.MemberWorkPhone, //workNo,
                    member.MemberEmail, //email,
                    member.PrefMarketingByMail);
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending member member personal details webservice data: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEnquiryDataSent;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.PersonalDetailsConfirmation;
                string strEmailFromAddress = Settings.WebsiteMailFromText;
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
                objTemplateVariables.Add("CustomerName", (txtFirstName.Value.Trim() + " " + txtSurname.Value.Trim()).Trim());

                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.ContactUsFeedbackConfirmation), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.PersonalDetailsConfirmation)).Emailhtml.Text);
                string strMessageBody = objParser.Parse();

                mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                string strAttachments = "";

                //now call messaging service
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strMessageBody, strAttachments);

                if (objSendResult.Success == true)
                {
                    blnEmailSent = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending contact us email confirmation: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }

        private Boolean SendAdminEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.PersonalDetailsFormAdmin;
                string strEmailFromAddress = Settings.WebsiteMailFromText;

                string strEmailToAddress = "";
                if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                {
                    //Use test
                    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                }
                else
                {
                    //Use Email Address for per marketing campaigns
                    strEmailToAddress = Settings.FeedbackCampaignEmailToListShort;
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                //objTemplateVariables.Add("FeedbackSubject", "Personal Details Update");
                //objTemplateVariables.Add("FeedbackSubjectDetail", "");
                objTemplateVariables.Add("SubmissionDate", DateTime.Now.ToShortDateString());
                objTemplateVariables.Add("MemberRecordID", hdnRecordId.Value.Trim());
                objTemplateVariables.Add("CustomerName", (txtFirstName.Value.Trim() + " " + txtSurname.Value.Trim()).Trim());
                objTemplateVariables.Add("CustomerEmail", txtEmail.Value.Trim());
                objTemplateVariables.Add("MembershipNo", txtMembership.Value.Trim());

                DateTime datDateOfBirth;
                if (DateTime.TryParse(drpDOBDay.SelectedValue + "/" + drpDOBMonth.SelectedValue + "/" + drpDOBYear.SelectedValue, out datDateOfBirth))
                {
                    objTemplateVariables.Add("DateOfBirth", datDateOfBirth.ToString("dd/MM/yyyy"));
                }
                else
                {
                    objTemplateVariables.Add("DateOfBirth", "");
                }

                objTemplateVariables.Add("HomePhoneNo", txtHomeNo.Value.Trim());
                objTemplateVariables.Add("WorkPhoneNo", txtWorkNo.Value.Trim());
                objTemplateVariables.Add("MobilePhoneNo", txtMobileNo.Value.Trim());

                objTemplateVariables.Add("Address1", txtAddress1.Value.Trim());
                objTemplateVariables.Add("Address2", txtAddress2.Value.Trim());
                objTemplateVariables.Add("Address3", txtAddress3.Value.Trim());
                objTemplateVariables.Add("Address4", txtAddress4.Value.Trim());
                objTemplateVariables.Add("Address5", txtAddress5.Value.Trim());
                objTemplateVariables.Add("Postcode", txtPostcode.Value.Trim());

                objTemplateVariables.Add("ContactByMarketing", chkContactByMarketing.Checked == true ? "Y" : "N");

                objTemplateVariables.Add("SubscribeToNewsletter", "N/A");
                objTemplateVariables.Add("PublishDetails", "N/A");

                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.ClubFeedbackFormAdmin), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.PersonalDetailsFormAdmin)).Emailhtml.Text);
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
                Log.Error(String.Format("Error sending contact us data email: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }

        private void BindDropDownLists()
        {
            try
            {
                // Populate day dropdown
                this.drpDOBDay.Items.Clear();
                this.drpDOBDay.DataSource = FormHelper.ListDaysOfMonth();
                this.drpDOBDay.DataBind();

                this.drpDOBDay.Items.Insert(0, Translate.Text("dd"));

                // Populate month dropdown
                this.drpDOBMonth.Items.Clear();
                this.drpDOBMonth.DataSource = FormHelper.ListMonthsOfYear();
                this.drpDOBMonth.DataBind();

                this.drpDOBMonth.Items.Insert(0, Translate.Text("mm"));

                // Populate year dropdown
                this.drpDOBYear.Items.Clear();
                this.drpDOBYear.DataSource = FormHelper.ListDOBYears();
                this.drpDOBYear.DataBind();

                this.drpDOBYear.Items.Insert(0, Translate.Text("yyyy"));
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error binding drop down lists: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}