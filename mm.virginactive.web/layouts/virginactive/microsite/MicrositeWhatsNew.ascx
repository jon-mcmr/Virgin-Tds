<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeWhatsNew.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeWhatsNew" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<div id="content">
	<div class="section news-section">
	    
        <div class="club-alert-panel">
            <asp:Panel ID="GlobalAlert" runat="server" CssClass="club-alert" Visible="false" />
            <asp:Panel ID="Alert" runat="server" CssClass="club-alert" Visible="false" />
        </div>

        <h1><%= ContextItem.Heading.Rendered %></h1>
        
        <asp:ScriptManager ID="scriptMgr" runat="server" />

        <asp:UpdatePanel runat="server" ID="ArticleUpdate">
            <ContentTemplate>
                <asp:ListView ID="NewList" runat="server" OnPagePropertiesChanging="NewList_PagePropertiesChanging">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <!--article-->
			            <div class="article <%# Container.DataItemIndex == 0 ? "first-child ": "" %>clearfix">
				            <p class="<%# (Container.DataItem as NewsItem).GetCssClass() %> ir category"><%# (Container.DataItem as NewsItem).Newscategory.Item.DisplayName %></p>
				
				            <div class="inside">
					            <p class="timestamp"><time pubdate="" datetime="<%# (Container.DataItem as NewsItem).Date.DateTime.ToString("yyyy-MM-ddTHH:mm")%>"><%# (Container.DataItem as NewsItem).Date.DateTime.ToString("dd MMM yyyy h:mm tt")%></time></p>
					            <h2><%# ((Container.DataItem as NewsItem).GetCssClass() == "icon-new")? String.Format(@"<span class=""new"">{0}</span>",Translate.Text("NEW")) : "" %> <%# (Container.DataItem as NewsItem).Title.Rendered %></h2>
					            <%# (Container.DataItem as NewsItem).Image.RenderCrop("220x120")%> 
                                <%# (Container.DataItem as NewsItem).Content.Text%>
				            </div>
			            </div>                       
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <p><%= Translate.Text("Sorry, we currently have no news to update you on!")%></p>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div class="pagination">
                    <div class="pagination-wrap">
                        <asp:DataPager ID="ArticlePager" runat="server" PagedControlID="NewList" PageSize="4">
                            <Fields>
                                    <asp:NextPreviousPagerField  ShowFirstPageButton="false" ShowNextPageButton="False" RenderDisabledButtonsAsLabels="true" ButtonCssClass="arrows arrow-prev" />
                                    <asp:NumericPagerField CurrentPageLabelCssClass="active" />
                                    <asp:NextPreviousPagerField ShowLastPageButton="false" ShowPreviousPageButton="False" ButtonCssClass="arrows arrow-next" RenderDisabledButtonsAsLabels="true"  />
                            </Fields>
                        </asp:DataPager>   
                    </div>                       
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>