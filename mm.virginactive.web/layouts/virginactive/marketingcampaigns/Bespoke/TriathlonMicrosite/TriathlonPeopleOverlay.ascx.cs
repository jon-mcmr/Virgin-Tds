using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonPeopleOverlay : System.Web.UI.UserControl
    {
        protected FeaturedProfileItem profile;

        public FeaturedProfileItem Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string RenderToString(FeaturedProfileItem profile)
        {
            return SitecoreHelper.RenderUserControl<TriathlonPeopleOverlay>("~/layouts/virginactive/marketingcampaigns/bespoke/triathlonmicrosite/triathlonpeopleoverlay.ascx",
                uc =>
                {
                    uc.Profile = profile;
                });
        }
    }
}