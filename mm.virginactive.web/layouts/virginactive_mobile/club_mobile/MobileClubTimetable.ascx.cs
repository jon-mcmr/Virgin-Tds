using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using System.Collections;
using mm.virginactive.common.Helpers;
using mm.virginactive.orm.Timetable;
using mm.virginactive.common.Globalization;
using Sitecore.Data.Items;
using Sitecore.Links;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileClubTimetable : System.Web.UI.UserControl
    {
        protected string clubId = "";
        //protected string timetableType = "";
        protected string dateRangeStr = "";
        protected string clubMemberPhone = "";
        protected PageSummaryItem firstTimeTableItem = null;
        protected string ClubName = "";
        protected string ClubHomeUrl = "";
        protected Item current = Sitecore.Context.Item;
        //protected string alert = "";
        protected ClubItem club;
        protected MicrositeHomeItem micrositeHome;
        protected ClubMicrositeLandingItem micrositeLanding;
        protected bool isMicrosite = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Sitecore.Context.Item.TemplateID.ToString() == SectionContainerItem.TemplateId.ToString())
                    {
                        club = new ClubItem(current.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}"" or @@tid=""{1}""]", ClassicClubItem.TemplateId, LifeCentreItem.TemplateId)));
                        ClubHomeUrl = LinkManager.GetItemUrl(club.InnerItem);
                    }
                    else
                    {
                        //Context item is a microsite timetable
                        isMicrosite = true;

                        micrositeLanding = new ClubMicrositeLandingItem(current.Parent.Parent);
                        micrositeHome = new MicrositeHomeItem(current.Parent);

                        club = micrositeLanding.Club.Item;
                        ClubHomeUrl = LinkManager.GetItemUrl(micrositeHome.InnerItem);
                    }


                    ClubName = club.Clubname.Rendered;
                    clubId = club.ClubId.Rendered;
                    clubMemberPhone = club.Memberstelephonenumber.Rendered;

                    //Get First Item in section for links
                    foreach (Item child in current.Children)
                    {
                        PageSummaryItem itm = new PageSummaryItem(child);
                        if (itm.Hidefrommenu != null)
                        {
                            if (!itm.Hidefrommenu.Checked)
                            {
                                firstTimeTableItem = itm;
                                break;
                            }
                        }
                    }

                    ClassContainer ClubClasses = Utility.GetClubTimetable(clubId);
                    if (ClubClasses != null)
                    {
                        List<Timetable> tempItems = new List<Timetable>();

                        //Check each Timetable in collection is valid
                        foreach (Timetable item in ClubClasses.Timetables)
                        {
                            if (item.Dayname != null)
                            {
                                tempItems.Add(item);
                            }
                        }

                        if ((tempItems.Count > 0))
                        {
                            dateRangeStr = StringHelper.GenerateDateRangeString(tempItems[0].Date, tempItems[tempItems.Count - 1].Date);

                            TimetableDayList.DataSource = tempItems;
                            TimetableDayList.DataBind();
                        }
                        else
                        {
                            pnlUnavailable.Visible = true;
                            pnlClasses.Visible = false;
                        }
                    }
                    else
                    {
                        pnlUnavailable.Visible = true;
                        pnlClasses.Visible = false;
                    }


                }
                catch (Exception ex)
                {
                    pnlUnavailable.Visible = true;
                    pnlClasses.Visible = false;
                    Log.Error(String.Format("Error retrieving timetable for club {1}: {0}", ex.Message, club != null ? club.Clubname.Raw : ""), this);
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

        protected void TimetableDayList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var timetable = dataItem.DataItem as Timetable;

                if (timetable != null && firstTimeTableItem != null)
                {
                    //Set Links
                    var ltrTimetableLink = e.Item.FindControl("ltrTimetableLink") as System.Web.UI.WebControls.Literal;

                    string dayName = "";
                    if (timetable.Date.Day == DateTime.Now.Day)
                    {
                        dayName = Translate.Text("Today");
                    }
                    else if (timetable.Date.Day == DateTime.Now.AddDays(1).Day)
                    {
                        dayName = Translate.Text("Tomorrow");
                    }
                    else
                    {
                        dayName = timetable.Dayname;
                    }

                    string dayUrlLink = "";

                    //Get the date
                    for (int dayIndex = 0; dayIndex < 7; dayIndex++)
                    {
                        if (timetable.Date.Day == DateTime.Now.AddDays(dayIndex).Day)
                        {
                            dayUrlLink = firstTimeTableItem.Url + "?day=" + dayIndex;

                        }
                    }
                    if (dayUrlLink != "")
                    {
                        ltrTimetableLink.Text = @"<a href=""" + dayUrlLink + @"""><span class=""arrow"">" + timetable.Date.ToString("dd") + "<strong> " + dayName + "</strong></span></a>";
                    }
                }

            }
        }
    }
}