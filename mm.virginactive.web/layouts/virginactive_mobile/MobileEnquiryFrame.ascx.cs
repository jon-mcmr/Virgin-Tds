using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Web;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileEnquiryFrame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string page = WebUtil.GetQueryString("action");

            if (page == "confirm")
            {
                //Load the club list page
                MobileEnquiryThanks mobileEnquiryThanks = Page.LoadControl("~/layouts/virginactive_mobile/MobileEnquiryThanks.ascx") as MobileEnquiryThanks;
                PagePh.Controls.Add(mobileEnquiryThanks);
            }
            else
            {
                //Load the regular enquiry form
                MobileEnquiryForm mobileEnquiryForm = Page.LoadControl("~/layouts/virginactive_mobile/MobileEnquiryForm.ascx") as MobileEnquiryForm;
                PagePh.Controls.Add(mobileEnquiryForm);
            }
        }
    }
}