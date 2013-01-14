using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Press;
using Sitecore.Diagnostics;

namespace mm.virginactive.web.layouts.virginactive.press
{
    public partial class LatestPressReleases : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PressLandingItem landing = new PressLandingItem(Sitecore.Context.Item.Axes.SelectItems(String.Format("ancestor-or-self::*[@@tid = '{0}' ]", PressLandingItem.TemplateId))[0]);
                List<PressArticleItem> pressArticles = landing.InnerItem.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", PressArticleItem.TemplateId)).ToList().ConvertAll(X => new PressArticleItem(X));
                pressArticles.Sort((x, y) => y.Date.DateTime.CompareTo(x.Date.DateTime));

                //We have a max number latest article items, remove anything over that number
                if (!String.IsNullOrEmpty(landing.Maxlatestreleases.ToString()))
                {
                    if (pressArticles.Count > landing.Maxlatestreleases.Integer)
                    {
                        pressArticles.RemoveRange(landing.Maxlatestreleases.Integer - 1, pressArticles.Count - landing.Maxlatestreleases.Integer);
                    }
                }

                LatestPressArticleList.DataSource = pressArticles;
                LatestPressArticleList.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error retriving popular article list: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}