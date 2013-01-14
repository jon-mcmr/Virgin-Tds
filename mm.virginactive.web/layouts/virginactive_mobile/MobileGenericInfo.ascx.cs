using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileGenericInfo : System.Web.UI.UserControl
    {
        protected GeneralPageItem Article = new GeneralPageItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}