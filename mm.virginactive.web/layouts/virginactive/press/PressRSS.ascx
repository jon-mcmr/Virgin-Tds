<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressRSS.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.press.PressRSS" %>
<%@ Import Namespace="Sitecore.Links" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

	                    <div class="subscribe-panel title-border">
	                        <a href="<%= LinkManager.GetItemUrl(feed) %>" class="rss-feed" target="_blank"><%= Translate.Text("Subscribe to RSS Feed")%></a>
	                    </div>
