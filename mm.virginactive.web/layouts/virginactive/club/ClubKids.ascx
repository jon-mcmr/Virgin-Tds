<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubKids.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubKids" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Kids" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>  
<%@ Import Namespace="mm.virginactive.common.Globalization" %> 
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.KidsShared" %>   

                <div id="primary-l">
                    <asp:PlaceHolder ID="ClubVSection" runat="server" Visible="false">

                    <section class="club-section  show-limit">
                        <h2><%= sharedClubVLanding.PageSummary.NavigationTitle.Raw %></h2>

                        <asp:ListView ID="ClubVList" runat="server" ItemPlaceholderID="ClubVPh">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ClubVPh" runat="server" />
                        </LayoutTemplate>

                        <ItemTemplate>
                        <section class="club-article">
                            <%# (Container.DataItem as KidsFeatureItem).Abstract.Image.RenderCrop("220x120")%> 
                            <div class="inner">
                                <h3><%# (Container.DataItem as KidsFeatureItem).Abstract.Subheading.Raw %></h3>
                                <%# (Container.DataItem as KidsFeatureItem).Abstract.Summary.Text %>
                            </div>
                        </section>
                        </ItemTemplate>
                        </asp:ListView>
                    </section>   
                                                       
                    </asp:PlaceHolder>
                    
                    <asp:PlaceHolder ID="ActiveSection" runat="server" Visible="false">
                    
                    <section class="club-section show-limit">
                        <h2><%= sharedActiveALanding.PageSummary.NavigationTitle.Raw%></h2>

                        <asp:ListView ID="ActiveAList" runat="server" ItemPlaceholderID="ActiveAPh">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ActiveAPh" runat="server" />
                        </LayoutTemplate>

                        <ItemTemplate>
                        <section class="club-article">
                            <%# (Container.DataItem as KidsFeatureItem).Abstract.Image.RenderCrop("220x120")%> 
                            <div class="inner">
                                <h3><%# (Container.DataItem as KidsFeatureItem).Abstract.Subheading.Raw %></h3>
                                <%# (Container.DataItem as KidsFeatureItem).Abstract.Summary.Text %>
                            </div>
                        </section>
                        </ItemTemplate>
                        </asp:ListView>                  
                    </asp:PlaceHolder>
                    

                </div> <!-- / primary-l -->