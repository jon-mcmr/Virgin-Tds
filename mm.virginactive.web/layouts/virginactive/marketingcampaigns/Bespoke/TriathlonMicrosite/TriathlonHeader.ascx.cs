using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Helpers;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Text;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.common.Globalization;
using mm.virginactive.controls.Model;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonHeader : System.Web.UI.UserControl
    {
        protected string londonTriathlonUrl;
        protected PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);
        protected PageSummaryItem currentSection;

        public string LondonTriathlonUrl
        {
            get { return londonTriathlonUrl; }
            set
            {
                londonTriathlonUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
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

	
            System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();
	  	
            markupBuilder.Append(@"<meta name='viewport' content='width=1020'>");
            markupBuilder.Append(@"<link rel='apple-touch-icon' href='/virginactive/images/apple-touch-icon.png'>");
            markupBuilder.Append(@"<link rel='shortcut icon' href='/virginactive/images/favicon.ico'>");
            markupBuilder.Append(@"<link href='/va_campaigns/Bespoke/LondonTriathlonII/css/styles.css' rel='stylesheet' type='text/css' media='screen' />");
            head.Controls.Add(new LiteralControl(markupBuilder.ToString()));

            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"                  
                    <script src='/va_campaigns/Bespoke/LondonTriathlonII/js/plugins.js' type='text/javascript'></script>
                    <script src='/va_campaigns/Bespoke/LondonTriathlonII/js/scripts.js' type='text/javascript'></script>
                    
                   "));
            }

            //Populate Nav links

            //Get triathlon root item
            ClubMicrositeItem micrositeItem = currentItem.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", ClubMicrositeItem.TemplateId));

            if (micrositeItem != null)
            {
                londonTriathlonUrl = micrositeItem.CampaignBase.QualifiedUrl;

                //Get child sections
                List<PageSummaryItem> childSectionList = new List<PageSummaryItem>();

                string[] sectionMicrositeIds = ItemPaths.MicrositeSectionTemplates.Split('|');
                StringBuilder query = new StringBuilder();
                query.Append("child::*[");
                foreach (string sectionTemplateId in sectionMicrositeIds)
                {
                    query.Append(String.Format("@@tid='{0}' or ", sectionTemplateId));
                }
                query.Remove(query.Length - 4, 4);
                query.Append("]");

                if (micrositeItem.InnerItem.Axes.SelectItems(query.ToString()) != null)
                {
                    //Add the root as the "home" item first
                    childSectionList.Add((PageSummaryItem)micrositeItem.InnerItem);

                    List<Item> contextAncestorsOrSelf = currentItem.InnerItem.Axes.SelectItems("./ancestor-or-self::*").ToList();
                    //need to figure out the 'active' first level item (the one to highlight)
                    //This can then also be used to build to second level

                    //Add the child sections
                    foreach (PageSummaryItem section in micrositeItem.InnerItem.Axes.SelectItems(query.ToString()).ToList().ConvertAll(Y => new PageSummaryItem(Y)))
                    {
                        if (SitecoreHelper.ListContainsItem(contextAncestorsOrSelf, section.InnerItem))
                        {
                            section.IsCurrent = true;
                            currentSection = section;
                        }
                        childSectionList.Add(section);
                    }

                    //if not in a section set Home link as current
                    if (currentSection == null)
                    {
                        childSectionList[0].IsCurrent = true;
                    }

                    if (childSectionList.Count > 0)
                    {
                        NavLinks.DataSource = childSectionList;
                        NavLinks.DataBind();
                    }
                }



            }

        }
    }
}