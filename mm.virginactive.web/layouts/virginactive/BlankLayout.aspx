<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlankLayout.aspx.cs" Inherits="mm.virginactive.web.layouts.virginactive.BlankLayout" Async="true" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html id="html" lang="en" class="ie ie6 nojs"> <![endif]-->
<!--[if IE 7 ]>    <html id="html" lang="en" class="ie ie7 nojs"> <![endif]-->
<!--[if IE 8 ]>    <html id="html" lang="en" class="ie ie8 nojs"> <![endif]-->
<!--[if IE 9 ]>    <html id="html" lang="en" class="ie ie9 nojs"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html id="html" lang="en" class="nojs"> <!--<![endif]-->
<head runat="server" id="HTMLHead">
    <title id="title" runat="server"></title>
  	<meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <script>
        document.getElementById("html").className = document.getElementById("html").className.replace(/(?:^|\s)nojs(?!\S)/, "");
    </script>
    <script src='/virginactive/scripts/modernizr_custom.js'></script>
    
    <link rel="stylesheet" href="/virginactive/css/cookies.css" />
    <script>
	    function addLoadEvent(func) {
	      var oldonload = window.onload;
	      if (typeof window.onload != 'function') {
	        window.onload = func;
	      } else {
	        window.onload = function() {
	          if (oldonload) {
	            oldonload();
	          }
	          func();
	        }
	      }
	    }
    </script>
</head>
<body runat="server" id="BodyTag">
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="RibbonPh" runat="server" />
        <sc:Placeholder Key="content" runat="server" />
            <asp:placeholder ID="phOverlayItems" runat="server" />
    </form>
    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>
    <script>window.jQuery || document.write("<script src='/virginactive/scripts/jquery-1.7.1.min.js'>\x3C/script>"); </script>
    <asp:placeholder ID="ScriptPh" runat="server" />   
    
    <script>
    	addLoadEvent(function(){
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
	    	
	    	if(!are_cookies_enabled()){
	    		$('#cookie-ribbon,#hdnCookieShowing').hide();
	    		$('body').removeClass('cookie-show');
	    	}
	    });
    </script>
 
</body>
</html>