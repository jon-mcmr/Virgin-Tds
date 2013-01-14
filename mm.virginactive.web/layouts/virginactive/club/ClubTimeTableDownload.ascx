<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubTimeTableDownload.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubTimeTableDownload" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs" %>

            <div id="primary-l">
	        <div class="timetable-es">
				<h2><%= currentItem.PageSummary.NavigationTitle.Rendered%></h2>
                <asp:ListView ID="DownloadModuleListing" runat="server" OnItemDataBound="DownloadModuleListing_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
	                    <section class="club-article">
                            <asp:Literal ID="image" runat="server"></asp:Literal>
	                        <div class="inner">
	                             <h3><%# (Container.DataItem as TimetableDownloadModuleItem).Abstract.Subheading.Rendered %></h3>
                                 <%# (Container.DataItem as TimetableDownloadModuleItem).Abstract.Summary.Text%>
	                             <p class="read-more">
                                    <%# (Container.DataItem as TimetableDownloadModuleItem).Timetable.Raw.Equals("") ? "" : String.Format(@" <p><a href=""{0}"" class=""btn btn-download-sm"">{1}</a></p>", (Container.DataItem as TimetableDownloadModuleItem).Timetable.MediaUrl, Translate.Text("Download PDF Timetable"))%>                                       
                                 </p>
	                        </div>
	                	</section>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            </div>


