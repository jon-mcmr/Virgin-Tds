<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeLayout.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.presentationstructures.MicrositeLayout" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

<div id="wrapper">
    <sc:Placeholder ID="phHeader" runat="server" Key="header" />
    <sc:Placeholder ID="phCarousel" runat="server" Key="carousel" />
    <sc:Placeholder ID="phCentre" runat="server" Key="centre" />
    <sc:Placeholder ID="phFooter" runat="server" Key="footer" />
</div>

