<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubContactDetails.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.ClubContactDetails" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
                        <input type="hidden" id="phErrorMessage" runat="server" class="ph-error-message" value="''" />
                        <% if (club != null)
                           {%>
                        <div class="contact-address">
		                    <h5><%= club.Clubname.Rendered%></h5>
		                    <p><%= ContactAddress%></p>
	                    
                        <div class="contact-email-social-lrg">
		                    <div class="contact-phone">
			                    <span><%= club.Memberstelephonenumber.Rendered%></span>
		                    </div>
                            <div class="contact-msg">
			                     <span><a href="#sendmessage-jump" class="external"><%= Translate.Text("Message club") %></a></span>
		                    </div>
	                    </div>
                        </div>
                        
                
                        <% }
                           else
                           {%>
                           <img src="/virginactive/images/contact-us.jpg" alt="Select a club" />
                           <h2>Select your club so we can provide you with their contact details</h2>
                        <% }%>

