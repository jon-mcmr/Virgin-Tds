using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using System.Collections.Specialized;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Globalization;
using System.Web.UI.HtmlControls;

namespace mm.virginactive.web.layouts.virginactive.presentationstructures
{
    public partial class ClubLayout : System.Web.UI.UserControl
    {

        protected ClubItem club;
        protected bool oneColumn = false;
        protected string Panels = "";

        public bool OneColumn
        {
            get { return oneColumn; }
            set { oneColumn = value; }
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {
            club = SitecoreHelper.GetCurrentClub(Sitecore.Context.Item);
            Club current = new Club(club.InnerItem);
            //Set Esporta Flag or classic club flag
            if (current.IsClassic) //Classic club flag overrides exesporta
            {
                EsportaFlag.Text = String.Format(@"<span class=""prev-esporta""><span class=""prev-by"">{0}</span> {1}</span>", Translate.Text("by"), Translate.Text("VIRGIN ACTIVE CLASSIC"));
            } else if (club.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric)
            {
                if (EsportaFlag != null)
                {
                    EsportaFlag.Text = @"<span class=""prev-esporta""> <span class=""prev-by"">" + Translate.Text("previously") + "</span> " + Translate.Text("ESPORTA") + "</span>";
                }
            }

            //Set club last visited 
            User objUser = new User();
            if (Session["sess_User"] != null)
            {
                objUser = (User)Session["sess_User"];
            }
            if (objUser.ShowGallery)
            {
                HtmlGenericControl BodyTag = (HtmlGenericControl)this.Page.FindControl("BodyTag");
                string classNames = BodyTag.Attributes["class"] != null ? BodyTag.Attributes["class"] : "";
                BodyTag.Attributes.Add("class", classNames.Length > 0 ? classNames + " gallery_open" : "gallery_open");
            }
            objUser.ClubLastVisitedID = club.ClubId.Rendered;
            Session["sess_User"] = objUser;

            //Set club last visited cookie
            if (objUser.Preferences != null)
            {
                if (objUser.Preferences.PersonalisedCookies)
                {
                    CookieHelper.AddClubsLastVisitedCookie(CookieKeyNames.ClubLastVisited, club.ClubId.Rendered);
                }
            }

            //Retrieve CSS class
            string rawParameters = Attributes["sc_parameters"];
            NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
            if (!String.IsNullOrEmpty(parameters["OneColumn"]))
            {
                OneColumn = Convert.ToBoolean(parameters["OneColumn"]);
            }

            if (!String.IsNullOrEmpty(parameters["HasPanels"]))
            {
                Panels = Convert.ToBoolean(parameters["HasPanels"]) ? "-panels" : "";
            }

            //Add club name to page title     
            string clubNameTitle = String.Format(" - {0}", club.Clubname.Raw);
            clubNameTitle = HtmlRemoval.StripTagsCharArray(clubNameTitle);

            Page.Title = Page.Title + clubNameTitle;

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
	            $(function(){
		            $('.va-overlay-link').vaOverlay({showMap:true });
                    $(""#carousel"").orbit({directionalNav:true, viewGallery:true });
                    $.va_init.functions.clubsSetup();
	            });
                </script>"));


            
        }
    }
}