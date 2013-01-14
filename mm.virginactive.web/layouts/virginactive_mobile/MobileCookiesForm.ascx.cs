using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Legals;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileCookiesForm : System.Web.UI.UserControl
    {
        protected CookiesFormItem cookiesForm = new CookiesFormItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {        


        }
    }
}