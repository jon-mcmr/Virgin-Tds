<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColourMeFitImagePanel.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.navigation.ColourMeFitImagePanel" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>   
<%@ Import Namespace="mm.virginactive.common.Globalization" %>                    
                <section class="<%= CssClass %>">	
                    <a name="<%= FacilityModule.Name %>"></a>
                    <asp:Literal ID="heading1" runat="server" Visible="false"></asp:Literal>

                    <%= GetImage %>
                    <div class="panel-inner">
                        <asp:Literal ID="heading2" runat="server" Visible="false"></asp:Literal>
                        <p class="intro"><%= FacilityModule.Abstract.Subheading.Rendered%></p>
                        <div class="panel-content">							
                            <%= FacilityModule.Abstract.Summary.Text%>

                           <% if (!String.IsNullOrEmpty(DetailLinkURL))
                              { %>
                               <ul>
                                <li id="viewDetailsLink" runat="server" visible="false"><a href="<%= DetailLinkURL%>"><%=Translate.Text("View Details")%></a></li>
                            </ul>
                           <% } %>
                            <!--
                            <ul>
                                <li><a href="">Find classes</a></li>
                                <li><a href="">Find workouts</a></li>
                            </ul> -->
                        </div>
                           <% if (FacilityModule.Showcolourmefit.Checked)
                              { %>
                               <ul class="cmf">
                                <li class="card"><%= Translate.Text("Cardio")%> 
                                    <span class="cmf-wrap">
                                        <span class="percentage"><span class="pc"><%= FacilityModule.Cardiopercentage.Raw %></span><span class="unit">%</span></span>
                                    </span>
                                </li>
                                <li class="stre"><%= Translate.Text("Strength")%>  
                                    <span class="cmf-wrap">
                                        <span class="percentage"><span class="pc"><%= FacilityModule.Strengthpercentage.Raw %></span><span class="unit">%</span></span>
                                    </span>
                                </li>
                                <li class="mind"><%= Translate.Text("Mind & Body")%>  
                                    <span class="cmf-wrap">
                                        <span class="percentage"><span class="pc"><%= FacilityModule.Balancepercentage.Raw %></span><span class="unit">%</span></span>
                                    </span>
                                </li>
                            </ul>
                           <% } %>
                    </div>
                </section>                 
               