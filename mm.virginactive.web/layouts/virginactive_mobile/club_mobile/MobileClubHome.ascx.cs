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
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.Mobile
{

    public partial class MobileClubHome : System.Web.UI.UserControl
    {

        protected string ClubName = "";
        protected string ClubTimetableUrl = "";
        protected string ClubEnquiriesUrl = "";
        protected string openingHours = "";
        protected string lat = "";
        protected string lng = "";

        protected ClubItem club = new ClubItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

            //Redirect to club home
            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = false;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;             

            if(club != null)
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
                if (club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableItem.TemplateId)) != null)
                {
                    TimetableItem timetableItem = new TimetableItem(club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableItem.TemplateId)));
                    SectionContainerItem timetableSectionItem = new SectionContainerItem(timetableItem.InnerItem.Axes.SelectSingleItem(String.Format(@"..", SectionContainerItem.TemplateId)));
                    ClubTimetableUrl = Sitecore.Links.LinkManager.GetItemUrl(timetableSectionItem);
                }
                else
                {
                    if (club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)) != null)
                    {
                        TimetableDownloadItem timetableDownloadItem = new TimetableDownloadItem(club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)));
                        ClubTimetableUrl = Sitecore.Links.LinkManager.GetItemUrl(timetableDownloadItem);
                    }
                }

                //Get enquiries link
                PageSummaryItem enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
                if(enqForm != null)
                {
                    ClubEnquiriesUrl = enqForm.Url + "?sc_trk=enq&c=" + club.InnerItem.ID.ToShortID();           
                }

                List<MediaItem> imageList;
                if (!String.IsNullOrEmpty(club.Imagegallery.Raw))
                {
                    imageList = club.Imagegallery.ListItems.ConvertAll(X => new MediaItem(X));
                    ImageList.DataSource = imageList;
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
                CookieHelper.AddClubsLastVisitedCookie(CookieKeyNames.ClubLastVisited, club.ClubId.Rendered);


                //Add club name to page title     
                string clubNameTitle = String.Format(" - {0}", club.Clubname.Raw);
                clubNameTitle = HtmlRemoval.StripTagsCharArray(clubNameTitle);

                Page.Title = Page.Title + clubNameTitle;

            }


        }
    }
}