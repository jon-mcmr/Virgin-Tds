using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.navigation
{
    public partial class ImagePanel : System.Web.UI.UserControl
    {
        private string cssClass = "";
        protected PageSummaryItem contextItem;
        private string path = "";
        private string rootUrl = "";

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }


        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public PageSummaryItem ContextItem
        {
            get { return contextItem; }
            set
            {
                contextItem = value;
            }
        }

        public string RootUrl
        {
            get { return rootUrl; }
            set
            {
                rootUrl = value;
            }
        }

        //This will change
        public string GetImage
        {
            get
            {
                if (cssClass == "half-panel")
                {
                    return new AbstractItem(contextItem.InnerItem).Image.RenderCrop("460x210");
                }

                if (cssClass == "third-panel")
                {
                    return new AbstractItem(contextItem.InnerItem).Image.RenderCrop("300x180");
                }

                if (cssClass == "full-panel")
                {
                    return new AbstractItem(contextItem.InnerItem).Image.RenderCrop("938x310");
                }
                return "";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Path))
            { //Do not build the second level for blog section.
                if (ContextItem == null)
                {
                    ContextItem = Sitecore.Context.Database.GetItem( Path );
                }
            }

            if (ContextItem != null)
            {
                //Retrieve CSS class
                CssClass = new AbstractItem(contextItem.InnerItem).GetPanelCssClass();

                if (!String.IsNullOrEmpty(RootUrl))
                {
                    contextItem.Url = String.Format( "{0}?section={1}",RootUrl,contextItem.Name );
                }

                //if (ContextItem.InnerItem.HasChildren)
                //{
                //    List<PageSummaryItem> secondLevel = ContextItem.InnerItem.Children.ToList().ConvertAll( x => new PageSummaryItem(x) );
                //    secondLevel.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

                //    if (secondLevel.Count > 0)
                //    {
                //        secondLevel.First().IsFirst = true;
                //        secondLevel.Last().IsLast = true;

                //        if (!String.IsNullOrEmpty(RootUrl)) //Set the jump links
                //        {
                //            foreach (PageSummaryItem child in secondLevel)
                //            {
                //                child.Url = String.Format("{0}#{1}", contextItem.Url, child.Name);
                //            }
                //        }

                //        ChildLinks.DataSource = secondLevel;
                //        ChildLinks.DataBind();
                //    }
                //}
            }
            
        }
    }
}