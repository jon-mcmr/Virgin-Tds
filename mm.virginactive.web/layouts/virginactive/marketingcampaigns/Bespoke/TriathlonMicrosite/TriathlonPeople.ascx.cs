using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using System.Text;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonPeople : System.Web.UI.UserControl
    {
        protected int count = 0;
        protected MicrositeSectionItem currentItem = new MicrositeSectionItem(Sitecore.Context.Item);
        protected SubheadingLinkWidgetItem clubFinder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentItem.InnerItem.HasChildren)
            {

                //Check if we have profile groups
                DropDownItem groupRoot = Sitecore.Context.Database.GetItem(ItemPaths.TriathlonProfileGroups);

                List<DropDownItem> profileGroups = groupRoot.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", DropDownItem.TemplateId)).ToList().ConvertAll(X => new DropDownItem(X));

                if (profileGroups.Count > 0)
                {
                    PlaceHolder overlayItems = (PlaceHolder)this.Page.FindControl("phOverlayItems");
                    overlayItems.Controls.Add(new LiteralControl("<div id=\"overlay\">"));

                    ProfileGroups.DataSource = profileGroups;
                    ProfileGroups.DataBind();
                    
                    overlayItems.Controls.Add(new LiteralControl("<div id=\"overlay-bg\" class=\"hidden\"></div></div>"));
                }




            }
            //Get widget details
            clubFinder = Sitecore.Context.Database.GetItem(ItemPaths.TriathlonClubFinderWidget);		
            
        }

        protected void ProfileGroups_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                System.Web.UI.WebControls.ListView ProfilePanels = (System.Web.UI.WebControls.ListView)e.Item.FindControl("ProfilePanels");
                
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                var currentGroup = dataItem.DataItem as DropDownItem;

                //Check if we have child profiles
                if (currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FeaturedProfileItem.TemplateId)) != null)
                {
                    List<FeaturedProfileItem> profileList = currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FeaturedProfileItem.TemplateId)).ToList().ConvertAll(Y => new FeaturedProfileItem(Y));
                    List<FeaturedProfileItem> profileGroupCollection = new List<FeaturedProfileItem>();

                    foreach (FeaturedProfileItem profile in profileList)
                    {
                        if (profile.ProfileCategory.Raw == currentGroup.Name)
                        {
                            profileGroupCollection.Add(profile);
                        }
                    }
                    if (profileGroupCollection.Count > 0)
                    {
                        ProfilePanels.DataSource = profileGroupCollection;
                        ProfilePanels.DataBind();
                        count = profileGroupCollection.Count;
                    }
                }
            }
        }

        protected void ProfilePanels_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                var currentProfile = dataItem.DataItem as FeaturedProfileItem;

                //ListView lstView = (ListView)e.Item.Parent;

                //Write out overlay markup for profile (write to bottom of page)

                PlaceHolder overlayItems = (PlaceHolder)this.Page.FindControl("phOverlayItems");
                if (overlayItems!= null)
                {
                    overlayItems.Controls.Add(new LiteralControl(TriathlonPeopleOverlay.RenderToString(currentProfile)));
                }
            }
        }
    }
}