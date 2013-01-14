using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Collections;
using Sitecore.Data.Items;
using Sitecore.Web;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubKidsFeedback : System.Web.UI.UserControl
    {
        protected FeedbackItem feedbackItem = new FeedbackItem(Sitecore.Context.Item);
        protected ManagerItem branchManager;
        protected KidsStaffItem manager;    

        protected ClubItem currentClub;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDropDownLists();

            try
            {
                currentClub = SitecoreHelper.GetCurrentClub(feedbackItem.InnerItem);

                //Find Club Kids manager (first person in the list of KidsStaff
                try
                {
                    manager = new KidsStaffItem(feedbackItem.InnerItem.Axes.SelectItems(String.Format("../descendant::*[@@tid='{0}']", KidsStaffItem.TemplateId))[0]);
                    branchManager = new ManagerItem(feedbackItem.InnerItem.Axes.SelectItems(String.Format("../descendant::*[@@tid='{0}']", ManagerItem.TemplateId))[0]);
                    if (manager != null)
                    {
                        ManagerPanel.Visible = true;
                    }
                }
                catch (Exception exe)
                {
                    Log.Error(String.Format("Could not fetch a club manager for club {1}: {0}", exe.Message, currentClub.Clubname.Raw), this);
                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(exe);
                }

                string val = Translate.Text("Please enter {0}");
                rfvName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a name")));
                revName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid name")));
                rfvEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("an email address")));
                revEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid email address")));
                rfvPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a phone number")));
                revPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid phone number")));
                rfvComments.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("your comments")));
                revEmail.ValidationExpression = Settings.EmailAddressRegularExpression;
                revPhone.ValidationExpression = Settings.PhoneNumberRegularExpression;
                revName.ValidationExpression = Settings.NameRegularExpression;

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error generating Club feedback form items: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
	            $(function(){
	                $.va_init.functions.setupForms();
	            });
                </script>"));
        }

        private void BindDropDownLists()
        {
            try
            {
                // Populate Query dropdown
                this.drpQueryTypeList.Items.Clear();

                string queryItemsString = feedbackItem.Kidsquerytypes.Raw.Replace("\r\n", ",");
                queryItemsString = queryItemsString.Replace("\n", ",");
                queryItemsString = queryItemsString.TrimEnd(',');
                string[] queryItems = queryItemsString.Split(',');

                int counter = 0;
                foreach (string item in queryItems)
                {
                    counter++;
                    ListItem itm = new ListItem(item, item);
                    itm.Selected = (counter == 1);
                    drpQueryTypeList.Items.Add(itm);
                }
                //this.drpQueryTypeList.Items.Insert(0, " ");
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending data email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid == true)
                {
                    if (currentClub.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric)
                    {
                        //TODO: Log data to database
                        //Data is sent to client via email
                        //SendAdminEmail();
                    }
                    else
                    {
                        //TODO: Log data to database
                        //Data is sent to client via email
                        SendFeedbackDataService();
                    }

                    //Data is sent to client via email
                    SendAdminEmail();

                    //Save feedback to report DB
                    SaveDataToReportDB();

                    //Send confirmation email
                    SendConfirmationEmail();

                    formToComplete.Visible = false;
                    formCompleted.Visible = true;
                    pnlForm.Update();
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending kids section feedback confirmation {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        private Boolean SendFeedbackDataService()
        {
            bool blnEnquiryDataSent = false;


            try
            {
                string CommentType = Translate.Text("Kids Club Enquiry") + "- ";
                CommentType += drpQueryTypeList.SelectedValue.ToString() != Translate.Text("Select") ? drpQueryTypeList.SelectedValue.ToString() + ": " : "";

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

                mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();

                virginActiveService.InsertMemberFeedback(
                    FirstName,
                    Surname,
                    FormHelper.IsMobilePhoneNumber(txtPhone.Value.Trim()) ? "" : txtPhone.Value.Trim(),
                    FormHelper.IsMobilePhoneNumber(txtPhone.Value.Trim()) ? txtPhone.Value.Trim() : "",
                    txtEmail.Value.Trim(),
                    //Set as a 'General' query type
                    Settings.FeedbackTypeGeneralComment.ToString(), //comment type string
                    Convert.ToInt32(Settings.FeedbackAboutIDGeneral), //comment about id
                    currentClub.ClubId.Rendered,
                    FormHelper.IsValidMembershipNumber(txtMembership.Value.Trim()) ? "Yes" : "No",
                    txtMembership.Value.Trim(),
                    CommentType + txtComments.Value.Trim(),
                    chkConsentToPublish.Checked ? "Yes" : "No",
                    "", //preferred contact
                    false //newsletter
                    );

                //var test = virginActiveService.ReadMemberFeedback();

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending kids club feedback webserivce data {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEnquiryDataSent;
        }


        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.KidsClubFeedbackConfirmation;
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
                objTemplateVariables.Add("ImageRootUrl", WebUtil.GetServerUrl());
                objTemplateVariables.Add("ClubPhoneNumber", currentClub.Salestelephonenumber.Rendered);

                System.Text.StringBuilder markupBuilder;
                markupBuilder = new System.Text.StringBuilder();

                markupBuilder.Append(currentClub.Addressline1.Rendered);
                markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline2.Rendered) ? "<br />" + currentClub.Addressline2.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline3.Rendered) ? "<br />" + currentClub.Addressline3.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(currentClub.Addressline4.Rendered) ? "<br />" + currentClub.Addressline4.Rendered : "");
                markupBuilder.Append("<br />" + currentClub.Postcode.Rendered);

                objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());

                //Find Club Kids manager (first person in the list of KidsStaff
                try
                {
                    manager = new KidsStaffItem(feedbackItem.InnerItem.Axes.SelectItems(String.Format("../descendant::*[@@tid='{0}']", KidsStaffItem.TemplateId))[0]);
                    if (manager != null)
                    {
                        objTemplateVariables.Add("KidsClubManagerName", manager.Person.GetFullName());        
                    }
                }
                catch (Exception exe)
                {
                    Log.Error(String.Format("Could not fetch a club manager for club {1}: {0}", exe.Message, currentClub.Clubname.Raw), this);
                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(exe);
                }

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.KidsClubFeedbackConfirmation), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.KidsClubFeedbackConfirmation)).Emailhtml.Text);
                string strMessageBody = objParser.Parse();

                mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                string strAttachments = "";

                //now call messaging service
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject,
                    true, strMessageBody, strAttachments);

                if (objSendResult.Success == true)
                {
                    blnEmailSent = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending confirmation email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);

            }

            return blnEmailSent;
        }

        private Boolean SaveDataToReportDB()
        {
            bool blnDataSaved = false;


            try
            {
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

                //Store Feedback Entitiy Details
                Feedback objFeedback = new Feedback();

                //Store Customer Entitiy Details
                Customer objCustomer = new Customer();
                objCustomer.EmailAddress = txtEmail.Value.Trim();
                objCustomer.Firstname = FirstName;
                objCustomer.Surname = Surname;
                objCustomer.MembershipNumber = txtMembership.Value.Trim();
                objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                objCustomer.PublishDetails = chkConsentToPublish.Checked;
                objCustomer.TelephoneNumber = txtPhone.Value.Trim();
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = "Kids Club Feedback";
                objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
                objFeedback.FeedbackTypeID = Constants.FeedbackType.KidsFeedback;
                objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                objFeedback.SubmissionDate = DateTime.Now;

                //Add Comment
                Comment objComment = new Comment();

                objComment.CommentDetail = drpQueryTypeList.SelectedValue.ToString() != Translate.Text("Select") ? drpQueryTypeList.SelectedValue.ToString() : "";
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 1;
                objComment.Subject = "Query Type";

                List<Comment> objComments = new List<Comment>();
                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtComments.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.GeneralComment;
                objComment.SortOrder = 1;
                objComment.Subject = "Comments";

                objComments.Add(objComment);
                objFeedback.Comments = objComments;

                string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                DataAccessLayer dal = new DataAccessLayer(connection);
                if (dal.SaveFeedback(Context.User.Identity.Name, feedbackItem.DisplayName, objFeedback) > 0)
                {
                    blnDataSaved = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error saving form data to reports db {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnDataSaved;
        }

        private Boolean SendAdminEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.KidsClubFeedbackFormAdmin;
                string strEmailFromAddress = Settings.WebsiteMailFromText;

                string strEmailToAddress = "";
                if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                {
                    //Use test
                    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                }
                else
                {
                    if (currentClub.Kidsfeedbackemail.Rendered.Trim() != "")
                    {
                        strEmailToAddress = currentClub.Kidsfeedbackemail.Rendered.Trim();
                    }
                    else if (currentClub.Salesemail.Rendered.Trim() != "")
                    {
                        strEmailToAddress = currentClub.Salesemail.Rendered.Trim();
                    }
                    else
                    {
                        strEmailToAddress = Settings.DefaultFormToEmailAddress;
                        //TODO: Send warning alert.
                        Log.Error(String.Format("Could not find sales email address for club {0}", currentClub.Clubname.Raw), this);
                    }
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("CustomerName", txtName.Value.Trim());
                objTemplateVariables.Add("CustomerEmail", txtEmail.Value.Trim());
                objTemplateVariables.Add("MembershipNo", txtMembership.Value.Trim());
                objTemplateVariables.Add("Telephone", txtPhone.Value.Trim());
                objTemplateVariables.Add("QueryType", drpQueryTypeList.SelectedValue.ToString() != Translate.Text("Select") ? drpQueryTypeList.SelectedValue.ToString() : "");
                objTemplateVariables.Add("Comments", txtComments.Value.Trim());
                objTemplateVariables.Add("ClubName", currentClub.Clubname.Rendered);
                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.KidsClubFeedbackFormAdmin), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.KidsClubFeedbackFormAdmin)).Emailhtml.Text);
                string strMessageBody = objParser.Parse();

                mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                string strAttachments = "";

                //now call messaging service
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strMessageBody, strAttachments);

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }

                if (objSendResult.Success == true)
                {
                    blnEmailSent = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending kids club feedback data email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }
    }
}