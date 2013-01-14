using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Globalization;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Diagnostics;
using mm.virginactive.common.Helpers;
using System.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using Sitecore.Collections;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal; 


namespace mm.virginactive.web.layouts.virginactive
{
    public partial class ContactForm : System.Web.UI.UserControl
    {

        protected ContactFormItem currentItem = new ContactFormItem( Sitecore.Context.Item );
        protected List<FAQItem> faqItems = new List<FAQItem>();
        protected string ContactAddress = "";
        protected ManagerItem manager;
        protected ClubItem currentClub;


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
            ScriptManager1.RegisterAsyncPostBackControl(btnSubmit);

            //Check Children
            if (currentItem.InnerItem.HasChildren)
            {
                //Bind FAQ Child Items to List
                List<Item> faqsList = currentItem.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", FAQItem.TemplateId)).ToList();

                faqItems = faqsList.ConvertAll(x => new FAQItem(x));

                //We have a max number of FAQ items, remove anything over that number
                if (faqItems.Count > currentItem.Maxnumberoffaqs.Integer)
                {
                    faqItems.RemoveRange(currentItem.Maxnumberoffaqs.Integer - 1, faqItems.Count - currentItem.Maxnumberoffaqs.Integer);
                }

                FaqList.DataSource = faqItems;
                FaqList.DataBind();
            }

            //Format Contact Address
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

            //no link through -just show heading
            markupBuilder.Append(currentItem.Addressline1.Rendered != "" ? currentItem.Addressline1.Rendered + "<br/>" : "");
            markupBuilder.Append(currentItem.Addressline2.Rendered != "" ? currentItem.Addressline2.Rendered + "<br/>" : "");
            markupBuilder.Append(currentItem.Addressline3.Rendered != "" ? currentItem.Addressline3.Rendered + "<br/>" : "");
            markupBuilder.Append(currentItem.Addressline4.Rendered != "" ? currentItem.Addressline4.Rendered + "<br/>" : "");
            markupBuilder.Append(currentItem.Postcode.Rendered);

            ContactAddress = markupBuilder.ToString();

            string val = Translate.Text("Please enter {0}");
            btnSubmit.Text = Translate.Text("Submit");
            rfvName.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a name")));
            revName.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid name")));
            rfvComments.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a comment")));
            rfvPhone.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a telephone number")));
            revPhone.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid telephone no")));
            rfvEmail.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("an email address")));
            revEmail.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid email address")));
            rfvClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
            revClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
            cvClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));

            revPhone.ValidationExpression = Settings.PhoneNumberRegularExpression;
            revName.ValidationExpression = Settings.NameRegularExpression;
            revEmail.ValidationExpression = Settings.EmailAddressRegularExpression;
            revClubName.ValidationExpression = "^((?!" + Translate.Text("Select a club") + ").)*$";

      
            //if (currentItem.InnerItem.HasChildren)
            //{

            //    faqItems = currentItem.InnerItem.GetChildren().ToList().ConvertAll(x => new FAQItem(x));

            //    //We have a max number of FAQ items, remove anything over that number
            //    if (faqItems.Count > currentItem.Maxnumberoffaqs.Integer)
            //    {
            //        faqItems.RemoveRange(currentItem.Maxnumberoffaqs.Integer - 1, faqItems.Count - currentItem.Maxnumberoffaqs.Integer);
            //    }

            //    FaqList.DataSource = faqItems;
            //    FaqList.DataBind();
            //}

            //Field hightlighting extra code

            if (!Page.IsPostBack)
            {
                this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
	            $(function(){
	                $.va_init.functions.setupForms();
                    ViewClubContactDetails();
                    SetClubContactDetails();
	                $.va_init.functions.setupAccordions();
	            });
                </script>"));


                BindDropDownLists();
            }


        }

        private void BindDropDownLists()
        {
            try
            {
                // Populate Query dropdown
                this.drpQueryTypeList.Items.Clear();

                //Need to store these in config as can not change if using VA webservice
                ListItem itm = new ListItem(Translate.Text("Select"), Translate.Text("Select"));
                itm.Selected = true;
                drpQueryTypeList.Items.Add(itm);

                itm = new ListItem(Settings.FeedbackTypeComplaint, Settings.FeedbackTypeComplaint);
                drpQueryTypeList.Items.Add(itm);

                itm = new ListItem(Settings.FeedbackTypeGeneralComment, Settings.FeedbackTypeGeneralComment);
                drpQueryTypeList.Items.Add(itm);

                itm = new ListItem(Settings.FeedbackTypeSayWellDone, Settings.FeedbackTypeSayWellDone);
                drpQueryTypeList.Items.Add(itm);



                //Populate the club selector
                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                ChildList clubLst = clubRoot.Children;
                Item[] clubs = clubLst.ToArray(); 

                //Populate the club selector
                List<ClubItem> clubList = clubs.ToList().ConvertAll(X => new ClubItem(X));
                clubList.RemoveAll(x => x.IsHiddenFromMenu());
                clubList.RemoveAll(x => x.IsPlaceholder.Checked && !x.ShowInClubSelect.Checked);

                /*
                //Sort clubs alphabetically
                clubList.Sort(delegate(ClubItem c1, ClubItem c2)
                {
                    return c1.Clubname.Raw.CompareTo(c2.Clubname.Raw);

                }); */

                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));
                clubFindSelect_Top.Items.Add(new ListItem(Translate.Text("Select a club"), ""));
                foreach (Item club in clubList)
                {
                    ClubItem clubItem = new ClubItem(club);
                    if (clubItem != null)
                    {

                        bool isSelected = false;

                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            ListItem lst = new ListItem(clubLabel, clubItem.ID.ToString());
                            lst.Attributes.Add("data-email", clubItem.Salesemail.Raw);
                            clubFindSelect.Items.Add(lst);
                            clubFindSelect_Top.Items.Add(lst);

                            if (isSelected)
                            {
                                clubFindSelect.SelectedValue = clubItem.ID.ToString();
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error binding data {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //check if we have a valid club selected
                Boolean blnClubSuccessfullySelected = clubFindSelect.SelectedIndex != 0;

                if (blnClubSuccessfullySelected == true)
                {
                    currentClub = (ClubItem)Sitecore.Context.Database.GetItem(clubFindSelect.SelectedValue);				

                    if (currentClub != null)
                    {
                        //get managers info                        
                        List<ManagerItem> staffMembers = null;
                        if (currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                        {
                            staffMembers = currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));

                            manager = staffMembers.First();
                        }

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

                        string classNames = formToComplete.Attributes["class"];
                        if (classNames.IndexOf(" hidden") == -1)
                        {
                            formToComplete.Attributes.Add("class", classNames + " hidden submitted");
                        }

                        classNames = formCompleted.Attributes["class"];
                        if (classNames.IndexOf(" hidden") != -1)
                        {
                            formToComplete.Attributes.Add("class", classNames.Replace(" hidden", ""));
                        }

                        //formCompleted.Attributes.Add("style", "display:block;");
                        //formToComplete.Visible = false;
                        formCompleted.Visible = true;

                        pnlForm.Update();
                    }
                }
                else
                {
                    cvClubName.IsValid = false;
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
                objCustomer.TelephoneNumber = txtPhone.Value;
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = "Contact Us Enquiry";
                objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
                objFeedback.FeedbackTypeID = Constants.FeedbackType.ContactUs;
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
                if (dal.SaveFeedback(Context.User.Identity.Name, currentItem.DisplayName, objFeedback) > 0)
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

        private Boolean SendFeedbackDataService()
        {
            bool blnEnquiryDataSent = false;

            try
            {
                mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();

                string CommentType = Translate.Text("General Enquiry") + "- ";
                CommentType += drpQueryTypeList.SelectedValue.ToString() != Translate.Text("Select") ? drpQueryTypeList.SelectedValue.ToString() + ": " : "";

                string WebserviceCommentType = drpQueryTypeList.SelectedValue.ToString() != Translate.Text("Select") ? drpQueryTypeList.SelectedValue.ToString(): Settings.FeedbackTypeGeneralComment;

                string FirstName = "";
                string Surname = "";

                if (txtName.Value.IndexOf(" ") != -1)
                {
                    FirstName= txtName.Value.Substring(0, txtName.Value.IndexOf(" ")).Trim();
                    Surname = txtName.Value.Substring(txtName.Value.IndexOf(" ") + 1).Trim();
                }
                else
                {
                    FirstName = txtName.Value.Trim();
                }

                virginActiveService.InsertMemberFeedback(
                    FirstName,
                    Surname,
                    FormHelper.IsMobilePhoneNumber(txtPhone.Value.Trim()) ? "" : txtPhone.Value.Trim(),
                    FormHelper.IsMobilePhoneNumber(txtPhone.Value.Trim()) ? txtPhone.Value.Trim() : "",
                    txtEmail.Value.Trim(),
                    WebserviceCommentType, //comment type string
                    Convert.ToInt32(Settings.FeedbackAboutIDGeneral), //comment about id
                    currentClub.ClubId.Rendered,
                    FormHelper.IsValidMembershipNumber(txtMembership.Value.Trim()) ? "Yes" : "No",
                    txtMembership.Value.Trim(),
                    CommentType + txtComments.Value.Trim(),
                    chkConsentToPublish.Checked ? "Yes" : "No",
                    "", //preferred contact
                    false //newsletter
                    );

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }

                //var test = virginActiveService.ReadMemberFeedback();
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending contact us general member feedback webservice data: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEnquiryDataSent;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.ContactUsFeedbackConfirmation;
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

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.ContactUsFeedbackConfirmation), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.ContactUsFeedbackConfirmation)).Emailhtml.Text);
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
                string strEmailSubject = EmailSubjects.ClubFeedbackFormAdmin;
                string strEmailFromAddress = Settings.WebsiteMailFromText;

                string strEmailToAddress = "";
                if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                {
                    //Use test
                    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                }
                else
                {
                    if (currentClub.Feedbackemail.Rendered.Trim() != "")
                    {
                        strEmailToAddress = currentClub.Feedbackemail.Rendered.Trim();
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

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.ClubFeedbackFormAdmin), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.ClubFeedbackFormAdmin)).Emailhtml.Text);
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
                Log.Error(String.Format("Error sending contact us data email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }
    }
}