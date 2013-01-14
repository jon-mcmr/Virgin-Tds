using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using System.Text;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using Sitecore.Diagnostics;
using Sitecore.Data.Fields;

namespace mm.virginactive.web.layouts.virginactive.navigation
{
    public partial class FacilitiesClassesNav : System.Web.UI.UserControl
    {
        protected PageSummaryItem healthAndBeautyLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.HealthAndBeautyLanding));
        protected PageSummaryItem personalTrainingLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.PersonalTrainingLanding));
        protected PageSummaryItem facilityAndClass = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesAndClasses));
        protected PageSummaryItem classLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClassesLanding));
        protected PageSummaryItem classListing = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.ClassesListing));
        protected PageSummaryItem facilityLanding = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesLanding));
        protected PageSummaryItem facilityListing = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesListing));
        protected SubheadingLinkWidgetItem widget = new SubheadingLinkWidgetItem(new HeaderItem(Sitecore.Context.Database.GetItem(ItemPaths.Header)).Facilitymenuwidget.Item);

        protected string classOutput = "";
        protected string healthAndBeautyOutput = "";
        //protected string[] facilityCss = { "level2-col level2-col1", "level2-col level2-col2", "level2-col level2-col3" };
        protected string ActiveFlag;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string[] facilityCss = Settings.FacilityColumnStyles.Split(';');
                PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);
                //Set active flag if current page is in facilities and classes           
                if (currentItem.InnerItem.Axes.SelectItems("./ancestor-or-self::*") != null)
                {
                    List<Item> contextAncestorsOrSelf = currentItem.InnerItem.Axes.SelectItems("./ancestor-or-self::*").ToList();

                    Item facilitiesAndClassesSection = Sitecore.Context.Database.GetItem(ItemPaths.FacilitiesAndClasses);

                    if (SitecoreHelper.ListContainsItem(contextAncestorsOrSelf, facilitiesAndClassesSection))
                    {
                        ActiveFlag = " active";
                    }
                }

                List<Panel> facilityPanels = new List<Panel>();
                //Generate and HTMLPanel (div) to match every type in facilityCss
                foreach (string facilityStyle in facilityCss)
                {
                    Panel panel = new Panel();
                    panel.CssClass = facilityStyle;
                    facilityPanels.Add(panel);
                }

                Item sharedFacilities = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities);
                List<PageSummaryItem> facilitySections = sharedFacilities.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                facilitySections.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

                int itemTotal = 0; //Counter to keep track of the numbe rof facilty menu items we have added so far


                foreach (PageSummaryItem section in facilitySections)
                {
                    List<PageSummaryItem> childItems = section.InnerItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                    childItems.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items
                    childItems.RemoveAll(x => ((CheckboxField)x.InnerItem.Fields["Hide from drop down"]).Checked); //Remove all hidden from MDD items
                    itemTotal += childItems.Count();
                }

                double maxFacilityLinksPerCol = ((double)(itemTotal) / Convert.ToDouble(Settings.NoFacilityCols));

                itemTotal = 0;
                int panelToUse = 0;
                int itemsInPanel = 0;

                foreach (PageSummaryItem section in facilitySections)
                {
                    StringBuilder sb = GetSectionHtml(section, facilityListing);
                    int listItemCount = CountWords(sb.ToString(), "<li>"); //We count the number of <li> items to determine the menu item count
                    itemTotal += listItemCount;

                    double panelLimit = maxFacilityLinksPerCol * (panelToUse + 1);

                    if (itemsInPanel == 0)
                    {
                        facilityPanels[panelToUse].Controls.Add(new LiteralControl(sb.ToString()));
                                                
                        if ((double)(itemTotal) > panelLimit)
                        {
                            itemsInPanel = 0;
                            panelToUse++;
                        }
                        else
                        {
                            itemsInPanel += listItemCount;
                        }
                    }
                    else
                    {
                        //see if we can fit into current panel or move to next
                        if (itemsInPanel > (maxFacilityLinksPerCol / 2))
                        {
                            //move to next panel
                            if ((double)(itemTotal) > panelLimit)
                            {
                                itemsInPanel = listItemCount;
                                panelToUse++;
                            }
                            else
                            {
                                itemsInPanel += listItemCount;
                            }

                            facilityPanels[panelToUse].Controls.Add(new LiteralControl(sb.ToString()));
                        }
                        else
                        {
                            //use current panel

                            facilityPanels[panelToUse].Controls.Add(new LiteralControl(sb.ToString()));

                            if ((double)(itemTotal) > panelLimit)
                            {
                                itemsInPanel = 0;
                                panelToUse++;
                            }
                            else
                            {
                                itemsInPanel += listItemCount;
                            }
                        }
                    }

                }

                //Add the newly populated facility panels
                foreach (Panel pane in facilityPanels)
                {
                    facilityPh.Controls.Add(pane);
                }

                //Generate the class links, no need to worry about multiple levels
                Item sharedClasses = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses);
                if (sharedClasses != null)
                {
                    List<PageSummaryItem> classSections = sharedClasses.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                    classSections.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items
                    foreach (PageSummaryItem section in classSections)
                    {
                        classOutput += GetSectionHtml(section, classListing).ToString();
                    }
                }

                //Bind Kids section
                KidsSection.Path = ItemPaths.KidsLanding;

                //Bind Health and Beauty section
                //HealthAndBeautySection.Path = ItemPaths.HealthAndBeautyLanding;

                //Generate the health and beauty links, these act just as jump links
                if (healthAndBeautyLanding != null)
                {
                    StringBuilder sb = new StringBuilder();
                    List<PageSummaryItem> healthAndBeautySections = healthAndBeautyLanding.InnerItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                    healthAndBeautySections.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items
                    foreach (PageSummaryItem section in healthAndBeautySections)
                    {
                        sb.AppendFormat(@"<li><a href=""{0}#{2}"">{1}</a></li>", healthAndBeautyLanding.Url, section.NavigationTitle.Text, section.Name.ToLower());
                    }
                    healthAndBeautyOutput = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error printing Facilities and classes Nav: {0}" + ex.Message), this);
                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
            }
            
        }


        private StringBuilder GetSectionHtml(PageSummaryItem section, PageSummaryItem landing)
        {

                StringBuilder sb = new StringBuilder();
                try
                {
                    sb.AppendFormat(@"<h3><a href=""{0}?section={2}"">{1}</a></h3>", landing.Url, section.NavigationTitle.Text, section.Name.ToLower());

                    List<PageSummaryItem> childItems = section.InnerItem.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                    childItems.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items
                    childItems.RemoveAll(x => ((CheckboxField)x.InnerItem.Fields["Hide from drop down"]).Checked); //Remove all hidden from MDD items

                    StringBuilder children = new StringBuilder();
                    foreach (PageSummaryItem child in childItems)
                    {
                        children.AppendFormat(@"<li><a href=""{0}?section={2}#{3}"">{1}</a></li>", landing.Url, child.NavigationTitle.Text, section.Name.ToLower(), child.Name.ToLower());
                    }

                    sb.AppendFormat(@"<ul>{0}</ul>", children.ToString());
                }
                catch (Exception ex)
                {
                    Log.Error(String.Format("Error printing Facilities and classes Nav: {0}" + ex.Message), this);
                    mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                }
                return sb;
        }

        private int CountWords(string text, string word)
        {
            int count = (text.Length - text.Replace(word, "").Length) / word.Length;
            return count;
        }

    }
}