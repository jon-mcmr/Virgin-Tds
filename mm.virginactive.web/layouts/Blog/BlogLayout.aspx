<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogLayout.aspx.cs" Inherits="Sitecore.Modules.Blog.Layouts.BlogLayout" %>
<!DOCTYPE html>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>

<!--[if lt IE 7 ]> <html lang="en" class="ie ie6 nojs"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="ie ie7 nojs"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="ie ie8 nojs"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="ie ie9 nojs"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html lang="en" class="nojs"> <!--<![endif]-->
  <head runat="server">
    <meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9,chrome=1"> 
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Your Health - Blog | Virgin Active</title>
    <link rel="shortcut icon" href="/virginactive/images/favicon.ico">
  	<link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">
	<!--[if lt IE 8]><link rel="stylesheet" href="/virginactive/css/ie.css" media="screen" />	<![endif]-->
    
	<script src="/virginactive/scripts/modernizr_custom.js"></script>
    <sc:VisitorIdentification ID="VisitorIdentification1" runat="server"/>
  </head>
  <body runat="server" id="BodyTag">
  <form method="post" runat="server" id="mainform">
  	<div id="skiptocontent" class="visuallyhidden"><a href="#searchbar">Skip to main content</a></div>
    <asp:PlaceHolder ID="RibbonPh" runat="server" />
    <div id="reveal">
		<div class="container">
            <sc:Placeholder ID="plhPromotionEventWidget" runat="server" Key="promotionevent" />
		</div>
	</div>
	<div id="wrapper">
            <sc:Placeholder ID="HeaderRegion" runat="server" Key="header" />
            <sc:placeholder ID="phContent" key="phContent" runat="server" />
            <sc:Placeholder ID="FooterRegion" runat="server" Key="footer" />
    </div>
    </form>
  	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.js"></script>
    <script>window.jQuery || document.write("<script src='/virginactive/scripts/jquery-1.6.2.min.js'>\x3C/script>")</script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/jquery-ui.js"></script>
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places&region=GB&language=en-GB&radius=1000"></script>
    <asp:placeholder ID="ScriptPh" runat="server" />
    <!--[if lt IE 7 ]>
	    <script src="/virginactive/scripts/dd_belatedpng.js"></script>
	    <script>DD_belatedPNG.fix(".png_bg"); // Fix any <img> or .png_bg bg-images. Also, please read goo.gl/mZiyb </script>
    <![endif]-->
  </body>
</html>
