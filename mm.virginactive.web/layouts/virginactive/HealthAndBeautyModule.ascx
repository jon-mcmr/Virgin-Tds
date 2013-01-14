<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HealthAndBeautyModule.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.HealthAndBeautyModule" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.HealthAndBeauty" %>
	        <section class="<%= CssClass %>" id="<%= HealthAndBeautyModuleInstance.Name.ToLower()%>">
	            <div <%= HealthAndBeautyModuleInstance.Flagasoffer.Checked ? "class=\"hb-offer\"" : "class=\"hb-no-offer\""  %>>
                    <%= HealthAndBeautyModuleInstance.Flagasoffer.Checked ? "<span class=\"img-note\">Heavenly Offer</span>" : ""%>
                    <asp:Literal ID="image" runat="server" />
	                <h3><%= HealthAndBeautyModuleInstance.Abstract.Subheading.Rendered%></h3>
                    <asp:ListView ID="OfferList" runat="server" ItemPlaceholderID="ItemPlaceholderOffers">
                        <LayoutTemplate>
                            <div class="offer">
                                <asp:PlaceHolder ID="ItemPlaceholderOffers" runat="server" />
                            </div>
                        </LayoutTemplate>   
                        
                        <ItemTemplate>
                                <p class="<%# (Container.DataItem as HealthAndBeautyOfferItem).GetOfferPanelCssClass()%>">
                                    <%# (Container.DataItem as HealthAndBeautyOfferItem).Description.Rendered %>
                                    <%# !string.IsNullOrEmpty((Container.DataItem as HealthAndBeautyOfferItem).Price.Rendered) ? "<em>" + (Container.DataItem as HealthAndBeautyOfferItem).Price.Rendered + "</em>" : ""%>
                                </p>

                        </ItemTemplate>     
                    </asp:ListView>
                    <%= HealthAndBeautyModuleInstance.Abstract.Summary.Rendered%>
				</div>
	        </section>

            