<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogTagCloud.ascx.cs" Inherits="Sitecore.Modules.Blog.Layouts.BlogTagCloud" %>
<%@ Import Namespace="System.Collections.Generic" %>

<asp:Panel ID="PanelTagCloud" runat="server" CssClass="tags-panel">
    <h4 class="aside-header"><sc:Text ID="titleTagcloud" runat="server" Field="titleTagcloud" /></h4>

    <div class="tags-cloud">
        <p>
        <asp:Repeater runat="server" ID="TagList">
            <ItemTemplate>
            <!--
                <a class="weight<%# GetTagWeightClass(((KeyValuePair<string, int>)Container.DataItem).Value) %>" href="<%# GetTagUrl(((KeyValuePair<string, int>)Container.DataItem).Key) %>">
                    <%# ((KeyValuePair<string, int>)Container.DataItem).Key %>
                </a> -->
                <span class="tag-name">
                <a href="<%# GetTagUrl(((KeyValuePair<string, int>)Container.DataItem).Key) %>">
                    <%# ((KeyValuePair<string, int>)Container.DataItem).Key %>
                </a>
                (<%# ((KeyValuePair<string, int>)Container.DataItem).Value %>)
                </span>
            </ItemTemplate>
        </asp:Repeater>
        </p>
    </div>
</asp:Panel>