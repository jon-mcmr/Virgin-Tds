<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileEndOfCampaign.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.Campaigns.MobileEndOfCampaign" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

		<div class="content campaignfinished subtitle">
			
            <%= campaign.Endofcampaignbodytext.Rendered %>
		
			<div class="campaign_button_group clearfix">
				<a href="<%= ClubFinderUrl %>" class="button half inline_vertical_image club_finder first"><span><strong><%= Translate.Text("Club finder") %></strong></span></a>
				<a href="<%= TimetablesUrl %>" class="button half inline_vertical_image timetables"><span><strong><%= Translate.Text("Timetables") %></strong></span></a>
				<a href="<%= MembershipEnquiryUrl + "?sc_trk=enq"%>" class="button red inline_horizontal_image membership_enquiry"><span><strong><%= Translate.Text("Membership Enquiry") %></strong></span></a>
			</div>
			
		</div>
