<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubTimetableFilter.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.ClubTimetableFilter" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

                    
                    <div class="item-row day_selector clearfix">
                    	<p><%= Translate.Text("Show")%>:</p>
                    	<ul id="select_day">
                        	<li><a href="#timetable_week" class="active"><%= Translate.Text("Week")%></a></li>
                        	<li><a href="#timetable_mon"><%= Translate.Text("Mon")%></a></li>
                        	<li><a href="#timetable_tue"><%= Translate.Text("Tue")%></a></li>
                        	<li><a href="#timetable_wed"><%= Translate.Text("Wed")%></a></li>
                        	<li><a href="#timetable_thu"><%= Translate.Text("Thu")%></a></li>
                        	<li><a href="#timetable_fri"><%= Translate.Text("Fri")%></a></li>
                        	<li><a href="#timetable_sat"><%= Translate.Text("Sat")%></a></li>
                        	<li><a href="#timetable_sun"><%= Translate.Text("Sun")%></a></li>
                        </ul>
                    </div>