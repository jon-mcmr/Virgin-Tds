<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubWhatsNew.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubWhatsNew"%>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
                <div id="primary-l">
                    <div class="club-alert-panel">
                        <asp:Panel ID="GlobalAlert" runat="server" CssClass="club-alert" Visible="false" />
                        <asp:Panel ID="Alert" runat="server" CssClass="club-alert" Visible="false" />
                    </div>
                    <!-- For Update panel -->
                    <asp:ScriptManager ID="scriptMgr" runat="server" />
                    <script type="text/javascript" language="javascript">
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                        function EndRequestHandler(sender, args) { scroll(0, 600); }   //Anchor to show/scroll after Pagination
                    </script>
                    <asp:UpdatePanel runat="server" ID="ArticleUpdate">
                        <ContentTemplate>
                            <asp:ListView ID="NewList" runat="server" OnPagePropertiesChanging="NewList_PagePropertiesChanging">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                                </LayoutTemplate>
                                <ItemTemplate>
                  	            <section class="club-news <%# (Container.DataItem as NewsItem).GetCssClass() %>">
                            
                    	            <div class="inner">
                                        <p class="timestamp"><time pubdate datetime="<%# (Container.DataItem as NewsItem).Date.DateTime.ToString("yyyy-MM-ddTHH:mm")%>"><%# (Container.DataItem as NewsItem).Date.DateTime.ToString("dd MMM yyyy h:mm tt")%></time></p>
                                        <h2><%# ((Container.DataItem as NewsItem).GetCssClass() == "icon-new")? String.Format(@"<span class=""new"">{0}</span>",Translate.Text("NEW")) : "" %> <%# (Container.DataItem as NewsItem).Title.Rendered %></h2>
                                        <%# (Container.DataItem as NewsItem).Image.RenderCrop("220x120")%> 
                                        <%# (Container.DataItem as NewsItem).Content.Text%>
                                     </div>
                                                    
                  	            </section>                            
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <p><%= Translate.Text("Sorry, we currently have no news to update you on!")%></p>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <nav class="pagination">
                                <div class="pagination-wrap">
                                    <asp:DataPager ID="ArticlePager" runat="server" PagedControlID="NewList" PageSize="4">
                                        <Fields>
                                              <asp:NextPreviousPagerField  ShowFirstPageButton="false" ShowNextPageButton="False" RenderDisabledButtonsAsLabels="true" ButtonCssClass="arrows arrow-prev" RenderNonBreakingSpacesBetweenControls="False" />
                                              <asp:NumericPagerField CurrentPageLabelCssClass="active" RenderNonBreakingSpacesBetweenControls="False" />
                                              <asp:NextPreviousPagerField ShowLastPageButton="false" ShowPreviousPageButton="False" ButtonCssClass="arrows arrow-next" RenderDisabledButtonsAsLabels="true" RenderNonBreakingSpacesBetweenControls="False"  />
                                        </Fields>
                                    </asp:DataPager>   
                                </div>                       
                            </nav>
                        </ContentTemplate>
                    </asp:UpdatePanel>        
                </div> <!-- / primary-l -->