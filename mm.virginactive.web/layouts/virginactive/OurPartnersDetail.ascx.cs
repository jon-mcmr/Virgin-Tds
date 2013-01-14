using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.OurPartners;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class OurPartnersDetail : System.Web.UI.UserControl
    {
        protected OurPartnersDetailItem currentItem = new OurPartnersDetailItem(Sitecore.Context.Item);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentItem.InnerItem.HasChildren)
            {
                List<OurPartnersModuleItem> moduleList = currentItem.InnerItem.Children.ToList().ConvertAll(X => new OurPartnersModuleItem(X));

                OurPartnersModuleListing.DataSource = moduleList;
                OurPartnersModuleListing.DataBind();
            }
        }

        protected void ModuleList_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var module = dataItem.DataItem as OurPartnersModuleItem;

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

        public string GetImage(OurPartnersModuleItem detailItem)
        {
            switch (detailItem.Abstract.GetPanelCssClass())
            {
                case "full-panel":
                    return detailItem.Abstract.Image.RenderCrop("440x300");
                case "half-panel":
                    return detailItem.Abstract.Image.RenderCrop("460x210");
                case "third-panel":
                    return detailItem.Abstract.Image.RenderCrop("280x160");
                case "quarter-panel":
                    return detailItem.Abstract.Image.RenderCrop("220x120");
                default:
                    return "";
            }
        }
    }
}