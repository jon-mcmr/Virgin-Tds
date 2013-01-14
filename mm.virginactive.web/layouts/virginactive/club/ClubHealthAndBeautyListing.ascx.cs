using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.HealthAndBeauty;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Collections;
using System.Web.Configuration;
using mm.virginactive.webservices;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class ClubHealthAndBeautyListing : System.Web.UI.UserControl
    {
        protected ClubHealthAndBeautyListingItem listing = new ClubHealthAndBeautyListingItem(Sitecore.Context.Item);
        protected ClubItem club;
        protected HealthAndBeautyListingItem sharedListing;
        protected HealthAndBeautyLandingItem sharedLanding;
        protected ManagerItem clubManager;
        protected string downloadPriceList = "";
        protected string downloadPriceListUrl = "";
        public List<TreatmentModuleItem> treatmentModules = new List<TreatmentModuleItem>();
        public List<TreatmentModuleItem> treatmentListToShow = new List<TreatmentModuleItem>();

        public ClubHealthAndBeautyListingItem Listing
        {
            get { return listing; }
            set
            {
                listing = value;
            }
        }

        public HealthAndBeautyListingItem SharedListing
        {
            get { return sharedListing; }
            set
            {
                sharedListing = value;
            }
        }
        public ManagerItem ClubManager
        {
            get { return clubManager; }
            set { clubManager = value; }
        }
        public HealthAndBeautyLandingItem SharedLanding
        {
            get { return sharedLanding; }
            set
            {
                sharedLanding = value;
            }
        }
        public ClubItem Club
        {
            get { return club; }
            set
            {
                club = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if listing is already initialised (i.e. loaded as the default listing page for the landing page)
            if (listing != null)
            {
                //Add datepicker jquery plugin script
                this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/jquery-ui-1.8.15.custom.min.js""></script>"));

                //Register script
                this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
                $(function(){
	                $.va_init.functions.addDatepicker();
                    $.va_init.functions.addTreatmentToDropDown();
                });
                </script>")); 

                //Get club                
                club = SitecoreHelper.GetCurrentClub(listing.InnerItem);

                //get the details of the first manager listed for the club (i.e. the manager)
                List<ManagerItem> staffMembers = null;
                Item[] mgrItems =  club.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId));
                if (mgrItems != null)
                {
                    staffMembers = mgrItems.ToList().ConvertAll(x => new ManagerItem(x));
                    clubManager = staffMembers.First();
                }

                //Get the facility modules that are to be shown on listing page
                treatmentModules = listing.Sharedtreatments.ListItems.ToList().ConvertAll(X => new TreatmentModuleItem(X));

                foreach (TreatmentModuleItem module in treatmentModules)
                {
                    //find which brand the treatment belows to
                    sharedLanding = new HealthAndBeautyLandingItem(module.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", HealthAndBeautyLandingItem.TemplateId)));
                    sharedListing = new HealthAndBeautyListingItem(module.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", HealthAndBeautyListingItem.TemplateId)));
                    break;
                }

                if (sharedLanding != null)
                {

                    string section = Sitecore.Web.WebUtil.GetQueryString("section");

                    if (!String.IsNullOrEmpty(section))
                    {
                        //get the correct section listing item from the querystring
                        sharedListing = sharedLanding.InnerItem.Axes.GetDescendant(section);
                    }

                    if (sharedListing != null)
                    {
                        if (listing.Pricelistavailable.Checked == true && listing.Pricelist != null)
                        {
                            downloadPriceListUrl = listing.Pricelist.MediaUrl;
                            downloadPriceList = @"<li><a href=""" + downloadPriceListUrl + @""">" + Translate.Text("Download a price list") + "</a></li>";
                        }
                        else
                        {
                            PriceList.Visible = false;
                        }

                        //Get list of shared treaments
                        if (sharedListing.InnerItem.HasChildren)
                        {
                            List<TreatmentModuleItem> sharedTreatmentList = sharedListing.InnerItem.Children.ToList().ConvertAll(X => new TreatmentModuleItem(X));
                            foreach (TreatmentModuleItem sharedTreatmentModule in sharedTreatmentList)
                            {
                                //Check if this treatment has been included for this club
                                foreach(TreatmentModuleItem module in treatmentModules)
                                {
                                    if (module.ID.ToString() == sharedTreatmentModule.ID.ToString())
                                    {
                                        //Show this treatment
                                        treatmentListToShow.Add(module);
                                    }
                                }
                            }

                            if (treatmentListToShow.Count > 0)
                            {
                                treatmentListToShow.First().IsFirst = true;

                                TreatmentList.DataSource = treatmentListToShow;
                                TreatmentList.DataBind();
                            }

                        }
                    }
                }

                string val = Translate.Text("Please enter {0}");
                rfvName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a name")));
                revName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid name")));
                rfvEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("an email address")));
                revEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid email address")));
                rfvPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a phone number")));
                revPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid phone number")));
                //rfvComments.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("your comments")));
                revEmail.ValidationExpression = Settings.EmailAddressRegularExpression;
                revPhone.ValidationExpression = Settings.PhoneNumberRegularExpression;
                revName.ValidationExpression = Settings.NameRegularExpression;

                if (!Page.IsPostBack)
                {
                    BindDropDownLists();
                }
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
                // Populate Treatment dropdown
                this.drpTreatment.Items.Clear();

                if (sharedListing != null && sharedListing.InnerItem.HasChildren)
                {
                    //List<TreatmentModuleItem> treatmentList = sharedListing.InnerItem.Children.ToList().ConvertAll(X => new TreatmentModuleItem(X));
                    ListItemCollection bookableTreatmentList = new ListItemCollection();


                    foreach (TreatmentModuleItem item in treatmentModules)
                    {
                        if (item.Isbookable.Checked == true)
                        {
                            ListItem li = new ListItem();
                            li.Text = item.Title.Rendered;
                            li.Value = item.Title.Rendered;
                            bookableTreatmentList.Add(li);
                        }
                    }

                    this.drpTreatment.DataSource = bookableTreatmentList;
                    this.drpTreatment.DataValueField = "Text";
                    this.drpTreatment.DataTextField = "Value";
                    this.drpTreatment.DataBind();

                    this.drpTreatment.Items.Insert(0, Translate.Text("Select"));
                }

                // Populate What Time dropdown
                this.drpPreferredTime.Items.Clear();
                this.drpPreferredTime.DataSource = FormHelper.ListCallBackWindows();
                this.drpPreferredTime.DataBind();

                this.drpPreferredTime.Items.Insert(0, Translate.Text("Select"));
            }
            catch(Exception ex)
            {
                Log.Error(String.Format("Error sending data email {1}: {0}", ex.Message, club.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid == true)
                {
                    //TODO: Log data to database
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
                Log.Error(String.Format("Error sending email confirmation {1}: {0}", ex.Message, club.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        private Boolean SendAdminEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.HealthAndBeautyBookingFormAdmin;
                string strEmailFromAddress = Settings.WebsiteMailFromText;

                string strEmailToAddress = "";
                if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                {
                    //Use test
                    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                }
                else
                {
                    if (listing.Healthandbeautybookingemail.Rendered.Trim() != "")
                    {
                        strEmailToAddress = listing.Healthandbeautybookingemail.Rendered.Trim();
                    }
                    else if (club.Salesemail.Rendered.Trim() != "")
                    {
                        strEmailToAddress = club.Salesemail.Rendered.Trim();
                    }
                    else
                    {
                        strEmailToAddress = Settings.DefaultFormToEmailAddress;
                        //TODO: Send warning alert.
                        Log.Error(String.Format("Can not find health and beauty email for club {0}", club.Clubname.Raw), this);
                    }
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("CustomerName", txtName.Value.Trim());
                objTemplateVariables.Add("CustomerEmail", txtEmail.Value.Trim());
                objTemplateVariables.Add("MembershipNo", txtMembership.Value.Trim());
                objTemplateVariables.Add("Telephone", txtPhone.Value.Trim());
                objTemplateVariables.Add("Treatment", drpTreatment.SelectedValue.ToString() != Translate.Text("Select") ? drpTreatment.SelectedValue.ToString() : "");
                objTemplateVariables.Add("PreferredDay", txtPreferredDay.Value.Trim());
                objTemplateVariables.Add("PreferredTime", drpPreferredTime.SelectedValue.ToString() != Translate.Text("Select") ? drpPreferredTime.SelectedValue.ToString() : "");
                objTemplateVariables.Add("Comments", txtComments.Value.Trim());
                objTemplateVariables.Add("ClubName", club.Clubname.Rendered);
                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.HealthAndBeautyBookingFormAdmin), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.HealthAndBeautyBookingFormAdmin)).Emailhtml.Text);
                string strMessageBody = objParser.Parse();

                mm.sharedtools.EmailMessagingService.SendResult objSendResult = new mm.sharedtools.EmailMessagingService.SendResult();
                mm.sharedtools.EmailMessagingService.Messaging objMessaging = new mm.sharedtools.EmailMessagingService.Messaging();

                string strAttachments = "";

                //now call messaging service
                objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", Settings.BccEmailAddress, strEmailSubject, true, strMessageBody, strAttachments);

                if (club.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", club.Clubname.Raw), this);
                }

                if (objSendResult.Success == true)
                {
                    blnEmailSent = true;
                }


            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending health and beauty data email {1}: {0}", ex.Message, club.Clubname.Raw), this);
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
                objCustomer.HomeClubID = club.ClubId.Rendered;
                objCustomer.TelephoneNumber = txtPhone.Value;
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = "Health And Beauty Enquiry";
                objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
                objFeedback.FeedbackTypeID = Constants.FeedbackType.HealthAndBeautyFeedback;
                objFeedback.PrimaryClubID = club.ClubId.Rendered;
                objFeedback.SubmissionDate = DateTime.Now;
                //objFeedback.PreferredCallbackDate = Convert.ToDateTime(txtPreferredDay.Value.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                //objFeedback.PreferredCallbackTime = drpPreferredTime.SelectedValue.ToString() != Translate.Text("Select") ? drpPreferredTime.SelectedValue.ToString() : "";

                //Add Comment
                Comment objComment = new Comment();

                objComment.CommentDetail = drpTreatment.SelectedValue.ToString() != Translate.Text("Select") ? drpTreatment.SelectedValue.ToString() : "";
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 1;
                objComment.Subject = "Treatment";

                List<Comment> objComments = new List<Comment>();
                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtPreferredDay.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 2;
                objComment.Subject = "Preferred Treatment Day";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = drpPreferredTime.SelectedValue.ToString() != Translate.Text("Select") ? drpPreferredTime.SelectedValue.ToString() : "";
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 3;
                objComment.Subject = "Preferred Treatment Time";

                objComments.Add(objComment);

                objFeedback.Comments = objComments;

                string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                DataAccessLayer dal = new DataAccessLayer(connection);
                if (dal.SaveFeedback(Context.User.Identity.Name, listing.DisplayName, objFeedback) > 0)
                {
                    blnDataSaved = true;
                }


            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error saving form data to reports db {1}: {0}", ex.Message, club.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnDataSaved;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;
            bool blnIsAppointment = false;

            try
            {
                if(txtPreferredDay.Value.Trim() != "" && drpTreatment.SelectedValue.ToString() != "" && drpTreatment.SelectedValue.ToString() != Translate.Text("Select"))
                {
                    blnIsAppointment = true;
                }
                string strEmailSubject = blnIsAppointment ? EmailSubjects.HealthAndBeautyAppointmentConfirmation : EmailSubjects.HealthAndBeautyEnquiryConfirmation;
                //string strEmailTemplate = blnIsAppointment ? EmailTemplates.HealthAndBeautyAppointmentConfirmation : EmailTemplates.HealthAndBeautyEnquiryConfirmation;
                EmailItem EmailTemplate = blnIsAppointment ? (EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.HealthAndBeautyAppointmentConfirmation) : (EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.HealthAndBeautyEnquiryConfirmation);


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
                if (clubManager != null)
                {
                    objTemplateVariables.Add("ClubManagerName", clubManager.Person.Firstname.Rendered + " " + clubManager.Person.Lastname.Rendered);
                }
                else
                {
                    objTemplateVariables.Add("ClubManagerName", "");
                }
                objTemplateVariables.Add("PreferredDay", txtPreferredDay.Value.Trim() != "" ? txtPreferredDay.Value.Trim() : "N/A");
                objTemplateVariables.Add("PreferredTime", drpPreferredTime.SelectedValue.ToString() != "" && drpPreferredTime.SelectedValue.ToString() != Translate.Text("Select") ? drpPreferredTime.SelectedValue.ToString() : "N/A");
                objTemplateVariables.Add("Treatment", drpTreatment.SelectedValue.ToString() != "" && drpTreatment.SelectedValue.ToString() != Translate.Text("Select") ? drpTreatment.SelectedValue.ToString() : "N/A");
                objTemplateVariables.Add("ClubName", club.Clubname.Rendered);
                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                objTemplateVariables.Add("ClubPhoneNumber", club.Salestelephonenumber.Rendered);

                System.Text.StringBuilder markupBuilder;
                markupBuilder = new System.Text.StringBuilder();

                markupBuilder.Append(club.Addressline1.Rendered);
                markupBuilder.Append(!String.IsNullOrEmpty(club.Addressline2.Rendered) ? "<br />" + club.Addressline2.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(club.Addressline3.Rendered) ? "<br />" + club.Addressline3.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(club.Addressline4.Rendered) ? "<br />" + club.Addressline4.Rendered : "");
                markupBuilder.Append("<br />" + club.Postcode.Rendered);

                objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());

                //Get the facility modules that are to be shown on listing page
                List<TreatmentModuleItem> treatmentModules = listing.Sharedtreatments.ListItems.ToList().ConvertAll(X => new TreatmentModuleItem(X));
               
                //Chosen Treatment and Brand details 
                string strTreatment = ""; 
                string strHealthAndBeautyBrand = "";
                string strTreatmentBrandDetails = "";

                strTreatment = drpTreatment.SelectedValue.ToString() != Translate.Text("Select") ? drpTreatment.SelectedValue.ToString() : "";

                foreach (TreatmentModuleItem module in treatmentModules)
                {
                    //find which brand the treatment belows to
                    sharedLanding = new HealthAndBeautyLandingItem(module.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", HealthAndBeautyLandingItem.TemplateId)));
                    if (sharedLanding != null)
                    {
                        strHealthAndBeautyBrand = sharedLanding.PageSummary.NavigationTitle.Rendered;
                        strEmailSubject = strEmailSubject.Replace("<SpaName>", strHealthAndBeautyBrand);
                    }
                    break;
                }

                if (strTreatment != "" && strHealthAndBeautyBrand != "" && blnIsAppointment)
                {
                    strTreatmentBrandDetails = strTreatment + " at " + strHealthAndBeautyBrand + ", " + club.Clubname.Rendered; 
                }
                else if (strHealthAndBeautyBrand != "")
                {
                    strTreatmentBrandDetails = strHealthAndBeautyBrand + ", " + club.Clubname.Rendered; 
                }
                else if (strTreatment != "")
                {
                    strTreatmentBrandDetails = strHealthAndBeautyBrand + ", " + club.Clubname.Rendered;
                }
                else
                {
                    strTreatmentBrandDetails = club.Clubname.Rendered;
                }

                objTemplateVariables.Add("TreatmentBrandDetails", strTreatmentBrandDetails);

                //Chosen Time and Day options
                string strDay = txtPreferredDay.Value.Trim();
                string strTime = drpPreferredTime.SelectedValue.ToString() != Translate.Text("Select") ? drpPreferredTime.SelectedValue.ToString() : "";
                string strDayAndTime = "";

                if (strDay != "" && strTime != "")
                {
                    strDayAndTime = strDay + " and " + strTime;
                }
                else if (strDay != "")
                {
                    strDayAndTime = strDay;
                }

                objTemplateVariables.Add("PreferredDayAndTime", strDayAndTime);

                //Parser objParser = new Parser(Server.MapPath(strEmailTemplate), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(EmailTemplate.Emailhtml.Text);
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
                Log.Error(String.Format("Error sending confirmation email {1}: {0}", ex.Message, club.Clubname.Raw), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            return blnEmailSent;
        }
    }
}