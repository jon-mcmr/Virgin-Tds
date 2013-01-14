<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileClubClasses.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileClubClasses" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.orm.Timetable" %>
<%@ Import Namespace="mm.virginactive.webservices.virginactive.classtimetable" %>

		<div class="content day_classes" id="pnlClasses" runat="server">
            <h1 class="clubname"><a href="<%= ClubHomeUrl %>"><%= ClubName %></a></h1>
            <h2 class="icon"><span><a href="<%= ClubTimetablesUrl %>"><%= Translate.Text("Timetables")%></a></span></h2>

            <% if(SecondLevelElements.Items.Count > 0) 
                {
            %>
                <asp:ListView ID="SecondLevelElements" runat="server">
                <LayoutTemplate>
					<div class="button_tab clearfix"> 
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
					</div>
                </LayoutTemplate>

                <ItemTemplate>

                    <a href="<%# (Container.DataItem as PageSummaryItem).Url%>?day=<%= day.ToString()%>" <%# (Container.DataItem as PageSummaryItem).IsFirst? (Container.DataItem as PageSummaryItem).IsCurrent? "class=\"button first active half\"" : "class=\"button first half\"" :  (Container.DataItem as PageSummaryItem).IsCurrent? "class=\"button active half\"" : "class=\"button half\""%>>
                        <span><strong><%# (Container.DataItem as PageSummaryItem).NavigationTitle.Rendered %></strong></span>
                    </a>

                </ItemTemplate>
            </asp:ListView>
            
            <% } %>			
       		
            <%= alert %>

       		<div class="clearfix">
                <asp:Literal ID="ltrPrevious" runat="server"></asp:Literal>
                <asp:Literal ID="ltrNext" runat="server"></asp:Literal>
       		</div>
			<h2 class="title"><%= dateStr%></h2>

            <% if (rptClass.Items != null && rptClass.Items.Count > 0) 
                {
            %>
			<ul class="data_list">
                <asp:Repeater runat="server" ID="rptClass">
                    <ItemTemplate>
				        <li>
					        <div class="title"><%# (Container.DataItem as Class).StartTime.Value.ToString("HH:mm")%> <strong><%# (Container.DataItem as Class).Classname%></strong></div>
					        <div class="info"><%# (Container.DataItem as Class).Studio%> <%=Translate.Text("with") %> <%# (Container.DataItem as Class).Teacher%> <span><%# (Container.DataItem as Class).Duration%> <%=Translate.Text("mins") %></span></div>
				        </li>	
                    </ItemTemplate>
                </asp:Repeater>								
			</ul>
            <% }
               else
               { 
               %>	
		    <div class="content no-classes" id="pnlUnavailable" runat="server">			
			    <div class="data_block">
				     <div class="data_content">
					    <p class="fourofour icon"></p>
					    <div class="subtitle">
                            <p><asp:literal ID="TimetableUnavailableMessage" runat="server" /></p>
					    </div>
				    </div>
			    </div>
		    </div>
            <% } %>
			<div id="pnlBookings" runat="server">
				<h3><%= Translate.Text("Bookings")%></h3>
				<a href="tel:<%= currentClub.Memberstelephonenumber.Rendered.Replace(" ", "") %>" class="button phone_number large_copy"><span><strong><%= Translate.Text("Call us now")%> <%= currentClub.Memberstelephonenumber.Rendered%></strong></span></a>
				<div id="pnlBookOnline" runat="server" visible="false">
					<div class="divider geolocation">
						<div class="text">or</div>
					</div>            
					<a href="<%= BookOnlineUrl %>" class="button red arrow large_copy"><span><strong><%= Translate.Text("Book online now")%></strong></span></a>
				</div>
			</div>
		</div>
