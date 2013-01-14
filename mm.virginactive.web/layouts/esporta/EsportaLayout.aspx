<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EsportaLayout.aspx.cs" Inherits="mm.virginactive.web.layouts.esporta.EsportaLayout" %>
<!DOCTYPE html>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<!--[if lt IE 7 ]> <html class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <html class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <html class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <html class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html> <!--<![endif]-->
<head runat="server" id="HTMLHead">
  	<meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<title></title>
  	<meta name="author" content="">
  	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	
	<link rel="shortcut icon" href="/virginactive/images/favicon-esporta.ico">
  	<link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">
	<link rel="stylesheet" href="/virginactive/css/esporta.css" type="text/css" />
	<link rel="stylesheet" href="/virginactive/css/fonts.css" type="text/css" />
    <link rel="stylesheet" href="/virginactive/css/cookies.css" />
    <script src="/virginactive/scripts/modernizr_custom.js"></script>
</head>  
<body runat="server" id="BodyTag" class="portal">
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="RibbonPh" runat="server" />
        <div id="esporta-wrap">
            <div id="esporta">
                <div id="esporta-inner">
       		        <h1>Welcome to the <span>Virgin Active</span> family</h1>
                    <div id="content-bg">
            	        <div id="content">
                	        <h2><span>Esporta Health Clubs</span> are now part of Virgin Active</h2>
                            <p>As a result we will be looking to put on more internal and intra club events as well as continuing to strive to deliver great service and outstanding value for money to all of our members. Being part of a bigger network will also give Virgin Active the opportunity to provide nationwide brand partnerships to our members.</p>
                            <a href="http://virginactive.co.uk" class="btn">Find your nearest club</a>
                        </div>
                    </div>
        	        <p id="footer"><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%></p>
                </div>
            </div>
        </div>
    </form>
        <asp:placeholder ID="ScriptPh" runat="server" />
        
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>
        <script>window.jQuery || document.write("<script src='/virginactive/scripts/jquery-1.7.1.min.js'>\x3C/script>")</script>
        <script>
        	function are_cookies_enabled()
        	{
        		var cookieEnabled = (navigator.cookieEnabled) ? true : false;
        	
        		if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled)
        		{ 
        			document.cookie="testcookie";
        			cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
        		}
        		return (cookieEnabled);
        	}
        	
        	if($('#cookie-ribbon').length){
        		if(!are_cookies_enabled()){
        			$('#cookie-ribbon').remove();
        		}
        	}
        </script>
</body>
</html>

