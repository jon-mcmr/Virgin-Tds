using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using System.Text;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using Sitecore.Diagnostics;


namespace mm.virginactive.web.layouts.virginactive.navigation
{
    public partial class MembershipsNav : System.Web.UI.UserControl
    {
        protected string ActiveFlag;
        protected SubheadingLinkWidgetItem widget = new SubheadingLinkWidgetItem(new HeaderItem(Sitecore.Context.Database.GetItem(ItemPaths.Header)).Facilitymenuwidget.Item);
        protected PageSummaryItem member = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.Memberships));
        protected HeaderItem Header;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);
                Header = new HeaderItem(Sitecore.Context.Database.GetItem(ItemPaths.Header));
                Item[] ancestors = currentItem.InnerItem.Axes.SelectItems("./ancestor-or-self::*");
                //Set active flag if current page is in facilities and classes
                if (ancestors != null)
                {
                    List<Item> contextAncestorsOrSelf = ancestors.ToList();
                    Item memberSection = member.InnerItem;

                    if (SitecoreHelper.ListContainsItem(contextAncestorsOrSelf, memberSection))
                    {
                        ActiveFlag = " active";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error printing your health Nav: {0}" + ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}