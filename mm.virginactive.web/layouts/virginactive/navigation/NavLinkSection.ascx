<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavLinkSection.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.navigation.NavLinkSection" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
                                                <%= HeaderIsH2? "<h2>":"<h3>" %>
                                                    <% if (HeaderNavigable)
                                                       { %>
                                                    <a href="<%=contextItem.Url %>"><%= contextItem.NavigationTitle.Text%></a>                                                    
                                                    <% }
                                                       else
                                                      { %>
                                                      <%= contextItem.NavigationTitle.Text%>
                                                    <%} %>
                                                <%= HeaderIsH2? "</h2>":"</h3>" %>

                                                 <asp:ListView ID="NavItems" runat="server">
                                                    
                                                    <LayoutTemplate>
					                                    <ul> 
                                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
					                                    </ul>
                                                    </LayoutTemplate>

                                                    <ItemTemplate>
                                                        <li>
                                                            <a href="<%#(Container.DataItem as PageSummaryItem).Url %>"><%#(Container.DataItem as PageSummaryItem).NavigationTitle.Rendered %></a>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:ListView>
