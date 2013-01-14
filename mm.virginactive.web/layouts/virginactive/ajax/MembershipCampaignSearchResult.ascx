<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MembershipCampaignSearchResult.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.MembershipCampaignSearchResult" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>

					<div class="club-results-list">
					
                        <asp:ListView ID="ClubList" runat="server" OnItemDataBound="ClubList_OnItemDataBound">
                        
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>

                            <ItemTemplate>
							<div class="club-summary-wrap <%# (Container.DataItem as Club).IsNearest? "club-nearest" : "" %>">
						        <div class="map-overlay-parent club-summary">
				                    <%# (Container.DataItem as Club).IsNearest? @"<div class=""title-note""><p>Nearest</p></div>" : "" %>
							        <div class="details-wrap">
                                        <a href="<%# String.Format("{0}?cid={1}", TargetUrl,(Container.DataItem as Club).ClubItm.InnerItem.ID.ToShortID()) %>"><%# (Container.DataItem as Club).ClubItm.Clubimage.RenderCrop("180x120")%></a>
							            <ul class="club-info">
								            <li class="club-name"><a href="<%# String.Format("{0}?cid={1}", TargetUrl,(Container.DataItem as Club).ClubItm.InnerItem.ID.ToShortID()) %>"><%# (Container.DataItem as Club).ClubItm.Clubname.Text %></a></li>
                                            <li class="club-distance"><%# (Container.DataItem as Club).DistanceFromSource.ToString() %> miles</li>
								            <li class="club-type">
                                                <asp:Literal ID="EsportaFlag" runat="server"></asp:Literal>
                                                <asp:Literal ID="ClassicClubFlag" runat="server"></asp:Literal>
                                	            <p class="club-address"><asp:Literal ID="Address" runat="server"></asp:Literal></p>
                                            </li>       
                                                                
                                            <li class="club-showmap"><a href="#" class="va-overlay-link" data-lat="<%# (Container.DataItem as Club).ClubItm.Lat.Raw %>" data-lng="<%# (Container.DataItem as Club).ClubItm.Long.Raw %>"><%= Translate.Text("Show on Map")%></a></li>
								            <li class="club-contact"><%# (Container.DataItem as Club).ClubItm.Salestelephonenumber.Text %></li>
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
							            </ul>
                                    </div>
						        </div>
                                <div class="info-panel">
									<h3 class="price">
										<span class="save-from">save from</span>
										<span class="price-value">£<%# (Container.DataItem as Club).DoubleClubCost %></span>
									</h3>
									<a data-gaqlabel="MoreIsLess" data-gaqaction="EnquireNow" data-gaqcategory="CTA" class="btn btn-cta gaqTag" href="<%# String.Format("{0}?cid={1}", TargetUrl,(Container.DataItem as Club).ClubItm.InnerItem.ID.ToShortID()) %>">Enquire now</a>
								</div> 
                            </div>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <p>Sorry, we could not find any clubs that match your search criteria.  Please try selecting fewer filters.</p>
                            </EmptyDataTemplate>

                        </asp:ListView>
					</div>