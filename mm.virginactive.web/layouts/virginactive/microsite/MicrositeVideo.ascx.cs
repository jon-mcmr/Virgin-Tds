using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeVideo : System.Web.UI.UserControl
    {
        public PromoAreaItem promoItem;
        protected void Page_Load(object sender, EventArgs e)
        {
            var currentItem = Sitecore.Context.Item;

            promoItem = new PromoAreaItem(currentItem);

            VideoHeading.Text = promoItem.Videoheading.Rendered;
            VideoIntro.Text = promoItem.Videointro.Rendered;
        }
    }
}