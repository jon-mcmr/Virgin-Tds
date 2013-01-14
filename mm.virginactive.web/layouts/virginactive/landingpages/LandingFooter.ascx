<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingFooter.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingFooter" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>	
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<div class="footer">
            <div class="container">
                <p class="pull-left"><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%>
              <%= footerLinks %> </p>
                <ul class="social pull-right">
                    <li class="pull-left"><a class="youtube" href="<%= Settings.YouTubeLinkUrl %>">YouTube</a></li>
                    <li class="pull-left"><a class="facebook" href="<%= Settings.FacebookLinkUrl %>">FaceBook</a></li>
                    <li class="pull-left"><a class="twit" href="<%= Settings.TwitterLinkUrl %>">Twitter</a></li>
                </ul>
            </div>
        </div>


 