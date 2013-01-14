using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.common.Helpers;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.virginactive.clubfinder
{

    public partial class ClubFinder : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
            ChildList clubLst = clubRoot.Children;
            Item[] clubs = clubLst.ToArray(); 
            if (clubs != null)
            {
                List<ClubItem> clubList = clubs.ToList().ConvertAll(X => new ClubItem(X));
                clubList.RemoveAll(x => x.IsHiddenFromMenu());
                //clubList.RemoveAll(x => x.IsPlaceholder.Checked); 
                clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));


                //Sort clubs alphabetically
                //clubList.Sort(delegate(ClubItem c1, ClubItem c2)
                //{
                //    return c1.Clubname.Raw.CompareTo(c2.Clubname.Raw);

                //});

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
                                    if (clubItem.PlaceholderCampaign.Item.TemplateID.ToString() == ClubMicrositeLandingItem.TemplateId)
                                    {
                                        clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.PlaceholderCampaign.Item.Axes.SelectSingleItem(
                                                String.Format("*[@@tid='{0}']", MicrositeHomeItem.TemplateId)).ID.ToShortID().ToString()));
                                    }
                                    else
                                    {
                                        clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.PlaceholderCampaign.Item.ID.ToShortID().ToString()));
                                    }
                                }
                                else
                                {
                                    clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                                }
                            }
                            else
                            {
                                clubFindSelect.Items.Add(new ListItem(clubLabel, clubItem.ID.ToShortID().ToString()));
                            }
                        }
                    }
                }
            }
            
             this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		            $(function(){
	                    clubFinderAutocomplete();
	                    $.va_init.functions.setupForms();
                    });
                </script>"));
        }
    }
}