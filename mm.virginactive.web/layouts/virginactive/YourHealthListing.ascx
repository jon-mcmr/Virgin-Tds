<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YourHealthListing.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.YourHealthListing" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>

            <div id="content" class="layout">                
                <div class="articles-section full-width-float">
                    <div class="articles-section-header float-container">
                        <h2 class="fl section-head"><%= listing.PageSummary.NavigationTitle.Text %></h2>
                    </div>

                   <asp:ListView ID="ArticleList" runat="server">
                        <LayoutTemplate>
                        <div class="articles-list float-container">	
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                        </div>
                        </LayoutTemplate>

                        <ItemTemplate>
                        <section class="article-summary">
                            <a href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%# (Container.DataItem as YourHealthArticleItem).Articleimage.RenderCrop("220x120", "article-summary-image") %></a>
                            <div class="article-summary-main">
                                <h4 class="section-name"><%= listing.PageSummary.NavigationTitle.Text.ToUpper() %></h4>
                                    <div class="article-text">
                                        <h3><a href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %></a></h3>
                                        <div class="article-author"><p><em><%= Translate.Text("by") %></em>&nbsp;<strong><%# (Container.DataItem as YourHealthArticleItem).GetArticleAuthor()%></strong></p></div>
										<% if (showSocial == true)
                                           {%>
                                        <div class="article-social">
                                            <li class="social-twitter"><span><%#  SocialMediaHelper.TweetButtonForUrl((Container.DataItem as YourHealthArticleItem).PageSummary.QualifiedUrl)%></span></li>
                                            <li class="social-facebook"><span><%# SocialMediaHelper.LikeButtonForUrl((Container.DataItem as YourHealthArticleItem).PageSummary.QualifiedUrl)%></span></li>                                               
                                        </div>
                                        <%  }%>
                                        <p>
                                            <%# (Container.DataItem as YourHealthArticleItem).Articleteaser.Text %>
                                        </p>
                                        <p class="readmore"><a class="moreinfo" href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%= Translate.Text("Read more")%></a></p>
                                        
                                    </div>
                            </div>
                        </section>
                        </ItemTemplate>

                   </asp:ListView>                                       

                    </div>
                    <!--
                    <nav class="pagination">
                        <ul>
                            <li class="arrows arrow-prev"><a href="#" class="inactive">Prev</a></li>
                            <li><a href="#" class="active">1</a></span>
                            <li><a href="#">2</a></span>
                            <li><a href="#">3</a></span>
                            <li><a href="#">4</a></span>
                            <li><a href="#">5</a></span>
                            <li><a href="#">6</a></span>
                            <li class="arrows arrow-next"><a href="#" class="xxinactive">Next</a></li>
                        </ul>
                    </nav> -->
                    
                </div>
            </div>