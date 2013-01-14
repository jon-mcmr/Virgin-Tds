<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingHeader.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingHeader" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>	
 

<div class="header">
            <div class="container">
                <a id="logo" href="<%= HomeOrClubPageUrl %>"><img src="/virginactive/images/ppc/logo.png"></a>
                <a class="header_text" href="<%= HomeOrClubPageUrl %>"><%= Translate.Text("Virgin Active") %> <span><%= Translate.Text("Health Clubs") %></span></a>
                <a class="main_site" href="<%= HomeOrClubPageUrl %>">Main site</a>
            </div>
        </div>

        