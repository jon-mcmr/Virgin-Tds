<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileClubTimetable.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileClubTimetable" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
		<div class="content" id="pnlClasses" runat="server">
            <h1 class="clubname"><a href="<%= ClubHomeUrl %>"><%= ClubName %></a></h1>
			
			<h2 class="icon"><span><%= Translate.Text("Timetables")%></span></h2>
					
			<h2 class="title"><%= dateRangeStr%></h2>			
                <asp:ListView ID="TimetableDayList" ItemPlaceholderID="TimetableDayListPh" runat="server" OnItemDataBound="TimetableDayList_OnItemDataBound">
                    <LayoutTemplate>
                        <ul class="data_list day_list">
                            <asp:PlaceHolder ID="TimetableDayListPh" runat="server" />
                        </ul>
                    </LayoutTemplate>

                    <ItemTemplate>                            
                        <li>
                            <asp:Literal ID="ltrTimetableLink" runat="server"></asp:Literal>
                        </li>
                    </ItemTemplate>
                </asp:ListView>  			
		</div>

		<div class="content" id="pnlUnavailable" runat="server" visible="false">
			<h1 class="clubname"><a href="<%= ClubHomeUrl %>"><%= ClubName %></a></h1>
			<h2 class="icon"><span><%= Translate.Text("Timetables")%></span></h2>
			
			<div class="data_block">
				 <div class="data_content">
					<p class="fourofour icon"><%= Translate.Text("Sorry, our class timetable<br /> is not yet available.")%></p>
					<div class="subtitle">
                        <p><%= Translate.Text("Please call the club on")%> <span><%= clubMemberPhone%></span> <%= Translate.Text("to find out more about our class timetable.")%></p>
					</div>
				</div>
			</div>
		</div>
