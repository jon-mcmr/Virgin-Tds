<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubAnnouncement.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubAnnouncement" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
			<div class="announcement">
						<div class="intro-panel">
							<h2><%= announcement.Heading.Rendered%></h2>
							<h3><%= announcement.Subheading.Rendered%></h3>
						</div>
						 <div class="content-panel">
	                        <p><%= announcement.Bodytext.Text%></p>
                         </div>
                         <div class="footer-panel">
                            <%= announcement.Additionalinfo.Text%>
                        </div>
            </div>