using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Workouts;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.common.Helpers;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class WorkoutZones : System.Web.UI.UserControl
    {
        private string currentGuid;
        protected WorkoutCategory ContextCategory;

        public string CurrentGuid
        {
            get { return currentGuid; }
            set { currentGuid = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNull(currentGuid, "Source category guid is null");
            Assert.ArgumentNotNullOrEmpty(ItemPaths.WorkoutCategories, "Workout category paths empty");

            List<WorkoutCategory> categoryList = new List<WorkoutCategory>();
            foreach (Item category in Sitecore.Context.Database.GetItem(ItemPaths.WorkoutCategories).Children)
            {
                DropDownItem cat = new DropDownItem(category);
                if (category.ID.ToString().Equals(CurrentGuid))
                {
                    ContextCategory = new WorkoutCategory(cat.ID.ToString(), cat.Id.Raw, cat.Value.Raw, Sitecore.Context.Item);
                    categoryList.Add(ContextCategory);
                }
                else
                {
                    categoryList.Add(new WorkoutCategory(cat.ID.ToString(), cat.Id.Raw, cat.Value.Raw));
                }
            }

            CategoryList.DataSource = categoryList;
            ZoneLinkList.DataSource = ContextCategory.WorkoutZones;
            ZoneList.DataSource = ContextCategory.WorkoutZones;

            CategoryList.DataBind();
            ZoneLinkList.DataBind();
            ZoneList.DataBind();            
        }

        public static string RenderToString(string Guid)
        {
            return SitecoreHelper.RenderUserControl<WorkoutZones>("~/layouts/virginactive/ajax/WorkoutZones.ascx",
                uc =>
                {
                    uc.CurrentGuid = Guid;
                });
        }
    }
}