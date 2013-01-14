using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates.AboutUs;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class AboutUsDetail : System.Web.UI.UserControl
    {
        protected AboutUsDetailItem currentItem = new AboutUsDetailItem(Sitecore.Context.Item);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentItem.InnerItem.HasChildren)
            {
                List<AboutUsModuleItem> moduleList = currentItem.InnerItem.Children.ToList().ConvertAll(X => new AboutUsModuleItem(X));

                AboutUsModuleListing.DataSource = moduleList;
                AboutUsModuleListing.DataBind();
            }
        }

        protected void ModuleList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var module = dataItem.DataItem as AboutUsModuleItem;

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

        public string GetImage(AboutUsModuleItem detailItem)
        {
            switch (detailItem.Abstract.GetPanelCssClass())
            {
                case "full-panel":
                    return detailItem.Abstract.Image.RenderCrop("938x310");
                case "half-panel":
                    return detailItem.Abstract.Image.RenderCrop("460x210");
                case "third-panel":
                    return detailItem.Abstract.Image.RenderCrop("280x180");
                case "quarter-panel":
                    return detailItem.Abstract.Image.RenderCrop("220x120");
                default:
                    return "";
            }
        }
    }
}