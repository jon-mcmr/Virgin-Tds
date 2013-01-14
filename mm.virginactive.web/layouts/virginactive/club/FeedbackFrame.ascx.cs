using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Web;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class FeedbackFrame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string page = WebUtil.GetQueryString("page");
            if(String.IsNullOrEmpty(page)) {
                //Load the regular club feedback form
                ClubFeedback clubFeedback = Page.LoadControl("~/layouts/virginactive/club/ClubFeedback.ascx") as ClubFeedback;
                PagePh.Controls.Add(clubFeedback);
            } else {
                //Load the kids
                ClubKidsFeedback clubKidsFeedback = Page.LoadControl("~/layouts/virginactive/club/ClubKidsFeedback.ascx") as ClubKidsFeedback;
                PagePh.Controls.Add(clubKidsFeedback);
            }
        }
    }
}