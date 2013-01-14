using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using Sitecore.Web;

namespace mm.virginactive.web.layouts.virginactive.presentationstructures
{
    public partial class CentralLayout : System.Web.UI.UserControl
    {
        protected PageSummaryItem contextTitle= new PageSummaryItem(Sitecore.Context.Item);
        //protected PageSummaryItem contextItem = new PageSummaryItem(Sitecore.Context.Item);
        protected string title = "";

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Item[] ancestorsWithHeading = contextTitle.InnerItem.Axes.SelectItems("ancestor-or-self::*[@IsHeading = '1' ]");

                if (ancestorsWithHeading != null)
                {
                    contextTitle = new PageSummaryItem(ancestorsWithHeading[0]);
                }

                if (!string.IsNullOrEmpty(WebUtil.GetQueryString("title")))
                {
                    layoutHeader.Text = WebUtil.GetQueryString("title");
                }
                else
                {
                    layoutHeader.Text = contextTitle.NavigationTitle.Rendered;
                }
            }


            catch (Exception ex)
            {
                Log.Error(String.Format("Error retriving a valid page title: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }

            if (Sitecore.Context.PageMode.IsPageEditorEditing) {
                PageEditorControls.Visible = true;
            }
        }
    }
}