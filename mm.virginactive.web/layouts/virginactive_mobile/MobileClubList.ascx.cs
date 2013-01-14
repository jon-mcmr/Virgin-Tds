using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.MobileTemplates;
using mm.virginactive.common.Globalization;
using System.Web.Caching;

namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileClubList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Item[] clubs = null;

            //Is it in cache  ----------------
            if (Cache["MobileClubList"] == null)
            {
                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                ChildList clubLst = clubRoot.Children;
                clubs = clubLst.ToArray();

                double cacheLiveTime = 20.0;
                Double.TryParse(Settings.ClubListsCacheMinutes, out cacheLiveTime);
                Cache.Insert("MobileClubList", clubs, null, DateTime.Now.AddMinutes(cacheLiveTime), Cache.NoSlidingExpiration);
            }
            else
            {
                clubs = (Item[])Cache["MobileClubList"];
            }
            //Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
            //ChildList clubLst = clubRoot.Children;
            //Item[] clubs = clubLst.ToArray();
            if (clubs != null)
            {
                List<ClubItem> clubItemList = clubs.ToList().ConvertAll(X => new ClubItem(X));
                clubItemList.RemoveAll(x => x.IsHiddenFromMenu());
                //clubItemList.RemoveAll(x => x.IsPlaceholder.Checked); 

                clubList.DataSource = clubItemList;
                clubList.DataBind();
                
            }            
        }

        protected void clubList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var club = dataItem.DataItem as ClubItem;

                if (club != null)
                {

                    //Get club Urls
                    var ltrClubLink = e.Item.FindControl("ltrClubLink") as System.Web.UI.WebControls.Literal;
                    
                    string ClubLinkUrl = new PageSummaryItem(club.InnerItem).Url;

                    ltrClubLink.Text = @"<li><a href=""" + ClubLinkUrl + @"""><span class=""arrow"">" + HtmlRemoval.StripTagsCharArray(club.Clubname.Text) + @"</span></a></li>";
                }
            }
        }
    }
}