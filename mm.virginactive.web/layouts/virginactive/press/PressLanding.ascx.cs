using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Press;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Links;
using mm.virginactive.common.Globalization;

namespace mm.virginactive.web.layouts.virginactive.press
{

    public partial class PressLanding : System.Web.UI.UserControl
    {
        private int pageSize;
        protected PressLandingItem Landing;

        protected void Page_Load(object sender, EventArgs e)
        {
            Landing = new PressLandingItem(Sitecore.Context.Item);

            //Pager settings-----
            NextPreviousPagerField fldPrev = ArticlePager.Fields[0] as NextPreviousPagerField;
            NextPreviousPagerField fldNext = ArticlePager.Fields[2] as NextPreviousPagerField;

            fldPrev.PreviousPageText = Translate.Text("Prev");
            fldNext.NextPageText = Translate.Text("Next");

            //Set pager size
            if (!String.IsNullOrEmpty(Landing.Articlesperpage.Integer.ToString()))
            {
                pageSize = Landing.Articlesperpage.Integer;
                ArticlePager.PageSize = pageSize;                
            }

            
            //------------------


            BindData();

        }

        private void BindData()
        {
            try
            {
                List<PressArticleItem> pressArticles = Landing.InnerItem.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", PressArticleItem.TemplateId)).ToList().ConvertAll(X => new PressArticleItem(X));
                pressArticles.Sort((x, y) => y.Date.DateTime.CompareTo(x.Date.DateTime));

                //Hide pager if we can show all items in one page.
                if (pressArticles.Count <= pageSize)
                {
                    ArticlePager.Visible = false;
                }

                ArticleList.DataSource = pressArticles;
                ArticleList.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error displaying press articles: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }

        protected void ArticleList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            this.ArticlePager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindData();
        }
    }
}