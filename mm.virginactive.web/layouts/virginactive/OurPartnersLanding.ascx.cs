using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates.OurPartners;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class OurPartnersLanding : System.Web.UI.UserControl
    {
        protected OurPartnersLandingItem currentItem = new OurPartnersLandingItem(Sitecore.Context.Item);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentItem.InnerItem.HasChildren)
            {
                List<OurPartnersDetailItem> detailList = currentItem.InnerItem.Children.ToList().ConvertAll(X => new OurPartnersDetailItem(X));

                OurPartnersListing.DataSource = detailList;
                OurPartnersListing.DataBind();
            }
        }
        
        protected void DetailList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var detailPage = dataItem.DataItem as OurPartnersDetailItem;

                if (detailPage != null)
                {
                    //Get image crop
                    var image = e.Item.FindControl("image") as System.Web.UI.WebControls.Literal;
                    if (image != null)
                    {
                        image.Text = GetImage(detailPage);
                    }
                }
            }
        } 

        public string GetImage(OurPartnersDetailItem detailItem)
        {
            switch (detailItem.Abstract.GetPanelCssClass())
            {
                case "full-panel":
                    return detailItem.Abstract.Image.RenderCrop("938x310");
                case "half-panel":
                    return detailItem.Abstract.Image.RenderCrop("460x210");
                case "third-panel":
                    return detailItem.Abstract.Image.RenderCrop("300x180");
                case "quarter-panel":
                    return detailItem.Abstract.Image.RenderCrop("220x120");
                default:
                    return "";
            }
        }
    }
}