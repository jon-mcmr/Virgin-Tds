using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership;
using mm.virginactive.wrappers.VirginActive.PageTemplates.PersonalTraining;
using Sitecore.Web;
using Sitecore.Diagnostics;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class PersonalTraining : System.Web.UI.UserControl
    {
        protected PageSummaryItem enqForm;
        protected PersonalTrainingItem currentItem = Sitecore.Context.Item;
        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.EnquiryForm, "Enquiry form path missing");

            enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));
        }
    }
}