using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;

namespace mm.virginactive.web.layouts.virginactive.navigation
{
    public partial class ClassListingSection : System.Web.UI.UserControl
    {

        protected PageSummaryItem contextItem;
        protected ClassesSubListingItem classSubListing;
 
        private string classFinderUrl = "";


        public ClassesSubListingItem ClassSubListing
        {
            get { return classSubListing; }
            set
            {
                classSubListing = value;
            }
        }

        public string ClassFinderUrl
        {
            get { return classFinderUrl; }
            set
            {
                classFinderUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClassSubListing != null)
            {
                if (ClassSubListing.InnerItem.HasChildren)
                {
                    List<ClassModuleItem> classes = ClassSubListing.InnerItem.Children.ToList().ConvertAll(x => new ClassModuleItem(x));
                    classes.RemoveAll(x => x.PageSummary.Hidefrommenu.Checked); //Remove all hidden items
                    ClassList.DataSource = classes;
                    ClassList.DataBind();
                }
            }
        }
    }
}