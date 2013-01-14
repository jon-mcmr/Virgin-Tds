using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Web;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileClubFinderFrame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string page = WebUtil.GetQueryString("action");

            if (page == "list")
            {
                //Load the club list page
                MobileClubList mobileClubList = Page.LoadControl("~/layouts/virginactive_mobile/MobileClubList.ascx") as MobileClubList;
                PagePh.Controls.Add(mobileClubList);
            }
            else if (page == "timetables")
            {
                //Load the club list page -timetebles
                MobileTimetableClubList mobileClubList = Page.LoadControl("~/layouts/virginactive_mobile/MobileTimetableClubList.ascx") as MobileTimetableClubList;
                PagePh.Controls.Add(mobileClubList);
            }
            else
            {
                //Load the regular club finder page
                MobileClubFinder mobileClubFinder = Page.LoadControl("~/layouts/virginactive_mobile/MobileClubFinder.ascx") as MobileClubFinder;
                PagePh.Controls.Add(mobileClubFinder);
            }

        }
    }
}