using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubTimeTableDownload : System.Web.UI.UserControl
    {
        protected TimetableDownloadItem currentItem = new TimetableDownloadItem(Sitecore.Context.Item);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentItem.InnerItem.HasChildren)
            {
                List<TimetableDownloadModuleItem> moduleList = currentItem.InnerItem.Children.ToList().ConvertAll(X => new TimetableDownloadModuleItem(X));

                DownloadModuleListing.DataSource = moduleList;
                DownloadModuleListing.DataBind();
            }
        }

        protected void DownloadModuleListing_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var module = dataItem.DataItem as TimetableDownloadModuleItem;

                if (module != null)
                {
                    //Get image crop
                    var image = e.Item.FindControl("image") as System.Web.UI.WebControls.Literal;
                    if (image != null)
                    {
                        image.Text = module.Abstract.Image.RenderCrop("180x120");
                    }
                }
            }
        }

    }
}