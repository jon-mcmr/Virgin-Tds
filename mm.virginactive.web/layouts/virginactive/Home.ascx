<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.Home" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>


	<div id="carouselWrapper">
		<div class="carousel-content">	
		<ul class="carousel-images">
		  <asp:Repeater ID="RptCarousel" runat="server" OnItemDataBound="RptCarousel_DataBound" >       
			<ItemTemplate>
					<li class="<%# Container.ItemIndex == 0 ? "active"  : "" %>"><asp:Image ID="carouselImage" runat="server" data-subcaption="Curabitur blandit tempus porttitor." />
						<ul class="hotspots">
							<asp:Repeater ID="youtubeHotSpots" runat="server" OnItemDataBound="youtubeHotSpot_DataBound">
							   
									<ItemTemplate>

									<li class="<%# String.Format("pos{0}", Container.ItemIndex + 1) %>">
										<a class="hotspot ir" href="#">Open Hotspot</a>
										<div class="hotspot-data data-below">
											<div class="inner">
												<asp:Literal ID="iframe" runat="server" />
											</div> 							
										</div>
									</li>
									</ItemTemplate>
							</asp:Repeater>
					   </ul>
					</li>
			</ItemTemplate>
		</asp:Repeater>
		 </ul>
			<div id="carousel-controls">
				<div id="streetview-wrap" class="has-tooltip glow-active">
					<div class="streetview-glow"></div>
					<a href="#" id="streetview" class="rep gaqTag streetview" data-gaqcategory="CTA" data-gaqaction="Streetview" data-gaqlabel="Homepage"><span></span><%= Translate.Text("Streetview")%></a>
					<div id="streetview_tooltip" class="tooltip">
						<p class="title"><%= Translate.Text("Look inside Virgin Active")%></p>
						<p class="intro"><%= Translate.Text("Take a walk through our clubs")%></p>
						<div class="tooltip-arrow"></div>
					</div>
				</div>
				<ul id="carousel-icons">
					<li class="prev">
						<a href="#" class="ir">Previous</a>
					</li>
					<li class="current"><div id="timer"><a href="#" class="ir">Play/Pause</a><span id="mask" class=""><span id="rotator" style="" class=""></span></span></span></div></li>
					
					<li class="next">
						<a href="#" class="ir">Next</a>
					</li>
				</ul>           	            
				
				<asp:ListView ID="ButtonList" ItemPlaceholderID="ButtonListPh" runat="server">
					<LayoutTemplate>
					<ul class="carousel-btns">
						<asp:PlaceHolder ID="ButtonListPh" runat="server" />
					</ul>
					</LayoutTemplate>

					<ItemTemplate>
					<li<%# Container.DataItemIndex == 0 ? " class=\"first\"" : "" %>>
						<a href="<%# (Container.DataItem as HomebuttonItem).Link.Url %>" class="btn btn-cta-big gaqTag" data-gaqcategory="CTA" data-gaqaction="<%# (Container.DataItem as HomebuttonItem).Buttontext.Raw %>" data-gaqlabel="Homepage"><%# (Container.DataItem as HomebuttonItem).Buttontext.Raw %></a>
						<div class="tooltip">
							<p class="title"><%# (Container.DataItem as HomebuttonItem).Tooltipheading.Raw %></p>
							<p class="intro"><%# (Container.DataItem as HomebuttonItem).Tooltiptext.Raw %></p>
							<div class="tooltip-arrow"></div>
						</div>
					</li>
					</ItemTemplate>
				</asp:ListView>
		   
			</div>	
		</div>
	</div>
	<div id="content" role="main">

	   <asp:ListView ID="PromoList" runat="server" OnItemDataBound="PromoRender_DataBound" >
				<LayoutTemplate>
				<asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
			</LayoutTemplate>
			<ItemTemplate>
  
			</ItemTemplate>        
		</asp:ListView>
		<asp:PlaceHolder runat="server" ID="PromoBarPlaceHolder">
			<div class="promo-bar clearfix">
				<%= PromoBar.Image.RenderCrop("807x100")%>
				<a href="<%= PromoBar.Link.Url %>" class="btn btn-cta-big">Book a Tour</a>
			</div>
		</asp:PlaceHolder>
		  <asp:ListView ID="PromoListLower" runat="server" OnItemDataBound="PromoRender_DataBound">
			<LayoutTemplate>
				<asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
			</LayoutTemplate>
			<ItemTemplate>
			</ItemTemplate>            
		</asp:ListView>  
	</div> <!-- /content -->
   
   <div class="blog-content">
		<div class="wrapper clearfix">
			<div class="blog-article">
				<h2><%= Translate.Text("From The Blog")%></h2>
				<p class="calendar">
					<span class="day"><%= BlogEntry.Created.ToString("MMM") %></span>
					<span class="date"><%= BlogEntry.Created.ToString("dd") %></span>
					<span class="year"><%= BlogEntry.Created.ToString("yyyy") %></span>
				</p>
				<div class="article-content">
					<h3><a href="<%= BlogEntry.Url %>"><%= BlogEntry.Title.Raw %></a></h3>
					<p class="author"><span>by</span> <%= BlogEntry.GetBlogAuthor() %></p>
					<p><%= BlogEntry.Introduction.Text%> </p>
					
					<a href="<%= BlogEntry.Url %>" class="read-more">Read more</a>
					<div class="article-tags">
						<ul>
							<asp:Literal ID="LitTags" runat="server" />
							
						</ul>
					</div>				
				</div>
			</div>
			<div class="latest-articles">
				<h2><%= Translate.Text("Latest Articles")%></h2>
				<ul>
					<asp:Literal ID="LitLinks" runat="server" />
												
				</ul>
			</div>
		</div>
	</div>
	<div id="va-overlay-wrap" class="streetview-wrap">
		<div id="overlay" class="streetview">
			<h2><%= Translate.Text("Look inside Virgin Active")%></h2>
			<div id="streetview-canvas"></div>
		</div>
	</div>
