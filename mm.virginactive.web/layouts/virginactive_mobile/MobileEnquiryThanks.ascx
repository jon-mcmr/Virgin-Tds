<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileEnquiryThanks.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileEnquiryThanks" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

	       	<div class="content enquirythanks">
	       		<h1 class="icon"><%= Translate.Text("Thank you for your interest")%></h1>
	       		
	       		<hr class="divider" />

	       		<div class="subtitle">
	       			<p><%= Translate.Text("A team member from Virgin Active") %> <%= ClubName %> <%=Translate.Text("will call you shortly.")%></p>
	       		</div>

	       		<h2 class="title"><%= Translate.Text("DETAILS SUPPLIED")%></h2>
	       		<ul class="data_list">
	       			<li>
	       				<strong><%= Translate.Text("Preferred Club")%></strong>
                        <%= ClubName %>
	       			</li>
	       			<li>
	       				<strong><%= Translate.Text("Your name")%></strong>
                        <%= CustomerName%>
       				</li>
       				<li>
	       				<strong><%= Translate.Text("Email address")%></strong>
                        <%= EmailAddress%>
       				</li>
       				<li>
	       				<strong><%= Translate.Text("Contact number")%></strong>
                        <%= Telephone%>
       				</li>
       				<li>
                        <%= SubscribeMessage%>
	       			</li>
	       		</ul>
	       		
	       		<a href="<%= HomeUrl %>" class="anchor_button"><%= Translate.Text("Back to homepage")%></a>
	       		
	       		<hr class="divider" />
	       		
	       		<a href="<%= ClubHomeUrl %>" class="anchor_button"><%= Translate.Text("Go to") %> <%= ClubName %></a>
	       		
	       	</div>

