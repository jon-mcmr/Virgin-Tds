using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Diagnostics;
using mm.virginactive.common.Globalization;
using Newtonsoft.Json;
using System.IO;
using mm.virginactive.common.Helpers;
using System.Globalization;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class MemberDetails : System.Web.UI.UserControl
    {
        protected string PersonalDetails = "";
        protected string swipeNumber = "";
        protected string dateOfBirth = "";
        protected string postcode = "";
        protected string errorMessage = "''";

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public string SwipeNumber
        {
            get { return swipeNumber; }
            set { swipeNumber = value; }
        }

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string Postcode
        {
            get { return postcode; }
            set { postcode = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool blnIsValid = true;

            //Do validation
            dateOfBirth = dateOfBirth.Replace("-", "/");

            DateTime datDateOfBirth;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!DateTime.TryParse(dateOfBirth, culture, styles, out datDateOfBirth))
            {
                blnIsValid = false;
            }

            if (swipeNumber == "" || dateOfBirth == "" || postcode == "")
            {
                blnIsValid = false;
            }

            if(blnIsValid)
            {
                try
                {

                    //Authenticate member on membership id, postcode and date of birth

                    mm.virginactive.webservices.virginactive.memberdetails.MemberDetails memberService = new mm.virginactive.webservices.virginactive.memberdetails.MemberDetails();
                    mm.virginactive.webservices.virginactive.memberdetails.MemberData member = memberService.GetMemberDetails(swipeNumber, datDateOfBirth.ToString("dd/MM/yyyy"), postcode);

                    if (member != null)
                    {
                        //member has been authenticated successfully                    
                        StringWriter result = new StringWriter();

                        using (JsonTextWriter w = new JsonTextWriter(result))
                        {
                            w.WriteStartArray();
                            WriteClubAsJson(w,
                                member.RecordID.ToString().Trim(),
                                member.MemberFirstName.Trim(),
                                member.MemberSurname.Trim(),
                                member.MemberAddress1.Trim(),
                                member.MemberAddress2.Trim(),
                                member.MemberAddress3.Trim(),
                                member.MemberAddress4.Trim(),
                                member.MemberAddress5.Trim(),
                                member.MemberPostCode.Trim(),
                                member.MemberHomePhone.Trim(),
                                member.MemberWorkPhone.Trim(),
                                member.MemberMobilePhone.Trim(),
                                member.MemberEmail.Trim(),
                                member.PrefMarketingByMail.ToString());
                            w.WriteEndArray();

                            PersonalDetails = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(String.Format("Error retrieving personal members data from web service: {0}", ex.Message), this);
                    errorMessage = String.Format("<p>{0}</p>", Translate.Text("Ah, sorry, bit of a glitch identifying your membership there. We're sure it's not a problem - just talk to the team at your club who will help you out in double-quick time."));
                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);

                    PersonalDetails = "";
                }
            }
        }

        private void WriteClubAsJson(JsonTextWriter writer, 
            string recordId,
            string firstName,
            string surname,
            string address1,
            string address2,
            string address3,
            string address4,
            string address5,
            string postcode,
            string homePhone,
            string workPhone,
            string mobilePhone,
            string email,
            string marketingByMail)
        {

            writer.WriteStartObject();      //  {
            writer.WritePropertyName("recordId"); //      "value" : 
            writer.WriteValue(recordId);   //          "...", 
            writer.WritePropertyName("firstName");   //      "label" :
            writer.WriteValue(firstName);     //          "..."
            writer.WritePropertyName("surname"); //      "value" : 
            writer.WriteValue(surname);   //          "...", 
            writer.WritePropertyName("address1"); //      "value" : 
            writer.WriteValue(address1);   //          "...", 
            writer.WritePropertyName("address2"); //      "value" : 
            writer.WriteValue(address2);   //          "...", 
            writer.WritePropertyName("address3"); //      "value" : 
            writer.WriteValue(address3);   //          "...", 
            writer.WritePropertyName("address4"); //      "value" : 
            writer.WriteValue(address4);   //          "...", 
            writer.WritePropertyName("address5"); //      "value" : 
            writer.WriteValue(address5);   //          "...", 
            writer.WritePropertyName("postcode"); //      "value" : 
            writer.WriteValue(postcode);   //          "...", 
            writer.WritePropertyName("homePhone"); //      "value" : 
            writer.WriteValue(homePhone);   //          "...", 
            writer.WritePropertyName("workPhone"); //      "value" : 
            writer.WriteValue(workPhone);   //          "...", 
            writer.WritePropertyName("mobilePhone"); //      "value" : 
            writer.WriteValue(mobilePhone);   //          "...", 
            writer.WritePropertyName("email"); //      "value" : 
            writer.WriteValue(email);   //          "...", 
            writer.WritePropertyName("marketingByMail"); //      "value" : 
            writer.WriteValue(marketingByMail);   //          "...", 
            writer.WriteEndObject();        //  }
        }

        public static string RenderToString(string swipeNo, string dateOfBirth, string postcode)
        {
            return SitecoreHelper.RenderUserControl<MemberDetails>("~/layouts/virginactive/ajax/MemberDetails.ascx",
                uc =>
                {
                    uc.SwipeNumber = swipeNo;
                    uc.DateOfBirth = dateOfBirth;
                    uc.Postcode = postcode;
                });
        }
    }
}