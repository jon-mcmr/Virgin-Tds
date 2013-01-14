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
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Offers;


namespace mm.virginactive.web.layouts.virginactive.clubfinder
{
    public partial class OfferEnquiryForm : System.Web.UI.UserControl
    {

        protected ManagerItem manager;
        protected OfferItem offerItem;
        protected ClubItem currentClub;
        protected EnquiryFormItem enquiryItem = new EnquiryFormItem(Sitecore.Context.Item);
        

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

            //Get offer item from item path
            string clubId = WebUtil.GetQueryString("clubId");
            string offerId = WebUtil.GetQueryString("offerId");

            offerItem = SitecoreHelper.GetOfferOnOfferId(offerId);
            currentClub = SitecoreHelper.GetClubOnClubId(clubId);

            if (offerItem != null && currentClub != null)
            {

                string val = Translate.Text("Please enter {0}");
                rfvName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a name")));
                revName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid name")));
                rfvEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("an email address")));
                revEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid email address")));
                rfvClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
                revClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
                cvClubName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select a club"));
                rfvPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a contact number")));
                revPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid contact number")));
                rfvHowDidYouFindUs.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select how you heard about us"));
                revHowDidYouFindUs.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", Translate.Text("Please select how you heard about us"));

                revEmail.ValidationExpression = Settings.EmailAddressRegularExpression;
                revName.ValidationExpression = Settings.NameRegularExpression;
                revPhone.ValidationExpression = Settings.PhoneNumberRegularExpression;
                revClubName.ValidationExpression = "^((?!" + Translate.Text("Select a club") + ").)*$";
                revHowDidYouFindUs.ValidationExpression = "^((?!" + Translate.Text("Select") + ").)*$";

                Assert.ArgumentNotNullOrEmpty(ItemPaths.Clubs, "Club root path not set");

                if (!Page.IsPostBack)
                {
                    BindDropDownLists();
                }


                this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
	            $(function(){
	                $.va_init.functions.setupForms();
	            });
                </script>"));
            }
        }

        private void BindDropDownLists()
        {
            try
            {
                //If this form is being called froma  club area, we will get a ShortGuid of the 

                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                ChildList clubLst = clubRoot.Children;
                Item[] clubs = clubLst.ToArray();

                //Populate the club selector
                List<ClubItem> clubList = clubs.ToList().ConvertAll(X => new ClubItem(X));
                clubList.RemoveAll(x => x.IsHiddenFromMenu());
                clubList.RemoveAll(x => x.IsPlaceholder.Checked && !x.ShowInClubSelect.Checked);
                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));

                foreach (Item club in clubList)
                {
                    ClubItem clubItem = new ClubItem(club);
                    if (clubItem != null)
                    {

                        bool isSelected = currentClub.ID.ToShortID().ToString().Equals(clubItem.ID.ToShortID().ToString());

                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            ListItem lst = new ListItem(clubLabel, clubItem.ID.ToString());
                            lst.Attributes.Add("data-email", clubItem.Salesemail.Raw);
                            clubFindSelect.Items.Add(lst);

                            if (isSelected)
                            {
                                clubFindSelect.SelectedValue = clubItem.ID.ToString();
                            }
                        }
                    }
                }

                // Populate Enquiry dropdown
                this.drpHowDidYouFindUs.Items.Clear();

                string queryItemsString = enquiryItem.Querytypes.Raw.Replace("\r\n", ",");
                queryItemsString = queryItemsString.Replace("\n", ",");
                queryItemsString = queryItemsString.TrimEnd(',');
                string[] queryItems = queryItemsString.Split(',');

                //drpHowDidYouFindUs.Items.Add(new ListItem(Translate.Text("Select"), ""));
                foreach (string item in queryItems)
                {
                    drpHowDidYouFindUs.Items.Add(new ListItem(item, item));
                }
                this.drpHowDidYouFindUs.Items.Insert(0, (Translate.Text("Select")));
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error binding drop down lists {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
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

                        //if (currentClub.ExEsporta.Checked == true)
                        //{
                        //    //Data is sent to client via email
                        //    SendAdminEmail();
                        //}
                        //   else
                        //   {
                        //Data is sent to client via service
                        //       SendEnquiryDataService();
                        //   }

                        //Data is sent to client via service
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

                        //System.Threading.Thread.Sleep(5000);
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
                Log.Error(String.Format("Error processing club enquiry form {0}", ex.Message), this);
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
                objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                objCustomer.SubscribeToNewsletter = chkSubscribe.Checked;
                objCustomer.TelephoneNumber = txtPhone.Value.Trim();
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = offerItem.Offertype.Item.Name + " Offer Enquiry";
                objFeedback.FeedbackSubjectDetail = offerItem.OfferId.Rendered;
                objFeedback.FeedbackTypeID = Constants.FeedbackType.ClubEnquiry;
                objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                objFeedback.SubmissionDate = DateTime.Now;

                //Add Comment
                Comment objComment = new Comment();

                objComment.CommentDetail = drpHowDidYouFindUs.SelectedValue.ToString();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 1;
                objComment.Subject = "How did you find out about us";

                List<Comment> objComments = new List<Comment>();
                objComments.Add(objComment);
                objFeedback.Comments = objComments;

                string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                DataAccessLayer dal = new DataAccessLayer(connection);
                if (dal.SaveFeedback(Context.User.Identity.Name, enquiryItem.DisplayName, objFeedback) > 0)
                {
                    blnDataSaved = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error saving form data to reports db {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }

            return blnDataSaved;
        }

        private Boolean SendEnquiryDataService()
        {
            bool blnEnquiryDataSent = false;


            try
            {
                mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();

                virginActiveService.InsertWebLead(
                    txtName.Value.Trim(),
                    currentClub.ClubId.Rendered,
                    txtPhone.Value.Trim(),
                    "", //postcode
                    txtEmail.Value.Trim(),
                    "", //preferred time
                    "", //memberFlag
                    Settings.WebLeadsSourceID, //sourceId
                    drpHowDidYouFindUs.SelectedValue.ToString() != "" && drpHowDidYouFindUs.SelectedValue.ToString() != Translate.Text("Select") ? drpHowDidYouFindUs.SelectedValue.ToString() : "", //source
                    offerItem.OfferId.Rendered != "" ? offerItem.OfferId.Rendered : "Virgin Active Website Campaign", //campaignId (offercode)
                    chkSubscribe.Checked ? true : false); //newsletter

                //var test = virginActiveService.ReadWebLead();

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending enquiry webservice data {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }

            return blnEnquiryDataSent;
        }

        private Boolean SendConfirmationEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.EnquiryConfirmation;
                string strEmailFromAddress = Settings.WebsiteMailFromText;
                string strEmailToAddress = txtEmail.Value.Trim();

                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                urlOptions.AlwaysIncludeServerUrl = true;
                urlOptions.AddAspxExtension = true;
                urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("HomePageUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.HomePage));
                objTemplateVariables.Add("FacilitiesAndClassesLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.FacilitiesLanding));
                objTemplateVariables.Add("YourHealthLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.YourHealthArticles));
                objTemplateVariables.Add("MembershipsLinkUrl", SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.MembershipOptions));
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
                objTemplateVariables.Add("ClubHomePageUrl", Sitecore.Links.LinkManager.GetItemUrl(currentClub, urlOptions));
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

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.EnquiryConfirmation), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.EnquiryConfirmation)).Emailhtml.Text);
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
                Log.Error(String.Format("Error sending confirmation email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }

            return blnEmailSent;
        }

        private Boolean SendAdminEmail()
        {
            bool blnEmailSent = false;

            try
            {
                string strEmailSubject = EmailSubjects.ClubEnquiryFormAdmin;
                string strEmailFromAddress = Settings.WebsiteMailFromText;

                string strEmailToAddress = "";
                if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
                {
                    //Use test
                    strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
                }
                else
                {
                    if (currentClub.Salesemail.Rendered.Trim() != "")
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

                User objUser = new User();
                //Set user session variable
                if (Session["sess_User"] != null)
                {
                    objUser = (User)Session["sess_User"];
                }

                string enquiryFormType = "";
                switch (objUser.EnquiryFormType)
                {
                    case EnquiryFormTypes.BookATour:
                        enquiryFormType = "Book A Visit";
                        break;
                    case EnquiryFormTypes.Corporate:
                        enquiryFormType = "Corporate";
                        break;
                    case EnquiryFormTypes.ExistingScheme:
                        enquiryFormType = "Existing Scheme";
                        break;
                    case EnquiryFormTypes.General:
                        enquiryFormType = "General";
                        break;
                    default:
                        enquiryFormType = "General";
                        break;
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("CustomerName", txtName.Value.Trim());
                objTemplateVariables.Add("CustomerEmail", txtEmail.Value.Trim());
                objTemplateVariables.Add("Telephone", txtPhone.Value.Trim() != "" ? txtPhone.Value.Trim() : "N/A");
                objTemplateVariables.Add("HowDidCustomerFindUs", drpHowDidYouFindUs.SelectedValue.ToString() != "" && drpHowDidYouFindUs.SelectedValue.ToString() != Translate.Text("Select") ? drpHowDidYouFindUs.SelectedValue.ToString() : "");
                objTemplateVariables.Add("SubscribeToNewsletter", chkSubscribe.Checked ? "Yes" : "No");
                objTemplateVariables.Add("ClubName", currentClub.Clubname.Rendered);
                objTemplateVariables.Add("EnquiryType", enquiryFormType);
                objTemplateVariables.Add("EmailSubject", strEmailSubject);
                objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                objTemplateVariables.Add("EmailToAddress", strEmailToAddress);

                //Parser objParser = new Parser(Server.MapPath(EmailTemplates.ClubEnquiryFormAdmin), objTemplateVariables);
                Parser objParser = new Parser(objTemplateVariables);
                objParser.SetTemplate(((EmailItem)Sitecore.Context.Database.GetItem(EmailPaths.ClubEnquiryFormAdmin)).Emailhtml.Text);
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

                if (currentClub.ClubId.Rendered == "")
                {
                    Log.Error(String.Format("No Club ID exists for club: {0}", currentClub.Clubname.Raw), this);
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error sending enquiry data email {1}: {0}", ex.Message, currentClub.Clubname.Raw), this);
            }

            return blnEmailSent;
        }

    }
}