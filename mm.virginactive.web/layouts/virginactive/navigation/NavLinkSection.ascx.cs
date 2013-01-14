using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Diagnostics;

namespace mm.virginactive.web.layouts.virginactive.navigation
{
    public partial class NavLinkSection : System.Web.UI.UserControl
    {
        private string cssClass = "";
        protected PageSummaryItem contextItem;
        private string path = "";
        private bool headerIsH2 = false;
        private bool headerNavigable = true;

        public bool HeaderIsH2
        {
            get { return headerIsH2; }
            set
            {
                headerIsH2 = value;
            }
        }

        public bool HeaderNavigable
        {
            get { return headerNavigable; }
            set
            {
                headerNavigable = value;
            }
        }

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }


        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public PageSummaryItem ContextItem
        {
            get { return contextItem; }
            set
            {
                contextItem = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!String.IsNullOrEmpty(Path))
                { //Do not build the second level for blog section.
                    ContextItem = Sitecore.Context.Database.GetItem(Path);

                    if (ContextItem != null)
                    {
                        if (ContextItem.InnerItem.HasChildren)
                        {
                            List<PageSummaryItem> secondLevel = ContextItem.InnerItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                            secondLevel.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

                            if (secondLevel != null && secondLevel.Count > 0)
                            {
                                secondLevel.First().IsFirst = true;
                                secondLevel.Last().IsLast = true;

                                NavItems.DataSource = secondLevel;
                                NavItems.DataBind();
                            }
                        }
                    }
                }
            }
            catch( Exception ex)
            {
                Log.Error(String.Format("Error printing Section Nav links: {0}" + ex.Message),this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}