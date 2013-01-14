using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;

namespace mm.virginactive.web.layouts.virginactive.controls
{
	public partial class ImageLinkWidget : System.Web.UI.UserControl
	{
		public ImageLinkWidgetItem CurrentImageLinkWidgetItem { get; set; }

		ImageLinkWidgetItem _currentItem;
		private int _itemIndex;
		

		protected void Page_Load(object sender, EventArgs e)
		{


			if (_itemIndex  % 2 == 0)
			{
				LitSectionTag.Text = String.Format("<section class=\"half-panel {0} clearfix\">   ", "fl");
				
				
			}
			else
			{
				LitSectionTag.Text = String.Format("<section class=\"half-panel {0} clearfix\">   ", "fr");

			}

			LitCssPanel.Text = String.Format("<span class=\"home-icon {0}\"><span></span>{1}</span>",
			CurrentImageLinkWidgetItem.GetPanelCssClass(),CurrentImageLinkWidgetItem.Widget.Buttontext.Rendered);
			LitLinkPanel.Text = String.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", 
				CurrentImageLinkWidgetItem.Widget.Buttonlink.Url,
				CurrentImageLinkWidgetItem.Widget.Heading.Rendered, 
				CurrentImageLinkWidgetItem.Image.RenderCrop("480x210"));
			//LitArrowLink.Text =
			//    String.Format("<a class=\"arrow\" href=\"{0}\" title=\"{1}\"><span></span>{2}</a>",
			//                  CurrentImageLinkWidgetItem.Widget.Buttonlink.Url,
			//                  CurrentImageLinkWidgetItem.Widget.Heading.Rendered,
			//                  Translate.Text("Read more")
			//        );
			LitButtonLink.Text =
				String.Format("<a href=\"{0}\">{1}</a>",
				CurrentImageLinkWidgetItem.Widget.Buttonlink.Url,
				CurrentImageLinkWidgetItem.Widget.Heading.Rendered);
			LitBodyText.Text = CurrentImageLinkWidgetItem.Widget.Bodytext.Text;
			LitSectionEndTag.Text = "</section>";


		}


		public void init(Item item, int itemIndex)
		{

			_currentItem = new ImageLinkWidgetItem(item);
			CurrentImageLinkWidgetItem = _currentItem;
			_itemIndex = itemIndex;

		}
	}
}