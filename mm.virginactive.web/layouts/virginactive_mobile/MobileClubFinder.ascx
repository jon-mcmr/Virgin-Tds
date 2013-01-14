<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileClubFinder.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileClubFinder" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>

		<div class="content clubfinder">
			<h1 class="icon"><%= Translate.Text("Club finder")%></h1>
						
			<a href="<%= ClubResultsUrl %>" class="button geolocation current_location"><span><strong><%= Translate.Text("Near my current location")%></strong></span></a>
			
			<div class="divider geolocation">
				<div class="text"><%= Translate.Text("or")%></div>
			</div>
			
			<a href="<%= ClubListUrl %>" class="button list"><span><strong><%= Translate.Text("Choose from a list")%></strong></span></a>
		
<%--			<div class="divider">
				<div class="text"><%= Translate.Text("or")%></div>
			</div>
			
    		<h2 class="title"><%= Translate.Text("Popular searches")%></h2>			
			<ul class="data_list">
                <li><a href="/gyms/gyms-in-belfast"><span class="arrow">Belfast</span></a></li>
                <li><a href="/gyms/gyms-in-birmingham"><span class="arrow">Birmingham</span></a></li>
                <li><a href="/gyms/gyms-in-brighton"><span class="arrow">Brighton</span></a></li>
                <li><a href="/gyms/gyms-in-bristol"><span class="arrow">Bristol</span></a></li>
                <li><a href="/gyms/gyms-in-cardiff"><span class="arrow">Cardiff</span></a></li>
                <li><a href="/gyms/gyms-in-coventry"><span class="arrow">Coventry</span></a></li>
                <li><a href="/gyms/gyms-in-croydon"><span class="arrow">Croydon</span></a></li>
                <li><a href="/gyms/gyms-in-derby"><span class="arrow">Derby</span></a></li>
                <li><a href="/gyms/gyms-in-edinburgh"><span class="arrow">Edinburgh</span></a></li>
                <li><a href="/gyms/gyms-in-glasgow"><span class="arrow">Glasgow</span></a></li>
                <li><a href="/gyms/gyms-in-leeds"><span class="arrow">Leeds</span></a></li>
                <li><a href="/gyms/gyms-in-london"><span class="arrow">London</span></a></li>
                <li><a href="/gyms/gyms-in-manchester"><span class="arrow">Manchester</span></a></li>
                <li><a href="/gyms/gyms-in-milton-keynes"><span class="arrow">Milton Keynes</span></a></li>
                <li><a href="/gyms/gyms-in-nottingham"><span class="arrow">Nottingham</span></a></li>
                <li><a href="/gyms/gyms-in-plymouth"><span class="arrow">Plymouth</span></a></li>
                <li><a href="/gyms/gyms-in-sheffield"><span class="arrow">Sheffield</span></a></li>
			</ul>--%>
		
		</div>
		
