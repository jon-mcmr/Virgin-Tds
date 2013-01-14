<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CampaignLayout.aspx.cs" Inherits="mm.virginactive.web.layouts.virginactive.CampaignLayout" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="ie ie6 nojs"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="ie ie7 nojs"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="ie ie8 nojs"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="ie ie9 nojs"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html lang="en" class="nojs"> <!--<![endif]-->
<head runat="server" id="HTMLHead">
  	<meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9,chrome=1">  
  	<meta name="author" content="">
  	<meta name="viewport" content="width=1020">
	   <link rel="shortcut icon" href="/virginactive/images/favicon.ico">
  	<link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">
	<link rel="stylesheet" href="/virginactive/css/fonts.css" type="text/css" />
	<link rel="stylesheet" href="/va_campaigns/css/campaign.css" type="text/css" />
	<link rel="stylesheet" href="/virginactive/css/cookies.css" />
    <title></title>
	<script src="/virginactive/scripts/modernizr_custom.js"></script>
    <!--Make sure to dynamically insert title, open tags and campaign type specific stylesheets-->
</head>
<body runat="server" id="BodyTag">
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="RibbonPh" runat="server" />
    <sc:Placeholder Key="content" runat="server" />

	<footer runat="server">
		<ul id="footer-list" >
			<li><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%></li>
            <li><a href="<%= privacy.Url %>"><%= privacy.NavigationTitle.Raw %></a></li>            
		</ul>
	</footer>

    </form>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.js"></script>
    <script>    window.jQuery || document.write("<script src='/virginactive/scripts/jquery-1.6.2.min.js'>\x3C/script>")</script>
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places&region=GB&language=en-GB&radius=1000"></script>
    <script src="/va_campaigns/js/plugins.js"></script>
    <script src="/va_campaigns/js/scripts.js"></script>    
    <asp:placeholder ID="ScriptPh" runat="server" />    
    </body>
</html>