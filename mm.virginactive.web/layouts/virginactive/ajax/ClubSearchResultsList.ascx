<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubSearchResultsList.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.ClubSearchResultsList" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
                   
					<script type="text/javascript">
					    var _resultString = '<%= matchingResults %>';
                    </script>
                    
					<div class="club-results-list">
					
                        <asp:ListView ID="ClubList" runat="server" OnItemDataBound="ClubList_OnItemDataBound">
                        
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>

                            <ItemTemplate>
						        <div class="map-overlay-parent club-summary<%# (Container.DataItem as Club).IsNearest? " nearest" : "" %>">
				                    <%# (Container.DataItem as Club).IsNearest? @"<div class=""title-note""><p>Nearest</p></div>" : "" %>
                                    <asp:Literal ID="ltrClubImageLink" runat="server"></asp:Literal>
							        <ul class="club-info">
								        <li class="club-name"><asp:Literal ID="ltrClubTitleLink" runat="server"></asp:Literal></li>
                                        <li class="club-distance"><%# (Container.DataItem as Club).DistanceFromSource.ToString() %> miles</li>
								        <li class="club-details">
                                            <asp:Literal ID="EsportaFlag" runat="server"></asp:Literal>
                                            <asp:Literal ID="ClassicClubFlag" runat="server"></asp:Literal>
                                	        <p class="club-address"><asp:Literal ID="Address" runat="server"></asp:Literal></p>
                                            <p class="club-contact rTapNumber<%# (Container.DataItem as Club).ClubItm.ResponseTapCode.Rendered %>"><%# (Container.DataItem as Club).ClubItm.Salestelephonenumber.Text %></p>
                                        </li>       
                                                                
                                        <li class="club-showmap"><a href="#" class="va-overlay-link" data-lat="<%# (Container.DataItem as Club).ClubItm.Lat.Raw %>" data-lng="<%# (Container.DataItem as Club).ClubItm.Long.Raw %>"><%= Translate.Text("Show on Map")%></a></li>
								        
                                        <% if (Convert.ToBoolean(Settings.ShowClubFinderIcons))
                                           { %>
                                        <li class="club-activities">
                                             <asp:ListView ID="FaclityList" ItemPlaceholderID="ItemPlaceholder2" runat="server" DataSource='<%# (Container.DataItem as Club).SearchFacilities %>'>
                                                <LayoutTemplate>							        
								                    <ul>
                                                        <asp:PlaceHolder ID="ItemPlaceholder2" runat="server" />
								                    </ul>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <li class="<%# (Container.DataItem as ClubFacility).FacilityCssClass %><%# (Container.DataItem as ClubFacility).IsLast? " last" : "" %>"><%# (Container.DataItem as ClubFacility).FacilityName %></li>
                                                </ItemTemplate>
                                            </asp:ListView>
								        </li>
                                        <% } %>
                                        <asp:Literal ID="ltrClubLinks" runat="server"></asp:Literal>
                                        <li class="club-visitlink"><asp:Literal ID="ltrClubCTALink" runat="server"></asp:Literal></li>                                        
							        </ul>
						        </div>                       
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <p><%= Translate.Text("Sorry we could not find any clubs that match your search criteria, please try selecting fewer filters.")%></p>
                            </EmptyDataTemplate>

                        </asp:ListView>
					</div>