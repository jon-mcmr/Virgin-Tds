using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubClasses : System.Web.UI.UserControl
    {
        protected PageSummaryItem clubTimetable;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClubClassesLandingItem context = new ClubClassesLandingItem(Sitecore.Context.Item);
            List<string> sharedClasses = context.SharedClasses.Raw.Split('|').ToList();
            List<ClassItem> clubSpecificClasses = new List<ClassItem>();
            if (context.InnerItem.HasChildren)
            {
                clubSpecificClasses = context.InnerItem.Children.ToList().ConvertAll(X => new ClassItem(X));
            }

            try
            {

                //Fetch all the class lists
                List<ClassesListingItem> classLists = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses).Children.ToList().ConvertAll(X => new ClassesListingItem(X));
                //classLists.RemoveAll(x => x.PageSummary.Hidefrommenu.Checked); //Remove all hidden class lists
                List<ClassesListingItem> classListsToRemove = new List<ClassesListingItem>();

                classLists.ForEach(delegate(ClassesListingItem cls)
                {
                    //Intially set ClassList of each class type to all shared classes
                    Item[] classListItms = cls.InnerItem.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", ClassModuleItem.TemplateId));
                    if (classListItms != null)
                    {
                        cls.ClassList = classListItms.ToList().ConvertAll(Y => new ClassModuleItem(Y));
                        //cls.ClassList.RemoveAll(x => x.PageSummary.Hidefrommenu.Checked); //Remove all hidden classes

                        //weed out all the shared classes that are not available in this club
                        cls.ClassList = (from facItem in cls.ClassList
                                         where sharedClasses.Contains(facItem.InnerItem.ID.ToString())
                                         select facItem).ToList();

                        //Include any club specific classes
                        if (clubSpecificClasses.Count > 0)
                        {
                            List<ClassItem> clsItemsToRemove = new List<ClassItem>();
                            clubSpecificClasses.ForEach(delegate(ClassItem clubSpecificCls)
                            {
                                if (SitecoreHelper.ListContainsItem(cls.InnerItem.Children.ToList(), clubSpecificCls.ClassCategory.Item))
                                {
                                    cls.ClassList.Insert(0, new ClassModuleItem(clubSpecificCls.InnerItem)); //Add the club specific classes to top of lists
                                    clsItemsToRemove.Add(clubSpecificCls);
                                }
                            });
                            clubSpecificClasses.RemoveAll(X => clsItemsToRemove.Contains(X)); //Remove the club specific classes we have already added
                        }

                        //Remove any un-used class types
                        if (cls.ClassList.Count == 0)
                            classListsToRemove.Add(cls);
                    }
                });

                //Remove any un-used class types
                classLists.RemoveAll(X => classListsToRemove.Contains(X));

                ClassListing.DataSource = classLists;
                ClassListing.DataBind();
                JumpLinkList.DataSource = classLists;
                JumpLinkList.DataBind();

                //Fetch the timetable page for the club
                Item timetableItm = SitecoreHelper.GetCurrentClub(context.InnerItem).InnerItem.Children["timetables"];

                if (timetableItm != null)
                {
                    clubTimetable = new PageSummaryItem(timetableItm);
                }
                else
                {
                    clubTimetable = new PageSummaryItem(context.InnerItem);
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Had a problem fetching class details for {0} error: {1}", context.InnerItem.Paths.ContentPath, ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
        }
    }
}