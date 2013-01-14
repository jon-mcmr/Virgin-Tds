<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonPeople.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonPeople" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>

            <div class="mainContent" >                
                <h3><%= currentItem.Abstract.Subheading.Rendered %></h3>
                <div class="intro"><%= currentItem.BodyText.Rendered %></div>

                <asp:ListView ID="ProfileGroups" runat="server" OnItemDataBound="ProfileGroups_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="hr"></div>
                        <section class="profile-group">
                            <h3><%# (Container.DataItem as DropDownItem).Value.Rendered%></h3>
                            <asp:ListView ID="ProfilePanels" runat="server" OnItemDataBound="ProfilePanels_OnItemDataBound">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                                </LayoutTemplate> 
                                <ItemTemplate>
                                <section class="listing half-listing">
                                    <a class="open-overlay img-container" href="#people-content<%# (Container.DataItem as FeaturedProfileItem).Name.Replace(" ", "") %>">
                                        <%# (Container.DataItem as FeaturedProfileItem).Person.Profileimage.RenderCrop("180x120")%>
                                    </a>
                                    <div class="inner">
                                        <h4><%# (Container.DataItem as FeaturedProfileItem).Person.Firstname.Rendered%> <%# (Container.DataItem as FeaturedProfileItem).Person.Lastname.Rendered%></h4>
                                        <%# (Container.DataItem as FeaturedProfileItem).Intoduction.Rendered%>
                                        <p class="view"><a class="open-overlay" href="#people-content<%# (Container.DataItem as FeaturedProfileItem).Name.Replace(" ", "") %>">View profile</a></p>
                                    </div>
                                </section>
                                </ItemTemplate>
                            </asp:ListView>
                        </section>
                    </ItemTemplate>
                </asp:ListView>                     
                <!--PROFILES-->
                <%--<section class="group">
                <h3>Team Freespeed</h3>--%>

                <%--</section>--%>


            </div>
            <% if (clubFinder != null)
               {%>
                <div class="cta-panel">
                    <div class="cta-panel-inner">
                        <h3><%= clubFinder.Widget.Heading.Rendered%></h3>
                        <%= clubFinder.Widget.Bodytext.Rendered%>
                        <a href="<%= clubFinder.Widget.Buttonlink.Url%>" class="btn-club" target="_blank"><%= clubFinder.Widget.Buttontext.Rendered%></a>
                    </div>
               </div>
           <%} %>

          


