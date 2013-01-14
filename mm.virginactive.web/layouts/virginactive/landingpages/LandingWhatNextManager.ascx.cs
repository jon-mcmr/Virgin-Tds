using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using Sitecore.Web;
using mm.virginactive.controls.Model;
using mm.virginactive.common.Helpers;
using mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.People;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
    public partial class LandingWhatNextManager : System.Web.UI.UserControl
    {
        protected LandingWhatsNextItem currentItem = new LandingWhatsNextItem(Sitecore.Context.Item);
        protected ClubItem clubItem = null;
        protected ManagerItem manager = null;
        protected string ManagerImageURL { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              SetSitecoreData();
            }
        }

        /// <summary>
        /// To set the sitecore data
        /// </summary>
        private void SetSitecoreData()
        {
            //For any sitecore data related code here.. 
            SetClubItem();
            SetManager(clubItem);
        }


        /// <summary>
        /// To set the club associated
        /// </summary>
        private void SetClubItem()
        {
            string clubId = WebUtil.GetQueryString("_clubid");

            //Get User from Session
            //Check Session
            User objUser = new User();
			if (Session["sess_User_landing"] != null)
            {
				objUser = (User)Session["sess_User_landing"];

                clubId = objUser.BrowsingHistory.LandingClubID;

            }

            //Get Club Data

            if (!String.IsNullOrEmpty(clubId))
                clubItem = SitecoreHelper.GetClubOnClubId(clubId);


        }

        /// <summary>
        /// Set the Manager object from club
        /// </summary>
        /// <param name="clubItem">The club where manager needs to be looked at</param>
        private void SetManager(ClubItem clubItem)
        {
            if (clubItem != null)
            {
                //get managers info                        
                List<ManagerItem> staffMembers = null;
                if (clubItem.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)) != null)
                {
                    staffMembers = clubItem.InnerItem.Axes.SelectItems(String.Format("descendant-or-self::*[@@tid = '{0}']", ManagerItem.TemplateId)).ToList().ConvertAll(x => new ManagerItem(x));

                    manager = staffMembers.First();
                }

                //If manager found than set the properties.
                if (manager != null)
                {
                    string name = manager.Person.Firstname.Rendered + " " + manager.Person.Lastname.Rendered;
                    ManagerImageURL = manager.Person.Profileimage.RenderCrop("160x160");

					String whatNextText = currentItem.WhatsNextBodytext.Rendered.Replace("##ManagerName##", manager.Person.Firstname.Rendered);
					whatNextText = whatNextText.Replace("##Club##", clubItem.Clubname.Rendered);
					whatNextText = whatNextText.Replace("##ClubPhone##", clubItem.Salestelephonenumber.Rendered);
					whatNextText = whatNextText.Replace("##ManagerFullName##", name);

					ltText.Text = whatNextText;
                }
            }
        }


    }
}