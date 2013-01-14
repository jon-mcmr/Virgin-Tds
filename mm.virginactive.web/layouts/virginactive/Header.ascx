<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.Header" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>	
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %> 
<input type="hidden" id="hdnLastClubVisitedId" runat="server" class="lastClubVisitedId"/>
		<div class="header-top-nav clearfix">
			<div class="wrapper">
				<p><%= CurrentItem.TopStrapline.Rendered %></p>
				<asp:ListView ID="LinkList" ItemPlaceholderID="LinkListPh" runat="server" OnItemDataBound="LinkList_ItemDataBound">
					<LayoutTemplate>
					<ul>
						<asp:PlaceHolder ID="LinkListPh" runat="server" />
					</ul>
					</LayoutTemplate>

					<ItemTemplate>
						<asp:PlaceHolder runat="server" ID="ListItemNormal"><li></asp:PlaceHolder>
						<asp:PlaceHolder runat="server" ID="ListItemLast" Visible="False"><li class="last"></asp:PlaceHolder>
						<a href="<%# (Container.DataItem as LinkItem).Link.Url %>"><%# (Container.DataItem as LinkItem).Link.Field.Text%></a>
					</li>
					</ItemTemplate>
				</asp:ListView>
				
			</div>
		</div>
		<header id="HeaderContainer" class="main_header clearfix" runat="server">
            <input type="hidden" id="homePageUrl" runat="server" class="homePageUrl"/>
			<div class="container">
                <div class="header-top wrapper">
				    <a href="<%= HomeOrClubPageUrl %>" id="logo"><img src="/virginactive/images/logo.png" width="110" height="85" alt="<%= Translate.Text("Virgin Active") %>" /></a>
                    <asp:Literal ID="ltrHeaderText" runat="server" ></asp:Literal>
				    <nav>
                        <sc:sublayout path="/layouts/virginactive/PrimaryNavigation.ascx" id="primary" runat="server" />
                    </nav>
                    <p id="member-login">
                        <a href="#" class="btn btn-cta overlay-clubfilterlist" 
                        data-list-title="Member Login" data-list-label="My club is:" 
                        data-form-submit="true" data-list-url="MemberLoginUrl" 
                        data-list-intro="Please select your home club from the list below and we'll then take you through to your member’s area">
                        <%= Translate.Text("Member login")%>
                        </a>
                    </p>
                </div>
			</div>
		</header>
        
		<div id="searchbar">
			<div class="container">
                <fieldset>
                    <label for="search"><%= Translate.Text("Search Virgin Active")%></label>
                    <input type="text" id="search" maxlength="40" class="searchclubs" placeholder="<%= Translate.Text("Start typing here to find clubs, facilities or classes...")%>" />
                    <input type="hidden" id="AjaxText" />
                </fieldset>
                
                <div class="last-visited">
                    <a href="<%= clubFinder.Url %>" id="clubfinder-link"><%= Translate.Text("Club Finder")%></a>
                    <asp:PlaceHolder ID="phLastClubVisitedPrompt" runat="server" Visible="false">
                        <div id="clubfinder-panel" class="tooltip">
                            <div class="tooltip-arrow"></div>
                            <div id="clubfinder-inner">
                                <div class="close-btn"><a href="#">Close</a></div>
                                <p id="last-visited"><%= Translate.Text("You last visited") %>:</p>
                                <p id="last-clubname"><a href="<%= ClubLastVisitedUrl %>"><%= ClubName %></a></p>
                                <p id="last-homeclub"><a href="#"><%= Translate.Text("Make it your home club")%></a></p>
                                <p id="last-viewclub"><a href="<%= ClubLastVisitedUrl %>"><%= Translate.Text("View Club details")%></a></p>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div>
			</div>
		</div>
        
        
