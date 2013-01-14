<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeVideo.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeVideo" %>
          
<div id="lightbox" class="iframed">
    <h1><asp:Literal runat="server" ID="VideoHeading" /></h1>
    <asp:Literal runat="server" ID="VideoIntro" />
	<iframe width="640" height="400" src="<%= promoItem.Videolink.Rendered %>" frameborder="0" allowfullscreen=""></iframe>
</div>


