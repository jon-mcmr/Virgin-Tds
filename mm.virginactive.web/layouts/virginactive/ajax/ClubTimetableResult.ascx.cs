using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Helpers;
using System.Text;
using System.IO;
using Sitecore.Web;
using Sitecore.Diagnostics;
using mm.virginactive.orm.Timetable;
using mm.virginactive.webservices.virginactive.classtimetable;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Web.Caching;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class ClubTimetableResult : System.Web.UI.UserControl
    {
        private string clubId;
        private string timetableType;
        private string venueHeading;
        private bool showBookClassTooltip;
        private string bookClassTooltip;
        //private string filters;

        public string ClubId
        {
            get { return clubId; }
            set { clubId = value; }
        }
        public string Type
        {
            get { return timetableType; }
            set { timetableType = value; }
        }
        public string VenueHeading
        {
            get { return venueHeading; }
            set { venueHeading = value; }
        }
        public bool ShowBookClassTooltip
        {
            get { return showBookClassTooltip; }
            set { showBookClassTooltip = value; }
        }
        public string BookClassTooltip
        {
            get { return bookClassTooltip; }
            set { bookClassTooltip = value; }
        }

        //public string Filters
        //{
        //    get { return filters; }
        //    set { filters = value; }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                ClassContainer ClubClasses = Utility.GetClubTimetable(clubId);

                //if (Cache[clubId] == null)
                //{
                //    //initialise webservice
                //    //mm.virginactive.webservices.virginactive.classtimetable.Service vs = new mm.virginactive.webservices.virginactive.classtimetable.Service();
                //    mm.virginactive.webservices.virginactive.classtimetable.VA_ClassTimetables vs = new mm.virginactive.webservices.virginactive.classtimetable.VA_ClassTimetables();
                //    //mm.virginactive.webservices.virginactive.classtimetable.VA_ClassTimetables vs = new mm.virginactive.webservices.virginactive.classtimetable.VA_ClassTimetables();							
                //    ClubClasses = vs.Classes(clubId);

                //    double cacheLiveTime = 20.0;
                //    Double.TryParse(Settings.TimetableCacheMinutes, out cacheLiveTime);
                //    Cache.Insert(clubId, ClubClasses, null, DateTime.Now.AddMinutes(cacheLiveTime), Cache.NoSlidingExpiration);
                //}
                //else //Fetch from cache
                //{
                //    ClubClasses = (struct_Classes)Cache[clubId];
                //}


                List<Timetable> tempItems = new List<Timetable>();

                foreach (Timetable item in ClubClasses.Timetables)
                {
                    int classCount = 0;
                    if (item.Dayname != null)
                    {
                        //Check that classes exist for that timetable
                        //Check which classes to display based on timetabletype
                        switch (timetableType)
                        {
                            case TimetableTypes.General:
                                venueHeading = Translate.Text("Studio");
                                //Show everything apart from classes from Swim source
                                foreach (Class classItem in item.Classes)
                                {
                                    if (classItem.Source != TimetableSources.Swim)
                                    {

                                        classCount++;
                                        break;
                                    }
                                }
                                break;
                            case TimetableTypes.Swim:
                                venueHeading = Translate.Text("Pool");
                                //Only show classes from Swim source
                                foreach (Class classItem in item.Classes)
                                {
                                    if (classItem.Source == TimetableSources.Swim)
                                    {
                                        classCount++;
                                        break;
                                    }
                                }
                                break;
                            default: break;
                        }

                        //Only add timetable to collection if classes exist
                        if (classCount > 0)
                        {
                            tempItems.Add(item);
                        }
                    }
                }
                rptDay.DataSource = tempItems;
                rptDay.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error retrieving timetable for club {1}: {0}", ex.Message, clubId), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        protected void rptDay_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = e.Item.DataItem as Timetable;

                if (item != null)
                {
                    //Bind Classes
                    var rptClass = e.Item.FindControl("rptClass") as Repeater;

                    List<Class> tempItems = new List<Class>();

                    //Check which classes to display based on timetabletype
                    switch (timetableType)
                    {
                        case TimetableTypes.General:
                            //Show everything apart from classes from Swim source
                            foreach (Class classItem in item.Classes)
                            {
                                if (classItem.Source != TimetableSources.Swim)
                                {
                                    tempItems.Add(classItem);
                                }
                            }
                            break;
                        case TimetableTypes.Swim:
                            //Only show classes from Swim source
                            foreach (Class classItem in item.Classes)
                            {
                                if (classItem.Source == TimetableSources.Swim)
                                {
                                    tempItems.Add(classItem);
                                }
                            }
                            break;
                        default: break;
                    }

                    rptClass.DataSource = tempItems;
                    rptClass.DataBind();
                }
            }
        }

        protected void rptClass_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Class Class = (Class)e.Item.DataItem;
                if (Class.Description.Trim() != "")
                {
                    ClubItem currentClub = SitecoreHelper.GetCurrentClub(Sitecore.Context.Item);
                    var InfoBox = e.Item.FindControl("ClassInfoBox") as Literal;
                    if (currentClub.InnerItem.TemplateID.ToString() == ClassicClubItem.TemplateId)
                    {
                        InfoBox.Text = @"<a class=""classtip""><img src=""/virginactive/images/classic/info-classic.png"" alt=""More information"" height=""16"" width=""16"" /><span class=""classInfoBox"">" + Class.Description + "</span></a>";
                    }
                    else
                    {
                        InfoBox.Text = @"<a class=""classtip""><img src=""/virginactive/images/icons/info.gif"" alt=""More information"" height=""16"" width=""16"" /><span class=""classInfoBox"">" + Class.Description + "</span></a>";
                    }
                }

                if (Class.IsBookable == true)
                {
                    var BookClassOrTokenLink = e.Item.FindControl("BookClassOrTokenLink") as Literal;

                    if (showBookClassTooltip && bookClassTooltip != "")
                    {
                        BookClassOrTokenLink.Text = @"<span class=""classtip click book"">" + Translate.Text("Book class") + @"<span class=""classInfoBox"">" + bookClassTooltip + "</span></span>";
                    }
                    else
                    {
                        //Get booking portal url
                        string memberLoginUrl = SitecoreHelper.GetMembershipLoginUrl(clubId);
                        BookClassOrTokenLink.Text = @"<span class=""bookClassLink""><a href=""" + memberLoginUrl + @""" title=""" + Translate.Text("Book class") + @""">" + Translate.Text("Book class") + @"</a></span>";
                    }

                }
                else if (Class.IsTokenRequired == true)
                {
                    ClubItem currentClub = SitecoreHelper.GetCurrentClub(Sitecore.Context.Item);
                    var BookClassOrTokenLink = e.Item.FindControl("BookClassOrTokenLink") as Literal;
                    if (currentClub.InnerItem.TemplateID.ToString() == ClassicClubItem.TemplateId)
                    {
                        BookClassOrTokenLink.Text = @"<span class=""tokenLink bookYes"">" + Translate.Text("Yes") + @"<a class=""classtip""><img src=""/virginactive/images/classic/info-classic.png"" alt=""More information"" height=""16"" width=""16"" /><span class=""tokenToolTip"">" + Translate.Text("Please book this class at Reception") + "</span></a></span>";
                    }
                    else
                    {
                        BookClassOrTokenLink.Text = @"<span class=""tokenLink bookYes"">" + Translate.Text("Yes") + @"<a class=""classtip""><img src=""/virginactive/images/icons/info.gif"" alt=""More information"" height=""16"" width=""16"" /><span class=""tokenToolTip"">" + Translate.Text("Please book this class at Reception") + "</span></a></span>";
                    }

                }
                else
                {
                    var BookClassOrTokenLink = e.Item.FindControl("BookClassOrTokenLink") as Literal;
                    BookClassOrTokenLink.Text = @"<span class=""column-centre"">" + "-" + @"</span>";
                }
            }
        }

        public string GetTimetableDay(Timetable item)
        {
            //switch(item.Dayname)
            //{
            //    case "Thursday": return item.Dayname.ToUpper().Substring(0,4);
            //    default: return item.Dayname.ToUpper().Substring(0, 3);
            //}
            return item.Dayname.ToUpper().Substring(0, 3);
        }

        public string GetTimetableDate(Timetable item)
        {
            return item.Date.ToString("dd");
        }

        public string GetTimetableClassName(Timetable item)
        {
            return "timetable_" + item.Dayname.ToLower().Substring(0, 3);
        }

        public static string RenderToString(string clubId, string timetableType, bool showBookClassTooltip, string bookClassTooltip)
        {
            return SitecoreHelper.RenderUserControl<ClubTimetableResult>("~/layouts/virginactive/ajax/ClubTimetableResult.ascx",
                uc =>
                {
                    uc.ClubId = clubId;
                    uc.Type = timetableType;
                    uc.ShowBookClassTooltip = showBookClassTooltip;
                    uc.BookClassTooltip = bookClassTooltip;
                });
        }

    }
}