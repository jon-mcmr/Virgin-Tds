<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubFinder.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.clubfinder.ClubFinder" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

            <script type="text/javascript">
                var _filters;
                var _ClubFindLocation;                 
            </script>
				<div id="content" class="layout-block">
					<div class="clubfinder-enquiry">
                        <div class="panel-l"></div>
                        
                        <div id="enquirypanel">
                            <div id="clubfinderform" class="frm">
                                <fieldset class="find">									
                                    <label for="findclub" class="ffb"><%= Translate.Text("I am looking for a club in:")%></label>
                                    <input type="text" id="findclub" maxlength="40" class="searchclubs" placeholder="<%= Translate.Text("Postcode, city, town or area...") %>" />
                                    <input type="hidden" id="input-entered" />
                                    <%--<input type="hidden" id="clubEmail" />--%>
                                    <h2>Popular searches:</h2>
                                    <ul>
                                    	<li><a href="/gyms/gyms-in-belfast">Belfast</a></li>
                                    	<li><a href="/gyms/gyms-in-birmingham">Birmingham</a></li>
                                    	<li><a href="/gyms/gyms-in-brighton">Brighton</a></li>
                                    	<li><a href="/gyms/gyms-in-bristol">Bristol</a></li>
                                    	<li><a href="/gyms/gyms-in-cardiff">Cardiff</a></li>
                                    	<li><a href="/gyms/gyms-in-coventry">Coventry</a></li>
                                    	<li><a href="/gyms/gyms-in-croydon">Croydon</a></li>
                                    	<li><a href="/gyms/gyms-in-derby">Derby</a></li>
                                    	<li><a href="/gyms/gyms-in-edinburgh">Edinburgh</a></li>
                                    	<li><a href="/gyms/gyms-in-glasgow">Glasgow</a></li>
                                    	<li><a href="/gyms/gyms-in-leeds">Leeds</a></li>
                                    	<li><a href="/gyms/gyms-in-london">London</a></li>
                                    	<li><a href="/gyms/gyms-in-manchester">Manchester</a></li>
                                    	<li><a href="/gyms/gyms-in-milton-keynes">Milton Keynes</a></li>
                                    	<li><a href="/gyms/gyms-in-nottingham">Nottingham</a></li>
                                    	<li><a href="/gyms/gyms-in-plymouth">Plymouth</a></li>
                                    	<li><a href="/gyms/gyms-in-sheffield">Sheffield</a></li>
                                    </ul>
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
					</div>
				</div> <!-- /content -->
       