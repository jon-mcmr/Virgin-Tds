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

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class SecondaryNavigationBasic : System.Web.UI.UserControl
    {
        protected PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean containsChildPages = false;
            //Check if item is the parent listing item or the child detail item 
            List<Item> childList = currentItem.InnerItem.GetChildren().ToList();
            foreach(Item childItem in childList)
            {
                List<TemplateItem> baseTemplates = childItem.Template.BaseTemplates.ToList();
                foreach (TemplateItem baseTemplate in baseTemplates)
                {
                    if (baseTemplate.ID.ToString() == PageSummaryItem.TemplateId)
                    {
                        //there are children that inherit from PageSummaryItem
                        containsChildPages = true;
                    }
                }
            }

            if (containsChildPages == true)
            {
                //We are on the parent listing page
                DataBindSecondaryNav(currentItem);
            }
            else
            {
                //We are on the detail page
                //Get the parent listing page
                PageSummaryItem landingPage = new PageSummaryItem(currentItem.InnerItem.Parent);
                DataBindSecondaryNav(landingPage);
            }
        }


        private void DataBindSecondaryNav(Item item)
        {
            SubSubNav.Visible = true;
            List<PageSummaryItem> secondLevel = item.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
            secondLevel.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

            if (secondLevel.Count > 0)
            {
                secondLevel.First().IsFirst = true;
                secondLevel.Last().IsLast = true;

                foreach (PageSummaryItem childItem in secondLevel)
                {
                    if (childItem.ID == currentItem.ID)
                    {
                        childItem.IsCurrent = true;
                    }
                }

                SecondLevelElements.DataSource = secondLevel;
                SecondLevelElements.DataBind();
            }
        }
    }
}