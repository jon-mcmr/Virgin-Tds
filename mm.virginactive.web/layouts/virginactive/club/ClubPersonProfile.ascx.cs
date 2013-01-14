using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using Sitecore.Diagnostics;
using mm.virginactive.controls.Model;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubPersonProfile : System.Web.UI.UserControl
    {
        protected PersonItem Person = new PersonItem(Sitecore.Context.Item);
        protected Boolean showSocial = false;

        public Boolean ShowSocial
        {
            get { return showSocial; }
            set
            {
                showSocial = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<TestimonialItem> test = new List<TestimonialItem>();
            try
            {
                if (Person.InnerItem.HasChildren)
                {
                    test = Person.InnerItem.Children.ToList().ConvertAll(X => new TestimonialItem(X));
                    if (test.Count > 0)
                    {
                        TestimonalSection.Visible = true;
                        TestList.DataSource = test;
                        TestList.DataBind();
                    }
                }

                if (Session["sess_User"] != null)
                {
                    User objUser = (User)Session["sess_User"];

                    if (objUser.Preferences.SocialCookies)
                    {
                        //Have permission to load in Social
                        showSocial = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error generating profile testimonials: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}