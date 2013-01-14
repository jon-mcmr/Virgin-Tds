using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.screportdal.Models;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingWhatNextThanks : System.Web.UI.UserControl
    {
        protected LandingWhatsNextItem currentItem = new LandingWhatsNextItem(Sitecore.Context.Item);
        private Customer _objCustomer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetCustomer();
                SetSitecoreData();
            }
        }

        private void SetSitecoreData()
        {
            //For any sitecore data related code here..      
            if (_objCustomer != null)
                ltSubHeading.Text = String.Format(currentItem.ThanksYouSubheading.Rendered, _objCustomer.Firstname);
            else
                ltSubHeading.Text = String.Format(currentItem.ThanksYouSubheading.Rendered, String.Empty);
        }

        private void SetCustomer()
        {
            if (Session["sess_Customer"] != null)
                _objCustomer = (Customer)Session["sess_Customer"];
        }
    }
}