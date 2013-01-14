<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestArticleList.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.LatestArticleList" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>

					<div class="latest-articles full-width-float">
						<div class="latest-articles-header">
							<h3><%= Translate.Text("LATEST ARTICLES")%></h3>
							<div class="articles-categories">
								<ul>
									<li><em><%= Translate.Text("View all articles in")%></em></li>
                                    <asp:ListView ID="ListingList" runat="server">
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li><a href="<%# (Container.DataItem as YourHealthListingItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthListingItem).PageSummary.NavigationTitle.Text %>"><%# (Container.DataItem as YourHealthListingItem).PageSummary.NavigationTitle.Text %></a></li>
                                        </ItemTemplate>
                                    </asp:ListView>
								</ul>
							</div>
						</div>
											
						
                       <asp:ListView ID="ArticleList" runat="server" OnItemDataBound="ArticleList_OnItemDataBound">
                            <LayoutTemplate>
                            <div class="articles-list">	
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                            </div>
                            </LayoutTemplate>

                            <ItemTemplate>
                            <section class="article-summary">
                                <a href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%# (Container.DataItem as YourHealthArticleItem).Articleimage.RenderCrop("220x120", "article-summary-image")%></a>
                                <div class="article-summary-main">
                                    <h4 class="section-name"><a id="lnkParentListingUrl" runat="server" title="<%# (Container.DataItem as YourHealthArticleItem).ListingName %>"><%# (Container.DataItem as YourHealthArticleItem).ListingName %></a></h4>
                                     
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