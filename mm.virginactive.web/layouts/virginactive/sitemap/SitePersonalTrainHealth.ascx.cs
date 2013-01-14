using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using System.Text;
using System.Collections.Specialized;

namespace mm.virginactive.web.layouts.virginactive.sitemap
{
    public partial class SitePersonalTrainHealth : System.Web.UI.UserControl
    {
        protected PageSummaryItem healthAndBeautyLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.HealthAndBeautyLanding));
        protected PageSummaryItem personalTrainingLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.PersonalTrainingLanding));
        protected NavLinkSection KidsSection;
        protected PlaceHolder HealthSection;

        private bool headerIsH2 = true;

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
            //Bind Kids section
            KidsSection.Path = ItemPaths.KidsLanding;


            if (healthAndBeautyLanding != null)
            {
                StringBuilder sb = new StringBuilder();
                List<PageSummaryItem> healthAndBeautySections = healthAndBeautyLanding.InnerItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                foreach (PageSummaryItem section in healthAndBeautySections)
                {
                    sb.AppendFormat(@"<li><a href=""{0}#{2}"">{1}</a></li>", healthAndBeautyLanding.Url, section.NavigationTitle.Text, section.Name.ToLower());
                }
                HealthSection.Controls.Add(new LiteralControl(sb.ToString()));                
            }


            //Set Header type param if available
            string rawParameters = Attributes["sc_parameters"];
            NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
            if (!String.IsNullOrEmpty(parameters["HeaderIsH2"]))
            {
                HeaderIsH2 = Convert.ToBoolean(parameters["HeaderIsH2"]);
            }
        }
    }
}