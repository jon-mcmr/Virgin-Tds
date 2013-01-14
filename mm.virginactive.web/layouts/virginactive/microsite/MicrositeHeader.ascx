<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeHeader.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeHeader" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<div class="header">
		<h1 id="logo"><a href="<%= micrositeUrl %>" class="ir" style="background-image:url('<%= micrositeLogo %>')"><%= micrositeHeading %></a></h1>
        	<div id="nav-wrap">
				<div class="nav">
				    <asp:ListView runat="server" ID="NavigationListView" ItemPlaceholderID="NavigationPlaceHolder">
				        <LayoutTemplate>
				            <ul>
				                <asp:PlaceHolder runat="server" ID="NavigationPlaceHolder" />
				            </ul>
				        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <a href="<%# ((PageSummaryItem)Container.DataItem).Url %>"<%# Sitecore.Context.Item.ID == ((PageSummaryItem)Container.DataItem).ID ? "class=\"active\"" : "" %>>
                                    <%# ((PageSummaryItem)Container.DataItem).NavigationTitle.Rendered.ToUpperInvariant() %>
                                </a>
                            </li>
                        </ItemTemplate>
				    </asp:ListView>
				</div>
			</div>
	</div>
