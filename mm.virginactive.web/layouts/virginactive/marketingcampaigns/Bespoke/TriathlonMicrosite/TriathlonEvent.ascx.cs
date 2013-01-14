using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns.Triathlon;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    

    public partial class TriathlonEvent : System.Web.UI.UserControl
    {
        protected string panel3link = "";

        protected TriathlonEventItem currentItem = new TriathlonEventItem(Sitecore.Context.Item);
        protected SubheadingLinkWidgetItem clubFinder;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Get widget details
            clubFinder = Sitecore.Context.Database.GetItem(ItemPaths.TriathlonClubFinderWidget);

            if (currentItem.Panel3Link.Url != "")
            {
                panel3link = @"<a href=""" + currentItem.Panel3Link.Url + @""" target=""_blank"">" + currentItem.Panel3LinkText.Rendered + @"</a>";
            }
        }
    }
}