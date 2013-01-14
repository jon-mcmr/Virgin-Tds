using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonFooter : System.Web.UI.UserControl
    {
        protected BespokeCampaignItem campaign = new BespokeCampaignItem(Sitecore.Context.Item);
        protected string privacyPolicyUrl;
        protected string termsConditionsUrl;

        public string PrivacyPolicyUrl
        {
            get { return privacyPolicyUrl; }
            set
            {
                privacyPolicyUrl = value;
            }
        }

        public string TermsConditionsUrl
        {
            get { return termsConditionsUrl; }
            set
            {
                termsConditionsUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            privacyPolicyUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.PrivacyPolicy);
            termsConditionsUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.TermsAndConditions);
        }
    }
}