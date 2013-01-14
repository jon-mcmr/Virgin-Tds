<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CentralLayout.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.presentationstructures.CentralLayout" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
            <div class="container clearfix"> <!-- This will need to be fixed!-->
                <asp:PlaceHolder runat="server" Visible="false" ID="PageEditorControls">
                    <div>
                        <strong>Current page navigation title: </strong><%= contextTitle.NavigationTitle.Rendered%> <br />
                        <strong>Page Description: </strong><%= contextTitle.NavigationTitle.Rendered%>
                    </div>
                </asp:PlaceHolder>
				<h1><asp:literal ID="layoutHeader" runat="server"></asp:literal></h1>
                <sc:Placeholder Key="phSecondaryNav" runat="server" />

                <sc:Placeholder ID="Placeholder1" runat="server" Key="centre" />
      
            </div>            

    