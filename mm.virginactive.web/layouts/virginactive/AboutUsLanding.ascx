<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AboutUsLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.AboutUsLanding" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.AboutUs" %>

			<div id="content" class="layout-panels">
                <asp:ListView ID="AboutUsListing" runat="server" OnItemDataBound="DetailList_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                    <section class="<%# (Container.DataItem as AboutUsDetailItem).Abstract.GetPanelCssClass() %>">
                        <div class="section-image-panel">	
                             <a href="<%# (Container.DataItem as AboutUsDetailItem).PageSummary.Url%>" title="<%= Translate.Text("Read more about")%> <%# (Container.DataItem as AboutUsDetailItem).PageSummary.NavigationTitle.Rendered %>"><asp:Literal ID="image" runat="server"></asp:Literal></a>
                            <div class="panel-arrow">
                                <a class="arrow" href="<%# (Container.DataItem as AboutUsDetailItem).PageSummary.Url%>" title="<%= Translate.Text("Read more about")%> <%# (Container.DataItem as AboutUsDetailItem).PageSummary.NavigationTitle.Rendered %>">
                                <span></span><%= Translate.Text("Read more")%></a>
                            </div>
                        </div>	
                        <div class="fl">
                            <h2><a href="<%# (Container.DataItem as AboutUsDetailItem).PageSummary.Url%>"><%# (Container.DataItem as AboutUsDetailItem).PageSummary.NavigationTitle.Rendered %></a></h2>
                        </div>
                        <div class="summary-text"><%# (Container.DataItem as AboutUsDetailItem).Abstract.Summary.Text%></div>
                    </section>
                    </ItemTemplate>
                </asp:ListView>
            </div> <!-- /content -->
