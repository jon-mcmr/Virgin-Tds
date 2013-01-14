<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubSearchResultsMap.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.ClubSearchResultsMap" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>					
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
					<!-- Google Maps placeholder -->
					<div id="map-canvas" class="club-results-map-view">
						
					</div>
					
					<div id="va-map-panels" class="map-panels-holder">
						<!-- JS checks if this div is present and sets google map center to coordinates contained inside -->
						<div class="map-center">
							<input type="hidden" class="va-marker-lat" value="<%= Lat.ToString() %>" />
							<input type="hidden" class="va-marker-lng" value="<%= Lng.ToString() %>" />
						</div>
						
						<!-- assuming it'll be a list of map panels here which JS will go over and populate on google map -->
                        <asp:ListView ID="ClubList" runat="server" OnItemDataBound="ClubList_OnItemDataBound">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                            </LayoutTemplate>

                            <ItemTemplate>
						    <div class="map-info-panel">
							    <input type="hidden" class="va-marker-lat" value="<%# (Container.DataItem as Club).ClubItm.Lat.Raw %>" />
							    <input type="hidden" class="va-marker-lng" value="<%# (Container.DataItem as Club).ClubItm.Long.Raw %>" />
                                							
							    <div class="close"></div>
                                <div class="panel-details-wrap">
                                    <asp:Literal ID="ltrClubImageLink" runat="server"></asp:Literal>							        
							        <div class="club-info">
								        <h5><asp:Literal ID="ltrClubTitleLink" runat="server"></asp:Literal></h5>
								        <p>
									        <%# (Container.DataItem as Club).ClubItm.Addressline1.Text %>
                                            <%# !String.IsNullOrEmpty((Container.DataItem as Club).ClubItm.Addressline2.Text) ? "<br />" + (Container.DataItem as Club).ClubItm.Addressline2.Text : "" %>
                                            <%# !String.IsNullOrEmpty((Container.DataItem as Club).ClubItm.Addressline3.Text) ? "<br />" + (Container.DataItem as Club).ClubItm.Addressline3.Text : "" %>
                                            <%# !String.IsNullOrEmpty((Container.DataItem as Club).ClubItm.Addressline4.Text) ? "<br />" + (Container.DataItem as Club).ClubItm.Addressline4.Text : "" %>
								        </p>
								        <p class="club-contact">
									        <strong><%= Translate.Text("Sales")%></strong><span class="rTapNumber<%#  (Container.DataItem as Club).ClubItm.ResponseTapCode.Rendered %>"> <%# (Container.DataItem as Club).ClubItm.Salestelephonenumber.Text %></span><br />
									        <strong><%= Translate.Text("Direct")%></strong> <%# (Container.DataItem as Club).ClubItm.Memberstelephonenumber.Text %><br />
								        </p>
							        </div>
                                </div>
                                    <% if (Convert.ToBoolean(Settings.ShowClubFinderIcons))
                                       { %>
                                <asp:ListView ID="FaclityList" ItemPlaceholderID="ItemPlaceholder2" runat="server" DataSource='<%# (Container.DataItem as Club).SearchFacilities %>'>
                                    <LayoutTemplate>
								        <ul class="club-activities">
                                        <asp:PlaceHolder ID="ItemPlaceholder2" runat="server" />
								        </ul>
                                    </LayoutTemplate>

                                    <ItemTemplate>
                                        <li class="<%# (Container.DataItem as ClubFacility).FacilityCssClass %><%# (Container.DataItem as ClubFacility).IsLast? " last" : "" %>"><%# (Container.DataItem as ClubFacility).FacilityName %></li>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p><%= Translate.Text("Sorry we could not find any clubs that match your search criteria, please try selecting fewer filters.")%></p>
                                    </EmptyDataTemplate>
                                </asp:ListView>
							    <% } %>
							    <div class="arrow-down"></div>
						    </div>                            
                            </ItemTemplate>
                        </asp:ListView>