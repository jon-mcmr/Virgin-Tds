<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActiveMatters.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.sitemap.SiteActiveMatters" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/NavLinkSection.ascx" TagName="NavSection" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.EviBlog" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
                       <section>
                        <%= HeaderIsH2? "<h2>":"<h3>" %><a href="<%= yourHealth.Url %>"><%= yourHealth.NavigationTitle.Text %></a><%= HeaderIsH2? "</h2>":"</h3>" %>

                        <va:NavSection 
                        ID="HealthArticles"
                        runat="server" />

                        <va:NavSection 
                        ID="HealthTools"
                        runat="server" />
                        
                        <!-- Workouts section removed     
                         -->    
                      <section>
                        <h3><a href="<%= blogItem.Url %>"><%=blogItem.NavigationTitle.Text %></a></h3> 
                                    
                        <asp:ListView ID="BlogPostList" runat="server">
                            <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </ul>                
                            </LayoutTemplate>

                            <ItemTemplate>                           
                                <li><a href="<%# (Container.DataItem as BlogEntryItem).PageSummary.Url %>"><%# (Container.DataItem as BlogEntryItem).Title.Text %></a></li>
                            </ItemTemplate>
                        </asp:ListView>     
                    </section>