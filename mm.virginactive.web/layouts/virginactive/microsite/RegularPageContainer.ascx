<%@ Control Language="c#" AutoEventWireup="true" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="page-content">
    <sc:Sublayout runat="server" Path="~/layouts/virginactive/microsite/MicrositeHeader.ascx" />
            
    <sc:Placeholder Key="content" runat="server" />

    <sc:Sublayout runat="server" Path="~/layouts/virginactive/microsite/MicrositeFooter.ascx" />
</div>