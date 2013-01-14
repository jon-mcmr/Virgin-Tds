<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegionSuggestions.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.RegionSuggestions" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>

<asp:ListView ID="SuggestionList" runat="server">
    <LayoutTemplate>
				<ul class="tab">
                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />     
                </ul>   
    </LayoutTemplate>

    <ItemTemplate>
    <li>
        <a href="<%# String.Format(@"{3}?lat={0}&lng={1}&searchloc={2}",(Container.DataItem as SearchTermMatchItem).Lat.Raw, (Container.DataItem as SearchTermMatchItem).Lng.Raw, (Container.DataItem as SearchTermMatchItem).Matchtitle.Raw, baseUrl) %>"<%# (Container.DataItem as SearchTermMatchItem).IsCurrent ? @" class=""active""" : "" %>><%# (Container.DataItem as SearchTermMatchItem).Matchtitle.Raw %></a>
    </li>
    </ItemTemplate>
</asp:ListView>