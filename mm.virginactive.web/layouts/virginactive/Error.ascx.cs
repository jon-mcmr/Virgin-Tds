using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class Error : System.Web.UI.UserControl
    {
        protected ErrorItem ErrorItm = new ErrorItem(Sitecore.Context.Item);
        protected PageSummaryItem clubFinder = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClubFinder));
        protected PageSummaryItem classes = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClassesLanding));
        protected PageSummaryItem enqForm;

        protected void Page_Load(object sender, EventArgs e)
        {
            enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
        }
    }
}