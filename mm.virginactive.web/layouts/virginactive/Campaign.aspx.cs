using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class Campaign : System.Web.UI.Page
    {
		protected CampaignItem campaign = new CampaignItem(Sitecore.Context.Item);
        protected PageSummaryItem privacy = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.TermsAndConditions));
        protected Boolean newSession = false;            

        protected void Page_Load(object sender, EventArgs e)
        {

            //THIS IS WHERE WE HIDE/SHOW COOKIE MESSAGE -N.B. User Session initialised and set in Header

            //Interorgate the request 'Do Not Track' settings.
            HttpContext objContext = HttpContext.Current;
            bool headerDoNotTrack = false;

            if (!string.IsNullOrEmpty(objContext.Request.Headers["DNT"]))
            {
                headerDoNotTrack = objContext.Request.Headers["DNT"] == "1" ? true : false;
            }

            User objUser = new User();

            //READ/SET COOKIE OPTIONS
            if (Session["sess_User"] == null)
            {
                newSession = true;
            }
            else
            {
                objUser = (User)Session["sess_User"];
            }

            //If this has not been set -we need to show the cookie message
            if (!headerDoNotTrack)
            {
                //Check if User Session object has been set -i.e. not first page load
                if (!newSession)
                {

                    //Have the cookie preferences been set?
                    if (objUser.Preferences == null)
                    {
                        //Show Cookie Preferences ribbon
                        CookieRibbon cookieDeclaration = Page.LoadControl("~/layouts/virginactive/CookieRibbon.ascx") as CookieRibbon;
                        RibbonPh.Controls.Add(cookieDeclaration);

                        string classNames = BodyTag.Attributes["class"] != null ? BodyTag.Attributes["class"] : "";
                        BodyTag.Attributes.Add("class", classNames.Length > 0 ? classNames + " cookie-show" : "cookie-show");
                    }
                }
                else
                {
                    //Seesion info not set (as it's a new session) -Check the 'Cookie Preferences Cookie' exists
                    if (string.IsNullOrEmpty(CookieHelper.GetOptInCookieValue(CookieKeyNames.MarketingCookies)))
                    {
                        //User has NOT stored settings
                        //Show Cookie Preferences ribbon
                        CookieRibbon cookieDeclaration = Page.LoadControl("~/layouts/virginactive/CookieRibbon.ascx") as CookieRibbon;
                        RibbonPh.Controls.Add(cookieDeclaration);

                        string classNames = BodyTag.Attributes["class"] != null ? BodyTag.Attributes["class"] : "";
                        BodyTag.Attributes.Add("class", classNames.Length > 0 ? classNames + " cookie-show" : "cookie-show");
                    }
                }
            }

            //READ/SET COOKIE OPTIONS
            if (newSession)
            {
                //New Session

                //Check if cookie preferences session cookie exists
                if (!string.IsNullOrEmpty(CookieHelper.GetOptInCookieValue(CookieKeyNames.MarketingCookies)))
                {
                    Preferences preferences = new Preferences();

                    preferences.MarketingCookies = CookieHelper.GetOptInCookieValue(CookieKeyNames.MarketingCookies) == "Y" ? true : false;
                    preferences.MetricsCookies = CookieHelper.GetOptInCookieValue(CookieKeyNames.MetricsCookies) == "Y" ? true : false;
                    preferences.PersonalisedCookies = CookieHelper.GetOptInCookieValue(CookieKeyNames.PersonalisedCookies) == "Y" ? true : false;
                    preferences.SocialCookies = CookieHelper.GetOptInCookieValue(CookieKeyNames.SocialCookies) == "Y" ? true : false;

                    //Store to session
                    objUser.Preferences = preferences;
                }
                else
                {
                    if (headerDoNotTrack)
                    {
                        //Set Preferences in User Session -default to N
                        Preferences preferences = new Preferences();

                        preferences.MarketingCookies = false;
                        preferences.MetricsCookies = false;
                        preferences.PersonalisedCookies = false;
                        preferences.SocialCookies = false;

                        objUser.Preferences = preferences;

                        //Set Cookie Preferences Cookie
                        CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MarketingCookies, "N");
                        CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MetricsCookies, "N");
                        CookieHelper.AddUpdateOptInCookie(CookieKeyNames.PersonalisedCookies, "N");
                        CookieHelper.AddUpdateOptInCookie(CookieKeyNames.SocialCookies, "N");

                        //Delete Existing Personalisation Cookie
                        CookieHelper.DeleteCookie();
                    }
                }
            }

            //DEFAULT PREFERENCES IF NOT SET
            if (objUser.Preferences == null)
            {
                //Set preferences in User Session -default to Y
                Preferences preferences = new Preferences();

                preferences.MarketingCookies = true;
                preferences.MetricsCookies = true;
                preferences.PersonalisedCookies = true;
                preferences.SocialCookies = true;

                //Store to session
                objUser.Preferences = preferences;

                //Set Cookie Preferences Cookie -default to permission allowed
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MarketingCookies, "Y");
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MetricsCookies, "Y");
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.PersonalisedCookies, "Y");
                CookieHelper.AddUpdateOptInCookie(CookieKeyNames.SocialCookies, "Y");
            }	

            //Save session
            Session["sess_User"] = objUser;

            //Add dynamic content to header
            HtmlHead head = (HtmlHead)Page.Header;

            Item currentItem = Sitecore.Context.Item;
            PageSummaryItem item = new PageSummaryItem(currentItem);
            string canonicalTag = item.GetCanonicalTag();

            string metaDescription = item.GetMetaDescription();

            //Add page title //todo: add club name
            string title = Translate.Text("Virgin Active");
            string browserPageTitle = item.GetPageTitle();

            string section = Sitecore.Web.WebUtil.GetQueryString("section");

            if (!String.IsNullOrEmpty(section))
            {
                PageSummaryItem listing = null;
                if (currentItem.TemplateID.ToString() == ClassesListingItem.TemplateId)
                {
                    //Get classes listing browser page title
                    listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses + "/" + section);
                }
                else if (currentItem.TemplateID.ToString() == FacilitiesListingItem.TemplateId)
                {
                    //Get facility listing browser page title
                    listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities + "/" + section);
                }

                if (listing != null)
                {
                    browserPageTitle = listing.GetPageTitle();
                    canonicalTag = String.IsNullOrEmpty(Request.QueryString["section"]) ? listing.GetCanonicalTag() : listing.GetCanonicalTag(Request.QueryString["section"]);
                    metaDescription = listing.GetMetaDescription();
                }
            }


            if (!String.IsNullOrEmpty(browserPageTitle))
            {
                title = String.Format("{0} | {1}", browserPageTitle, title);
            }

            Page.Title = title;
            //Add canonical
            Page.Header.Controls.Add(new Literal() { Text = canonicalTag });
            //Add meta description
            Page.Header.Controls.Add(new Literal() { Text = metaDescription });

            //Add Open Tag
            //Set user session variable
            if (Session["sess_User"] != null)
            {
                objUser = (User)Session["sess_User"];

                if (objUser.Preferences != null)
                {
                    if (objUser.Preferences.MarketingCookies && objUser.Preferences.MetricsCookies)
                    {
                        head.Controls.Add(new LiteralControl(OpenTagHelper.OpenTagVirginActiveUK));
                    }
                }
            }

        }
    }
}