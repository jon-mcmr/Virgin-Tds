using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns.Triathlon;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonTrainingPlans : System.Web.UI.UserControl
    {

        protected TriathlonTrainingPlansItem currentItem = new TriathlonTrainingPlansItem(Sitecore.Context.Item);
        protected SubheadingLinkWidgetItem clubFinder;

        protected void Page_Load(object sender, EventArgs e)
        {
            string test = currentItem.Event1File.MediaUrl;

            //Get widget details
            clubFinder = Sitecore.Context.Database.GetItem(ItemPaths.TriathlonClubFinderWidget);		

        }
    }
}