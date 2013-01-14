<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassListingSection.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.navigation.ClassListingSection" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.FacilitiesAndClassesTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>  
<%@ Import Namespace="mm.virginactive.common.Globalization" %>  

                <section class="width-full">
                    <h2 id="<%= ClassSubListing.Name %>"><%= ClassSubListing.PageSummary.NavigationTitle.Text%></h2>
                    <div class="panel-content">						
                    <%= classSubListing.Abstract.Image.RenderCrop("280x180") %>
                        <p class="intro"><%= classSubListing.Abstract.Subheading.Rendered %></p>
                        <%= classSubListing.Abstract.Summary.Rendered %>
                    </div>	
                    		
                    <div class="list-container">
                        <div class="title-note">
                            <p><%= Translate.Text("Classes") %></p>
                        </div>

                        <table class="classes-list" summary="<%= Translate.Text("Table listing all classes in the") %> <%= ClassSubListing.PageSummary.NavigationTitle.Text%> <%= Translate.Text("category") %>">
                            <thead>
                                <th class="class-details"></th>
                                <th class="cardio"><%= Translate.Text("Cardio")%></th>
                                <th class="strength"><%= Translate.Text("Strength")%></th>
                                <th class="mind"><%= Translate.Text("Mind & Body")%></th>
                            </thead>	

                        <asp:ListView ID="ClassList" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>

                            <ItemTemplate>
                            <tr class="cmf">
                                <td class="details">
                                    <h3 data-guid="<%#  (Container.DataItem as ClassModuleItem).InnerItem.ID.ToShortID() %>"><%# (Container.DataItem as ClassModuleItem).Title.Text %></h3>
                                    <p><%# (Container.DataItem as ClassModuleItem).Summary.Text %></p>
                                    <p><a href="#" class="va-overlay-link" ><%= Translate.Text("Find nearest class")%></a></p>
                                </td>
                                <%# (Container.DataItem as ClassModuleItem).Showcolourmefit.Checked ? String.Format("<td class='card'><span class='cmf-wrap'><span class='percentage'><span class='pc'>{0}</span><span class='unit'>%</span></span></span></td><td class='stre'><span class='cmf-wrap'><span class='percentage'><span class='pc'>{1}</span><span class='unit'>%</span></span></span></td><td class='mind'><span class='cmf-wrap'><span class='percentage'><span class='pc'>{2}</span><span class='unit'>%</span></span></span></td>", (Container.DataItem as ClassModuleItem).Cardiopercentage.Raw, (Container.DataItem as ClassModuleItem).Strengthpercentage.Raw, (Container.DataItem as ClassModuleItem).Balancepercentage.Raw) : ""%>
                            </tr>
                            </ItemTemplate>
                        </asp:ListView>
                        </table>
                    </div>
                </section>