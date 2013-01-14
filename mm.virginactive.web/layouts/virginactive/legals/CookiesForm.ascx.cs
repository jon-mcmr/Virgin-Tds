using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Globalization;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Legals;

namespace mm.virginactive.web.layouts.virginactive.legals
{
    public partial class CookiesForm : System.Web.UI.UserControl
    {
        protected CookiesFormItem cookieForm = new CookiesFormItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {                   
            if (!Page.IsPostBack)
            {
                User objUser = new User();
                //Set user session variable
                if (Session["sess_User"] != null)
                {
                    objUser = (User)Session["sess_User"];
                }

                if (objUser.Preferences != null)
                {
                    //Set values based on stored cookie preferences

                    radMarketingOn.Checked = objUser.Preferences.MarketingCookies;
                    radMarketingOff.Checked = !objUser.Preferences.MarketingCookies;

                    radMetricsOn.Checked = objUser.Preferences.MetricsCookies;
                    radMetricsOff.Checked = !objUser.Preferences.MetricsCookies;

                    radPersonalisedOn.Checked = objUser.Preferences.PersonalisedCookies;
                    radPersonalisedOff.Checked = !objUser.Preferences.PersonalisedCookies;

                    radSocialOn.Checked = objUser.Preferences.SocialCookies;
                    radSocialOff.Checked = !objUser.Preferences.SocialCookies;
                }

                btnSubmit.Text = cookieForm.Buttontext.Rendered;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {           
            User objUser = new User();
            //Set user session variable
            if (Session["sess_User"] != null)
            {
                objUser = (User)Session["sess_User"];
            }

            //Set Preferences in User Session
            Preferences preferences = new Preferences();

            preferences.MarketingCookies = radMarketingOn.Checked;
            preferences.MetricsCookies = radMetricsOn.Checked;
            preferences.PersonalisedCookies = radPersonalisedOn.Checked;
            preferences.SocialCookies = radSocialOn.Checked;

            objUser.Preferences = preferences;
            Session["sess_User"] = objUser;

            //Set Opt Out Cookie
            CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MarketingCookies, radMarketingOn.Checked ? "Y" : "N");
            CookieHelper.AddUpdateOptInCookie(CookieKeyNames.MetricsCookies, radMetricsOn.Checked ? "Y" : "N");
            CookieHelper.AddUpdateOptInCookie(CookieKeyNames.PersonalisedCookies, radPersonalisedOn.Checked ? "Y" : "N");
            CookieHelper.AddUpdateOptInCookie(CookieKeyNames.SocialCookies, radSocialOn.Checked ? "Y" : "N");

            if (radPersonalisedOn.Checked == false)
            {
                //Delete personalisation cookie
                CookieHelper.DeleteCookie();
            }

            string classNames = pnlConfirmation.Attributes["class"];
            pnlConfirmation.Attributes.Add("class", classNames.Replace(" hidden", ""));

            classNames = pnlFormPrompt.Attributes["class"];
            pnlFormPrompt.Attributes.Add("class", classNames + " hidden");

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		            $(function(){
	                    virginactive.reinit();
                    });
                </script>"));
            pnlForm.Update();
        }

        protected void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (pnlFormPrompt.Visible == false)
            {
                pnlFormPrompt.Visible = true;
                pnlConfirmation.Visible = false;
                pnlForm.Update();
            }
        }
    }
}