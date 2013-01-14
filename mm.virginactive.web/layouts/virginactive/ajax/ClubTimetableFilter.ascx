<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubTimetableFilter.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.ClubTimetableFilter" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

                    
                    <div id="timetable-filter" class="filter-tabs">
                    	<p class="show"><%= Translate.Text("Show")%>:</p>
                    	<ul class="tab">
                        	<li><a href="#" class="show_all active" rel="week"><%= Translate.Text("Week")%></a></li>
                        	<li><a href="#" class="show_mon" rel="mon"><%= Translate.Text("Mon")%></a></li>
                        	<li><a href="#" class="show_tue" rel="tue"><%= Translate.Text("Tue")%></a></li>
                        	<li><a href="#" class="show_wed" rel="wed"><%= Translate.Text("Wed")%></a></li>
                        	<li><a href="#" class="show_thu" rel="thu"><%= Translate.Text("Thu")%></a></li>
                        	<li><a href="#" class="show_fri" rel="fri"><%= Translate.Text("Fri")%></a></li>
                        	<li><a href="#" class="show_sat" rel="sat"><%= Translate.Text("Sat")%></a></li>
                        	<li><a href="#" class="show_sun" rel="sun"><%= Translate.Text("Sun")%></a></li>
                        </ul>
                        <p class="filter-class"><a href="#" data-filterbyclass="<%= Translate.Text("Filter by class")%>" data-clearfilter="<%= Translate.Text("Clear filter")%>"><%= Translate.Text("Filter by class")%></a></p>
                    </div>