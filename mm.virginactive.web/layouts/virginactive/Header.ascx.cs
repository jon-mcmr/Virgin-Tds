using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using Sitecore.Resources.Media;
using Sitecore.Data.Fields;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using Sitecore.Collections;
using System.Web.UI.HtmlControls;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected PageSummaryItem clubFinder = new PageSummaryItem( Sitecore.Context.Database.GetItem(ItemPaths.ClubFinder) );
        protected HeaderItem currentItem = null;
        protected FeaturedPromotionEventWidgetItem fpew = null;
        protected string homeOrClubPageUrl = "";
        protected string clubLastVisitedUrl = "";
        protected string clubName = "";
        protected ClubItem clubLastVisited = null;
        protected Boolean newSession = false;

        public string ClubName
        {
            get { return clubName; }
            set { clubName = value; }
        }
        public string ClubLastVisitedUrl
        {
            get { return clubLastVisitedUrl; }
            set { clubLastVisitedUrl = value; }
        }
        public string HomeOrClubPageUrl
        {
            get { return homeOrClubPageUrl; }
            set { homeOrClubPageUrl = value; }
        }

        public ClubItem ClubLastVisited
        {
            get { return clubLastVisited; }
            set { clubLastVisited = value; }
        }
        public FeaturedPromotionEventWidgetItem FeaturedPromoEventItem
        {
            get { return fpew; }
            set { fpew = value; }
        }
        public HeaderItem CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }


        
        protected void Page_Load(object sender, EventArgs e)
        {
            //set current item
            currentItem = Sitecore.Context.Database.GetItem(ItemPaths.Header);



			//Header links
			Item[] links = currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", LinkItem.TemplateId));
			if (links != null)
			{
				List<LinkItem> headerLinks = links.ToList().ConvertAll(X => new LinkItem(X));
				LinkList.DataSource = headerLinks;
				LinkList.DataBind();
			}

            //Set featured promo link and home page link
            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = true;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            //set Featured Promo Event Teaser
            if (currentItem.InnerItem.HasChildren)
            {
                List<FeaturedPromotionEventWidgetItem> allFeatures = currentItem.InnerItem.Children.ToList().ConvertAll(X => new FeaturedPromotionEventWidgetItem(X));
                fpew = new FeaturedPromotionEventWidgetItem(allFeatures.First());
            }

            //Set home page link (set as club home if home club set)
            string startPath = Sitecore.Context.Site.StartPath.ToString();
            var homeItem = Sitecore.Context.Database.GetItem(startPath);
            hdnLastClubVisitedId.Value = "";

            //set hidden field for javascript referencing
            homePageUrl.Value = Sitecore.Links.LinkManager.GetItemUrl(homeItem, urlOptions);
            homePageUrl.Value = homePageUrl.Value.IndexOf("Home.aspx") != -1 ? homePageUrl.Value : homePageUrl.Value + "Home.aspx";

            //Set home page link
            homeOrClubPageUrl = Sitecore.Links.LinkManager.GetItemUrl(homeItem, urlOptions);

            //Set Header Text -need logic to display <h1> or <h2>
            //Format Contact Address
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

            if (Sitecore.Context.Item.ID == homeItem.ID)
            {
                //Page is home page
                markupBuilder.Append(@"<h1 class=""headertext""><a href=""");
                markupBuilder.Append(HomeOrClubPageUrl + @""">");
                markupBuilder.Append(Translate.Text("Virgin Active") + " <span>");
                markupBuilder.Append(Translate.Text("Health Clubs") + "</span></a></h1>");
            }
            else
            {
                markupBuilder.Append(@"<h2 class=""headertext""><a href=""");
                markupBuilder.Append(HomeOrClubPageUrl + @""">");
                markupBuilder.Append(Translate.Text("Virgin Active") + " <span>");
                markupBuilder.Append(Translate.Text("Health Clubs") + "</span></a></h2>");
            }

            ltrHeaderText.Text = markupBuilder.ToString();

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

            //SHOW/HIDE PERSONALISED CONTENT
            if (newSession && (Sitecore.Context.Item.ID == homeItem.ID))
            {
                //Loading home page for the first time -Check if last page visited cookie exists
                //if(!string.IsNullOrEmpty(CookieHelper.GetCookieValue(CookieKeyNames.ClubLastVisited)))
                if (!string.IsNullOrEmpty(CookieHelper.GetClubLastVisitedCookieValue(CookieKeyNames.ClubLastVisited)))
                {
                    //Display message prompting then to go to club
                    phLastClubVisitedPrompt.Visible = true;
                    //clubLastVisited = SitecoreHelper.GetClubOnClubId(CookieHelper.GetCookieValue(CookieKeyNames.ClubLastVisited));
                    clubLastVisited = SitecoreHelper.GetClubOnClubId(CookieHelper.GetClubLastVisitedCookieValue(CookieKeyNames.ClubLastVisited));
                    if (clubLastVisited != null)
                    {
                        ClubLastVisitedUrl = Sitecore.Links.LinkManager.GetItemUrl(clubLastVisited, urlOptions);
                        ClubName = clubLastVisited.Clubname.Rendered;
                        hdnLastClubVisitedId.Value = clubLastVisited.ClubId.Rendered;			

                        //Store to session
                        objUser.ClubLastVisitedID = clubLastVisited.ClubId.Rendered;
                    }
                }
            }
            else
            {                
                //User session already exists but only visiting home page for the first time
                if ((objUser.HomePageVisited == false) && (Sitecore.Context.Item.ID == homeItem.ID))
                {
                    //if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue(CookieKeyNames.ClubLastVisited)))
                    if (!string.IsNullOrEmpty(CookieHelper.GetClubLastVisitedCookieValue(CookieKeyNames.ClubLastVisited)))
                    {
                        //Display message prompting then to go to club
                        phLastClubVisitedPrompt.Visible = true;
                        //clubLastVisited = SitecoreHelper.GetClubOnClubId(CookieHelper.GetCookieValue(CookieKeyNames.ClubLastVisited));
                        clubLastVisited = SitecoreHelper.GetClubOnClubId(CookieHelper.GetClubLastVisitedCookieValue(CookieKeyNames.ClubLastVisited));
                        if (clubLastVisited != null)
                        {
                            ClubLastVisitedUrl = Sitecore.Links.LinkManager.GetItemUrl(clubLastVisited, urlOptions);
                            ClubName = clubLastVisited.Clubname.Rendered;
                            hdnLastClubVisitedId.Value = clubLastVisited.ClubId.Rendered;

                            //Store to session
                            objUser.ClubLastVisitedID = clubLastVisited.ClubId.Rendered;
                        }
                    }
                }
            }

            //Check if a club has been set as home page
            if (!String.IsNullOrEmpty(CookieHelper.GetCookieValue(CookieKeyNames.HomeClub)))           
            {
                ClubItem club = SitecoreHelper.GetClubOnClubId(CookieHelper.GetCookieValue(CookieKeyNames.HomeClub));
                if (club != null)
                {
                    homeOrClubPageUrl = Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions);

                    //Store to session
                    objUser.HomeClubID = club.ClubId.Rendered;
                }
            }

            //Save session
            Session["sess_User"] = objUser;

            Sitecore.Data.Items.Item headerItem = Sitecore.Context.Database.GetItem("/sitecore/content/page-elements/header");

            //Get featured promotion event item details for banner
            Sitecore.Data.Items.Item featuredPromotionEventItem = Sitecore.Context.Database.GetItem("/sitecore/content/Widgets/featured-promotion-event");
            if (featuredPromotionEventItem != null)
            {
                //fldPromotionEventDate.DataSource = featuredPromotionEventItem.ID.ToString();

                //fldPromotionEventSubheading.DataSource = featuredPromotionEventItem.ID.ToString();
            }         
        }


        protected void rptPromotionEvent_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var subItem = e.Item.DataItem as Item;
                if (subItem != null)
                {
                    //Get link
                    var lnkPromotionEventLink = e.Item.FindControl("lnkPromotionEventLink") as Sitecore.Web.UI.WebControls.Link;
                    if (lnkPromotionEventLink != null)
                    {
                        lnkPromotionEventLink.DataSource = subItem.ID.ToString();

                        //Get Link Category
                        lnkPromotionEventLink.CssClass = subItem.Fields["Link category"].Value;
                    }
                }
            }
        }

        protected void rptSections_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var sectionItem = e.Item.DataItem as Item;
                if (sectionItem != null)
                {
                    //Get Section Name
                    var fldSectionName = e.Item.FindControl("fldSectionName") as Sitecore.Web.UI.WebControls.FieldRenderer;
                    if (fldSectionName != null)
                    {
                        fldSectionName.DataSource = sectionItem.ID.ToString();
                        //Get Modules for Section
                        var rptSectionModules = e.Item.FindControl("rptSectionModules") as Repeater;

                        rptSectionModules.DataSource = GetChildrenDataSource(sectionItem);
                        rptSectionModules.DataBind();
                    }
                }
            }
        }

        protected void rptModules_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var moduleItem = e.Item.DataItem as Item;
                if (moduleItem != null)
                {
                    //Get Module Link
                    var fldModuleLink = e.Item.FindControl("fldModuleLink") as Sitecore.Web.UI.WebControls.Link;
                    var fldLinkName = e.Item.FindControl("fldLinkName") as Sitecore.Web.UI.WebControls.Text;
                    //fldModuleLink. = LinkManager.GetItemUrl(moduleItem);
                    fldLinkName.DataSource = moduleItem.ID.ToString();
                }
            }
        }

        private object GetPromotionEventDataSource(Item listParentItem)
        {
            Debug.ArgumentNotNull(listParentItem, "listparentitem");
            return listParentItem.GetChildren();
        }

        private object GetChildrenDataSource(Item listParentItem)
        {
            Debug.ArgumentNotNull(listParentItem, "currentitem");
            return listParentItem.GetChildren();
        }

        private object GetClassSectionsDataSource(Item currentItem)
        {
            Debug.ArgumentNotNull(currentItem, "currentitem");
            return currentItem.GetChildren();
        }

        //protected void btnSetHomeClub_Click(object sender, EventArgs e)
        //{
        //    User objUser = new User();
        //    //Set user session variable
        //    if (Session["sess_User"] != null)
        //    {
        //        objUser = (User)Session["sess_User"];
        //    }

        //    clubLastVisited = SitecoreHelper.GetClubOnClubId(CookieHelper.GetCookieValue(CookieKeyNames.ClubLastVisited));

        //    if (clubLastVisited != null)
        //    {
        //        //Set Home Club ID User Session
        //        objUser.HomeClubID = clubLastVisited.ClubId.Rendered;
        //        Session["sess_User"] = objUser;

        //        //Set Home Club ID Cookie
        //        CookieHelper.AddUpdateCookie(CookieKeyNames.HomeClub, clubLastVisited.ClubId.Rendered);

        //        //string url = Sitecore.Links.LinkManager.GetItemUrl(clubLastVisited);
        //        //Response.Redirect(url);
        //    }
        //    phLastClubVisitedPrompt.Visible = false;

        //}
    	protected void LinkList_ItemDataBound(object sender, ListViewItemEventArgs e)
    	{
			if (e.Item.ItemType == ListViewItemType.DataItem)
			{
				ListViewDataItem item = e.Item as ListViewDataItem;
				if (item.DataItemIndex == ((List<LinkItem>)LinkList.DataSource).Count - 1)
				{
					PlaceHolder ListItemNormal = e.Item.FindControl("ListItemNormal") as PlaceHolder;
					PlaceHolder ListItemLast = e.Item.FindControl("ListItemLast") as PlaceHolder;

					ListItemNormal.Visible = false;
					ListItemLast.Visible = true;
				}
			}
    	}
    }
}