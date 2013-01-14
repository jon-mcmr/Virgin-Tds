using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using mm.virginactive.orm.Timetable;
using mm.virginactive.web.layouts.virginactive.ajax;
using mm.virginactive.webservices.virginactive.classtimetable;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using System.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.SharedContent;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeTimetable : System.Web.UI.UserControl
    {
		protected MicrositeTimetableLandingItem micrositeTimetableLanding;
		protected MicrositeTimetableItem micrositeTimetable = new MicrositeTimetableItem(Sitecore.Context.Item);
		protected ClubItem currentClub;

        protected string clubId = "";
        protected string timetableType = "";
        protected string dateRangeStr = "";
        //protected string timetableNameStr = "";
        protected string clubMemberPhone = "";
        protected string alert = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
					ClubMicrositeLandingItem landing = new ClubMicrositeLandingItem(micrositeTimetable.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", ClubMicrositeLandingItem.TemplateId)));

                    currentClub = landing.Club.Item;
					micrositeTimetableLanding = micrositeTimetable.InnerItem.Parent;

					//Set Navigation elements
					List<PageSummaryItem> sectionTimetables = micrositeTimetable.InnerItem.Axes.SelectItems(String.Format("..//*[@@tid='{0}']", MicrositeTimetableItem.TemplateId)).ToList().ConvertAll(x => new PageSummaryItem(x));
					sectionTimetables.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

					if (sectionTimetables.Count > 1)
					{
						sectionTimetables.First().IsFirst = true;
						sectionTimetables.Last().IsLast = true;

						//Set current
						foreach (PageSummaryItem child in sectionTimetables)
						{
							Item current = child.InnerItem.Axes.SelectSingleItem(String.Format("descendant-or-self::*[@@id='{0}']", micrositeTimetable.ID.ToString()));
							if (current != null)
							{
								child.IsCurrent = true;
							}
						}

						SecondLevelElements.DataSource = sectionTimetables;
						SecondLevelElements.DataBind();
					}

                    clubId = currentClub.ClubId.Raw;
                    //timetableType = TimetableTypes.General;
					timetableType = micrositeTimetable.Type.Rendered;
					if (micrositeTimetable.Showalert.Checked)
                    {
						string textToParse = micrositeTimetable.Alerttext.Rendered;

                        Hashtable objTemplateVariables = new Hashtable();
                        objTemplateVariables.Add("SalesNumber", currentClub.Salestelephonenumber.Rendered);

                        Parser objParser = new Parser(objTemplateVariables);
                        objParser.SetTemplate(textToParse);
                        textToParse = objParser.Parse();

                        alert = @"<div class=""club-alert-panel""><div class=""club-alert"">" + textToParse + @"</div></div>";
                    }

                    clubMemberPhone = currentClub.Memberstelephonenumber.Raw != "" ? currentClub.Memberstelephonenumber.Raw : currentClub.Salestelephonenumber.Raw;

                    Item settings = Sitecore.Context.Database.GetItem(ItemPaths.TimetableShared);
                    if (settings != null)
                    {
                        TimetableSharedItem settingsItem = new TimetableSharedItem(settings);
                        TimetableUnavailableHeading.Text = settingsItem.Timetableunavailableheading.Text;
                        TimetableUnavailableMessage.Text =
                            settingsItem.Timetableunavailabletext.Text.Replace("#clubMemberPhone#", clubMemberPhone);
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

                    if ((tempItems.Count > 0))
                    {
                        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                        stringBuilder.Append(@"<strong>");
                        stringBuilder.Append(StringHelper.GenerateDateString(tempItems[0].Date));
                        stringBuilder.Append(@"</strong> ");
                        stringBuilder.Append(Translate.Text("to"));
                        stringBuilder.Append(@" <strong>");
                        stringBuilder.Append(StringHelper.GenerateDateString(tempItems[tempItems.Count - 1].Date));
                        stringBuilder.Append("</strong>");

                        dateRangeStr = stringBuilder.ToString();
                        //timetableNameStr = currentItem.PageSummary.DisplayName;

                        //create ajax controls
                        ClubTimetableResult timetableResult = this.Page.LoadControl("~/layouts/virginactive/microsite/MicrositeTimetableResult.ascx") as ClubTimetableResult;
                        ClubTimetableFilter timetableFilter = this.Page.LoadControl("~/layouts/virginactive/microsite/MicrositeTimetableFilter.ascx") as ClubTimetableFilter;

                        timetableResult.ClubId = clubId;
                        timetableResult.Type = timetableType;

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
                    Log.Error(String.Format("Error retrieving timetable for club {1}: {0}", ex.Message, "Microsite"), this);
                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                }

                //Add dynamic content to header
                HtmlHead head = (HtmlHead)Page.Header;

                //Add Open Tag
                if (Session["sess_User"] != null)
                {
                    User objUser = (User)Session["sess_User"];
                    if (objUser.Preferences != null)
                    {
                        if (objUser.Preferences.MarketingCookies && objUser.Preferences.MetricsCookies)
                        {
                            head.Controls.Add(new LiteralControl(OpenTagHelper.OpenTagVirginActiveUK));
                        }
                    }
                }
            }

        }
    }
}