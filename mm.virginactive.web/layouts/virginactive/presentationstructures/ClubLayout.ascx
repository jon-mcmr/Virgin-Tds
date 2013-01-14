<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubLayout.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.presentationstructures.ClubLayout" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
        
        <div class="clubs_height_controller">
            <sc:Placeholder ID="ClubCarousel" Key="ClubCarousel" runat="server" />
            <div id="clubs" class="container">
                <h1><%= club.Clubname.Rendered %><asp:Literal ID="EsportaFlag" runat="server"></asp:Literal></h1>
                <sc:Placeholder Key="phSecondaryNav" runat="server" />
                <div id="content" class="layout<%=Panels %>">

                    <sc:Placeholder Key="centre" runat="server" />

                    <% if (!OneColumn)
                       { %>
                       <aside>
                    <% } %>

                    <sc:Placeholder key="RightColumn" runat="server" />

                    <% if (!OneColumn)
                       { %>
                       </aside>
                    <% } %>
                </div><!-- /content -->
            </div>
        </div>