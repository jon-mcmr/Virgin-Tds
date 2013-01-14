<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberBenefits.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.MemberBenefits" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Membership" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
            <div id="content" class="layout">
                <div class="intro-membership">
                    <h2><%= PromoListing.PageSummary.NavigationTitle.Text %></h2>
                    <p><%= PromoListing.Teaser.Text %></p>
                </div>
                
                <div class="benefits-offers-list">
                    <asp:ListView ID="OfferList" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                        </LayoutTemplate>

                        <ItemTemplate>
                        <div class="benefits-offer offer-<%# (Container.DataItem as MemberPromotionItem).IsFirst? "primary" : "secondary" %>">
                            <%# (Container.DataItem as MemberPromotionItem).IsFirst ? (Container.DataItem as MemberPromotionItem).Image.RenderCrop("460x300") : (Container.DataItem as MemberPromotionItem).Image.RenderCrop("300x180")%>
                            <div class="offer-info">
                                <h2><%# (Container.DataItem as MemberPromotionItem).Title.Rendered %></h2>
                                <p class="intro"><%# (Container.DataItem as MemberPromotionItem).Teaser.Rendered%></p>
                                <div class="exclusive-box highlight-panel">
                                    <div class="title-note">
                                        <p><%# (Container.DataItem as MemberPromotionItem).Bannertext.Rendered %></p>
                                    </div>
                                    <p class="intro"><%# (Container.DataItem as MemberPromotionItem).Promotion.Rendered%></p>
                                </div>
                                <p class="description"><%# (Container.DataItem as MemberPromotionItem).Description.Rendered%></p>
                            </div>
                        </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                
                <div class="contrast-cta">
                    <p><span><%= Translate.Text("Ready to join?")%></span> <%= Translate.Text("Come visit us and we'll show you around.")%></p>
                    
                    <a href="<%= enqForm.Url + "?sc_trk=enq" %>" class="btn btn-cta-big"><%= Translate.Text("Book a Visit")%></a>
                </div>
                
                
            </div> <!-- /content -->
                  