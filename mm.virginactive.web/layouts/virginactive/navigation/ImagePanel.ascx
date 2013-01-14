<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImagePanel.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.navigation.ImagePanel" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>                    
    
 	
                    <section class="<%= CssClass %>">
					 	<div class="section-image-panel">	
							<a href="<%=contextItem.Url %>" title="<%= contextItem.NavigationTitle.Text%>"><%= GetImage %></a>
                            <div class="panel-arrow">
							    <a class="arrow" href="<%=contextItem.Url %>" title="<%= contextItem.NavigationTitle.Text%>"><span></span>Read more</a>
                            </div>
						</div>

						<div class="fl">
                            <h2><a href="<%=contextItem.Url %>"><%= contextItem.NavigationTitle.Text%></a></h2>
<%--
                        <asp:ListView ID="ChildLinks" runat="server">
                            <LayoutTemplate>
							<ul class="item">    
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>  
							</ul>                                                          
                            </LayoutTemplate>

                            <ItemTemplate>
                               <li><a href="<%#(Container.DataItem as PageSummaryItem).Url %>"><%#(Container.DataItem as PageSummaryItem).NavigationTitle.Text %></a></li>
                            </ItemTemplate>

                        </asp:ListView>--%>
                        </div>
						
                        <div class="summary-text"><p><%= new AbstractItem(contextItem.InnerItem).Summary.Rendered %></p></div>
						
					</section>
  