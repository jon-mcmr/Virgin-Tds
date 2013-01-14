
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndoorIFrame.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.indoortriathlon.IndoorIFrame" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<%@ Import Namespace="Sitecore.Web" %>
<div id="content">
   <div class="layout-block indoor-tri">                
               
       <div class="header_width_social clearfix">
	        <h2><%= currentItem.Heading.Rendered%></h2>
	                
	        <!--Social-->
           <% if (showSocial == true)
               {%>
	        <ul class="social-info clearfix">
	            <li class="social-twitter"><span><%= SocialMediaHelper.TweetButtonForUrl(pageUrl) %></span></li>
               <li class="social-facebook"><span><%= SocialMediaHelper.LikeButtonIFrame(pageUrl)%></span></li>		
	            <li class="social-googleplus"><span><%= SocialMediaHelper.GooglePlusButton %></span></li>
	        </ul>
           <%  }%>
	    </div>
               
       <%= currentItem.Body.Text%>

       <%= Iframe %>

       <script>
           window.onload = function () {
               function displayMessage(evt) {
                   var message;
                   if (evt.origin !== "http://www.racetimingsystems.net") {
                   }
                   else {
                       //message = "I got " + evt.data + " from " + evt.origin;
                       $('#Iframe').attr('height', evt.data);
                   }
               }

               if (window.addEventListener) {
                   // For standards-compliant web browsers
                   window.addEventListener("message", displayMessage, false);
               }
               else {
                   window.attachEvent("onmessage", displayMessage);
               }
           };
       </script>

   </div>
           
</div> <!-- /content -->
