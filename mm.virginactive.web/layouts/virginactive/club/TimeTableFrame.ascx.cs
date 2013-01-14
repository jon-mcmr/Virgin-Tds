using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Web;
using Sitecore.Web;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class TimeTableFrame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get the details of the club
            ClubItem currentClub = new ClubItem(Sitecore.Context.Item.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}"" or @@tid=""{1}""]", ClassicClubItem.TemplateId, LifeCentreItem.TemplateId)));

            if (currentClub.GetCrmSystem() == ClubCrmSystemTypes.ClubCentric)
            {
                //Load the download timetable area
                ClubTimetable clubTimetable = Page.LoadControl("~/layouts/virginactive/club/ClubFeedback.ascx") as ClubTimetable;
                PagePh.Controls.Add(clubTimetable);
            }
            else
            {
                //Load rendered time tables
                ClubTimeTableDownload clubTimetableDownload = Page.LoadControl("~/layouts/virginactive/club/ClubKidsFeedback.ascx") as ClubTimeTableDownload;
                PagePh.Controls.Add(clubTimetableDownload);
            }
        }
    }
}