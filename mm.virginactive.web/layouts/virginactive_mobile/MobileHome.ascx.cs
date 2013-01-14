using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.MobileTemplates;


namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileHome : System.Web.UI.UserControl
    {
        protected HomePageItem currentItem = new HomePageItem(Sitecore.Context.Item);
        protected MobileHomeItem mobileItem = null;

        //protected ClubItem clubLastVisited = null;
        //protected string ClubLastVisitedUrl = "";
        //protected string ClubName = "";

        protected string ClubFinderUrl = "";
        protected string TimetablesUrl = "";
        protected string MembershipEnquiryUrl = "";
        

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack != true)
            {
                mobileItem = (MobileHomeItem)Sitecore.Context.Database.GetItem(ItemPaths.MobileHomePage);

                //generate the image carousel
                List<ImageCarouselItem> imgCar = new List<ImageCarouselItem>();
                if (mobileItem.InnerItem.HasChildren)
                {
                    imgCar = mobileItem.InnerItem.Children.ToList().ConvertAll(X => new ImageCarouselItem(X));
                    ImageList.DataSource = imgCar;
                    ImageList.DataBind();
                }

                
                //Set links
                ClubFinderUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.ClubFinder);
                TimetablesUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.ClubFinder) + "?action=timetables";
                MembershipEnquiryUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.EnquiryForm);

                //Redirect to club home
                Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                urlOptions.AlwaysIncludeServerUrl = true;
                urlOptions.AddAspxExtension = false;
                urlOptions.LanguageEmbedding = LanguageEmbedding.Never;      

                User objUser = new User();
                //Set user session variable
                if (Session["sess_User"] != null)
                {
                    objUser = (User)Session["sess_User"];   
                }
                //Set Home Page Visited Flag
                objUser.HomePageVisited = true;
                Session["sess_User"] = objUser;


                //Loading home page for the first time -Check if last page visited cookie exists
                if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue(CookieKeyNames.ClubLastVisited)))
                {

                    string[] clubIds = CookieHelper.GetClubsLastVisitedCookieValue(CookieKeyNames.ClubLastVisited);
                    int count = 0;

                    if (clubIds != null && clubIds.Length > 0)
                    {
                        //Display message prompting then to go to club
                        phLastClubVisitedPrompt.Visible = true;

                        List<ClubItem> lastVisitedClubs = new List<ClubItem>();

                        foreach (string clubId in clubIds)
                        {
                            lastVisitedClubs.Add(SitecoreHelper.GetClubOnClubId(clubId));

                            if (count == 0)
                            {
                                //Store to session
                                objUser.ClubLastVisitedID = clubId;
                            }

                            count++;
                        }

                        ClubList.DataSource = lastVisitedClubs;
                        ClubList.DataBind();
                    }
                    //clubLastVisited = SitecoreHelper.GetClubOnClubId(CookieHelper.GetCookieValue(CookieKeyNames.ClubLastVisited));
                    //if (clubLastVisited != null)
                    //{
                    //    ClubLastVisitedUrl = Sitecore.Links.LinkManager.GetItemUrl(clubLastVisited, urlOptions);
                    //    ClubName = clubLastVisited.Clubname.Rendered;

                    //    //Store to session
                    //    objUser.ClubLastVisitedID = clubLastVisited.ClubId.Rendered;
                    //}
                }
            }
        }
    }
}