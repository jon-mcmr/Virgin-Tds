using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubLanding : System.Web.UI.UserControl
    {
        protected ClubItem context = new ClubItem(Sitecore.Context.Item);
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}