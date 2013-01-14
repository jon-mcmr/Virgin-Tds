<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPageLayout.aspx.cs"
    Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingPageLayout" %>

<%@ OutputCache Location="None" VaryByParam="none" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<%@ Register Src="LandingHeader.ascx" TagName="LandingHeader" TagPrefix="uc1" %>
<%@ Register Src="LandingFooter.ascx" TagName="LandingFooter" TagPrefix="uc2" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html lang="en" class="ie ie6 nojs"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="ie ie7 nojs"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="ie ie8 nojs"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="ie ie9 nojs"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<html lang="en" class="nojs" itemscope itemtype="http://schema.org/Event">
<!--<![endif]-->
<head runat="server" id="HTMLHead">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9,chrome=1">
    <meta name="viewport" content="width=1024">
    <title></title>
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false&amp;libraries=places&amp;region=GB&amp;language=en-GB&amp;radius=1000"></script>
    <link rel="shortcut icon" href="/virginactive/images/favicon.ico">
    <link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">
     <link rel="stylesheet" href="../../../virginactive/css/q1/main.css"/>
    <link rel="stylesheet" href="../../../virginactive/css/fonts.css" />
    <!--[if lt IE 8]><link rel="stylesheet" href="/virginactive/css/ie.css" media="screen" />	<![endif]-->
    <script src="/virginactive/scripts/modernizr_custom.js"></script>
    <sc:VisitorIdentification ID="VisitorIdentification1" runat="server" />
	
</head>
<body runat="server" id="BodyTag">
    <form id="form1" runat="server">
    <asp:PlaceHolder ID="RibbonPh" runat="server" />
   
        <uc1:LandingHeader ID="LandingHeader1" runat="server" />
        
        <div id="contentDiv" runat="server" class="content">
        <sc:Placeholder ID="scPhPanels" runat="server" Key="LandingPanels" />
        </div>
        
        <uc2:LandingFooter ID="LandingFooter1" runat="server" />
  
    </form>

     <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script>        window.jQuery || document.write('<script src="virginactive/scripts/vendor/jquery.min.js"><\/script>')</script>
    
    <script src="/virginactive/scripts/vendor/jquery.colorbox.js"></script>
    <script src="/virginactive/scripts/vendor/chosen.js"></script>
    <script src="/virginactive/scripts/campaigns/q1.js"></script>
</body>
</html>
