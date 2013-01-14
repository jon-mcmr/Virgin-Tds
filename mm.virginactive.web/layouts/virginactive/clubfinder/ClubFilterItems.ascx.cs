using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Web.UI.HtmlControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs;

namespace mm.virginactive.web.layouts.virginactive.clubfinder
{
    public partial class ClubFilterItems : System.Web.UI.UserControl
    {
        private Item sharedContent;
        protected void Page_Load(object sender, EventArgs e)
        {

            sharedContent = Sitecore.Context.Database.GetItem(ItemPaths.SharedClubContent);
            if (sharedContent != null && sharedContent.HasChildren)
            {
                List<ClassItem> clubSpecificClasses = null;
                List<FacilityItem> clubSpecificFacilities = null;

                Item[] classList = Sitecore.Context.Database.SelectItems(String.Format("fast:{1}//*[@@templateid='{0}']", ClassItem.TemplateId, ItemPaths.Clubs));
                if (classList != null)
                {
                    classList =
                        classList.ToList().ConvertAll(x => new PageSummaryItem(x)).Where(x => !x.Hidefrommenu.Checked).
                            ToList().Select(x => x.InnerItem).ToArray();
                    clubSpecificClasses = classList.ToList().ConvertAll(X => new ClassItem(X)).Where(x => x.ClassCategory.Raw != "").ToList();
                }

                Item[] facList = Sitecore.Context.Database.SelectItems(String.Format("fast:{1}//*[@@templateid='{0}']", FacilityItem.TemplateId, ItemPaths.Clubs));
                if (facList != null)
                {
                    facList =
                        facList.ToList().ConvertAll(x => new PageSummaryItem(x)).Where(x => !x.Hidefrommenu.Checked).
                            ToList().Select(x => x.InnerItem).ToArray();
                    clubSpecificFacilities = facList.ToList().ConvertAll(X => new FacilityItem(X)).Where(x => x.FacilityArea.Raw != "").ToList();
                }

                foreach (Item child in sharedContent.Children)
                {
                    if (child.Name == "Facilities" || child.Name == "Classes")
                    {
                        PageSummaryItem currentSection = new PageSummaryItem(child);
                        filter.Controls.Add(new LiteralControl(String.Format(@"<p class=""master-section""><strong>{0}</strong></p>",currentSection.NavigationTitle.Text)));
                        Panel listPanel = new Panel();
                        listPanel.CssClass = "accordion-list";
                        foreach (Item subSection in child.Children)
                        {
                               
                            try
                            {
                                AccordionLinks control = this.Page.LoadControl("~/layouts/virginactive/clubfinder/AccordionLinks.ascx") as AccordionLinks;

                                if (control != null)
                                {
                                    control.ContextItem = new PageSummaryItem(subSection);
                                    if (child.Name == "Classes")
                                    {
                                        control.IsClassSection = true;
                                    }
                                    control.ClubSpecificClasses = clubSpecificClasses;
                                    control.ClubSpecificFacilities = clubSpecificFacilities;
                                    listPanel.Controls.Add(control);
                                }
                            }
                            catch (Exception ex)
                            {
                                filter.Controls.Add(new LiteralControl(String.Format(@"<p class=""master-section""><strong>{0}</strong></p>", ex.Message)));
                                mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                                //TODO: Add exception handeling
                            }
                        }
                        filter.Controls.Add(listPanel);
                    }
                }
                    
            }
            
        }
    }
}