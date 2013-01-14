<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestPressReleases.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.press.LatestPressReleases" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Press" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
							<div class="list-panel">
		                        <h3 class="aside-header"><%= Translate.Text("LATEST PRESS RELEASES")%></h3>

                                <asp:ListView runat="server" ID="LatestPressArticleList">
                                    <LayoutTemplate>
                                        <ul>
                                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                                        </ul>
                                    </LayoutTemplate>

                                    <ItemTemplate>
		                            <li>
		                                <span><%# (Container.DataItem as PressArticleItem).Date.DateTime.ToString("MMM dd, yyyy") %></span>
                                        <h4><a href="<%# (Container.DataItem as PressArticleItem).PageSummary.Url %>"><%# (Container.DataItem as PressArticleItem).Articleheading.Raw %></a></h4>
		                                <a href="<%# (Container.DataItem as PressArticleItem).PageSummary.Url %>" class="moreinfo"><%= Translate.Text("Read more")%></a>
		                            </li>                                    
                                    </ItemTemplate>
                                </asp:ListView>
		                    </div>