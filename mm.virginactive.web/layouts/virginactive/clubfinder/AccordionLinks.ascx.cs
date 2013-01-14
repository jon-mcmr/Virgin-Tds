using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using Sitecore.Collections;

namespace mm.virginactive.web.layouts.virginactive.clubfinder
{
    public partial class AccordionLinks : System.Web.UI.UserControl
    {
        protected PageSummaryItem contextItem;
        protected bool isClassSection = false;

        public PageSummaryItem ContextItem
        {
            get { return contextItem; }
            set
            {
                contextItem = value;
            }
        }

        public bool IsClassSection
        {
            get { return isClassSection; }
            set
            {
                isClassSection = value;
            }
        }

        public List<ClassItem> ClubSpecificClasses { get; set; }
        public List<FacilityItem> ClubSpecificFacilities { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
 
            if (ContextItem != null )
            { 
                if (ContextItem.InnerItem.HasChildren)
                {
                    try
                    {
                        List<PageSummaryItem> secondLevel = null;
                        if (isClassSection)
                        {
                            Item[] secondLvlItems = ContextItem.InnerItem.Axes.SelectItems(String.Format(@"descendant::*[@@tid=""{0}""]", ClassModuleItem.TemplateId));

                            if (secondLvlItems != null)
                            {
                                secondLevel = secondLvlItems.ToList().ConvertAll(x => new PageSummaryItem(x));

                                //Handle club specific classes
                                try
                                {
                                    if (ClubSpecificClasses.Count > 0)
                                    {
                                        ClubSpecificClasses = (from c in ClubSpecificClasses
                                                                where c.ClassCategory.Item.ParentID == ContextItem.ID
                                                                select c).ToList();

                                        if (ClubSpecificClasses.Count > 0)
                                        {
                                            secondLevel.AddRange(ClubSpecificClasses.ConvertAll(Y => new PageSummaryItem(Y.InnerItem)));
                                        }
                                    }
                                }
                                catch (Exception exp)
                                {
                                    Log.Error(String.Format("Could not populate club specific classes in accordion: {0}", exp.Message), this);
                                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(exp);
                                }
                            }

                        }
                        else
                        {
                            ChildList secondLvlItems = ContextItem.InnerItem.Children;
                            if(secondLvlItems != null) {
                                secondLevel = secondLvlItems.ToList().ConvertAll(x => new PageSummaryItem(x));
                            }

                            //Handle club specific facilities
                            try
                            {
                                if (ClubSpecificFacilities.Count > 0)
                                {
                                    ClubSpecificFacilities = (from c in ClubSpecificFacilities
                                                                where c.FacilityArea.Item.ID == ContextItem.ID
                                                                select c).ToList();

                                    if (ClubSpecificFacilities.Count > 0)
                                    {
                                        secondLevel.AddRange(ClubSpecificFacilities.ConvertAll(Y => new PageSummaryItem(Y.InnerItem)));
                                    }
                                }
                            }
                            catch (Exception exp)
                            {
                                Log.Error(String.Format("Could not populate club specific facilities in accordion: {0}", exp.Message), this);
                                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(exp);
                            }
                        }
                        secondLevel.First().IsFirst = true;
                        secondLevel.Last().IsLast = true;

                        LinkList.DataSource = secondLevel;
                        LinkList.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(String.Format("Could not populate accordion links: {0}", ex.Message), this);
                        mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                    }
                }

            }
           
        }
    }
}