using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using System.Text;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive.marketingcampaigns
{
    public partial class TriathlonSectionLayout : System.Web.UI.UserControl
    {
        protected PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] sectionMicrositeIds = ItemPaths.MicrositeSectionTemplates.Split('|');

            if (currentItem.InnerItem.HasChildren)
            {
                //Check if we have child sections (by seeing if the item inherits the abstract template)

                if (sectionMicrositeIds.Count() > 0)
                {
                    List<AbstractItem> childSectionList = new List<AbstractItem>();

                    StringBuilder query = new StringBuilder();
                    query.Append("child::*[");

                    foreach (string sectionTemplateId in sectionMicrositeIds)
                    {
                        query.Append(String.Format("@@tid='{0}' or ", sectionTemplateId));
                    }
                    query.Remove(query.Length - 4, 4);
                    query.Append("]");

                    //Check if we have faqs (by seeing if the item inherits the abstract template)
                    if (currentItem.InnerItem.Axes.SelectItems(query.ToString()) != null)
                    {
                        foreach (AbstractItem newItem in currentItem.InnerItem.Axes.SelectItems(query.ToString()).ToList().ConvertAll(Y => new AbstractItem(Y)))
                        {
                            childSectionList.Add(newItem);
                        }
                    }

                    if (childSectionList.Count > 0)
                    {
                        ChildSectionListing.DataSource = childSectionList;
                        ChildSectionListing.DataBind();
                    }
                }
            }
            

            //If there is a parent section -add link to parent level
            //Get parent page

            string linkUrl = "", linkText = "";

            if (currentItem.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor::*[@@tid=""{0}""]", MicrositeSectionItem.TemplateId)) != null)
            {

                PageSummaryItem parentSection = new PageSummaryItem(currentItem.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor::*[@@tid=""{0}""]", MicrositeSectionItem.TemplateId)));

                linkUrl = parentSection.Url;
                linkText = string.Format(Translate.Text("BACK TO {0} HOMEPAGE"), parentSection.NavigationTitle.Rendered.ToUpper());
                ltrParentSectionLink.Text = @"<a href=""" + linkUrl + @""" id=""back"">" + linkText + @"</a>";

                //Get other sections (that sit under the same section)
                if (sectionMicrositeIds.Count() > 0)
                {
                    List<AbstractItem> otherSectionList = new List<AbstractItem>();

                    StringBuilder query = new StringBuilder();
                    query.Append("child::*[");

                    foreach (string sectionTemplateId in sectionMicrositeIds)
                    {
                        query.Append(String.Format("@@tid='{0}' or ", sectionTemplateId));
                    }
                    query.Remove(query.Length - 4, 4);
                    query.Append("]");

                    //Check if we have faqs (by seeing if the item inherits the abstract template)
                    if (parentSection.InnerItem.Axes.SelectItems(query.ToString()) != null)
                    {
                        foreach (AbstractItem newItem in parentSection.InnerItem.Axes.SelectItems(query.ToString()).ToList().ConvertAll(Y => new AbstractItem(Y)))
                        {
                            if (newItem.ID.ToShortID() != currentItem.ID.ToShortID())
                            {
                                otherSectionList.Add(newItem);
                            }
                        }
                    }

                    if (otherSectionList.Count > 0)
                    {
                        OtherSectionListing.DataSource = otherSectionList;
                        OtherSectionListing.DataBind();
                    }
                }
            }
        }

        protected void SectionList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var module = dataItem.DataItem as AbstractItem;

                if (module != null)
                {
                    //Get image crop
                    var image = e.Item.FindControl("image") as System.Web.UI.WebControls.Literal;
                    if (image != null)
                    {
                        image.Text = GetImage(module);
                    }
                }
            }
        }

        public string GetImage(AbstractItem detailItem)
        {
            switch (detailItem.GetPanelCssClass())
            {
                case "full-panel":
                    return detailItem.Image.RenderCrop("938x310");
                case "half-panel":
                    return detailItem.Image.RenderCrop("480x210");
                case "third-panel":
                    return detailItem.Image.RenderCrop("300x180");
                case "quarter-panel":
                    return detailItem.Image.RenderCrop("220x120");
                default:
                    return "";
            }
        }

    }
}