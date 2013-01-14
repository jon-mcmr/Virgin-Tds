<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MicrositeTimetable.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.microsite.MicrositeTimetable" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<div id="content" class="allow_overflow clearfix">
 		<section class="single-article allow_overflow section clearfix">   
 			<h1><%= micrositeTimetableLanding.Heading.Rendered%></h1>
 		    <%= micrositeTimetableLanding.Introduction.Rendered%>
            <% if(SecondLevelElements.Items.Count > 0) 
                {
            %>
            <asp:ListView ID="SecondLevelElements" runat="server">
                <LayoutTemplate>
					<ul class="timetable-type clearfix">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder> 
					</ul>
                </LayoutTemplate>

                <ItemTemplate>
					<li><a href="<%# (Container.DataItem as PageSummaryItem).Url%>" <%# (Container.DataItem as PageSummaryItem).IsCurrent? "class=\"active\"" : "" %>>
						<%# (Container.DataItem as PageSummaryItem).NavigationTitle.Rendered %></a>
					</li>
                </ItemTemplate>
            </asp:ListView>
            <% } %>				
			
            <div id="primary-l" class="timetable-wrap">
                  	<%--<h2><%= timetableNameStr%></h2>--%>
  				    <div class="item-row clearfix">
  					    <p class="week"><%= dateRangeStr%></p>
  					    <ul class="icon-list" id="lstIcons" runat="server">
  						    <li><a href="#" class="icon-print"><%= Translate.Text("Print") %></a></li>
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
        </section>
 	</div>