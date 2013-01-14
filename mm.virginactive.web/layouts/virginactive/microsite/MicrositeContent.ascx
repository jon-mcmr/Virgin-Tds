<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeContent.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeContent" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.ClubMicrosites" %>
<div id="content">

    <div class="section single-article">                                   
        <h1><%= new ContentItem(Sitecore.Context.Item).Heading.Rendered %></h1>

        <%= new ContentItem(Sitecore.Context.Item).Copy.Rendered %>
    </div>
</div>
