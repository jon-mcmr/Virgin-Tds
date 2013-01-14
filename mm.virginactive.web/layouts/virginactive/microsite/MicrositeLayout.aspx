<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MicrositeLayout.aspx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeLayout" %>

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
    <title></title>
  	<meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9,chrome=1"> 
    <script>
        document.getElementById("html").className = document.getElementById("html").className.replace(/(?:^|\s)nojs(?!\S)/, "");
    </script>
    <script src='/virginactive/scripts/modernizr_custom.js'></script>
    
    <meta name='viewport' content='width=1020'>
   <link href='/virginactive/css/fonts.css' rel='stylesheet' type='text/css' media='screen' />
   <link rel='apple-touch-icon' href='/virginactive/images/microsites/apple-touch-icon.png' />                    
   <link rel='shortcut icon' href='/virginactive/images/microsites/favicon.ico'>
    <link href='/virginactive/css/microsite.css' rel='stylesheet' type='text/css' media="screen" />
     <link href='/virginactive/css/microsite_print.css' rel='stylesheet' type='text/css' media="print" />
     <script src='http://maps.googleapis.com/maps/api/js?sensor=false&amp;libraries=places&amp;region=GB&amp;language=en-GB&amp;radius=1000'></script>

    <link rel="stylesheet" href="/virginactive/css/cookies.css" />
    
    <script>
        function addLoadEvent(func) {
            var oldonload = window.onload;
            if (typeof window.onload != 'function') {
                window.onload = func;
            } else {
                window.onload = function () {
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

        <div class="site_container">
            <sc:Placeholder runat="server" Key="container"/>
        </div>
           
    </form>
    



    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>
    <script>window.jQuery || document.write("<script src='/virginactive/scripts/jquery-1.7.1.min.js'>\x3C/script>"); </script>
    <asp:placeholder ID="ScriptPh" runat="server" />   
    
    <script src='/virginactive/scripts/microsites/supersized.3.2.6.js' type='text/javascript'></script>
    <script src='/virginactive/scripts/microsites/plugins.js' type='text/javascript'></script>
    <script src='/virginactive/scripts/microsites/jquery-ui.min.js'  type='text/javascript'></script>
    <script src='/virginactive/scripts/microsites/jquery.mCustomScrollbar.js'  type='text/javascript'></script>
    <script src='/virginactive/scripts/microsites/jquery.mousewheel.min.js'  type='text/javascript'></script>
    <script src='/virginactive/scripts/microsites/jquery.easing.1.3.js'  type='text/javascript'></script>
    <script src='/virginactive/scripts/microsites/script.js' type='text/javascript'></script>

    <script>
        addLoadEvent(function () {
            function are_cookies_enabled() {
                var cookieEnabled = (navigator.cookieEnabled) ? true : false;

                if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
                    document.cookie = "testcookie";
                    cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
                }
                return (cookieEnabled);
            }

            if (!are_cookies_enabled()) {
                $('#cookie-ribbon,#hdnCookieShowing').hide();
                $('body').removeClass('cookie-show');
            }
        });
    </script>
 
</body>
</html>