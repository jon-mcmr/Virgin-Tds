using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.HealthAndBeauty;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using System.Collections.Specialized;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class HealthAndBeautyModule : System.Web.UI.UserControl
    {
        protected HealthAndBeautyModuleItem healthAndBeautyModuleInstance;
        private string cssClass = "";

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }


        public HealthAndBeautyModuleItem HealthAndBeautyModuleInstance
        {
            get { return healthAndBeautyModuleInstance; }
            set
            {
                healthAndBeautyModuleInstance = value;
            }
        }  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HealthAndBeautyModuleInstance != null)
            {
                CssClass = HealthAndBeautyModuleInstance.Abstract.GetPanelCssClass();

                if (HealthAndBeautyModuleInstance.InnerItem.HasChildren)
                {
                    List<HealthAndBeautyOfferItem> offers = HealthAndBeautyModuleInstance.InnerItem.Children.ToList().ConvertAll(X => new HealthAndBeautyOfferItem(X));
                    OfferList.DataSource = offers;
                    OfferList.DataBind();
                }

                //Get image crop
                image.Text = GetImage(HealthAndBeautyModuleInstance);
            }
        }

        public string GetImage(HealthAndBeautyModuleItem detailItem)
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