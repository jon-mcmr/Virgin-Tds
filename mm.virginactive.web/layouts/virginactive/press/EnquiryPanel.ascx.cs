using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Press;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive.press
{
    public partial class EnquiryPanel : System.Web.UI.UserControl
    {
        protected PressLandingItem Landing = new PressLandingItem(Sitecore.Context.Database.GetItem(ItemPaths.PressLanding));
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}