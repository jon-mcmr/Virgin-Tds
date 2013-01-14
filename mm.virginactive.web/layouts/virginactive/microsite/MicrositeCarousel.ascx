<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeCarousel.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeCarousel" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="Sitecore.Resources.Media" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites" %>
<div id="content" class="home">
            <div class="sidebar">
			    <div id="controls-wrapper" class="load-item">
				    <!--Navigation-->
				    <ul id="slide-list">
                        <asp:Repeater ID="LinkList" runat="server">
                            <ItemTemplate> 
                               <li>
                                    <a href="#panel<%# Container.ItemIndex %>"><%# (Container.DataItem as FacilityModuleItem).Heading.Rendered %></a>
                               </li>
                            </ItemTemplate>
                        </asp:Repeater>
	                </ul>
			    </div>
			    <div id="cta">
			        <asp:PlaceHolder runat="server" ID="VideoPlaceholder">
				        <a href="<%= VideoLink %>" class="btn btn-cta-big video-btn lightbox clearfix">
				            <span><asp:Literal runat="server" ID="VideoThumbnail"/><span class="video ir"><%= Translate.Text("video") %></span></span>
				            <strong><asp:Literal runat="server" ID="VideoHeading"/></strong>
				        </a>
                    </asp:PlaceHolder>
			        <asp:PlaceHolder runat="server" ID="FilePlaceholder">
				        <a href="<%= FileLink %>" class="btn btn-cta-big video-btn clearfix">
				            <span><asp:Literal runat="server" ID="FileThumbnail"/></span>
				            <strong><asp:Literal runat="server" ID="FileHeading"/></strong>
				        </a>
                    </asp:PlaceHolder>
                    <asp:Literal ID="ltrLinkButton" runat="server"></asp:Literal>
			    </div>
		    </div>
	
		    <ul class="panels">
			    <li id="panelNOJS" class="panel">
			        <asp:Literal runat="server" ID="NonJsImage" />
			    </li>
                
                <asp:Repeater runat="server" ID="PanelList" OnItemDataBound="PanelListItemDataBound">
                    
                    <ItemTemplate>
                        
                        <li id="panel<%# Container.ItemIndex %>" class="panel">
                            <%# GetFirstImage(Container.DataItem as FacilityModuleItem) %>

                            <div class="side_panel">
                	            <div id="scroll_container-0" class="scroll_container">
                		            <div class="customScrollBox">
                		                <div class="container">
			                	            <h1><%# (Container.DataItem as FacilityModuleItem).Heading.Rendered %></h1>
				                            <%# (Container.DataItem as FacilityModuleItem).Summary.Rendered %>
				                            <asp:PlaceHolder runat="server" ID="SubheadingPlaceHolder"><h2><%# (Container.DataItem as FacilityModuleItem).Subheading.Rendered %></h2></asp:PlaceHolder>
                                            <asp:ListView runat="server" ID="GalleryList" ItemPlaceholderID="GalleryListPh">
                                                <LayoutTemplate>
                                                    <ul class="gallery thumbs">
                                                        <asp:PlaceHolder runat="server" ID="GalleryListPh"></asp:PlaceHolder>
                                                    </ul>
                                                </LayoutTemplate>
                                                
                                                <ItemTemplate>
                                                    <li>
                                                        <a href="<%# GetGalleryImageLink(Container.DataItem as MediaItem, Container.DataItemIndex) %>" <%# GetGalleryImageClassOrTarget(Container.DataItemIndex) %>>
                                                            <img src="<%# Sitecore.StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(Container.DataItem as MediaItem)) %>?w=90&h=60&c=90x60" width="90" height="60" alt="<%# (Container.DataItem as MediaItem).Alt %>" />
                                                        </a>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
				                            <div class="also">
					                            <h2><%# Translate.Text("See also") %></h2>
					                            <ul class="panel-list">
					                                <asp:Repeater runat="server" ID="RelatedList">
                                                        <ItemTemplate>
                                                            <li>
                                                                <a href="#panel<%# Container.ItemIndex %>">
                                                                    <%# (Container.DataItem as FacilityModuleItem).Heading.Rendered %>
                                                                </a>
                                                            </li>
                                                        </ItemTemplate>
					                                </asp:Repeater>
					                            </ul>
					        	            </div>
			        		            </div><!--end container-->
			        	
				        	            <div class="dragger_container">
				        	                <div class="dragger">&nbsp;</div>
				        	            </div>
				        	
			        	            </div><!--end customScrollBox-->
		        	            </div><!--end scroll_container-->
	        	            </div><!--end side_panel-->
			            </li>
                        

                    </ItemTemplate>
                </asp:Repeater>

			    
		    </ul>
	    </div>