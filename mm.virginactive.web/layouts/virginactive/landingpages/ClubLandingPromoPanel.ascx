<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubLandingPromoPanel.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.landingpages.ClubLandingPromoPanel" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
        <div class="section hero">
            <div class="container">
				<img src="<%= currentLanding.BackgroundImage.MediaUrl %>" width="1200" alt="<%= currentLanding.BackgroundImage.MediaItem.Alt %>" />
                <div class="row">
                    <h1 class="span12"><%= currentLanding.Heading.Rendered %></h1>
                    <p class="span12 subtitle"><%= currentLanding.Subheading.Rendered %></p>
                </div>
            </div>
        </div>
        <div class="section info_strip club">
            <div class="container">
                <h1><%= clubItem.Clubname.Rendered%></h1>
                <p><%= membershipTeaser%></p>
               <asp:PlaceHolder ID="phSearchAgain" Visible="false" runat="server"> <a href="#lightbox" class="btn gray search lightbox">Search Again</a></asp:PlaceHolder>
            </div>
        </div>
