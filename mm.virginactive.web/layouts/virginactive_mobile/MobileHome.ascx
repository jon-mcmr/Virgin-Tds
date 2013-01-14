<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileHome.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileHome" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>

		<div class="content">
			<div class="carousel">
                <ul class="clearfix items">
                    <asp:ListView ID="ImageList" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <img src="<%# Sitecore.StringUtil.EnsurePrefix('/', SitecoreHelper.GetOptimizedMediaUrl((Container.DataItem as ImageCarouselItem).Image)) %>" />
                                <%--<img src="<%# (Container.DataItem as ImageCarouselItem).Image.MediaUrl %>" />--%>
                                <p><%# (Container.DataItem as ImageCarouselItem).ImageCaption.Rendered %></p>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
			</div>
            <asp:PlaceHolder ID="phLastClubVisitedPrompt" runat="server" Visible="false">
			    <h2 class="title"><%= Translate.Text("Last visited club") %></h2>
			    <ul class="data_list short">
                    <asp:ListView ID="ClubList" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                        </LayoutTemplate>
                        <ItemTemplate>
				            <li><a href="<%# new PageSummaryItem((Container.DataItem as ClubItem).InnerItem).Url %>"><span class="arrow"><%# HtmlRemoval.StripTagsCharArray((Container.DataItem as ClubItem).Clubname.Rendered) %></span></a></li>
                        </ItemTemplate>
                    </asp:ListView>
			    </ul>
            </asp:PlaceHolder>
			
			<div class="button_group clearfix">
				<a href="<%= ClubFinderUrl %>" class="button half inline_vertical_image club_finder first"><span><strong><%= Translate.Text("Club finder") %></strong></span></a>
				<a href="<%= TimetablesUrl %>" class="button half inline_vertical_image timetables"><span><strong><%= Translate.Text("Timetables") %></strong></span></a>
				<a href="<%= MembershipEnquiryUrl + "?sc_trk=enq"%>" class="button red inline_horizontal_image membership_enquiry"><span><strong><%= Translate.Text("Membership Enquiry") %></strong></span></a>
			</div>
						
		</div>
