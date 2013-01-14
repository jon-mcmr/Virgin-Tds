using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using Sitecore.Data.Items;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Web.UI.HtmlControls;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using Sitecore.Diagnostics;
using System.Collections;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using CustomItemGenerator.Fields.SimpleTypes;
using Sitecore.Links;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingEnquiryForm : System.Web.UI.UserControl
    {
		protected LandingEnquiryItem currentItem = new LandingEnquiryItem(Sitecore.Context.Item);
		protected LandingPageItem currentLanding = null;
		protected ClubItem clubItem = null;
		protected string clubId;
		protected string profile;
		protected string region;
		protected ManagerItem manager;

        protected string TimeTableUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            SetSitecoreData();

            btnSubmit.Text = Translate.Text("Submit");


            SetValidation();
        }

        private void SetValidation()
        {
            string val = Translate.Text("Please enter {0}");

            rfvName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a name")));
            revName.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid name")));
            rfvEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("an email address")));
            revEmail.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid email address")));
            rfvPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a phone number")));
            revPhone.ErrorMessage = String.Format("<div class='error-msg'><p>{0}</p></div>", String.Format(val, Translate.Text("a valid phone number")));
            revEmail.ValidationExpression = Settings.EmailAddressRegularExpression;
            revPhone.ValidationExpression = Settings.PhoneNumberRegularExpression;
            revName.ValidationExpression = Settings.NameRegularExpression;
        }

		private void SetSitecoreData()
		{
			//Get User from Session
			//Check Session
			User objUser = new User();
			if (Session["sess_User_landing"] != null)
			{
				objUser = (User)Session["sess_User_landing"];

				clubId = objUser.BrowsingHistory.LandingClubID;
				profile = objUser.BrowsingHistory.LandingProfile;
				region = objUser.BrowsingHistory.LandingRegion;
			}

            //Set currentLanding item
            if (currentItem != null)
            {
                currentLanding = currentItem.InnerItem.Parent;
            }

			if (currentLanding != null)
				btnSubmit.Attributes.Add("data-gaqlabel", currentLanding.LandingBase.LandingId.Rendered);

			//Get Club Data

			if (!String.IsNullOrEmpty(clubId))
				clubItem = SitecoreHelper.GetClubOnClubId(clubId);

            if (clubItem != null)
            {
                //Image Carousel
                List<MediaItem> imageList;
                if (!String.IsNullOrEmpty(clubItem.Imagegallery.Raw))
                {
                    imageList = clubItem.Imagegallery.ListItems.ConvertAll(X => new MediaItem(X));

                    ClubImages.DataSource = imageList;
                    ClubImages.DataBind();
                }

                System.Text.StringBuilder markupBuilder;
                markupBuilder = new System.Text.StringBuilder();

                markupBuilder.Append(clubItem.Addressline1.Rendered);
                markupBuilder.Append(!String.IsNullOrEmpty(clubItem.Addressline2.Rendered) ? ", " + clubItem.Addressline2.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(clubItem.Addressline3.Rendered) ? ", " + clubItem.Addressline3.Rendered : "");
                markupBuilder.Append(!String.IsNullOrEmpty(clubItem.Addressline4.Rendered) ? ", " + clubItem.Addressline4.Rendered : "");
                markupBuilder.Append(", " + clubItem.Postcode.Rendered);
				markupBuilder.Append(" | " + clubItem.Salestelephonenumber.Rendered);
                
                litClubAddress.Text = markupBuilder.ToString();

                Item timetableItem = clubItem.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@name='{0}']", Settings.TimetableSectionName.ToLower()));

                    //clubItem.InnerItem.Axes.SelectSingleItem(String.Format("descendant::*[@@name='{0}']", Settings.TimetableSectionName));
                if (timetableItem != null)
                {
                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = false;
                    urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                    string timetableUrl = Sitecore.Links.LinkManager.GetItemUrl(timetableItem, urlOptions);

                    TimeTableUrl = timetableUrl;
                }
            }
		}

		protected void ClubImagesOnDataBound(object sender, ListViewItemEventArgs e)
		{
			if (e.Item.ItemType == ListViewItemType.DataItem)
			{

				ListViewDataItem dataItem = (ListViewDataItem)e.Item;

				var image = dataItem.DataItem as MediaItem;
				var ListViewImage = e.Item.FindControl("ListViewImage") as System.Web.UI.WebControls.Literal;

				if (ListViewImage != null)
				{
					if (dataItem.DataItemIndex == 0)
					{
						//ListViewImage.Text = @"<div class=""span6"">" + image.RenderCrop("460x210") + "</div>";
						ListViewImage.Text = @"<div class=""span6""><img src=""" + Sitecore.StringUtil.EnsurePrefix('/', SitecoreHelper.GetCropSizeMediaUrl(image, "460x210")) + @""" /></div>";
					}
                    else if (dataItem.DataItemIndex < 3)
					{
						//ListViewImage.Text = @"<div class=""span3"">" + image.RenderCrop("200x110") + "</div>";
						ListViewImage.Text = @"<div class=""span3""><img src=""" + Sitecore.StringUtil.EnsurePrefix('/', SitecoreHelper.GetCropSizeMediaUrl(image, "220x120")) + @""" /></div>";
					}
				}

			}
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			try
			{
				if (Page.IsValid == true)
				{
					//Get User from Session
					//Check Session
					User objUser = new User();
					if (Session["sess_User_landing"] != null)
					{
						objUser = (User)Session["sess_User_landing"];

						clubId = objUser.BrowsingHistory.LandingClubID;
						profile = objUser.BrowsingHistory.LandingProfile;
						region = objUser.BrowsingHistory.LandingRegion;
					}

					//Get Club Data

					if (!String.IsNullOrEmpty(clubId))
					{
						clubItem = SitecoreHelper.GetClubOnClubId(clubId);

						if (clubItem != null)
						{

							//Store Feedback Entitiy Details
							Feedback objFeedback = new Feedback();

							//Store Customer Entitiy Details
							Customer objCustomer = new Customer();

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

							objCustomer.EmailAddress = txtEmail.Value;
							objCustomer.Firstname = FirstName;
							objCustomer.Surname = Surname;
							objCustomer.HomeClubID = clubItem.ClubId.Rendered;
							objCustomer.TelephoneNumber = txtPhone.Value;
							objCustomer.SubscribeToNewsletter = chkSubscribe.Checked;

							objFeedback.Customer = objCustomer;
                            objFeedback.FeedbackSubject = "Landing Enquiry"; //Landing Enquiry
                            objFeedback.FeedbackSubjectDetail = "Virgin Active Website Landing Page"; //Virgin Active Website Landing Page
							objFeedback.FeedbackTypeID = Convert.ToInt32(currentLanding.LandingBase.LandingId.Rendered); 
							objFeedback.PrimaryClubID = clubItem.ClubId.Rendered;
							objFeedback.SubmissionDate = DateTime.Now;
                            
							//Add Comment
							Comment objComment = new Comment();

							//objComment.CommentDetail = source.Items[source.SelectedIndex].Value; 
							objComment.CommentDetail = currentLanding.LandingBase.LandingIdName.Rendered;// "Q1 Landing Page -2013"; //Other
							objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
							objComment.SortOrder = 1;
							objComment.Subject = Translate.Text("How did you hear about us?");

							List<Comment> objComments = new List<Comment>();
							objComments.Add(objComment);

							objFeedback.Comments = objComments;

							string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
							DataAccessLayer dal = new DataAccessLayer(connection);
							int successFlag = dal.SaveFeedback(Context.User.Identity.Name, currentItem.DisplayName, objFeedback);

							if (successFlag > 0)
							{
								//Data is sent to client via email
								SendAdminEmail();

								//Data is sent to client via service
								SendEnquiryDataService();
	
								//Data is sent to customer via email
								SendConfirmationEmail();

								//Save objFeedback back to session
								Session["sess_Customer"] = objCustomer;

								//Redirect to 'Whats Next' Page
                                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                                urlOptions.AlwaysIncludeServerUrl = true;
                                urlOptions.AddAspxExtension = false;
                                urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                                Item whatNextItem = currentItem.InnerItem.Parent.Axes.SelectSingleItem(String.Format("descendant-or-self::*[@@tid = '{0}']", Settings.LandingPagesWhatsNextTemplate));

                                string whatNextUrl = Sitecore.Links.LinkManager.GetItemUrl(whatNextItem, urlOptions)+"?_clubid=" + clubItem.ClubId.Rendered;

                                Response.Redirect(whatNextUrl);
							}
							else
							{
								//imageStatic.Attributes.Add("style", "display:none;");
								//formToComplete.Attributes.Add("style", "display:block;");
								//formCompleted.Attributes.Add("style", "display:none;");
								//pnlForm.Update();

								Log.Error(String.Format("Error storing campaign details"), this);
							}
						}
						else
						{
							//imageStatic.Attributes.Add("style", "display:none;");
							//formToComplete.Attributes.Add("style", "display:block;");
							//formCompleted.Attributes.Add("style", "display:none;");
							//pnlForm.Update();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(String.Format("Error processing club enquiry form {0}", ex.Message), this);
				mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
			}
		}
		private Boolean SendEnquiryDataService()
		{
			bool blnEnquiryDataSent = false;

			try
			{
				mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices virginActiveService = new mm.virginactive.webservices.virginactive.feedbackenquiry.MM_WebServices();

				virginActiveService.InsertWebLead(
					txtName.Value.Trim(),
					clubItem.ClubId.Rendered,
					txtPhone.Value.Trim(),
					"", //postcode
					txtEmail.Value.Trim(),
					string.Empty,
					string.Empty,
					Settings.WebLeadsSourceID,
					currentLanding.LandingBase.LandingIdName.Rendered,
					"Virgin Active Website Landing Page",				
					chkSubscribe.Checked
					); //newsletter

				//var test = virginActiveService.ReadWebLead();

				if (clubItem.ClubId.Rendered == "")
				{
					Log.Error(String.Format("No Club ID exists for club: {0}", clubItem.Clubname.Raw), this);
				}
			}
			catch (Exception ex)
			{
				Log.Error(String.Format("Error sending enquiry webservice data {1}: {0}", ex.Message, clubItem.Clubname.Raw), this);
				mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
			}

			return blnEnquiryDataSent;
		}

		private Boolean SendConfirmationEmail()
		{
			bool blnEmailSent = false;

			try
			{

				//get managers info                        
				List<ManagerItem> staffMembers = null;
				if (clubItem.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
				{
					staffMembers = clubItem.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));

					manager = staffMembers.First();
				}

                if (currentLanding != null && currentLanding.LandingBase.EmailTemplate.Item != null)
                {
                        //Get Campaign Email object
                    EmailBaseItem emailItem = currentLanding.LandingBase.EmailTemplate.Item;

                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = true;

                        //string strEmailSubject = campaign.CampaignBase.Emailsubject.Rendered;
                        string strEmailSubject = emailItem.Subject.Raw;
                        //string strEmailFromAddress = Settings.WebsiteMailFromText;
                        string strEmailFromAddress = emailItem.Fromaddress.Raw;
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

                        //objTemplateVariables.Add("CampaignTermsAndConditionsLinkUrl", new PageSummaryItem(currentCampaign.CampaignBase.Termsandconditionslink.Item).QualifiedUrl);
                        objTemplateVariables.Add("CustomerName", txtName.Value.Trim());
                        if (manager != null)
                        {
                            objTemplateVariables.Add("ClubManagerName", manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered);
                        }
                        else
                        {
                            objTemplateVariables.Add("ClubManagerName", "");
                        }
                        objTemplateVariables.Add("ClubName", clubItem.Clubname.Rendered);
                        objTemplateVariables.Add("EmailSubject", strEmailSubject);
                        objTemplateVariables.Add("EmailFromAddress", strEmailFromAddress);
                        objTemplateVariables.Add("EmailToAddress", strEmailToAddress);
                        objTemplateVariables.Add("YouTubeLinkUrl", Settings.YouTubeLinkUrl);
                        objTemplateVariables.Add("TwitterLinkUrl", Settings.TwitterLinkUrl);
                        objTemplateVariables.Add("FacebookLinkUrl", Settings.FacebookLinkUrl);
                        objTemplateVariables.Add("McCormackMorrisonLinkUrl", Settings.McCormackMorrisonUrl);
                        objTemplateVariables.Add("ImageRootUrl", Sitecore.Web.WebUtil.GetServerUrl());
                        objTemplateVariables.Add("ClubPhoneNumber", clubItem.Salestelephonenumber.Rendered);
                        objTemplateVariables.Add("ClubHomePageUrl", Sitecore.Links.LinkManager.GetItemUrl(clubItem, urlOptions));

                        System.Text.StringBuilder markupBuilder;
                        markupBuilder = new System.Text.StringBuilder();

                        markupBuilder.Append(clubItem.Addressline1.Rendered);
                        markupBuilder.Append(!String.IsNullOrEmpty(clubItem.Addressline2.Rendered) ? "<br />" + clubItem.Addressline2.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(clubItem.Addressline3.Rendered) ? "<br />" + clubItem.Addressline3.Rendered : "");
                        markupBuilder.Append(!String.IsNullOrEmpty(clubItem.Addressline4.Rendered) ? "<br />" + clubItem.Addressline4.Rendered : "");
                        markupBuilder.Append("<br />" + clubItem.Postcode.Rendered);

                        objTemplateVariables.Add("ClubAddress", markupBuilder.ToString());

                        //Parser objParser = new Parser(campaign.CampaignBase.GetTemplateHtml(), objTemplateVariables);
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
                            if (clubItem.InnerItem.TemplateName == Templates.ClassicClub)
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
				Log.Error(String.Format("Error sending module enquiry email confirmation {1}: {0}", ex.Message, clubItem.Clubname.Raw), this);
				mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
			}

			return blnEmailSent;
		}

		private Boolean SendAdminEmail()
		{
			bool blnEmailSent = false;

			try
			{
				string strEmailSubject = EmailSubjects.FeedbackCampaignFormAdmin;
				string strEmailFromAddress = Settings.WebsiteMailFromText;

				string strEmailToAddress = "";
				string strEmailBccAddress = "";
				if (Sitecore.Configuration.Settings.GetSetting("SystemEmails").ToLower() == "false")
				{
					strEmailToAddress = Settings.McCormackMorrisonTestEmailAddress;
				}
				else
				{
					//form sent to webleads -send confirmation to admin team
					strEmailToAddress = Settings.FeedbackCampaignEmailToListShort;
				}

				//Populate email text variables
				Hashtable objTemplateVariables = new Hashtable();
				objTemplateVariables.Add("CustomerName", txtName.Value);
				objTemplateVariables.Add("CustomerEmail", txtEmail.Value);
				objTemplateVariables.Add("HomeClubID", clubItem.ClubId.Rendered);
				objTemplateVariables.Add("ClubName", clubItem.Clubname.Rendered);
				objTemplateVariables.Add("Telephone", txtPhone.Value);
				objTemplateVariables.Add("Comments", "N/A");
				objTemplateVariables.Add("HowDidCustomerFindUs", "N/A"); //drpHowDidYouFindUs.SelectedValue.ToString() != "" && drpHowDidYouFindUs.SelectedValue.ToString() != Translate.Text("Select") ? drpHowDidYouFindUs.SelectedValue.ToString() : "");
				objTemplateVariables.Add("SubscribeToNewsletter", chkSubscribe.Checked ? "Yes" : "No");
				objTemplateVariables.Add("PublishDetails", "N/A");
				objTemplateVariables.Add("FeedbackSubject", currentLanding.LandingBase.LandingId.Rendered); //currentCampaign.CampaignBase.Campaigntype.Raw);
				objTemplateVariables.Add("FeedbackSubjectDetail", currentLanding.DisplayName); //currentCampaign.CampaignBase.Campaignname.Rendered);
				objTemplateVariables.Add("SubmissionDate", DateTime.Now.ToShortDateString());
				objTemplateVariables.Add("ImageSubmitted", "N/A");
				objTemplateVariables.Add("EnquiryType", "General");

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
				objSendResult = objMessaging.SendMailSecure(strEmailFromAddress, strEmailToAddress, "", strEmailBccAddress, strEmailSubject, true, strMessageBody, strAttachments);


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
    }
}