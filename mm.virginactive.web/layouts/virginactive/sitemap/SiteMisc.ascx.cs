using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Press;
using Sitecore.Diagnostics;

namespace mm.virginactive.web.layouts.virginactive.sitemap
{
    public partial class SiteMisc : System.Web.UI.UserControl
    {

        private bool headerIsH2 = true;
        private string cssClass = @" class=""last""";
        protected PressLandingItem Press;
        protected string partnersUrl = "";
        protected string legalsUrl = "";

        public string PartnersUrl
        {
            get { return partnersUrl; }
            set
            {
                partnersUrl = value;
            }
        }
        public string LegalsUrl
        {
            get { return legalsUrl; }
            set
            {
                legalsUrl = value;
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

        public string CssClass
        {
            get { return cssClass; }
            set
            {
                cssClass = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.AboutUs, "About Us path null or empty");
            Assert.ArgumentNotNullOrEmpty(ItemPaths.Legal, "legals path null or empty");
            Assert.ArgumentNotNullOrEmpty(ItemPaths.PressLanding, "Press landing path null or empty");

            AboutUs.Path = ItemPaths.AboutUs;
            Legals.Path = ItemPaths.Legal;
            Press = Sitecore.Context.Database.GetItem(ItemPaths.PressLanding);

            //Set Header type param if available
            string rawParameters = Attributes["sc_parameters"];
            NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
            if (!String.IsNullOrEmpty(parameters["HeaderIsH2"]))
            {
                HeaderIsH2 = Convert.ToBoolean(parameters["HeaderIsH2"]);
            }
            //Set CSS class if available
            if (!String.IsNullOrEmpty(parameters["CssClass"]))
            {
                CssClass = parameters["CssClass"];
            }

            GetPartners();
        }

        private void GetPartners()
        {
            Item componentBase = Sitecore.Context.Database.GetItem(ItemPaths.Components);
            Item partnerFolder = componentBase.Axes.SelectSingleItem("*[@@key='partners']");

            if (partnerFolder != null)
            {
                PartnerList.DataSource =
                    partnerFolder.Axes.SelectItems(String.Format("*[@@tid='{0}']", LinkItem.TemplateId)).ToList().ConvertAll(x => new LinkItem(x));
                PartnerList.DataBind();
            }
        }
    }
}