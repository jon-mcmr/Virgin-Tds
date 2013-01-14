<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GlobalLayout.aspx.cs" Inherits="mm.virginactive.web.layouts.global.GlobalLayout" %>
<!DOCTYPE html>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.Global" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace=" mm.virginactive.common.Constants.SitecoreConstants " %>

<!--[if lt IE 7 ]> <html class="ie6 no-js"> <![endif]-->
<!--[if IE 7 ]>    <html class="ie7 no-js"> <![endif]-->
<!--[if IE 8 ]>    <html class="ie8 no-js"> <![endif]-->
<!--[if IE 9 ]>    <html class="ie9 no-js"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <html class="no-js"> <!--<![endif]-->
<head runat="server" id="HTMLHead">
  	<meta charset="utf-8">
  	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<title>Global Portal | Virgin Active</title>
	<meta name="description" content="">
  	<meta name="author" content="">
  	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	
	<link rel="shortcut icon" href="/virginactive/images/favicon.ico">
  	<link rel="apple-touch-icon" href="/virginactive/images/apple-touch-icon.png">
	<link rel="stylesheet" href="/virginactive/css/portal.css" type="text/css" />
	<link rel="stylesheet" href="/virginactive/css/fonts.css" type="text/css" />	
	
	<script src="/virginactive/scripts/modernizr_custom.js"></script>
	<!--[if lt IE 8]><link rel="stylesheet" href="/virginactive/css/ie.css" media="screen" type="text/css" />	<![endif]-->
</head>
<body runat="server" id="BodyTag" class="portal">
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="RibbonPh" runat="server" />
		<div class="wrapper main">
            <div id="content" class="clearfix">
				<div class="header">
					<h1><a href="#"><img src="/virginactive/images/portal/logo.png" alt="Virgin Active Health Clubs" /></a></h1>			           
					<h2><%= currentItem.Subheading.Rendered%></h2> 
				</div>   
				
				<div class="section clearfix">
					<div class="africa">
						<h3>Africa</h3>   
                            <h4><asp:Hyperlink id="lnkSouthAfrica" runat="server" class="sa"></asp:Hyperlink></h4>
                            <div class="clubs" id="clubsSouthAfrica" runat="server">
                                <div class="dropdown"><%= ClubListSouthAfrica.Items.Count%> clubs</div>
                                <div class="list clearfix">
	                                <asp:ListView ID="ClubListSouthAfrica" runat="server">                                                    
	                                    <LayoutTemplate>
						                    <ul> 
	                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
						                    </ul>
	                                    </LayoutTemplate>
	                                    <ItemTemplate>
	                                        <li>
	                                            <a href="<%#(Container.DataItem as ClubLinkItem).Cluburl.Rendered %>" <%# (Container.DataItem as ClubLinkItem).IsFirst? "class=\"external club sa first\"" : "class=\"external club sa\"" %>><%#(Container.DataItem as ClubLinkItem).Clubname.Rendered%></a>
	                                        </li>
	                                    </ItemTemplate>
	                                </asp:ListView>   
                               	</div>                             
                            </div>
					</div>
					<div class="europe">
						<h3>Europe</h3>
						<ul>
							<li class="italy">
                                <h4><asp:Hyperlink id="lnkItaly" runat="server" class="it"></asp:Hyperlink></h4>
                                <div class="clubs" id="clubsItaly" runat="server">
                                    <div class="dropdown"><%= ClubListItaly.Items.Count%> clubs</div>
                                    <div class="list clearfix">
	                                    <asp:ListView ID="ClubListItaly" runat="server">                                                    
	                                        <LayoutTemplate>
						                        <ul> 
	                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
						                        </ul>
	                                        </LayoutTemplate>
	
	                                        <ItemTemplate>
	                                            <li>
	                                                <a href="<%#(Container.DataItem as ClubLinkItem).Cluburl.Rendered %>" <%# (Container.DataItem as ClubLinkItem).IsFirst? "class=\"external club it first\"" : "class=\"external club it\"" %>><%#(Container.DataItem as ClubLinkItem).Clubname.Rendered%></a>
	                                            </li>
	                                        </ItemTemplate>
	                                    </asp:ListView>
	                            	</div>
                                </div>                               
                            </li>
							<li class="spain">
                                <h4><asp:Hyperlink id="lnkSpain" runat="server" class="es"></asp:Hyperlink></h4>
                                <div class="clubs" id="clubsSpain" runat="server">
                                    <div class="dropdown"><%= ClubListSpain.Items.Count%> clubs</div>
                                    <div class="list clearfix">
	                                    <asp:ListView ID="ClubListSpain" runat="server">                                                    
	                                        <LayoutTemplate>
						                        <ul> 
	                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
						                        </ul>
	                                        </LayoutTemplate>
	                                        <ItemTemplate>
	                                            <li>
	                                                <a href="<%#(Container.DataItem as ClubLinkItem).Cluburl.Rendered %>" <%# (Container.DataItem as ClubLinkItem).IsFirst? "class=\"external club es first\"" : "class=\"external club es\"" %>><%#(Container.DataItem as ClubLinkItem).Clubname.Rendered%></a>
	                                            </li>
	                                        </ItemTemplate>
	                                    </asp:ListView>
	                            	</div>
                                </div>
                            </li>
							<li class="portugal">
                                <h4><asp:Hyperlink id="lnkPortugal" runat="server" class="po"></asp:Hyperlink></h4>
                                <div class="clubs" id="clubsPortugal" runat="server">
                                    <div class="dropdown"><%= ClubListPortugal.Items.Count%> clubs</div>
                                    <div class="list clearfix">
	                                    <asp:ListView ID="ClubListPortugal" runat="server">                                                    
	                                        <LayoutTemplate>
						                        <ul> 
	                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
						                        </ul>
	                                        </LayoutTemplate>
	
	                                        <ItemTemplate>
	                                            <li>
	                                                <a href="<%#(Container.DataItem as ClubLinkItem).Cluburl.Rendered %>" <%# (Container.DataItem as ClubLinkItem).IsFirst? "class=\"external club po first\"" : "class=\"external club po\"" %>><%#(Container.DataItem as ClubLinkItem).Clubname.Rendered%></a>
	                                            </li>
	                                        </ItemTemplate>
	                                    </asp:ListView>
	                            	</div>
                                </div>
                            </li>
							<li class="uk">
                                <h4><asp:Hyperlink id="lnkUnitedKingdon" runat="server" class="uk"></asp:Hyperlink></h4>
                                <div class="clubs" id="clubsUnitedKingdon" runat="server">
                                    <div class="dropdown"><%= ClubListUK.Items.Count%> clubs</div>
                                    <div class="list clearfix">
	                                    <asp:ListView ID="ClubListUK" runat="server" OnItemDataBound="ClubListUK_OnItemDataBound">                                                    
	                                        <LayoutTemplate>
						                        <ul> 
	                                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
						                        </ul>
	                                        </LayoutTemplate>
	                                        <ItemTemplate>
	                                            <li>
	                                                <asp:Literal ID="lnkClub" runat="server"></asp:Literal>
	                                            </li>
	                                        </ItemTemplate>
	                                    </asp:ListView>
	                            	</div>
                                </div>
                            </li>
						</ul>	
					</div>				
					<div class="australasia">
						<h3>Australasia</h3>
                        <h4><asp:Hyperlink id="lnkAustralia" runat="server" class="au"></asp:Hyperlink></h4>
                        <div class="clubs" id="clubsAustralia" runat="server">
                            <div class="dropdown"><%= ClubListAustralia.Items.Count%> clubs</div>
                            <div class="list clearfix">
	                            <asp:ListView ID="ClubListAustralia" runat="server">                                                    
	                                <LayoutTemplate>
						                <ul> 
	                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
						                </ul>
	                                </LayoutTemplate>
	
	                                <ItemTemplate>
	                                    <li>
	                                        <a href="<%#(Container.DataItem as ClubLinkItem).Cluburl.Rendered %>" <%# (Container.DataItem as ClubLinkItem).IsFirst? "class=\"external club au first\"" : "class=\"external club au\"" %>><%#(Container.DataItem as ClubLinkItem).Clubname.Rendered%></a>
	                                    </li>
	                                </ItemTemplate>
	                            </asp:ListView>
	                    	</div>
                        </div>
					</div>
				</div>
						
            </div> <!-- /content -->
        </div>
        <div class="wrapper">
		
			<div class="aside">
				<h2 class="members"><%= currentItem.Numberofmembers.Rendered%> <span>members</span></h2>
				<h2 class="clubs"><%= currentItem.Numberofclubs.Rendered%> <span>clubs</span></h2>
                <%= currentItem.Footertext.Rendered%>
			</div>
			
			<div class="footer">
				<p class="fl"><%= Translate.Text("© Copyright ##Year## Virgin Active. All rights reserved.")%></p>
				<p class="fr"><a href="<%= Settings.McCormackMorrisonUrl %>"><img src="/virginactive/images/portal/mm.png" alt="McCormack &amp; Morrison" /></a></p>
			</div>
	    </div>



	
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.js"></script>
	<script>window.jQuery || document.write("<script src='/virginactive/scripts/jquery-1.7.1.min.js'>\x3C/script>")</script>
	<script src="/virginactive/scripts/global/site-script.js"></script>

	<!--[if lt IE 7 ]>
    	<script src="scripts/libs/dd_belatedpng.js"></script>
    	<script>DD_belatedPNG.fix("img, .png_bg, div, span"); // Fix any <img> or .png_bg bg-images. Also, please read goo.gl/mZiyb </script>
  	<![endif]-->
    </form>
    <asp:placeholder ID="ScriptPh" runat="server" />
</body>
</html>
