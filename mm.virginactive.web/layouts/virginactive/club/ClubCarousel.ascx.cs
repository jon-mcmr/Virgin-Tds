using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;
using CustomItemGenerator.Fields.SimpleTypes;
using Sitecore.Data.Items;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubCarousel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClubItem contextClub = SitecoreHelper.GetCurrentClub(Sitecore.Context.Item);

            List<MediaItem> imageList;
            if (!String.IsNullOrEmpty(contextClub.Imagegallery.Raw))
            {
                ClubImagePanel.Visible = true;
                imageList = contextClub.Imagegallery.ListItems.ConvertAll(X => new MediaItem(X));
                ImageList.DataSource = imageList;
                ThumbList.Visible = false;
                //ThumbList.DataSource = imageList;

                //ThumbList.DataBind();
                ImageList.DataBind();
            }           
        }
    }
}