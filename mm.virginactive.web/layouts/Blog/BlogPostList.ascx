<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogPostList.ascx.cs" Inherits="Sitecore.Modules.Blog.Layouts.BlogPostList" %>
<%@ Import Namespace="Sitecore.Data" %>
<%@ Import Namespace="Sitecore.Modules.Blog.Managers" %>
<%@ Import Namespace="mm.virginactive.wrappers.EviBlog" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
            <asp:ScriptManager ID="scriptMgr" runat="server" />
            <!--
            <asp:UpdatePanel runat="server" ID="ArticleUpdate">
                <ContentTemplate> -->
                <asp:ListView ID="EntryList" runat="server" OnItemDataBound="EntryDataBound" OnPagePropertiesChanging="EntryList_PagePropertiesChanging">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <article role="article">
		                <p class="calendar">
			                <span class="day"><%#(Container.DataItem as BlogEntryItem).Created.ToString("MMM")%></span>
                            <span class="date"><%#(Container.DataItem as BlogEntryItem).Created.ToString("dd")%></span>
			                <span class="year"><%#(Container.DataItem as BlogEntryItem).Created.ToString("yyyy")%></span>				
		                </p>

                        <div class="article-content-wrap">
                            <header>
                                <div class="header-details">
                                    <h2>
                                        <a href="<%#(Container.DataItem as BlogEntryItem).Url %>"><%#(Container.DataItem as BlogEntryItem).Title.Rendered%></a>
                                    </h2>
                                    <p class="article-details"><span class="article-by"><%= Translate.Text("by") %></span><span class="bold"><%#(Container.DataItem as BlogEntryItem).GetBlogAuthor()%></span></p>
                                </div>
                            </header>

                            <div class="article-content">				                                                
				                <p>
					                <%#(Container.DataItem as BlogEntryItem).Introduction.Rendered%>
				                </p>
                                <%# (Container.DataItem as BlogEntryItem).Showimage.Checked ? (Container.DataItem as BlogEntryItem).Image.RenderCrop("610x300") : "" %>
                                 <p class="link-arrow"><a href="<%# Eval("Url") %>">Read more</a></p>
			                </div>
                            
                            <footer>
                                <div class="article-tags" id="articleTags" runat="server" visible="false">
									<h3><%= Translate.Text("Tags") %></h3>
									<ul>
                                    <asp:Repeater runat="server" ID="TagList">
                                        <ItemTemplate>
                                            <li class="tag-name">
                                                <asp:HyperLink runat="server" ID="TagLink" NavigateUrl='<%# GetTagUrl(Container.DataItem as string) %>'>
                                                    <%# Container.DataItem %>
                                                </asp:HyperLink>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>                                                                      
                                    </ul>
			                    </div>
                            </footer>

                        </div>
                    </article>

                </ItemTemplate>
                <EmptyDataTemplate>
                    <p><%= Translate.Text("no posts found!")%></p>
                </EmptyDataTemplate>
                </asp:ListView>

                <% if (ArticlePager.TotalRowCount > ArticlePager.PageSize)
                    {%>
                <nav class="pagination">
                    <div class="pagination-wrap">
                    <asp:DataPager ID="ArticlePager" runat="server" PagedControlID="EntryList" PageSize="4">
                        <Fields>
                                <asp:NextPreviousPagerField  ShowFirstPageButton="false" ShowNextPageButton="False" RenderDisabledButtonsAsLabels="true" ButtonCssClass="arrows arrow-prev" />
                                <asp:NumericPagerField CurrentPageLabelCssClass="active" />
                                <asp:NextPreviousPagerField ShowLastPageButton="false" ShowPreviousPageButton="False" ButtonCssClass="arrows arrow-next" RenderDisabledButtonsAsLabels="true"  />
                        </Fields>
                    </asp:DataPager>  
                    </div>                        
                </nav> 

                <% }%>
                <!--
                </ContentTemplate>
            </asp:UpdatePanel> -->
<!--
<div class="viewMoreWrapper">
    <a runat="server" id="ancViewMore" class="viewMore" href="#">View More</a>
    <img src="/sitecore modules/Blog/Images/ajax-loader.gif" class="loadingAnimation" alt="Loading..." />
</div>
-->