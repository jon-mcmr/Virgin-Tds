using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using System.Text;

namespace mm.virginactive.web.layouts.virginactive.indoortriathlon
{
    public partial class IndoorLinks : System.Web.UI.UserControl
    {

        protected IndoorLinksItem currentItem = new IndoorLinksItem(Sitecore.Context.Item);


        protected void Page_Load(object sender, EventArgs e)
        {
            //Add dynamic content
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/indoortriathlon/indoor-tri.js""></script>"));

            if (currentItem.InnerItem.HasChildren)
            {

                //Check if we have links
                if (currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FileImageLinkWidgetItem.TemplateId)) != null)
                {
                    List<FileImageLinkWidgetItem> linkList = currentItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", FileImageLinkWidgetItem.TemplateId)).ToList().ConvertAll(Y => new FileImageLinkWidgetItem(Y));

                    if (linkList.Count > 0)
                    {
                        LinkPanels.DataSource = linkList;
                        LinkPanels.DataBind();
                    }
                }
            }

        }
    }
}