<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileLayout.aspx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileLayout" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<!DOCTYPE html>
<html class="no-js">
	<head runat="server" id="HTMLHead">
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		<title></title>
		<meta name="viewport" content="width=device-width,initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
		<meta name="apple-mobile-web-app-capable" content="yes">
		
		<link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">

		<link rel="stylesheet" href="/virginactive_mobile/css/fonts.css">
		<link rel="stylesheet" href="/virginactive_mobile/css/main.css">
		<script src="/virginactive_mobile/js/vendor/modernizr-2.6.2.min.js"></script>
         <script src='//d3c3cq33003psk.cloudfront.net/opentag-32825-405843.js' async defer></script>
	</head>
<body runat="server" id="BodyTag">
    <form id="form1" runat="server">
		<!-- Homepage header -->
		<div class="header">
            <asp:Literal ID="ltrHeaderText" runat="server"></asp:Literal>
		</div>
        <sc:Placeholder ID="Placeholder1" runat="server" Key="content" />         
		<div class="footer">
			<div class="social clearfix">
				<ul class="clearfix">
					<li><a href="<%= youTubeUrl%>" class="youtube ir"><%= Translate.Text("YouTube")%></a></li>
					<li><a href="<%= facebookUrl%>" class="facebook ir"><%= Translate.Text("Facebook")%></a></li>
					<li><a href="<%= twitterUrl%>" class="twitter ir"><%= Translate.Text("Twitter")%></a></li>
				</ul>
				<a href="<%= HomeMainSiteUrl %>" class="main_site"><%= Translate.Text("Go to main website")%></a>
			</div>		
			<div class="legals">
				<ul>
					<li><a href="<%= privacyUrl %>"><%= Translate.Text("Privacy Policy")%></a></li>
                    <li><a href="<%= termsAndConditionsUrl %>"><%= Translate.Text("Terms & Conditions")%></a></li>
                    <li><a href="<%= cookiesUrl %>"><%= Translate.Text("Cookies")%></a></li>
				</ul>
				<p class="copy">
                    <%= Translate.Text("© 2012 Virgin Active")%>
				</p>
			</div>
		</div>

		<script src="/virginactive_mobile/js/vendor/jquery-1.8.1.min.js"></script>
		<script type="text/javascript">
			var addToHomeConfig = {
				message: "Add this mobile site on your phone: Tap on the arrow and then <strong>'Add to Home Screen'</strong>",
				touchIcon: true,
				lifespan: 15000,
				expire: 20
			};	
		</script>
		<script src="/virginactive_mobile/js/vendor/add2home.js"></script>
		<script src="/virginactive_mobile/js/mobile.js"></script>
    </form>
</body>
</html>
