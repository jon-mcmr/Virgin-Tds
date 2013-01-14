using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.controls.Model;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using Sitecore.Data.Items;
using System.Text;
using mm.virginactive.controls.Util;
using Sitecore.Collections;
using mm.virginactive.common.Constants.SitecoreConstants;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Globalization;
using mm.virginactive.common.Helpers;
using Sitecore.Links;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingClubFinder : System.Web.UI.UserControl
    {
        private string _landingId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HtmlGenericControl BodyTag = (HtmlGenericControl)this.Page.FindControl("BodyTag");
                _landingId = BodyTag.Attributes["data-landing"];

                BindClubDropDown();
            }
        }

        private List<Club> GetClubListByLandingId(string landingId)
        {
            List<Club> clubs = null;

            if (!String.IsNullOrEmpty(landingId))
            {
                Item item = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(landingId));

                ProfileLandingPageItem profileItem = new ProfileLandingPageItem(item);
               

                if (profileItem != null)
                {
                    if (profileItem.LandingPage.LandingBase.Usecustomclublist.Checked)
                    {
                        //Use a custom list of clubs
                        clubs = profileItem.LandingPage.LandingBase.Customclublist.ListItems.ConvertAll(X => new Club(X));


                    }

                    if (clubs != null)
                    {
                        StringBuilder facilitiesList = new StringBuilder();

                        if (profileItem.FacilityorClass != null)
                        {
                            List<Item> clubFacs = profileItem.FacilityorClass.ListItems;

                            if (clubFacs != null)
                            {
                                string delimiter = "";
                                foreach (Item clubFacItem in clubFacs)
                                {
                                    facilitiesList.Append(delimiter);
                                    facilitiesList.Append(clubFacItem.ID);
                                    delimiter = ",";

                                }


                                clubs = ClubUtil.FilterClubs(clubs, facilitiesList.ToString());
                            }
                        }
                    }
                    else
                    {
                        StringBuilder facilitiesList = new StringBuilder();

                        if (profileItem.FacilityorClass != null)
                        {
                            List<Item> clubFacs = profileItem.FacilityorClass.ListItems;

                            if (clubFacs != null)
                            {
                                string delimiter = "";
                                foreach (Item clubFacItem in clubFacs)
                                {
                                    facilitiesList.Append(delimiter);
                                    facilitiesList.Append(clubFacItem.ID);
                                    delimiter = ",";
                                }

                                //Use a custom list of clubs
                                Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
                                ChildList clubLst = clubRoot.Children;
                                clubs = clubLst.ToList().ConvertAll(X => new Club(X));
                                //clubs.RemoveAll(x => x.IsHiddenFromMenu());

                                clubs = ClubUtil.FilterClubs(clubs, facilitiesList.ToString());
                            }
                        }
                    }


                }



            }
            return clubs;
        }

        private void BindClubDropDown()
        {
            clubFindSelect.Items.Add(new ListItem(Translate.Text("Select a club"), ""));

            List<Club> clubs = GetClubListByLandingId(_landingId);

            if (clubs != null)
            {
                foreach (Club club in clubs)
                {
                    string clubLabel = HtmlRemoval.StripTagsCharArray(club.ClubItm.Clubname.Text).Trim();

                    Sitecore.Links.UrlOptions urlOptions = new Sitecore.Links.UrlOptions();
                    urlOptions.AlwaysIncludeServerUrl = true;
                    urlOptions.AddAspxExtension = false;
                    urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

                    string clubUrl = Sitecore.Links.LinkManager.GetItemUrl(Sitecore.Context.Item, urlOptions) + "/enquiry?_clubId=" + club.ClubItm.ClubId.Rendered;
					
					if(Sitecore.Context.Item.TemplateID.ToString() == Settings.LandingPagesEnquiryTemplate)
						clubUrl = Sitecore.Links.LinkManager.GetItemUrl(Sitecore.Context.Item, urlOptions) + "?_clubId=" + club.ClubItm.ClubId.Rendered;


                    ListItem lst = new ListItem(clubLabel, clubUrl);
                    clubFindSelect.Items.Add(lst);
                }
            }


        }
    }
}