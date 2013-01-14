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
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using System.Web.Caching;


namespace mm.virginactive.web.layouts.Mobile
{
    public partial class MobileTimetableClubList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Item[] clubs = null;

            //Is it in cache  ----------------
            if (Cache["MobileTimetableClubList"] == null)
            {
                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                ChildList clubLst = clubRoot.Children;
                clubs = clubLst.ToArray();

                double cacheLiveTime = 20.0;
                Double.TryParse(Settings.ClubListsCacheMinutes, out cacheLiveTime);
                Cache.Insert("MobileTimetableClubList", clubs, null, DateTime.Now.AddMinutes(cacheLiveTime), Cache.NoSlidingExpiration);
            }
            else
            {
                clubs = (Item[])Cache["MobileTimetableClubList"];
            }

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
                    string ClubLinkUrl = "";

                    if (club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableItem.TemplateId)) != null)
                    {
                        TimetableItem timetableItem = new TimetableItem(club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableItem.TemplateId)));

                        if (club.IsPlaceholder.Checked)
                        {
                            Item campaign;
                            if (club.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                            {
                                campaign =
                                    club.PlaceholderCampaign.Item.Axes.SelectSingleItem(
                                        String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId));

                                wrappers.VirginActive.PageTemplates.ClubMicrosites.MicrositeTimetableItem micrositeTimetableItem = new wrappers.VirginActive.PageTemplates.ClubMicrosites.MicrositeTimetableItem(campaign.Axes.SelectSingleItem(String.Format(@"descendant::*[@@tid = '{0}']", wrappers.VirginActive.PageTemplates.ClubMicrosites.MicrositeTimetableItem.TemplateId)));

                                ClubLinkUrl = Sitecore.Links.LinkManager.GetItemUrl(micrositeTimetableItem);
                            }
                        }
                        else
                        {
                            SectionContainerItem timetableSectionItem = new SectionContainerItem(timetableItem.InnerItem.Parent);
                            ClubLinkUrl = timetableSectionItem.PageSummary.Url;
                        }
                    }
                    else
                    {
                        if (club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)) != null)
                        {

                            TimetableDownloadItem timetableDownloadItem = new TimetableDownloadItem(club.InnerItem.Axes.SelectSingleItem(String.Format(@"descendant-or-self::*[@@tid = '{0}']", TimetableDownloadItem.TemplateId)));
                            ClubLinkUrl = timetableDownloadItem.PageSummary.Url;
                        }
                    }

                    if (ClubLinkUrl != "")
                    {
                        ltrClubLink.Text = @"<li><a href=""" + ClubLinkUrl + @"""><span class=""arrow"">" + HtmlRemoval.StripTagsCharArray(club.Clubname.Text) + @"</span></a></li>";
                    }
                }
            }
        }
    }
}