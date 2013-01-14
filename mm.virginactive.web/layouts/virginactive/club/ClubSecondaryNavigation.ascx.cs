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
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.HealthAndBeauty;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.PageTemplates;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubSecondaryNavigation : System.Web.UI.UserControl
    {
        protected PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);
        protected PageSummaryItem currentfirstLevelItem;
        private string section = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            section = Sitecore.Web.WebUtil.GetQueryString("section");
            Item[] firstList = currentItem.InnerItem.Axes.SelectItems(String.Format(@"ancestor-or-self::*[@@tid=""{0}"" or @@tid=""{1}""]/child::*", ClassicClubItem.TemplateId, LifeCentreItem.TemplateId));
            if (firstList != null)
            {
                List<Item> firstLevelList = firstList.ToList();
                List<Item> contextAncestorsOrSelf = currentItem.InnerItem.Axes.SelectItems("./ancestor-or-self::*").ToList();

                List<PageSummaryItem> firstLevel = firstLevelList.ConvertAll(x => new PageSummaryItem(x));
                firstLevel.RemoveAll(x => x.Hidefrommenu.Checked);
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

            if (currentfirstLevelItem != null && 
                (currentfirstLevelItem.InnerItem.TemplateID.ToString() != ItemPaths.BlogTemplateId) &&
                (currentfirstLevelItem.InnerItem.TemplateID.ToString() != TimetableDownloadItem.TemplateId) &&
                (currentfirstLevelItem.InnerItem.TemplateID.ToString() != FeedbackItem.TemplateId)
                )
            {   //Do not build the second level for blog section.

                if (currentfirstLevelItem.InnerItem.HasChildren)
                {
                    bool notBound = true;


                    if (currentfirstLevelItem.InnerItem.TemplateID.ToString() == HealthAndBeautyLandingItem.TemplateId)
                    {
                        //Get treatment listing details
                        ClubHealthAndBeautyListingItem listing = new ClubHealthAndBeautyListingItem(Sitecore.Context.Item);
                        //Get the facility modules that are to be shown on listing page
                        List<TreatmentModuleItem> treatmentModules = listing.Sharedtreatments.ListItems.ToList().ConvertAll(X => new TreatmentModuleItem(X));                        

                        HealthAndBeautyLandingItem currentBrand = null;
                        HealthAndBeautyListingItem currentListing = null;
                        List<HealthAndBeautyListingItem> sharedListings = new List<HealthAndBeautyListingItem>();
                        List<HealthAndBeautyListingItem> clubListings = new List<HealthAndBeautyListingItem>();
                        List<HealthAndBeautyListingItem> clubListingsAvailable = new List<HealthAndBeautyListingItem>();
                        
                        foreach (TreatmentModuleItem module in treatmentModules)
                        {
                            //if first item load the shared content
                            if (currentBrand == null)
                            {
                                //find which brand the treatment belongs to
                                Item brand = module.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", HealthAndBeautyLandingItem.TemplateId));

                                if (brand != null)
                                {
                                    currentBrand = new HealthAndBeautyLandingItem(brand);
                                    sharedListings = currentBrand.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", HealthAndBeautyListingItem.TemplateId)).ToList().ConvertAll(x => new HealthAndBeautyListingItem(x));
                                }
                            }

                            //check current listing
                            currentListing = new HealthAndBeautyListingItem(module.InnerItem.Axes.SelectSingleItem(String.Format(@"ancestor-or-self::*[@@tid=""{0}""]", HealthAndBeautyListingItem.TemplateId)));
                            
                            if(currentListing != null)
                            {
                                //Add listing to the listings available collection
                                bool blnExist = false;
                                //Check if it exists already
                                foreach (HealthAndBeautyListingItem existingClubListings in clubListingsAvailable)
                                {
                                    if(existingClubListings.ID.ToString() == currentListing.ID.ToString())
                                    {
                                        blnExist = true;
                                        break;
                                    }
                                }
                                if(blnExist == false)
                                {
                                    //Check that it exists as a listing under the shared brand
                                    foreach (HealthAndBeautyListingItem sharedList in sharedListings)
                                    {
                                        if (sharedList.ID.ToString() == currentListing.ID.ToString())
                                        {
                                            clubListingsAvailable.Add(currentListing);
                                        }
                                    }
                                }
                            }
                        }
                        if (clubListingsAvailable.Count > 1)
                        {
                            HealthAndBeautySecondaryNav(clubListingsAvailable, new PageSummaryItem(currentfirstLevelItem.InnerItem.Children[0]).Url);
                            
                        }
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

        private void HealthAndBeautySecondaryNav(List<HealthAndBeautyListingItem> clubListingsAvailable, string rootUrl)
        {
            SubSubNav.Visible = true;
            List<PageSummaryItem> secondLevel = clubListingsAvailable.ToList().ConvertAll(x => new PageSummaryItem(x));
            secondLevel.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

            if (secondLevel.Count > 0)
            {
                secondLevel.First().IsFirst = true;
                secondLevel.Last().IsLast = true;

                if (!String.IsNullOrEmpty(rootUrl)) //Set the jump links
                {
                    bool activeSet = false;
                    foreach (PageSummaryItem child in secondLevel)
                    {
                        if (!String.IsNullOrEmpty(section))
                        {
                            if (child.Name == section)
                            {
                                child.IsCurrent = true;
                                activeSet = true;
                            }
                        }
                        child.Url = String.Format("{0}?section={1}", rootUrl, child.Name);
                    }

                    if (!activeSet)
                    {
                        secondLevel.First().IsCurrent = true;
                    }
                }

                SecondLevelElements.DataSource = secondLevel;
                SecondLevelElements.DataBind();
            }
        }
    }
}