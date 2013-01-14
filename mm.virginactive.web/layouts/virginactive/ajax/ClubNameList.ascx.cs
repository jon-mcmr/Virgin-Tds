using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using mm.virginactive.common.Constants.SitecoreConstants;
using Sitecore.Collections;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.common.Helpers;
using System.Text;
using Sitecore.Web;
using System.IO;
using Newtonsoft.Json;
using mm.virginactive.controls.Model;
using mm.virginactive.controls.Util;
using System.Web.Caching;

namespace mm.virginactive.web.layouts.virginactive.ajax
{
    public partial class ClubNameList : System.Web.UI.UserControl
    {
        protected string ClubNames = "";
        private string query;
        private string filter;
        //private bool includeEsporta = true;

        public string Query
        {
            get { return query; }
            set { query = value; }
        }

        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        //public bool IncludeEsporta
        //{
        //    get { return includeEsporta; }
        //    set { includeEsporta = value; }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

            List<Club> clubs = new List<Club>();

            //excluding all ex-esporta clubs?
            //if (!IncludeEsporta)
            //{
            //    //Do we need to filter on facilities?  ----------------
            //    if (!String.IsNullOrEmpty(Filter))
            //    {
            //        //Is it in cache  ----------------
            //        if (Cache["ClubListExcEs:" + Filter] == null)
            //        {
            //            clubs = GetClubs();

            //            clubs.RemoveAll(x => x.ClubCentric);
            //            clubs = ClubUtil.FilterClubs(clubs, Filter);

            //            double cacheLiveTime = 20.0;
            //            Double.TryParse(Settings.ClubListsCacheMinutes, out cacheLiveTime);
            //            Cache.Insert("ClubListExcEs:" + Filter, clubs, null, DateTime.Now.AddMinutes(cacheLiveTime), Cache.NoSlidingExpiration);
            //        }
            //        else //Fetch from cache
            //        {
            //            clubs = (List<Club>)Cache["ClubListExcEs:" + Filter];
            //        }
            //    }
            //    else
            //    {
            //        //Is it in cache  ----------------
            //        if (Cache["ClubListExcEs"] == null)
            //        {
            //            clubs = GetClubs();

            //            clubs.RemoveAll(x => x.ClubCentric);

            //            double cacheLiveTime = 20.0;
            //            Double.TryParse(Settings.ClubListsCacheMinutes, out cacheLiveTime);
            //            Cache.Insert("ClubListExcEs", clubs, null, DateTime.Now.AddMinutes(cacheLiveTime), Cache.NoSlidingExpiration);
            //        }
            //        else //Fetch from cache
            //        {
            //            clubs = (List<Club>)Cache["ClubListExcEs"];
            //        }
            //    }
            //}
            //else
            //{



            //Do we need to filter on facilities?  ----------------
            if (!String.IsNullOrEmpty(Filter))
            {
                //Is it in cache  ----------------
                if (Cache["ClubList:" + Filter] == null)
                {
                    clubs = GetClubs();

                    clubs = ClubUtil.FilterClubs(clubs, Filter);

                    double cacheLiveTime = 20.0;
                    Double.TryParse(Settings.ClubListsCacheMinutes, out cacheLiveTime);
                    Cache.Insert("ClubList:" + Filter, clubs, null, DateTime.Now.AddMinutes(cacheLiveTime), Cache.NoSlidingExpiration);
                }
                else //Fetch from cache
                {
                    clubs = (List<Club>)Cache["ClubList:" + Filter];
                }
            }
            else
            {
                //Is it in cache  ----------------
                if (Cache["ClubList"] == null)
                {
                    clubs = GetClubs();

                    double cacheLiveTime = 20.0;
                    Double.TryParse(Settings.ClubListsCacheMinutes, out cacheLiveTime);
                    Cache.Insert("ClubList", clubs, null, DateTime.Now.AddMinutes(cacheLiveTime), Cache.NoSlidingExpiration);
                }
                else //Fetch from cache
                {
                    clubs = (List<Club>)Cache["ClubList"];
                }
            }
            //}

            StringWriter result = new StringWriter();

            using (JsonTextWriter w = new JsonTextWriter(result))
            {

                w.WriteStartArray();
                foreach (Club club in clubs)
                {
                    ClubItem clubItem = club.ClubItm;
                    string clubLabel = HtmlRemoval.StripTagsCharArray(clubItem.Clubname.Text).Trim();


                    string memberLoginUrl = SitecoreHelper.GetMembershipLoginUrl(clubItem);

                    if (!String.IsNullOrEmpty(Query))
                    {
                        if (clubItem.Clubname.Text.IndexOf(Query, StringComparison.OrdinalIgnoreCase) >= 0)
                        {       
                            //Only include the clubs that match the query.
                            WriteClubAsJson(w, clubItem.Salesemail.Text, clubLabel, clubItem.Name, clubItem.InnerItem.ID.ToString(), memberLoginUrl);
                        }
                    }
                    else
                    {                        
                        WriteClubAsJson(w, clubItem.Salesemail.Text, clubLabel, clubItem.Name, clubItem.InnerItem.ID.ToString(), memberLoginUrl);
                    }
                }

                w.WriteEndArray();
                ClubNames = result.ToString();
            }
        }

        private void WriteClubAsJson(JsonTextWriter writer, string email, string clubName, string name, string clubGUID, string memberLoginUrl)
        {

            writer.WriteStartObject();      //  {
            writer.WritePropertyName("email"); //      "value" : 
            writer.WriteValue(email);   //          "...", 
            writer.WritePropertyName("label");   //      "label" :
            writer.WriteValue(clubName);     //          "..."
            writer.WritePropertyName("clubGUID"); //      "value" : 
            writer.WriteValue(clubGUID);   //          "...", 
            writer.WritePropertyName("name"); //      "value" : 
            writer.WriteValue(name);   //          "...", 
            writer.WritePropertyName("memberLoginUrl"); //      "value" : 
            writer.WriteValue(memberLoginUrl);   //          "...", 
            writer.WriteEndObject();        //  }
        }

        private List<Club> GetClubs()
        {
            List<Club> clubs = new List<Club>();

            Item clubRoot = Sitecore.Context.Database.GetItem(ItemPaths.Clubs);
            ChildList clubLst = clubRoot.Children;
            Item[] clubsL = clubLst.ToArray();

            if (clubsL != null)
            {
                clubs = clubsL.ToList().ConvertAll(X => new Club(X));
                clubs.RemoveAll(x => x.IsHiddenFromMenu);
                clubs.RemoveAll(x => x.IsPlaceholder && !x.ShowInClubSelect);

                return clubs;
            }
            else
            {
                return null;
            }
        }

        public static string RenderToString(string searchTerm, string filter)
        {
            return SitecoreHelper.RenderUserControl<ClubNameList>("~/layouts/virginactive/ajax/ClubNameList.ascx",
                uc =>
                {
                    uc.Query = searchTerm;
                    uc.Filter = filter;
                });
        }


        //public static string RenderToString(string searchTerm, string filter, bool inlcudeEsporta)
        //{
        //    return SitecoreHelper.RenderUserControl<ClubNameList>("~/layouts/virginactive/ajax/ClubNameList.ascx",
        //        uc =>
        //        {
        //            uc.Query = searchTerm;
        //            uc.Filter = filter;
        //            uc.IncludeEsporta = inlcudeEsporta;
        //        });
        //}
    }
}