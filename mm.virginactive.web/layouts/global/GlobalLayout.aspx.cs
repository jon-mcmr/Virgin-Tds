using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.controls.Model;
using System.Web.UI.HtmlControls;
using mm.virginactive.common.Helpers;
using mm.virginactive.web.layouts.virginactive;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.Global;
using Sitecore.Collections;
using Sitecore.Data.Items;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Globalization;
using mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates;

namespace mm.virginactive.web.layouts.global
{
    public partial class GlobalLayout : System.Web.UI.Page
    {
        protected GlobalHomeItem currentItem = new GlobalHomeItem(Sitecore.Context.Item);
        //protected string UKHost = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            //Check details of each country
            List<CountryItem> lstCountries = new List<CountryItem>();

            lstCountries = currentItem.InnerItem.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", CountryItem.TemplateId)).ToList().ConvertAll(Y => new CountryItem(Y));

            foreach (CountryItem country in lstCountries)
            {
                switch (country.InnerItem.Name)
                {
                    case "south-africa":
                        lnkSouthAfrica.Text = country.Name.Raw;
                        lnkSouthAfrica.NavigateUrl = country.Url.Raw;

                        if (country.InnerItem.HasChildren && country.Showclublist.Checked)
                        {
                            List<ClubLinkItem> clubs = country.InnerItem.Children.ToList().ConvertAll(x => new ClubLinkItem(x));

                            if (clubs != null && clubs.Count > 0)
                            {
                                clubs.First().IsFirst = true;
                                clubs.Last().IsLast = true;

                                ClubListSouthAfrica.DataSource = clubs;
                                ClubListSouthAfrica.DataBind();
                            }
                        }
                        else
                        {
                            clubsSouthAfrica.Visible = false;
                        }
                        break;
                    case "italy":
                        lnkItaly.Text = country.Name.Raw;
                        lnkItaly.NavigateUrl = country.Url.Raw;

                        if (country.InnerItem.HasChildren && country.Showclublist.Checked)
                        {
                            List<ClubLinkItem> clubs = country.InnerItem.Children.ToList().ConvertAll(x => new ClubLinkItem(x));

                            if (clubs != null && clubs.Count > 0)
                            {
                                clubs.First().IsFirst = true;
                                clubs.Last().IsLast = true;

                                ClubListItaly.DataSource = clubs;
                                ClubListItaly.DataBind();
                            }
                        }
                        else
                        {
                            clubsItaly.Visible = false;
                        }
                        break;
                    case "spain":
                        lnkSpain.Text = country.Name.Raw;
                        lnkSpain.NavigateUrl = country.Url.Raw;

                        if (country.InnerItem.HasChildren && country.Showclublist.Checked)
                        {
                            List<ClubLinkItem> clubs = country.InnerItem.Children.ToList().ConvertAll(x => new ClubLinkItem(x));

                            if (clubs != null && clubs.Count > 0)
                            {
                                clubs.First().IsFirst = true;
                                clubs.Last().IsLast = true;

                                ClubListSpain.DataSource = clubs;
                                ClubListSpain.DataBind();
                            }
                        }
                        else
                        {
                            clubsSpain.Visible = false;
                        }
                        break;
                    case "portugal":
                        lnkPortugal.Text = country.Name.Raw;
                        lnkPortugal.NavigateUrl = country.Url.Raw;

                        if (country.InnerItem.HasChildren && country.Showclublist.Checked)
                        {
                            List<ClubLinkItem> clubs = country.InnerItem.Children.ToList().ConvertAll(x => new ClubLinkItem(x));

                            if (clubs != null && clubs.Count > 0)
                            {
                                clubs.First().IsFirst = true;
                                clubs.Last().IsLast = true;

                                ClubListPortugal.DataSource = clubs;
                                ClubListPortugal.DataBind();
                            }
                        }
                        else
                        {
                            clubsPortugal.Visible = false;
                        }
                        break;
                    case "united-kingdom":
                        lnkUnitedKingdon.Text = country.Name.Raw;
                        lnkUnitedKingdon.NavigateUrl = country.Url.Raw;

                        //UKHost = country.Url.Raw;
                        //if (UKHost.EndsWith("/"))
                        //{
                        //    UKHost = UKHost.Remove(UKHost.Length - 1);
                        //}

                        if (country.Usemainclublist.Checked)
                        {
                            //Use the club items from main site clubs section

                            Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);

                            List<ClubItem> clubList = clubRoot.Children.ToList().ConvertAll(X => new ClubItem(X));

                            clubList.RemoveAll(x => x.IsHiddenFromMenu());
                            clubList.RemoveAll(x => x.IsPlaceholder.Checked && !x.ShowInClubSelect.Checked);

                            if (clubList != null && clubList.Count > 0 && country.Showclublist.Checked)
                            {
                                clubList.First().IsFirst = true;
                                clubList.Last().IsLast = true;

                                ClubListUK.DataSource = clubList;
                                ClubListUK.DataBind();
                            }
                            else
                            {
                                clubsUnitedKingdon.Visible = false;
                            }
                        }

                        break;
                    case "australia":
                        lnkAustralia.Text = country.Name.Raw;
                        lnkAustralia.NavigateUrl = country.Url.Raw;

                        if (country.InnerItem.HasChildren && country.Showclublist.Checked)
                        {
                            List<ClubLinkItem> clubs = country.InnerItem.Children.ToList().ConvertAll(x => new ClubLinkItem(x));

                            if (clubs != null && clubs.Count > 0)
                            {
                                clubs.First().IsFirst = true;
                                clubs.Last().IsLast = true;

                                ClubListAustralia.DataSource = clubs;
                                ClubListAustralia.DataBind();
                            }
                        }
                        else
                        {
                            clubsAustralia.Visible = false;
                        }
                        break;
                    default:
                        break;
                }
            }

            //Load all club lists.


            ////Save session
            //Session["sess_User"] = objUser;

            //Add dynamic content to header
            //HtmlHead head = (HtmlHead)Page.Header;

            //if (objUser.Preferences.MarketingCookies && objUser.Preferences.MetricsCookies)
            //{
            //Have permission to load in OpenTag
            //head.Controls.Add(new LiteralControl(OpenTagHelper.OpenTagVirginActiveGroup));
            //}

            PageSummaryItem item = new PageSummaryItem(currentItem);

            string canonicalTag = item.GetCanonicalTag();

            string metaDescription = item.GetMetaDescription();

            //Add page title //todo: add club name
            string title = Translate.Text("Virgin Active");
            string browserPageTitle = item.GetPageTitle();

            string section = Sitecore.Web.WebUtil.GetQueryString("section");

            if (!String.IsNullOrEmpty(section))
            {
                PageSummaryItem listing = null;
                if (currentItem.InnerItem.TemplateID.ToString() == ClassesListingItem.TemplateId)
                {
                    //Get classes listing browser page title
                    listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedClasses + "/" + section);
                }
                else if (currentItem.InnerItem.TemplateID.ToString() == FacilitiesListingItem.TemplateId)
                {
                    //Get facility listing browser page title
                    listing = Sitecore.Context.Database.GetItem(ItemPaths.SharedFacilities + "/" + section);
                }

                if (listing != null)
                {
                    browserPageTitle = listing.GetPageTitle();
                    canonicalTag = String.IsNullOrEmpty(Request.QueryString["section"]) ? listing.GetCanonicalTag() : listing.GetCanonicalTag(Request.QueryString["section"]);
                    metaDescription = listing.GetMetaDescription();
                }
            }
        }

        protected void ClubListUK_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;

                var club = dataItem.DataItem as ClubItem;
                var lnkClub = e.Item.FindControl("lnkClub") as System.Web.UI.WebControls.Literal;

                string ClubLinkUrl = "http://www.virginactive.co.uk/clubs/" + club.Name;


                if (lnkClub != null)
                {
                    if (club.IsFirst)
                    {
                        lnkClub.Text = @"<a href=""" + ClubLinkUrl + @""" class=""external club uk first"">" + club.Clubname.Rendered + @"</a>";
                    }
                    else
                    {
                        lnkClub.Text = @"<a href=""" + ClubLinkUrl + @""" class=""external club uk"">" + club.Clubname.Rendered + @"</a>";
                    }
                }

            }
        }

    }
}