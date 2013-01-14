<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileMicrositeHome.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileMicrositeHome" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="Sitecore.Data.Items" %>   
<%@ Import Namespace="Sitecore.Resources.Media" %> 
<%@ Import Namespace="mm.virginactive.common.Helpers" %>

		<div class="content club">			
			<h1 class="clubname"><%= ClubName %></h1>
			<div class="carousel">				
                    <asp:ListView ID="ImageList" ItemPlaceholderID="ImageListPh" runat="server">
                        <LayoutTemplate>
                            <ul class="clearfix items">
                                <asp:PlaceHolder ID="ImageListPh" runat="server" />
                            </ul>
                        </LayoutTemplate>

                        <ItemTemplate>                            
                             <li>
                                <img src="<%# Sitecore.StringUtil.EnsurePrefix('/', SitecoreHelper.GetOptimizedMediaUrl(Container.DataItem as MediaItem)) %>" alt="<%# (Container.DataItem as MediaItem).Alt %>" />
                               <%-- <img src="<%# Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(Container.DataItem as MediaItem)) %>" alt="<%# (Container.DataItem as MediaItem).Alt %>" />--%>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>  
			</div>

			<div class="button_group clearfix">
				<a href="<%= ClubTimetableUrl %>" class="button inline_horizontal_image timetables first half "><span><strong><%= Translate.Text("Timetables")%></strong></span></a>
				<a href="<%= ClubEnquiriesUrl %>" class="button inline_horizontal_image half membership_enquiry"><span><strong><%= Translate.Text("Enquiry")%></strong></span></a>
			</div>

			<div class="data_block contact_info">
				 <h2><%= Translate.Text("Club Information")%></h2>
				 <div class="data_content clearfix">
					<p class="address">
                        <asp:literal ID="Address" runat="server"></asp:literal>
                    </p>
                    <%= openingHours%>   
				 </div>     			 
				 <div class="data_footer clearfix">
					<ul>
						<li class="phone"><a href="tel:<%= club.Salestelephonenumber.Rendered%>"><%= club.Salestelephonenumber.Rendered%></a></li>
						<li class="contact_us"><a href="mailto:<%= club.Salesemail.Rendered%>"><%= Translate.Text("Contact Us")%></a></li>
					</ul>
				 </div>
			</div>
			
			<div class="data_block map">
				<h2><%= Translate.Text("Map")%></h2>
				<div class="data_content map_content" data-lng="<%= lng%>" data-lat="<%= lat%>">
					<p><%= Translate.Text("Loading map content")%></p>
				</div>
				<div class="data_footer clearfix">
					<a href="https://maps.google.co.uk/?q=<%= lat %>,<%= lng %>"><%= Translate.Text("View Map")%></a>
				</div>
			</div>          		
		</div>
