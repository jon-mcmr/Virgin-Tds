using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.HealthAndBeauty;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Workouts;
using mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class SecondaryNavigation : System.Web.UI.UserControl
    {
        protected PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);
        protected PageSummaryItem currentfirstLevelItem;
        private string section = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            section = Sitecore.Web.WebUtil.GetQueryString("section");
            if (currentItem.InnerItem.Axes.SelectItems(String.Format("./ancestor-or-self::*[@@tid='{0}']/child::*", SectionContainerItem.TemplateId)) != null)
            {
                List<Item> firstLevelList = currentItem.InnerItem.Axes.SelectItems(String.Format("./ancestor-or-self::*[@@tid='{0}']/child::*", SectionContainerItem.TemplateId)).ToList();
                List<Item> contextAncestorsOrSelf = currentItem.InnerItem.Axes.SelectItems("./ancestor-or-self::*").ToList();

                List<PageSummaryItem> firstLevel = firstLevelList.ConvertAll(x => new PageSummaryItem(x));
                firstLevel.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items
                firstLevel.RemoveAll(x => x.InnerItem.TemplateID.ToString() == WidgetContainerItem.TemplateId); //Remove all widget containers

                if (firstLevel.Count > 0)
                {
                    firstLevel.Last().IsLast = true;

                    //need to figure out the 'active' first level item (the one to highlight)
                    //This can then also be used to build to second level
                    foreach (PageSummaryItem item in firstLevel)
                    {
                        if (SitecoreHelper.ListContainsItem(contextAncestorsOrSelf, item.InnerItem))
                        {
                            item.IsCurrent = true;
                            currentfirstLevelItem = item;
                            break;
                        }
                    }

                    //Bind the first level items
                    TopElements.DataSource = firstLevel;
                    TopElements.DataBind();
                }
            }

            if (currentfirstLevelItem != null && (currentfirstLevelItem.InnerItem.TemplateID.ToString() != ItemPaths.BlogTemplateId))
            {   //Do not build the second level for blog section.
               
                if (currentfirstLevelItem.InnerItem.HasChildren)
                {
                    bool notBound = true;                    
                    
                    //Get Facility & class items, different behaviour for these (jump links)
                    if (currentfirstLevelItem.InnerItem.TemplateID.ToString() == FacilitiesLandingItem.TemplateId)
                    {
                        Item sharedFacility = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities);
                        DataBindSecondaryNav(sharedFacility, new PageSummaryItem(currentfirstLevelItem.InnerItem.Children[0]).Url);
                        notBound = false;
                    }

                    if (currentfirstLevelItem.InnerItem.TemplateID.ToString() == ClassesLandingItem.TemplateId)
                    {
                        Item shareClass = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses);
                        DataBindSecondaryNav(shareClass, new PageSummaryItem(currentfirstLevelItem.InnerItem.Children[0]).Url);
                        notBound = false;
                    }

                    if (currentfirstLevelItem.InnerItem.TemplateID.ToString() == HealthAndBeautyLandingItem.TemplateId)
                    {
                        //Secondary Navigation is hidden (child elements are displayed in screen and non navigatable)
                        SecondLevelNavigation.Visible = false;
                        notBound = false;
                    }

                    //Hide for workouts section
                    if (currentfirstLevelItem.InnerItem.TemplateID.ToString() == WorkoutLandingItem.TemplateId)
                    {
                        //Secondary Navigation is hidden (child elements are displayed in screen and non navigatable)
                        SecondLevelNavigation.Visible = false;
                        notBound = false;
                    }

                    //Hide for Indoor modules
                    if (currentfirstLevelItem.InnerItem.TemplateID.ToString() == IndoorGeneralItem.TemplateId ||
                        currentfirstLevelItem.InnerItem.TemplateID.ToString() == IndoorClassesItem.TemplateId ||
                        currentfirstLevelItem.InnerItem.TemplateID.ToString() == IndoorHistoryItem.TemplateId ||
                        currentfirstLevelItem.InnerItem.TemplateID.ToString() == IndoorEventsItem.TemplateId
                        )
                    {
                        //Secondary Navigation is hidden (child elements are displayed in screen and non navigatable)
                        SecondLevelNavigation.Visible = false;
                        notBound = false;
                    }

                    if (notBound)
                    {
                        DataBindSecondaryNav(currentfirstLevelItem.InnerItem, "");
                    }
                   
                }
            }
        }


        private void DataBindSecondaryNav(Item item, string rootUrl)
        {
            SubSubNav.Visible = true;
            List<PageSummaryItem> secondLevel = item.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
            secondLevel.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

            if (secondLevel.Count > 1)
            {
                secondLevel.First().IsFirst = true;
                secondLevel.Last().IsLast = true;

                if (!String.IsNullOrEmpty(rootUrl)) //Set the jump links
                {
                    foreach (PageSummaryItem child in secondLevel)
                    {
                        if (!String.IsNullOrEmpty(section))
                        {
                            if (child.Name == section)
                            {
                                child.IsCurrent = true;
                            }
                        }
                        child.Url = String.Format("{0}?section={1}", rootUrl, child.Name);
                    }
                }
                else
                {
                    //Set current
                     foreach (PageSummaryItem child in secondLevel)
                    {
                        Item test = child.InnerItem.Axes.SelectSingleItem(String.Format("descendant-or-self::*[@@id='{0}']", currentItem.ID.ToString()));
                        if (test != null)
                        {                            
                            child.IsCurrent = true;                            
                        }
                    }
                }
                
                SecondLevelElements.DataSource = secondLevel;
                SecondLevelElements.DataBind();
            }
        }
    }
}