<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberLandingCorporate.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.MemberLandingCorporate" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Membership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
            <% if (!isClubSection)
                { %>
            <div id="content" class="layout">
                <% } %> 	
				<div class="membership-corp">
                    <section class="membs-type">
                        <h2><%= new MembershipCorporateLandingItem(contextGroupOrSharedLevel).Subheading.Rendered%></h2>
                        <div class="content-panel">
                            <%= new MembershipCorporateLandingItem(contextGroupOrSharedLevel).Introduction.Rendered%>
                        </div>
						<div class="offers">
							<h3><%= Translate.Text("What we offer")%></h3>
                             <%= new MembershipCorporateLandingItem(contextGroupOrSharedLevel).Benefitslist.Rendered%>
						</div>
                    </section>
					<section class="howtojoin">
						<h3><%= Translate.Text("How to join")%></h3>
                        <asp:ListView ID="WidgetList" runat="server" OnItemDataBound="WidgetList_OnItemDataBound">
                            <LayoutTemplate><asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder></LayoutTemplate>
                            <ItemTemplate>
							    <div class="membs-cta<%# (Container.DataItem as LinkWidgetItem).IsLast ? @" last" : ""  %>">
								    <h4><%# (Container.DataItem as LinkWidgetItem).Widget.Heading.Rendered %></h4>
                                    <p><%# (Container.DataItem as LinkWidgetItem).Widget.Bodytext.Rendered %></p>
	                        	    <a href="<%# (Container.DataItem as LinkWidgetItem).Widget.Buttonlink.Url %>" class="btn btn-cta-big"><%# (Container.DataItem as LinkWidgetItem).Widget.Buttontext.Rendered %></a>                                    
							    </div>
                            </ItemTemplate>
                            <ItemSeparatorTemplate>
                                <p class="select-membs"><%= Translate.Text("OR")%></p>
                            </ItemSeparatorTemplate>
                        </asp:ListView>
					</section>
                </div>
            <% if (!isClubSection)
                { %>
            </div>
                <% } %> 
            
