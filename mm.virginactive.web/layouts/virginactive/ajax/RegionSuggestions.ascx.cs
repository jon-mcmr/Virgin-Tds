using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Helpers;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using System.Text.RegularExpressions;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class RegionSuggestions : System.Web.UI.UserControl
    {
        protected string baseUrl = "";
        private string term = "";

        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }

        public string Term
        {
            get { return term; }
            set { term = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Item componentBase = Sitecore.Context.Database.GetItem(ItemPaths.Components);

            if (componentBase != null)
            {
                Item[] terms = componentBase.Axes.SelectItems(String.Format("descendant::*[@@tid='{0}']", SearchTermItem.TemplateId));
                if (terms != null)
                {
                    foreach (Item term in terms)
                    {
                        SearchTermItem itm = new SearchTermItem(term);

                        Regex match = new Regex(itm.Term.Raw);
                        if (match.IsMatch(Term))
                        {
                            if (term.HasChildren)
                            {
                                Item[] matchItms = term.Children.ToArray();
                                List<SearchTermMatchItem> matches = matchItms.ToList().ConvertAll(X => new SearchTermMatchItem(X));
                                matches.ForEach(delegate(SearchTermMatchItem m)
                                {
                                    if (m.Matchtitle.Raw.ToLower() == Term.ToLower())
                                    {
                                        m.IsCurrent = true;
                                    }
                                });
                                SuggestionList.DataSource = matches;
                                SuggestionList.DataBind();
                            }
                            break;
                        }

                    }
                }
            }
        }

        public static string RenderToString(string searchTerm, string url)
        {
            return SitecoreHelper.RenderUserControl<RegionSuggestions>("~/layouts/virginactive/ajax/RegionSuggestions.ascx",
                uc =>
                {
                    uc.Term = searchTerm;
                    uc.BaseUrl = url;
                });
        }
    }
}