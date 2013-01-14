<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestArticleListRightCol.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.LatestArticleListRightCol" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
                    <div class="latest-articles-aside articles-list">
                        <h4 class="aside-header"><%= Translate.Text("LATEST ARTICLES")%></h4>
                        
                        <asp:ListView ID="ArticleList" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                            </LayoutTemplate>

                            <ItemTemplate>
                            <div class="post-summary highlight-panel">
                                <h5><a href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %></a></h5>
                                <p><%# (Container.DataItem as YourHealthArticleItem).Articleteaser.Text %></p>
                                <a class="moreinfo" href="<%# (Container.DataItem as YourHealthArticleItem).PageSummary.Url %>" title="<%# (Container.DataItem as YourHealthArticleItem).Articletitle.Text %>"><%= Translate.Text("Read more")%></a>
                            </div>
                            </ItemTemplate>
                        </asp:ListView>                       
                    </div>