<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageLinkWidget.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.controls.ImageLinkWidget" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
    

    <asp:Literal ID="LitSectionTag" runat="Server" />
       
        <div class="half-promo">
			 <asp:Literal ID="LitCssPanel" runat="server" />
             <asp:Literal ID="LitLinkPanel" runat="server" />
             
       <%--
            <div class="panel-arrow">
            
                <asp:Literal ID="LitArrowLink" runat="server" />
            
            </div>      --%>
        </div>
        <h3><asp:Literal ID="LitButtonLink" runat="server" /></h3>
        <div class="summary-text">
        <asp:Literal ID="LitBodyText" runat="server" />
        </div>


        <asp:Literal ID="LitSectionEndTag" runat="server" />

   