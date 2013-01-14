using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Modules.Blog.Layouts;
using Sitecore.Web.UI.WebControls;
using Sitecore.Collections;
using mm.virginactive.web.layouts.virginactive.controls;
using mm.virginactive.wrappers.EviBlog;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.PageTemplates;
using mm.virginactive.common.Helpers;
using mm.virginactive.common.Constants.SitecoreConstants;
using mm.virginactive.wrappers.VirginActive.BaseTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;
using mm.virginactive.controls.Model;
using Image = System.Web.UI.WebControls.Image;


namespace mm.virginactive.web.layouts.virginactive
{
	public partial class Home : System.Web.UI.UserControl
	{
		protected string Promo1Heading = "";
		protected string Promo1BodyText = "";
		protected string Promo2Heading = "";
		protected string Promo2BodyText = "";
		protected ImageLinkWidgetItem promo1, promo2;

		protected HomePageItem currentItem = new HomePageItem(Sitecore.Context.Item);
		protected string carouselImageDetails = "";
		protected string carouselImageIcons = "";
		protected string CssClass = "";

		protected BlogEntryItem blogEntry;
		protected PageSummaryItem linkedBlogEntry;
		protected PageSummaryItem blogItem;

		protected PromoBarItem promoBar;

		public PromoBarItem PromoBar
		{
			get { return promoBar; }
			set { promoBar = value; }
		}


		public BlogEntryItem BlogEntry
		{
			get { return blogEntry; }
			set
			{
				blogEntry = value;
			}
		}

		public PageSummaryItem LinkedBlogEntry
		{
			get { return linkedBlogEntry; }
			set
			{
				linkedBlogEntry = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.IsPostBack != true)
			{
				Item WidgetContainer = currentItem.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@tid='{0}']", WidgetContainerItem.TemplateId));

				if (WidgetContainer != null)
				{

					Item upperPanelItem = Sitecore.Context.Database.GetItem(ItemPaths.HomePagePanelUpper);
					List<Item> promoUpperPanelItems =  promoItemsGet(upperPanelItem);
					if(promoUpperPanelItems.Count >= 1)
					{
						PromoList.DataSource = promoUpperPanelItems;
						PromoList.DataBind();
					}


					Item lowerPanelItem = Sitecore.Context.Database.GetItem(ItemPaths.HomePagePanelLower);
				 

					List<Item> promoLowerPanelItems =  promoItemsGet(lowerPanelItem);
					if (promoLowerPanelItems.Count >= 1)
					{
						PromoListLower.DataSource = promoLowerPanelItems;
						PromoListLower.DataBind();
					}


					//Image Carousel
					//Get Carousel Images (Find all descendants of template type HomePageCarouselImageItem) 
					Item[] carousel = WidgetContainer.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", HomePageCarouselImageItem.TemplateId));
					if (carousel != null)
					{
						List<HomePageCarouselImageItem> carouselImages = carousel.ToList().ConvertAll(X => new HomePageCarouselImageItem(X));
						RptCarousel.DataSource = carouselImages;
						RptCarousel.DataBind();

					}


					//Home buttons
					Item[] btns = WidgetContainer.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", HomebuttonItem.TemplateId));
					if (btns != null)
					{
						List<HomebuttonItem> homeButtons = btns.ToList().ConvertAll(X => new HomebuttonItem(X));
						ButtonList.DataSource = homeButtons;
						ButtonList.DataBind();
					}
				}
			}
	  

			this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""http://maps.googleapis.com/maps/api/js?sensor=false""></script>"));
			this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script src=""/virginactive/scripts/virgin.streetview.js""></script>"));
			this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
					$(function(){
						$.va_init.functions.homepageSpecific();
						$('#streetview-wrap a').vaOverlay({showStreetview:true });
					});
				</script>"));

			User objUser = new User();
			//Set user session variable
			if (Session["sess_User"] != null)
			{
				objUser = (User)Session["sess_User"];   
			}
			//Set Home Page Visited Flag
			objUser.HomePageVisited = true;
			Session["sess_User"] = objUser;

			//Check if a club has been set as home page
			if (!String.IsNullOrEmpty(objUser.HomeClubID))
			{
				ClubItem club = SitecoreHelper.GetClubOnClubId(objUser.HomeClubID);
				if (club != null)
				{
					//Redirect to club home
					UrlOptions urlOptions = new UrlOptions();
					urlOptions.AlwaysIncludeServerUrl = true;
					urlOptions.AddAspxExtension = false;
					urlOptions.LanguageEmbedding = LanguageEmbedding.Never;

					Response.Redirect(Sitecore.Links.LinkManager.GetItemUrl(club, urlOptions));
				}
			}


			blogArticlesGet();

			promoBarGet();



		}



		protected void promoBarGet()
		{
			  Item WidgetContainer = currentItem.InnerItem.Axes.SelectSingleItem(String.Format("child::*[@@tid='{0}']", WidgetContainerItem.TemplateId));

				if (WidgetContainer != null)
				{
				  Item[] promoBarItems =  WidgetContainer.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", PromoBarItem.TemplateId));
					
					if(promoBarItems != null && promoBarItems.Any())
					{
						promoBar = promoBarItems[0];
					}
					else
					{
						PromoBarPlaceHolder.Visible = false;
					}
				}

		}

		protected List<Item> promoItemsGet(Item promoItems)
		{

		   
			List<Item> itemList = new List<Item>();

		
			
			foreach (Item item in promoItems.Children)
			{
				itemList.Add(item);

			}

		  


			return itemList;




		}


		protected void PromoRender_DataBound(object Sender, ListViewItemEventArgs e)
		{

			ListViewDataItem dataItem = (ListViewDataItem) e.Item;

			if (dataItem == null)
			{
				return;
			}


			if ( ((Item)dataItem.DataItem).TemplateID.ToString().Equals(YoutubeLinkWidgetItem.TemplateId) ) 
			{

				YoutubeLinkWidget youtubeWidgetControl = LoadControl("/layouts/virginactive/controls/YoutubeLinkWidget.ascx") as YoutubeLinkWidget;


				youtubeWidgetControl.init((Item)dataItem.DataItem, dataItem.DataItemIndex);


				e.Item.Controls.Add(youtubeWidgetControl);
			}
			else
			{

				ImageLinkWidget imageWidgetControl = LoadControl("/layouts/virginactive/controls/ImageLinkWidget.ascx") as ImageLinkWidget;


				imageWidgetControl.init((Item)dataItem.DataItem, dataItem.DataItemIndex);


				e.Item.Controls.Add(imageWidgetControl);
			}


		
		}


		protected void blogArticlesGet()
		{
			blogItem = Sitecore.Context.Database.GetItem(ItemPaths.YourHealthBlog);

			
			if (blogItem != null)
			{
				if (blogItem.InnerItem.HasChildren)
				{
					List<BlogEntryItem> blogPosts = new List<BlogEntryItem>();
					blogPosts = blogItem.InnerItem.Axes.SelectItems(String.Format(@"descendant::*[@@tid=""{0}""]", BlogEntryItem.TemplateId.ToString())).ToList().ConvertAll(X => new BlogEntryItem(X));
					blogPosts.Sort((x, y) => y.Created.CompareTo(x.Created));



					//Check if a blog article has been selected on the page
					List<Item> blogItemList = currentItem.BlogPost.ListItems.ToList();

					if (blogItemList.Count > 0)
					{

						foreach (Item blog in blogItemList)
						{

							if (blog.TemplateID.ToString().Equals(BlogEntryItem.TemplateId))
							{
								blogEntry = blog;
							}
							break;
						}
					}
					else //get most recent article
					{
						foreach (Item blog in blogPosts)
						{

							if (blog.TemplateID.ToString().Equals(BlogEntryItem.TemplateId))
							{
								blogEntry = blog;
							}
							break;
						}
					   
					}

					//Latest articles link list
					int counter = 0;
					string links = String.Empty;
					foreach(BlogEntryItem blogLink in blogPosts.Where(x => x.ID != blogEntry.ID).Take(5))
					{
						counter++;

						if (counter == 5 || counter == blogPosts.Count)
						{
							links += String.Format("<li class=\"last\"><a href=\"{0}\">{1}</a></li>", blogLink.Url, blogLink.Title.Raw);
						}
						else
						{
							links += String.Format("<li><a href=\"{0}\">{1}</a></li>", blogLink.Url, blogLink.Title.Raw);
						}
									
					}

					LitLinks.Text = links;

					//Create tag links
					 string blogUrl = LinkManager.GetItemUrl(blogItem);
			
					for (int i = 0; i < blogEntry.TagsSplit.Count(); i++)
					{
											 
						LitTags.Text = String.Format("<li><a href=\"{0}?tag={1}\">{1}</a> </li>", blogUrl, blogEntry.TagsSplit[i]);
					}
				}

			}
		}

		protected void RptCarousel_DataBound(object Sender, RepeaterItemEventArgs e)
		{
			HomePageCarouselImageItem carouselItem = e.Item.DataItem as HomePageCarouselImageItem;


			string mediaUrl = carouselItem.Image.MediaUrl;
			string dataIcon = carouselItem.GetPanelCssClass();
			string dataCaption = carouselItem.Heading.Raw;
			string dataSubCaption = carouselItem.Subheading.Raw;

			Image carouselImage = e.Item.FindControl("carouselImage") as Image;
			carouselImage.ImageUrl = mediaUrl;
			carouselImage.Attributes.Add("data-icon", dataIcon);
			carouselImage.Attributes.Add("data-caption", dataCaption);
			carouselImage.Attributes.Add("data-subcaption", dataSubCaption);
			carouselImage.Attributes.Add("width", "1200");
			carouselImage.Attributes.Add("height", "534");


			Repeater youtubeRepeater = e.Item.FindControl("youtubeHotSpots") as Repeater;

			youtubeRepeater.DataSource =
			  carouselItem.InnerItem.Axes.SelectItems(String.Format("child::*[@@tid='{0}']", HotspotItem.TemplateId));

			youtubeRepeater.DataBind();


		}


		protected void youtubeHotSpot_DataBound (object Sender, RepeaterItemEventArgs e)
		{
			Item item = e.Item.DataItem as Item;


			HotspotItem hotspot = new HotspotItem(item);

			YoutubeHelper youtubeHelper = new YoutubeHelper();
			string iframe = String.Format("<iframe width=\"335\" height=\"188\" src=\"http://www.youtube.com/embed/{0}?wmode=transparent\" frameborder=\"0\" allowfullscreen></iframe>", youtubeHelper.YoutubeIDGet(hotspot.Videolink.Text));

			Literal iframeLiteral = e.Item.FindControl("iframe") as Literal;

			iframeLiteral.Text = iframe;

		}

	}
}