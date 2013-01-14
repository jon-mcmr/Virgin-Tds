using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Workouts;
using Sitecore.Diagnostics;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Data.Items;
using mm.virginactive.web.layouts.virginactive.ajax;
using Sitecore.Web;
using Sitecore.Collections;

namespace mm.virginactive.web.layouts.virginactive.workouts
{
    public partial class WorkoutLanding : System.Web.UI.UserControl
    {
        protected WorkoutLandingItem landing = new WorkoutLandingItem(Sitecore.Context.Item);

        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.WorkoutCategories, "Workout category paths empty");

           

            Item categoryList = Sitecore.Context.Database.GetItem(ItemPaths.WorkoutCategories);
            ChildList children = categoryList.Children;
            string categoryguid = children[0].ID.ToString(); 

            //We need to check if the querystring is send with area=categoryname
            //If so, load the relavent category, otherwise load first category
            if (!String.IsNullOrEmpty(WebUtil.GetQueryString("area")))
            {
                if (children[WebUtil.GetQueryString("area")] != null)
                {
                    categoryguid = children[WebUtil.GetQueryString("area")].ID.ToString();
                }
            }
            

            if (categoryList.HasChildren)
            {
                string script = String.Format(@"<script language=""javascript"">LoadWorkout('{0}');</script>", categoryguid);
                this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(script));
            }
            //Uncomment the code below and comment the block above if you want to test the ajax control directly

            /*
            WorkoutZones zones = Page.LoadControl("~/layouts/virginactive/ajax/WorkoutZones.ascx") as WorkoutZones;
            zones.CurrentGuid = firstCategory.Children[0].ID.ToString()
            TestPh.Controls.Add(zones); */

            //Register script
            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
                $(function(){
	                $.va_init.functions.workouts();
                });
                </script>")); 

           
             

            
        }
    }
}