<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubFacilities.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubFacilities" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>

            <asp:ListView ID="JumpLinkList" runat="server" ItemPlaceholderID="JumpPlaceholder">
            <LayoutTemplate>
                <nav class="jump-nav">
                    <ul>
                <asp:PlaceHolder ID="JumpPlaceholder" runat="server" />
                    </ul>
                </nav>
            </LayoutTemplate>

            <ItemTemplate>
                <li><a href="#<%# (Container.DataItem as FacilitiesListingItem).Name %>"><%# (Container.DataItem as FacilitiesListingItem).PageSummary.NavigationTitle.Raw %></a></li>
            </ItemTemplate>
            </asp:ListView>
            
            <div id="primary-l">
                <asp:ListView ID="FacilityListing" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                        
                    <ItemTemplate>
                    <section class="club-section show-limit">
                        <h2 id="<%# (Container.DataItem as FacilitiesListingItem).Name %>"><%# (Container.DataItem as FacilitiesListingItem).PageSummary.NavigationTitle.Raw %></h2>
                        <asp:ListView ID="FacilityList" runat="server" ItemPlaceholderID="ItemPlaceholderFacility"
                        DataSource="<%# (Container.DataItem as FacilitiesListingItem).FacilityList %>">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholderFacility" runat="server" />
                            </LayoutTemplate>   
                        
                            <ItemTemplate>
                            <section class="club-article">
                                <% if (club.InnerItem.TemplateID.ToString() == ClassicClubItem.TemplateId)
                                   { %>
                                   <%# (Container.DataItem as FacilityModuleItem).Abstract.Classicimage.RenderCrop("220x120") %>
                                <% }
                                   else
                                   { %>
                                    <%# (Container.DataItem as FacilityModuleItem).Abstract.Image.RenderCrop("220x120") %>
                                <% } %>
                                <div class="inner">
                                    <h3><%# (Container.DataItem as FacilityModuleItem).PageSummary.NavigationTitle.Raw %></h3>
                                    <%# (Container.DataItem as FacilityModuleItem).Clublistingsummary.Text %>
                                </div>
                            </section>
                            </ItemTemplate>     
                        </asp:ListView>
                    </section>
                    </ItemTemplate>
                </asp:ListView>
            </div>