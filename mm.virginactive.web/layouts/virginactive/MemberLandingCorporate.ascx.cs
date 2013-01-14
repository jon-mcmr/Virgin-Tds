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
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using Sitecore.Web;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class MemberLandingCorporate : System.Web.UI.UserControl
    {
        protected MembershipCorporateLandingItem contextGroupOrSharedLevel = null;
        protected bool isClubSection = false;
        protected string corporateEnquiryUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Item context = Sitecore.Context.Item;

            //Check if we are Group level or Club specific level
            if (context.TemplateID.ToString() == MembershipCorporateLandingItem.TemplateId)
            {
                //Group level
                contextGroupOrSharedLevel = new MembershipCorporateLandingItem(Sitecore.Context.Item);
            }
            else
            {
                //Club area -grab landing from shared content area
                isClubSection = true;
                contextGroupOrSharedLevel = Sitecore.Context.Database.GetItem(ItemPaths.SharedMembershipCorporate);
            }

            //Get link button items
            if (contextGroupOrSharedLevel.InnerItem.HasChildren)
            {
                List<LinkWidgetItem> linkWidgets = contextGroupOrSharedLevel.InnerItem.Children.ToList().ConvertAll(X => new LinkWidgetItem(X));

                linkWidgets.First().IsFirst = true;
                linkWidgets.Last().IsLast = true;

                WidgetList.DataSource = linkWidgets;
                WidgetList.DataBind();
            }

        }

        protected void WidgetList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    ListViewDataItem dataItem = (ListViewDataItem)e.Item;

            //    var widgetItem = dataItem.DataItem as LinkWidgetItem;

            //    if (widgetItem != null)
            //    {
            //        //Get link url
            //        var linkUrl = e.Item.FindControl("linkUrl") as System.Web.UI.WebControls.Literal;
            //        if (linkUrl != null)
            //        {
            //            if (isClubSection)
            //            {
            //                //get clubID
            //                ClubItem club = new ClubItem(Sitecore.Context.Item.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}"" or @@tid=""{1}""]", ClassicClubItem.TemplateId, LifeCentreItem.TemplateId)));

            //                //append club id to url
            //                if (club != null)
            //                {
            //                    linkUrl.Text = widgetItem.Widget.Buttonlink.Url.IndexOf("?") == -1 ? widgetItem.Widget.Buttonlink.Url + "&c=" + club.ID.ToString() : widgetItem.Widget.Buttonlink.Url + "?c=" + club.ID.ToString();
            //                }
            //            }
            //            else
            //            {
            //                linkUrl.Text = widgetItem.Widget.Buttonlink.Url;
            //            }
            //        }
            //    }
            //}
        }
    }
}