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
using Sitecore.Collections;

namespace mm.virginactive.web.layouts.virginactive.navigation
{
    public partial class YourHealthNav : System.Web.UI.UserControl
    {
        protected string ActiveFlag;
        protected PageSummaryItem blogItem;
        protected PageSummaryItem yourHealth;
        //protected PageSummaryItem workoutRoot;
        //protected List<Item> workoutCategories = new List<Item>();
        protected void Page_Load(object sender, EventArgs e)
        {                     
            try
            {
                Item blog = Sitecore.Context.Database.GetItem(ItemPaths.YourHealthBlog);
                if (blog != null)
                {
                    blogItem = new PageSummaryItem(blog);
                }
                Item health = Sitecore.Context.Database.GetItem(ItemPaths.YourHealth);
                if (health != null)
                {
                    yourHealth = new PageSummaryItem(health);
                }
                
                

                HealthArticles.Path = ItemPaths.YourHealthArticles;
                HealthTools.Path = ItemPaths.YourHealthTools;

                /*
                workoutRoot = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.YourHealthWorkouts));
                Item categoryList = Sitecore.Context.Database.GetItem(ItemPaths.WorkoutCategories);
                if (categoryList != null)
                {
                    if (categoryList.HasChildren)
                    {
                        workoutCategories = categoryList.Children.ToList();
                        WorkoutCategoryList.DataSource = workoutCategories;
                        WorkoutCategoryList.DataBind();
                    }
                } */

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
                        if (blogPosts.Count > 2)
                        {
                            //blogPosts.RemoveRange(2 - 1, blogPosts.Count - 2);
                            blogPosts.RemoveRange(2, blogPosts.Count - 2);
                        }
                        
                        BlogPostList.DataSource = blogPosts;
                        BlogPostList.DataBind();
                    }
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