using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.controls.Model;
using mm.virginactive.controls.Util;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class MembershipCampaignMapResult : System.Web.UI.UserControl
    {
        private double? lat;
        private double? lng;
        protected List<Club> clubs;
        protected string targetUrl = "";

        public string TargetUrl
        {
            get { return targetUrl; }
            set { targetUrl = value; }
        }

        public double? Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        public double? Lng
        {
            get { return lng; }
            set { lng = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            clubs = ClubUtil.GetNearestClubs(Lat, Lng, 5);


            if (clubs != null)
            {
                ClubList.DataSource = clubs;
                ClubList.DataBind();
            }
        }

        public static string RenderToString(double? lat, double? lng, string target)
        {
            return SitecoreHelper.RenderUserControl<MembershipCampaignMapResult>("~/layouts/virginactive/ajax/MembershipCampaignMapResult.ascx",
                uc =>
                {
                    uc.Lat = lat;
                    uc.Lng = lng;
                    uc.TargetUrl = target;
                });
        }
    }
}