<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleBody.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ArticleBody" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>	
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
                    <section class="article-summary single-article">
                    
                        <div class="article-summary-main">
                            <h2><%= article.Articletitle.Text %></h2>
                            <div class="author-social">
                                <h4 class="section-name"><a href="<%= listing.PageSummary.Url %>"><%= listing.PageSummary.NavigationTitle.Text.ToUpper() %></a></h4>
                                <div class="article-author"><p><em><%= Translate.Text("by")%></em>&nbsp;<strong> <%= article.GetArticleAuthor() %></strong></p></div>

                                	<% if (showSocial == true)
                                           {%>
                                <ul class="social-info">
                                    <li class="social-twitter"><span><%= SocialMediaHelper.TweetButtonForUrl(articleUrl) %></span></li>
                                    <li class="social-facebook"><span><%= SocialMediaHelper.LikeButtonIFrame(articleUrl)%></span></li>
                                    <li class="social-googleplus"><span><%= SocialMediaHelper.GooglePlusButton %></span></li>
                                </ul>
                                        <%  }%>
                            </div>
                            <div class="article-text">
                                <p class="intro">
                                <%= article.Articleteaser.Text %>
                                </p>
                               <%= article.Articleuppercontent.Text %>    
                                <div class="article-image-caption">
                                    <%= article.Articleimage.RenderCrop("680x360") %>
                                    <p><%= article.Articleimagecaption.Text %></p>
                                </div>
                                <%= article.Articlelowercontent.Text %>                             
                            </div>
                        </div>
                        
                    </section>