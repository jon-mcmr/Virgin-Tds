<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteFacilityAndClass.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.sitemap.SiteFacilityAndClass" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
                <section>
				    <%= HeaderIsH2? "<h2>":"<h3>" %><a href="<%= facilityLanding.Url %>"><%= Translate.Text("Facilities & Classes")%></a><%= HeaderIsH2? "</h2>":"</h3>" %>
				    <h3><a href="<%= facilityLanding.Url %>"><%= facilityLanding.NavigationTitle.Raw %></a></h3>

                    <asp:PlaceHolder ID="FacilityPanels" runat="server" />
						
						
				    <h3><a href="<%=classLanding.Url %>"><%=classLanding.NavigationTitle.Raw%></a></h3>
                    <asp:PlaceHolder ID="ClassPanels" runat="server" />
				</section>