using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;

namespace mm.virginactive.web.layouts.virginactive.club
{
    public partial class ClubPeopleListing : System.Web.UI.UserControl
    {
        protected List<PersonItem> People = new List<PersonItem>();
        protected string[] az =  "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".Split(',');

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Sitecore.Context.Item.HasChildren)
            {
                People = Sitecore.Context.Item.Children.ToList().ConvertAll(X => new PersonItem(X));
                People.Sort((a, b) => string.Compare(a.Firstname.Raw, b.Firstname.Raw)); //Sort aphabetically by first name               
            }

            if (People.Count > 0)
            {
                AlphaFilter.DataSource = az;
                AlphaFilter.DataBind();

                PeopleList.DataSource = People;
                PeopleList.DataBind();
            }

            
        }
    }
}