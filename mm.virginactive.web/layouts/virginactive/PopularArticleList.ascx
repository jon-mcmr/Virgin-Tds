<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopularArticleList.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.PopularArticleList" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>

						<div class="most-popular-panel">
							<h4 class="aside-header"><%= Translate.Text("MOST POPULAR")%></h4>
                       <asp:ListView ID="ArticleList" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>

                            <ItemTemplate>

							<div class="article-summary">
								<h4 class="section-name"><%# (Container.DataItem as YourHealthArticleItem).ListingName %></h4>
								
								<div class="article-text">
									<h5><a href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %></a></h5>
									<p>
										<%# (Container.DataItem as YourHealthArticleItem).Articleteaser.Text %>
									</p>
									<div class="article-cta-info">
										<a class="moreinfo" href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%= Translate.Text("Read more")%></a>
										<span class="author-name"><em><%= Translate.Text("by") %></em>&nbsp;<%# (Container.DataItem as YourHealthArticleItem).GetArticleAuthor()%></span>
									</div>
								</div>
							</div>

                            </ItemTemplate>

                       </asp:ListView>  


						</div>