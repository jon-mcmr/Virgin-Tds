<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.press.PressLanding" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Press" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>           
            <div id="content" class="layout">
                    
                <div class="press">
					<div id="primary-l" >    
	           			<p class="title-border"><%= Translate.Text("LATEST PRESS RELEASE")%></p>

                        <!-- For Update panel -->
                        <asp:ScriptManager ID="scriptMgr" runat="server" />

                        <asp:UpdatePanel runat="server" ID="ArticleUpdate">
                            <ContentTemplate>
                            <asp:ListView ID="ArticleList" runat="server" OnPagePropertiesChanging="ArticleList_PagePropertiesChanging">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                                </LayoutTemplate>

                                <ItemTemplate>
	                            <section <%# (Container.DataItem as PressArticleItem).PageSummary.IsFirst ? @"id=""top-listing""" : "" %> class="article-summary<%# (Container.DataItem as PressArticleItem).PageSummary.IsFirst ?"" : " listing" %>">
                            
	                                <p class="calendar">
	                                    <span class="day"><%# (Container.DataItem as PressArticleItem).Date.DateTime.ToString("MMM") %></span>
	                                    <span class="date"><%# (Container.DataItem as PressArticleItem).Date.DateTime.ToString("dd") %></span>
	                                    <span class="year"><%# (Container.DataItem as PressArticleItem).Date.DateTime.ToString("yyyy") %></span>
	                                </p>
	                                <div class="article-summary-main">
	                                    <h2><a href="<%# (Container.DataItem as PressArticleItem).PageSummary.Url %>"><%# (Container.DataItem as PressArticleItem).Articleheading.Raw %></a></h2>
	                                    <div class="article-summary-copy">
		 							        <%# (Container.DataItem as PressArticleItem).Image.RenderCrop("280x160", "fr") %>
	                                        <%# (Container.DataItem as PressArticleItem).Teaser.Rendered %>
									        <p><a href="<%# (Container.DataItem as PressArticleItem).PageSummary.Url %>" class="moreinfo"><%= Translate.Text("Read more") %></a></p>
	                                    </div>
	                                </div>
	                            </section>                                
                                </ItemTemplate>
                            </asp:ListView>

                            <nav class="pagination">
                                <div class="pagination-wrap">
                                    <asp:DataPager ID="ArticlePager" runat="server" PagedControlID="ArticleList" PageSize="4">
                                        <Fields>
                                                <asp:NextPreviousPagerField  ShowFirstPageButton="false" ShowNextPageButton="False" RenderDisabledButtonsAsLabels="true" ButtonCssClass="arrows arrow-prev" />
                                                <asp:NumericPagerField CurrentPageLabelCssClass="active" />
                                                <asp:NextPreviousPagerField ShowLastPageButton="false" ShowPreviousPageButton="False" ButtonCssClass="arrows arrow-next" RenderDisabledButtonsAsLabels="true"  />
                                        </Fields>
                                    </asp:DataPager> 
                                </div>                         
                            </nav> 
                            </ContentTemplate>
                        </asp:UpdatePanel>                    
	                </div>

	                <aside class="column-240">
                        <sc:Placeholder Key="RightColumn" runat="server" />
	                </aside>
				</div><!-- /press -->
                    
            </div> <!-- /content -->
                        