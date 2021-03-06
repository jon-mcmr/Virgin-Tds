﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubSecondaryNavigation.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubSecondaryNavigation" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>	 
            <nav id="club" class="subnav">	
                <asp:ListView ID="TopElements" runat="server">
                    <LayoutTemplate>
					    <ul> 
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
					    </ul>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <li <%# (Container.DataItem as PageSummaryItem).IsLast? "class=\"last\"" : ""  %>>
                            <a href="<%#(Container.DataItem as PageSummaryItem).Url %>" <%# (Container.DataItem as PageSummaryItem).IsCurrent? String.Format(@"class=""{0} active""", (Container.DataItem as PageSummaryItem).GetIconCssClass() ) : String.Format(@"class=""{0}""", (Container.DataItem as PageSummaryItem).GetIconCssClass() )   %>><%#(Container.DataItem as PageSummaryItem).NavigationTitle.Rendered %></a>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </nav>				
            <asp:PlaceHolder ID="SubSubNav" runat="server" Visible="false">
            <% if(SecondLevelElements.Items.Count > 0) 
                {
            %>
            <nav class="sub-subnav">
                    <asp:ListView ID="SecondLevelElements" runat="server">
                    <LayoutTemplate>
					    <ul> 
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
					    </ul>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <li><a href="<%#(Container.DataItem as PageSummaryItem).Url %>" <%# (Container.DataItem as PageSummaryItem).IsCurrent? "class=\"active\"" : ""  %> ><%#(Container.DataItem as PageSummaryItem).NavigationTitle.Rendered %></a></li>
                    </ItemTemplate>
                </asp:ListView>
            </nav>
            <% } %>
            </asp:PlaceHolder>