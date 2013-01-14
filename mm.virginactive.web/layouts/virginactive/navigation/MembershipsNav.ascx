<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MembershipsNav.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.navigation.MembershipsNav" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
                        <li id="nav-memb" class="level1<%=ActiveFlag %>"><a href="<%= new PageSummaryItem(member.InnerItem.Children[0]).Url %>" id="nav-title-memb" class="drop"><%= member.NavigationTitle.Text %></a>
                          <div class="dropdown">
                                <% if (!String.IsNullOrEmpty(Header.Columnone.Raw))
                                   { %>
                                <div class="level2 nav-drop3 nav-drop3-col1">
                                    <%= Header.Columnone.Text %> 
                                </div>
                                <% } %>

                                <% if (!String.IsNullOrEmpty(Header.Columntwo.Raw))
                                   { %>
                                <div class="level2 nav-drop3 nav-drop3-col2">
                                    <%= Header.Columntwo.Text%> 
                                </div>
                                <% } %>

                                <% if (!String.IsNullOrEmpty(Header.Columnthree.Raw))
                                   { %>
                                <div class="level2 nav-drop3 nav-drop3-col3">
                                    <%= Header.Columnthree.Text %> 
                                </div>
                                <% } %>
                              
                                <div class="level2 nav-drop3 nav-drop3-col4">
                                    <h2><a href="<%= widget.Widget.Buttonlink.Url %>"><%= widget.Widget.Heading.Text %></a></h2>
                                    <p class="bold"><%= widget.Subheading.Text %></p>
                                    <%= widget.Widget.Bodytext.Text %>                                    
                                    <p><a href="<%= widget.Widget.Buttonlink.Url %>" class="btn"><%= widget.Widget.Buttontext.Text %></a></p>
                                </div> 
                            </div>
                        </li>