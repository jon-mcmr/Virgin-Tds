using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeWhatsNew : System.Web.UI.UserControl
    {
        protected SharedWhatsNewItem SharedRoot = new SharedWhatsNewItem(Sitecore.Context.Database.GetItem(ItemPaths.SharedWhatsNew));
        protected WhatsNewItem ContextItem = new WhatsNewItem(Sitecore.Context.Item);
        private int pageSize;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Show global alert
            if (!String.IsNullOrEmpty(SharedRoot.GlobalAlert.Raw))
            {
                GlobalAlert.Visible = true;
                GlobalAlert.Controls.Add(new LiteralControl(String.Format("<p>{0}</p>", SharedRoot.GlobalAlert.Raw)));
            }
            //Show local alert
            if (!String.IsNullOrEmpty(ContextItem.Alerttext.Raw))
            {
                Alert.Visible = true;
                Alert.Controls.Add(new LiteralControl(String.Format("<p>{0}</p>", ContextItem.Alerttext.Raw)));
            }

            //Pager settings-----
            NextPreviousPagerField fldPrev = ArticlePager.Fields[0] as NextPreviousPagerField;
            NextPreviousPagerField fldNext = ArticlePager.Fields[2] as NextPreviousPagerField;

            fldPrev.PreviousPageText = Translate.Text("Prev");
            fldNext.NextPageText = Translate.Text("Next");

            //Set pager size
            if (!String.IsNullOrEmpty(ContextItem.Newsitemsperpage.Integer.ToString()))
            {
                pageSize = ContextItem.Newsitemsperpage.Integer;
                ArticlePager.PageSize = pageSize;
            }
            //------------------

            BindData();
        }


        private void BindData()
        {
            List<NewsItem> newsItems = new List<NewsItem>();

            Item[] items = ContextItem.InnerItem.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", NewsItem.TemplateId));

            if (SharedRoot != null)
            {
                Item[] sharedNews = SharedRoot.InnerItem.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}' and @#Publish to all clubs#='1']", SharedNewsItem.TemplateId));

                if (sharedNews != null)
                {
                    List<NewsItem> convertedItems = sharedNews.ToList().ConvertAll(X => new NewsItem(X));

                    WhatsNewItem whatsNewItem = new WhatsNewItem(Sitecore.Context.Item);
                    if (!String.IsNullOrEmpty(whatsNewItem.Sharednewscutoff.Raw))
                    {
                        convertedItems =
                            convertedItems.Where(x => x.Date.DateTime >= whatsNewItem.Sharednewscutoff.DateTime).ToList();
                    }

                    newsItems.AddRange(convertedItems);
                }
            }

            if (items != null)
            {
                newsItems.AddRange(items.ToList().ConvertAll(X => new NewsItem(X)));
            }

            if (newsItems.Count > 0)
            {
                //Hide pager if we can show all items in one page.
                if (newsItems.Count <= pageSize)
                {
                    ArticlePager.Visible = false;
                }

                //Latest news items first
                newsItems.Sort(delegate(NewsItem itmY, NewsItem itmX)
                {
                    return itmX.Date.DateTime.CompareTo(itmY.Date.DateTime);
                });

                NewList.DataSource = newsItems;
                NewList.DataBind();
            }
            else
            {
                ArticlePager.Visible = false;
                NewList.DataSource = newsItems;
                NewList.DataBind();
            }
        }

        protected void NewList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            this.ArticlePager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindData();
        }
    }
}