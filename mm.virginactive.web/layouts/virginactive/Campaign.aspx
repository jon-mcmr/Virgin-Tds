<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Campaign.aspx.cs" Inherits="mm.virginactive.web.layouts.virginactive.Campaign" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <html class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <html class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <html class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html> <!--<![endif]-->
<head runat="server" >
  	<meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<title></title>
  	<meta name="author" content="">
  	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	
	<link rel="shortcut icon" href="/virginactive/images/favicon.ico">
  	<link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">
	<link rel="stylesheet" href="/virginactive/css/campaign.css" type="text/css" />
	<link rel="stylesheet" href="/virginactive/css/fonts.css" type="text/css" />	
	<link rel="stylesheet" href="/virginactive/css/cookies.css" />
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.js"></script>
	<!--[if lt IE 9]><script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
	<!--[if lt IE 8]><link rel="stylesheet" href="/virginactive/css/ie.css" media="screen" type="text/css" />	<![endif]-->
</head>
    <body runat="server" id="BodyTag">
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="RibbonPh" runat="server" />
    <div id="campaign">
    		<div id="wrapper">
			
            <div id="content">
				<section id="panel-left" style="">
					<div class="container">
						<header>
							<a href="/"><img src="/virginactive/images/campaigns/logo.png" alt="Virgin Active Health Clubs" /></a>
						</header>		
						<h1><%= campaign.Heading.Raw %></h1>
                        
						<h2><a href="<%= campaign.Actionlink.Url %>" class="cta-book"><%= campaign.Actiontext.Rendered %></a></h2>
						<%= campaign.Bodytext.Text %>

						<p class="btn"><a href="<%= campaign.Buttonlink.Url %>" class="btn btn-cta-xl"><%= campaign.Buttontext.Rendered %></a></p>
						<footer>
							<%= campaign.Footerimage.Rendered %>
						</footer>
					</div>					
				</section>
				
				<section id="panel-right">
					<%= campaign.Sideheroimage.Rendered %>
					<div class="ribbon-r"></div>
				</section>
						
            </div> <!-- /content -->
			
			<footer>
				<div class="container">
					<ul>
						<li><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%></li>
                        <li><a href="<%= privacy.Url %>#tcjump"><%= privacy.NavigationTitle.Raw %></a></li>
					</ul>
				</div>	
		    </footer>
		</div>
	

	<!--[if lt IE 7 ]>
    	<script src="/virginactive/scripts/libs/dd_belatedpng.js"></script>
    	<script>DD_belatedPNG.fix("img, .png_bg"); // Fix any <img> or .png_bg bg-images. Also, please read goo.gl/mZiyb </script>
  	<![endif]-->
	
    </div>
    </form> 
    <asp:placeholder ID="ScriptPh" runat="server" />
</body>
</html>
