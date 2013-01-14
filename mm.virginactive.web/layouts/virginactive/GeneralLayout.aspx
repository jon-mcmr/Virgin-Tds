<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralLayout.aspx.cs" Inherits="mm.virginactive.web.layouts.virginactive.GeneralLayout" Async="true"  EnableEventValidation="true" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>

<%@ OutputCache Location="None" VaryByParam="none" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="ie ie6 nojs"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="ie ie7 nojs"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="ie ie8 nojs"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="ie ie9 nojs"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html lang="en" class="nojs"> <!--<![endif]-->
<head runat="server" id="HTMLHead">
    <meta charset="utf-8">

  	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9,chrome=1">  	
  	<meta name="viewport" content="width=1024px">
    <title></title>
	<link rel="shortcut icon" href="/virginactive/images/favicon.ico">
  	<link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">

	<!--[if lt IE 8]><link rel="stylesheet" href="/virginactive/css/ie.css" media="screen" />	<![endif]-->

	<script src="/virginactive/scripts/modernizr_custom.js"></script>
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false&amp;libraries=places&amp;region=GB&amp;language=en-GB&amp;radius=1000"></script>
    <script src='//d3c3cq33003psk.cloudfront.net/opentag-32825-405843.js' async defer></script>
    <sc:VisitorIdentification ID="VisitorIdentification1" runat="server"/>
</head>
<body runat="server" id="BodyTag">
<form id="form1" runat="server">
    
    <ul id="skiplinks">
		<li runat="server"><a href="#main-nav" id="skipto-mainnav"><%= Translate.Text("Skip to main navigation")%></a></li>
		<li runat="server"><a href="#search" id="skipto-search"><%= Translate.Text("Skip to search")%></a></li>
		<li runat="server"><a href="#content" id="skipto-content"><%= Translate.Text("Skip to main content")%></a></li>
    </ul>
    <asp:PlaceHolder ID="RibbonPh" runat="server" />
    <div id="reveal">
		<div class="container">
            <sc:Placeholder ID="plhPromotionEventWidget" runat="server" Key="promotionevent" />
		</div>
	</div>

	<div id="wrapper">
        <sc:Placeholder ID="HeaderRegion" runat="server" Key="header" />
        <sc:Placeholder ID="Placeholder1" runat="server" Key="content" />
        <sc:Placeholder ID="FooterRegion" runat="server" Key="footer" />
    </div>
</form>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>
    <script>window.jQuery || document.write("<script src='/virginactive/scripts/jquery-1.7.1.min.js'>\x3C/script>")</script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/jquery-ui.js"></script>
    
    <asp:placeholder ID="ScriptPh" runat="server" />
    <asp:Placeholder ID="randPh" runat="server">
    
    </asp:Placeholder>

    <!--[if lt IE 7 ]>
	    <script src="/virginactive/scripts/dd_belatedpng.js"></script>
	    <script>DD_belatedPNG.fix(".png_bg"); // Fix any <img> or .png_bg bg-images. Also, please read goo.gl/mZiyb </script>
    <![endif]-->
    <a href="#top" id="toTop" style="display: inline;" class="hidden"><span id="toTopHover"></span>To Top</a>


</body>
</html>