<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeFooter.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeFooter" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites" %>
    
<div class="footer">
<a class="main-site" href="http://www.virginactive.co.uk">Main Site</a>
		<p class="copyright"><%= Translate.Text("© ##Year## Virgin Active")%> </p>
    <p class="opening_hours"><asp:Literal runat="server" ID="OpeningHoursLiteral" /></p>
		<ul id="footer-links">
		    <asp:ListView runat="server" ID="NavigationListView" ItemPlaceholderID="NavigationPlaceHolder">
				<LayoutTemplate>
				    <asp:PlaceHolder runat="server" ID="NavigationPlaceHolder" />
				</LayoutTemplate>
                <ItemTemplate>
                    <%# Container.DataItemIndex == 0 ? "" : " / " %>
                    <li>
                        <a href="<%# ((PageSummaryItem)Container.DataItem).Url %>">
                            <%# ((PageSummaryItem)Container.DataItem).NavigationTitle.Rendered %>
                        </a>
                    </li>
                </ItemTemplate>
			</asp:ListView>
            <li id="twitter"><a href="<%= TwitterUrl %>" target="_blank"><img src="/virginactive/images/microsites/<%= TwitterImage %>" alt="<%= Translate.Text("Follow us on Twitter") %>" /></a></li>
		</ul>
	</div>