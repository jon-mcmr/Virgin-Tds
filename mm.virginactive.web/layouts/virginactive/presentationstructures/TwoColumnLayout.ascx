<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwoColumnLayout.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.presentationstructures.TwoColumnLayout" %>
            <div id="content" class="layout">                
                <div class="column-wide">
                    <sc:Placeholder ID="Placeholder1" runat="server" Key="left" />
                </div>
                
                <aside class="column-240">
                    <sc:Placeholder ID="Placeholder2" runat="server" Key="right" />
                </aside>
            </div> <!-- /content -->
       
