using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Helpers;
using mm.virginactive.controls.Model;
using System.Web.UI.HtmlControls;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubAnnouncement : System.Web.UI.UserControl
    {
        protected ClubAnnouncementItem announcement = new ClubAnnouncementItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                User objUser = new User();
                if (Session["sess_User"] != null)
                {
                    objUser = (User)Session["sess_User"];
                }

                objUser.ShowGallery = false;
                HtmlGenericControl BodyTag = (HtmlGenericControl)this.Page.FindControl("BodyTag");
                string classNames = BodyTag.Attributes["class"] != null ? BodyTag.Attributes["class"] : "";
                classNames = classNames.Replace("gallery_open", "").Trim();
                BodyTag.Attributes.Add("class", classNames);

                Session["sess_User"] = objUser;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Issues pulling Announcement information for club announcment page: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}