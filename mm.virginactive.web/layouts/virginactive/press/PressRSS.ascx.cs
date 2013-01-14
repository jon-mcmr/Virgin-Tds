using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive.press
{
    public partial class PressRSS : System.Web.UI.UserControl
    {
        protected Item feed = Sitecore.Context.Database.GetItem(ItemPaths.PressRSSFeed);

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}