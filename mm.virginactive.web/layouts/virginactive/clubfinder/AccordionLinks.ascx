<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccordionLinks.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.clubfinder.AccordionLinks" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>						
                            <div class="accordion-item" rel="<%= ContextItem.NavigationTitle.Raw %>">
								<h5 class="closed"><%= ContextItem.NavigationTitle.Raw %></h5>
								<div class="accordion-body js-hide">
                                    <asp:ListView ID="LinkList" runat="server">
                                    <LayoutTemplate>
					                    <ul> 
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
					                    </ul>
                                    </LayoutTemplate>
									<ItemTemplate>
                                        <li>
                                             <a href="#" rel="<%# (Container.DataItem as PageSummaryItem).ID.ToShortID().ToString() %>"><%#(Container.DataItem as PageSummaryItem).NavigationTitle.Rendered %></a>
                                        </li>
                                    </ItemTemplate>
                                  </asp:ListView>
								</div>
                            </div>
							
                          