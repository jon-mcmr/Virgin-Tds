using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.orm.Timetable;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using Sitecore.Web;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using Sitecore.Links;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using System.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.SharedContent;

namespace mm.virginactive.web.layouts.Mobile
{

    public partial class MobileClubClasses : System.Web.UI.UserControl
    {
        protected TimetableItem timetableItem;
        protected ClubItem currentClub;

        protected string ClubEnquiriesUrl = "";
        protected string ClubName = "";
        protected string ClubHomeUrl = "";
		protected string BookOnlineUrl = "";
        private string venueHeading = "";
        protected int day; // 0=Today, 1=Tomorrow ..7=One week from now
        string timetableType = "";
        protected string dateStr = "";
        protected string clubMemberPhone = "";
        protected string ClubTimetablesUrl = "";
        protected string alert = "";

        public string VenueHeading
        {
            get { return venueHeading; }
            set { venueHeading = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
			//This is a timetable item (or microsite timetable item -treated the same)
			timetableItem = new TimetableItem(Sitecore.Context.Item);

			List<PageSummaryItem> sectionTimetables = null;
			if (Sitecore.Context.Item.TemplateID.ToString() == TimetableItem.TemplateId.ToString())
			{
				//This is a timetable item
				currentClub = new ClubItem(Sitecore.Context.Item.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}"" or @@tid=""{1}""]", ClassicClubItem.TemplateId, LifeCentreItem.TemplateId)));
				ClubHomeUrl = currentClub != null ? LinkManager.GetItemUrl(currentClub.InnerItem) : "";

				sectionTimetables = timetableItem.InnerItem.Axes.SelectItems(String.Format("..//*[@@tid='{0}']", TimetableItem.TemplateId)).ToList().ConvertAll(x => new PageSummaryItem(x));
				sectionTimetables.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items
			}
			else
			{
				//This is a microsoft timetable item
				MicrositeHomeItem micrositeHome = new MicrositeHomeItem(timetableItem.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", MicrositeHomeItem.TemplateId)));
				ClubMicrositeLandingItem micrositeLanding = micrositeHome.InnerItem.Parent;
				currentClub = micrositeLanding.Club.Item;
				ClubHomeUrl = currentClub != null ? LinkManager.GetItemUrl(micrositeHome.InnerItem) : "";

				sectionTimetables = timetableItem.InnerItem.Axes.SelectItems(String.Format("..//*[@@tid='{0}']", MicrositeTimetableItem.TemplateId)).ToList().ConvertAll(x => new PageSummaryItem(x));
				sectionTimetables.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items
			}

			//Set Navigation elements

			if (sectionTimetables != null && sectionTimetables.Count > 1)
			{
				sectionTimetables.First().IsFirst = true;
				sectionTimetables.Last().IsLast = true;

				//Set current
				foreach (PageSummaryItem child in sectionTimetables)
				{
					Item current = child.InnerItem.Axes.SelectSingleItem(String.Format("descendant-or-self::*[@@id='{0}']", timetableItem.ID.ToString()));
					if (current != null)
					{
						child.IsCurrent = true;
					}
				}

				SecondLevelElements.DataSource = sectionTimetables;
				SecondLevelElements.DataBind();
			}

			PageSummaryItem timetableSectionItem = new PageSummaryItem(timetableItem.InnerItem.Parent);
			ClubTimetablesUrl = Sitecore.Links.LinkManager.GetItemUrl(timetableSectionItem);


            if (currentClub != null)
            {
                ClubName = currentClub.Clubname.Rendered;
                clubMemberPhone = currentClub.Memberstelephonenumber.Rendered;

                //Get enquiries link
                PageSummaryItem enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
                if (enqForm != null)
                {
                    ClubEnquiriesUrl = enqForm.Url + "?sc_trk=enq&c=" + currentClub.InnerItem.ID.ToShortID();
                }

				//Set Book Online Link
				if (currentClub.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric || currentClub.GetCrmSystem() == ClubCrmSystemTypes.Vision)
				{
					//Show link
					pnlBookOnline.Visible = true;
					BookOnlineUrl = SitecoreHelper.GetMembershipLoginUrl(currentClub);
				}

                if (timetableItem.Showalert.Checked)
                {
                    string textToParse = timetableItem.Alerttext.Rendered;

                    Hashtable objTemplateVariables = new Hashtable();
                    objTemplateVariables.Add("SalesNumber", currentClub.Salestelephonenumber.Rendered);

                    Parser objParser = new Parser(objTemplateVariables);
                    objParser.SetTemplate(textToParse);
                    textToParse = objParser.Parse();

                    alert = @"<div class=""message""><p>" + textToParse + @"</p></div>";
                }
            }



            //What day are we rendering?
            int.TryParse(WebUtil.GetQueryString("day"), out day);

            timetableType = timetableItem.Type.Raw;

            Item settings = Sitecore.Context.Database.GetItem(ItemPaths.TimetableShared);
            if (settings != null)
            {
                TimetableSharedItem settingsItem = new TimetableSharedItem(settings);
                //TimetableUnavailableHeading.Text = settingsItem.Timetableunavailableheading.Text;
                TimetableUnavailableMessage.Text =
                    settingsItem.Mobiletimetableunavailabletext.Text.Replace("#clubMemberPhone#", clubMemberPhone);
            }

            try
            {
                //Only showing for one week from today (0)
                if (day >= 0 && day < 7)
                {

                    ClassContainer ClubClasses = Utility.GetClubTimetable(currentClub.ClubId.Raw);

                    if (ClubClasses != null)
                    {
                        //Set Previous Next Links
                        if (day == 0)
                        {
                            ltrNext.Text = @"<a href=""" + timetableItem.PageSummary.Url + @"?day=" + Convert.ToString(day + 1) + @""" class=""next"">" + Translate.Text("Tomorrow") + "</a>";
                        }
                        else if (day == 1)
                        {
                            ltrPrevious.Text = @"<a href=""" + timetableItem.PageSummary.Url + @"?day=" + Convert.ToString(day - 1) + @""" class=""previous"">" + Translate.Text("Today") + "</a>";
                            string nextDayName = DateTime.Now.AddDays(day + 1).DayOfWeek.ToString();
                            ltrNext.Text = @"<a href=""" + timetableItem.PageSummary.Url + @"?day=" + Convert.ToString(day + 1) + @""" class=""next"">" + Translate.Text(nextDayName) + "</a>";
                        }
                        else if (day == 6)
                        {

                            string previousDayName = DateTime.Now.AddDays(day - 1).DayOfWeek.ToString();
                            ltrPrevious.Text = @"<a href=""" + timetableItem.PageSummary.Url + @"?day=" + Convert.ToString(day - 1) + @""" class=""previous"">" + Translate.Text(previousDayName) + "</a>";
                        }
                        else
                        {
                            string previousDayName = DateTime.Now.AddDays(day - 1).DayOfWeek.ToString();
                            ltrPrevious.Text = @"<a href=""" + timetableItem.PageSummary.Url + @"?day=" + Convert.ToString(day - 1) + @""" class=""previous"">" + Translate.Text(previousDayName) + "</a>";
                            string nextDayName = DateTime.Now.AddDays(day + 1).DayOfWeek.ToString();
                            ltrNext.Text = @"<a href=""" + timetableItem.PageSummary.Url + @"?day=" + Convert.ToString(day + 1) + @""" class=""next"">" + Translate.Text(nextDayName) + "</a>";
                        }

                        //Set Date String
                        dateStr = StringHelper.GenerateDateStringMobile(DateTime.Now.AddDays(day));

                        foreach (Timetable item in ClubClasses.Timetables)
                        {
                            int classCount = 0;
                            if (item.Dayname != null)
                            {
                                //Check what day we are showing
                                if (item.Dayname.Equals(DateTime.Now.AddDays(day).DayOfWeek.ToString(), StringComparison.OrdinalIgnoreCase))
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
											//Do not show bookings panel if we are showing Swimming timetable
											pnlBookings.Visible = false;

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
                                        //Bind Classes
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
                        }
                    }
                    else
                    {
                        pnlUnavailable.Visible = true;
                        pnlClasses.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                pnlUnavailable.Visible = true;
                pnlClasses.Visible = false;
            }

            //Set club last visited 
            User objUser = new User();
            if (Session["sess_User"] != null)
            {
                objUser = (User)Session["sess_User"];
            }
            objUser.ClubLastVisitedID = currentClub.ClubId.Rendered;
            Session["sess_User"] = objUser;

            CookieHelper.AddClubsLastVisitedCookie(CookieKeyNames.ClubLastVisited, currentClub.ClubId.Rendered);

            //Add club name to page title     
            string clubNameTitle = String.Format(" - {0}", currentClub.Clubname.Raw);
            clubNameTitle = HtmlRemoval.StripTagsCharArray(clubNameTitle);

            Page.Title = Page.Title + clubNameTitle;
        }
    }
}