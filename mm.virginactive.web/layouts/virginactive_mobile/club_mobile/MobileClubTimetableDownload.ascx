<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileClubTimetableDownload.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileClubTimetableDownload" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs" %>

		<div class="content timetables-download">
			<h1 class="clubname"><a href="<%= ClubHomeUrl %>"><%= ClubName %></a></h1>
			<h2 class="icon"><span>Timetables</span></h2>		
            <asp:ListView ID="DownloadModuleListing" runat="server">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                </LayoutTemplate> 
                <ItemTemplate>		
			        <div class="data_block">
				        <div class="data_content clearfix">
					        <div class="download-info">
						        <h3><%# (Container.DataItem as TimetableDownloadModuleItem).Timetablename.Rendered %></h3>
                                <%# (Container.DataItem as TimetableDownloadModuleItem).Abstract.Summary.Text%>
					        </div>					
				        </div>
				            <div class="data_footer">
                            <%# (Container.DataItem as TimetableDownloadModuleItem).Timetable.Raw.Equals("") ? "" : String.Format(@" <a href=""{0}"" class=""download"">{1}</a>", (Container.DataItem as TimetableDownloadModuleItem).Timetable.MediaUrl, Translate.Text("Download PDF"))%>                                       
				            </div>
			        </div>
                </ItemTemplate>
            </asp:ListView>
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

