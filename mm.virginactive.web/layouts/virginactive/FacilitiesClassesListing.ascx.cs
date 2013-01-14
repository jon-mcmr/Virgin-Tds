using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.web.layouts.virginactive.navigation;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using System.Collections.Specialized;


namespace mm.virginactive.web.layouts.virginactive
{
    public partial class FacilitiesLandingListing : System.Web.UI.UserControl
    {

        private string cssClass = "";

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Item currentItem = Sitecore.Context.Item;

            //Retrieve CSS class
            string rawParameters = Attributes["sc_parameters"];
            NameValueCollection parameters = Sitecore.Web.WebUtil.ParseUrlParameters(rawParameters);
            cssClass = parameters["CssClass"];

            string section = Sitecore.Web.WebUtil.GetQueryString("section");

            if ( currentItem.TemplateID.ToString() == ClassesListingItem.TemplateId )
            {
                if ( !String.IsNullOrEmpty(section) )
                {
                    //Perform facility listing functionality

                    Item listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses + "/" + section);
                    if (listing != null)
                    {
                        List<PageSummaryItem> listingCollection = listing.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                        listingCollection.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

                        foreach (PageSummaryItem child in listingCollection)
                        {
                            ClassListingSection panel = this.Page.LoadControl("~/layouts/virginactive/navigation/ClassListingSection.ascx") as ClassListingSection;
                            panel.ClassSubListing = new ClassesSubListingItem(child);
                            panel.EnableViewState = false;
                            phListing.Controls.Add(panel);
                        }
                    }
                }

                this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		             $("".va-overlay-link"").vaOverlay({updateClasses:true, updateDivContainer:true  });
                </script>"));
            }


            if ( currentItem.TemplateID.ToString() == FacilitiesListingItem.TemplateId )
            {
                
                if ( !String.IsNullOrEmpty(section) )
                {
                    //Perform facility listing functionality

                    Item listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities + "/" + section);
                    if (listing != null)
                    {
                        List<PageSummaryItem> listingCollection = listing.Children.ToList().ConvertAll(x => new PageSummaryItem(x));
                        listingCollection.RemoveAll(x => x.Hidefrommenu.Checked); //Remove all hidden items

                        foreach (PageSummaryItem child in listingCollection)
                        {
                            ColourMeFitImagePanel panel = this.Page.LoadControl("~/layouts/virginactive/navigation/ColourMeFitImagePanel.ascx") as ColourMeFitImagePanel;
                            panel.FacilityModule = new FacilityModuleItem(child);
                            phListing.Controls.Add(panel);
                        }
                    }
                }

            }

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		        $(function(){
		            $.va_init.functions.cmfDials();
                });
            </script>"));

        }
    }
}