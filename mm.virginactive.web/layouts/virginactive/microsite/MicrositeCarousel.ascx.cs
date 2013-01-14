using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;
using System.Collections.Specialized;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites;
using mm.virginactive.wrappers.VirginActive.ModuleTemplates;
using mm.virginactive.wrappers.VirginActive.ComponentTemplates;

namespace mm.virginactive.web.layouts.virginactive.microsite
{
    public partial class MicrositeCarousel : System.Web.UI.UserControl
    {
        private List<FacilityModuleItem> facilities;
        public string VideoLink;
        public string FileLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentItem = Sitecore.Context.Item;

            if (!IsPostBack)
            {
                facilities = GetDataSource(currentItem);


                LinkList.DataSource = facilities;
                LinkList.DataBind();

                if (facilities.Any())
                {
                    NonJsImage.Text = GetFirstImage(facilities[0]);
                }

                PanelList.DataSource = facilities;
                PanelList.DataBind();

                //Get link item
                LinkButtonItem linkButton = GetLinkButton(currentItem);
                if(linkButton != null)
                {
                    //set link button
                    ltrLinkButton.Text = @"<a href=""" + linkButton.Buttonlink.Url + @""" class=""btn btn-cta-big"">" + linkButton.Buttontext.Rendered + "</a>";
                }
            }
            GetPromoLinks();
        }

        public string GetFirstImage(FacilityModuleItem facility)
        {
            if (facility.Imagegallery.ListItems.Any())
            {
                MediaItem firstImage = new MediaItem(facility.Imagegallery.ListItems[0]);
                return String.Format("<img src=\"{0}\" alt=\"{1}\" class=\"supersized\" />", StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(firstImage)), firstImage.Alt);
            }

            return String.Empty;
        }

        private List<FacilityModuleItem> GetDataSource(Item currentItem)
        {
            var widgetContainer = currentItem.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", FacilityModulesItem.TemplateId));

            if (widgetContainer != null)
            {
                Item[] moduleItems =
                    widgetContainer.Axes.SelectItems(String.Format("*[@@tid='{0}']", FacilityModuleItem.TemplateId));

                if (moduleItems != null)
                {
                    return moduleItems.ToList().ConvertAll(x => new FacilityModuleItem(x));
                }
            }

            return new List<FacilityModuleItem>();
        }

        private LinkButtonItem GetLinkButton(Item currentItem)
        {
            var widgetContainer = currentItem.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", WidgetContainerItem.TemplateId));

            if (widgetContainer != null)
            {
                LinkButtonItem linkButton =
                    widgetContainer.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", LinkButtonItem.TemplateId));

                if (linkButton != null)
                {
                    return linkButton;
                }
            }

            return null;
        }

        protected string GetGalleryImageLink(MediaItem mediaItem, int index)
        {
            if (index == 0)
            {
                return "#";
            }

            return MediaManager.GetMediaUrl(mediaItem);
        }

        protected string GetGalleryImageClassOrTarget(int index)
        {
            if (index == 0)
            {
                return "class=\"active-thumb\"";
            }

            return "target=\"_blank\"";
        }

        protected void RelatedListItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Don't show current panel in related list
            if (((RepeaterItem)e.Item.Parent.Parent).ItemIndex == e.Item.ItemIndex)
            {
                e.Item.Visible = false;
            }
        }

        protected void PanelListItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater RelatedList = e.Item.FindControl("RelatedList") as Repeater;
            ListView GalleryList = e.Item.FindControl("GalleryList") as ListView;

            RelatedList.ItemDataBound += RelatedListItemDataBound;
            RelatedList.DataSource = facilities;
            RelatedList.DataBind();

            // Only show gallery controls if there are multiple images for this panel
            List<MediaItem> galleryItems = (e.Item.DataItem as FacilityModuleItem).Imagegallery.ListItems.ConvertAll(x => new MediaItem(x));
            if (galleryItems.Count > 1)
            {
                GalleryList.DataSource = galleryItems;
                GalleryList.DataBind();
            }
            else
            {
                GalleryList.Visible = false;
                PlaceHolder SubheadingPlaceHolder = e.Item.FindControl("SubheadingPlaceHolder") as PlaceHolder;
                SubheadingPlaceHolder.Visible = false;
            }
            
        }

        protected string GetInterestedUrl()
        {
            var currentItem = Sitecore.Context.Item;

            var interestedItem = currentItem.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", MembershipEnquiryItem.TemplateId));
            if (interestedItem == null)
            {
                return String.Empty;
            }

            return LinkManager.GetItemUrl(interestedItem);
        }

        private void GetPromoLinks()
        {
            var currentItem = Sitecore.Context.Item;

            var promoItem = currentItem.Axes.SelectSingleItem(String.Format("*[@@tid='{0}']", PromoAreaItem.TemplateId));
            if (promoItem == null)
            {
                VideoPlaceholder.Visible = false;
                VideoLink = "#";
                FilePlaceholder.Visible = false;
                FileLink = "#";
                return;
            }

            PromoAreaItem promo = new PromoAreaItem(promoItem);

            GetVideoLink(promo);
            GetFileLink(promo);
        }

        private void GetVideoLink(PromoAreaItem promo)
        {
            VideoThumbnail.Text = promo.Videothumbnail.RenderCrop("120x68");
            VideoHeading.Text = promo.Videoheading.Rendered;

            if (String.IsNullOrEmpty(promo.Videolink.Rendered))
            {
                VideoPlaceholder.Visible = false;
                VideoLink = "#";
                return;
            }

            VideoLink = String.Format("{0}#lightbox", LinkManager.GetItemUrl(promo.InnerItem));
        }

        private void GetFileLink(PromoAreaItem promo)
        {
            FileThumbnail.Text = promo.Filethumbnail.RenderCrop("120x68");
            FileHeading.Text = promo.Fileheading.Rendered;

            if (String.IsNullOrEmpty(promo.File.MediaUrl))
            {
                FilePlaceholder.Visible = false;
                FileLink = "#";
                return;
            }

            FileLink = promo.File.MediaUrl;
        }
    }
}