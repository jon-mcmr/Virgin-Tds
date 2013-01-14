<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubResults.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.clubfinder.ClubResults" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
            
            <script type="text/javascript">
                var thisUrl = '<%= thisUrl %>';
                var _ClubFindLat = <%= Lat.ToString() %>;
                var _ClubFindLng = <%= Lng.ToString() %>;
                var _SelectedClubLat = '';
                var _SelectedClubLng = '';
                var _SelectedClub = '';
                var _ClubFindLocation = '<%= location.ToString() %>'; 
                var _ActiveTab = 'list';
                var _filters = new Array(); 
                <% if(!String.IsNullOrEmpty(Filter)) { %>
                    _filters.push('<%= Filter %>');
                <% } %>
            </script>
            <div id="content" class="layout">
                <div id="enquiryform-results" class="clubfinder-enquiry">

                    <div id="clubfinderform" class="frm">
                        <fieldset class="find">									
                            <label for="findclub" class="ffb"><%= Translate.Text("I am looking for a club in:")%></label>
                            <input type="text" maxlength="40" id="findclub" class="searchclubs" placeholder="<%= Translate.Text("Postcode, city, town or area...") %>" />
                            <input type="hidden" id="input-entered" />
                        </fieldset>
                    </div>
                                                
                    <div class="frm">
                        <fieldset class="pickclub">
                            <div id="pickclub_or"><p class="ffb"><%= Translate.Text("OR")%></p></div>
                            <div class="wrap">
                                <label for="textFind" class="ffb"><%= Translate.Text("Pick your club from the list:")%></label>
                                <select class="chzn-clublist selectclub" id="clubFindSelect" runat="server">
                                </select>
                            </div>								
                        </fieldset>
                    </div>
                </div>
                
                <div id="primary-r" class="club-finder-results">
                    <div class="results-head">
						<h3><%= Translate.Text("Results")%></h3>
						
						<div class="view-links">
							<span class="clubs-list-view"><a href="" class="active"><%= Translate.Text("List View")%></a></span>
							<span class="clubs-map-view"><a href=""><%= Translate.Text("Map View")%></a></span>
                            <!--
						    <div class="matching-club-note">
							    <p id="result-count"><%= resultsStr %></p>
						    </div> -->
						</div>
					</div>
					<div id="results">
                    <asp:PlaceHolder ID="resultPh" runat="server" />
                    </div>
                </div> <!-- / primary-r -->

                <aside class="clubfinder">
					<h3 class="results-side-header-tall"><%= Translate.Text("Your search")%></h3>
                    <div id="filter-results-panel">
                    	<div class="filter-results filter-results-top">
                        <% if(!String.IsNullOrEmpty(location)) { %>
                            <h3><%= Translate.Text("Location")%></h3>
                            <p><%= location %></p>
                        <% } %>

                        </div>                       
                    </div>
                    <sc:Placeholder Key="ResultsLeftColumn" runat="server" />                 
                </aside>

            </div> <!-- /content -->