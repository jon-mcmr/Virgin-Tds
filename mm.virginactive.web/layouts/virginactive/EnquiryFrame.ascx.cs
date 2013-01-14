using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Web;
using mm.virginactive.web.layouts.virginactive.clubfinder;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.controls.Model;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class EnquiryFrame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            User objUser = new User();
            //Set user session variable
            if (Session["sess_User"] != null)
            {
                objUser = (User)Session["sess_User"];
            }

            string page = WebUtil.GetQueryString("page");
            if (page == "corp")
            {
                objUser.EnquiryFormType = EnquiryFormTypes.Corporate;

                //Load the corporate
                CorporateEnquiryForm corporateEnquiryForm = Page.LoadControl("~/layouts/virginactive/CorporateEnquiryForm.ascx") as CorporateEnquiryForm;
                PagePh.Controls.Add(corporateEnquiryForm);
            }
            else if (page == "existing")
            {
                //Load the regular club enquiry form but mark as 'join existing scheme'

                objUser.EnquiryFormType = EnquiryFormTypes.ExistingScheme;

                ClubEnquiryForm clubEnquiryForm = Page.LoadControl("~/layouts/virginactive/clubfinder/ClubEnquiryForm.ascx") as ClubEnquiryForm;
                PagePh.Controls.Add(clubEnquiryForm);
            }
            else if (page == "bookatour")
            {
                //Load the regular club enquiry form but mark as 'join existing scheme'

                objUser.EnquiryFormType = EnquiryFormTypes.BookATour;

                ClubEnquiryForm clubEnquiryForm = Page.LoadControl("~/layouts/virginactive/clubfinder/ClubEnquiryForm.ascx") as ClubEnquiryForm;
                PagePh.Controls.Add(clubEnquiryForm);
            }
            else if (page == "groupoffer")
            {
                //Load the group offer

                objUser.EnquiryFormType = EnquiryFormTypes.GroupOffer;

                GroupOfferEnquiryForm clubEnquiryForm = Page.LoadControl("~/layouts/virginactive/clubfinder/GroupOfferEnquiryForm.ascx") as GroupOfferEnquiryForm;
                PagePh.Controls.Add(clubEnquiryForm);
            }
            else if (page == "standardoffer")
            {
                //Load the regular club enquiry form but mark as 'standard offer'

                objUser.EnquiryFormType = EnquiryFormTypes.StandardOffer;

                OfferEnquiryForm clubEnquiryForm = Page.LoadControl("~/layouts/virginactive/clubfinder/OfferEnquiryForm.ascx") as OfferEnquiryForm;
                PagePh.Controls.Add(clubEnquiryForm);
            }
            else
            {
                //Load the regular club enquiry form

                objUser.EnquiryFormType = EnquiryFormTypes.General;

                ClubEnquiryForm clubEnquiryForm = Page.LoadControl("~/layouts/virginactive/clubfinder/ClubEnquiryForm.ascx") as ClubEnquiryForm;
                PagePh.Controls.Add(clubEnquiryForm);
            }


            Session["sess_User"] = objUser;
        }
    }
}