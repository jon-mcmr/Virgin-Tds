<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeVideoPage.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeVideoPage" %>

          
<div id="content">
    <div class="lightbox-container">
        <h1><asp:Literal runat="server" ID="VideoHeading" /></h1>
        <asp:Literal runat="server" ID="VideoIntro" />
	    <div class="video-wrapper"><iframe width="640" height="400" src="<%= promoItem.Videolink.Rendered %>" frameborder="0" allowfullscreen="" class="iframe"></iframe></div>
    </div>
</div>



    