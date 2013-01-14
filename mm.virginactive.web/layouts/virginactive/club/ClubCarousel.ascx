<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubCarousel.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubCarousel" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="Sitecore.Data.Items" %>   
<%@ Import Namespace="Sitecore.Resources.Media" %>     
        <asp:PlaceHolder ID="ClubImagePanel" runat="server" Visible="false">
        <div id="carousel-wrap">
            
            <ul id="carousel"> 
                <asp:ListView ID="ImageList" ItemPlaceholderID="ImageListPh" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ImageListPh" runat="server" />
                    </LayoutTemplate>

                    <ItemTemplate>                            
                         <li><img src="<%# Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(Container.DataItem as MediaItem)) %>" alt="<%# (Container.DataItem as MediaItem).Alt %>" /></li>
                    </ItemTemplate>
                </asp:ListView>               
            </ul>
            <p id="view-photos" class="view-photos"><a href="#" data-viewphotos="<%= Translate.Text("View Photos")%>" data-closegallery="<%= Translate.Text("Close Gallery")%>"><%= Translate.Text("Close Gallery")%></a></p>
            <!--ul id="carousel-thumbs">
                <asp:ListView ID="ThumbList" runat="server">
                    <LayoutTemplate>
                    <ul id="carousel-thumbs"> 
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </ul>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <li><a href="#"><img src="<%# Sitecore.StringUtil.EnsurePrefix('/', (Container.DataItem as MediaItem).InnerItem.Children["110x70"].Fields["Url"].Value) %>" alt="<%# (Container.DataItem as MediaItem).Alt %>" /></a></li>                        
                    </ItemTemplate>
                </asp:ListView>
            </ul-->
        </div>
        </asp:PlaceHolder>