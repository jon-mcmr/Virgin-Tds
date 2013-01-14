using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using Sitecore.Data;
using Sitecore.Collections;
using CustomItemGenerator.Fields.ListTypes;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class ClubFinderWidget : System.Web.UI.UserControl
    {
        
        protected SubheadingLinkWidgetItem clubFinder;
        protected string clubFinderItemPath = ItemPaths.ClubFinderWidget;

        protected void Page_Load(object sender, EventArgs e)
        {
            clubFinder = Sitecore.Context.Database.GetItem(clubFinderItemPath);
            
        }
    }
}