<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileLandingPagePromo.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive_mobile.landingpages.MobileLandingPagePromo" %>
     <%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>

<h1 class="campaignname"><%= currentItem.InnerItem.DisplayName %></h1>
<img src="<%= currentItem.BackgroundImage.MediaUrl %>" alt="<%= currentItem.BackgroundImage.MediaItem.Alt %>" class="max-width" />

<div class="info_strip">
	<p><strong><%= currentItem.Subheading.Rendered %></p>
</div>

<h1 class="icon"><%= Translate.Text("Club finder")%></h1>
			
<a href="<%= ClubResultsUrl %>" class="button geolocation current_location"><span><strong><%= Translate.Text("Near my current location")%></strong></span></a>

<div class="divider geolocation">
	<div class="text"><%= Translate.Text("or")%></div>
</div>

<a href="<%= ClubListUrl %>" class="button list"><span><strong><%= Translate.Text("Choose from a list")%></strong></span></a>
            
		