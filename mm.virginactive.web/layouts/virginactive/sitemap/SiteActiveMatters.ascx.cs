using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Data.Items;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.EviBlog;
using Sitecore.Diagnostics;
using mm.virginactive.web.layouts.virginactive.navigation;
using System.Collections.Specialized;

namespace mm.virginactive.web.layouts.virginactive.sitemap
{
    public partial class SiteActiveMatters : System.Web.UI.UserControl
    {

        protected string ActiveFlag;
        protected PageSummaryItem blogItem;
        protected PageSummaryItem yourHealth;
        protected NavLinkSection HealthArticles;
        protected NavLinkSection HealthTools;
        
        protected PageSummaryItem workoutRoot;
        //protected List<Item> workoutCategories = new List<Item>(); **Workouts section removed
        protected ListView BlogPostList;
        private bool headerIsH2 = true;

        public bool HeaderIsH2
        {
            get { return headerIsH2; }
            set
            {
                headerIsH2 = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                blogItem = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.YourHealthBlog));
                yourHealth = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.YourHealth));

                HealthArticles.Path = ItemPaths.YourHealthArticles;
                HealthTools.Path = ItemPaths.YourHealthTools;

                /*  ************Workouts section removed**************
                workoutRoot = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.YourHealthWorkouts));
                Item categoryList = Sitecore.Context.Database.GetItem(ItemPaths.WorkoutCategories);
                if (categoryList.HasChildren)
                {
                    workoutCategories = categoryList.Children.ToList();
                    WorkoutCategoryList.DataSource = workoutCategories;
                    WorkoutCategoryList.DataBind();
                }
                */

                PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);
                Item[] ancestorsOrSelf = currentItem.InnerItem.Axes.SelectItems("./ancestor-or-self::*");
                //Set active flag if current page is in facilities and classes
                if (ancestorsOrSelf != null)
                {
                    List<Item> contextAncestorsOrSelf = ancestorsOrSelf.ToList();
                    Item facilitiesAndClassesSection = Sitecore.Context.Database.GetItem(ItemPaths.YourHealth);

                    if (SitecoreHelper.ListContainsItem(contextAncestorsOrSelf, facilitiesAndClassesSection))
                    {
                        ActiveFlag = " active";
                    }
                }

                if (blogItem != null)
                {
                    if (blogItem.InnerItem.HasChildren)
                    {
                        List<BlogEntryItem> blogPosts = new List<BlogEntryItem>();
                        blogPosts = blogItem.InnerItem.Axes.SelectItems(String.Format(@"descendant::*[@@tid=""{0}""]", BlogEntryItem.TemplateId.ToString())).ToList().ConvertAll(X => new BlogEntryItem(X));
                        blogPosts.Sort((x, y) => y.Created.CompareTo(x.Created));

                        //We hardcode the list to two latest posts for now
                        if (blogPosts.Count > 5)
                        {
                            blogPosts.RemoveRange(5 - 1, blogPosts.Count - 5);
                        }

                        BlogPostList.DataSource = blogPosts;
                        BlogPostList.DataBind();
                    }
                }

                //Set Header type param if available
                string rawParameters = Attributes["sc_parameters"];
                NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
                if (!String.IsNullOrEmpty(parameters["HeaderIsH2"]))
                {
                    HeaderIsH2 = Convert.ToBoolean(parameters["HeaderIsH2"]);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error printing Your Health Nav: {0}", ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}