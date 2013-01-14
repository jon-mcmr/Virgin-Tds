<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileError.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileError" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
		<div class="content">						
			<h1 class="fourofour icon"><%= ErrorItm.PageSummary.NavigationTitle.Text %></h1>
            <div class="subtitle">
                <%= ErrorItm.Body.Text %>		
            </div>
			<div class="button_group clearfix">
				<a href="<%= clubFinder.Url %>" class="button half inline_horizontal_image club_finder first"><span><strong><%= Translate.Text("Club finder")%></strong></span></a>
				<a href="<%= clubFinder.Url + "?action=timetables" %>" class="button half inline_horizontal_image timetables"><span><strong><%= Translate.Text("Timetables")%></strong></span></a>
				<a href="<%= enqForm.Url + "?sc_trk=enq" %>" class="button red inline_horizontal_image membership_enquiry"><span><strong><%= Translate.Text("Membership Enquiry")%></strong></span></a>
			</div>
			<div class="subtitle">
	            <p><%= Translate.Text("None of the above? In that case, maybe it's better if you head to the") + " "%><a href="/"><%= Translate.Text("homepage")%></a></p>
			</div>
		</div>
