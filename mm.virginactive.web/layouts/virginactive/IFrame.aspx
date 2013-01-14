<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IFrame.aspx.cs" Inherits="mm.virginactive.web.layouts.virginactive.IFrame" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html id="html" lang="en" class="ie ie6 nojs"> <![endif]-->
<!--[if IE 7 ]>    <html id="html" lang="en" class="ie ie7 nojs"> <![endif]-->
<!--[if IE 8 ]>    <html id="html" lang="en" class="ie ie8 nojs"> <![endif]-->
<!--[if IE 9 ]>    <html id="html" lang="en" class="ie ie9 nojs"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html id="html" lang="en" class="nojs"> <!--<![endif]-->
<head runat="server" id="HTMLHead">
    <title></title>
  	<meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <script>
        document.getElementById("html").className = document.getElementById("html").className.replace(/(?:^|\s)nojs(?!\S)/, "");
    </script>
    <link href='/virginactive/css/microsite.css' rel='stylesheet' type='text/css' media="screen" />
    <script src='/virginactive/scripts/modernizr_custom.js'></script>
    
</head>
<body class="iframed">
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="RibbonPh" runat="server" />

                  <sc:Placeholder runat="server" Key="content"/>
       
           
    </form>
    
    <script>
    		addLoadEvent(function(){
    			if (top != self) {
    				$('html,body').addClass('iframed');
    				
    				var id = document.location.hash.replace("#","");
    				var html_required = $('<div>').append($(document.location.hash).clone()).html();
    			
    				$('body').html(html_required);
    				var width = $(document.location.hash).width();
    				var height = $(document.location.hash).height();
    				
    				parent.$().colorbox.resize({innerWidth:width,innerHeight:height});
    				parent.$('.cboxIframe').css('height',height);
    				
    				$(document).keydown(function(){
    					if (event.which != 27) {
    						return;
    					}
    					
    					parent.$.fn.colorbox.close();
    				});
    
    			}
    		});
    	</script>
</body>
</html>
