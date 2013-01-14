using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.common.Globalization;
using mm.virginactive.controls.Model;
using mm.virginactive.controls.Util;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeLocation : System.Web.UI.UserControl
    {
        public ClubItem ActiveClub;
        public int ZoomLevel = 15;
        //public string NorthEastPoint;
        //public string SouthWestPoint;

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentItem = Sitecore.Context.Item;
            LocationItem location = new LocationItem(currentItem);

            var containerItem = currentItem.Parent.Parent;
            ClubMicrositeLandingItem landing = new ClubMicrositeLandingItem(containerItem);

            Sitecore.Data.Items.Item activeClubItem = landing.Club.Item;

            ActiveClub = new ClubItem(activeClubItem);

            double lat = 0;
            double lng = 0;
            double distance = 10;
            int zoom = 10;
            
            double.TryParse(ActiveClub.Lat.Rendered, out lat);
            double.TryParse(ActiveClub.Long.Rendered, out lng);
            double.TryParse(location.Findclubsrange.Rendered, out distance);
            int.TryParse(location.Zoomlevel.Rendered, out ZoomLevel);

            //NorthEastPoint = location.NorthEastpoint.Rendered;
            //SouthWestPoint = location.SoutWestpoint.Rendered;

            List<Club> clubs = ClubUtil.GetNearestClubsInRange(lat, lng, distance).Where(x => x.ClubItm.InnerItem.ID != ActiveClub.InnerItem.ID).ToList();

            ClubRepeater.DataSource = clubs;
            ClubRepeater.DataBind();
        }

        public string WriteAddress()
        {
            List<String> addressLines = new List<string>();
            if (!String.IsNullOrEmpty(ActiveClub.Addressline1.Rendered))
                addressLines.Add(ActiveClub.Addressline1.Rendered);
            if (!String.IsNullOrEmpty(ActiveClub.Addressline2.Rendered))
                addressLines.Add(ActiveClub.Addressline2.Rendered);
            if (!String.IsNullOrEmpty(ActiveClub.Addressline3.Rendered))
                addressLines.Add(ActiveClub.Addressline3.Rendered);
            if (!String.IsNullOrEmpty(ActiveClub.Addressline4.Rendered) || !String.IsNullOrEmpty(ActiveClub.Postcode.Rendered))
                addressLines.Add(String.Format("{0} {1}", ActiveClub.Addressline4.Rendered, ActiveClub.Postcode.Rendered));

            return String.Format("<h1>{0}</h1><p>{1}<br />{2}: {3}</p>", ActiveClub.Clubname.Rendered, String.Join("<br />", addressLines.ToArray()), Translate.Text("Tel"), ActiveClub.Salestelephonenumber.Rendered);
        }
    }
}