<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SecondaryNavigationBasic.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.SecondaryNavigationBasic" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>	             
            <asp:PlaceHolder ID="SubSubNav" runat="server" Visible="false">
            <nav class="sub-subnav" id="SecondLevelNavigation" runat="server">
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
            </asp:PlaceHolder>				

