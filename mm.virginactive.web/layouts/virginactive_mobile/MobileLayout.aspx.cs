using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using System.Web.UI.HtmlControls;
using Sitecore.Resources.Media;
using Sitecore.Data.Managers;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileLayout : System.Web.UI.Page
    {
        protected string homeOrClubPageUrl = "";
        protected string youTubeUrl = "";
        protected string facebookUrl = "";
        protected string twitterUrl = "";
        protected string privacyUrl = "";
        protected string termsAndConditionsUrl = "";
        protected string cookiesUrl = "";
        protected Boolean newSession = false;
        protected Item currentItem = Sitecore.Context.Item;
        private string metaProperty = @"<meta property=""{0}"" content=""{1}"" />";
        protected string HomeMainSiteUrl = "";

        public string HomeOrClubPageUrl
        {
            get { return homeOrClubPageUrl; }
            set { homeOrClubPageUrl = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //CREATE NEW USER SESSION
            //Check if we are loading home page for the first time
            User objUser = new User();

            //Check Session
            if (Session["sess_User"] == null)
            {
                newSession = true;
            }
            else
            {
                objUser = (User)Session["sess_User"];
            }

            //READ/SET COOKIE OPTIONS
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
            }

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

            //SET PAGE META DATA
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

            Page.Title = title;
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

            string startPath = Sitecore.Context.Site.StartPath.ToString();
            var homeItem = Sitecore.Context.Database.GetItem(startPath);
            var options = new UrlOptions { AlwaysIncludeServerUrl = true, AddAspxExtension = false, LanguageEmbedding = LanguageEmbedding.Never };
            var homeUrl = LinkManager.GetItemUrl(homeItem, options);
            var mediaOptions = new MediaUrlOptions { AbsolutePath = true };

            //Set return to main site link
            HomeMainSiteUrl = homeUrl + "?sc_device=default&persisted=true";

            openGraphDescription = SitecoreHelper.GetSiteSetting("Facebook description");
            openGraphImage = SitecoreHelper.GetSiteSetting("Facebook image url");

            //Overwrite if we are inheriting from Social Open Graph
            SocialOpenGraphItem openGraphDetails;
            var itemTemplate = TemplateManager.GetTemplate(currentItem);

            if (itemTemplate.InheritsFrom("Social Open Graph"))
            {
                openGraphDetails = (SocialOpenGraphItem)currentItem;

                openGraphTitle = openGraphDetails.Title.Raw != "" ? openGraphDetails.Title.Raw : openGraphTitle;
                openGraphDescription = openGraphDetails.Description.Raw != "" ? openGraphDetails.Description.Raw : openGraphDescription;
                openGraphImage = openGraphDetails.ImageUrl.Raw != "" ? openGraphDetails.ImageUrl.Raw : openGraphImage;
            }

            Literal openGraphMeta = new Literal();
            openGraphMeta.Text = String.Format(metaProperty, "og:title", openGraphTitle);
            openGraphMeta.Text = openGraphMeta.Text +  String.Format(metaProperty, "og:description", openGraphDescription);
            openGraphMeta.Text = openGraphMeta.Text + String.Format(metaProperty, "og:image", openGraphImage);

            Page.Header.Controls.Add(openGraphMeta);

            //SET PAGE LINKS

            youTubeUrl = Settings.YouTubeLinkUrl;
            twitterUrl = Settings.TwitterLinkUrl;
            facebookUrl = Settings.FacebookLinkUrl;

            privacyUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy);
            termsAndConditionsUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions);
            cookiesUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.CookiesForm);

            //Set featured promo link and home page link
            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = true;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            //Set home page link (set as club home if home club set)
            homeOrClubPageUrl = Sitecore.Links.LinkManager.GetItemUrl(homeItem, urlOptions);

            //<h1><a href="/" class="logo"><strong>VIRGIN ACTIVE</strong> HEALTH CLUBS</a></h1>
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

            if (Sitecore.Context.Item.ID == homeItem.ID)
            {
                //Page is home page
                markupBuilder.Append(@"<h1><a class=""logo"" href=""");
                markupBuilder.Append(HomeOrClubPageUrl + @"""><strong class=""home"">");
                markupBuilder.Append(Translate.Text("VIRGIN ACTIVE") + "</strong> ");
                markupBuilder.Append(Translate.Text("HEALTH CLUBS") + "</a></h1>");
            }
            else
            {
                markupBuilder.Append(@"<a class=""logo"" href=""");
                markupBuilder.Append(HomeOrClubPageUrl + @"""><strong>");
                markupBuilder.Append(Translate.Text("VIRGIN ACTIVE") + "</strong> ");
                markupBuilder.Append(Translate.Text("HEALTH CLUBS") + "</a>");
            }

            ltrHeaderText.Text = markupBuilder.ToString();
        }
    }
}