using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using System.Collections;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using Sitecore.Collections;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class FeedbackCampaign : System.Web.UI.UserControl
    {
        protected WizardCampaignItem campaign = new WizardCampaignItem(Sitecore.Context.Item);
        protected bool ImageFileProvided = false;
        protected ClubItem currentClub;
        protected string clubID = "";

        public ClubItem CurrentClub
        {
            get { return currentClub; }
            set
            {
                currentClub = value;
            }
        }
        public string ClubID
        {
            get { return clubID; }
            set
            {
                clubID = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //Register Async Post Back Control
            //ScriptManager1.RegisterAsyncPostBackControl(btnSubmit);

            List<ClubItem> clubs = null;

            if (campaign.Campaign.CampaignBase.Usecustomclublist.Checked)
            {
                //Use a custom list of clubs
                clubs = campaign.Campaign.CampaignBase.Customclublist.ListItems.ConvertAll(X => new ClubItem(X));
            }
            else
            {
                //Use a custom list of clubs
                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                ChildList clubLst = clubRoot.Children;
                clubs = clubLst.ToList().ConvertAll(X => new ClubItem(X));
                clubs.RemoveAll(x => x.IsHiddenFromMenu());
            }

            if (clubs != null)
            {
                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));

                /*
                //Sort clubs alphabetically
                clubList.Sort(delegate(ClubItem c1, ClubItem c2)
                {
                    return c1.Clubname.Raw.CompareTo(c2.Clubname.Raw);

                });*/

                foreach (ClubItem clubItem in clubs)
                {
                    if (clubItem != null)
                    {
                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.InnerItem.ID.ToString()));
                        }
                    }
                }
            }

            //generate the image carousel
            List<ImageCarouselItem> imgCar = new List<ImageCarouselItem>();
            if (campaign.InnerItem.HasChildren)
            {
                imgCar = campaign.InnerItem.Children.ToList().ConvertAll(X => new ImageCarouselItem(X));
                ImageList.DataSource = imgCar;
                ImageList.DataBind();
            }

            //add the item specific css stylesheet
            string classNames = article.Attributes["class"];
            article.Attributes.Add("class", classNames.Length > 0 ? classNames + " feedback-" + campaign.Campaign.CampaignBase.Cssstyle.Raw : "feedback-" + campaign.Campaign.CampaignBase.Cssstyle.Raw);

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
                title.Text = campaign.PageSummary.NavigationTitle.Raw + " | " + Translate.Text("Virgin Active");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnIsValid = true;
                mm.virginactive.screportdal.Models.Image objImage = null;

                string strMIME = filename.PostedFile.ContentType;
                //validate image
                if ((filename.HasFile) && (strMIME.ToUpper() == "IMAGE/JPEG" || strMIME.ToUpper() == "IMAGE/PJPEG" || strMIME.ToUpper() == "IMAGE/JPG" || strMIME.ToUpper() == "IMAGE/GIF"))
                {                    
                    byte[] imageBytes = new byte[filename.PostedFile.InputStream.Length + 1];
                    filename.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);

                    if (imageBytes.Length <= 5000000) //Check if number of bytes greater than 5mb
                    {
                        objImage = new mm.virginactive.screportdal.Models.Image();
                        //Add Image
                        objImage.CommentImageTypeID = Constants.CommentImageType.CompetitionImage;
                        objImage.SortOrder = 1;
                        objImage.Subject = "Your Story";
                        objImage.ImageFile = imageBytes;
                        objImage.ImageMIMEType = strMIME;

                        ImageFileProvided = true;
                    }
                    else
                    {
                        blnIsValid = false;
                    }
                }

                //check if we have a valid club selected
                blnIsValid = clubFindSelect.SelectedIndex != 0;

                if (blnIsValid == true)
                {
                    currentClub = (ClubItem)Sitecore.Context.Database.GetItem(clubFindSelect.SelectedValue);				

                    if (currentClub != null)
                    {
                        ClubID = currentClub.ClubId.Rendered;
                    }

                    //Store Feedback Entitiy Details
                    Feedback objFeedback = new Feedback();

                    //Store Customer Entitiy Details
                    Customer objCustomer = new Customer();
                    objCustomer.EmailAddress = email.Value;

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

                    objCustomer.Firstname = FirstName;
                    objCustomer.Surname = Surname;
                    //objCustomer.HomeClubID = txtClubCode.Value;
                    objCustomer.HomeClubID = clubID;
                    objCustomer.SubscribeToNewsletter = subscribe.Checked;
                    objCustomer.TelephoneNumber = contact.Value;

                    objFeedback.Customer = objCustomer;
                    objFeedback.FeedbackSubject = "Your Story";
                    objFeedback.FeedbackSubjectDetail = "Share Your Success";
                    objFeedback.FeedbackTypeID = Constants.FeedbackType.ShareYourSuccessCompetition;
                    //objFeedback.PrimaryClubID = txtClubCode.Value;
                    objFeedback.PrimaryClubID = ClubID;
                    objFeedback.SubmissionDate = DateTime.Now;

                    //Add Comment
                    Comment objComment = new Comment();

                    objComment.CommentDetail = story.Value;
                    objComment.CommentImageTypeID = Constants.CommentImageType.CompetitionAnswer;
                    objComment.SortOrder = 1;
                    objComment.Subject = "Your Story";

                    List<Comment> objComments = new List<Comment>();
                    objComments.Add(objComment);
                    objFeedback.Comments = objComments;

                    ////Add Image
                    if (objImage != null)
                    {
                        List<mm.virginactive.screportdal.Models.Image> objImages = new List<mm.virginactive.screportdal.Models.Image>();
                        objImages.Add(objImage);
                        objFeedback.Images = objImages;
                    }

                    string connection = Sitecore.Configuration.Settings.GetSetting("ConnectionString_VirginActiveReports");
                    DataAccessLayer dal = new DataAccessLayer(connection);
                    int successFlag = dal.SaveFeedback(Context.User.Identity.Name, campaign.DisplayName, objFeedback);

                    // Successfully deleted
                    if (successFlag > 0)
                    {
                        
                        //Data is sent to client via email
                        SendAdminEmail();
                        
                        imageCarousel.Attributes.Add("style", "display:none;");
                        innerstory.Attributes.Add("style", "display:none;");
                        innerform.Attributes.Add("style", "display:none;");
                        formSummary.Attributes.Add("style", "display:none;");
                        formCompleted.Attributes.Add("style", "display:block;");

                        string classNames = article.Attributes["class"];
                        article.Attributes.Add("class", classNames.Length > 0 ? classNames + " ImageOverlay" : "ImageOverlay");
                        ClearFormFields();
                    }
                    else
                    {
                        imageCarousel.Attributes.Add("style", "display:none;");
                        innerstory.Attributes.Add("style", "display:none;");
                        innerform.Attributes.Add("style", "display:none;");
                        formSummary.Attributes.Add("style", "display:block;");
                        formCompleted.Attributes.Add("style", "display:none;");

                        string classNames = article.Attributes["class"];
                        article.Attributes.Add("class", classNames.Length > 0 ? classNames + " ImageOverlay" : "ImageOverlay");

                        cvFormSubmission.IsValid = false;
                        cvFormSubmission.ErrorMessage = Translate.Text("Error uploading details");
                        Log.Error(String.Format("Error storing campaign details"), this);
                    }
                }
                else
                {
                    imageCarousel.Attributes.Add("style", "display:none;");
                    innerstory.Attributes.Add("style", "display:none;");
                    innerform.Attributes.Add("style", "display:block;");
                    formSummary.Attributes.Add("style", "display:none;");
                    formCompleted.Attributes.Add("style", "display:none;");

                    string classNames = article.Attributes["class"];
                    article.Attributes.Add("class", classNames.Length > 0 ? classNames + " ImageOverlay" : "ImageOverlay");

                    cvImageSubmission.IsValid = false;
                    cvImageSubmission.ErrorMessage = Translate.Text("File size limit 5Mb");
                    cvImageSubmission.Attributes.Add("style", "display:none;");

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
                    strEmailToAddress = Settings.FeedbackCampaignEmailToList;
                }

                //Populate email text variables
                Hashtable objTemplateVariables = new Hashtable();
                objTemplateVariables.Add("CustomerName", yourname.Value);
                objTemplateVariables.Add("CustomerEmail", email.Value);
                //objTemplateVariables.Add("HomeClubID", txtClubCode.Value);
                objTemplateVariables.Add("HomeClubID", clubID);
                objTemplateVariables.Add("Telephone", contact.Value);
                objTemplateVariables.Add("Comments", story.Value);
                objTemplateVariables.Add("SubscribeToNewsletter", subscribe.Checked ? "Yes" : "No");
                objTemplateVariables.Add("PublishDetails", "N/A");
                objTemplateVariables.Add("FeedbackSubject", "Your Story");
                objTemplateVariables.Add("FeedbackSubjectDetail", "Share Your Success");
                objTemplateVariables.Add("SubmissionDate", DateTime.Now.ToShortDateString());
                objTemplateVariables.Add("ImageSubmitted", ImageFileProvided ? "Yes" : "No");

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
            story.Value = string.Empty;
            clubFindSelect.SelectedIndex = 0;
            yourname.Value = string.Empty;
            email.Value = string.Empty;
            contact.Value = string.Empty;
            subscribe.Checked = false;
        }
    }
}