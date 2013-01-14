using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Helpers;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using System.Text.RegularExpressions;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class ClubContactDetails : System.Web.UI.UserControl
    {
        protected string clubId = "";
        protected string contactAddress = "";
        protected ClubItem club = null;
        protected string errorMessage = "''";

        public ClubItem Club
        {
            get { return club; }
            set { club = value; }
        }

        public string ClubID
        {
            get { return clubId; }
            set { clubId = value; }
        }

        public string ContactAddress
        {
            get { return contactAddress; }
            set { contactAddress = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Get club url
                club = Sitecore.Context.Database.GetItem(clubId);
                if (club != null)
                {
                    //Format Contact Address
                    System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

                    markupBuilder.Append(club.Addressline1.Rendered != "" ? club.Addressline1.Rendered + "<br/>" : "");
                    markupBuilder.Append(club.Addressline2.Rendered != "" ? club.Addressline2.Rendered + "<br/>" : "");
                    markupBuilder.Append(club.Addressline3.Rendered != "" ? club.Addressline3.Rendered + "<br/>" : "");
                    markupBuilder.Append(club.Addressline4.Rendered != "" ? club.Addressline4.Rendered + "<br/>" : "");
                    markupBuilder.Append(club.Postcode.Rendered);

                    contactAddress = markupBuilder.ToString();
                }
                else
                {
                    Log.Error(String.Format("Error retrieving club data"), this);
                    errorMessage = String.Format("<p>{0}</p>", "Error retrieving club data");
                }
            }
            catch(Exception ex)
            {
                Log.Error(String.Format("Error retrieving club data {0}", ex.Message), this);
                errorMessage = String.Format("<p>{0}</p>", "Error retrieving club data");
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        public static string RenderToString(string clubId)
        {
            return SitecoreHelper.RenderUserControl<ClubContactDetails>("~/layouts/virginactive/ajax/ClubContactDetails.ascx",
                uc =>
                {
                    uc.ClubID = clubId;
                });
        }
    }
}