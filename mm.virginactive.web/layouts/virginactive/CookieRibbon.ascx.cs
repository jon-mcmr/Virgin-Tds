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
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using Sitecore.Collections;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Legals;


namespace mm.virginactive.web.layouts.virginactive
{
    public partial class CookieRibbon : System.Web.UI.UserControl
    {
        protected CookiesFormItem cookieForm;
        protected string cookieFormUrl = "";
        protected CookieRibbonItem cookieRibbon;
        //protected PageSummaryItem currentItem = new PageSummaryItem(Sitecore.Context.Item);

        public string CookieFormUrl
        {
            get { return cookieFormUrl; }
            set { cookieFormUrl = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Set featured promo link and home page link
            Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
            urlOptions.AlwaysIncludeServerUrl = true;
            urlOptions.AddAspxExtension = true;
            urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

            //Get widget details
            cookieRibbon = Sitecore.Context.Database.GetItem(ItemPaths.CookieRibbon);			

            cookieForm = Sitecore.Context.Database.GetItem(ItemPaths.CookiesForm);

            if (cookieForm != null)
            {

                urlOptions.SiteResolving = Sitecore.Configuration.Settings.Rendering.SiteResolving;
                cookieFormUrl = LinkManager.GetItemUrl(cookieForm, urlOptions);
            }

//            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
//		            $(function(){
//	                    DisableCookies();
//                    });
//                </script>"));
        }
    }
}