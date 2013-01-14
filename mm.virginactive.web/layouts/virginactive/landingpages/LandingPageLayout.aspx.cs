using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.controls.Model;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Data.Managers;
using Sitecore.Web;
using Sitecore;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Util;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{

	public partial class LandingPageLayout : System.Web.UI.Page
	{
		protected PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);
		protected Boolean newSession = false;
		private string metaProperty = @"<meta property=""{0}"" content=""{1}"" />";
		protected string GoogleShareText { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
                SetClassName();
				SetPage();
				SetSession();
                SetRegionData();
			}
		}

        private void SetRegionData()
        {
            string region = WebUtil.GetQueryString("_region");
            
            if (String.IsNullOrEmpty(region))
            {
                //Check Session
                User objUser = new User();
                if (Session["sess_User_landing"] != null)
                {
					objUser = (User)Session["sess_User_landing"];
                    region = objUser.BrowsingHistory.LandingRegion;
                }
            }

            if (!String.IsNullOrEmpty(region))
            {
                LocationPointerItem pointerItem = null;

                pointerItem = GymLocationManager.GetLocation(region);

                if (pointerItem != null)
                {
                    HtmlGenericControl BodyTag = (HtmlGenericControl)this.Page.FindControl("BodyTag");
                    BodyTag.Attributes.Add("data-region", pointerItem.LocationName.Rendered);
                    BodyTag.Attributes.Add("data-Lat", pointerItem.Lat.Rendered);
                    BodyTag.Attributes.Add("data-Long", pointerItem.Long.Rendered);

                }
            }
        }

        private void SetClassName()
        {
            string cssName = "home";
            string templateId = Sitecore.Context.Item.TemplateID.ToString();

            if (templateId == Settings.LandingPagesEnquiryTemplate)
                cssName = "club";
			else if (templateId == Settings.LandingPagesWhatsNextTemplate)
			{
				cssName = "confirmation";

				//code to set the meta tag for google+ link
				LandingWhatsNextItem whatNextItem = new LandingWhatsNextItem(Sitecore.Context.Item);
				GoogleShareText = whatNextItem.GooglePlusShareText.Rendered;

				Literal googlePlusTag = new Literal();
				googlePlusTag.Text = "<meta itemprop='name' content='" + GoogleShareText + "' />";

				Page.Header.Controls.Add(googlePlusTag);
			}

            contentDiv.Attributes.Add("class", "content "+ cssName);
        }

		private void SetPage()
		{

			//Check Session
			if (Session["sess_User_landing"] == null)
			{
				newSession = true;
			}

			//THIS IS WHERE WE HIDE/SHOW COOKIE MESSAGE -N.B. User Session initialised and set in Header

			//Interorgate the request 'Do Not Track' settings.
			HttpContext objContext = HttpContext.Current;
			bool headerDoNotTrack = false;

			if (!string.IsNullOrEmpty(objContext.Request.Headers["DNT"]))
			{
				headerDoNotTrack = objContext.Request.Headers["DNT"] == "1" ? true : false;
			}

			//If this has not been set -we need to show the cookie message
			if (!headerDoNotTrack)
			{
				//Check if User Session object has been set -i.e. not first page load
				if (!newSession)
				{
					User objUser = new User();

					objUser = (User)Session["sess_User_landing"];

					//Have the cookie preferences been set?
					if (objUser.Preferences == null)
					{
						//Show Cookie Preferences ribbon
						CookieRibbon cookieDeclaration = Page.LoadControl("~/layouts/virginactive/CookieRibbon.ascx") as CookieRibbon;
						RibbonPh.Controls.Add(cookieDeclaration);

						Control HeaderRegion = (Control)Page.FindControl("HeaderRegion");
						if (HeaderRegion != null && HeaderRegion.Controls.Count > 0)
						{
							Control HeaderControlContainer = HeaderRegion.Controls[0];
							if (HeaderControlContainer != null && HeaderControlContainer.Controls.Count > 0)
							{
								Control HeaderControl = HeaderControlContainer.Controls[0];
								if (HeaderControl != null)
								{
									//HtmlGenericControl HeaderContainer =
									//    (HtmlGenericControl)HeaderControl.FindControl("HeaderContainer");
									//HtmlGenericControl ShowMorePanel =
									//    (HtmlGenericControl)HeaderControl.FindControl("ShowMore");

									string classNames = BodyTag.Attributes["class"] != null
															? BodyTag.Attributes["class"]
															: "";
									BodyTag.Attributes.Add("class",
														   classNames.Length > 0
															   ? classNames + " cookie-show"
															   : "cookie-show");

									//ShowMorePanel.Attributes.Add("style", "display:none;");
								}
							}
						}
					}
				}
				else
				{
					//Session info not set (as it's a new session) -Check the 'Cookie Preferences Cookie' exists
					if (string.IsNullOrEmpty(CookieHelper.GetOptInCookieValue(CookieKeyNames.MarketingCookies)))
					{
						//User has NOT stored settings
						//Show Cookie Preferences ribbon
						CookieRibbon cookieDeclaration = Page.LoadControl("~/layouts/virginactive/CookieRibbon.ascx") as CookieRibbon;
						RibbonPh.Controls.Add(cookieDeclaration);

						string classNames = BodyTag.Attributes["class"] != null
												? BodyTag.Attributes["class"]
												: "";
						BodyTag.Attributes.Add("class",
												classNames.Length > 0
													? classNames + " cookie-show"
													: "cookie-show");
					}
				}

			}

			PageSummaryItem item = new PageSummaryItem(currentItem);

			string canonicalTag = item.GetCanonicalTag();

			string metaDescription = item.GetMetaDescription();

			//Add page title //todo: add club name
			string title = Translate.Text("Virgin Active");
			string browserPageTitle = item.GetPageTitle();

			if (!String.IsNullOrEmpty(browserPageTitle))
			{
				title = String.Format("{0} | {1}", browserPageTitle, title);
			}

			Page.Title = browserPageTitle;

			//Add canonical
			Page.Header.Controls.Add(new Literal() { Text = canonicalTag });
			//Add meta description
			Page.Header.Controls.Add(new Literal() { Text = metaDescription });

			//Load OpenTag container for all pages (Google needs it)

			HtmlHead head = (HtmlHead)Page.Header;
			head.Controls.Add(new LiteralControl(OpenTagHelper.OpenTagVirginActiveUK));

			//Add open graph tags (controls info facebook needs)
			string openGraphDescription = "";
			string openGraphImage = "";
			string openGraphTitle = title;

			openGraphDescription = SitecoreHelper.GetSiteSetting("Facebook description");
			openGraphImage = SitecoreHelper.GetSiteSetting("Facebook image url");

			//Overwrite if we are inheriting from Social Open Graph
			SocialOpenGraphItem openGraphDetails;
			var itemTemplate = TemplateManager.GetTemplate(currentItem);

			if (itemTemplate.InheritsFrom("Social Open Graph"))
			{
				openGraphDetails = (SocialOpenGraphItem)currentItem.InnerItem;

				openGraphTitle = openGraphDetails.Title.Raw != "" ? openGraphDetails.Title.Raw : openGraphTitle;
				openGraphDescription = openGraphDetails.Description.Raw != "" ? openGraphDetails.Description.Raw : openGraphDescription;
				openGraphImage = openGraphDetails.ImageUrl.Raw != "" ? openGraphDetails.ImageUrl.Raw : openGraphImage;
			}

			Literal openGraphMeta = new Literal();
			openGraphMeta.Text = String.Format(metaProperty, "og:title", openGraphTitle);
			openGraphMeta.Text += String.Format(metaProperty, "og:description", openGraphDescription);
			openGraphMeta.Text += String.Format(metaProperty, "og:image", openGraphImage);

			Page.Header.Controls.Add(openGraphMeta);

			//Add no-follow meta tags
			if (item.Noindex != null)
			{
				if (item.Noindex.Checked)
				{
					Page.Header.Controls.Add(new LiteralControl(@"<meta name=""robots"" content=""noindex, follow"" />"));
				}
			}
		}

		private void SetSession()
		{
			//CREATE NEW USER SESSION
			//Check if we are loading home page for the first time
            User objUser;

			//Check Session
			if (Session["sess_User_landing"] == null)
			{
				newSession = true;
                 objUser = new User();
			}
			else
			{
				objUser = (User)Session["sess_User_landing"];
            }

            #region //READ/SET COOKIE OPTIONS
            if (newSession)
            {
                //New Session

                //Check gallery preference
                if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue(CookieKeyNames.ShowGallery)))
                {
                    //Store to session
                    objUser.ShowGallery = CookieHelper.GetCookieValue(CookieKeyNames.ShowGallery) == "Y" ? true : false;
                }

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
                    //Interorgate the request 'Do Not Track' settings.
                    HttpContext objContext = HttpContext.Current;
                    bool headerDoNotTrack = false;

                    if (!string.IsNullOrEmpty(objContext.Request.Headers["DNT"]))
                    {
                        headerDoNotTrack = objContext.Request.Headers["DNT"] == "1" ? true : false;
                    }

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

                //Set Browsing History-User is coming to page for the first time

                string fullQueryString = Request.QueryString.ToString();

                BrowsingHistory browsingHistory = new BrowsingHistory();
                if (!string.IsNullOrEmpty(WebUtil.GetQueryString("_profile")))
                {
                    browsingHistory.LandingProfile = WebUtil.GetQueryString("_profile");
                }
                if (!string.IsNullOrEmpty(WebUtil.GetQueryString("_region")))
                {
                    browsingHistory.LandingRegion = WebUtil.GetQueryString("_region");
                }
                if (!string.IsNullOrEmpty(WebUtil.GetQueryString("_clubId")))
                {
                    browsingHistory.LandingClubID = WebUtil.GetQueryString("_clubId");

                    if (currentItem.InnerItem.TemplateID.ToString() == LandingPageItem.TemplateId)
                    {
                        //Get the enquiry page for this item
                        LandingEnquiryItem enquiryPage = currentItem.InnerItem.Axes.SelectSingleItem(String.Format("descendant::*[@@tid='{0}']", LandingEnquiryItem.TemplateId));

                        //Redirect to the enquiry page
                        Response.Redirect(Sitecore.Links.LinkManager.GetItemUrl(enquiryPage) + "?" + fullQueryString);
                    }
                }

                UrlOptions options = new UrlOptions();
               
                browsingHistory.LandingPageUrl = Sitecore.Links.LinkManager.GetItemUrl(currentItem);

                objUser.BrowsingHistory = browsingHistory;

				Session["sess_User_landing"] = objUser;

            }
            else
            {
                if (!String.IsNullOrEmpty(WebUtil.GetQueryString("_clubId")) && objUser.BrowsingHistory.LandingClubID != WebUtil.GetQueryString("_clubId"))
                {
                    objUser.BrowsingHistory.LandingClubID = WebUtil.GetQueryString("_clubId");
                }
                if (!String.IsNullOrEmpty(WebUtil.GetQueryString("_profile")) && objUser.BrowsingHistory.LandingProfile != WebUtil.GetQueryString("_profile"))
                {
                    objUser.BrowsingHistory.LandingProfile = WebUtil.GetQueryString("_profile");
                }
                if (!String.IsNullOrEmpty(WebUtil.GetQueryString("_region")) && objUser.BrowsingHistory.LandingRegion != WebUtil.GetQueryString("_region"))
                {
                    objUser.BrowsingHistory.LandingRegion = WebUtil.GetQueryString("_region");
                }

				Session["sess_User_landing"] = objUser;
            }
            #endregion

            //DEFAULT PREFERENCES
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
			Session["sess_User_landing"] = objUser;
		}
	}
}