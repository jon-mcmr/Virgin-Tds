<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacilitiesClassesNav.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.navigation.FacilitiesClassesNav" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/NavLinkSection.ascx" TagName="NavSection" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>


                        <li id="nav-faci" class="level1<%=ActiveFlag %>"><a href="<%= new PageSummaryItem(facilityAndClass.InnerItem.Children[0]).Url %>" id="nav-title-faci" class="drop"><%= facilityAndClass.NavigationTitle.Text %></a>
                            <div class="dropdown">
                                <div class="level2 nav-drop-col1">
                                    <h2><a href="<%=facilityLanding.Url %>"><%=facilityLanding.NavigationTitle.Raw %></a></h2>
                                    <asp:PlaceHolder ID="facilityPh" runat="server" />
                                </div>
                                <div class="level2 nav-drop-col2">
                                    <h2><a href="<%=classLanding.Url %>"><%=classLanding.NavigationTitle.Raw%></a></h2>
                                    <div Class="level2-col">
                                    <%= classOutput%>
                                    </div>
                                </div>
                                <div class="level2 nav-drop-col3">
                                    <div class="level2-col level2-col4">
                                        <h2><a href="<%= healthAndBeautyLanding.Url%>"><%= healthAndBeautyLanding.NavigationTitle.Text%></a></h2>
                                        <ul class="md-inner">
                                            <li><a href="<%= healthAndBeautyLanding.Url%>">Spa Treatments</a></li>
                                            <%= healthAndBeautyOutput%>
                                        </ul>
                                    </div>
                                </div>
                                 <div class="level2 nav-drop-col4">
                                    <div class="level2-col level2-col6">
                                    <h2><a href="<%= personalTrainingLanding.Url%>"><%= personalTrainingLanding.NavigationTitle.Text%></a></h2>                                       
                                    </div>
                                </div>
                                <div class="level2 nav-drop-col5">
                                    <div class="level2-col level2-col5">
                                        <va:NavSection 
                                            ID="KidsSection"
                                            runat="server"
                                            HeaderIsH2="true" />                                        
                                    </div>
                                </div>
                               
                                <div class="level2 nav-drop-club">
                                    <h2><a href="<%= widget.Widget.Buttonlink.Url %>"><%= widget.Widget.Heading.Text %></a></h2>
                                    <p class="bold"><%= widget.Subheading.Text %></p>
                                    <%= widget.Widget.Bodytext.Text %>                                    
                                    <p><a href="<%= widget.Widget.Buttonlink.Url %>" class="btn"><%= widget.Widget.Buttontext.Text %></a></p>
                                </div>
                            </div>
                        </li>