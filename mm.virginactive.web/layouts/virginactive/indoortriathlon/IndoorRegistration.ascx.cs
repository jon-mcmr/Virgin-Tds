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
using System.Text;
using mm.virginactive.webservices.virginactive.indoortriathlon;
using mm.virginactive.common.Globalization;
using Sitecore.Collections;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using Sitecore.Diagnostics;
using System.Web.Services;
using System.Net;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal.Common;
using mm.virginactive.screportdal;
using System.IO;
using Sitecore.SecurityModel;

namespace mm.virginactive.web.layouts.virginactive.indoortriathlon
{
    public partial class IndoorRegistration : System.Web.UI.UserControl
    {

        protected IndoorRegistrationItem currentItem = new IndoorRegistrationItem(Sitecore.Context.Item);
        protected Transaction transaction;
        protected string raceType;
        protected string vaStatus;
        protected string address;

        protected string twitterText;
        protected string twitterShareURL;
        protected string articleURL = "";
        protected string downloadPDFUrl = "";

        protected string currentItemUrl = "";

        protected ClubItem currentClub;
        public ClubItem CurrentClub
        {
            get { return currentClub; }
            set
            {
                currentClub = value;
            }
        }
        public Transaction Transaction
        {
            get { return transaction; }
            set
            {
                transaction = value;
            }
        }
        public string ArticleURL
        {
            get { return articleURL; }
            set
            {
                articleURL = value;
            }
        }

        protected static UserCredentials getCredentails()
        {
            UserCredentials userCredentials = new UserCredentials();
            userCredentials.userid = Settings.IndoorTriathlonServiceUsername; // "VARegistration";
            userCredentials.password = Settings.IndoorTriathlonServicePassword; // "cr34m t34";

            return userCredentials;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            currentItemUrl = Sitecore.Links.LinkManager.GetItemUrl(currentItem);

            //Add dynamic content
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/_plugins/validation/jquery.validate.js""></script>"));
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/_plugins/validation/additional-methods.js""></script>"));
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/indoortriathlon/indoor-tri.js""></script>"));

            twitterText = currentItem.TwitterText.Field.Value;
            twitterShareURL = currentItem.TwittershareURL.Field.Value; //dynamic twitter text and link added

            //Get Indoor Tri Home Page -(set to be the Facebook article page)
            Item homePage = Sitecore.Context.Database.GetItem(ItemPaths.IndoorTriathlonHomePage);

            if (homePage != null)
            {
                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                urlOptions.AlwaysIncludeServerUrl = true;
                urlOptions.AddAspxExtension = true;

                articleURL = Sitecore.Links.LinkManager.GetItemUrl(homePage, urlOptions);
            }

            if (!String.IsNullOrEmpty(currentItem.DownloadPDFLink.MediaUrl))
            {
                downloadPDFUrl = currentItem.DownloadPDFLink.MediaUrl; //confirmation page pdf download link
            }

            if (!Page.IsPostBack)
            {
                SetPage();               
            }

//            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
//
//var prm = Sys.WebForms.PageRequestManager.getInstance();
//
//prm.add_endRequest(function() {
//    indoorTry.formAfterPostBackHandler1;
//});
//                </script>"));

        }

        private void SetPage()
        {
            btnFindTimeSlot.Text = Translate.Text("Continue");
            btnSelectTimeSlot.Text = Translate.Text("Continue");
            btnPersonalDetails.Text = Translate.Text("Submit Now");
            btnResetTimeSlot.Text = Translate.Text("Reset");
            btnResetForm.Text = Translate.Text("Reset");
            cvFindTimeSlotUnavailable.ErrorMessage = Translate.Text("<p>Sorry, there are no times available for this date. Please select another time slot.</p>");
            cvFindTimeSlotError.ErrorMessage = Translate.Text("<p>Sorry, the booking system is not available right now - please try again later.</p>");
            cvSelectTimeSlotUnavailable.ErrorMessage = Translate.Text("<p>Sorry, there are no spaces left for this time. Please select another time slot.</p>");
            cvSelectTimeSlotError.ErrorMessage = Translate.Text("<p>Sorry, the booking system is not available right now - please try again later.</p>");
            cvSubmitSubscription.ErrorMessage = Translate.Text("<p>Sorry, the booking system is not available right now - please try again later.</p>");

            string val = Translate.Text("Please enter {0}");
            rfvFirstName.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a first name")));
            revFirstName.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid first name")));
            rfvSurname.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a surname")));
            revSurname.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid surname")));
            cvDateOfBirth.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid date of birth")));
            rfvAddressLine1.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("an address")));
            revAddressLine1.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid address")));
            //rfvAddressLine2.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("an address")));
            revAddressLine2.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid address")));
            rfvAddressLine3.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("an address")));
            revAddressLine3.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid address")));
            rfvPostcode.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a postcode")));
            revPostcode.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid postcode")));
            rfvPhoneNumber.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a phone number")));
            revPhoneNumber.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid phone number")));
            rfvEmail.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("an email address")));
            revEmail.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid email address")));
            rfvNOKFirstName.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a name")));
            revNOKFirstName.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid name")));
            rfvNOKSurname.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a surname")));
            revNOKSurname.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid surname")));
            rfvNOKRelationship.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("your relationship")));
            revNOKRelationship.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid relationship")));
            rfvNOKPhoneNumber.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a phone number")));
            revNOKPhoneNumber.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid phone number")));
            cvTermsAndConditions.ErrorMessage = "<p>Please agree to the terms and conditions</p>";
            //optional fields
            revFirstName2.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid first name")));
            revSurname2.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid surname")));
            revFirstName3.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid first name")));
            revSurname3.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid surname")));
            revTeamName.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid team name")));
            cvDateOfBirth2.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid date of birth")));
            cvDateOfBirth3.ErrorMessage = String.Format("<p>{0}</p>", String.Format(val, Translate.Text("a valid date of birth")));

            //regular expressions validators
            revFirstName.ValidationExpression = Settings.NameRegularExpression;
            revSurname.ValidationExpression = Settings.NameRegularExpression;
            revNOKFirstName.ValidationExpression = Settings.NameRegularExpression;
            revNOKSurname.ValidationExpression = Settings.NameRegularExpression;
            revFirstName2.ValidationExpression = Settings.NameRegularExpression;
            revSurname2.ValidationExpression = Settings.NameRegularExpression;
            revFirstName3.ValidationExpression = Settings.NameRegularExpression;
            revSurname3.ValidationExpression = Settings.NameRegularExpression;

            revAddressLine1.ValidationExpression = Settings.AddressLineRegularExpression;
            revAddressLine2.ValidationExpression = Settings.AddressLineRegularExpression;
            revAddressLine3.ValidationExpression = Settings.AddressLineRegularExpression;
            revPostcode.ValidationExpression = Settings.PostcodeRegularExpression;

            revEmail.ValidationExpression = Settings.EmailAddressRegularExpression;
            revPhoneNumber.ValidationExpression = Settings.PhoneNumberRegularExpression;

            revTeamName.ValidationExpression = Settings.AddressLineRegularExpression;
            revNOKRelationship.ValidationExpression = Settings.GeneralTextRegularExpression;
            revNOKPhoneNumber.ValidationExpression = Settings.PhoneNumberRegularExpression;


            //Radio Buttons
            radAMRaceTime.Text = Translate.Text("AM");
            radPMRaceTime.Text = Translate.Text("PM");
            radIndividual.Text = Translate.Text("Individual");
            radGroup.Text = Translate.Text("Team");
            radMember.Text = Translate.Text("Member");
            radNonMember.Text = Translate.Text("Non-member");
            radStaff.Text = Translate.Text("Staff");
            radMale.Text = Translate.Text("Male");
            radFemale.Text = Translate.Text("Female");
            radMale2.Text = Translate.Text("Male");
            radFemale2.Text = Translate.Text("Female");
            radMale3.Text = Translate.Text("Male");
            radFemale3.Text = Translate.Text("Female");

            clubFindSelect.Items.Clear();
            clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));

            ////Get the races from sitecore

            Item races = Sitecore.Context.Database.GetItem(ItemPaths.IndoorTriathlonRaces);

            if (races != null && races.HasChildren)
            {
                List<DropDownItem> raceList = races.Children.ToList().ConvertAll(X => new DropDownItem(X));

                int counter = 1;
                raceList.ForEach((delegate(DropDownItem race)
                {
                    var radioRace = this.FindControl("radRace" + counter.ToString()) as System.Web.UI.WebControls.RadioButton;
                    if (radioRace != null)
                    {
                        radioRace.Text = Translate.Text(race.Value.Rendered);
                    }
                    counter++;
                }));
            }

            ////Get the dates from sitecore
            drpRaceDate.Items.Clear();
            drpRaceDate.Items.Add(new ListItem(Translate.Text("Select a date"), ""));

            Item dates = Sitecore.Context.Database.GetItem(ItemPaths.IndoorTriathlonEventDates);

            if (dates != null && dates.HasChildren)
            {
                List<DateItem> dateList = dates.Children.ToList().ConvertAll(X => new DateItem(X));

                foreach (DateItem date in dateList)
                {
                    drpRaceDate.Items.Add(new ListItem(date.Dateentry.DateTime.ToString("dddd MMMM d, yyyy"), date.Dateentry.DateTime.ToString("dd/MM/yyyy")));
                }
            }

            try
            {
                //Bind clubs drop down list
                Registration vs = new Registration();               

                //Add Authentication
                //vs.PreAuthenticate = true;
                //NetworkCredential myCred = new NetworkCredential("VARegistration", "cr34m t34", "");
                //CredentialCache myCache = new CredentialCache();
                //myCache.Add(new Uri(vs.Url), "Basic", myCred);     
                //vs.Credentials = myCache;               

                //New code added - alternate way of passing user credentials
                vs.UserCredentialsValue = getCredentails();
                
                //Get the club names from webservice
                Transaction trans = vs.GetLocations();

                if (trans.Locations != null)
                {
                    foreach (Location location in trans.Locations)
                    {
                        if (!String.IsNullOrEmpty(location.Title))
                        {
                            ListItem lst = new ListItem(location.Title, location.Id.ToString());
                            clubFindSelect.Items.Add(lst);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error retrieving Indoor Triathlon clubs: {0}", ex.Message), this);

                //Error -Display message
                cvFindTimeSlotError.IsValid = false;

            }

            //Update Page
            pnlForm.Update();

        }

        protected void btnFindTimeSlot_Click(object sender, EventArgs e)
        {
            cvFindTimeSlotUnavailable.IsValid = true;
            cvFindTimeSlotError.IsValid = true;

            try
            {
                    //check if we have a valid club selected
                    Boolean blnClubSuccessfullySelected = clubFindSelect.SelectedIndex != 0;

                    if (blnClubSuccessfullySelected == true)
                    {
                        //Populate Transaction object
                        transaction = new Transaction();

                        int locationId = 0;
                        int.TryParse(clubFindSelect.SelectedValue, out locationId);

                        transaction.QueryLocationId = locationId;
                        transaction.QueryAM = radAMRaceTime.Checked;
                        transaction.QueryDate = Convert.ToDateTime(drpRaceDate.SelectedValue, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

                        if (radRace1.Checked)
                        {
                            transaction.QueryRaceId = 1;
                        }
                        else if (radRace2.Checked)
                        {
                            transaction.QueryRaceId = 2;
                        }
                        else if (radRace3.Checked)
                        {
                            transaction.QueryRaceId = 3;
                        }

                        Registration vs = new Registration();

                        //Add Authentication
                        //vs.PreAuthenticate = true;
                        //NetworkCredential myCred = new NetworkCredential("VARegistration", "cr34m t34");
                        //CredentialCache myCache = new CredentialCache();
                        //myCache.Add(new Uri(vs.Url), "Basic", myCred);
                        //vs.Credentials = myCache;

                        //New code added - alternate way of passing user credentials
                        vs.UserCredentialsValue = getCredentails();

                        transaction = vs.GetAvailability(transaction);

                        if (transaction.Waves != null && transaction.Waves.Count() > 0 && transaction.Status == TransactionStatus.OK)
                        {
                            drpTimeSlot.Items.Clear();

                            //Populate drop down
                            drpTimeSlot.Items.Add(new ListItem(Translate.Text("Select a time slot"), ""));

                            foreach (Wave wave in transaction.Waves)
                            {
                                drpTimeSlot.Items.Add(new ListItem(wave.GunTime.ToString("HH:mm"), wave.GunTime.ToString("HH:mm")));
                            }

                            //save results to session
                            Session["sess_Transaction"] = transaction;

                            //Update Page

                            string classNames;
                            classNames = pnlStep2.Attributes["class"];
                            pnlStep2.Attributes.Add("class", classNames.Replace(" hidden", ""));

                            pnlForm.Update();
                            return;
                        }
                        else if (transaction.Status == TransactionStatus.Unavailable)
                        {
                            //Display message
                            cvFindTimeSlotUnavailable.IsValid = false;

                            //Update Page
                            pnlForm.Update();
                            return;
                        }
                        else
                        {
                            Log.Error(String.Format("Error retrieving Indoor Triathlon time slots: {0}", transaction.Err_msg), this);
                        }
                    }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error retrieving Indoor Triathlon time slots: {0}", ex.Message), null);
            }

            //Error -Display message
            cvFindTimeSlotError.IsValid = false;

            //Update Page
            pnlForm.Update();
        }

        protected void btnSelectTimeSlot_Click(object sender, EventArgs e)
        {
            cvSelectTimeSlotUnavailable.IsValid = true;
            cvSelectTimeSlotError.IsValid = true;

            try
            {               

                    //check if we have a valid club selected
                    Boolean blnTimeSuccessfullySelected = clubFindSelect.SelectedIndex != 0;

                    if (blnTimeSuccessfullySelected == true && Session["sess_Transaction"] != null)
                    {
                        //load transaction
                        transaction = (Transaction)Session["sess_Transaction"];
                        //selected wave
                        DateTime selectedWaveDateTime = Convert.ToDateTime(drpRaceDate.SelectedValue.Trim() + " " + drpTimeSlot.SelectedValue.Trim() + ":00", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

                        //set transaction
                        foreach (Wave wave in transaction.Waves)
                        {
                            if (wave.GunTime == selectedWaveDateTime)
                            {
                                //assign wave to subscriber
                                transaction.Subscriber = new Athlete();
                                transaction.Subscriber.Wave = wave;

                                //assign race
                                transaction.Subscriber.Race = wave.Race;
                                Registration vs = new Registration();

                                //Add Authentication
                                //vs.PreAuthenticate = true;
                                //NetworkCredential myCred = new NetworkCredential("VARegistration", "cr34m t34");
                                //CredentialCache myCache = new CredentialCache();
                                //myCache.Add(new Uri(vs.Url), "Basic", myCred);
                                //vs.Credentials = myCache;

                                //New code added - alternate way of passing user credentials
                                vs.UserCredentialsValue = getCredentails();

                                transaction = vs.GetAvailability(transaction);

                                if (transaction.Status == TransactionStatus.OK)
                                {
                                    transaction = vs.Reserve(transaction);

                                    if (transaction.Status == TransactionStatus.OK)
                                    {

                                        Session["sess_Transaction"] = transaction;

                                        //Update Page
                                        string classNames;

                                        classNames = pnlStep3.Attributes["class"];
                                        pnlStep3.Attributes.Add("class", classNames.Replace(" hidden", ""));

                                        classNames = pnlStep4.Attributes["class"];
                                        pnlStep4.Attributes.Add("class", classNames.Replace(" hidden", ""));

                                        classNames = pnlStep5.Attributes["class"];
                                        pnlStep5.Attributes.Add("class", classNames.Replace(" hidden", ""));

                                        classNames = pnlStep6.Attributes["class"];
                                        pnlStep6.Attributes.Add("class", classNames.Replace(" hidden", ""));

                                        classNames = timeWarning.Attributes["class"];
                                        timeWarning.Attributes.Add("class", classNames.Replace(" hidden", ""));

                                        ////Test
                                        //Athlete subscriber = transaction.Subscriber;

                                        //subscriber.IsTeam = true;
                                        //subscriber.Type = AthleteType.Member;

                                        //subscriber.Firstname = "MyName";
                                        //subscriber.Lastname = "MySurname";
                                        //subscriber.Dob = Convert.ToDateTime("12/12/1980", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                                        //subscriber.Gender = AthleteGender.Male;

                                        //subscriber.Address1 = "MyAddress1";
                                        //subscriber.Address2 = "MyAddress2";
                                        //subscriber.City = "MyAddress3";
                                        //subscriber.Postcode = "sw11 1hh";

                                        //subscriber.Phone = "0123412341234";
                                        //subscriber.Email = "myTest@myTest2.com";

                                        ////Add Team Details

                                        ////Team name
                                        //subscriber.Team = "My Team";
                                        ////Add user 1
                                        //subscriber.Firstname2 = "MyTeamMateName";
                                        //subscriber.Lastname2 = "MyTeamMateSurname";
                                        //subscriber.Dob2 = Convert.ToDateTime("12/12/1978", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                                        //subscriber.Gender2 = AthleteGender.Male;

                                        ////Add Next Of Kin
                                        //subscriber.NOKName = "MyNextForename MyNextSurname";
                                        //subscriber.NOKRelation = "Brother";
                                        //subscriber.NOKPhone = "01234567890";

                                        //transaction.Subscriber = subscriber;

                                        ////Set user
                                        //User user = new User();
                                        //user.Firstname = "MyName";
                                        //user.Lastname = "MySurname";
                                        //user.Password = "";
                                        //user.Username = "myTest@myTest444.com";

                                        //transaction.User = user;

                                        ////Now subscribe
                                        ////Registration vs = new Registration();
                                        //transaction = vs.Subscribe(transaction);


                                        //End Test
                                        pnlForm.Update();
                                        return;
                                    }
                                    else if (transaction.Status == TransactionStatus.Unavailable)
                                    {
                                        //Display error
                                        cvSelectTimeSlotUnavailable.IsValid = false;

                                        //Update Page
                                        pnlForm.Update();
                                        return;
                                    }
                                    else
                                    {
                                        Log.Error(String.Format("Error Message from Indoor Tri webservice Reserve: {0}", transaction.Err_msg), this);
                                    }
                                }
                                else if (transaction.Status == TransactionStatus.Unavailable)
                                {
                                    //Display error
                                    cvSelectTimeSlotUnavailable.IsValid = false;

                                    //Update Page
                                    pnlForm.Update();
                                    return;
                                }
                                else
                                {
                                    Log.Error(String.Format("Error Message from Indoor Tri webservice GetAvailability: {0}", transaction.Err_msg), this);
                                }
                                break;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error reserving Indoor Triathlon selecting time slots: {0}", ex.Message), null);

                //Error -Display message
                cvSelectTimeSlotError.IsValid = false;
            }

            //Error -Display message
            cvSelectTimeSlotError.IsValid = false;

            //Update Page
            pnlForm.Update();
        }

        //Confirmation panel
        protected void btnPersonalDetails_Click(object sender, EventArgs e)
        {
            cvSubmitSubscription.ErrorMessage = Translate.Text("<p>Sorry, the booking system is not available right now - please try again later.</p>");
            cvSubmitSubscription.IsValid = true;

            //custom validation

            //check dob is fine
            DateTime datDateOfBirth;
            if (!DateTime.TryParse(txtDOBDay.Value.Trim() + "/" + txtDOBMonth.Value.Trim() + "/" + txtDOBYear.Value.Trim(), out datDateOfBirth))
            {
                cvDateOfBirth.IsValid = false;
            }
            //check dob2 is fine
            if ((txtDOBDay2.Value == "" && txtDOBMonth2.Value == "" && txtDOBYear2.Value == "") || (txtDOBDay2.Value == "DD" && txtDOBMonth2.Value == "MM" && txtDOBYear2.Value == "YYYY"))
            {
                //valid
            }
            else
            {
                //check date is fine
                if (!DateTime.TryParse(txtDOBDay2.Value.Trim() + "/" + txtDOBMonth2.Value.Trim() + "/" + txtDOBYear2.Value.Trim(), out datDateOfBirth))
                {
                    cvDateOfBirth2.IsValid = false;
                }
            }
            //check dob3 is fine
            if ((txtDOBDay3.Value == "" && txtDOBMonth3.Value == "" && txtDOBYear3.Value == "") || (txtDOBDay3.Value == "DD" && txtDOBMonth3.Value == "MM" && txtDOBYear3.Value == "YYYY"))
            {
                //valid
            }
            else
            {
                //check date is fine
                if (!DateTime.TryParse(txtDOBDay3.Value.Trim() + "/" + txtDOBMonth3.Value.Trim() + "/" + txtDOBYear3.Value.Trim(), out datDateOfBirth))
                {
                    cvDateOfBirth3.IsValid = false;
                }
            }
            //check Terms and Conditions
            cvTermsAndConditions.IsValid = chkTermsAndConditions.Checked;


            try
            {

                if (Page.IsValid == true)
                {
                    //Background data
                    if (radRace1.Checked)
                    {
                        raceType = radRace1.Text;
                    }
                    else if (radRace2.Checked)
                    {
                        raceType = radRace2.Text;
                    }
                    else
                    {
                        raceType = radRace3.Text;
                    }

                    if (radMember.Checked)
                    {
                        vaStatus = radMember.Text;
                    }
                    else if (radNonMember.Checked)
                    {
                        vaStatus = radNonMember.Text;
                    }
                    else
                    {
                        vaStatus = radStaff.Text;
                    }

                    //Save form details to report Db
                    SaveDataToReportDB();

                    //check if we have a data stored

                    if (Session["sess_Transaction"] != null)
                    {

                        //load transaction
                        transaction = (Transaction)Session["sess_Transaction"];

                        //Set transaction object

                        Athlete subscriber = transaction.Subscriber;

                        subscriber.IsTeam = radGroup.Checked ? true : false;

                        if (radMember.Checked)
                        {
                            subscriber.Type = AthleteType.Member;
                        }
                        else if (radNonMember.Checked)
                        {
                            subscriber.Type = AthleteType.Non_Member;
                        }
                        else if (radStaff.Checked)
                        {
                            subscriber.Type = AthleteType.Staff;
                        }

                        subscriber.Firstname = txtFirstName.Value.Trim();
                        subscriber.Lastname = txtSurname.Value.Trim();
                        subscriber.Dob = Convert.ToDateTime(txtDOBDay.Value.Trim() + "/" + txtDOBMonth.Value.Trim() + "/" + txtDOBYear.Value.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                        subscriber.Gender = radMale.Checked ? AthleteGender.Male : AthleteGender.Female;

                        subscriber.Address1 = txtAddressLine1.Value.Trim();
                        subscriber.Address2 = txtAddressLine2.Value.Trim() != "Address Line 2" ? txtAddressLine2.Value.Trim() : "";
                        //subscriber.Address3 = txtAddressLine3.Value.Trim();
                        subscriber.City = txtAddressLine3.Value.Trim();
                        //subscriber.County = txtAddressLine3.Value.Trim();
                        subscriber.Postcode = txtPostcode.Value.Trim();

                        subscriber.Phone = txtPhoneNumber.Value.Trim();
                        subscriber.Email = txtEmail.Value.Trim();

                        //Add Team Details

                        if (radGroup.Checked)
                        {
                            //Team name
                            subscriber.Team = txtTeamName.Value.Trim();
                            //Add user 1
                            subscriber.Firstname2 = txtFirstName2.Value.Trim();
                            subscriber.Lastname2 = txtSurname2.Value.Trim();
                            subscriber.Dob2 = Convert.ToDateTime(txtDOBDay2.Value.Trim() + "/" + txtDOBMonth2.Value.Trim() + "/" + txtDOBYear2.Value.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                            subscriber.Gender2 = radMale2.Checked ? AthleteGender.Male : AthleteGender.Female;

                            //Add user 2
                            if (txtSurname3.Value.Trim() != ""  && txtSurname3.Value.Trim() != "Last")
                            {
                                subscriber.Firstname3 = txtFirstName3.Value.Trim();
                                subscriber.Lastname3 = txtSurname3.Value.Trim();
                                subscriber.Dob3 = Convert.ToDateTime(txtDOBDay3.Value.Trim() + "/" + txtDOBMonth3.Value.Trim() + "/" + txtDOBYear3.Value.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                                subscriber.Gender3 = radMale3.Checked ? AthleteGender.Male : AthleteGender.Female;
                            }
                        }

                        //Add Next Of Kin
                        subscriber.NOKName = txtNOKFirstName.Value.Trim() + " " + txtNOKSurname.Value.Trim();
                        subscriber.NOKRelation = txtNOKRelationship.Value.Trim();
                        subscriber.NOKPhone = txtNOKPhoneNumber.Value.Trim();

                        //address
                        System.Text.StringBuilder markupBuilder;
                        markupBuilder = new System.Text.StringBuilder();
                        markupBuilder.Append(subscriber.Address1 + "<br />");
                        markupBuilder.Append(!String.IsNullOrEmpty(subscriber.Address2) ? " " + subscriber.Address2 + "<br />" : "");
                        markupBuilder.Append(subscriber.City + "<br />");
                        markupBuilder.Append(subscriber.Postcode + "<br />");
                        address = markupBuilder.ToString();

                        transaction.Subscriber = subscriber;

                        //Set user
                        User user = new User();
                        user.Firstname = txtFirstName.Value.Trim();
                        user.Lastname = txtSurname.Value.Trim();
                        user.Password = "";
                        user.Username = txtEmail.Value.Trim();

                        transaction.User = user;

                        //Now subscribe
                        Registration vs = new Registration();

                        //Add Authentication
                        //vs.PreAuthenticate = true;
                        //NetworkCredential myCred = new NetworkCredential("VARegistration", "cr34m t34");
                        //CredentialCache myCache = new CredentialCache();
                        //myCache.Add(new Uri(vs.Url), "Basic", myCred);
                        //vs.Credentials = myCache;

                        //New code added - alternate way of passing user credentials
                        vs.UserCredentialsValue = getCredentails();

                        transaction = vs.Subscribe(transaction);

                        if (transaction.Status == TransactionStatus.OK)
                        {
                            //Save to session
                            Session["sess_Transaction"] = transaction;

                            string classNames;
                            classNames = pnlConfirmation.Attributes["class"];
                            pnlConfirmation.Attributes.Add("class", classNames.Replace(" hidden", ""));

                            classNames = confirmationFooter.Attributes["class"];
                            confirmationFooter.Attributes.Add("class", classNames.Replace(" hidden", ""));

                            classNames = formIntro.Attributes["class"];
                            formIntro.Attributes.Add("class", classNames + " hidden");

                            classNames = pnlStep1.Attributes["class"];
                            pnlStep1.Attributes.Add("class", classNames + " hidden");

                            classNames = pnlStep2.Attributes["class"];
                            pnlStep2.Attributes.Add("class", classNames + " hidden");

                            classNames = pnlStep3.Attributes["class"];
                            pnlStep3.Attributes.Add("class", classNames + " hidden");

                            classNames = pnlStep4.Attributes["class"];
                            pnlStep4.Attributes.Add("class", classNames + " hidden");

                            classNames = pnlStep5.Attributes["class"];
                            pnlStep5.Attributes.Add("class", classNames + " hidden");

                            classNames = pnlStep6.Attributes["class"];
                            pnlStep6.Attributes.Add("class", classNames + " hidden");

                            classNames = timeWarning.Attributes["class"];
                            timeWarning.Attributes.Add("class", classNames + " hidden");

                            pnlForm.Update();
                            return;
                        }
                        else
                        {
                            Log.Error(String.Format("Error Message from Indoor Tri webservice Subscribe: {0}", transaction.Err_msg), this);
                        }
                    }
                }
                else
                {
                    cvSubmitSubscription.ErrorMessage = Translate.Text("<p>Errors on the page, please check and try again</p>");
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error subscribing Indoor Tri webservice: {0}", ex.Message), this);

            }

            //hide/show group panel

            if (radGroup.Checked)
            {
                string classNames = pnlStep4.Attributes["class"];
                pnlStep4.Attributes.Add("class", classNames.Replace(" hidden", ""));
            }

            //Display error message
            cvSubmitSubscription.IsValid = false;
            pnlForm.Update();

        }

        protected void btnResetTimeSlot_Click(object sender, EventArgs e)
        {
            ////Reset drop down
            //drpTimeSlot.ClearSelection();
            //drpTimeSlot.Items.Add(new ListItem(Translate.Text("Select a time slot"), ""));

            ////Reset panel 1
            //clubFindSelect.SelectedIndex = 0;
            //radRace1.Checked = false;
            //radRace2.Checked = false;
            //radRace3.Checked = false;
            //drpRaceDate.SelectedIndex = 0;
            //radAMRaceTime.Checked = false;
            //radPMRaceTime.Checked = false;

            ////Update panel styles
            //string classNames;
            //classNames = pnlStep2.Attributes["class"];
            //pnlStep2.Attributes.Add("class", classNames + " hidden");

            //pnlForm.Update();

            ////Clear Session Data

            transaction = null;
            if (HttpContext.Current.Session["sess_Transaction"] != null)
            {
                HttpContext.Current.Session.Remove("sess_Transaction");
            }

            //N.B. Can not reload page in an ajax panel

            //Page.ClientScript.RegisterStartupScript(
            //  this.GetType(), "redirect",
            //  "window.location.href='" + currentItem.PageSummary.QualifiedUrl + "';", true);

            Response.Redirect(currentItem.PageSummary.Url);
        }

        protected void btnResetForm_Click(object sender, EventArgs e)
        {
            ////Reset panel 1
            //clubFindSelect.SelectedIndex = 0;
            //radRace1.Checked = false;
            //radRace2.Checked = false;
            //radRace3.Checked = false;
            //drpRaceDate.SelectedIndex = 0;
            //radAMRaceTime.Checked = false;
            //radPMRaceTime.Checked = false;

            //cvFindTimeSlotUnavailable.IsValid = true;
            //cvFindTimeSlotError.IsValid = true;

            ////Reset panel 2
            //drpTimeSlot.ClearSelection();
            //drpTimeSlot.Items.Add(new ListItem(Translate.Text("Select a time slot"), ""));

            //cvSelectTimeSlotUnavailable.IsValid = true;
            //cvSelectTimeSlotError.IsValid = true;

            ////Reset Personal Details
            //radIndividual.Checked = false;
            //radGroup.Checked = false;
            //radMember.Checked = false;
            //radNonMember.Checked = false;
            //radStaff.Checked = false;
            //txtFirstName.Value = "";
            //txtSurname.Value = "";
            //txtDOBDay.Value = "";
            //txtDOBMonth.Value = "";
            //txtDOBYear.Value = "";
            //radMale.Checked = false;
            //radFemale.Checked = false;
            //txtAddressLine1.Value = "";
            //txtAddressLine2.Value = "";
            //txtAddressLine3.Value = "";
            //txtPostcode.Value = "";
            //txtPhoneNumber.Value = "";
            //txtEmail.Value = "";
            //txtTeamName.Value = "";
            //txtFirstName2.Value = "";
            //txtFirstName3.Value = "";
            //txtSurname2.Value = "";
            //txtSurname3.Value = "";
            //txtDOBDay2.Value = "";
            //txtDOBDay3.Value = "";
            //txtDOBMonth2.Value = "";
            //txtDOBMonth3.Value = "";
            //txtDOBYear2.Value = "";
            //txtDOBYear3.Value = "";
            //radMale2.Checked = false;
            //radMale3.Checked = false;
            //radFemale2.Checked = false;
            //radFemale3.Checked = false;
            //txtNOKFirstName.Value = "";
            //txtNOKSurname.Value = "";
            //txtNOKRelationship.Value = "";
            //txtNOKPhoneNumber.Value = "";
            //chkTermsAndConditions.Checked = false;
            //chkParentalConsent.Checked = false;

            //cvSubmitSubscription.IsValid = true;

            ////Update panel styles
            //string classNames;

            //classNames = pnlStep1.Attributes["class"];
            //pnlStep1.Attributes.Add("class", classNames.Replace(" hidden", ""));

            //classNames = pnlStep2.Attributes["class"];
            //pnlStep2.Attributes.Add("class", classNames + " hidden");

            //classNames = pnlStep1.Attributes["class"];
            //pnlStep1.Attributes.Add("class", classNames.Replace(" hidden", ""));

            //classNames = formIntro.Attributes["class"];
            //formIntro.Attributes.Add("class", classNames + " hidden");

            //classNames = pnlStep1.Attributes["class"];
            //pnlStep1.Attributes.Add("class", classNames + " hidden");

            //classNames = pnlStep2.Attributes["class"];
            //pnlStep2.Attributes.Add("class", classNames + " hidden");

            //classNames = pnlStep3.Attributes["class"];
            //pnlStep3.Attributes.Add("class", classNames + " hidden");

            //classNames = pnlStep4.Attributes["class"];
            //pnlStep4.Attributes.Add("class", classNames + " hidden");

            //classNames = pnlStep5.Attributes["class"];
            //pnlStep5.Attributes.Add("class", classNames + " hidden");

            //classNames = pnlStep6.Attributes["class"];
            //pnlStep6.Attributes.Add("class", classNames + " hidden");

            //classNames = timeWarning.Attributes["class"];
            //timeWarning.Attributes.Add("class", classNames + " hidden");

            ////Clear Session Data
            transaction = null;
            if (HttpContext.Current.Session["sess_Transaction"] != null)
            {
                HttpContext.Current.Session.Remove("sess_Transaction");
            }

            Response.Redirect(currentItem.PageSummary.Url);

            //pnlForm.Update();
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
                //objCustomer.JobTitle = txtJobTitle.Value.Trim();
                //objCustomer.CompanyName = txtCompanyName.Value.Trim();
                //objCustomer.CompanyWebsite = txtCompanyWebsite.Value.Trim();
                //objCustomer.CompanyLocation = txtLocation.Value.Trim();
                //objCustomer.HomeClubID = currentClub.ClubId.Rendered;
                //objCustomer.SubscribeToNewsletter = chkSubscribe.Checked;
                objCustomer.TelephoneNumber = txtPhoneNumber.Value.Trim();
                objCustomer.AddressLine1 = txtAddressLine1.Value.Trim();
                objCustomer.AddressLine2 = txtAddressLine2.Value.Trim() != "Address Line 2" ? txtAddressLine2.Value.Trim() : "";
                objCustomer.AddressLine3 = txtAddressLine3.Value.Trim();
                objCustomer.Postcode = txtPostcode.Value.Trim();
                objCustomer.DateOfBirth = Convert.ToDateTime(txtDOBDay.Value.Trim() + "/" + txtDOBMonth.Value.Trim() + "/" + txtDOBYear.Value.Trim(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                objCustomer.Gender = radMale.Checked ? "M" : "F";
                objFeedback.Customer = objCustomer;
                objFeedback.FeedbackSubject = "Indoor Triathlon Registration";
                objFeedback.FeedbackSubjectDetail = "Virgin Active Website";
                objFeedback.FeedbackTypeID = Constants.FeedbackType.IndoorTriathlonRegistration;
                //objFeedback.PrimaryClubID = currentClub.ClubId.Rendered;
                objFeedback.SubmissionDate = DateTime.Now;

                //Add Comment
                Comment objComment = new Comment();

                objComment.CommentDetail = clubFindSelect.SelectedItem.Text;
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 1;
                objComment.Subject = "Club";

                List<Comment> objComments = new List<Comment>();
                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = raceType;
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 2;
                objComment.Subject = "Race";

                objComments.Add(objComment);

                objComment = new Comment();
                //objComment.CommentDetail = drpExistingMembers.SelectedValue.ToString() != Translate.Text("Select") ? drpExistingMembers.SelectedValue.ToString() : "";
                objComment.CommentDetail = drpRaceDate.SelectedValue;
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 3;
                objComment.Subject = "Race Day";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = drpTimeSlot.SelectedValue;
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 4;
                objComment.Subject = "Time Slot";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = radIndividual.Checked ? "Individual" : "Team";
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 5;
                objComment.Subject = "Individual or Team";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = vaStatus;
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 6;
                objComment.Subject = "Virgin Active Status";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtTeamName.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 7;
                objComment.Subject = "Team Name";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtFirstName2.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 8;
                objComment.Subject = "Team Member 2 First Name";

                objComment = new Comment();
                objComment.CommentDetail = txtSurname2.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 9;
                objComment.Subject = "Team Member 2 Surname";

                objComments.Add(objComment);

                objComment = new Comment();
                if(txtDOBDay2.Value != "DD")
                {
                    objComment.CommentDetail = txtDOBDay2.Value.Trim() != "" ? txtDOBDay2.Value.Trim() + "/" + txtDOBMonth2.Value.Trim() + "/" + txtDOBYear2.Value.Trim() : "";
                }
                else
                {
                    objComment.CommentDetail = "";
                }
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 10;
                objComment.Subject = "Team Member 2 Date of Birth";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = radMale2.Checked ? "M" : radFemale2.Checked ? "F" : "";
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 11;
                objComment.Subject = "Team Member 2 Gender";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtFirstName3.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 12;
                objComment.Subject = "Team Member 3 First Name";

                objComment = new Comment();
                objComment.CommentDetail = txtSurname3.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 13;
                objComment.Subject = "Team Member 3 Surname";

                objComments.Add(objComment);

                objComment = new Comment();
                if (txtDOBDay3.Value != "DD")
                {
                    objComment.CommentDetail = txtDOBDay3.Value.Trim() != "" ? txtDOBDay3.Value.Trim() + "/" + txtDOBMonth3.Value.Trim() + "/" + txtDOBYear3.Value.Trim() : "";
                }
                else
                {
                    objComment.CommentDetail = "";
                }
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 14;
                objComment.Subject = "Team Member 3 Date of Birth";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = radMale3.Checked ? "M" : radFemale3.Checked ? "F" : "";
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 15;
                objComment.Subject = "Team Member 3 Gender";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtNOKFirstName.Value.Trim() + " " + txtNOKSurname.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 16;
                objComment.Subject = "Next of Kin Name";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtNOKRelationship.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 17;
                objComment.Subject = "Next of Kin Relationship";

                objComments.Add(objComment);

                objComment = new Comment();
                objComment.CommentDetail = txtNOKPhoneNumber.Value.Trim();
                objComment.CommentImageTypeID = Constants.CommentImageType.QuestionnaireAnswer;
                objComment.SortOrder = 18;
                objComment.Subject = "Next of Kin Phone Number";

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
            }

            return blnDataSaved;
        }
    }
}