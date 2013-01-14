using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using System.Text;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonSectionListing : System.Web.UI.UserControl
    {
        protected MicrositeSectionItem currentItem = new MicrositeSectionItem(Sitecore.Context.Item);
        protected SubheadingLinkWidgetItem clubFinder;
        protected String pageName;

        public string PageName
        {
            get { return pageName; }
            set
            {
                pageName = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = currentItem.Name.Replace(" ", "-");

            if (currentItem.InnerItem.HasChildren)
            {
                
                //Check if we have faqs
                if (currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FAQItem.TemplateId)) != null)
                {
                    List<FAQItem> faqList = currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FAQItem.TemplateId)).ToList().ConvertAll(Y => new FAQItem(Y));

                    if (faqList.Count > 0)
                    {
                        FAQPanels.DataSource = faqList;
                        FAQPanels.DataBind();
                    }
                    else
                    {
                        pnlFaqs.Visible = false;
                    }
                }
                else
                {
                    pnlFaqs.Visible = false;
                }

                //Check if we have links
                if (currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FileImageLinkWidgetItem.TemplateId)) != null)
                {
                    List<FileImageLinkWidgetItem> linkList = currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FileImageLinkWidgetItem.TemplateId)).ToList().ConvertAll(Y => new FileImageLinkWidgetItem(Y));

                    if (linkList.Count > 0)
                    {
                        LinkPanels.DataSource = linkList;
                        LinkPanels.DataBind();
                    }
                }
            }

            //Get widget details
            clubFinder = Sitecore.Context.Database.GetItem(ItemPaths.TriathlonClubFinderWidget);
            
        }

    }
}