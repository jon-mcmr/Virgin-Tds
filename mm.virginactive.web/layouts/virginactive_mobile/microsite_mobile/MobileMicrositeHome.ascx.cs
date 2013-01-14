using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Links;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates.ClubMicrosites;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileMicrositeHome : System.Web.UI.UserControl
    {
        protected string ClubName = "";
        protected string ClubTimetableUrl = "";
        protected string ClubEnquiriesUrl = "";
        protected string openingHours = "";
        protected string lat = "";
        protected string lng = "";

        //protected ClubItem club = new ClubItem(Sitecore.Context.Item);
        protected ClubItem club;
        protected MicrositeHomeItem micrositeHome = new MicrositeHomeItem(Sitecore.Context.Item);
        protected ClubMicrositeLandingItem micrositeLanding = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

            //Redirect to club home
            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = false;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            micrositeLanding = micrositeHome.InnerItem.Parent;

            if (micrositeLanding != null)
            {
                club = micrositeLanding.Club.Item;

                if (club != null)
                {
                    //Get opening hours
                    if (club.Openinghours.Rendered.Trim() != "")
                    {
                        //add opening hours details                
                        markupBuilder.Append(@"<p class=""openingtimes"">");
                        markupBuilder.Append(club.Openinghours.Rendered);
                        markupBuilder.Append(@"</p>");

                        openingHours = markupBuilder.ToString();
                    }

                    //Get address

                    markupBuilder = new System.Text.StringBuilder();

                    markupBuilder.Append(club.Addressline1.Text);
                    markupBuilder.Append(!String.IsNullOrEmpty(club.Addressline2.Text) ? "<br />" + club.Addressline2.Text : "");
                    markupBuilder.Append(!String.IsNullOrEmpty(club.Addressline3.Text) ? "<br />" + club.Addressline3.Text : "");
                    markupBuilder.Append("<br />");
                    markupBuilder.Append(!String.IsNullOrEmpty(club.Addressline4.Text) ? club.Addressline4.Text + " " : "");
                    markupBuilder.Append(club.Postcode.Text);

                    Address.Text = markupBuilder.ToString();

                    //Get Club details

                    ClubName = club.Clubname.Rendered;
                    lng = club.Long.Raw;
                    lat = club.Lat.Raw;

                    //Get timetable link
                    if (micrositeHome.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", MicrositeTimetableItem.TemplateId)) != null)
                    {
						MicrositeTimetableItem timetableItem = new MicrositeTimetableItem(micrositeHome.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", MicrositeTimetableItem.TemplateId)));
						MicrositeTimetableLandingItem timetableSectionItem = new MicrositeTimetableLandingItem(timetableItem.InnerItem.Parent);
                        ClubTimetableUrl = Sitecore.Links.LinkManager.GetItemUrl(timetableSectionItem);
                    }
                    else
                    {
                        if (micrositeHome.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)) != null)
                        {
                            TimetableDownloadItem timetableDownloadItem = new TimetableDownloadItem(micrositeHome.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)));
                            ClubTimetableUrl = Sitecore.Links.LinkManager.GetItemUrl(timetableDownloadItem);
                        }
                    }

                    //Get enquiries link
                    PageSummaryItem enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
                    if (enqForm != null)
                    {
                        ClubEnquiriesUrl = enqForm.Url + "?sc_trk=enq&c=" + club.InnerItem.ID.ToShortID();
                    }

                    //Get Media Items from Widget Container
                    List<FacilityModuleItem> facilities = GetDataSource(micrositeHome.InnerItem);
                    List<MediaItem> mediaItems = new List<MediaItem>();
                    if (facilities != null)
                    {
                        foreach (FacilityModuleItem facilityModule in facilities)
                        {
                            List<MediaItem> galleryItems = facilityModule.Imagegallery.ListItems.ConvertAll(x => new MediaItem(x));
                            if (galleryItems != null && galleryItems.Count > 0)
                            {
                                mediaItems.Add(galleryItems[0]);
                            }
                        }
                    }

                    if (mediaItems.Count > 0)
                    {
                        ImageList.DataSource = mediaItems;
                        ImageList.DataBind();
                    }


                    //Set club last visited 
                    User objUser = new User();
                    if (Session["sess_User"] != null)
                    {
                        objUser = (User)Session["sess_User"];
                    }
                    objUser.ClubLastVisitedID = club.ClubId.Rendered;
                    Session["sess_User"] = objUser;

                    //Set club last visited cookie
                    //if (objUser.Preferences != null)
                    //{
                    //    if (objUser.Preferences.PersonalisedCookies)
                    //    {
                    CookieHelper.AddClubsLastVisitedCookie(CookieKeyNames.ClubLastVisited, club.ClubId.Rendered);
                    //    }
                    //}
                }
            }
        }

        private List<FacilityModuleItem> GetDataSource(Item currentItem)
        {
            var widgetContainer = currentItem.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", FacilityModulesItem.TemplateId));

            if (widgetContainer != null)
            {
                Item[] moduleItems =
                    widgetContainer.Axes.SelectItems(String.Format("*[@@tid='{0}']", FacilityModuleItem.TemplateId));

                if (moduleItems != null)
                {
                    return moduleItems.ToList().ConvertAll(x => new FacilityModuleItem(x));
                }
            }

            return new List<FacilityModuleItem>();
        }
    }
}