using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Diagnostics;
using mm.virginactive.web.layouts.virginactive.navigation;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.sitemap
{
    public partial class SiteMembership : System.Web.UI.UserControl
    {
        private bool headerIsH2 = true;
        protected PageSummaryItem memberBenifits, memberReciprocal;
        protected NavLinkSection MemberOptions;
        protected string membershipUrl = "";
        public string MembershipUrl
        {
            get { return membershipUrl; }
            set
            {
                membershipUrl = value;
            }
        }

        public bool HeaderIsH2
        {
            get { return headerIsH2; }
            set
            {
                headerIsH2 = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Club Finder Details
            membershipUrl = SitecoreHelper.GetQualifiedUrlFromItemPath(ItemPaths.MembershipOptions);

            Assert.ArgumentNotNullOrEmpty(ItemPaths.MembershipOptions, "member option path null or empty");
            Assert.ArgumentNotNullOrEmpty(ItemPaths.MembershipReciprocal, "Membership  path null or empty");
            

            MemberOptions.Path = ItemPaths.MembershipOptions;
            //Set Header type param if available
            string rawParameters = Attributes["sc_parameters"];
            NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
            if (!String.IsNullOrEmpty(parameters["HeaderIsH2"]))
            {
                HeaderIsH2 = Convert.ToBoolean(parameters["HeaderIsH2"]);
            }

            memberBenifits = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.MemberBenefits));
            memberReciprocal = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.MembershipReciprocal));
        }
    }
}