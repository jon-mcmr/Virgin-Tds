<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YourHealthNav.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.navigation.YourHealthNav" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/NavLinkSection.ascx" TagName="NavSection" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.EviBlog" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="Sitecore.Data.Items" %>

                        <li id="nav-your" class="level1<%=ActiveFlag %>"><a href="<%= new PageSummaryItem(yourHealth.InnerItem.Children[0]).Url %>" id="nav-title-your" class="drop"><%= yourHealth.NavigationTitle.Text %></a>
                          <div class="dropdown">
                                <div class="level2 nav-drop2 nav-drop2-col1">
                                    <va:NavSection 
                                    ID="HealthArticles"
                                    runat="server"
                                    HeaderIsH2="true" />
                                </div>
                                <div class="level2 nav-drop2 nav-drop2-col2">
                                    <va:NavSection 
                                    ID="HealthTools"
                                    runat="server"
                                    HeaderIsH2="true" />
                                </div>

                                <div class="level2 level2-col nav-drop2-col4">
                                    <h2><a href="<%= blogItem.Url %>"><%=blogItem.NavigationTitle.Text %></a></h2> 
                                    
                                    <asp:ListView ID="BlogPostList" runat="server">
                                        <LayoutTemplate>
                                        
                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                        
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                        <div class="nav-blog">
                                           <%# (Container.DataItem as BlogEntryItem).Showimage.Checked ? (Container.DataItem as BlogEntryItem).Image.RenderCrop("110x70") : "" %>
                                           <p><a href="<%# (Container.DataItem as BlogEntryItem).PageSummary.Url %>"><%# (Container.DataItem as BlogEntryItem).Title.Text %></a></p>
                                           <p class="date"><%# (Container.DataItem as BlogEntryItem).Created.ToString("dd MMMM yyyy") %></p>
                                           <p class="intro"><%# (Container.DataItem as BlogEntryItem).Introduction.Text %></p>
                                        </div>
                                        </ItemTemplate>
                                    </asp:ListView>          
                                                               
                                    <p class="read-more"><a href="<%= blogItem.Url %>"><%= Translate.Text("Read more blog posts")%></a></p>
                                </div>
                            </div>
                        </li>