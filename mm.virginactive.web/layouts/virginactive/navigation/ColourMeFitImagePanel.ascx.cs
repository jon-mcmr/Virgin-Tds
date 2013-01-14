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
    public partial class ColourMeFitImagePanel : System.Web.UI.UserControl
    {
        private string cssClass = "";
        protected PageSummaryItem contextItem;
        protected FacilityModuleItem facilityModule;
        private string rootUrl = "";
        private bool headerIsH2 = false;
        private bool isFullWidth = false;
        private bool showColourMeFit = false;
        private string cardioPercentage = "";
        private string strengthPercentage = "";
        private string balancePercentage = "";
        private string detailLinkURL = "";

        public string CssClass
        {
            get { return cssClass; }
            set { cssClass = value; }
        }

        public string DetailLinkURL
        {
            get { return detailLinkURL; }
            set { detailLinkURL = value; }
        }

        public PageSummaryItem ContextItem
        {
            get { return contextItem; }
            set
            {
                contextItem = value;
            }
        }

        public FacilityModuleItem FacilityModule
        {
            get { return facilityModule; }
            set
            {
                facilityModule = value;
            }
        }

        public string RootUrl
        {
            get { return rootUrl; }
            set
            {
                rootUrl = value;
            }
        }

        public bool HeaderIsH2
        {
            get { return headerIsH2; }
            set
            {
                headerIsH2 = value;
            }
        }

        public bool IsFullWidth
        {
            get { return isFullWidth; }
            set
            {
                isFullWidth = value;
            }
        }

        public bool ShowColourMeFit
        {
            get { return showColourMeFit; }
            set
            {
                showColourMeFit = value;
            }
        }

        public string CardioPercentage
        {
            get { return cardioPercentage; }
            set
            {
                cardioPercentage = value;
            }
        }

        public string StrengthPercentage
        {
            get { return strengthPercentage; }
            set
            {
                strengthPercentage = value;
            }
        }

        public string BalancePercentage
        {
            get { return balancePercentage; }
            set
            {
                balancePercentage = value;
            }
        }

        //This will change
        public string GetImage
        {
            get
            {
                if (cssClass == "full-panel")
                {
                    return FacilityModule.Abstract.Image.RenderCrop("440x360");
                }

                if (cssClass == "half-panel")
                {
                    return FacilityModule.Abstract.Image.RenderCrop("170x210");
                }

                if (cssClass == "third-panel")
                {
                    return FacilityModule.Abstract.Image.RenderCrop("280x180");
                }

                if (cssClass == "quarter-panel")
                {
                    return FacilityModule.Abstract.Image.RenderCrop("200x110");
                }

                return "";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (FacilityModule != null)
            {
                //Retrieve CSS class
                cssClass = FacilityModule.Abstract.GetPanelCssClass();
               
                if (cssClass == "full-panel")
                {
                    HeaderIsH2 = true;
                } 

                //Get context item
                if (contextItem == null)
                {
                    contextItem = Sitecore.Context.Item;
                }

                System.Text.StringBuilder markupBuilder = new System.Text.StringBuilder();

                //check if link through to detail page
                if (FacilityModule.Isdetailpage.Checked == false)
                {
                    //no link through -just show heading
                    markupBuilder.Append(HeaderIsH2 ? "<h2>" : "<h3>");
                    markupBuilder.Append(FacilityModule.PageSummary.NavigationTitle.Text);
                    markupBuilder.Append(HeaderIsH2 ? "</h2>" : "</h3>");
                }
                else
                {
                    //link heading
                    markupBuilder.Append(@"<a href=""");
                    markupBuilder.Append(String.Format("{0}?facility={1}", contextItem.Url, FacilityModule.Name));
                    markupBuilder.Append(@""">");
                    markupBuilder.Append(HeaderIsH2 ? "<h2>" : "<h3>");
                    markupBuilder.Append(FacilityModule.PageSummary.NavigationTitle.Text);
                    markupBuilder.Append(HeaderIsH2 ? "</h2>" : "</h3>");
                    markupBuilder.Append(@"</a>");

                    //additional 'View more' link through to detail page
                    viewDetailsLink.Visible = true;
                    detailLinkURL = String.Format("{0}?facility={1}", contextItem.Url, FacilityModule.Name);
                }

                //Configure Layout
                if (cssClass == "full-panel")
                {
                    IsFullWidth = true;
                    HeaderIsH2 = true;
                    //use heading position 2
                    heading2.Text = markupBuilder.ToString();
                    heading2.Visible = true;
                }
                else
                {
                    //use title position 2
                    heading1.Text = markupBuilder.ToString();
                    heading1.Visible = true;
                }


                //Retrieve Colour Me Fit
                ShowColourMeFit = FacilityModule.Showcolourmefit.Checked;

                if (ShowColourMeFit)
                {
                    CardioPercentage = Convert.ToString(FacilityModule.Cardiopercentage);
                    StrengthPercentage = Convert.ToString(FacilityModule.Strengthpercentage);
                    BalancePercentage = Convert.ToString(FacilityModule.Balancepercentage);
                }



            }

        }

        public string GetColourMeFitImages
        {
            get
            {
                if (cssClass == "full-panel")
                {
                    return @"<img src=""/virginactive/images/placeholders/fac-hero-placeholder-r.png"" alt=""Placeholder"" />";
                    //TO DO:
                    //return @"<ul><li class="colour-me-fit-full"></ul>";
                }

                if (cssClass == "half-panel")
                {
                    return @"<img src=""/virginactive/images/placeholders/half-footer.png"" alt=""Placeholder"" />";
                    //TO DO
                    //return @"<ul><li class="colour-me-fit-cardio-half"></li><li class="colour-me-fit-strength-half"></li><li class="colour-me-fit-balance-half"></li></ul>";
                }

                if (cssClass == "third-panel")
                {
                    return @"<img src=""/virginactive/images/placeholders/third-footer.png"" alt=""Placeholder"" />";
                }

                if (cssClass == "quarter-panel")
                {
                    return @"<img src=""/virginactive/images/placeholders/quarter-footer.png"" alt=""Placeholder"" />";
                }

                return "";
            }
        }
    }
}