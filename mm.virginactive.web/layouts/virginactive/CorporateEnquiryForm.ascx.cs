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


namespace mm.virginactive.web.layouts.virginactive
{
    public partial class CorporateEnquiryForm : System.Web.UI.UserControl
    {

        protected ManagerItem manager;
        protected EnquiryFormItem enquiryItem = new EnquiryFormItem(Sitecore.Context.Item);
        protected List<ClubItem> currentClubs;
        protected ClubItem currentClub;
        protected List<string> clubs;
        protected string clubNames = "";

        public string ClubNames
        {
            get { return clubNames; }
            set
            {
                clubNames = value;
            }
        }

        public ClubItem CurrentClub
        {
            get { return currentClub; }
            set
            {
                currentClub = value;
            }
        }
        public List<ClubItem> CurrentClubs
        {
            get { return currentClubs; }
            set
            {
                currentClubs = value;
            }
        }
 
        protected void Page_Load(object sender, EventArgs e)
        {

            string val = Translate.Text("Please enter {0}");
            rfvClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
            revClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
            //cvClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
            rfvName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a name")));
            revName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid name")));
            rfvJobTitle.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a job title")));
            revJobTitle.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid job title")));
            rfvCompanyName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a company name")));
            //revCompanyName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid company name")));
            rfvEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("an email address")));
            revEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid email address")));
            rfvPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a contact number")));
            revPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid contact number")));
            rfvNoEmployees.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("number of employees")));
            rfvExistingMembers.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("no. of existing members")));

            cmpvNoEmployees.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("This is more than no. of employees"));

            revName.ValidationExpression = Settings.NameRegularExpression;
            revJobTitle.ValidationExpression = Settings.GeneralTextRegularExpression;
            //revCompanyName.ValidationExpression = Settings.GeneralTextRegularExpression;
            revEmail.ValidationExpression = Settings.EmailAddressRegularExpression;
            revPhone.ValidationExpression = Settings.PhoneNumberRegularExpression;
            revClubName.ValidationExpression = "^((?!" + Translate.Text("Select Clubs") + ").)*$";

            Assert.ArgumentNotNullOrEmpty(ItemPaths.Clubs, "Club root path not set");

            if (!Page.IsPostBack)
            {
                BindDropDownLists();
            }
            else
            {
                //Post Back -Grab club data from drop down lists
                clubs = new List<string>();
                int counter = 1;

                do
                {
                    clubs.Add(HttpContext.Current.Request.Form[String.Format("content_0$centre_0$ctl00$findclub{0}", counter.ToString())]);

                    counter++;
                }while(!string.IsNullOrEmpty(HttpContext.Current.Request.Form[String.Format("content_0$centre_0$ctl00$findclub{0}", counter.ToString())]));

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

                //Populate the club selector
                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                ChildList clubLst = clubRoot.Children;
                Item[] clubs = clubLst.ToArray(); 

                //Populate the club selector
                List<ClubItem> clubList = clubs.ToList().ConvertAll(X => new ClubItem(X));
                clubList.RemoveAll(x => x.IsHiddenFromMenu());
                clubList.RemoveAll(x => x.IsPlaceholder.Checked && !x.ShowInClubSelect.Checked);
                findclub1.Items.Add(new ListItem(Translate.Text("Select a club"), ""));  //Added default first option

                foreach (Item club in clubList)
                {
                    ClubItem clubItem = new ClubItem(club);
                    if (clubItem != null)
                    {

                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            ListItem lst = new ListItem(clubLabel, clubItem.ID.ToString());
                            lst.Attributes.Add("data-email", clubItem.Salesemail.Raw);
                            findclub1.Items.Add(lst);
                        }                       
                    }
                }

                // Populate No Employees dropdown
                //this.drpNoEmployees.Items.Clear();

                //string employeesItemsString = enquiryItem.Noofemployees.Raw.Replace("\r\n", ",");
                //employeesItemsString = employeesItemsString.Replace("\n", ",");
                //employeesItemsString = employeesItemsString.TrimEnd(',');
                //string[] employeesItems = employeesItemsString.Split(',');

                //foreach (string item in employeesItems)
                //{
                //    drpNoEmployees.Items.Add(new ListItem(item, item));
                //}
                //this.drpNoEmployees.Items.Insert(0, (Translate.Text("Select")));

                // Populate Existing Members dropdown
                //this.drpExistingMembers.Items.Clear();

                //string membersItemsString = enquiryItem.Existingmembers.Raw.Replace("\r\n", ",");
                //membersItemsString = membersItemsString.Replace("\n", ",");
                //membersItemsString = membersItemsString.TrimEnd(',');
                //string[] membersItems = membersItemsString.Split(',');

                //foreach (string item in membersItems)
                //{
                //    drpExistingMembers.Items.Add(new ListItem(item, item));
                //}
                //this.drpExistingMembers.Items.Insert(0, (Translate.Text("Select")));
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error binding drop down lists: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //check if we have a valid club selected
				Boolean blnClubSuccessfullySelected = clubs.Count > 0;

                if (blnClubSuccessfullySelected == true)
                {
                    //string[] clubs = txtClubGUID.Value.Split('|');
                    currentClubs = new List<ClubItem>();
                    foreach (string club in clubs)
                    {
                        if (club != "")
                        {
                            bool clubExists = false;
                            //Check for duplicates
                            foreach (ClubItem existingClubs in currentClubs)
                            {
                                if (existingClubs.ID.ToString() == club)
                                {
                                    clubExists = true;
                                }
                            }
                            if (clubExists == false)
                            {
                                currentClubs.Add((ClubItem)Sitecore.Context.Database.GetItem(Server.UrlDecode(club)));
                            }
                        }
                    }

                    if (currentClubs.Count > 0 && currentClubs[0] != null)
                    {
                        foreach (ClubItem club in currentClubs)
                        {
                            clubNames += HtmlRemoval.StripTagsCharArray(club.Clubname.Rendered).Trim() + ", ";
                        }
                        if (clubNames.LastIndexOf(",") != -1)
                        {
                            clubNames = clubNames.Substring(0, clubNames.LastIndexOf(","));
                        }

                        //set current club as first club in the collection
                        currentClub = currentClubs[0];
                        //get managers info
                        List<ManagerItem> staffMembers = null;
                        if (currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                        {
                            staffMembers = currentClub.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));
                            manager = staffMembers.First();
                        }
                        else
                        {
                            Log.Error(String.Format("No club manager exists for club: {0}", currentClub.Clubname.Raw), this);
                        }

                        //Send data to service
                        SendEnquiryDataService();

                        //Save feedback to report DB
                        SaveDataToReportDB();

                        //Send confirmation email
                        SendConfirmationEmail();

                        //get user session
                        User objUser = new User();
                        if (Session["sess_User"] != null)
                        {
                            objUser = (User)Session["sess_User"];
                        }

                        if (!string.IsNullOrEmpty(objUser.ClubLastVisitedID))
                        {
                            ClubItem club = SitecoreHelper.GetClubOnClubId(objUser.ClubLastVisitedID);
                            if (club != null)
                            {
                                //Set url 
                                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                                urlOptions.AlwaysIncludeServerUrl = true;
                                urlOptions.AddAspxExtension = true;
                                urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                                //Show club last visited link
                                System.Text.StringBuilder markupBuilder;
                                markupBuilder = new System.Text.StringBuilder();
                                markupBuilder.Append("<span>");
                                markupBuilder.Append(Translate.Text("You last visited") + " ");
                                markupBuilder.Append(@"<a href=""");
                                markupBuilder.Append(Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions));
                                markupBuilder.Append(@""" class=""wl"">");
                                markupBuilder.Append(club.Clubname.Rendered);
                                markupBuilder.Append("</a></span>");

                                //ltrClubLastVisitedLink.Text = markupBuilder.ToString();
                            }
                        }

                        formToComplete.Visible = false;
                        formCompleted.Visible = true;
                        pnlForm.Update();
                    }
                }
                else
                {
                    //cvClubName.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error processing club enquiry form {0}", ex.Message), this);
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
                objCustomer.JobTitle = txtJobTitle.Value.Trim();
                objCustomer.CompanyName = txtCompanyName.Value.Trim();
                objCustomer.CompanyWebsite = txtCompanyWebsite.Value.Trim();
                objCustomer.CompanyLocation = txtLocation.Value.Trim();
                //objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                //objCustomer.SubscribeToNewsletter = chkSubscribe.Checked;
                objCustomer.TelephoneNumber = txtPhone.Value;
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = "Corporate Enquiry";
                objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
                objFeedback.FeedbackTypeID = Constants.FeedbackType.CorporateEnquiry;
                //objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                objFeedback.SubmissionDate = DateTime.Now;

                //Add Comment
                Comment objComment = new Comment();

                objComment.CommentDetail = ClubNames;
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 1;
                objComment.Subject = "Clubs interested in";

                List<Comment> objComments = new List<Comment>();
                objComments.Add(objComment);

                objComment = new Comment();
                //objComment.CommentDetail = drpNoEmployees.SelectedValue.ToString() != Translate.Text("Select") ? drpNoEmployees.SelectedValue.ToString() : "";
                objComment.CommentDetail = txtNoEmployees.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 2;
                objComment.Subject = "No of Employees";

                objComments.Add(objComment);

                objComment = new Comment();
                //objComment.CommentDetail = drpExistingMembers.SelectedValue.ToString() != Translate.Text("Select") ? drpExistingMembers.SelectedValue.ToString() : "";
                objComment.CommentDetail = txtExistingMembers.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 3;
                objComment.Subject = "Existing Virgin Active members";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtComments.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.GeneralComment;
                objComment.SortOrder = 1;
                objComment.Subject = "Questions/Comments";

                objComments.Add(objComment);

                objFeedback.Comments = objComments;

                string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                DataAccessLayer dal = new DataAccessLayer(connection);
                if(dal.SaveFeedback(Context.User.Identity.Name, enquiryItem.DisplayName, objFeedback) > 0)
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

        private Boolean SendEnquiryDataService()
        {
            bool blnEnquiryDataSent = false;

            try
            {
                //Virgin Active requirement to concatenate fields into one string 

                System.Text.StringBuilder strCommentsField = new System.Text.StringBuilder();
                strCommentsField.Append("Comment: ");
                strCommentsField.Append(txtComments.Value.Trim());
                strCommentsField.Append("||");
                strCommentsField.Append("Job Title: ");
                strCommentsField.Append(txtJobTitle.Value.Trim());
                strCommentsField.Append("||");
                strCommentsField.Append("Company Name: ");
                strCommentsField.Append(txtCompanyName.Value.Trim());
                strCommentsField.Append("||");
                strCommentsField.Append("Company Website: ");
                strCommentsField.Append(txtCompanyWebsite.Value.Trim());
                strCommentsField.Append("||");
                strCommentsField.Append("Location(s): ");
                strCommentsField.Append(txtLocation.Value.Trim());
                strCommentsField.Append("||");
                strCommentsField.Append("Clubs of interest: ");
                strCommentsField.Append(ClubNames);
                strCommentsField.Append("||");
                strCommentsField.Append("No. of Employees: ");
                //strCommentsField.Append(drpNoEmployees.SelectedValue.ToString() != Translate.Text("Select") ? drpNoEmployees.SelectedValue.ToString() : "");
                strCommentsField.Append(txtNoEmployees.Value.Trim());
                strCommentsField.Append("||");
                strCommentsField.Append("Existing Virgin Active Members: ");
                //strCommentsField.Append(drpExistingMembers.SelectedValue.ToString() != Translate.Text("Select") ? drpExistingMembers.SelectedValue.ToString() : "");
                strCommentsField.Append(txtExistingMembers.Value.Trim());
                strCommentsField.Append("||");

                mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();
                virginActiveService.InsertCorporateWebLead(

                    txtName.Value.Trim(),
                    currentClub.ClubId.Rendered,
                    txtPhone.Value.Trim(),
                    txtEmail.Value.Trim(),
                    strCommentsField.ToString(),
                    false, //publish info
                    false, //share info
                    false);

                //var test = virginActiveService.ReadCorporateWebLead();

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending corporate enquiry webserivce data {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEnquiryDataSent;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.CorporateEnquiryConfirmation;
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

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.CorporateEnquiryConfirmation), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.CorporateEnquiryConfirmation)).Emailhtml.Text);
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
                Log.Error(String.Format("Error sending corporate enquiry confirmation email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
            return blnEmailSent;
        }

        //private Boolean SendAdminEmail()
        //{
        //    bool blnEmailSent = false;

        //    try
        //    {
        //        string strEmailSubject = EmailSubjects.CorporateEnquiryFormAdmin;
        //        string strEmailFromAddress = Settings.WebsiteMailFromAddress;

        //        string strEmailToAddress = "";
                //if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                //{
                //    //Use test
                //    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                //}
                //else
                //{
                //    if (currentClub.Salesemail.Rendered.Trim() != "")
                //    {
                //        strEmailToAddress = currentClub.Salesemail.Rendered.Trim();
                //    }
                //    else
                //    {
                //        strEmailToAddress = Settings.DefaultFormToEmailAddress;
                //        //TODO: Send warning alert.
                //        Log.Error(String.Format("Could not find sales email address for club {0}", currentClub.Clubname.Raw), this);
                //    }
                //}
                

        //        //Populate email text variables
        //        Hashtable objTemplateVariables = new Hashtable();
        //        objTemplateVariables.Add("ClubNames", clubNames);
        //        objTemplateVariables.Add("CustomerName", txtName.Value.Trim());
        //        objTemplateVariables.Add("JobTitle", txtJobTitle.Value.Trim());
        //        objTemplateVariables.Add("CustomerEmail", txtEmail.Value.Trim());
        //        objTemplateVariables.Add("Telephone", txtPhone.Value.Trim());
        //        objTemplateVariables.Add("CompanyName", txtCompanyName.Value.Trim() != "" ? txtCompanyName.Value.Trim() : "N/A");
        //        objTemplateVariables.Add("CompanyWebsite", txtCompanyWebsite.Value.Trim() != "" ? txtCompanyWebsite.Value.Trim() : "N/A");
        //        objTemplateVariables.Add("Location", txtLocation.Value.Trim() != "" ? txtLocation.Value.Trim() : "N/A");
        //        objTemplateVariables.Add("NoOfEmployees", drpNoEmployees.SelectedValue.ToString() != "" && drpNoEmployees.SelectedValue.ToString() != Translate.Text("Select") ? drpNoEmployees.SelectedValue.ToString() : "N/A");
        //        objTemplateVariables.Add("ExistingMembers", drpExistingMembers.SelectedValue.ToString() != "" && drpExistingMembers.SelectedValue.ToString() != Translate.Text("Select") ? drpExistingMembers.SelectedValue.ToString() : "N/A");
        //        objTemplateVariables.Add("ClubName", currentClub.Clubname.Rendered);
        //        objTemplateVariables.Add("EmailSubject", strEmailSubject);
        //        objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
        //        objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

        //        Parser objParser = new Parser(Server.MapPath(EmailTemplates.CorporateEnquiryFormAdmin), objTemplateVariables);
        //        string strMessageBody = objParser.Parse();

        //        mm.virginactive.webservices.WEBSRV_Messaging.SendResult objSendResult = new mm.virginactive.webservices.WEBSRV_Messaging.SendResult();
        //        mm.virginactive.webservices.WEBSRV_Messaging.Messaging objMessaging = new mm.virginactive.webservices.WEBSRV_Messaging.Messaging();

        //        string strAttachments = "";

        //        //now call messaging service
        //        objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject,
        //            mm.virginactive.webservices.WEBSRV_Messaging.MailFormat.Html, strMessageBody, strAttachments, Settings.MailPasscode);

        //        if (objSendResult.Success == true)
        //        {
        //            blnEmailSent = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(String.Format("Error sending data email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
        //    }

        //    return blnEmailSent;
        //}
    }
}