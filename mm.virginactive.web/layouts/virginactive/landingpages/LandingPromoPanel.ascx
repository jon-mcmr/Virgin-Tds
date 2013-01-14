<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingPromoPanel.ascx.cs"
    Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingPromoPanel" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<div class="section hero">
    <div class="container">
        <img src="<%= currentItem.BackgroundImage.MediaUrl %>" width="1200" height="471"
            alt="<%= currentItem.BackgroundImage.MediaItem.Alt %>" />
        <div class="row">
            <h1 class="span12">
                <asp:Literal ID="ltHeading" runat="server"></asp:Literal></h1>
            <p class="span12 subtitle">
                <asp:Literal ID="ltSubHeading" runat="server"></asp:Literal></p>
            <p class="span12">
                <a href="#lightbox" class="btn large lightbox cboxElement">
                    <asp:Literal ID="ltButtonText" runat="server"></asp:Literal></a></p>
        </div>
    </div>
</div>
<div class="section info_strip">
    <div class="container">
        <p>
            <asp:Literal ID="ltCallToAction" runat="server"></asp:Literal></p>
    </div>
</div>

