using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;

namespace mm.virginactive.web.layouts.virginactive.controls
{
    public partial class YoutubeLinkWidget : System.Web.UI.UserControl
    {

        YoutubeLinkWidgetItem _currentVideoItem;
        private int _itemIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_itemIndex % 2 == 0)
            {
                LitSectionTag.Text = String.Format("<section class=\"half-panel {0} clearfix\">   ", "fl");


            }
            else
            {
                LitSectionTag.Text = String.Format("<section class=\"half-panel {0} clearfix\">   ", "fr");

            }


            YoutubeHelper youtubeHelper = new YoutubeHelper();
            LitYoutubeLink.Text =
                string.Format(
                    "<iframe width='480' height='300' frameborder='0' src='http://www.youtube.com/embed/{0}?wmode=transparent' wmode='Opaque'></iframe>",
                   youtubeHelper.YoutubeIDGet( _currentVideoItem.YoutubeCode.Url));
            LitTextLink.Text = string.Format("<h3><a href=\"{0}\"> {1}</a></h3>", _currentVideoItem.Link.Url,  _currentVideoItem.Heading.Rendered);


            LitSectionEndTag.Text = "</section>";

        }

        public void init(YoutubeLinkWidgetItem videoItem, int itemIndex)
        {
            _currentVideoItem = new YoutubeLinkWidgetItem(videoItem);
            _itemIndex = itemIndex;
        }


    }
}