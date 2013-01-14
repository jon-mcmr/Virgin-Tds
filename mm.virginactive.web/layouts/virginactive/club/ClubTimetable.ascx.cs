using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.orm.Timetable;
using mm.virginactive.web.layouts.virginactive.ajax;
using Sitecore.Data.Items;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Constants;
using mm.virginactive.webservices.virginactive.classtimetable;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Diagnostics;
using System.Web.Caching;
using System.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.SharedContent;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubTimetable : System.Web.UI.UserControl
    {
        protected string clubId = "";
        protected string clubMemberPhone = "";
        protected string timetableType = "";
        protected string dateRangeStr = "";
        protected string timetableNameStr = "";
        protected string alert = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                TimetableItem currentItem = new TimetableItem(Sitecore.Context.Item);

                ClubItem club = new ClubItem(Sitecore.Context.Item.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}"" or @@tid=""{1}""]", ClassicClubItem.TemplateId, LifeCentreItem.TemplateId)));
                try
                {
                    clubMemberPhone = club.Memberstelephonenumber.Rendered;
                    clubId = club.ClubId.Rendered;
                    timetableType = currentItem.Type.Rendered;

                    Item settings = Sitecore.Context.Database.GetItem(ItemPaths.TimetableShared);
                    if (settings != null)
                    {
                        TimetableSharedItem settingsItem = new TimetableSharedItem(settings);
                        TimetableUnavailableHeading.Text = settingsItem.Timetableunavailableheading.Text;
                        TimetableUnavailableMessage.Text =
                            settingsItem.Timetableunavailabletext.Text.Replace("#clubMemberPhone#", clubMemberPhone);
                    }


                    if(currentItem.Showalert.Checked)
                    {
                        string textToParse = currentItem.Alerttext.Rendered;

                        Hashtable objTemplateVariables = new Hashtable();
                        objTemplateVariables.Add("SalesNumber", club.Salestelephonenumber.Rendered);

                        Parser objParser = new Parser(objTemplateVariables);
                        objParser.SetTemplate(textToParse);
                        textToParse = objParser.Parse();

                        alert = @"<div class=""club-alert-panel""><div class=""club-alert""><p>" + textToParse + @"</p></div></div>";
                    }

                    ClassContainer ClubClasses = Utility.GetClubTimetable(clubId);

                    ////Need to lookup the daterange from the timetable
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

                    //Check each Timetable in collection is valid
                    foreach (Timetable item in ClubClasses.Timetables)
                    {
                        if (item.Dayname != null)
                        {
                            tempItems.Add(item);
                        }
                    }

                    if ((tempItems.Count > 0) && (currentItem.Type.Rendered != ""))
                    {
                        System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
                        markupBuilder.Append(@"<span class=""bold"">");
                        markupBuilder.Append(StringHelper.GenerateDateString(tempItems[0].Date));
                        markupBuilder.Append(@"</span> to <span class=""bold"">");
                        markupBuilder.Append(StringHelper.GenerateDateString(tempItems[tempItems.Count - 1].Date));
                        markupBuilder.Append("</span>");

                        dateRangeStr = markupBuilder.ToString();
                        timetableNameStr = currentItem.PageSummary.DisplayName;

                        //create ajax controls
                        ClubTimetableResult timetableResult = this.Page.LoadControl("~/layouts/virginactive/ajax/ClubTimetableResult.ascx") as ClubTimetableResult;
                        ClubTimetableFilter timetableFilter = this.Page.LoadControl("~/layouts/virginactive/ajax/ClubTimetableFilter.ascx") as ClubTimetableFilter;

                        timetableResult.ClubId = clubId;
                        timetableResult.Type = timetableType;
                        timetableResult.ShowBookClassTooltip = currentItem.Showbookclasstooltip.Checked;

                        string textToParse = currentItem.Bookclasstooltip.Raw;

                        Hashtable objTemplateVariables = new Hashtable();
                        objTemplateVariables.Add("salesnumber", club.Salestelephonenumber.Rendered);

                        Parser objParser = new Parser(objTemplateVariables);
                        objParser.SetTemplate(textToParse);
                        textToParse = objParser.Parse();

                        timetableResult.BookClassTooltip = textToParse;

                        filterPh.Controls.Add(timetableFilter);
                        resultPh.Controls.Add(timetableResult);
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        //hide print button
                        lstIcons.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    ErrorMessage.Visible = true;
                    //hide print button
                    lstIcons.Visible = false;
                    Log.Error(String.Format("Error retrieving timetable for club {1}: {0}", ex.Message, club.Clubname.Raw), this);
                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                }
            } 
            

            
        }
    }
}