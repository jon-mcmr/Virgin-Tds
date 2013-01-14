using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.LandingTemplates;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.screportdal.Models;
using mm.virginactive.screportdal;
using mm.virginactive.common.Helpers;
using System.Text;

namespace mm.virginactive.web.layouts.virginactive.landingpages
{
	public partial class LandingTwitterPanel : System.Web.UI.UserControl
	{
		protected LandingPageItem currentItem = new LandingPageItem(Sitecore.Context.Item);

		protected string TwitterHeading { get; set; }
		protected string TwitterImage { get; set; }
		protected string TwitterUsername { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				//If twitter heading/media item is not present ie field is not there on the child template then pick up the value from parent template.
				if (String.IsNullOrEmpty(currentItem.TwitterHeading.Rendered))
					TwitterHeading = new LandingPageItem(currentItem.InnerItem.Parent).TwitterHeading.Rendered;
				else
					TwitterHeading = currentItem.TwitterHeading.Rendered;

				if (String.IsNullOrEmpty(currentItem.Username.Rendered))
					TwitterUsername = new LandingPageItem(currentItem.InnerItem.Parent).Username.Rendered;
				else
					TwitterUsername = currentItem.Username.Rendered;


				if (currentItem.InnerItem.TemplateID.ToString() == Settings.LandingPagesWhatsNextTemplate)
				{
					phTwitterHeading.Visible = true;
				}
				else
					phTwitterHeading.Visible = false;

				if (String.IsNullOrEmpty(currentItem.Image.MediaUrl))
				{
					TwitterImage = new LandingPageItem(currentItem.InnerItem.Parent).Image.MediaUrl;

				}
				else
				{
					TwitterImage = currentItem.Image.MediaUrl;
				}
				SetTweets();

		

			}
		}

		private void SetTweets()
		{
			List<TwitterFeed> tweets = null;
			string username =string.Empty;

			if (String.IsNullOrEmpty(currentItem.Username.Rendered))
			{
				username = new LandingPageItem(currentItem.InnerItem.Parent).Username.Rendered;
			}
			else
			{
				username = currentItem.Username.Rendered;		 
			}

			tweets = TwitterHelper.GetLatestFavouriteTweets(username);
			List<TwitterList> twitterList = new List<TwitterList>();

			if (tweets != null && tweets.Count > 0)
			{
				foreach (TwitterFeed feed in tweets)
				{
					string[] tweetWords = feed.FeedDescription.Split(' ');
					TwitterList tList = new TwitterList();

					StringBuilder tDesc = new StringBuilder();

					foreach (string str in tweetWords)
					{
						string newStr = str;


						if (str.StartsWith("@") || str.StartsWith("#"))
						{
							newStr = str.Replace(str, "<a href='http://twitter.com/" + str + "' target='_blank'>" + str + "</a>");
						}

						if (str.EndsWith(":"))
							newStr = string.Empty;

						if(str.StartsWith("http://") || str.StartsWith("https://"))
						{
							newStr = str.Replace(str, "<a href='" + str + "' target='_blank'>" + str + "</a>");
						}

						tDesc.Append(newStr + " ");
					}

					tList.Tweet = tDesc.ToString();

					tList.FavUserName = "@" + feed.FeedDescription.Split(':').First();
					tList.UserLink = "http://twitter.com/" + tList.FavUserName;

					twitterList.Add(tList);

				}

				repTweets.DataSource = twitterList;
				repTweets.DataBind();
			}
		}

	}

	public class TwitterList
	{
		public string UserLink { get; set; }
		/// <summary>
		/// Fav user name
		/// </summary>
		public string FavUserName { get; set; }
		public string Tweet { get; set; }
	}
}
