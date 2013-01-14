<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubClasses.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubClasses" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

            <asp:ListView ID="JumpLinkList" runat="server" ItemPlaceholderID="JumpPlaceholder">
            <LayoutTemplate>
                <nav class="jump-nav">
                    <ul>
                <asp:PlaceHolder ID="JumpPlaceholder" runat="server" />
                    </ul>
                </nav>
            </LayoutTemplate>

            <ItemTemplate>
                <li><a href="#<%# (Container.DataItem as ClassesListingItem).Name %>"><%# (Container.DataItem as ClassesListingItem).PageSummary.NavigationTitle.Raw %></a></li>
            </ItemTemplate>
            </asp:ListView>

            <div id="primary-l">
                <asp:ListView ID="ClassListing" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                        
                    <ItemTemplate>
                    <h2 id="<%# (Container.DataItem as ClassesListingItem).Name %>"><%# (Container.DataItem as ClassesListingItem).PageSummary.NavigationTitle.Raw %></h2>
                    <section class="club-section">                        
                        <asp:ListView ID="ClassList" runat="server" ItemPlaceholderID="ItemPlaceholderFacility"
                        DataSource="<%# (Container.DataItem as ClassesListingItem).ClassList %>">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholderFacility" runat="server" />
                            </LayoutTemplate>   
                        
                            <ItemTemplate>
                            <section class="club-article">                                    
                                <h3><%# (Container.DataItem as ClassModuleItem).PageSummary.NavigationTitle.Raw %></h3>
                                <p><%# (Container.DataItem as ClassModuleItem).Summary.Text%></p>
							    <p class="moreinfo"><a href="<%= clubTimetable.Url %>?class=<%# (Container.DataItem as ClassModuleItem).Name %>" data-findclass="<%# (Container.DataItem as ClassModuleItem).PageSummary.NavigationTitle.Raw %>"><%= Translate.Text("View full timetable")%></a></p>
                            </section>
                            </ItemTemplate>     
                        </asp:ListView>
                    </section>
                    </ItemTemplate>
                </asp:ListView>
            </div>
                    