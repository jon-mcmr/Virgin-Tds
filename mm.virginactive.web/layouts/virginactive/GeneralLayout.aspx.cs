using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Globalization;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;
using mm.virginactive.wrappers.EviBlog;
using Sitecore.Links;
using Sitecore.Resources.Media;
using mm.virginactive.common.Helpers;
using Sitecore.Data.Managers;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon;
using System.Web.UI.HtmlControls;
using Sitecore.Web.UI.XslControls;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class GeneralLayout : System.Web.UI.Page
    {
        protected Item currentItem = Sitecore.Context.Item;
        private string metaTag = @"<meta name=""{0}"" content=""{1}"" />";
        private string metaProperty = @"<meta property=""{0}"" content=""{1}"" />";
        private string canonicalTag = @"<link rel=""canonical"" href=""{0}"" />";
        protected string bodyClass = "";
        protected Boolean newSession = false;
        //protected string homePageUrl = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            //Set a css class for <body> tag if we are using homepage or in a classic club
            if (currentItem.TemplateID.ToString() == HomePageItem.TemplateId)
            {
                BodyTag.Attributes.Add("class", "home");
            }
            else
            {
                if (Sitecore.Context.Item.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", ClassicClubItem.TemplateId)) != null)
                {
                    BodyTag.Attributes.Add("class", "classic");
                }
            }

            //Check Session
            if (Session["sess_User"] == null)
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

                    objUser = (User)Session["sess_User"];

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
                                    HtmlGenericControl HeaderContainer =
                                        (HtmlGenericControl)HeaderControl.FindControl("HeaderContainer");
                                    HtmlGenericControl ShowMorePanel =
                                        (HtmlGenericControl)HeaderControl.FindControl("ShowMore");

                                    string classNames = BodyTag.Attributes["class"] != null
                                                            ? BodyTag.Attributes["class"]
                                                            : "";
                                    BodyTag.Attributes.Add("class",
                                                           classNames.Length > 0
                                                               ? classNames + " cookie-show"
                                                               : "cookie-show");

                                    ShowMorePanel.Attributes.Add("style", "display:none;");
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

                        Control HeaderRegion = (Control)Page.FindControl("HeaderRegion");
                        if (HeaderRegion != null && HeaderRegion.Controls.Count > 0)
                        {
                            Control HeaderControlContainer = HeaderRegion.Controls[0];
                            if (HeaderControlContainer != null && HeaderControlContainer.Controls.Count > 0)
                            {
                                Control HeaderControl = HeaderControlContainer.Controls[0];
                                if (HeaderControl != null)
                                {
                                    //HtmlGenericControl HeaderContainerPanel = (HtmlGenericControl)HeaderControl.FindControl("HeaderContainer");                        
                                    HtmlGenericControl ShowMorePanel =
                                        (HtmlGenericControl)HeaderControl.FindControl("ShowMore");

                                    string classNames = BodyTag.Attributes["class"] != null
                                                            ? BodyTag.Attributes["class"]
                                                            : "";
                                    BodyTag.Attributes.Add("class",
                                                           classNames.Length > 0
                                                               ? classNames + " cookie-show"
                                                               : "cookie-show");

									if (ShowMorePanel != null)
									{
										ShowMorePanel.Attributes.Add("style", "display:none;"); 
									}
                                }
                            }
                        }
                    }
                }
            }
            /*
            //handle 404 header
            if (currentItem.Name.Contains("404"))
            {
                Response.TrySkipIisCustomErrors = true;
                Response.StatusCode = 404;
                Response.StatusDescription = "Page not found";
            }*/

            PageSummaryItem item = new PageSummaryItem(currentItem);

            //Add Facebook Application ID
            //Get Facebook Settings
            //string facebookAppID = Sitecore.Configuration.Settings.GetSetting("Virgin.FacebookAppId");
            //Add Facebook Settings to Head tag
            //if (!String.IsNullOrEmpty(facebookAppID))
            //{
            //    String facebookMetaTag = @"<meta property=""fb:app_id"" content=""{0}"" />";
            //    facebookMetaTag = string.Format(facebookMetaTag, facebookAppID);
            //    Page.Header.Controls.Add(new LiteralControl(facebookMetaTag));
            //}

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

            switch (item.InnerItem.TemplateName)
            {
                case Templates.YourHealthArticle:
                    YourHealthArticleItem yourHealthArticle = (YourHealthArticleItem)item.InnerItem;
                    openGraphDescription = yourHealthArticle.Articleteaser.Rendered != "" ? yourHealthArticle.Articleteaser.Rendered : SitecoreHelper.GetSiteSetting("Facebook description");
                    openGraphImage = yourHealthArticle.Articleimage.MediaItem != null ? homeUrl + yourHealthArticle.Articleimage.MediaUrl : SitecoreHelper.GetSiteSetting("Facebook image url");
                    break;
                case Templates.BlogEntry:
                    BlogEntryItem blogEntry = (BlogEntryItem)item.InnerItem;
                    openGraphDescription = blogEntry.Introduction.Rendered != "" ? blogEntry.Introduction.Rendered : SitecoreHelper.GetSiteSetting("Facebook description");
                    openGraphImage = blogEntry.Image.MediaItem != null ? homeUrl + blogEntry.Image.MediaUrl : SitecoreHelper.GetSiteSetting("Facebook image url");
                    break;
                case Templates.IndoorHistory:
                                //IndoorHistoryItem indoorHistoryItem = (IndoorHistoryItem)item.InnerItem;
                                //openGraphDescription = indoorHistoryItem.Subheading.Rendered != "" ? indoorHistoryItem.Subheading.Rendered : SitecoreHelper.GetSiteSetting("Facebook description");
                                //openGraphImage = indoorHistoryItem.Image.MediaItem != null ? homeUrl + indoorHistoryItem.Image.MediaUrl : SitecoreHelper.GetSiteSetting("Facebook image url");
                                //break;
                default:
                    openGraphDescription = SitecoreHelper.GetSiteSetting("Facebook description");
                    openGraphImage = SitecoreHelper.GetSiteSetting("Facebook image url");
                    break;
            }

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
            openGraphMeta.Text += String.Format(metaProperty, "og:description", openGraphDescription);
            openGraphMeta.Text += String.Format(metaProperty, "og:image", openGraphImage);

            Page.Header.Controls.Add(openGraphMeta);



            //if (!newSession)
            //{
            //    User objUser = (User)Session["sess_User"];
            //    //Add dynamic content to header
            //    HtmlHead head = (HtmlHead)Page.Header;

            //    if (objUser.Preferences != null)
            //    {
            //        if (objUser.Preferences.MarketingCookies && objUser.Preferences.MetricsCookies)
            //        {
            //            //Have permission to load in OpenTag
            //            head.Controls.Add(new LiteralControl(OpenTagHelper.OpenTagVirginActiveUK));
            //        }

            //        //Add Social
            //        if (objUser.Preferences.SocialCookies)
            //        {
            //            //Add open graph tags (controls info facebook needs)
            //            string openGraphDescription = "";
            //            string openGraphImage = "";
            //            string openGraphTitle = title;

            //            string startPath = Sitecore.Context.Site.StartPath.ToString();
            //            var homeItem = Sitecore.Context.Database.GetItem(startPath);
            //            var options = new UrlOptions { AlwaysIncludeServerUrl = true, AddAspxExtension = false, LanguageEmbedding = LanguageEmbedding.Never};
            //            var homeUrl = LinkManager.GetItemUrl(homeItem, options);
            //            var mediaOptions = new MediaUrlOptions { AbsolutePath = true };

            //            switch (item.InnerItem.TemplateName)
            //            {
            //                case Templates.YourHealthArticle:
            //                    YourHealthArticleItem yourHealthArticle = (YourHealthArticleItem)item.InnerItem;
            //                    openGraphDescription = yourHealthArticle.Articleteaser.Rendered != "" ? yourHealthArticle.Articleteaser.Rendered : SitecoreHelper.GetSiteSetting("Facebook description");
            //                    openGraphImage = yourHealthArticle.Articleimage.MediaItem != null ? homeUrl + yourHealthArticle.Articleimage.MediaUrl : SitecoreHelper.GetSiteSetting("Facebook image url");
            //                    break;
            //                case Templates.BlogEntry:
            //                    BlogEntryItem blogEntry = (BlogEntryItem)item.InnerItem;
            //                    openGraphDescription = blogEntry.Introduction.Rendered != "" ? blogEntry.Introduction.Rendered : SitecoreHelper.GetSiteSetting("Facebook description");
            //                    openGraphImage = blogEntry.Image.MediaItem != null ? homeUrl + blogEntry.Image.MediaUrl : SitecoreHelper.GetSiteSetting("Facebook image url");
            //                    break;
            //                case Templates.IndoorHistory:
            //                    IndoorHistoryItem indoorHistoryItem = (IndoorHistoryItem)item.InnerItem;
            //                    openGraphDescription = indoorHistoryItem.Subheading.Rendered != "" ? indoorHistoryItem.Subheading.Rendered : SitecoreHelper.GetSiteSetting("Facebook description");
            //                    openGraphImage = indoorHistoryItem.Image.MediaItem != null ? homeUrl + indoorHistoryItem.Image.MediaUrl : SitecoreHelper.GetSiteSetting("Facebook image url");
            //                    break;
            //                default:
            //                    openGraphDescription = SitecoreHelper.GetSiteSetting("Facebook description");
            //                    openGraphImage = SitecoreHelper.GetSiteSetting("Facebook image url");
            //                    break;
            //            }

            //            //Overwrite if we are inheriting from Social Open Graph
            //            SocialOpenGraphItem openGraphDetails;
            //            var itemTemplate = TemplateManager.GetTemplate(currentItem);

            //            if (itemTemplate.InheritsFrom("Social Open Graph"))
            //            {
            //                openGraphDetails = (SocialOpenGraphItem)currentItem;

            //                openGraphTitle = openGraphDetails.Title.Raw != "" ? openGraphDetails.Title.Raw : openGraphTitle;
            //                openGraphDescription = openGraphDetails.Description.Raw != "" ? openGraphDetails.Description.Raw : openGraphDescription;
            //                openGraphImage = openGraphDetails.ImageUrl.Raw != "" ? openGraphDetails.ImageUrl.Raw : openGraphImage;
            //            }

            //            Literal openGraphMeta = new Literal();
            //            openGraphMeta.Text = String.Format(metaProperty, "og:title", openGraphTitle);
            //            openGraphMeta.Text += String.Format(metaProperty, "og:description", openGraphDescription);
            //            openGraphMeta.Text += String.Format(metaProperty, "og:image", openGraphImage);

            //            if (item.InnerItem.TemplateName != Templates.BlogHome && item.InnerItem.TemplateName != Templates.YourHealthLanding && item.InnerItem.TemplateName != Templates.YourHealthListing)
            //            {
            //                //Add the open graph meta info (Do not want this on the listing pages as want to force facebook to index the detail pages)
            //                Page.Header.Controls.Add(openGraphMeta);
            //            }
            //        }
            //    }
            //}

            ////Add canonical
            //if (item != null)
            //{
            //    Literal canon = new Literal();
            //    canon.Text = String.Format(canonicalTag, item.QualifiedUrl);
            //    Page.Header.Controls.Add(canon);
            //}

            ////Add description meta tags.
            //if (!String.IsNullOrEmpty(item.Pagedescription.Raw))
            //{
            //    Literal meta = new Literal();
            //    meta.Text = String.Format(metaTag, "description", item.Pagedescription.Raw);
            //    Page.Header.Controls.Add(meta);
            //}

            //Add no-follow meta tags
            if (item.Noindex != null)
            {
                if (item.Noindex.Checked)
                {
                    Page.Header.Controls.Add(new LiteralControl(@"<meta name=""robots"" content=""noindex, follow"" />"));
                }
            }

            //Add Versioned Style Sheet
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
            markupBuilder.Append(@"<link rel='stylesheet' href='/virginactive/css/style.css?ver=" + Settings.DeploymentVersionNumber + "' />");
            markupBuilder.Append(@"<link rel='stylesheet' href='/virginactive/css/fonts.css?ver=" + Settings.DeploymentVersionNumber + "' />");
            markupBuilder.Append(@"<link rel='stylesheet' href='/virginactive/css/cookies.css?ver=" + Settings.DeploymentVersionNumber + "' />");
            Page.Header.Controls.Add(new LiteralControl(markupBuilder.ToString()));

            //Add Scripts
            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"
					<script src='/virginactive/scripts/plugins.js?ver=" + Settings.DeploymentVersionNumber + @"'></script>
					<script src='/virginactive/scripts/jquery.virgin.init.js?ver=" + Settings.DeploymentVersionNumber + @"'></script>
					<script src='/virginactive/scripts/home.js'></script>
					<script src='/virginactive/scripts/sitecore/jquery.virgin.ajax.js?ver=" + Settings.DeploymentVersionNumber + @"'></script>
					<script src='/virginactive/scripts/sitecore/jquery.virginsearch.ajax.js?ver=" + Settings.DeploymentVersionNumber + @"'></script>
					<script src='/virginactive/scripts/sitecore/VirginActiveHelper.js?ver=" + Settings.DeploymentVersionNumber + @"'></script>
					<script src='/virginactive/scripts/sitecore/validation.js?ver=" + Settings.DeploymentVersionNumber + @"'></script>	
                   "));
            }


        }
    }
}