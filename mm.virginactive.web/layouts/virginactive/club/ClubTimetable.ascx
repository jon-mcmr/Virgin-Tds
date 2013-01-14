<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubTimetable.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubTimetable" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%--            <script type="text/javascript">
                var _ClubID = '<%= clubId %>';
                var _ActiveTab = 'Week';
                var _filter = ''; 
            </script>--%>

            <div id="primary-l" class="timetable-wrap">
                  	<h2><%= timetableNameStr%></h2>
                    <div class="item-row">
                    	<p class="week"><%= dateRangeStr%></p>
                        <ul class="icon-list" id="lstIcons" runat="server">
                        	<li><a href="#" class="icon-print">Print</a></li>
                        	<!-- <li><a href="#" class="icon-pdf">Download PDF</a></li> -->
                        </ul>
                    </div>
                    <asp:PlaceHolder ID="filterPh" runat="server" />
                        <%= alert %>
                    <asp:PlaceHolder ID="resultPh" runat="server" />
                    <asp:Panel ID="ErrorMessage" runat="server" Visible="false">
                	    <div class="timetable-error">
						    <h2><asp:literal ID="TimetableUnavailableHeading" runat="server" /></h2>
                		    <p><asp:literal ID="TimetableUnavailableMessage" runat="server" /></p>
                	    </div>
                    </asp:Panel>
            </div>
