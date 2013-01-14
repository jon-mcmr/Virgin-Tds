using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Membership;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership;
using Sitecore.Web;
using Sitecore.Diagnostics;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Helpers;
using Sitecore.Collections;
using Sitecore.Links;

namespace mm.virginactive.web.layouts.virginactive
{
    public partial class MemberLanding : System.Web.UI.UserControl
    {
        protected PersonalmembershipItem context = new PersonalmembershipItem(Sitecore.Context.Item);
        protected PageSummaryItem enqForm;
        protected List<LinkWidgetItem> buttons;
        protected string attribute = @"data-url=""/memberships/personal""";

        protected void Page_Load(object sender, EventArgs e)
        {
            Assert.ArgumentNotNullOrEmpty(ItemPaths.EnquiryForm, "Enquiry form path missing");


            enqForm = new PageSummaryItem(Sitecore.Context.Database.GetItem(ItemPaths.EnquiryForm));

            if (context.InnerItem.HasChildren)
            {
                buttons = context.InnerItem.Children.ToList().ConvertAll(X => new LinkWidgetItem(X));
            }

            Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
            ChildList clubLst = clubRoot.Children;
            Item[] clubs = clubLst.ToArray();

            if (clubs != null)
            {

                List<ClubItem> clubList = clubs.ToList().ConvertAll(X => new ClubItem(X));
                clubList.RemoveAll(x => x.IsHiddenFromMenu());
                clubSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));
                /*
                //Sort clubs alphabetically
                clubList.Sort(delegate(ClubItem c1, ClubItem c2)
                {
                    return c1.Clubname.Raw.CompareTo(c2.Clubname.Raw);

                });*/

                foreach (ClubItem clubItem in clubList)
                {
                    if (clubItem != null)
                    {
                        

                        if (!String.IsNullOrEmpty(clubItem.Clubname.Text))
                        {
                            string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();

                            //check if club is just a placeholder for a campaign
                            if (clubItem.IsPlaceholder.Checked == true)
                            {
                                if (clubItem.PlaceholderCampaign.Item != null)
                                {
                                    //redirect to campaign -set value to url
                                    UrlOptions opt = new UrlOptions();
                                    opt.AddAspxExtension = false;
                                    opt.LanguageEmbedding = LanguageEmbedding.Never;
                                    opt.AlwaysIncludeServerUrl = true;

                                    if (clubItem.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                                    {
                                        clubSelect.Items.Add(new ListItem(clubLabel, LinkManager.GetItemUrl(clubItem.PlaceholderCampaign.Item.Axes.SelectSingleItem(
                                                String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId)), opt) + "?page=Interested"));
                                    }
                                    else
                                    {
                                        clubSelect.Items.Add(new ListItem(clubLabel, LinkManager.GetItemUrl(clubItem.PlaceholderCampaign.Item, opt) + "?page=Interested"));
                                    }
                                }
                                else
                                {
                                    clubSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                                }
                            }
                            else
                            {
                                clubSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                            }

                        }
                    }
                }

                if (String.IsNullOrEmpty(context.Panelvideourl.Raw))
                {
                    IframePlaceholder.Visible = false;
                }
            }

            Control scriptPh = this.Page.FindControl("ScriptPh");
            if (scriptPh != null)
            {
                scriptPh.Controls.Add(new LiteralControl(@"<script>
		            $(function(){
	                    $.va_init.functions.setupForms();
                    });
                </script>"));
            }
        }
    }
}